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
	public partial class TelaJaPossuiCadastro : ContentPage
	{
		public TelaJaPossuiCadastro ()
		{
			InitializeComponent ();
           
        
    }

        

        private async void CriarContaBt_Clicked_1(object sender, EventArgs e)
        {
            // Obter os valores digitados nos Entry
            string usernameOrEmail = UsuariaEntry.Text;
            string password = SenhaEntry.Text;

            // Verificar se as credenciais estão corretas no banco de dados
            var user = await App.Database.GetUserAsync(usernameOrEmail, password);

            if (user != null)
            {
                // Se as credenciais estiverem corretas, navegue para outra página
                await Navigation.PushAsync(new MainPage()); // Substitua "MainPage" com a página de destino
                await DisplayAlert("Bem Vinda de Volta", "Você Agora pode prosseguir ", "OK");
            }
            else
            {
                // Exibir uma mensagem de erro
                await DisplayAlert("Erro", "Nome de usuário, email ou senha incorretos.", "OK");
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TelaCadastro());
        }
    }
}