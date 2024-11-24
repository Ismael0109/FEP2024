using AppNamespace;
using AppNamespace.Models;
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
            _database.CreateTableAsync<TaskModel>().Wait();
            _database.CreateTableAsync<TaskItem>().Wait();
           
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
        // Método para salvar uma nova tarefa no banco de dados
        public Task<int> SaveTaskAsync(TaskItem task)
        {
            if (task.Id != 0)
            {
                // Atualiza a tarefa existente
                return _database.UpdateAsync(task);
            }
            else
            {
                // Insere uma nova tarefa
                return _database.InsertAsync(task);
            }
        }

        // Método para obter todas as tarefas
        public Task<List<TaskItem>> GetTasksAsync()
        {
            return _database.Table<TaskItem>().ToListAsync();
        }

        // Método para obter uma tarefa específica por ID
        public Task<TaskItem> GetTaskAsync(int id)
        {
            return _database.Table<TaskItem>().Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        // Método para deletar uma tarefa
        public Task<int> DeleteTaskAsync(TaskItem task)
        {
            return _database.DeleteAsync(task);
        }
        // Método para buscar tarefas de uma data específica
        public Task<List<TaskModel>> GetTasksForDateAsync(DateTime date)
        {
            // Busque tarefas que coincidem com a data selecionada
            return _database.Table<TaskModel>()
                            .Where(t => t.Date.Date == date.Date)
                            .ToListAsync();
        }
        public async Task<List<TaskItem>> GetTasksForDate(DateTime date)
        {
            // Obter as tarefas com base na data
            var tasks = await _database.Table<TaskItem>()
                                        .Where(t => t.Date.Date == date.Date)
                                        .ToListAsync();
            return tasks;
        }
    }
}
    

