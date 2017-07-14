using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Techo_App.ViewModels;

namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpcionesPage : ContentPage
    {
        OpcionesViewModel opcionesViewModel;
        public OpcionesPage()
        {
            InitializeComponent();
            opcionesViewModel = new OpcionesViewModel(Navigation);
            BindingContext = opcionesViewModel;
        }
        
    }
}