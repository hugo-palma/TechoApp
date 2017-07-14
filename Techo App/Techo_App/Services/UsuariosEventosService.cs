using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

namespace Techo_App.Services
{
    public class UsuariosEventosService
    {
        public async Task<string>setUsuarioEvento(UsuariosEventos usuarioEvento)
        {
            RestClient<UsuariosEventos> restClient = new RestClient<UsuariosEventos>("userEvents");
            var resultado = await restClient.PostAsync(usuarioEvento);
            string status = (string)JObject.Parse(resultado)["status"];
            return status;
        }
        public async Task<List<Evento>>GetEventsByAssistanceAsync(int idUser)
        {
            RestClient<Evento> restClient = new RestClient<Evento>("eventsByAssistance", idUser.ToString());
            var resultado = await restClient.GetAsync();
            return resultado;
        }
    }
}
