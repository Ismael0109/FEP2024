using AppNamespace.Models;
using System;
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
            BindingContext = this;
            LoadTasks();
        }

        private async void LoadTasks()
        {
            var tasks = await _database.GetTasksByDate(_selectedDate);
            TasksListView.ItemsSource = tasks;
        }

        private void TasksListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private async void TasksListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is TaskItem selectedTask)
            {
                string startTimeFormatted = selectedTask.StartTime.ToString(@"hh\:mm");
                string endTimeFormatted = selectedTask.EndTime.ToString(@"hh\:mm");

                await DisplayAlert("Detalhes da Tarefa",
                    $"Tarefa: {selectedTask.Title}\nInício: {startTimeFormatted}\nTérmino: {endTimeFormatted}\nDescrição: {selectedTask.Description}",
                    "OK");
            }
            else
            {
                await DisplayAlert("Erro", "O tipo de item selecionado não é válido.", "OK");
            }
        }

        // Função para apagar a tarefa
        private async void DeleteTaskButton_Clicked(object sender, EventArgs e)
        {
            if (TasksListView.SelectedItem is TaskItem selectedTask)
            {
                bool confirm = await DisplayAlert("Confirmação", $"Deseja apagar a tarefa \"{selectedTask.Title}\"?", "Sim", "Não");
                if (confirm)
                {
                    await _database.DeleteTaskAsync(selectedTask);
                    await DisplayAlert("Sucesso", "Tarefa apagada com sucesso!", "OK");
                    LoadTasks(); // Recarrega a lista de tarefas
                }
            }
            else
            {
                await DisplayAlert("Erro", "Nenhuma tarefa selecionada!", "OK");
            }

        }
      
        }
    }



