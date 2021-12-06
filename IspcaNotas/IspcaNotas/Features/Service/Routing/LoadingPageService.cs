using IspcaNotas.Features.Interface;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace IspcaNotas.Features.Service.Routing
{
    public class LoadingPageService : IAuthenticationService
    {
        public async Task<bool> IsLogged()
        {
            Preferences.Remove("MyFirebaseToken");
            Preferences.Remove("MyEmail");
            Preferences.Remove("MySenha");

            string usuario = Preferences.Get("MyFirebaseToken", "doesnot");
            string email = Preferences.Get("MyEmail", "doesnot");
            string senha = Preferences.Get("MySenha", "doesnot");

            await Task.Delay(500);
            return usuario != "doesnot" || email != "doesnot" || senha != "doesnot";
        }
    }
}
