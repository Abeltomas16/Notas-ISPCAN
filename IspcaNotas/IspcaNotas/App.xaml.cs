﻿using Firebase.Auth;
using Firebase.Database;
using IspcaNotas.Features.Interface;
using IspcaNotas.Features.Service.Actividades;
using IspcaNotas.Features.Service.Cadeira;
using IspcaNotas.Features.Service.Login;
using IspcaNotas.Features.Service.Routing;
using IspcaNotas.Features.Service.usuario;
using IspcaNotas.ViewModel;
using Splat;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas
{
    public partial class App : Application
    {
        public App()
        {
            Dependences();
            InitializeComponent();

            XF.Material.Forms.Material.Init(this);
            MainPage = new AppShell();
        }

        private void Dependences()
        {
            Locator.CurrentMutable.RegisterLazySingleton<IRouting>(() => new IRoutingService());
            Locator.CurrentMutable.RegisterLazySingleton<IAuthenticationService>(() => new LoadingPageService());
            Locator.CurrentMutable.RegisterLazySingleton<ILogin>(() => new LoginService());
            Locator.CurrentMutable.RegisterLazySingleton<IUsuario>(() => new UsuarioService());
            Locator.CurrentMutable.RegisterLazySingleton<IActividades>(() => new ActividadseService());
            Locator.CurrentMutable.RegisterLazySingleton<ICadeira>(() => new CadeiraService());


            Locator.CurrentMutable.Register(() => new CadeiraViewModel());
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
            Locator.CurrentMutable.Register(() => new LoginViewModel());
            Locator.CurrentMutable.Register(() => new ActividadesViewModel());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
