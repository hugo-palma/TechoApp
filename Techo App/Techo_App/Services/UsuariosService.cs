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
                var fb = DependencyService.Get<IFirebase>().getFirebaseUserId();
                Debug.WriteLine(fb + "es el id de firebase");
                var resultfireb = await sesionService.UpdateFirebaseIdToken(sesion.idUsuarios, (string)fb);
                await sesionService.SetSesionDbAsync(sesion);
                //no esta
                return "unsuccessful";
            }
            
        }
        public async Task<string> PostUsuarioFaceAsync(String jsonFacebook)
        {
            var charsToRemove = new string[] { @"\" };
            foreach (var c in charsToRemove)
            {
                jsonFacebook = jsonFacebook.Replace(c, string.Empty);
            }
            Usuario usuarioFacebook = new Usuario();
            usuarioFacebook.nombre = (string)JObject.Parse(jsonFacebook)["first_name"];
            usuarioFacebook.apellido = (string)JObject.Parse(jsonFacebook)["last_name"];
            usuarioFacebook.idRol = 1;
            var fotoJson = JObject.Parse(jsonFacebook)["picture"];
            if (fotoJson != null)
            {
                var datas = fotoJson["data"];
                var url = datas["url"];
                string loquequiero = url.Value<string>();
                usuarioFacebook.foto = loquequiero;
            }
            usuarioFacebook.correo = (string)JObject.Parse(jsonFacebook)["email"];
            if(usuarioFacebook.correo == null)
            {
                usuarioFacebook.correo = (string)JObject.Parse(jsonFacebook)["id"];
            }
            usuarioFacebook.password = (string)JObject.Parse(jsonFacebook)["id"];
            RestClient<Usuario> restClient = new RestClient<Usuario>("users");
            var jsonResult = await restClient.PostAsync(usuarioFacebook);
            //api/friends/{id}
            if (jsonResult.Contains("added"))
            {
                return "added";
            }
            else
            {
                await ConvertJsonToSesionAsync(jsonResult);
                return "logged";
            }
        }
        private async Task<Sesion> ConvertJsonToSesionAsync(string json)
        {
            var sesion = new Sesion();
            var jsonUsuario = JObject.Parse(json)["options"];
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
            var fb = DependencyService.Get<IFirebase>().getFirebaseUserId();
            Debug.WriteLine(fb + "es el id de firebase");
            var resultfireb = await sesionService.UpdateFirebaseIdToken(sesion.idUsuarios, (string)fb);
            await sesionService.SetSesionDbAsync(sesion);
            return JsonConvert.DeserializeObject<Sesion>(json);
        }
    }
}
