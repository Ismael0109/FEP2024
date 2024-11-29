using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEP2024
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        private int _currentYear;
        private int _currentMonth;
        private List<Button> _dayButtons;
        private Database _database;

        public string CurrentMonthYear => new DateTime(_currentYear, _currentMonth, 1).ToString("MMMM yyyy");
        public CalendarPage()
        {
            InitializeComponent();
            // Caminho do banco de dados (ajuste o caminho conforme necessário)
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.db3");

            _database = new Database(dbPath); // Instância do banco de dados

            _currentYear = DateTime.Now.Year;
            _currentMonth = DateTime.Now.Month;
            _dayButtons = new List<Button>();

            // Configura a propriedade BindingContext para atualizar o título
            BindingContext = this;

            // Preenche o calendário com os dias do mês atual
            FillCalendar();
        }
        private void FillCalendar()
        {
            // Limpe qualquer botão existente para recriar o calendário
            CalendarGrid.Children.Clear();

            // Obtenha o primeiro dia da semana e o número total de dias no mês atual
            DateTime firstDayOfMonth = new DateTime(_currentYear, _currentMonth, 1);
            int daysInMonth = DateTime.DaysInMonth(_currentYear, _currentMonth);

            // Calcule o dia da semana do primeiro dia do mês (0 = domingo, 1 = segunda-feira, ...)
            int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            // Inicialize a contagem do dia
            int dayCounter = 1;

            // Preencha o calendário
            for (int row = 1; row <= 6; row++) // Até 6 semanas para cobrir todos os casos possíveis
            {
                for (int col = 0; col < 7; col++) // 7 dias da semana
                {
                    if (row == 1 && col < firstDayOfWeek)
                    {
                        // Preencha espaços vazios antes do primeiro dia do mês
                        CalendarGrid.Children.Add(new Label { Text = "", BackgroundColor = Color.Transparent }, col, row);
                    }
                    else if (dayCounter <= daysInMonth)
                    {
                        // Crie um botão para o dia válido
                        var dayButton = new Button
                        {
                            Text = dayCounter.ToString(),
                            BackgroundColor = Color.FromHex("#D7B3FF"), // Cor de fundo hexadecimal
                            TextColor = Color.Black
                        };

                        // Capture o valor do dia no escopo do loop, garantindo que esteja correto
                        int validDay = dayCounter;
                        dayButton.Clicked += (sender, args) => OnDayClicked(validDay);

                        // Adicione o botão ao grid
                        CalendarGrid.Children.Add(dayButton, col, row);

                        // Incrementar o contador de dias
                        dayCounter++;
                    }
                    else
                    {
                        // Preencha espaços vazios após o último dia do mês
                        CalendarGrid.Children.Add(new Label { Text = "", BackgroundColor = Color.Transparent }, col, row);
                    }
                }
            }
        }



        private async void OnDayClicked(int day)
        {
            // Certifique-se de que _currentMonth e _currentYear estão corretos
            if (_currentMonth >= 1 && _currentMonth <= 12 && _currentYear > 0 && day >= 1 && day <= DateTime.DaysInMonth(_currentYear, _currentMonth))
            {
                await Navigation.PushAsync(new AddTaskPage(day, _currentMonth, _currentYear, _database));
            }
            else
            {
                await DisplayAlert("Erro", "A data selecionada é inválida.", "OK");
            }
        }


    
        


        private void btMesAnterior_Clicked(object sender, EventArgs e)
        {
            // Muda para o mês anterior
            _currentMonth--;
            if (_currentMonth < 1)
            {
                _currentMonth = 12;
                _currentYear--;
            }
                // Atualiza a exibição do calendário
                OnPropertyChanged(nameof(CurrentMonthYear));
                FillCalendar();
            }

            private void btMesPosterior_Clicked(object sender, EventArgs e)
            {
            // Muda para o próximo mês
            _currentMonth++;
            if (_currentMonth > 12)
            {
                _currentMonth = 1;
                _currentYear++;
            }

            // Atualiza a exibição do calendário
            OnPropertyChanged(nameof(CurrentMonthYear));
            FillCalendar();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TelaPrincipal());
        }
    } } 
    
