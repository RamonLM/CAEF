using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.Entities.CAEF
{
    public class SolicitudAlumno
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("SolicitudDocente")]
        public int IdSolicitud { get; set; }
        [Required]
        [ForeignKey("Alumno")]
        public int IdAlumno { get; set; }
        public virtual SolicitudDocente SolicitudDocente { get; set; }
        public virtual Alumno Alumno { get; set; }
    }
}
