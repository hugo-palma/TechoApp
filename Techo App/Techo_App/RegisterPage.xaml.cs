using System;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.Views;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Techo_App.Models;
using Techo_App.ViewModels;
using Techo_App.Services;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using PCLCrypto;

using Xamarin.Auth;
using System.Threading;

namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        IAccountManagerService _services;
        private Evento evento;
        private UsuariosService usuariosService;
        public string jsonResultado;
        public RegisterPage(Evento evento)
        {
            InitializeComponent();
            this.evento = evento;
            _services = DependencyService.Get<IAccountManagerService>();
            tcs = new TaskCompletionSource<bool>();
        }
        public Task PageClosedTask { get { return tcs.Task; } }

        private TaskCompletionSource<bool> tcs { get; set; }
        
        private string requestOriginal;
        private async void btnFace_Clicked(object sender, EventArgs e)
        {
            var accounts = _services.Accounts;
            if (accounts.Contains(LoginServices.Facebook))
            {
                var access_token = _services.GetPropertyFromAccount(LoginServices.Facebook, "access_token");
                var fbUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,picture,age_range,email,gender,is_verified&access_token=" + access_token);
                var httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("token", access_token);
                var response = await httpClient.GetAsync(fbUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var a = (JObject)JsonConvert.DeserializeObject(content);
                    usuariosService = new UsuariosService();
                    var resultado = await usuariosService.PostUsuarioFaceAsync(content);

                    if (resultado == "added" || resultado == "logged")
                    {
                        UsuariosEventosService usuariosEventosService = new UsuariosEventosService();
                        UsuariosEventos usuariosEventos = new UsuariosEventos();
                        SesionService sesionService = new SesionService();
                        var idUsuarios = await sesionService.GetSesionIdUserDbAsync();
                        usuariosEventos.idEvento = evento.idEventos;
                        usuariosEventos.idUsuario = idUsuarios;
                        var resultadoUE = await usuariosEventosService.setUsuarioEvento(usuariosEventos);
                        Navigation.InsertPageBefore(new DetalleEventoPage(evento), this);
                        await Navigation.PopAsync();
                    }
                    var login = a["fisrt_name"];
                    var image = "https://graph.facebook.com/me/picture?access_token=" + access_token;
                }
            }
            else
            {
                AuthorizePage authorizePage = new AuthorizePage(LoginServices.Facebook, this);
                var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                authorizePage.Disappearing += (sender2, e2) =>
                {
                    ExtraerInfoFacebook();
                };
                await Navigation.PushAsync(authorizePage);
            }
        }
        private async void btnCorreo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateRegisterPage(evento, this));
        }
        private async void btnIniciar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(evento, this));
        }
        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void btnTwitter_Clicked(object sender, EventArgs e)
        {
            //otro codigo
            var httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            var accounts = _services.Accounts;
            if (accounts.Contains(LoginServices.Twitter))
            {
                var account = _services.getAccount();
                var request = new OAuth1Request("GET", new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
                        null,
                        account);
                var response = await request.GetResponseAsync();
                var json = response.GetResponseText();
                            }
            else
            {
                var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                var authorizePage = new AuthorizePage(LoginServices.Twitter, this);
                authorizePage.Disappearing += async(sender2, e2) =>
                {
                    //waitHandle.Set();
                    await ExtraerInfoTwitter();
                };
                await Navigation.PushAsync(authorizePage);
            }
        }
        private async Task ExtraerInfoTwitter()
        {
            var account = _services.getAccount();
            if (account != null)
            {
                var request = new OAuth1Request("GET", new Uri("https://api.twitter.com/1.1/account/verify_credentials.json"),
                        null,
                        account);
                var response = await request.GetResponseAsync();
                var json = response.GetResponseText();

                Debug.WriteLine(json);
                usuariosService = new UsuariosService();
                var resultado = await usuariosService.PostUsuarioTwitterAsync(json);
                UsuariosEventosService usuariosEventosService = new UsuariosEventosService();
                UsuariosEventos usuariosEventos = new UsuariosEventos();
                SesionService sesionService = new SesionService();
                var idUsuarios = await sesionService.GetSesionIdUserDbAsync();
                usuariosEventos.idEvento = evento.idEventos;
                usuariosEventos.idUsuario = idUsuarios; 
                var resultadoUE = await usuariosEventosService.setUsuarioEvento(usuariosEventos);
                await Navigation.PushAsync( new DetalleEventoPage(evento));

            }
        }
        private async Task ExtraerInfoFacebook()
        {
            var accounts = _services.Accounts;
            if (accounts.Contains(LoginServices.Facebook))
            {
                var access_token = _services.GetPropertyFromAccount(LoginServices.Facebook, "access_token");
                var fbUri = new Uri("https://graph.facebook.com/me?fields=first_name,last_name,picture.width(300).height(300),age_range,email,gender,is_verified&access_token=" + access_token);
                var httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("token", access_token);
                var response = await httpClient.GetAsync(fbUri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var a = (JObject)JsonConvert.DeserializeObject(content);
                    usuariosService = new UsuariosService();
                    var resultado = await usuariosService.PostUsuarioFaceAsync(content);
                    if (resultado == "added")
                    {
                        UsuariosEventosService usuariosEventosService = new UsuariosEventosService();
                        UsuariosEventos usuariosEventos = new UsuariosEventos();
                        SesionService sesionService = new SesionService();
                        var idUsuarios = await sesionService.GetSesionIdUserDbAsync();
                        usuariosEventos.idEvento = evento.idEventos;
                        usuariosEventos.idUsuario = idUsuarios;
                        var resultadoUE = await usuariosEventosService.setUsuarioEvento(usuariosEventos);
                        Navigation.InsertPageBefore(new DetalleEventoPage(evento), this);
                        await Navigation.PopAsync();
                    }
                    if (resultado == "logged")
                    {
                        Navigation.InsertPageBefore(new DetalleEventoPage(evento), this);
                        await Navigation.PopAsync();
                    }
                }
            }
        }
    }
}