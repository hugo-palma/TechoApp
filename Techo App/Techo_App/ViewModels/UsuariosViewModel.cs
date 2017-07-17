
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Techo_App.Models;
using Techo_App.Services;
using Xamarin.Forms;
using System.Diagnostics;
using Plugin.Media.Abstractions;

namespace Techo_App.ViewModels
{
    class UsuariosViewModel : INotifyPropertyChanged
    {
        private Evento evento;
        private INavigation Navigation;
        private RegisterPage registerPage;
        public UsuariosViewModel(Evento evento, INavigation Navigation, RegisterPage registerPage)
        {
            this.evento = evento;
            this.registerPage = registerPage;
            this.Navigation = Navigation;
        }
        MediaFile _mediaFile;
        private Image _photoUsuario = new Image();
        public Image photoUsuario
        {
            get { return _photoUsuario; }
            set {
                _photoUsuario = value;
                OnPropertyChanged();
            }
        }
        public Usuario SelectedUsuario
        {
            get { return _SelectedUsuario; }
            set
            {
                _SelectedUsuario = value;
                OnPropertyChanged();
            }
        }
        private Usuario _SelectedUsuario = new Usuario();
        
        public Command PostCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var usuariosServices = new UsuariosService();
                    //manejando imagen
                    if (_mediaFile != null)
                    {
                        var stream = _mediaFile.GetStream();
                        var bytes = new byte[stream.Length];
                        await stream.ReadAsync(bytes, 0, (int)stream.Length);
                        string base64 = System.Convert.ToBase64String(bytes);
                        _SelectedUsuario.foto = base64;
                    }
                    else
                    {
                        _SelectedUsuario.foto = null;
                    }
                    //logica de envio
                    _SelectedUsuario.idRol = 1;
                    var result = await usuariosServices.PostUsuarioAsync(_SelectedUsuario);
                    if(result.GetType() == typeof(Usuario))
                    {
                        //insertar datos en base de datos de sqlite
                        Navigation.InsertPageBefore(new DetalleEventoPage(evento), registerPage);
                        await Navigation.PopToRootAsync();
                    }
                    else if(result.ToString() == "added")
                    {
                        //cambiar a eventodetail
                        Navigation.InsertPageBefore(new DetalleEventoPage(evento), registerPage);
                        Navigation.RemovePage(registerPage);
                        await Navigation.PopAsync();
                    }
                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void setImage(MediaFile mediafile)
        {
            _mediaFile = mediafile;
        }
    }
    
}
