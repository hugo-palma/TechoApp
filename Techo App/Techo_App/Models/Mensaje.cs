
namespace Techo_App.Models
{
    public class Mensaje
    {
        public int idUsuario { get; set; }
        public int idGrupo { get; set; }
        public string mensaje { get; set; }
        public string fechaCreacion { get; set; }
        public int estado { get; set; }
        public bool recibido { get; set; }
        public string foto { get; set; }
    }
}
