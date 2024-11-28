using AppNamespace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEP2024
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskPage : ContentPage
    {
        private DateTime _selectedDate;
        private Database _database;
        private int _selectedDay;
        private int _selectedMonth;
        private int _selectedYear;

        // Construtor que aceita a data selecionada
        public AddTaskPage(int day, int month, int year, Database database)
        {
            InitializeComponent();
            // Verifique se os valores do ano, mês e dia são válidos
            if (year < 1 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                throw new ArgumentOutOfRangeException("Os parâmetros fornecidos para a data são inválidos.");
            }

            _selectedDay = day;
            _selectedMonth = month;
            _selectedYear = year;
            _database = database;
        }


        private async void btSalvarTarefa_Clicked(object sender, EventArgs e)
        {
            // Obter os dados inseridos pelo usuário
            string title = TitleEntry.Text;
            string description = DescriptionEditor.Text;
            TimeSpan startTime = StartTimePicker.Time;
            TimeSpan endTime = EndTimePicker.Time;

            // Criar um objeto DateTime apenas se os parâmetros forem válidos
            try
            {
                var task = new TaskItem
                {
                    Title = title,
                    Description = description,
                    Date = new DateTime(_selectedYear, _selectedMonth, _selectedDay), // Verifique se o dia é válido
                    StartTime = startTime,
                    EndTime = endTime
                };

                // Salvar a tarefa no banco de dados
                await _database.SaveTaskAsync(task);

                // Confirmar que a tarefa foi salva
                await DisplayAlert("Sucesso", "Tarefa salva com sucesso!", "OK");

                // Voltar para a página anterior (calendário)
                await Navigation.PopAsync();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                await DisplayAlert("Erro", "A data fornecida é inválida. Por favor, verifique e tente novamente.", "OK");
            }

        }

        private async void btIrParaTelaTarefasDoDia_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedDate = new DateTime(_selectedYear, _selectedMonth, _selectedDay);
                await Navigation.PushAsync(new TasksPage(selectedDate, _database));
            }
            catch (ArgumentOutOfRangeException)
            {
                await DisplayAlert("Erro", "Data inválida!", "OK");
            }
        }

    }
}
    