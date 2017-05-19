using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.DTO
{
    public class ActaDocenteDTO
    {
        [Required]
        public int IdMateria { get; set; }
        [Required]
        public string Periodo { get; set; }
        [Required]
        public int IdCarrera { get; set; }
        [Required]
        public int IdTipoExamen { get; set; }
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Required]
        public int IdEstado { get; set; }
    }
}
