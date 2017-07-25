using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLocationPage : ContentPage
    {
        public AddLocationPage()
        {
            InitializeComponent();
            MainPicker.Items.Add("Santa Tecla");
            MainPicker.Items.Add("Antiguo Cuscatlan");
            MainPicker.Items.Add("San Benito");
        }

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccionado = MainPicker.Items[MainPicker.SelectedIndex];
            if(seleccionado == "Santa Tecla")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("McDonald's Plaza Merliot");
                subPicker.Items.Add("Semáforo Santa Mónica");
                subPicker.Items.Add("Semáforo Plaza Merliot");
                subPicker.Items.Add("Centro Comercial Plaza Merliot");
            }
            if (seleccionado == "Antiguo Cuscatlan")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("Centro Comercial Multiplaza");
                subPicker.Items.Add("Semáforo Casino Colonial");
                subPicker.Items.Add("Semáforo Frente a La Gran Vía");
                subPicker.Items.Add("McDonald's Santa Elena");
                subPicker.Items.Add("Rendondel orden de malta");
            }
            if (seleccionado == "San Benito")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("Semáforo La Taberna");
                subPicker.Items.Add("Semáforo Las Azaleas");
                subPicker.Items.Add("Semáforo CIFCO");
                subPicker.Items.Add("Calle entre marte y sheraton");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}