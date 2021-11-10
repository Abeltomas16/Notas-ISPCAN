using IspcaNotas.Features.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IspcaNotas.ViewModel
{
    class HomeViewModel
    {
        public HomeViewModel()
        {
            VerificaaLogin();
        }

        private async void VerificaaLogin()
        {
            var result = DependencyService.Resolve<IAuthenticationService>();
            await Shell.Current.GoToAsync("//LoginView");
        }
    }
}
