using CAEF.Models.Contexts;
using CAEF.Models.Entities.CAEF;
using CAEF.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAEF.Repositories
{
    public class ActasAdministrativasRepository : CRUDRepository<SolicitudAdmin>, ISolicitudesAdministrativasRepository
    {

        public ActasAdministrativasRepository(CAEFContext contectoCAEF, UsuarioUABCContext contextoUABC) : base(contectoCAEF, contextoUABC)
        {
        }

        public List<SolicitudAlumno> BuscarAlumnos(int idActa)
        {
            return _contextoCAEF.SolicitudesAlumno.Include((sal => sal.Alumno)).
                Where(sal => sal.IdSolicitud == idActa).ToList();
        }

        public Usuario BuscarDocente(int idDocente)
        {
            return _contextoCAEF.Usuarios.
                Where(doc => doc.Id == idDocente).FirstOrDefault();
        }

        public SolicitudDocente BuscarSolicitud(int idActa)
        {
            var solicitud = _contextoCAEF.SolicitudesDocente.
                  Include(sa => sa.Materia).
                  Include(sa => sa.Carrera).
                  Include(sa => sa.Empleado).
                  Include(sa => sa.TipoExamen)
                  .Where(sa => sa.Id == idActa)
                  .FirstOrDefault();

            if (solicitud != null)
            {
                var empleado = _contextoCAEF.Usuarios.Where(e => e.Id == solicitud.IdEmpleado).FirstOrDefault();

                solicitud.Empleado = empleado;
            }

            return solicitud;
        }

        public List<SolicitudDocente> BuscarSolicitudesAdministrador(int idEmpleado)
        {
            return _contextoCAEF.SolicitudesDocente.
                  Include(sa => sa.Materia).
                  Include(sa => sa.Estado).
                  Include(sa => sa.Empleado).
                  Where(sa=> sa.Estado.Id==1).
                  ToList();
        }

        public List<SolicitudDocente> BuscarSolicitudesAdministrativos(int idEmpleado)
        {

            return _contextoCAEF.SolicitudesDocente.
                  Include(sa => sa.Materia).
                  Include(sa => sa.Estado).
                  Include(sa => sa.Empleado).
                  Where(sa => sa.Estado.Id != 1).
                  Where(sa => sa.IdEmpleado == idEmpleado).
                  ToList();
        }

        public override List<SolicitudAdmin> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public List<SolicitudAdmin> ObtenerSolcitudesAdministrador(DateTime? fecha, string nombreDocente, string materia, string tipoExamen, string periodo, string semestre, string estado)
        {
            //int idDocente;
            string nombreDocenteBase;
            //idDocente = _contextoCAEF.SolicitudesAdministrativo.Where(s=> s.SolicitudDocente.Empleado.Nombre)

            return _contextoCAEF.SolicitudesAdministrativo.
                 Include(sa => sa.SolicitudDocente.Materia).
                 Include(sa => sa.SolicitudDocente.Estado).
                 Include(sa => sa.SolicitudDocente.Empleado).
                 Include(sa => sa.SolicitudDocente.TipoExamen).
                 Where(sa => (((fecha == null) && (sa.FechaAceptacion == sa.FechaAceptacion)) || ((fecha != null) && (sa.FechaAceptacion == fecha)))
                         &&  (((nombreDocente == null))  || ((nombreDocente != null) && (sa.SolicitudDocente.Empleado.Nombre + " " + sa.SolicitudDocente.Empleado.ApellidoP + " " + sa.SolicitudDocente.Empleado.ApellidoM == nombreDocente)))
                         &&  (((materia == null) && (sa.SolicitudDocente.Materia.Nombre == sa.SolicitudDocente.Materia.Nombre)) || ((materia != null) && (sa.SolicitudDocente.Materia.Nombre == materia)))
                         &&  (((tipoExamen == null) && (sa.SolicitudDocente.TipoExamen.Nombre == sa.SolicitudDocente.TipoExamen.Nombre)) || ((tipoExamen != null) && (sa.SolicitudDocente.TipoExamen.Nombre == tipoExamen)))
                         &&  (((periodo == null) && (sa.CicloEscolar == sa.CicloEscolar)) || ((periodo != null) && (sa.CicloEscolar == periodo)))
                         &&  (((semestre == null) && (sa.EtapaSemestre == sa.EtapaSemestre)) || ((semestre != null) && (sa.EtapaSemestre == semestre)))
                         &&  (((estado == null) && (sa.SolicitudDocente.Estado.Nombre == sa.SolicitudDocente.Estado.Nombre)) || ((estado != null) && (sa.SolicitudDocente.Estado.Nombre == estado)))).
                 ToList();
        }

        public List<SolicitudAdmin> ObtenerSolicitudesDocentes(int idEmpleado)
        {
            return _contextoCAEF.SolicitudesAdministrativo.
                  Include(sa => sa.SolicitudDocente.Materia).
                 Include(sa => sa.SolicitudDocente.Estado).
                 Include(sa => sa.SolicitudDocente.Empleado).
                 Include(sa => sa.SolicitudDocente.TipoExamen).
                  Where(sa => sa.SolicitudDocente.IdEmpleado == idEmpleado).
                  ToList();
        }
    }
}
