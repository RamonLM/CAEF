using System.Collections.Generic;
using CAEF.Models.Entities.FIAD;

namespace CAEF.Repositories
{
    public interface IFIADRepository
    {
        IEnumerable<UsuarioFIAD> ObtenerUsuarios();
        bool UsuarioExiste(string correo);
    }
}