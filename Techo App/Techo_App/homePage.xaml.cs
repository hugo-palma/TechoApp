using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.Views;

namespace Techo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class homePage : ContentPage
    {
        private Label _eventosLabel;
        private Image _eventosImg;
        
        public homePage()
        {
            InitializeComponent();
            //Obteniendo componentes de la vista xaml
            _eventosImg = this.FindByName<Image>("eventosImg");
            _eventosLabel = this.FindByName<Label>("eventosLabel");

            //creando gesture para eventos
            var EventosTapGestureRecognizer = new TapGestureRecognizer();
            EventosTapGestureRecognizer.Tapped += async (sender, e) => {
                DependencyService.Get<ISound>().KeyboardClick();
                _eventosImg.Opacity = .5;
                await Task.Delay(200);
                _eventosImg.Opacity = 1;
                await Navigation.PushAsync(new EventosPage());
            };

            //Asignando el gesture a los componentes de la vista
            _eventosImg.GestureRecognizers.Add(EventosTapGestureRecognizer);
            _eventosLabel.GestureRecognizers.Add(EventosTapGestureRecognizer);

            
        }
        
    }
}