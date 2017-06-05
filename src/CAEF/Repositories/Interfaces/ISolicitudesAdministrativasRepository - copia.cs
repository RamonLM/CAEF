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
        List<SolicitudAdmin> ObtenerSolcitudesAdministrador(DateTime? fecha,string nombreDocente,string materia,string tipoExamen,string periodo,string semestre,string estado);
        Usuario BuscarDocente(int idDocente);
        List<SolicitudAdmin> ObtenerSolicitudesDocentes(int idEmpleado);
    }
}
