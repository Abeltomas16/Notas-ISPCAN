using IspcaNotas.Features.Interface;
using System;
using System.Collections.Generic;
using System.Text;


namespace IspcaNotas.Features.Service.Routing
{
    public class LoadingPageService : IAuthenticationService
    {
        public bool IsLogged()
        {
            return false;
        }
    }
}
