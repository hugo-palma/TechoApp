using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.Services;
using Techo_App.Views;
using Xamarin.Forms;

namespace Techo_App.ViewModels
{
    public class EventosViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation;
        public bool logeado = false;
        private SesionService sesionService = new SesionService();
        private UsuariosEventosService usuariosEventosService;
        public EventosViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
            InitializeDataAsync();
        }
        
        private List<Evento> _listaEventos;
        public List<Evento> ListaEventos
        {
            get { return _listaEventos; }
            set
            {
                _listaEventos = value;
                OnPropertyChanged();
            }
        }
        private Evento _evento;
        public Evento evento
        {
            get { return _evento; }
            set
            {
                _evento = value;
                OnPropertyChanged();
            }
        }
        private async Task InitializeDataAsync()
        {
            var listaTemp = new List<Evento>();
            if (await VerificarSesion() == true)
            {
                usuariosEventosService = new UsuariosEventosService();
                List<Sesion> sesion = new List<Sesion>();
                sesion = await sesionService.GetSesionDbAsync();
                int idUsuario = await GetIdUsuario();
                listaTemp = await usuariosEventosService.GetEventsByAssistanceAsync(idUsuario);
                foreach(var evento in listaTemp)
                {
                    if (evento.registrado == 1)
                        evento.textoBoton = "Ver";
                    else
                        evento.textoBoton = "Participar";
                }
            }
            else
            {
                var eventosServices = new EventosService();
                listaTemp = await eventosServices.GetEventosAsync();
                foreach (var evento in listaTemp)
                {
                    evento.textoBoton = "Participar";
                }
            }
            ListaEventos = listaTemp;
            //select from usuario
        }
        public Command EventoPickedCommand
        {
            get
            {
                return new Command(async (evento) =>
                {
                    var eventoSeleccionado = evento as Evento;
                    if(await VerificarSesion() == true)
                    {
                        usuariosEventosService = new UsuariosEventosService();
                        UsuariosEventos usuarioEvento = new UsuariosEventos();
                        usuarioEvento.idEvento = eventoSeleccionado.idEventos;
                        usuarioEvento.idUsuario = await GetIdUsuario();
                        if(eventoSeleccionado.registrado == 0)
                        {
                            var resultado = await usuariosEventosService.setUsuarioEvento(usuarioEvento);
                            if(resultado == "successful")
                            {

                            }
                        }
                        await Navigation.PushAsync(new DetalleEventoPage(eventoSeleccionado));
                    }
                    else
                    {
                        RegisterPage rp = new RegisterPage(eventoSeleccionado);
                        await Navigation.PushAsync(rp);
                    }
                });
            }
        }
        public Command AddLocationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PushAsync(new AddLocationPage());
                });
            }
        }
        private async Task<bool> VerificarSesion()
        {
            var sesionesIni = await sesionService.GetSesionDbAsync();
            List<Sesion> listaSesiones = new List<Sesion>();
            listaSesiones = sesionesIni;
            if(listaSesiones.Count > 0)
            {
                return true;
            }
            return false;
        }
        private async Task<int> GetIdUsuario()
        {
            var sesionesIni = await sesionService.GetSesionDbAsync();
            List<Sesion> listaSesiones = new List<Sesion>();
            listaSesiones = sesionesIni;
            return listaSesiones[0].idUsuarios;
        }
        public void setLogeado(bool estado)
        {
            this.logeado = estado;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
