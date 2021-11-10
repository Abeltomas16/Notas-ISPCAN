using IspcaNotas.Features.Interface;
using IspcaNotas.Features.Service.Routing;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;

namespace IspcaNotas.ViewModel
{
    public class LoadingViewModel
    {
        private IRouting routingService;
        private IAuthenticationService AuthenticationService;
        public LoadingViewModel(IRouting routing = null, IAuthenticationService authentication = null)
        {
            routingService = routing ?? Locator.Current.GetService<IRouting>();
            AuthenticationService = authentication ?? Locator.Current.GetService<IAuthenticationService>();
        }
        public async void Init()
        {
            var isAuthenticated = AuthenticationService.IsLogged();
            if (isAuthenticated)
            {
                await routingService.NavigateTo("certo");
            }
            else
            {
                await this.routingService.NavigateTo("///main/home");
            }
        }
    }
}
