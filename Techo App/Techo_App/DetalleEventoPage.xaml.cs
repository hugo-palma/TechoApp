using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleEventoPage : ContentPage
    {
        private Image _checklistImg;
        private Image _emailImg;
        private Image _amigosImg;
        private Image _llamarImg;

        public DetalleEventoPage(Evento evento)
        {
            InitializeComponent();
            labelEvento.Text = evento.nombre;
            _checklistImg = this.FindByName<Image>("checklistImg");
            _emailImg = this.FindByName<Image>("emailImg");
            _amigosImg = this.FindByName<Image>("amigosImg");
            _llamarImg = this.FindByName<Image>("llamarImg");

            //creando gesture para checklist
            var checklistTapGestureRecognizer = new TapGestureRecognizer();
            checklistTapGestureRecognizer.Tapped += async (sender, e) => {
                DependencyService.Get<ISound>().KeyboardClick();
                _checklistImg.Opacity = .5;
                await Task.Delay(200);
                _checklistImg.Opacity = 1;
                await Navigation.PushAsync(new ChecklistPage(evento));
            };
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
            var amigosTapGestureRecognizer = new TapGestureRecognizer();
            amigosTapGestureRecognizer.Tapped += async (sender, e) => {
                DependencyService.Get<ISound>().KeyboardClick();
                _amigosImg.Opacity = .5;
                await Task.Delay(200);
                _amigosImg.Opacity = 1;
                await Navigation.PushAsync(new AttendantsPage(evento));
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
            _checklistImg.GestureRecognizers.Add(checklistTapGestureRecognizer);
            _emailImg.GestureRecognizers.Add(emailTapGestureRecognizer);
            _amigosImg.GestureRecognizers.Add(amigosTapGestureRecognizer);
            _llamarImg.GestureRecognizers.Add(llamarTapGestureRecognizer);

            

        }
        public Command AddLocationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PushAsync(new AddLocationPage());
                });
            }
        }
    }
}