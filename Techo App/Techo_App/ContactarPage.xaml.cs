using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactarPage : ContentPage
    {
        private Image _checklistImg;
        private Image _emailImg;
        private Image _amigosImg;
        private Image _llamarImg;

        public ContactarPage()
        {
            InitializeComponent();
            
            _emailImg = this.FindByName<Image>("emailImg");
            _llamarImg = this.FindByName<Image>("llamarImg");
            //creando gesture para email
            var emailTapGestureRecognizer = new TapGestureRecognizer();
            emailTapGestureRecognizer.Tapped += async (sender, e) => {
                DependencyService.Get<ISound>().KeyboardClick();
                _emailImg.Opacity = .5;
                await Task.Delay(200);
                _emailImg.Opacity = 1;
                Device.OpenUri(new Uri("mailto:alejandra.svendblad@techo.org"));
                //await Navigation.PushAsync(new EventosPage());
            };
            var llamarTapGestureRecognizer = new TapGestureRecognizer();
            llamarTapGestureRecognizer.Tapped += async (sender, e) => {
                DependencyService.Get<ISound>().KeyboardClick();
                _llamarImg.Opacity = .5;
                await Task.Delay(200);
                _llamarImg.Opacity = 1;
                Device.OpenUri(new Uri("tel:22433655"));
                //await Navigation.PushAsync(new EventosPage());
            };
            //Asignando el gesture a los componentes de la vista
            _emailImg.GestureRecognizers.Add(emailTapGestureRecognizer);
            _llamarImg.GestureRecognizers.Add(llamarTapGestureRecognizer);

        }
    }
}