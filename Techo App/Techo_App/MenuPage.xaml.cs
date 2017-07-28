using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.Views;
using Techo_App.Services;
using Techo_App.Models;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : MasterDetailPage
    {
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
                TargetType = typeof(EventosPage)
            });
            masterPageItem.Add(new MasterPageItem
            {
                Title = "Contacto",
                IconSource = "call.png",
                TargetType = typeof(ContactarPage)
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
            if (await sesionService.CheckSesionDbAsync() == true)
            {
                List<Sesion> listaSesiones = await sesionService.GetSesionDbAsync();
                if (listaSesiones.Count > 0)
                {
                    Sesion sesion = listaSesiones[0];
                    nombreUsuario.Text = sesion.firstName + " " + sesion.lastName;
                    if (sesion.photo != null)
                    {
                        if (sesion.photo.Contains("https:"))
                        {
                            imagenPerfil.Source = ImageSource.FromUri(new Uri(sesion.photo));
                        }
                        else
                        {
                            imagenPerfil.Source = ImageSource.FromUri(new Uri("http://www.palmapplicationsv.com/techoapp/public/" + sesion.photo));
                        }
                    }
                    else
                    {
                        imagenPerfil.Source = ImageSource.FromFile("photo.png");
                    }
                }
            }
            else
            {
                nombreUsuario.Text = "";
                imagenPerfil.Source = ImageSource.FromFile("photo.png");
            }
        }
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if(item != null)
            {
                if(this.Detail.GetType() != item.TargetType)
                {
                    if(this.GetType() == typeof(EventosPage))
                    {
                         Detail = (Page)Activator.CreateInstance(item.TargetType);
                    }
                    else
                    {
                        Page newPage = (Page)Activator.CreateInstance(item.TargetType);
                        await Navigation.PushAsync(newPage);
                    }
                    ListView.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }
        protected override void OnAppearing()
        {
            ImprimirImagen();
            Detail = new TabbedPage
            {
                Children =
                {
                    new EventosPage(),
                    new ConversacionesPage()
                }
            };
            base.OnAppearing();
        }
    }
}