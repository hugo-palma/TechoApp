using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techo_App.Models
{
    public class UsuariosEventos
    {
        public int idUsuariosEventos { get; set; }
        public int idEvento { get; set; }
        public int idUsuario { get; set; }
        public string fechaCreacion { get; set; }
    }
}
