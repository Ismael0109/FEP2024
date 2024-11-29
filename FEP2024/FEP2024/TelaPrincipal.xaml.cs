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
            // Inicializa a lista de mensagens com uma mensagem inicial do bot
            Messages = new ObservableCollection<Message>
    {
        new Message { Sender = "Bot", Text = "Se sente segura? Digite (s) para Sim ou (n) para Não." }
    };

            // Associa a lista ao CollectionView
            MessagesCollection.ItemsSource = Messages;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            string userMessage = MessageEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(userMessage))
            {
                // Adiciona a mensagem do usuário à coleção
                Messages.Add(new Message { Sender = "Você", Text = userMessage });

                // Limpa o campo de entrada
                MessageEntry.Text = string.Empty;

                // Responde à mensagem do usuário
                RespondToUser(userMessage);
            }
        }

        private void RespondToUser(string userMessage)
        {
            userMessage = userMessage.ToLower(); // Transforma a mensagem em minúsculas para evitar problemas de case

            switch (userMessage)
            {
                case "n":
                    Messages.Add(new Message
                    {
                        Sender = "Bot",
                        Text = "Irei te ajudar! Digite:\n" +
                               "(1) Para ligar para a Polícia,\n" +
                               "(2) Para enviar uma mensagem de ajuda a um contato de confiança,\n" +
                               "(3) Para receber um passo a passo do que fazer."
                    });
                    break;

                case "1":
                    Messages.Add(new Message { Sender = "Bot", Text = "Iremos te redirecionar para o 180." });
                    // Implementação futura para discagem
                    break;

                case "2":
                    Messages.Add(new Message { Sender = "Bot", Text = "Iremos redirecionar para o seu contato de confiança. Ao chegar lá, digite SOS." });
                    // Implementação futura para compartilhar localização
                    break;

                case "3":
                    Messages.Add(new Message
                    {
                        Sender = "Bot",
                        Text = "Siga os seguintes passos:\n" +
                               "1. Mantenha seu telefone sempre carregado.\n" +
                               "2. Identifique rotas de fuga seguras.\n" +
                               "3. Tenha contatos de emergência acessíveis."
                    });
                    break;

                case "s":
                    Messages.Add(new Message { Sender = "Bot", Text = "Ótimo! Qualquer coisa, estou aqui para ajudar." });
                    break;

                default:
                    Messages.Add(new Message { Sender = "Bot", Text = "Por favor, digite uma opção válida (s, n, 1, 2 ou 3)." });
                    break;
            }
        


        PessoasListView.ItemsSource = GetPessoas();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var pessoa = e.Item as Pessoa;
                DisplayAlert("Detalhes", $"Nome: {pessoa.Nome}\nLocalização: {pessoa.TelaLocalizacao}", "OK");
            }
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            PessoasListView.ItemsSource = GetPessoas();


        }

        private List<Pessoa> GetPessoas()
        {
            return new List<Pessoa>
            {
               new Pessoa { Nome = "Ismael", TelaLocalizacao = "Rua Joaquim Pires Cerveira, 220"},
                new Pessoa { Nome = "Ana Elisa", TelaLocalizacao = "Rua Nova Ponte, 120"},
                new Pessoa { Nome = "Karol", TelaLocalizacao = "Rua Alexandre Vannucchi Leme, 18"}
            };
        }
        public class Pessoa
        {
            public string Nome { get; set; }
            public string TelaLocalizacao { get; set; }
        }

        private async void btItens_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Itens Básicos", "1. Medicamentos e Curativos .\n" +
                "2. Mochila com roupas, mas separe em sacolas, para evitar a desconfiança por parte do agressor, podendo simbolizar alguma roupa suja que deverá ser lavada e separe ao longo dos dias.\n"+
                "3. Separe e faça cópia dos Documento: RG, CPF, Certidão de Nascimento, Carteira de Trabalho, Título de Eleitor e o Cartão Saúde", "OK");
            return;
        }

        private async void btPassos_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Passos para o Escape", "1. Salve contatos de confiança e coloque (CCC) antes, para encontrá-los rapidamente\n" + "2. Se tiver vizinho de confiança, peça que o mesmo observe a casa e ligue 190, caso veja algo estranho.\n" + "3. Procure Companhia que confie para ir registrar o boletim de ocorrência na delegacia contra o(a) agressor(a)", "OK");
            return;
        }

        private async void btTransporte_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Transporte", "1. Qual será utilizado.\n" +
               "2. Valores para pagar a passagem ou gasolina.\n" 
               , "OK");
            return;
        }

        private async void btCriancas_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Crianças e Adolescentes", "1. Explique a situação com um linguajar adequado.\n" +
              "2. Em caso de emergência, oriente o menor de idade à pedir ajuda, ligar para o 190 ou falar com vizinhos de confiança.\n"+
              "3. Se possível, ligue para a escola em que estuda, para deixar os diretores da instituição cientes das situações." +
              "4. Após a fuga, registre um boletim de ocorrência e abra medidas protetivas para você e seu filho." , "OK");
            return;
        }

        private async void btSeguranca_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Segurança Pessoal", "1. Sempre que possível, tire fotos, prints, grave áudios e consiga provas das violências sofridas, independente se forem físicas, verbais, sexuais ou psicológicas.\n" +
              "2. Envie os registros para mais de uma pessoa que você tenha confiança.\n" +
              "3. Se não puder evitar a violência, vá para um canto e mantenha-se com o rosto protegido e os braços ao redor de cada lado da cabeça, com os dedos entrelaçados." , "OK");
            return;
        }

        private async void btRenda_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bens e Renda", "1. Guarde consigo cartões de banco, senhas e outros itens, dos quais precisará para fazer a movimentação bancária.\n" +
             "2. Se puder, tenha dinheiro vivo em mãos e, em caso de precisar guardá-lo, mantenha com uma pessoa que confia ou em um local sigiloso. \n" +
             "3. Guarde comprovantes de pagamento de imóvel, mais ainda, se for FGTS ou dinheiro por herança, pois é direito seu.", "OK");
            return;
        }
    }





    // Classe para representar uma mensagem
    public class Message
    {
        public string Sender { get; set; }
        public string Text { get; set; }
    }
}