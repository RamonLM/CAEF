using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.DTO
{
    public class SolicitudAlumnoDTO
    {
        public int IdSolicitud { get; set; }
        public int IdAlumno { get; set; }
        public AlumnoDTO Alumno { get; set; }
    }
}
