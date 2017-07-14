using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Techo_App.ViewModels;

namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventosPage : ContentPage
    {
        public EventosPage()
        {
            InitializeComponent();
            EventosViewModel eventosViewModel = new EventosViewModel(Navigation);
            BindingContext = eventosViewModel;
        }
    }
}