using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Techo_App.Services;
using Techo_App.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Techo_App.ViewModels
{
    class AttendantsViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation;
        private Evento evento;
        public ObservableCollection<Usuario> usuarios { get; set; }
        private ContentPage content;
        public AttendantsViewModel(INavigation Navigation, Evento evento, ContentPage content)
        {
            this.Navigation = Navigation;
            this.evento = evento;
            this.content = content;
            InitializeDataAsync();
            

        }
        private List<Usuario> _listaUsuarios;
        public List<Usuario> ListaUsuarios
        {
            get { return _listaUsuarios; }
            set
            {
                _listaUsuarios = value;
                OnPropertyChanged();
            }
        }
        private SesionService sesionService;
        private FriendsService friendsService;
        private async Task InitializeDataAsync()
        {
            sesionService = new SesionService();
            var listaTemp = new List<Usuario>();

            friendsService = new FriendsService();
            int idUsuario = await sesionService.GetSesionIdUserDbAsync();
            listaTemp = await friendsService.GetUsersByEventAsync(idUsuario, evento.idEventos);

            usuarios = new ObservableCollection<Usuario>();
            ListView listView = new ListView();
            listView.RowHeight = 170;
            listView.ItemTemplate = new DataTemplate(typeof(CustomUsuarioCell));
            foreach (var usuario in listaTemp)
            {
                if(usuario.foto == null)
                {
                    usuario.foto = "photo.png";
                }
                else
                {
                    usuario.foto = "http://www.palmapplicationsv.com/techoapp/public/" + usuario.foto;
                }
                usuarios.Add(usuario);
            }
            listView.ItemsSource = usuarios;
            content.Content = listView;

        }

        public Command UsuarioPickedCommand
        {
            get
            {
                return new Command(async (usuario) =>
                {
                    var eventoSeleccionado = usuario as Usuario;
                    
                });
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class CustomUsuarioCell : ViewCell
    {
        public CustomUsuarioCell()
        {
            var tempLabel = new Label();
            var imagenPerfil = new Image();
            var nombreLabel = new Label();
            var apellidoLabel = new Label();
            var buttonAgregar = new Button();
            var verticaLayout = new StackLayout();
            var horizontalLayout = new StackLayout();

            //asignando bindings
            nombreLabel.SetBinding(Label.TextProperty, new Binding("nombre"));
            apellidoLabel.SetBinding(Label.TextProperty, new Binding("apellido"));
            Image temporal = new Image();
            tempLabel.SetBinding(Label.TextProperty, new Binding("foto"));
            Debug.WriteLine(tempLabel.Text + ", seria la foto que deseo");
            imagenPerfil.SetBinding(Image.SourceProperty, "foto");
            //imagenPerfil.Source = ImageSource.FromUri(new Uri("http://www.palmapplicationsv.com/techoapp/public/" + tempLabel.Text));
            //imagenPerfil.Source = ImageSource.FromFile("photo.png");

            //asignando propiedas de diseño
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            horizontalLayout.HorizontalOptions = LayoutOptions.Fill;
            verticaLayout.BackgroundColor = Color.FromHex("025d91");
            buttonAgregar.BackgroundColor = Color.FromHex("3d84f7");
            buttonAgregar.Text = "Agregar";
            imagenPerfil.HorizontalOptions = LayoutOptions.Start;
            imagenPerfil.WidthRequest = 100;
            imagenPerfil.HeightRequest = 100;
            nombreLabel.FontSize = 24;
            nombreLabel.TextColor = Color.White;
            apellidoLabel.FontSize = 24;
            apellidoLabel.TextColor = Color.White;

            //agregando hijos a las jerarquias de vistas

            horizontalLayout.Children.Add(imagenPerfil);
            horizontalLayout.Children.Add(nombreLabel);
            horizontalLayout.Children.Add(apellidoLabel);
            verticaLayout.Children.Add(horizontalLayout);
            verticaLayout.Children.Add(buttonAgregar);

            View = verticaLayout;
        }
    }
}
