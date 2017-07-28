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
            _amigosImg = this.FindByName<Image>("amigosImg");

            //creando gesture para checklist
            var checklistTapGestureRecognizer = new TapGestureRecognizer();
            checklistTapGestureRecognizer.Tapped += async (sender, e) => {
                DependencyService.Get<ISound>().KeyboardClick();
                _checklistImg.Opacity = .5;
                await Task.Delay(200);
                _checklistImg.Opacity = 1;
                await Navigation.PushAsync(new ChecklistPage(evento));
            };
            var amigosTapGestureRecognizer = new TapGestureRecognizer();
            amigosTapGestureRecognizer.Tapped += async (sender, e) => {
                DependencyService.Get<ISound>().KeyboardClick();
                _amigosImg.Opacity = .5;
                await Task.Delay(200);
                _amigosImg.Opacity = 1;
                await Navigation.PushAsync(new AttendantsPage(evento));
            };
            //Asignando el gesture a los componentes de la vista
            _checklistImg.GestureRecognizers.Add(checklistTapGestureRecognizer);
            _amigosImg.GestureRecognizers.Add(amigosTapGestureRecognizer);

            tItem.Command = new Command(async () =>
            {
                await Navigation.PushAsync(new AddLocationPage());
            });

        }
    }
}