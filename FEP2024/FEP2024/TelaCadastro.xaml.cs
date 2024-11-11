using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

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
            Preferences.Set("UserSenha", senha);
            Preferences.Set("UserUsuaria", usuaria);
            Preferences.Set("UserEmail", email);

            await Navigation.PopAsync();

            await DisplayAlert("Sucesso!!", "Conta Criada com Sucesso!", "OK");
            return ;

        }
    }
}