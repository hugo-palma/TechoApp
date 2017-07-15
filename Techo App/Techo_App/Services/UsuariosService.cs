using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;

using System.Diagnostics;
using Xamarin.Forms;
using Techo_App.Services;
using Newtonsoft.Json.Linq;

[assembly: Dependency(typeof(UsuariosService))]
namespace Techo_App.Services
{
    public class UsuariosService
    {
        public async Task<Object> PostUsuarioAsync(Usuario usuario)
        {
            RestClient<Usuario> restClient = new RestClient<Usuario>("users");
            string jsonResult = await restClient.PostAsync(usuario);
            //api/friends/{id}

            string status = (string)JObject.Parse(jsonResult)["status"];
            
            if (status == "added")
            {
                return "added";
            }
            else
            {
                var jsonUsuario = JObject.Parse(jsonResult)["options"];
                Sesion sesion = new Sesion();
                if (jsonUsuario["idUsuarios"] != null)
                {
                    sesion.idUsuarios = jsonUsuario["idUsuarios"].Value<int>();
                }
                if (jsonUsuario["nombre"] != null)
                {
                    sesion.firstName = jsonUsuario["nombre"].Value<string>();
                }
                if (jsonUsuario["apellido"] != null)
                {
                    sesion.lastName = jsonUsuario["apellido"].Value<string>();
                }
                if (jsonUsuario["foto"] != null)
                {
                    sesion.photo = jsonUsuario["foto"].Value<string>();
                }
                if (jsonUsuario["idRol"] != null)
                {
                    sesion.role = jsonUsuario["idRol"].Value<int>();
                }
                SesionService sesionService = new SesionService();
                await sesionService.SetSesionDbAsync(sesion);
                //no esta
                return "unsuccessful";
            }
            
        }
        
    }
}
