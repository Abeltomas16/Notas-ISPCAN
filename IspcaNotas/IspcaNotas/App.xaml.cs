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
            MainPage = new NavigationPage(new AppShell());
        }

        private void Dependences()
        {
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
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
