using AutoMapper;
using CAEF.Models.DTO;
using CAEF.Models.Entities.CAEF;
using CAEF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAEF.Services;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Globalization;
using System.Linq;

namespace CAEF.Controllers
{
    public class CAEFController : Controller
    {        
        private IFIADRepository _repositorioFIAD;
        private CarreraRepository _repositorioCarrera;
        private MateriaRepository _repositorioMateria;

        private UsuarioServices _servicioUsuario;
        private RolServices _serviocioRol;
        private SolicitudAdministrativaServices _servicioSolicitud;
        public static IEnumerable<int> idsSolicitud;

        public CAEFController(IFIADRepository repositorioFIAD,
            UsuarioServices repositorioUsuario, RolServices servicioRol,
            SolicitudAdministrativaServices servicioSolicitud, CarreraRepository repositorioCarrera,
            MateriaRepository repositorioMateria)
        {            
            _repositorioFIAD = repositorioFIAD;
            _servicioUsuario = repositorioUsuario;
            _serviocioRol = servicioRol;
            _servicioSolicitud = servicioSolicitud;
            _repositorioCarrera = repositorioCarrera;
            _repositorioMateria = repositorioMateria;
        }

        [Authorize]
        [HttpGet("Acta")]
        public IActionResult SolicitarActaAdmin()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);

