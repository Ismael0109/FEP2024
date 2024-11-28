using AppNamespace.Models;
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

            BindingContext = this; // Para garantir que o binding esteja funcional
            LoadTasks();           // Método que carrega as tarefas para aquele dia
        }

        private async void LoadTasks()
        {
            // Carrega as tarefas a partir do banco de dados para a data selecionada
            var tasks = await _database.GetTasksByDate(_selectedDate);

            // Certifique-se de que "tasks" é uma lista de TaskItem
            TasksListView.ItemsSource = tasks;
        }

        private void TasksListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
        }

        private async void TasksListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Verifique se o item é do tipo TaskItem antes de fazer o cast
            if (e.Item is TaskItem selectedTask)
            {
                // Formatar as horas de início e término
                string startTimeFormatted = selectedTask.StartTime.ToString(@"hh\:mm");
                string endTimeFormatted = selectedTask.EndTime.ToString(@"hh\:mm");

                // Exibir um alerta com as horas da tarefa
                await DisplayAlert("Detalhes da Tarefa",
                    $"Tarefa: {selectedTask.Title}\nInício: {startTimeFormatted}\nTérmino: {endTimeFormatted}\nDescrição: {selectedTask.Description}",
                    "OK");
            }
            else
            {
                await DisplayAlert("Erro", "O tipo de item selecionado não é válido.", "OK");
            }
        }
    }
    }


    
