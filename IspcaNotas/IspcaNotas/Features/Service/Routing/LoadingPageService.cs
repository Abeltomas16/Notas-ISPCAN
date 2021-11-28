using IspcaNotas.Features.Interface;
using Xamarin.Essentials;

namespace IspcaNotas.Features.Service.Routing
{
    public class LoadingPageService : IAuthenticationService
    {
        public bool IsLogged()
        {

            string usuario = Preferences.Get("MyFirebaseToken", "doesnot");
            string email = Preferences.Get("MyEmail", "doesnot");
            string senha = Preferences.Get("MySenha", "doesnot");
            return usuario != "doesnot" || email != "doesnot" || senha != "doesnot";
        }
    }
}
