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
    public class NotificacionesController : Controller
    {
        private IFIADRepository _repositorioFIAD;
        private SolicitudAdministrativaServices _servicioActas;
        private UsuarioServices _servicioUsuario;

        public NotificacionesController(IFIADRepository repositorioFIAD, SolicitudAdministrativaServices servicioActas, UsuarioServices servicioUsuario)
        {
            _repositorioFIAD = repositorioFIAD;
            _servicioActas = servicioActas;
            _servicioUsuario = servicioUsuario;
        }

        [Authorize]
        [HttpGet("Notificaciones")]
        public IActionResult ListarNotificacionesDocente()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            var actas = _servicioUsuario.ObtenerUsuarios();

                return View(actas);

        }

        [Authorize]
        [HttpGet("Usuarios/NotificacionesDocente")]
        public IActionResult VerActas()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            //var actasDTO = Mapper.Map<IEnumerable<ActaAdministradorDTO>>(actas);
                var actas = _servicioActas.ObtenerSolicitudesAdministrativos(usuarioActual);
                return Ok(actas); 
        }

    }
}
