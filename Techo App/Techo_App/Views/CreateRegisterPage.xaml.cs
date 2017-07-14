
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Diagnostics;
using Techo_App.ViewModels;
using Techo_App.Models;
namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRegisterPage : ContentPage
    {
        private MediaFile _mediaFile;
        private UsuariosViewModel usuariosViewModel;
        public CreateRegisterPage(Evento evento)
        {
            InitializeComponent();
            usuariosViewModel = new UsuariosViewModel(evento);
            usuariosViewModel.Navigation = Navigation;
            BindingContext = usuariosViewModel;
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();
            if (_mediaFile == null)
                return;

            FileImage.Source = ImageSource.FromStream(() =>
            {
                var stream = _mediaFile.GetStream();
                return stream;
            });
            usuariosViewModel.setImage(_mediaFile);
        }        
    }
}