using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IspcaNotas.ViewModel
{
    public class LoginViewModel
    {
        private string Senha = string.Empty;
        private string Email = string.Empty;
        public string _email
        {
            set { Email = value; }
        }
        public string _senha
        {
            set { Senha = value; }
        }
        IRouting routing;
        ILogin LoginNegocios;
        public ICommand ExecuteSignIn { get; }
        public LoginViewModel()
        {
            LoginNegocios = Locator.Current.GetService<ILogin>();
            this.routing = routing ?? Locator.Current.GetService<IRouting>();
            ExecuteSignIn = new Command(() => ValidarEntrada(this.Email, this.Senha));
        }

        internal async void ValidarEntrada(string email, string senha)
        {
            try
            {
                var resultado = await LoginNegocios.SignIn(email, Senha);
                if (resultado != null)
                    await routing.NavigateTo("///main/home");

            }
            catch 
            {
                await XF.Material.Forms.UI.Dialogs.MaterialDialog.Instance.SnackbarAsync("Senha errada", XF.Material.Forms.UI.Dialogs.MaterialSnackbar.DurationShort);
            }
        }

    }
}
