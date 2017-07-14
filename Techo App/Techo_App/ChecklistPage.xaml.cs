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
    public partial class ChecklistPage : ContentPage
    {
        public ChecklistPage()
        {
            InitializeComponent();
        }

        private void switch1_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            if (isToggled)
            {
                sl1.BackgroundColor = Color.FromHex("0adb23");
            }
            else
            {
                sl1.BackgroundColor = Color.Default;
            }
            verificar();
        }
        private void switch2_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            if (isToggled)
            {
                sl2.BackgroundColor = Color.FromHex("0adb23");
            }
            else
            {
                sl2.BackgroundColor = Color.Default;
            }
            verificar();
        }

        private void switch3_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            if (isToggled)
            {
                sl3.BackgroundColor = Color.FromHex("0adb23");
            }
            else
            {
                sl3.BackgroundColor = Color.Default;
            }
            verificar();
        }
        private void verificar()
        {
            if(sl1.BackgroundColor != Color.Default && sl2.BackgroundColor != Color.Default && sl3.BackgroundColor != Color.Default)
            {
                btnFacebook.IsVisible = true;
            }
        }
    }
}