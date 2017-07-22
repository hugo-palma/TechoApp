using Newtonsoft.Json.Linq;
using System;
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
        public async Task <List<Usuario>>GetFriendsById(int idUsuario)
        {
            RestClient<Usuario> restClient = new RestClient<Usuario>("friends", idUsuario.ToString());
            var listaAmigos = await restClient.GetAsync();
            return listaAmigos;
        }
        public async Task<Object> PostFrienshipRequest(int idUsuario, int idAmigo)
        {
            SolicitudAmistad solicitud = new SolicitudAmistad();
            RestClient<SolicitudAmistad> restClient = new RestClient<SolicitudAmistad>("friendshipRequest");
            solicitud.idUsuarios = idUsuario;
            solicitud.idAmigo = idAmigo;
            string jsonResult = await restClient.PostAsync(solicitud);
            string status = (string)JObject.Parse(jsonResult)["status"];
            if(status == "requested")
            {
                return "successful";
            }
            return "unsuccessful";
        }
        
    }
}
