using CAEF.Models.Contexts;
using CAEF.Models.Entities.CAEF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAEF.Repositories
{
    public class UsuarioRepository : CRUDRepository<Usuario>, IUsuarioRepository
    {
        //private UsuarioUABCContext _contextoUABC;
        public UsuarioRepository(CAEFContext contectoCAEF, UsuarioUABCContext contextoUABC) : base(contectoCAEF, contextoUABC)
        {       
        }

        public bool ConsultarBaseDeDatosUABC(string correo)
        {
            throw new NotImplementedException();
        }

        public bool ConsultarBaseDeDatosFIAD(string correo)
        {
            throw new NotImplementedException();
        }

        public override List<Usuario> BuscarTodos()
        {
            return _contextoCAEF.Usuarios
                 .Include(u => u.Rol)
                 .ToList();
        }
    }
}
