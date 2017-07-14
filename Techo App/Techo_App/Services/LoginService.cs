using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.RestClient;
namespace Techo_App.Services
{
    public class LoginService
    {
        private Sesion sesion;
        public async Task<Object> PostLoginRequestAsync(Identidad identidad)
        {
            RestClient<Identidad> restClient = new RestClient<Identidad>("login");
            
            string jsonResult = await restClient.PostAsync(identidad);
            string status = (string) JObject.Parse(jsonResult)["status"];
            var jsonUsuario = JObject.Parse(jsonResult)["options"]; 
            if (status == "successful")
            {
                sesion = new Sesion();
                Usuario usuario = new Usuario();
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
                if (jsonUsuario["foto"]!= null)
                {
                    sesion.photo = jsonUsuario["foto"].Value<string>();
                }
                if (jsonUsuario["idRol"] != null)
                {
                    sesion.role = jsonUsuario["idRol"].Value<int>();
                }
                SesionService sesionService = new SesionService();
                await sesionService.SetSesionDbAsync(sesion);
                return "successful";
            }
            else
            {
                //no esta
                return "unsuccessful";
            }
        }
    }
}
