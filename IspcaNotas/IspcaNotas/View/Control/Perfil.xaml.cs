using IspcaNotas.Model;
using IspcaNotas.View.Animation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();
            var email = Preferences.Get("MyEmail", null);
            var name = Preferences.Get("MyName", null);
            var Telefone = Preferences.Get("MyTelefone", null);
            usuarios.Add(new UsuarioDTO
            {
                Email = email,
                Name = name,
                Telefone = Telefone
            });
            CollectionUser.ItemsSource = usuarios;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new MyPopupPage(Features.Enums.EnumPerfil.Email));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new MyPopupPage(Features.Enums.EnumPerfil.Senha));
        }
    }
}