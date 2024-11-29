using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FEP2024
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btAna_Clicked(object sender, EventArgs e)
        {

        }

        private void btIsmael_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TelaCadastro());
        }

        private void btKarol_Clicked(object sender, EventArgs e)
        {

        }

        private void btLuisa_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TelaPrincipal());
        }

        private void btMi_Clicked(object sender, EventArgs e)
        {

        }
    }
}
