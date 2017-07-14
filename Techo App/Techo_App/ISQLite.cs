using SQLite.Net.Async;
using SQLite.Net;

namespace Techo_App
{
    public interface ISQLite
    {
        void CloseConnection();
        SQLiteConnection GetConnection();

        SQLiteAsyncConnection GetAsyncConnection();
        void DeleteDatabase();
    }
}
