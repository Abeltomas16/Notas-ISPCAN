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
using XF.Material.Forms.UI.Dialogs;

namespace IspcaNotas.View.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocenteNota : ContentPage
    {
        UsuarioDTO estudanteCurrent = null;
        UsuarioViewModel estudantesViewModel = Locator.Current.GetService<UsuarioViewModel>();
        public DocenteNota()
        {
            InitializeComponent();
            try
            {
                BindingContext = estudantesViewModel;
                txtNome.Text = Application.Current.Properties["NomeUsuario"].ToString();
                txtDisciplina.Text = Application.Current.Properties["Nomecadeira"].ToString();
            }
            catch (Exception erro)
            {
                MaterialDialog.Instance.SnackbarAsync(message: erro.Message, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                   new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                   {
                       BackgroundColor = Color.Orange,
                       MessageTextColor = Color.Black
                   });
            }
        }

        private async void ViewEstudante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UsuarioDTO estudante = (e.CurrentSelection.FirstOrDefault() as UsuarioDTO);

                if (estudante == null) return;
                activityIndicatorAlunos.IsRunning = true;

                Locator.CurrentMutable.Register(() => new NotasViewModel(estudante.Key));

                //  Modelos.Notas notasEstudante = await notasNegocios.MostrarPorCadeiraEEStudante(_DocenteCadeira, estudante);
                await Task.Delay(2000);
                activityIndicatorAlunos.IsRunning = false;
                await Shell.Current.Navigation.PushAsync(new NotasDetalhes(estudante));
            }
            catch (Exception erro)
            {
                await DisplayAlert("Info", erro.Message, "Ok");
            }
        }
    }
}