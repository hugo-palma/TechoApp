using Xamarin.Forms;
using Techo_App.Models;
namespace Techo_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CreateSesionTable();
            MainPage = new NavigationPage(new MenuPage());
            //MainPage = new Techo_App.MainPage();
        }
        public void CreateSesionTable()
        {
            var db = DependencyService.Get<ISQLite>().GetConnection();

            db.CreateTable<Sesion>();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
