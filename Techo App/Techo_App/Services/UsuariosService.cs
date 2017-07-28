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
                var idUsuarios = (string)JObject.Parse(jsonResult)["options"];
                var sesion = new Sesion();
                sesion.idUsuarios = int.Parse(idUsuarios);
                sesion.firstName = usuario.nombre;
                sesion.lastName = usuario.apellido;
                sesion.photo = usuario.foto;
                sesion.role = usuario.idRol;
                SesionService sesionService = new SesionService();
                await sesionService.SetSesionDbAsync(sesion);
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
                //var fb = DependencyService.Get<IFirebase>().getFirebaseUserId();
                //Debug.WriteLine(fb + "es el id de firebase");
                //var resultfireb = await sesionService.UpdateFirebaseIdToken(sesion.idUsuarios, (string)fb);
                await sesionService.SetSesionDbAsync(sesion);
                //no esta
                return "unsuccessful";
            }
            
        }
        public async Task<string> PostUsuarioTwitterAsync(string jsonTwitter)
        {
            Usuario usuarioTwitter = new Usuario();

            var id = (string)JObject.Parse(jsonTwitter)["id"];

            usuarioTwitter.nombre = (string)JObject.Parse(jsonTwitter)["name"];
            usuarioTwitter.idRol = 1;
            usuarioTwitter.foto = (string)JObject.Parse(jsonTwitter)["profile_image_url_https"];
            usuarioTwitter.password = (string)JObject.Parse(jsonTwitter)["id"];
            usuarioTwitter.correo = (string)JObject.Parse(jsonTwitter)["id"];
            RestClient<Usuario> restClient = new RestClient<Usuario>("users");
            var jsonResult = await restClient.PostAsync(usuarioTwitter);
            //api/friends/{id}
            if (jsonResult.Contains("added"))
            {
                var idUsuarios = (string)JObject.Parse(jsonResult)["options"];
                var sesion = new Sesion();
                sesion.idUsuarios = int.Parse(idUsuarios);
                sesion.firstName = usuarioTwitter.nombre;
                sesion.lastName = usuarioTwitter.apellido;
                sesion.photo = usuarioTwitter.foto;
                sesion.role = usuarioTwitter.idRol;
                SesionService sesionService = new SesionService();
                await sesionService.SetSesionDbAsync(sesion);
                return "added";
            }
            else
            {
                await ConvertJsonToSesionAsync(jsonResult);
                return "logged";
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
                var sesion = new Sesion();
                var idUsuarios = (string)JObject.Parse(jsonResult)["options"];
                sesion.idUsuarios = int.Parse(idUsuarios);
                sesion.firstName = usuarioFacebook.nombre;
                sesion.lastName = usuarioFacebook.apellido;
                sesion.photo = usuarioFacebook.foto;
                sesion.role = usuarioFacebook.idRol;
                SesionService sesionService = new SesionService();
                await sesionService.SetSesionDbAsync(sesion);
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
            ///var fb = DependencyService.Get<IFirebase>().getFirebaseUserId();
            //Debug.WriteLine(fb + "es el id de firebase");
            //var resultfireb = await sesionService.UpdateFirebaseIdToken(sesion.idUsuarios, (string)fb);
            await sesionService.SetSesionDbAsync(sesion);
            return JsonConvert.DeserializeObject<Sesion>(json);
        }
    }
}
