using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.Views;
using Techo_App.Services;
using Techo_App.Models;
using System.Threading.Tasks;

namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : MasterDetailPage
    {
        public Image prueba;
        private SesionService sesionService;
        public ListView ListView { get { return listView; } }
        public MenuPage()
        {
            InitializeComponent();
            ImprimirImagen();
            Detail = new TabbedPage
            {
                Children =
                {
                    new EventosPage(),
                    new ConversacionesPage()
                }
            };
            var masterPageItem = new List<MasterPageItem>();
            masterPageItem.Add(new MasterPageItem
            {
                Title = "Principal",
                IconSource = "icon.png",
                TargetType = typeof(homePage)
            });
            masterPageItem.Add(new MasterPageItem
            {
                Title = "Opciones",
                IconSource = "settings.png",
                TargetType = typeof(OpcionesPage)
            });
            listView.ItemsSource = masterPageItem;
            ListView.ItemSelected += OnItemSelected;
        }
        private async Task ImprimirImagen()
        {
            sesionService = new SesionService();
            List<Sesion> listaSesiones = await sesionService.GetSesionDbAsync();
            if(listaSesiones.Count > 0)
            {
                Sesion sesion = listaSesiones[0];
                if (sesion != null)
                {
                    imagenPerfil.Source = ImageSource.FromUri(new Uri("http://www.palmapplicationsv.com/techoapp/public/" + sesion.photo));
                }
                else
                {
                    imagenPerfil.Source = ImageSource.FromFile("photo.png");
                }
            }
            else
            {
                imagenPerfil.Source = ImageSource.FromFile("photo.png");
            }
        }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if(item != null)
            {
                if(this.GetType() != item.TargetType)
                {
                    Page newPage = (Page)Activator.CreateInstance(item.TargetType);
                    await Navigation.PushAsync(newPage);
                    ListView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }
    }
}