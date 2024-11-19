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

        

        private void CriarContaBt_Clicked_1(object sender, EventArgs e)
        {

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