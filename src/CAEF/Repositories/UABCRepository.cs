using CAEF.Models.Contexts;
using CAEF.Models.Entities.UABC;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CAEF.Repositories
{
    public class UABCRepository : IUABCRepository
    {
        private UsuarioUABCContext _context;

        public UABCRepository(UsuarioUABCContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioUABC> ObtenerUsuarios()
        {
            return _context.UsuariosUABC.ToList();
        }

        public bool UsuarioExiste(string correo)
        {
            var resultado = _context.Users
                .Where(u => u.Email == correo)
                .FirstOrDefault();

            return resultado == null ? false : true;
        }
    }
}
