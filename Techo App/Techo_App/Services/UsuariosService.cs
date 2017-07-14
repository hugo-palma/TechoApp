using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

using System.Diagnostics;
using Xamarin.Forms;
using Techo_App.Services;

[assembly: Dependency(typeof(UsuariosService))]
namespace Techo_App.Services
{
    public class UsuariosService
    {
        public async Task<Object> PostUsuarioAsync(Usuario usuario)
        {
            RestClient<Usuario> restClient = new RestClient<Usuario>("users");
            string jsonResult = await restClient.PostAsync(usuario);
            Response response = new Response();
            //api/friends/{id}
            response = JsonConvert.DeserializeObject<Response>(jsonResult);
            if(response.status == "added")
            {
                return "added";
            }
            else
            {
                try
                {
                    return "user";
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e);
                    return "error";
                }
            }
        }
        
    }
}
