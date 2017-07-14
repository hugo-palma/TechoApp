using SQLite.Net.Attributes;
namespace Techo_App.Models
{
    public class Sesion
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int idUsuarios { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string photo { get; set; }
        public int role { get; set; }
    }
}
