using System;
using System.IO;
using Xamarin.Forms;
using SQLite.Net.Async;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Techo_App.Droid;
[assembly: Dependency(typeof(SQLiteAndroid))]
namespace Techo_App.Droid
{
    public class SQLiteAndroid : ISQLite
    {
        private SQLiteConnectionWithLock _conn;
        public SQLiteAndroid()
        {

        }
        private static string GetDatabasePath()
        {
            const string sqliteFilename = "mydatabase.db3";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);

            return path;
        }
        public SQLiteConnection GetConnection()
        {
            var dbPath = GetDatabasePath();
            return new SQLiteConnection(new SQLitePlatformAndroid(), dbPath);
        }
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var dbPath = GetDatabasePath();

            var platForm = new SQLitePlatformAndroid();

            var connectionFactory = new Func<SQLiteConnectionWithLock>(
                () =>
                {
                    if(_conn == null)
                    {
                        _conn = new SQLiteConnectionWithLock(platForm, new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: true));
                    }
                    return _conn;

                });
            return new SQLiteAsyncConnection(connectionFactory);
        }
        public void DeleteDatabase()
        {
            var path = GetDatabasePath();
            try
            {
                if(_conn != null)
                {
                    _conn.Close();
                }
            }
            catch
            {
                //no hay que preocuparse si tira una exception
            }
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            _conn = null;
        }
        public void CloseConnection()
        {
            if( _conn != null)
            {
                _conn.Close();
                _conn.Dispose();
                _conn = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}