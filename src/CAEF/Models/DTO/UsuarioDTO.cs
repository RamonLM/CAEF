using CAEF.Models.Entities.CAEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.DTO
{
    public class UsuarioDTO
    {
        [Required]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}
