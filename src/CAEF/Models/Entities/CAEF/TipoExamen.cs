using System.ComponentModel.DataAnnotations;

namespace CAEF.Models.Entities.CAEF
{
    public class TipoExamen
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
