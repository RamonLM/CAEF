using System.Collections.Generic;

namespace CAEF.Repositories
{
    public interface ICRUDRepository<Entidad> where Entidad : class
    {
        void Agregar(Entidad entidad);
        void Borrar(Entidad entidad);
        void Modificar(Entidad entidad);
        Entidad BuscarID(int id);
        List<Entidad> BuscarTodos();
    }
}
