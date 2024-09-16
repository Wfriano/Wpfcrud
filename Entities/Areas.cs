using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Areas
    {
        [Key]
        public int IdArea { get; set; }
        public string NombreArea { get; set; }
    }
}
