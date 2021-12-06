using IspcaNotas.ViewModel;
using Splat;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActividadesTabbed : ContentPage
    {
        ActividadesViewModel Service { get; } = Locator.Current.GetService<ActividadesViewModel>();
        public ActividadesTabbed()
        {
            InitializeComponent();
            BindingContext = Service;
        }
        private async void MenuPerfil_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfil());
        }

        private async void MenuSobre_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Sobre());
        }

        private void MenuLogout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyEmail");
            Preferences.Remove("MySenha");
            Preferences.Remove("MyFirebaseToken");
            Preferences.Remove("Categoria");
            Application.Current.Properties.Clear();

            Process.GetCurrentProcess().Kill();
        }
    }
}