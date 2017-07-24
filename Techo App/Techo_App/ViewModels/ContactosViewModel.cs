using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.Services;
using Techo_App.Views;
using Xamarin.Forms;

namespace Techo_App.ViewModels
{
    public class ContactosViewModel : INotifyPropertyChanged
    {
        INavigation Navigation;
        ContentPage contentPage;
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
        private StackLayout _stackLayout;
        public StackLayout stackLayout
        {
            get { return _stackLayout; }
            set
            {
                _stackLayout = value;
                OnPropertyChanged(nameof(stackLayout));
            }
        }
        public ContactosViewModel(INavigation Navigation, ContentPage contentPage)
        {
            this.contentPage = contentPage;
            this.Navigation = Navigation;
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

            if (idUsuario != 0)
            {
                ai = new ActivityIndicator();
                ai.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
                ai.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
                stackLayout.Children.Add(ai);
                IsBusy = true;
                contentPage.Content = stackLayout;
                var listaTemp = await friendsService.GetFriendsById(idUsuario);
                

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
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Debug.WriteLine(e.Item);
            var usuario = e.Item as Usuario;
            var sesionId = await sesionService.GetSesionIdUserDbAsync();
            int[] arreglo = new int[1];
            arreglo[0] = usuario.idUsuarios;
            var idGrupo = await friendsService.PostNuevaConversacionAsync(sesionId, arreglo, usuario.nombre);
            int idGrupoint = int.Parse((string) idGrupo);
            await Navigation.PushAsync(new MensajePage(idGrupoint, usuario.nombre));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
