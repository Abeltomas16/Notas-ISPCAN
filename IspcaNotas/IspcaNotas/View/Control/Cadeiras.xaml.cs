using IspcaNotas.Features.Enums;
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
    public partial class Cadeiras : ContentPage
    {
        private EnumAdmCRUD EnumCadeiras;
        CadeiraDTO cadeiraCurrent = null;
        CadeiraViewModel CadeiraViewModel { get; } = Locator.Current.GetService<CadeiraViewModel>();
        public Cadeiras()
        {
            InitializeComponent();

            try
            {
                EnumCadeiras = EnumAdmCRUD.Cadastrar;
                //Passo o tipo de telas do cadastro, porque há dois tipos:CADEIRA MOSTRAR e TELA DE INSERÇÃO DAS CADEIRAS
                BindingContext = CadeiraViewModel;
                txtNome.Text = String.Empty;
            }
            catch (Exception erro)
            {
                DisplayAlert("Info", erro.Message, "Ok");
            }

        }

        private async void ViewCadeiras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = await DisplayActionSheet("Acção", "Cancelar", null, new string[] { "Editar", "Apagar" });
            if (result == null || result == "Cancelar") return;
            cadeiraCurrent = e.CurrentSelection[0] as CadeiraDTO;
            if (result.Equals("Editar"))
            {
                txtNome.Text = cadeiraCurrent.name;
                btCancelarEditar.IsVisible = true;

                EnumCadeiras = EnumAdmCRUD.Editar;
                btSalvarEditar.Text = "Actualizar";
            }
            else if (result.Equals("Apagar"))
            {
                try
                {
                    var questao = await DisplayAlert("Notas", "Tens certeza que pretendes Apagar a cadeira selecionada?", "Sim", "Cancelar");
                    if (!questao)
                        return;

                    var resultado = await CadeiraViewModel.Apagar(cadeiraCurrent.IDCadeira);
                    await CadeiraViewModel.Carregar();
                    await DisplayAlert("Info", resultado, "Ok");
                }
                catch (Exception erro)
                {
                    if (CadeiraViewModel.Busy)
                        CadeiraViewModel.Busy = false;
                    await DisplayAlert("Info", erro.Message, "Ok");
                }
                finally
                {
                    if (CadeiraViewModel.Busy)
                        CadeiraViewModel.Busy = false;
                }
            }
        }

        private void btCancelarEditar_Clicked(object sender, EventArgs e)
        {
            btCancelarEditar.IsVisible = false;
            EnumCadeiras = EnumAdmCRUD.Cadastrar;
            btSalvarEditar.Text = "Salvar";
            LimparCampo();
        }
        private void LimparCampo()
        {
            txtNome.Text = string.Empty;
        }

        private async void btSalvarEditar_Clicked(object sender, EventArgs e)
        {
            CadeiraDTO cadeira = new CadeiraDTO()
            {
                name = txtNome.Text
            };
            try
            {
                EnumOperacoes operacao = EnumOperacoes.Cadastrar;
                string resultado = string.Empty;

                if (EnumCadeiras == EnumAdmCRUD.Cadastrar)
                    operacao = EnumOperacoes.Cadastrar;
                else
                {
                    if (cadeiraCurrent == null)
                        return;
                    cadeira.IDCadeira = cadeiraCurrent.IDCadeira;
                    operacao = EnumOperacoes.Editar;
                }
                resultado = await CadeiraViewModel.CadastrarEditar(cadeira, operacao);

                await CadeiraViewModel.Carregar();
                await DisplayAlert("Info", resultado, "Ok");
            }
            catch (Exception erro)
            {
                if (CadeiraViewModel.Busy)
                    CadeiraViewModel.Busy = false;
                await DisplayAlert("Info", erro.Message, "Ok");
            }
            finally
            {
                btCancelarEditar_Clicked(null, EventArgs.Empty);
                if (CadeiraViewModel.Busy)
                    CadeiraViewModel.Busy = false;
            }
        }
    }
}
