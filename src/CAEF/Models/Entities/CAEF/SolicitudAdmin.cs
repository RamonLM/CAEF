using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.Entities.CAEF
{
    public class SolicitudAdmin
    {
        [Key, ForeignKey("SolicitudDocente")]
        public int IdSolicitud { get; set; }
        [Required]
        public string ClaveUnidad { get; set; }
        [Required]
        public string UnidadAcademica { get; set; }
        [Required]
        public DateTime FechaAceptacion { get; set; }
        [Required]
        public int NumeroAlumnos { get; set; }
        [Required]
        public string CalificacionLetra { get; set; }
        [Required]
        public string CicloEscolar { get; set; }
        [Required]
        [ForeignKey("SubtipoExamen")]
        public int IdSubtipoExamen { get; set; }
        [Required]
        public string PlanEstudios { get; set; }
        [Required]
        public string EtapaSemestre { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public string URLDocumento { get; set; }
        public virtual SolicitudDocente SolicitudDocente { get; set; }
        public virtual SubtipoExamen SubTipoExamen { get; set; }
    }
}
