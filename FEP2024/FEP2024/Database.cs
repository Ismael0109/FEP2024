using AppNamespace;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FEP2024
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        // Construtor que inicializa o banco de dados
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait(); // Cria a tabela de usuários se não existir
        }

        // Método para inserir um novo usuário
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        // Método para obter todos os usuários
        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        // Método para encontrar um usuário pelo nome ou email e senha
        public Task<User> GetUserAsync(string usernameOrEmail, string password)
        {
            return _database.Table<User>()
                            .Where(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Senha == password)
                            .FirstOrDefaultAsync();
        }
    }
}
    

