using CAEF.Models.Contexts;
using CAEF.Models.Entities.CAEF;
using System.Collections.Generic;
using System.Linq;

namespace CAEF.Services
{
    public class RolServices
    {
        private CAEFContext _contextoCAEF;        

        public RolServices(CAEFContext contextoCAEF)
        {
            _contextoCAEF = contextoCAEF;
        }

        public IEnumerable<Rol> ObtenerRoles()
        {
            return _contextoCAEF.Roles.ToList();
        }
    }
}
