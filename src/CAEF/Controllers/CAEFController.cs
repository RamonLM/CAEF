using AutoMapper;
using CAEF.Models.DTO;
using CAEF.Models.Entities.CAEF;
using CAEF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAEF.Services;

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
