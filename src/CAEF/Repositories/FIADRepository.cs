using CAEF.Models.Contexts;
using CAEF.Models.Entities.FIAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Repositories
{
    public class FIADRepository : IFIADRepository
    {
        private UsuarioFIADContext _context;

        public FIADRepository(UsuarioFIADContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioFIAD> ObtenerUsuarios()
        {
            return _context.UsuariosFIAD.ToList();
        }

        public bool UsuarioExiste(string correo)
        {
            var resultado = _context.UsuariosFIAD
                .Where(u => u.Correo == correo)
                .FirstOrDefault();

            return resultado == null ? false : true;
        }
    }
}
