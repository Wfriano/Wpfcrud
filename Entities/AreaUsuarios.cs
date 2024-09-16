using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    
    public class AreaUsuarios
    {
        [Key, Column(Order = 0)]
        public int IdArea { get; set; }
        [Key, Column(Order = 1)]
        public int IdUsuario { get; set; }
    }
}