            if (usuarioActual.RolId == 1)
            {
                return View();
            }
            else
            {
                return View("SolicitarActaDocente");
            }
        }

        [Authorize]
        [HttpGet("RevisarActa/{id}")]
        public IActionResult RevisarActaAdmin()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);

            if (usuarioActual.RolId == 1)
            {
                return View();
            }
            else
            {
                return View("RevisarActaDocente");
            }
        }


        [Authorize]
        [HttpGet("VerActa/{id?}")]
        public IActionResult VerActa(int id)
        {
            #region >v
            //var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            //SolicitudDocente solicitud = _servicioSolicitud.ObtenerSolicitudDocente(id);

            //if (solicitud == null)
            //    return Redirect("/");
            //else if (usuarioActual.RolId == 1)
            //    return View();
            //else
            //    return Redirect("/");
            #endregion

            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            SolicitudAdmin solicitud = _servicioSolicitud.ObtenerSolicitudAministrativa(id);

            if (solicitud == null)
                return Redirect("/");
            else 
                return View();
        }

        [Authorize]
        [HttpGet("CAEF/VerActa/{id?}")]
        public IActionResult VerActaA(int id)
        {
            #region alomejor se ocupa :v
            //var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            //SolicitudDocente solicitud = _servicioSolicitud.ObtenerSolicitudDocente(id);

            //if (solicitud == null)
            //    return Redirect("/");
            //else if (usuarioActual.RolId == 1)
            //    return Ok(solicitud);
            //else
            //    return Redirect("/");
            #endregion

            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            SolicitudAdmin solicitud = _servicioSolicitud.ObtenerSolicitudAministrativa(id);

            if (solicitud == null)
                return Redirect("/");
            else
                return Ok(solicitud);
        }

        [Authorize]
        [HttpPost("CAEF/AgregarActaDocente")]
        public async Task<IActionResult> AgregarActaDocente([FromBody] ActaDocenteDTO acta)
        {
            var id = _servicioSolicitud.AgregarActaDocente(Mapper.Map<SolicitudDocente>(acta));

            if (id != 0)
            {
                await _servicioUsuario.GuardarCambios();
                return Ok(id);
            }
            else
            {
                return BadRequest("Ocurrió un error al agregar solicitud.");
            }
        }

        [Authorize]
        [HttpPost("CAEF/EditarActaDocente")]
        public async Task<IActionResult> EditarActaDocente([FromBody] ActaDocenteDTO acta)
        {
            var id = _servicioSolicitud.ActualizarActaDocente(Mapper.Map<SolicitudDocente>(acta));

            if (id != 0)
            {
                await _servicioUsuario.GuardarCambios();

                if(acta.IdEstado == 2)
                {
                    // Correo aquí
                }

                return Ok(id);
            }
            else
            {
                return BadRequest("Ocurrió un error al agregar solicitud.");
            }
        }

        [Authorize]
        [HttpPost("CAEF/AgregarActaAdmin")]
        public async Task<IActionResult> AgregarActaAdmin([FromBody] ActaAdminDTO acta)
        {
            _servicioSolicitud.AgregarActaAdmin(Mapper.Map<SolicitudAdmin>(acta));

            if (await _servicioUsuario.GuardarCambios())
            {
                return Ok("Se agregó la solicitud suxessful");
            }
            else
            {
                return BadRequest("Ocurrió un error al agregar solicitud.");
            }
        }

        [Authorize]
        [HttpPost("CAEF/AgregarSolicitudAlumno")]
        public async Task<IActionResult> AgregarSolicitudAlumno([FromBody] IEnumerable<SolicitudAlumnoDTO2> solicitud)
            {

            if (solicitud != null)
            {
                _servicioSolicitud.AgregarSolicitudAlumno(Mapper.Map<IEnumerable<SolicitudAlumno>>(solicitud),idsSolicitud);

                if (await _servicioUsuario.GuardarCambios())
                {
                    return Ok("Se agregó la solicitud correctamente.");
                }
            }
            return BadRequest("Ocurrió un error al agregar solicitud.");
        }

        [HttpGet("GenerarPDF/{id}")]
        public FileStreamResult GenerarPDF(int id)
        {
            SolicitudAdmin admin = _servicioSolicitud.ObtenerSolicitudAministrativa(id);
            SolicitudDocente docente = _servicioSolicitud.ObtenerSolicitudDocente(id);
            IEnumerable<SolicitudAlumno> alumnos = _servicioSolicitud.ObtenerSolicitudesAlumno(docente.Id);
            Usuario solicitante = _servicioUsuario.ObtenerUsuario(docente.IdEmpleado);
            Materia materia = _servicioUsuario.ObtenerMateria(docente.IdMateria);
            Carrera carrera = _servicioUsuario.ObtenerCarrera(docente.IdCarrera);
            TipoExamen tipoExamen = _servicioUsuario.ObtenerTipoExamen(docente.IdTipoExamen);
            SubtipoExamen subTipoExamen = _servicioUsuario.ObtenerSubtipoExamen(admin.IdSubtipoExamen);
            int grupoSolicitud = alumnos.FirstOrDefault().Alumno.Grupo;

            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.Letter);
            document.SetMargins(50, 50, 50, 50);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();
            document.AddTitle("Solicitud CAEF");
            Font TitleFont = new Font(Font.HELVETICA, 12, Font.BOLD, BaseColor.Black);
            Paragraph Title = new Paragraph("UNIVERSIDAD AUTÓNOMA DE BAJA CALIFORNIA", TitleFont);
            Title.Alignment = Element.ALIGN_CENTER;
            document.Add(Chunk.Newline);
            document.Add(Title);
            document.Add(Chunk.Newline);

            Font SubtitleFont = new Font(Font.HELVETICA, 12, Font.BOLD, BaseColor.Gray);
            Paragraph Subtitle = new Paragraph("COORDINACIÓN DE SERVICIOS ESTUDIANTILES Y GESTIÓN ESCOLAR", SubtitleFont);
            Paragraph Subtitle2 = new Paragraph("ACTA DE EVALUACION FINAL", SubtitleFont);
            Subtitle.Alignment = Element.ALIGN_CENTER;
            Subtitle2.Alignment = Element.ALIGN_CENTER;
            document.Add(Subtitle);
            document.Add(Subtitle2);
            document.Add(Chunk.Newline);

            Font TextoIzquierdaFont = new Font(Font.TIMES_ROMAN, 10, Font.BOLD, BaseColor.Black);
            Paragraph subDir = new Paragraph("SUBDIRECCIÓN", TextoIzquierdaFont);
            subDir.Alignment = Element.ALIGN_RIGHT;
            document.Add(subDir);

            DateTime fecha = DateTime.Now;
            Font TextoIzquierdaNo = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL, BaseColor.Black);
            int periodo = 1;
            if (fecha.Month > 6)
            {
                periodo = 2;
            }
            Paragraph No = new Paragraph("No.10/" + (fecha.Year - 2000) + "-" + periodo, TextoIzquierdaNo);
            No.Alignment = Element.ALIGN_RIGHT;
            document.Add(No);

            Paragraph Asunto = new Paragraph("Asunto: Control de Actas", TextoIzquierdaNo);
            Asunto.Alignment = Element.ALIGN_RIGHT;
            document.Add(Asunto);

            var mes = DateTime.Now.ToString("MMMM", new CultureInfo("es-ES"));
            Paragraph Fecha = new Paragraph("Ensenada, B.C, a " + fecha.Day + " de " + mes + " del " + fecha.Year, TextoIzquierdaFont);
            Fecha.Alignment = Element.ALIGN_RIGHT;
            document.Add(Fecha);
            document.Add(Chunk.Newline);

            Font examenFont = new Font(Font.TIMES_ROMAN, 10, Font.UNDERLINE, BaseColor.Black);
            Chunk examenUnderline = new Chunk(tipoExamen.Nombre + " (" + subTipoExamen.Nombre + ")", examenFont);
            Paragraph examenInfo = new Paragraph("EXAMEN: " + examenUnderline);
            examenInfo.Alignment = Element.ALIGN_CENTER;
            document.Add(examenInfo);
            document.Add(Chunk.Newline);

            PdfPTable table = new PdfPTable(11);
            table.TotalWidth = 500f;
            table.LockedWidth = true;
            Font TablaFont = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL, BaseColor.Black);

            PdfPCell claveUnidadT = new PdfPCell(new Phrase("CLAVE DE UNIDAD", TablaFont));
            claveUnidadT.Colspan = 1;
            claveUnidadT.HorizontalAlignment = 1;
            claveUnidadT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell unidadAcademicaT = new PdfPCell(new Phrase("UNIDAD ACADEMICA", TablaFont));
            unidadAcademicaT.Colspan = 4;
            unidadAcademicaT.HorizontalAlignment = 1;
            unidadAcademicaT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell claveCarreraT = new PdfPCell(new Phrase("CLAVE CARRERA", TablaFont));
            claveCarreraT.Colspan = 1;
            claveCarreraT.HorizontalAlignment = 1;
            claveCarreraT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell celdaCarreraT = new PdfPCell(new Phrase("CARRERA", TablaFont));
            celdaCarreraT.Colspan = 3;
            celdaCarreraT.HorizontalAlignment = 1;
            celdaCarreraT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell planEstudiosT = new PdfPCell(new Phrase("PLAN DE ESTUDIOS", TablaFont));
            planEstudiosT.Colspan = 2;
            planEstudiosT.HorizontalAlignment = 1;
            planEstudiosT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell claveUnidad = new PdfPCell(new Phrase(admin.ClaveUnidad));
            claveUnidad.Colspan = 1;
            claveUnidad.HorizontalAlignment = 1;
            PdfPCell unidadAcademica = new PdfPCell(new Phrase(admin.UnidadAcademica));
            unidadAcademica.Colspan = 4;
            unidadAcademica.HorizontalAlignment = 1;
            PdfPCell claveCarrera = new PdfPCell(new Phrase(carrera.Id + ""));
            claveCarrera.Colspan = 1;
            claveCarrera.HorizontalAlignment = 1;
            PdfPCell celdaCarrera = new PdfPCell(new Phrase(carrera.Nombre));
            celdaCarrera.Colspan = 3;
            celdaCarrera.HorizontalAlignment = 1;
            PdfPCell planEstudios = new PdfPCell(new Phrase(admin.PlanEstudios));
            planEstudios.Colspan = 2;
            planEstudios.HorizontalAlignment = 1;

            PdfPCell claveMateriaT = new PdfPCell(new Phrase("CLAVE DE MATERIA", TablaFont));
            claveMateriaT.Colspan = 1;
            claveMateriaT.HorizontalAlignment = 1;
            claveMateriaT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell MateriaT = new PdfPCell(new Phrase("MATERIA", TablaFont));
            MateriaT.Colspan = 5;
            MateriaT.HorizontalAlignment = 1;
            MateriaT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell SemestreT = new PdfPCell(new Phrase("SEMESTRE", TablaFont));
            SemestreT.Colspan = 1;
            SemestreT.HorizontalAlignment = 1;
            SemestreT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell GrupoT = new PdfPCell(new Phrase("GRUPO", TablaFont));
            GrupoT.Colspan = 1;
            GrupoT.HorizontalAlignment = 1;
            GrupoT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell CicloT = new PdfPCell(new Phrase("CICLO ESCOLAR", TablaFont));
            CicloT.Colspan = 1;
            CicloT.HorizontalAlignment = 1;
            CicloT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell PeriodoT = new PdfPCell(new Phrase("PERIODO", TablaFont));
            PeriodoT.Colspan = 2;
            PeriodoT.HorizontalAlignment = 1;
            PeriodoT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell claveMateria = new PdfPCell(new Phrase(materia.Id + ""));
            claveMateria.Colspan = 1;
            claveMateria.HorizontalAlignment = 1;
            PdfPCell Materia = new PdfPCell(new Phrase(materia.Nombre));
            Materia.Colspan = 5;
            Materia.HorizontalAlignment = 1;
            PdfPCell Semestre = new PdfPCell(new Phrase(admin.EtapaSemestre));
            Semestre.Colspan = 1;
            Semestre.HorizontalAlignment = 1;
            PdfPCell Grupo = new PdfPCell(new Phrase(grupoSolicitud + ""));
            Grupo.Colspan = 1;
            Grupo.HorizontalAlignment = 1;
            PdfPCell Ciclo = new PdfPCell(new Phrase(admin.CicloEscolar));
            Ciclo.Colspan = 1;
            Ciclo.HorizontalAlignment = 1;
            PdfPCell Periodo = new PdfPCell(new Phrase(docente.Periodo));
            Periodo.Colspan = 2;
            Periodo.HorizontalAlignment = 1;

            table.AddCell(claveUnidadT);
            table.AddCell(unidadAcademicaT);
            table.AddCell(claveCarreraT);
            table.AddCell(celdaCarreraT);
            table.AddCell(planEstudiosT);
            table.AddCell(claveUnidad);
            table.AddCell(unidadAcademica);
            table.AddCell(claveCarrera);
            table.AddCell(celdaCarrera);
            table.AddCell(planEstudios);

            table.AddCell(claveMateriaT);
            table.AddCell(MateriaT);
            table.AddCell(SemestreT);
            table.AddCell(GrupoT);
            table.AddCell(CicloT);
            table.AddCell(PeriodoT);
            table.AddCell(claveMateria);
            table.AddCell(Materia);
            table.AddCell(Semestre);
            table.AddCell(Grupo);
            table.AddCell(Ciclo);
            table.AddCell(Periodo);
            document.Add(table);
            document.Add(Chunk.Newline);

            PdfPTable tablaAlumnos = new PdfPTable(11);
            tablaAlumnos.TotalWidth = 500f;
            tablaAlumnos.LockedWidth = true;

            PdfPCell ContadorT = new PdfPCell(new Phrase("NO.", TablaFont));
            ContadorT.Colspan = 1;
            ContadorT.HorizontalAlignment = 1;
            ContadorT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell NombreAlumnoT = new PdfPCell(new Phrase("NOMBRE DEL ALUMNO", TablaFont));
            NombreAlumnoT.Colspan = 6;
            NombreAlumnoT.HorizontalAlignment = 1;
            NombreAlumnoT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell MatriculaT = new PdfPCell(new Phrase("MATRICULA", TablaFont));
            MatriculaT.Colspan = 2;
            MatriculaT.HorizontalAlignment = 1;
            MatriculaT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell CalificacionT = new PdfPCell(new Phrase("CALIFICACION", TablaFont));
            CalificacionT.Colspan = 2;
            CalificacionT.HorizontalAlignment = 1;
            CalificacionT.BackgroundColor = new BaseColor(193, 193, 193);

            tablaAlumnos.AddCell(ContadorT);
            tablaAlumnos.AddCell(NombreAlumnoT);
            tablaAlumnos.AddCell(MatriculaT);
            tablaAlumnos.AddCell(CalificacionT);

            int count = 1;
            foreach (SolicitudAlumno solicitud in alumnos)
            {
                PdfPCell Contador = new PdfPCell(new Phrase(count++ + ""));
                Contador.Colspan = 1;
                Contador.HorizontalAlignment = 1;
                PdfPCell NombreAlumno = new PdfPCell(new Phrase(solicitud.Alumno.Nombre + " " + solicitud.Alumno.ApellidoP + " " + solicitud.Alumno.ApellidoM));
                NombreAlumno.Colspan = 6;
                NombreAlumno.HorizontalAlignment = 1;
                PdfPCell Matricula = new PdfPCell(new Phrase(solicitud.Alumno.Id + ""));
                Matricula.Colspan = 2;
                Matricula.HorizontalAlignment = 1;
                PdfPCell Calificacion = new PdfPCell(new Phrase(solicitud.Alumno.Promedio + ""));
                Calificacion.Colspan = 2;
                Calificacion.HorizontalAlignment = 1;

                tablaAlumnos.AddCell(Contador);
                tablaAlumnos.AddCell(NombreAlumno);
                tablaAlumnos.AddCell(Matricula);
                tablaAlumnos.AddCell(Calificacion);
            }

            document.Add(tablaAlumnos);
            document.Add(Chunk.Newline);

            PdfPTable tablaMotivo = new PdfPTable(1);
            tablaMotivo.TotalWidth = 500f;
            tablaMotivo.LockedWidth = true;
            PdfPCell MotivoT = new PdfPCell(new Phrase("MOTIVO", TablaFont));
            MotivoT.Colspan = 1;
            MotivoT.HorizontalAlignment = 1;
            MotivoT.BackgroundColor = new BaseColor(193, 193, 193);
            PdfPCell Motivo = new PdfPCell(new Phrase(docente.Motivo));
            Motivo.Colspan = 1;
            Motivo.HorizontalAlignment = 0;

            tablaMotivo.AddCell(MotivoT);
            tablaMotivo.AddCell(Motivo);
            document.Add(tablaMotivo);

            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

        [Authorize]
        [HttpPost("CAEF/ObtenerIdSolicitud")]
        public async Task<IActionResult> ObtenerIDS([FromBody] IEnumerable<int> Ids)
        {

                idsSolicitud = Ids;

           return Ok("Se obtuvieron los Ids");

        }

        [Authorize]
        [HttpPost("CAEF/RemoverSolcitudAlumno")]
        public async Task<IActionResult> RemoverSolicitudAlumno([FromBody] SolicitudAlumnoDTO2 solicitudAlumno)
        {

            if (solicitudAlumno != null)
            {
                _servicioSolicitud.BorrarSolcitudAlumno(Mapper.Map<SolicitudAlumno>(solicitudAlumno));

                if (await _servicioUsuario.GuardarCambios())
                {
                    return Ok();
                }
            }
            return BadRequest("Ocurrió un error al remover la solicitud.");
        }

        [Authorize]
        [HttpGet("CAEF/Roles")]
        public IActionResult VerRoles()
        {
            var roles = _serviocioRol.ObtenerRoles();
            return Ok(roles);
        }

        [Authorize]
        [HttpGet("CAEF/Carreras")]
        public IActionResult VerCarreras()
        {
            var carreras = _repositorioCarrera.BuscarTodos();
            return Ok(carreras);
        }

        [Authorize]
        [HttpGet("CAEF/Materias")]
        public IActionResult VerMaterias()
        {
            var materias = _repositorioMateria.BuscarTodos();
            return Ok(materias);
        }

        [Authorize]
        [HttpGet("CAEF/Subtipos")]
        public IActionResult VerSubtipos()
        {
            var subtipos = _servicioSolicitud.ObtenerSubtiposExamen();
            return Ok(subtipos);
        }

        [Authorize]
        [HttpGet("CAEF/Tipos")]
        public IActionResult VerTipos()
        {
            var tipos = _servicioSolicitud.ObtenerTiposExamen();
            return Ok(tipos);
        }

        [Authorize]
        [HttpGet("/CAEF/RevisarActa/{id}")]
        public IActionResult RevisarActaAdmin(int id)
        {
            var acta = _servicioSolicitud.ObtenerActaActual(id);
            return Ok(acta);
        }

        [Authorize]
        [HttpGet("/caef/ObtenerAlumno/{id}")]
        public IActionResult obtenerAlumnodeActa(int id)
        {
            var alumno = _servicioSolicitud.ObtenerAlumnos(id);
            var alumnosDTO = Mapper.Map<IEnumerable<SolicitudAlumnoDTO>>(alumno);
            return Ok(alumnosDTO);
        }
    }
}
