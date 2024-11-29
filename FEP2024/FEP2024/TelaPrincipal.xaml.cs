using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FEP2024
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TelaPrincipal : TabbedPage
    {
        public ObservableCollection<Message> Messages { get; set; }
        public TelaPrincipal()
        {
            InitializeComponent();
            // Inicializa a lista de mensagens com uma mensagem do bot
            Messages = new ObservableCollection<Message>
            {
                new Message { Sender = "Bot", Text = "Se sente Segura? Digite (s) para Sim ou (n) Para Não" }
            };

            // Associa a lista ao CollectionView

            MessagesCollection.ItemsSource = Messages;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            string userMessage = MessageEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(userMessage))
            {
                // Adiciona a mensagem do usuário
                Messages.Add(new Message { Sender = "Você", Text = userMessage });

                // Limpa o campo de entrada
                MessageEntry.Text = string.Empty;

                // Gera uma resposta do bot
                RespondToUser(userMessage);
            }
        }

        private void RespondToUser(string userMessage)
        {
            // Transforma a mensagem em minúsculas para evitar problemas de case
            userMessage = userMessage.ToLower();

            // Verifica o conteúdo da mensagem para gerar a resposta
            if (userMessage.Contains("n") || userMessage.Contains("N"))
            {
                Messages.Add(new Message { Sender = "Bot", Text = "Irei te Ajudar! Digite (1) Para ligar para a Polícia, (2) Para Enviar uma Chamada de ajuda para um Contato de Confiança ou (3) Para receber um passo a passo do que fazer." });
            }
            else if (userMessage.Contains("1"))
            {
                Messages.Add(new Message { Sender = "Bot", Text = "Iremos te redirecionar para o 180" });
            }
            else if (userMessage.Contains("2"))
            {
                Messages.Add(new Message { Sender = "Bot", Text = "Iremos te redirecionar para seu contato de confiança. Ao chegar lá digite SOS " });
            }
            else if (userMessage.Contains("3"))
            {
                Messages.Add(new Message
                {
                    Sender = "Bot",
                    Text = "Siga os seguintes Passos: " +
                    "1. Se possível, mantenha um telefone sempre carregado." +
                    "2. Identifique rotas de fuga." +
                    "3. Tenha contatos de emergência acessíveis. "
                });
            }
            else
            {
                Messages.Add(new Message { Sender = "Bot", Text = "Digite um número válido " });

            }

        }
    }

}


// Classe para representar uma mensagem
public class Message
{
    public string Sender { get; set; }
    public string Text { get; set; }
}