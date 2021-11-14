using IspcaNotas.Features.Enums;
using IspcaNotas.Features.Service.Actividades;
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
    public partial class Actividades : ContentPage
    {
        private EnumAdmCRUD EnumActividades;
        ActividadeDTO actividadeCurrent = null;
        ActividadesViewModel Service { get; } = Locator.Current.GetService<ActividadesViewModel>();
        public Actividades()
        {
            InitializeComponent();
            try
            {
                EnumActividades = EnumAdmCRUD.Cadastrar;
                BindingContext = Service;
            }
            catch (Exception erro)
            {
                DisplayAlert("Info", erro.Message, "Ok");
            }
        }
        private async void ViewActividades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = await DisplayActionSheet("Acção", "Cancelar", null, new string[] { "Editar", "Apagar" });
            if (result == null || result == "Cancelar") return;
            actividadeCurrent = e.CurrentSelection[0] as ActividadeDTO;
            if (result.Equals("Editar"))
            {
                labelDescricao.Text = actividadeCurrent.Descricao;
                labelIMG.Text = actividadeCurrent.IMGURL;
                btCancelarEditar.IsVisible = true;

                EnumActividades = EnumAdmCRUD.Editar;
                btSalvarEditar.Text = "Actualizar";
            }
            else if (result.Equals("Apagar"))
            {
                try
                {
                    var questao = await DisplayAlert("Notas", "Tens certeza que pretendes Apagar a actividade selecionada?", "Sim", "Cancelar");
                    if (!questao)
                        return;

                 ///???????????   var resultado = await Service.Apagar(actividadeCurrent.IDActividade.Value);
                    Service.Carregar();
                   /// await DisplayAlert("Info", resultado, "Ok");
                }
                catch (Exception erro)
                {
                    if (Service.Busy)
                        Service.Busy = false;
                    await DisplayAlert("Info", erro.Message, "Ok");
                }
                finally
                {
                    if (Service.Busy)
                        Service.Busy = false;
                }
            }
        }
        private void MaterialButtonCancelar_Clicked(object sender, EventArgs e)
        {
            btCancelarEditar.IsVisible = false;
            EnumActividades = EnumAdmCRUD.Cadastrar;
            btSalvarEditar.Text = "Salvar";
            LimparCampo();
        }
        private void LimparCampo()
        {
            labelDescricao.Text = string.Empty;
            labelIMG.Text = string.Empty;
            labelDescricao.Placeholder = "Descrição";
            labelIMG.Placeholder = "URL da imagem";
        }
        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
           ActividadeDTO actividades = new ActividadeDTO()
            {
                Descricao = labelDescricao.Text,
                IMGURL = labelIMG.Text
            };
            try
            {
                EnumOperacoes operacao = EnumOperacoes.Cadastrar;
                string resultado = string.Empty;

                if (EnumActividades == EnumAdmCRUD.Cadastrar)
                    operacao = EnumOperacoes.Cadastrar;
                else
                {
                    if (actividadeCurrent == null)
                        return;
                    actividades.IDActividade = actividadeCurrent.IDActividade;
                    operacao = EnumOperacoes.Editar;
                }
              //??????????????????  resultado = await Service.CadastrarEditar(actividades, operacao);

                Service.Carregar();
                await DisplayAlert("Info", resultado, "Ok");
            }
            catch (Exception erro)
            {
                if (Service.Busy)
                    Service.Busy = false;
                await DisplayAlert("Info", erro.Message, "Ok");
            }
            finally
            {
                MaterialButtonCancelar_Clicked(null, EventArgs.Empty);
                if (Service.Busy)
                    Service.Busy = false;
            }
        }
    }
}
