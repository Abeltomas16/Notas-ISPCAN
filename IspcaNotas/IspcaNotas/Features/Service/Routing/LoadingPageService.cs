using IspcaNotas.Features.Interface;
using Xamarin.Essentials;

namespace IspcaNotas.Features.Service.Routing
{
    public class LoadingPageService : IAuthenticationService
    {
        public bool IsLogged()
        {
            Preferences.Remove("MyFirebaseToken");
            Preferences.Remove("MyEmail");
            Preferences.Remove("MySenha");
            string usuario = Preferences.Get("MyFirebaseToken", "doesnot");
            return usuario != "doesnot";
        }
    }
}
