using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techo_App.Models
{
    public class Conversacion
    {
        public int idConversacion { get; set; }
        public string nombre { get; set; }
        public int idUsuarios { get; set; }
        public int [] idFriends { get; set; } 
    }
}
