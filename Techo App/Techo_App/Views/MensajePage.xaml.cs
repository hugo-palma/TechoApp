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
    public partial class MensajePage : ContentPage
    {
        MessageViewModel messageViewModel;
        public MensajePage(int idGrupo, string nombre)
        {
            InitializeComponent();
            this.Title = nombre;
            messageViewModel = new MessageViewModel(this, idGrupo);
            BindingContext = messageViewModel;
        }
    }
}