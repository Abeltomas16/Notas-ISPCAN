using IspcaNotas.Features.Enums;
using IspcaNotas.Model;
using IspcaNotas.ViewModel;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           // await Navigation.PushAsync(new Perfil(_UsuarioCorrente));
        }

        private async void MenuSobre_Clicked(object sender, EventArgs e)
        {
         //   await Navigation.PushAsync(new Sobre());
        }
    }
}