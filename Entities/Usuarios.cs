using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cargo { get; set; }
        public string Correo { get; set; }
    }
}
