using CAEF.Models.Contexts;
using CAEF.Models.Entities.UABC;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CAEF.Models.Seed
{
    public class SemillaUABC
    {
        private UsuarioUABCContext _context;
        private UserManager<UsuarioUABC> _userManager;

        public SemillaUABC(UsuarioUABCContext context, UserManager<UsuarioUABC> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /*
         * Genera datos automáticamente dentro de la base de
         * datos (UsuariosUABC) en caso de encontrarse vacía
         */
        public async Task GeneraDatosSemilla()
        {
            // Checa si la base de datos se encuentra vacía
            if (await _userManager.FindByEmailAsync("rlopez1@uabc.edu.mx") == null)
            {
                var usuarioA = new UsuarioUABC()
                {
                    Matricula = 338323,
                    UserName = "rlopez1",
                    Nombre = "José Ramón",
                    ApellidoP = "López",
                    ApellidoM = "Madueño",
                    Email = "rlopez1@uabc.edu.mx"
                };

                var usuarioB = new UsuarioUABC()
                {
                    Matricula = 335127,
                    UserName = "samuel.parra",
                    Nombre = "César Samuel",
                    ApellidoP = "Parra",
                    ApellidoM = "Salas",
                    Email = "samuel.parra@uabc.edu.mx"
                };

                var usuarioC = new UsuarioUABC()
                {
                    Matricula = 331364,
                    UserName = "celso.figueroa",
                    Nombre = "Celso",
                    ApellidoP = "Figueroa",
                    ApellidoM = "Jacinto",
                    Email = "celso.figueroa@uabc.edu.mx"
                };

                var usuarioD = new UsuarioUABC()
                {
                    Matricula = 338833,
                    UserName = "kevin.zavala",
                    Nombre = "Kevin Enrique",
                    ApellidoP = "Zavala",
                    ApellidoM = "Níz",
                    Email = "kevin.zavala@uabc.edu.mx"
                };

                var result = await _userManager.CreateAsync(usuarioA, "password");
                var result2 = await _userManager.CreateAsync(usuarioB, "password");
                var result3 = await _userManager.CreateAsync(usuarioC, "password");
                var result4 = await _userManager.CreateAsync(usuarioD, "password");

                await _context.SaveChangesAsync();
            }
        }
    }
}
