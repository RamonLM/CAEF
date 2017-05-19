using CAEF.Models.Contexts;
using CAEF.Models.Entities.FIAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.Seed
{
    public class SemillaFIAD
    {
        private UsuarioFIADContext _context;

        public SemillaFIAD(UsuarioFIADContext context)
        {
            _context = context;
        }

        public async Task GeneraDatosSemilla()
        {
            if (!_context.UsuariosFIAD.Any())
            {
                var usuarioA = new UsuarioFIAD()
                {
                    Id = 338323,
                    Nombre = "José Ramón",
                    ApellidoP = "López",
                    ApellidoM = "Madueño",
                    Correo = "rlopez1@uabc.edu.mx"
                };

                var usuarioB = new UsuarioFIAD()
                {
                    Id = 335127,
                    Nombre = "César Samuel",
                    ApellidoP = "Parra",
                    ApellidoM = "Salas",
                    Correo = "samuel.parra@uabc.edu.mx"
                };

                var usuarioC = new UsuarioFIAD()
                {
                    Id = 331364,
                    Nombre = "Celso",
                    ApellidoP = "Figueroa",
                    ApellidoM = "Jacinto",
                    Correo = "celso.figueroa@uabc.edu.mx"
                };

                _context.UsuariosFIAD.Add(usuarioA);
                _context.UsuariosFIAD.Add(usuarioB);
                _context.UsuariosFIAD.Add(usuarioC);
                await _context.SaveChangesAsync();
            }
        }
    }
}
