using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Models.Entities.CAEF
{
    public class SubtipoExamen
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
