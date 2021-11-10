using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            XF.Material.Forms.Material.Init(this);
            MainPage = new NavigationPage(new View.LoginView());
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
