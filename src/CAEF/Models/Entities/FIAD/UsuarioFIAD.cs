using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAEF.Models.Entities.FIAD
{
    public class UsuarioFIAD
    {
        [Key]
        [Column("Numero_Empleado")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
    }
}
