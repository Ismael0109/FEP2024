using SQLite;

namespace AppNamespace
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Username { get; set; }

        [Unique]
        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
