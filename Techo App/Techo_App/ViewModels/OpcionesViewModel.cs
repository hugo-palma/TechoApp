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
        IAccountManagerService _services;
        INavigation Navigation;
        private SesionService sesionService;
        public OpcionesViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
            _services = DependencyService.Get<IAccountManagerService>();
        }
        public Command CerrarSesionCommand
        {
            get
            {
               return new Command(async () =>
               {
                   _services.EraseAll();
                   sesionService = new SesionService();
                   await sesionService.BorrarSesion();
                   await Navigation.PopAsync();
               });
            }
        }
    }
}
