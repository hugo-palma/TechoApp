namespace Techo_App.Models
{
    public class Evento
    {
        public int idEventos { get; set; }
        public string nombre { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string ubicacionNombre { get; set; }
        public string ubicacionCoordenada { get; set; }
        public string tipo { get; set; }
        public int registrado { get; set; }
        public string textoBoton { get; set; }
    }
}
