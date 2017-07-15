using System.Collections.Generic;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

namespace Techo_App.Services
{
    public class FriendsService
    {
        public async Task<List<Voluntario>>GetUsersByEventAsync(int idUsuario, int idEvento)
        {
            RestClient<Voluntario> restClient = new RestClient<Voluntario>("attendants", idUsuario.ToString(), idEvento.ToString());
            var listaEventos = await restClient.GetAsync();
            return listaEventos;
        }
    }
}
