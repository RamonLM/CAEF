using System.Collections.Generic;
using CAEF.Models.Entities.CAEF;
using System.Threading.Tasks;

namespace CAEF.Repositories
{
    public interface IUsuarioRepository
    {
        bool ConsultarBaseDeDatosUABC(string correo);
        bool ConsultarBaseDeDatosFIAD(string correo);
    }
}
