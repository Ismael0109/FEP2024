using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEP2024
{
    public partial class App : Application
    {
        static Database database;

        // Inicializa a conexão com o banco de dados
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    // Define o caminho do banco de dados
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppDatabase.db3");
                    database = new Database(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#add8e6")
            };
           
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
