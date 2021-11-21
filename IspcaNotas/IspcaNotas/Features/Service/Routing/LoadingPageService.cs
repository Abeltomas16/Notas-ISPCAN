using IspcaNotas.Features.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace IspcaNotas.Features.Service.Routing
{
    public class LoadingPageService : IAuthenticationService
    {
        public bool IsLogged()
        {
            Preferences.Remove("MyFirebaseToken");
            string usuario = Preferences.Get("MyFirebaseToken", "doesnot");
            return usuario != "doesnot";
        }
    }
}
