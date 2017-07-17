﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Techo_App.Models;
using Techo_App.Services;
using Xamarin.Forms;

namespace Techo_App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private Evento evento;
        public LoginViewModel(Evento evento)
        {
            this.evento = evento;
        }
        public INavigation Navigation;
        private string _mensaje;
        public string mensaje
        {
            get { return _mensaje; }
            set
            {
                _mensaje = value;
                OnPropertyChanged();
            }
        }
        public Identidad identidad
        {
            get
            {
                return _identidad;
            }
            set
            {
                _identidad = value;
                OnPropertyChanged();
            }
        }
        private Identidad _identidad = new Identidad();
        public Command LoginCommand
        {
            get{
                return new Command(async () =>
                    {
                        if(identidad.correo == null || identidad.password == null)
                        {
                            mensaje = "Ingrese sus datos";
                        }
                        else
                        {
                            var loginservice = new LoginService();
                            var result = await loginservice.PostLoginRequestAsync(_identidad);
                            if (result.ToString() == "successful")
                            {
                                //credenciales verificadas
                                await Navigation.PushAsync(new DetalleEventoPage(evento));
                            }
                            else if (result.ToString() == "unsuccessful")
                            {
                                //cambiar a eventodetail
                                mensaje = "Su usuario o contraseña no son correctos";
                            }
                        }
                    });
            }
        }
        public Command CancelarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PopAsync();
                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
