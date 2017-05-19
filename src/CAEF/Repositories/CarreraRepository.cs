using System.Collections.Generic;
using System.Linq;
using CAEF.Models.Entities.CAEF;
using CAEF.Models.Contexts;

namespace CAEF.Repositories
{
    public class CarreraRepository : CRUDRepository<Carrera>
    {
        public CarreraRepository(CAEFContext contextoCAEF): base (contextoCAEF)
        { }

        public override List<Carrera> BuscarTodos()
        {
            return _contextoCAEF.Carreras.ToList();
        }
    }
}
