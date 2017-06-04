using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.DTO
{
    public class FiltrosDTO
    {
        [Required]
        public DateTime? fecha { get; set; }
        [Required]
        public string docente { get; set; }
        [Required]
        public string materia { get; set; }
        [Required]
        public string tipoExamen { get; set; }
        [Required]
        public string periodo { get; set; }
        [Required]
        public string semestre { get; set; }
        [Required]
        public string estado { get; set; }
    }
}
