using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite.Net.Async;
using Xamarin.Forms;

using Techo_App.Services;
using Techo_App.Helpers;
using Techo_App.Models;

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
    }
}
