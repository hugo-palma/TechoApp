using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.Views;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Techo_App.Models;

namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private Evento evento;
        public RegisterPage(Evento evento)
        {
            InitializeComponent();
            this.evento = evento;
        }
        private string clientId = "1857233457861970";
        private string clave = "731178225f0645d15787e5d027269f0b";
        private string requestOriginal;
        private async void btnFace_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new DetalleEventoPage());
            var apiRequest = "https://www.facebook.com/dialog/oauth?client_id="
                + clientId
                + "&display=popup&respones_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
            requestOriginal = apiRequest;
            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };
            webView.Navigated += WebViewOnNavigated;
            Content = webView;
        }
        private async void WebViewOnNavigated(Object sender, WebNavigatedEventArgs e)
        {
            var newapiRequest = "https://graph.facebook.com/v2.9/oauth/access_token?client_id="
                + clientId
                + "&redirect_uri=https://www.facebook.com/connect/login_success.html"
                + "&client_secret="
                + clave
                + "&code="
                + ExtractCodeFromUrl(e.Url);
            var httpClient = new HttpClient();
            var accesTokenJson = await httpClient.GetStringAsync(newapiRequest);

            string access_token = (string)JObject.Parse(accesTokenJson)["access_token"];

            var accessToken = access_token;
            if (accessToken != "")
            {
                await GetFacebookProfileAsync(accessToken);
            }
        }
        private string ExtractAccessTokenFromUrl(string url)
        {
            if(url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                if(Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.iOS)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }
                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                return accessToken;
            }
            return string.Empty;
        }
        private string ExtractCodeFromUrl(string url)
        {
            if (url.Contains("code"))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html?code=", "");
                if (Device.RuntimePlatform == Device.WinPhone || Device.RuntimePlatform == Device.iOS)
                {
                    at = url.Replace("https://www.facebook.com/connect/login_success.html?code=", "");
                }
                var code = at;
                return code;
            }
            return string.Empty;
        }
        public async Task GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.9/me/"
                + "?fields=name,picture,cover,age_range,devices,email,gender,is_verified"
                + "&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            await Navigation.PushAsync(new DetalleEventoPage(evento));
        }
        private async void btnCorreo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateRegisterPage(evento));
        }
        private async void btnIniciar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(evento));
        }
        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}