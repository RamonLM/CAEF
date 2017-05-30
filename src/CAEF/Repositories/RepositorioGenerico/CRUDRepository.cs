using System;
using System.Collections.Generic;
using CAEF.Models.Contexts;


namespace CAEF.Repositories
{
    public abstract class CRUDRepository<Entidad> : ICRUDRepository<Entidad> where Entidad : class
    {
        protected CAEFContext _contextoCAEF;
        protected UsuarioUABCContext _contextoUABC;      

        public CRUDRepository(CAEFContext contextoCAEF)
        {
            _contextoCAEF = contextoCAEF;
        }
        public CRUDRepository(CAEFContext contextoCAEF, UsuarioUABCContext contextoUABC)
        {
            _contextoCAEF = contextoCAEF;
            _contextoUABC = contextoUABC;
        }
        public void Agregar(Entidad entidad)
        {            
            _contextoCAEF.Add(entidad);
        }

        public void Borrar(Entidad entidad)
        {
            _contextoCAEF.Remove(entidad);
        }

        public Entidad BuscarID(int id)
        {
            return _contextoCAEF.Find<Entidad>(id);            
        }

        public abstract List<Entidad> BuscarTodos();

        public void Modificar(Entidad entidad)
        {
            _contextoCAEF.Set<Entidad>();
        }
    }
}
