using IspcaNotas.ViewModel;
using Splat;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        LoadingViewModel ViewModel { get; set; } = Locator.Current.GetService<LoadingViewModel>();
        public LoadingPage()
        {
            InitializeComponent();
            ViewModel.Init();
        }
    }
}