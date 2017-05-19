using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAEF.Models.Entities.CAEF
{
    public class SolicitudDocente
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Materia")]
        public int IdMateria { get; set; }
        [Required]
        public string Periodo { get; set; }
        [Required]
        [ForeignKey("Carrera")]
        public int IdCarrera { get; set; }
        [Required]
        [ForeignKey("TipoExamen")]
        public int IdTipoExamen { get; set; }
        [Required]
        [ForeignKey("Usuario")]
        public int IdEmpleado { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Required]
        [ForeignKey("Estado")]
        public int IdEstado { get; set; }
        public virtual Materia Materia { get; set; }
        public virtual Carrera Carrera { get; set; }
        public virtual TipoExamen TipoExamen { get; set; }
        public virtual Usuario Empleado { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
