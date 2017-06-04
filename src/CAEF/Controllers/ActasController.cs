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
    public class ActasController : Controller
    {
        private IFIADRepository _repositorioFIAD;
        private SolicitudAdministrativaServices _servicioActas;
        private UsuarioServices _servicioUsuario;

        public ActasController(IFIADRepository repositorioFIAD, SolicitudAdministrativaServices servicioActas, UsuarioServices servicioUsuario)
        {
            _repositorioFIAD = repositorioFIAD;
            _servicioActas = servicioActas;
            _servicioUsuario = servicioUsuario;
        }

        [Authorize]
        [HttpGet("Actas")]
        public IActionResult ListaSolicitudesAdmin()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            var actas = _servicioUsuario.ObtenerUsuarios();

            if (usuarioActual.RolId == 1)
            {
                return View(actas);
            }
            else
            {
                return View("ListarSolicitudesDocente");
            }

        }

        [Authorize]
        [HttpPost("Usuarios/Actas")]
        public IActionResult VerActas([FromBody] FiltrosDTO s)
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            //var actasDTO = Mapper.Map<IEnumerable<ActaAdministradorDTO>>(actas);
            if (s != null) 
            {
                #region inicializar nullos
                s.docente = string.IsNullOrEmpty(s.docente.Trim()) ? null : s.docente.Trim();
                s.materia = string.IsNullOrEmpty(s.materia.Trim()) ? null : s.materia.Trim();
                s.tipoExamen = string.IsNullOrEmpty(s.tipoExamen.Trim()) ? null : s.tipoExamen.Trim();
                s.periodo = string.IsNullOrEmpty(s.periodo.Trim()) ? null : s.periodo.Trim();
                s.semestre = string.IsNullOrEmpty(s.semestre.Trim()) ? null : s.semestre.Trim();
                s.estado = string.IsNullOrEmpty(s.estado.Trim()) ? null : s.estado.Trim();
                #endregion


                var actas = _servicioActas.MostrarSolicitudesAdministrativos(usuarioActual, s.fecha, s.docente, s.materia, s.tipoExamen, s.periodo, s.semestre, s.estado);
                return Ok(actas);
            }
            else
            {
                var actas = _servicioActas.MostrarSolicitudesAdministrativos(usuarioActual, null, null, null, null, null, null, null);
                return Ok(actas);
            }
           
        }

        [Authorize]
        [HttpGet("Usuarios/ActasD")]
        public IActionResult VerActas()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            //var actasDTO = Mapper.Map<IEnumerable<ActaAdministradorDTO>>(actas);
            var actas = _servicioActas.ObtenerSolicitudesDocente(usuarioActual);
            return Ok(actas);
        }

    }
}
