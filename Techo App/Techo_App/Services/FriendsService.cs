using System.Collections.Generic;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

namespace Techo_App.Services
{
    public class FriendsService
    {
        public async Task<List<Usuario>>GetUsersByEventAsync(int idUsuario, int idEvento)
        {
            RestClient<Usuario> restClient = new RestClient<Usuario>("attendants", idUsuario.ToString(), idEvento.ToString());
            var listaEventos = await restClient.GetAsync();
            return listaEventos;
        }
    }
}
