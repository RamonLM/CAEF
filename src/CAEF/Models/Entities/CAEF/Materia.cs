using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAEF.Models.Entities.CAEF
{
    public class Materia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Clave_Materia")]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Carrera { get; set; }
    }
}
