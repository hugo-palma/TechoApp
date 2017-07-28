using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techo_App.Models;
using Techo_App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Techo_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLocationPage : ContentPage
    {
        SesionService sesionService;
        public AddLocationPage()
        {
            InitializeComponent();
            
            MainPicker.Items.Add("Santa Tecla");
            MainPicker.Items.Add("Antiguo Cuscatlan");
            MainPicker.Items.Add("San Benito");
            MainPicker.Items.Add("Paseo General Escalón");
            MainPicker.Items.Add("Salvador del mundo");
            MainPicker.Items.Add("Metrocentro");
            MainPicker.Items.Add("San Miguel");
            MainPicker.Items.Add("Santa Ana");
            MainPicker.Items.Add("Soyapango");
            MainPicker.Items.Add("Lourdes");
            initializeData();
        }
        private async Task initializeData()
        {
            sesionService = new SesionService();
            var exists = await sesionService.CheckSesionDbAsync();
            if (exists == false)
            {
                StackLayout sl = new StackLayout();
                
                Label l = new Label();
                l.HorizontalOptions = LayoutOptions.Center;
                l.VerticalOptions = LayoutOptions.Start;
                l.Text = "Necesitas iniciar sesion primero";
                l.FontSize = 24;
                sl.Children.Add(l);
                Content = sl;
            }
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
                subPicker.Items.Add("Centro Comercial Santa Rosa");
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
            if(seleccionado == "Paseo General Escalón")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("Semáforo Fuentes Beethoven");
                subPicker.Items.Add("Semáforo La Curacao, Paseo");
                subPicker.Items.Add("Semáforo Hotel Crowne Plaza");
                subPicker.Items.Add("Centro Comercial El Paseo");
                subPicker.Items.Add("Semáforo BK 75 Av.");
            }
            if (seleccionado == "Salvador del mundo")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("Semáforo Salvador del Mundo");
                subPicker.Items.Add("Centro Comercial Galerías");
                subPicker.Items.Add("Semáforo Av. Olímpica y Av. Francisco Gavidia");
                subPicker.Items.Add("Semáforo Texaco, Av. Manuel E. Araujo");
            }
            if (seleccionado == "Salvador del mundo")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("Semáforo Letras de Metrocentro");
                subPicker.Items.Add("Centro Comercial Metrocentro");
                subPicker.Items.Add("Centro Comercial Metrosur");
                subPicker.Items.Add("Semáforo Calle Gabriela Mistral frente al Mundo Feliz");
            }
            if (seleccionado == "San Miguel")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("San Miguel");
            }
            if (seleccionado == "Santa Ana")
            {
                subPicker.Items.Clear();
                subPicker.Items.Add("Santa Ana");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            LocationService locationService = new LocationService();
            Location location = new Location();
            SesionService sesionService = new SesionService();
            var idUsuarios = await sesionService.GetSesionIdUserDbAsync();
            location.idUser = idUsuarios;
            string lugar = subPicker.Items[subPicker.SelectedIndex];
            location.place = lugar;
            var result = await locationService.PostLoacationCheckInAsync(location);
            if(result == "successful")
            {
                btnEnviar.BackgroundColor = Color.FromHex("3d84f7");
                lblResultado.Text = "Check In realizado correctamente";
            }
            else if(result == "unsuccessful")
            {
                lblResultado.Text = "Hubo un problema al realizar el check in";
            }
        }
    }
}