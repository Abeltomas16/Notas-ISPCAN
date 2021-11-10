using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private async void BTEntrar(object sender, EventArgs e)
        {
               
        }
        private void AjudaFacebook(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.facebook.com/abel.tomas.16"));
        }
        private void MenuItem2_Clicked(object sender, EventArgs e)
        {
            var texto = (ToolbarItem)sender;
            DisplayAlert("info", texto.Text, "ok");
        }
    }
}
