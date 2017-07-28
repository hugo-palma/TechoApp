using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Techo_App.ViewModels;

namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConversacionesPage : ContentPage
    {
        ConversacionesViewModel conversacionesViewModel;
        public ConversacionesPage()
        {
            InitializeComponent();
            //conversacionesViewModel = new ConversacionesViewModel(Navigation, this);
            //BindingContext = conversacionesViewModel;
            
        }
    }
}