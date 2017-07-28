using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Techo_App
{
    public enum LoginServices
    {
        Facebook,
        Twitter,
        // 
    }

    public class AuthorizePage : ContentPage
    {
        public LoginServices Service { get; private set; }
        public string json;
        public RegisterPage registerPage;
        public AuthorizePage(LoginServices service, RegisterPage registerPage)
        {
            Service = service;
        }
        public void setJson(string json)
        {
            registerPage.jsonResultado = json;
        }
    }
}