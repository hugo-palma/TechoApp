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
        public ObservableCollection<Voluntario> usuarios { get; set; }
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
            var listaTemp = new List<Voluntario>();

            friendsService = new FriendsService();
            int idUsuario = await sesionService.GetSesionIdUserDbAsync();
            listaTemp = await friendsService.GetUsersByEventAsync(idUsuario, evento.idEventos);

            usuarios = new ObservableCollection<Voluntario>();
            ListView listView = new ListView();
            listView.RowHeight = 130;
            ListViewBehaviorNoSelected lBNS = new ListViewBehaviorNoSelected();
            listView.Behaviors.Add(lBNS);
            listView.ItemTemplate = new DataTemplate(typeof(CustomUsuarioCell));
            foreach (var voluntario in listaTemp)
            {
                if(voluntario.amigos == 0)
                {
                    voluntario.textoBoton = "Solicitud Pendiente";
                }
                else if(voluntario.amigos == 1)
                {
                    voluntario.textoBoton = "Ver";
                }
                else if(voluntario.amigos == 2)
                {
                    voluntario.textoBoton = "Enviar Solicitud";
                }

                if(voluntario.foto == null)
                {
                    voluntario.foto = "photo.png";
                }
                else
                {
                    voluntario.foto = "http://www.palmapplicationsv.com/techoapp/public/" + voluntario.foto;
                }
                usuarios.Add(voluntario);
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
            var completoVerticalLayout = new StackLayout();
            var completoHorizontalLayout = new StackLayout();
            var horizontalLayout = new StackLayout();
            var boxview = new BoxView();
            //asignando bindings
            nombreLabel.SetBinding(Label.TextProperty, new Binding("nombre"));
            apellidoLabel.SetBinding(Label.TextProperty, new Binding("apellido"));
            Image temporal = new Image();
            tempLabel.SetBinding(Label.TextProperty, new Binding("foto"));
            imagenPerfil.SetBinding(Image.SourceProperty, "foto");
            buttonAgregar.SetBinding(Button.TextProperty, "textoBoton");

            //asignando propiedas de diseño
            completoHorizontalLayout.BackgroundColor = Color.FromHex("025d91");
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            completoHorizontalLayout.Orientation = StackOrientation.Horizontal;
            completoHorizontalLayout.HorizontalOptions = LayoutOptions.Fill;
            buttonAgregar.BackgroundColor = Color.FromHex("3d84f7");
            verticaLayout.HorizontalOptions = LayoutOptions.Center;
            imagenPerfil.HorizontalOptions = LayoutOptions.Start;
            imagenPerfil.WidthRequest = 100;
            imagenPerfil.HeightRequest = 100;
            nombreLabel.FontSize = 24;
            nombreLabel.TextColor = Color.White;
            apellidoLabel.FontSize = 24;
            apellidoLabel.TextColor = Color.White;
            //completoVerticalLayout.BackgroundColor = Color.Red;
            //boxview.HeightRequest = 5;
            //agregando hijos a las jerarquias de vistas

            
            horizontalLayout.Children.Add(nombreLabel);
            horizontalLayout.Children.Add(apellidoLabel);
            verticaLayout.Children.Add(horizontalLayout);
            verticaLayout.Children.Add(buttonAgregar);
            completoHorizontalLayout.Children.Add(imagenPerfil);
            completoHorizontalLayout.Children.Add(verticaLayout);
            completoVerticalLayout.Children.Add(completoHorizontalLayout);
            //completoVerticalLayout.Children.Add(boxview);
            View = completoVerticalLayout;
        }
    }
}
