using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techo_App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactosPage : ContentPage
    {
        ContactosViewModel contactosViewModel;
        public ContactosPage()
        {
            InitializeComponent();
            contactosViewModel = new ContactosViewModel(Navigation, this);
            BindingContext = contactosViewModel;
        }
    }
}