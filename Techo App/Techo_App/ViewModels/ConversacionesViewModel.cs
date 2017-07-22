using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Collections.ObjectModel;

using Techo_App.Models;
using Techo_App.Services;
using System.Threading.Tasks;
using Techo_App.Views;

namespace Techo_App.ViewModels
{
    public class ConversacionesViewModel : INotifyPropertyChanged
    {

        INavigation Navigation;
        private ContentPage _contentpage;
        private SesionService sesionService;
        private FriendsService friendsService;
        private ListView _listView;
        public ListView listView
        {
            get { return _listView; }
            set
            {
                _listView = value;
                OnPropertyChanged(nameof(listView));
            }
        }
        private ObservableCollection<Usuario> _usuariosOC;
        private ObservableCollection<Usuario> usuariosOC
        {
            get
            {
                return _usuariosOC;
            }
            set
            {
                _usuariosOC = value;
                OnPropertyChanged();
            }
        }
        public ContentPage contentpage {
            get { return _contentpage; }
            set
            {
                _contentpage = value;
                OnPropertyChanged(nameof(contentpage));
            }
                
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private StackLayout _stackLayout;
        public StackLayout stackLayout
        {
            get { return _stackLayout; }
            set { _stackLayout = value;
                OnPropertyChanged(nameof(stackLayout));
            }
        }
        private ActivityIndicator _ai;
        public ActivityIndicator ai
        {
            get { return _ai; }
            set
            {
                _ai = value;
                OnPropertyChanged(nameof(ai));
            }
        }
        public ConversacionesViewModel(INavigation Navigation, ContentPage contentPage)
        {
            this.Navigation = Navigation;
            _contentpage = contentPage;
            _listView = new ListView();
            _stackLayout = new StackLayout();
            _refreshCommand = new Command(RefreshListView);
            _usuariosOC = new ObservableCollection<Usuario>();
            
            InitializeDataAsync();
            
        }
        private async Task InitializeDataAsync()
        {
            sesionService = new SesionService();
            friendsService = new FriendsService();
            int idUsuario = await sesionService.GetSesionIdUserDbAsync();
            
            if(idUsuario != 0)
            {
                ai = new ActivityIndicator();
                ai.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
                ai.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
                stackLayout.Children.Add(ai);
                IsBusy = true;
                contentpage.Content = stackLayout;
                var listaTemp = await friendsService.GetFriendsById(idUsuario);
                usuariosOC = new ObservableCollection<Usuario>();

                listView.RowHeight = 75;
                listView.ItemTemplate = new DataTemplate(typeof(CustomFriendsCell));
                foreach (var usuario in listaTemp)
                {
                    if (usuario.foto == null)
                    {
                        usuario.foto = "photo.png";
                    }
                    else
                    {
                        if (usuario.foto.Contains("https:"))
                        {

                        }
                        else
                        {
                            usuario.foto = "http://www.palmapplicationsv.com/techoapp/public/" + usuario.foto;
                        }
                    }
                    usuariosOC.Add(usuario);
                }
                listView.ItemsSource = usuariosOC;
                listView.IsPullToRefreshEnabled = true;
                listView.SetBinding(ListView.RefreshCommandProperty, new Binding("RefreshCommand"));
                listView.ItemTapped += ListView_ItemTapped;
                stackLayout.Children.Add(listView);
                IsBusy = false;
            }
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new MensajePage());
        }

        async void RefreshListView()
        {
            //IsBusy = true;
            sesionService = new SesionService();
            friendsService = new FriendsService();
            int idUsuario = await sesionService.GetSesionIdUserDbAsync();
            var listaTemp = await friendsService.GetFriendsById(idUsuario);
            foreach (var usuario in listaTemp)
            {
                if (usuario.foto == null)
                {
                    usuario.foto = "photo.png";
                }
                else
                {
                    if (usuario.foto.Contains("https:"))
                    {

                    }
                    else
                    {
                        usuario.foto = "http://www.palmapplicationsv.com/techoapp/public/" + usuario.foto;
                    }
                }
            }
            listView.IsRefreshing = false;
            //IsBusy = false;
        }
        Command _refreshCommand;
        public Command RefreshCommand
        {
            get { return _refreshCommand; }
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class CustomFriendsCell : ViewCell
    {
        public CustomFriendsCell()
        {
            var tempLabel = new Label();
            var imagenPerfil = new Image();
            var nombreLabel = new Label();
            var apellidoLabel = new Label();
            var verticaLayout = new StackLayout();
            var completoVerticalLayout = new StackLayout();
            var completoHorizontalLayout = new StackLayout();
            var horizontalLayout = new StackLayout();


            //asignando bindings
            nombreLabel.SetBinding(Label.TextProperty, new Binding("nombre"));
            apellidoLabel.SetBinding(Label.TextProperty, new Binding("apellido"));
            imagenPerfil.SetBinding(Image.SourceProperty, "foto");
            
            

            //asignando propiedas de diseño
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            horizontalLayout.HorizontalOptions = LayoutOptions.FillAndExpand;
            completoHorizontalLayout.Orientation = StackOrientation.Horizontal;
            completoHorizontalLayout.HorizontalOptions = LayoutOptions.Fill;
            verticaLayout.HorizontalOptions = LayoutOptions.Center;
            imagenPerfil.HorizontalOptions = LayoutOptions.Start;
            imagenPerfil.WidthRequest = 70;
            imagenPerfil.HeightRequest = 70;
            nombreLabel.FontSize = 24;
            nombreLabel.TextColor = Color.Blue;
            apellidoLabel.FontSize = 24;
            apellidoLabel.TextColor = Color.Blue;
            //agregando hijos a las jerarquias de vistas


            horizontalLayout.Children.Add(nombreLabel);
            horizontalLayout.Children.Add(apellidoLabel);
            verticaLayout.Children.Add(horizontalLayout);
            completoHorizontalLayout.Children.Add(imagenPerfil);
            completoHorizontalLayout.Children.Add(verticaLayout);
            completoVerticalLayout.Children.Add(completoHorizontalLayout);
            View = completoVerticalLayout;
        }
    }
}
