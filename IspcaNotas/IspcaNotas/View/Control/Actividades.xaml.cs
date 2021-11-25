using IspcaNotas.Features.Enums;
using IspcaNotas.Model;
using IspcaNotas.ViewModel;
using Splat;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

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
                MaterialDialog.Instance.SnackbarAsync(message: erro.Message, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
               new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
               {
                   BackgroundColor = Color.Orange,
                   MessageTextColor = Color.Black
               });
            }
        }
        private async void ViewActividades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = await DisplayActionSheet("Acção", "Cancelar", null, new string[] { "Editar", "Apagar" });
            if (result == null || result == "Cancelar") return;
            actividadeCurrent = e.CurrentSelection[0] as ActividadeDTO;
            try
            {
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
                    var questao = await DisplayAlert("Notas", "Tens certeza que pretendes Apagar a actividade selecionada?", "Sim", "Cancelar");
                    if (!questao)
                        return;

                    var resultado = await Service.Apagar(actividadeCurrent.IDActividade);
                    await Service.Carregar();
                    await MaterialDialog.Instance.SnackbarAsync(message: resultado, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                       new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                       {
                           BackgroundColor = Color.Orange,
                           MessageTextColor = Color.Black
                       });
                }
            }
            catch (Exception erro)
            {
                if (Service.Busy)
                    Service.Busy = false;
                await MaterialDialog.Instance.SnackbarAsync(message: erro.Message, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                {
                    BackgroundColor = Color.Orange,
                    MessageTextColor = Color.Black
                });
            }
            finally
            {
                if (Service.Busy)
                    Service.Busy = false;
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
                IMGURL = labelIMG.Text,
                DataCadastro = DateTime.Now
            };
            try
            {
                string resultado = string.Empty;

                if (EnumActividades == EnumAdmCRUD.Cadastrar)
                    resultado = await Service.Cadastrar(actividades);
                else
                {
                    if (actividadeCurrent == null)
                        return;

                    actividades.IDActividade = actividadeCurrent.IDActividade;
                    resultado = await Service.Editar(actividades);
                }
                await Service.Carregar();
                await MaterialDialog.Instance.SnackbarAsync(message: resultado, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                {
                    BackgroundColor = Color.Orange,
                    MessageTextColor = Color.Black
                });
            }
            catch (Exception erro)
            {
                if (Service.Busy)
                    Service.Busy = false;
                await MaterialDialog.Instance.SnackbarAsync(message: erro.Message, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                 new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                 {
                     BackgroundColor = Color.Orange,
                     MessageTextColor = Color.Black
                 });
            }
            finally
            {
                MaterialButtonCancelar_Clicked(null, EventArgs.Empty);
                if (Service.Busy)
                    Service.Busy = false;
            }
        }

        private async void MenuSobre_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Sobre());
        }
    }
}
