using SQLite;
using System;

namespace AppNamespace.Models
{
    // Modelo de dados da tarefa
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // ID da tarefa (chave primária)

        public string Title { get; set; } // Título da tarefa
        public string Description { get; set; } // Descrição da tarefa
        public DateTime Date { get; set; } // Data da tarefa
        public TimeSpan StartTime { get; set; } // Hora de início
        public TimeSpan EndTime { get; set; } // Hora de término
    }
}
