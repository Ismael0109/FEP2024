using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using AppNamespace;

namespace FEP2024
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TelaCadastro : ContentPage
    {
        public TelaCadastro()
        {
            InitializeComponent();
        }

        private async void CriarContaBt_Clicked(object sender, EventArgs e)
        {
            string usuaria = UsuariaEntry.Text;
            string email = EmailEntry.Text;
            string senha = SenhaEntry.Text;

            if (string.IsNullOrWhiteSpace(usuaria) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                await DisplayAlert("Erro", "Por favor, Preencha todos os campos!", "OK");
                return;
                    }
            User newUser = new User
            {
                Username = usuaria,
                Email = email,
                Senha = senha
            };

            try
            {
                // Tenta salvar o usuário no banco de dados
                await App.Database.SaveUserAsync(newUser);
                await DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "OK");

                // Navega para a tela de login ou outra página após o registro
                await Navigation.PopAsync(); // Volta para a página anterior (LoginPage)
            }
            catch (Exception ex)
            {
                // Exibe mensagem de erro caso haja problema ao salvar (ex: duplicação)
                await DisplayAlert("Erro", $"Erro ao registrar o usuário: {ex.Message}", "OK");
            }


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TelaJaPossuiCadastro());
            
        }
    }
}