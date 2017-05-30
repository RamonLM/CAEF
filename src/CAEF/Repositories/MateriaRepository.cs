using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAEF.Models.Entities.CAEF;
using CAEF.Models.Contexts;

namespace CAEF.Repositories
{
    public class MateriaRepository : CRUDRepository<Materia>
    {
        public MateriaRepository(CAEFContext contextoCAEF) : base(contextoCAEF)
        { }

        public override List<Materia> BuscarTodos()
        {
            return _contextoCAEF.Materias.ToList();
        }
    }
}
