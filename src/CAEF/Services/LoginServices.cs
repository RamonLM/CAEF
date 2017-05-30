using CAEF.Models.DTO;
using CAEF.Models.Entities.UABC;
using CAEF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Services
{
    public class LoginServices
    {        
        private IFIADRepository _repositorioFIAD;
        private IUABCRepository _repositorioUABC;
        private UsuarioServices _servicioUsuario;
        private SignInManager<UsuarioUABC> _signIn;

        public LoginServices(
            SignInManager<UsuarioUABC> signInManager,
            IFIADRepository repositorioFIAD,
            IUABCRepository repositorioUABC,            
            UsuarioServices repositorioUsuario)
        {
            _repositorioFIAD = repositorioFIAD;
            _repositorioUABC = repositorioUABC;            
            _signIn = signInManager;
            _servicioUsuario = repositorioUsuario;
        }

        public async Task<string> Login(LoginDTO login)
        {
            var username = login.Email.Split('@')[0];

            if (_repositorioUABC.UsuarioExiste(login.Email))
            {
                var signIn = await _signIn.PasswordSignInAsync(username,
                                                     login.Password,
                                                     true, false);
                if (signIn.Succeeded)
                {
                    if (_repositorioFIAD.UsuarioExiste(login.Email))
                    {
                        if (_servicioUsuario.UsuarioDuplicado(login.Email))
                        {
                            //return Ok();
                            return null;
                        }
                        else
                        {
                            await _signIn.SignOutAsync();
                            return "El usuario no se encuentra registrado en el sistema.";
                            //return BadRequest("El usuario no se encuentra registrado en el sistema.");
                        }
                    }
                    else
                    {
                        await _signIn.SignOutAsync();
                        return "El usuario no pertenece a FIAD.";
                        //return BadRequest("El usuario no pertenece a FIAD.");
                    }
                }
                else
                {
                    await _signIn.SignOutAsync();
                    return "Ocurrió un error al iniciar sesión. Favor de verificar los datos.";
                    //return BadRequest("Ocurrió un error al iniciar sesión. Favor de verificar los datos.");
                }
            }
            else
            {
                return "El usuario no pertenece a UABC.";
                //return BadRequest("El usuario no pertenece a UABC.");
            }
        }
    }
}
