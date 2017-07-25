﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.Converters;
using Techo_App.ViewModels;
using Techo_App.Models;
namespace Techo_App.Views
{
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel loginViewModel;
        private Evento evento;
        public LoginPage(Evento evento, RegisterPage registerPage)
        {
            InitializeComponent();
            this.evento = evento;
            loginViewModel = new LoginViewModel(evento, Navigation, registerPage);
            BindingContext = loginViewModel;
        }
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}