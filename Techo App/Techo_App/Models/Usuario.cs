
namespace Techo_App.Models
{
    public class Usuario
    {
        public int idUsuarios { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string foto { get; set; }
        public string password { get; set; }
        public int idRol { get; set; }
        public string nombreRol { get; set; }
    }
    
}
