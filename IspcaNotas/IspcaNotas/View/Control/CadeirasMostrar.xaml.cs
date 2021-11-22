using IspcaNotas.Features.Collections;
using IspcaNotas.Model;
using IspcaNotas.ViewModel;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadeirasMostrar : ContentPage
    {
        CadeiraViewModel ViewModel { get; }
        public CadeirasMostrar()
        {
            InitializeComponent();
            ViewModel = Locator.Current.GetService<CadeiraViewModel>("CadeirasMostrar");
            BindingContext = ViewModel;
        }
        public CadeirasMostrar(List<CadeiraDTO> cadeirasDoDocente)
        {
            InitializeComponent();

            //Locator.CurrentMutable.Register(() => new CadeiraViewModel("CadeirasMostrar", cadeirasDoDocente), "CadeirasListarProf");
           // ViewModel = Locator.Current.GetService<CadeiraViewModel>("CadeirasListarProf");

            BindingContext = new CadeiraViewModel("CadeirasMostrar", cadeirasDoDocente);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private void ViewCadeiras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CadeirasDocente.Cadeiras = new List<CadeiraDTO>();
            foreach (CadeiraDTO item in e.CurrentSelection)
                CadeirasDocente.Cadeiras.Add(item);
        }

        private async void okk_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}