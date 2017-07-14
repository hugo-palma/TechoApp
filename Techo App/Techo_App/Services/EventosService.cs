using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Techo_App.Models;
using Techo_App.RestClient;
using Techo_App.Services;

[assembly: Dependency(typeof(EventosService))]
namespace Techo_App.Services
{
    public class EventosService
    {
        public async Task<List<Evento>> GetEventosAsync()
        {
            RestClient<Evento> restClient = new RestClient<Evento>("events");
            var listaEventos = await restClient.GetAsync();
            return listaEventos;
        }
    }
}
