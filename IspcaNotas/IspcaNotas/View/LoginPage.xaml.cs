using IspcaNotas.Features.Interface;
using IspcaNotas.ViewModel;
using Splat;
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
    public partial class LoginPage : ContentPage
    {
        LoginViewModel ViewModel { get; set; } = Locator.Current.GetService<LoginViewModel>();
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
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
