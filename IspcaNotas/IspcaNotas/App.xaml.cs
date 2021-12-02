using Firebase.Auth;
using Firebase.Database;
using IspcaNotas.Features.Interface;
using IspcaNotas.Features.Service.Actividades;
using IspcaNotas.Features.Service.Cadeira;
using IspcaNotas.Features.Service.Login;
using IspcaNotas.Features.Service.Routing;
using IspcaNotas.Features.Service.usuario;
using IspcaNotas.ViewModel;
using Splat;
using Xamarin.Forms;

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
            Locator.CurrentMutable.RegisterLazySingleton<IDocente>(() => new DocenteService());
            Locator.CurrentMutable.RegisterLazySingleton<IUsuario>(() => new UsuarioService());
            Locator.CurrentMutable.RegisterLazySingleton<IActividades>(() => new ActividadesService());
            Locator.CurrentMutable.RegisterLazySingleton<ICadeira>(() => new CadeiraService());
            Locator.CurrentMutable.RegisterLazySingleton<INotas>(() => new NotasService());
            Locator.CurrentMutable.RegisterLazySingleton<IEstudante>(() => new EstudanteService());

            Locator.CurrentMutable.Register(() => new CadeiraViewModel("Cadeiras"), "Cadeira");
            Locator.CurrentMutable.Register(() => new CadeiraViewModel("CadeirasMostrar"), "CadeirasMostrar");
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
            Locator.CurrentMutable.Register(() => new LoginViewModel());
            Locator.CurrentMutable.Register(() => new ActividadesViewModel());
            Locator.CurrentMutable.Register(() => new UsuarioViewModel());
            Locator.CurrentMutable.Register(() => new DocenteViewModel());
            Locator.CurrentMutable.Register(() => new EstudantesViewModel());
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
