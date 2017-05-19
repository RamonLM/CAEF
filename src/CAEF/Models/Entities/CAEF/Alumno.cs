using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.Entities.CAEF
{
    public class Alumno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Matricula_Alumno")]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string ApellidoP { get; set; }
        [Required]
        public string ApellidoM { get; set; }
        [Required]
        public int Grupo { get; set; }
        [Required]
        public int Promedio { get; set; }
    }
}
