using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Techo_App.Services;

namespace Techo_App.ViewModels
{
    public class OpcionesViewModel
    {
        INavigation Navigation;
        private SesionService sesionService;
        public OpcionesViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
        }
        public Command CerrarSesionCommand
        {
            get
            {
               return new Command(async () =>
               {
                   sesionService = new SesionService();
                   await sesionService.BorrarSesion();
                   await Navigation.PopAsync();
               });
            }
        }
    }
}
