using CAEF.Models.Entities.CAEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAEF.Repositories.Interfaces
{
    public interface ISolicitudesAdministrativasRepository
    {
        List<SolicitudDocente> BuscarSolicitudesAdministrador(int idEmpleado);
        List<SolicitudDocente> BuscarSolicitudesAdministrativos(int idEmpleado);
        SolicitudDocente BuscarSolicitud(int idActa);
        List<SolicitudAlumno> BuscarAlumnos(int idActa);
        Usuario BuscarDocente(int idDocente);
    }
}
