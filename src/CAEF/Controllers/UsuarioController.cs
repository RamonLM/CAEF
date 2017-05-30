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
    public class UsuarioController : Controller
    {        
        private IFIADRepository _repositorioFIAD;
        private UsuarioServices _servicioUsuario;        

        public UsuarioController(IFIADRepository repositorioFIAD, UsuarioServices repositorioUsuario)
        {            
            _repositorioFIAD = repositorioFIAD;
            _servicioUsuario = repositorioUsuario;
        }

        [Authorize]
        [HttpGet("Usuarios")]
        public IActionResult ListarUsuarios()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            var usuarios = _servicioUsuario.ObtenerUsuarios();
            var usuariosDTO = Mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            if (usuarioActual.RolId == 1)
            {
                return View(usuariosDTO);
            }
            else
            {
                return Redirect("/");
            }
        }

        [Authorize]
        [HttpGet("Usuarios/Listar")]
        public IActionResult VerUsuarios()
        {
            var usuarios = _servicioUsuario.ObtenerUsuarios();
            var usuariosDTO = Mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDTO);
        }

        [Authorize]
        [HttpPost("Usuarios/Editar")]
        public async Task<IActionResult> EditarUsuarios([FromBody] UsuarioDTO usuario)
        {
            _servicioUsuario.EditarUsuario(Mapper.Map<Usuario>(usuario));
            if (await _servicioUsuario.GuardarCambios())
            {
                return Ok("El usuario fue modificado exitosamente");
            }
            return BadRequest("Ocurrió un error al modificar usuario");
        }

        [Authorize]
        [HttpPost("Usuarios/Borrar")]
        public async Task<IActionResult> BorrarUsuarios([FromBody] UsuarioDTO usuario)
        {
            _servicioUsuario.BorrarUsuario(Mapper.Map<Usuario>(usuario));
            if (await _servicioUsuario.GuardarCambios())
            {
                return Ok("Se ha eliminado el usuario.");
            }
            return BadRequest("Error al borrar el usuario");
        }

        [Authorize]
        [HttpGet("AgregarUsuario")]
        public IActionResult AgregarUsuario()
        {
            var usuarioActual = _servicioUsuario.UsuarioAutenticado(User.Identity.Name);
            if (usuarioActual.RolId == 1)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [Authorize]
        [HttpPost("Usuarios/Agregar")]
        public async Task<IActionResult> AgregarUsuario([FromBody] UsuarioDTO usuario)
        {
            var usuarioDuplicado = _servicioUsuario.UsuarioDuplicado(usuario.Correo);
            var usuarioExisteFIAD = _repositorioFIAD.UsuarioExiste(usuario.Correo);
            var usuarioExisteUABC = _servicioUsuario.UsuarioExiste(usuario.Correo);

            if (!usuarioExisteUABC) return BadRequest("El usuario no es miembro de UABC.");
            if (!usuarioExisteFIAD) return BadRequest("El usuario no es miembro de FIAD.");
            if (usuarioDuplicado) return BadRequest("El usuario ya está registrado en el sistema.");

            _servicioUsuario.AgregarUsuario(Mapper.Map<Usuario>(usuario));

            if (await _servicioUsuario.GuardarCambios())
            {
                return Ok("Se agregó el usuario correctamente.");
            }
            else
            {
                return BadRequest("Ocurrió un error al agregar usuario.");
            }
        }

    }
}
