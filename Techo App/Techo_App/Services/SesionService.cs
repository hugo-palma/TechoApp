using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite.Net.Async;
using Xamarin.Forms;

using Techo_App.Services;
using Techo_App.Helpers;
using Techo_App.Models;
using Techo_App.RestClient;
using Newtonsoft.Json.Linq;

[assembly: Dependency(typeof(SesionService))]
namespace Techo_App.Services
{
    public class SesionService
    {
        private static readonly AsyncLock Locker = new AsyncLock();
        private SQLiteAsyncConnection Database { get; } = DependencyService.Get<ISQLite>().GetAsyncConnection();

        public async Task<List<Sesion>> GetSesionDbAsync()
        {
            using (await Locker.LockAsync())
            {
                return await Database.Table<Sesion>().Where(x => x.id > 0).ToListAsync();
            }
        }
        public async Task<int>GetSesionIdUserDbAsync()
        {
            var lista = new List<Sesion>();
            using (await Locker.LockAsync())
            {
                lista = await Database.Table<Sesion>().Where(x => x.id > 0).ToListAsync();
            }
            Sesion sesion = lista[0];
            return sesion.idUsuarios;
        }
        public async Task<bool>CheckSesionDbAsync()
        {
            var lista = new List<Sesion>();
            using (await Locker.LockAsync())
            {
                lista = await Database.Table<Sesion>().Where(x => x.id > 0).ToListAsync();
            }
            if(lista.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task SetSesionDbAsync(Sesion sesion)
        {
            using (await Locker.LockAsync())
            {
                await Database.InsertAsync(sesion);
            }
        }
        public async Task BorrarSesion()
        {
            using (await Locker.LockAsync())
            {
                await Database.DeleteAllAsync<Sesion>();
            }
        }
        /*
        public async Task<string> UpdateFirebaseIdToken(int idUsuarios, string firebaseUserId)
        {
            RestClient<FirebaseToken> restClient = new RestClient<FirebaseToken>("enviarToken");
            FirebaseToken firebaseToken = new FirebaseToken();
            firebaseToken.idUser = idUsuarios;
            firebaseToken.idFcm = firebaseUserId;
            string jsonResult = await restClient.PostAsync(firebaseToken);
            var status = JObject.Parse(jsonResult)["status"];
            if ((string)status == "sent")
            {
                return "succesful";
            }
            return "unsuccessful";
        }*/
    }
}
