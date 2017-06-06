using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAEF.Models.Contexts;
using CAEF.Models.Entities.CAEF;
using CAEF.Repositories;
using Microsoft.EntityFrameworkCore;
using CAEF.Models.DTO;

namespace CAEF.Services
{
    public class SolicitudAdministrativaServices
    {
        private CAEFContext _contextoCAEF;
        private ActasAdministrativasRepository _repositorioSolicitudes;

        public SolicitudAdministrativaServices(CAEFContext contextoCAEF, ActasAdministrativasRepository repositorioSolicitudes)
        {
            _contextoCAEF = contextoCAEF;
            _repositorioSolicitudes = repositorioSolicitudes;
        }

        public SolicitudDocente ObtenerActaActual(int acta)
        {
            return _repositorioSolicitudes.BuscarSolicitud(acta);
        }

        public IEnumerable<SolicitudAlumno> ObtenerAlumnos(int acta)
        {
            return _repositorioSolicitudes.BuscarAlumnos(acta);
        }

        public Usuario ObtenerDocente(int idDocente)
        {
            return _repositorioSolicitudes.BuscarDocente(idDocente);
        }

        public IEnumerable<SubtipoExamen> ObtenerSubtiposExamen()
        {
            return _contextoCAEF.SubtiposExamen.ToList();
        }

        public IEnumerable<TipoExamen> ObtenerTiposExamen()
        {
            return _contextoCAEF.TiposExamen.ToList();
        }

        public int AgregarActaDocente(SolicitudDocente acta)
        {
            _contextoCAEF.SolicitudesDocente.Add(acta);
            _contextoCAEF.SaveChanges();
            return acta.Id;
        }

        public int ActualizarActaDocente(SolicitudDocente acta)
        {
            _contextoCAEF.SolicitudesDocente.Update(acta);
            _contextoCAEF.SaveChanges();
            return acta.Id;
        }
        
        public void AgregarActaAdmin(SolicitudAdmin acta)
        {
            _contextoCAEF.SolicitudesAdministrativo.Add(acta);
        }

        public void ActualizarActaAdmin(SolicitudAdmin acta)
        {
            _contextoCAEF.SolicitudesAdministrativo.Update(acta);
            _contextoCAEF.SaveChanges();
        }

        public void BorrarSolcitudAlumno(SolicitudAlumno solicitud)
        {
            _contextoCAEF.SolicitudesAlumno.Remove(solicitud);
        }

        public void AgregarSolicitudAlumno(IEnumerable<SolicitudAlumno> solicitudes, IEnumerable<int> Ids)
        {
            int count = 0;
            foreach (SolicitudAlumno solicitud in solicitudes)
            {
                solicitud.IdAlumno = solicitud.Alumno.Id;
                SolicitudAlumno solicitudActual;

                if (AlumnoExiste(solicitud.Alumno.Id))
                {
                    //_contextoCAEF.Alumnos.Attach(solicitud.Alumno);

                    if (Ids != null)
                    {
                        if (count < Ids.Count())
                        {
                            solicitudActual = _contextoCAEF.SolicitudesAlumno.Find(Ids.ElementAt(count));
                        }
                        else
                        {
                            solicitudActual = null;
                        }
                    }
                    else
                    {
                        solicitudActual = null;
                    }

                    if (solicitudActual != null)
                    {
                        _contextoCAEF.SolicitudesAlumno.Remove(solicitudActual);
                        _contextoCAEF.SolicitudesAlumno.Add(solicitud);
                    }
                    else
                    {
                        _contextoCAEF.SolicitudesAlumno.Add(solicitud);
                    }
                }
                else
                {
                    if (Ids != null)
                    {
                        if (count < Ids.Count())
                        {
                            solicitudActual = _contextoCAEF.SolicitudesAlumno.Find(Ids.ElementAt(count));
                        }
                        else
                        {
                            solicitudActual = null;
                        }
                    }
                    else
                    {
                        solicitudActual = null;
                    }

                    if (solicitudActual != null)
                    {
                        _contextoCAEF.SolicitudesAlumno.Remove(solicitudActual);
                        _contextoCAEF.SolicitudesAlumno.Add(solicitud);

                    }
                    else
                    {
                        _contextoCAEF.SolicitudesAlumno.Add(solicitud);
                    }
                }

                count++;
            }

            if (Ids != null)
            {
                if (Ids.Count() > count)
                {
                    for (int i = count; i <= Ids.Count() - count; i++)
                    {
                        var solicitudRemover = _contextoCAEF.SolicitudesAlumno.Find(Ids.ElementAt(i));
                        _contextoCAEF.SolicitudesAlumno.Remove(solicitudRemover);
                    }

                }
            }

        }

        public IEnumerable<SolicitudDocente> ObtenerSolicitudesAdministrativos(Usuario usuario)
        {
            if (usuario.RolId == 1)
            {
                return _repositorioSolicitudes.BuscarSolicitudesAdministrador(usuario.Id);
            }
            else
            {
                return _repositorioSolicitudes.BuscarSolicitudesAdministrativos(usuario.Id);
            }

        }

        public IEnumerable<SolicitudAdmin> ObtenerSolicitudesDocente(Usuario usuario)
        {

            return _repositorioSolicitudes.ObtenerSolicitudesDocentes(usuario.Id);


        }

        public IEnumerable<SolicitudAdmin> MostrarSolicitudesAdministrativos(Usuario usuario,DateTime? fecha, string nombreDocente, string materia, string tipoExamen, string periodo, string semestre,string estado)
        {
            //if (usuario.RolId == 1)
            //{
            return _repositorioSolicitudes.ObtenerSolcitudesAdministrador(fecha, nombreDocente, materia, tipoExamen, periodo, semestre, estado);
            //}
            //else
            //{
            //    return _repositorioSolicitudes.BuscarSolicitudesAdministrativos(usuario.Id);
            //}

        }
        
        public bool AlumnoExiste(int Id)
        {
            var resultado = _contextoCAEF.Alumnos
                .Where(a => a.Id == Id)
                .FirstOrDefault();

            return resultado == null ? false : true;
        }

        public bool SolicitudExiste(int Id)
        {
            var resultado = _contextoCAEF.SolicitudesAlumno
                .Where(s => s.Id == Id)
                .FirstOrDefault();

            return resultado == null ? false : true;
        }
        
        public SolicitudDocente ObtenerSolicitudDocente(int id)
        {
            SolicitudDocente solicitud = _contextoCAEF.SolicitudesDocente
                .Where(s => s.Id == id)
                .FirstOrDefault();

            return solicitud;
        }

        public SolicitudAdmin ObtenerSolicitudAministrativa(int id)
        {
            SolicitudAdmin solicitud = _contextoCAEF.SolicitudesAdministrativo
                .Include(s => s.SubTipoExamen)
                .Include(s=> s.SolicitudDocente)
                .Include(s => s.SolicitudDocente.Carrera)
                .Include(s => s.SolicitudDocente.Materia)
                .Include(s => s.SolicitudDocente.TipoExamen)
                .Include(s => s.SubTipoExamen)
                .Where(s => s.IdSolicitud == id)
                .FirstOrDefault();

            return solicitud;
        }

        public IEnumerable<SolicitudAlumno> ObtenerSolicitudesAlumno(int id)
        {
            return _contextoCAEF.SolicitudesAlumno
                .Include(s => s.Alumno)
                .Where(s => s.IdSolicitud == id)
                .ToList();
        }
    }
}
