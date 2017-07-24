using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

namespace Techo_App.Services
{
    public class ConversacionesService
    {
        public async Task<List<Grupo>> GetConversacionesAsync(int id)
        {
            RestClient<Grupo> restClient = new RestClient<Grupo>("conversaciones", id.ToString());
            var listaEventos = await restClient.GetAsync();
            return listaEventos;
        }
    }
}
