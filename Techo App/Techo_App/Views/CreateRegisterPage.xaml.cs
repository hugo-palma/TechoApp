
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Techo_App.ViewModels;
using Techo_App.Models;
namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateRegisterPage : ContentPage
    {
        private MediaFile _mediaFile;
        private UsuariosViewModel usuariosViewModel;
        public CreateRegisterPage(Evento evento, RegisterPage registerPage)
        {
            InitializeComponent();
            usuariosViewModel = new UsuariosViewModel(evento, Navigation, registerPage);
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