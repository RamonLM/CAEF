using System;
using System.ComponentModel.DataAnnotations;

namespace CAEF.Models.Entities.CAEF
{
    public class Estado
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
    }
}
