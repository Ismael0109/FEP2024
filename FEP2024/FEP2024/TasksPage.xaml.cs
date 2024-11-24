using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEP2024
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksPage : ContentPage
    {
        private DateTime _selectedDate;
        private Database _database;

        public TasksPage(DateTime selectedDate, Database database)
        {
            InitializeComponent();
            _selectedDate = selectedDate;
            _database = database;

            // Use a data e o banco para carregar as tarefas para esse dia
            LoadTasksForDate();
        }

        private async void LoadTasksForDate()
        {
            // Carregar as tarefas da data selecionada
            var tasks = await _database.GetTasksForDate(_selectedDate);

            // Exemplo: Mostrar tarefas em uma lista
            TasksListView.ItemsSource = tasks;
        }
    }
    }
