using IspcaNotas.Commom.Resources;
using IspcaNotas.Commom.Validation;
using IspcaNotas.Features.Enums;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace IspcaNotas.ViewModel
{
    public class CadeiraViewModel : BaseViewModel
    {
        //Total de cadeiras existentes
        public int Total { get; private set; }
        ICadeira cadeiraService = Locator.Current.GetService<ICadeira>();
        CadeiraValidator Validations;
        public CadeiraViewModel(string Tela)
        {
            Validations = Locator.Current.GetService<CadeiraValidator>();
            if (Tela == "CadeirasMostrar")
                Task.Run(async () => await CarregarCadeiraLivres());
            else  //QUANDO A TELA NÃO FOR CADEIRA MOSTRAR, LOGO É A TELA DE INSERÇÃO DAS CADEIRAS
                Task.Run(async () => await Carregar());
        }
        public CadeiraViewModel(string Tela, List<CadeiraDTO> cadeiras)
        {
            Validations = Locator.Current.GetService<CadeiraValidator>();
            //Depois de pegar todas cadeiras, seleciono todas em que um pof leciona
            selectedCadeira = new ObservableCollection<object>();
            Task.Run(async () => await Carregar())
                .ContinueWith(
                tarefaAnterior =>
                {
                    for (int i = 0; i < Cadeiras.Count; i++)
                    {
                        for (int j = 0; j < cadeiras.Count; j++)
                        {
                            if (Cadeiras[i].IDCadeira == cadeiras[j].IDCadeira)
                                SelectedCadeira.Add(Cadeiras[i]);
                        }
                    }
                },
                TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        ObservableCollection<object> selectedCadeira;
        public ObservableCollection<object> SelectedCadeira
        {
            get
            {
                return selectedCadeira;
            }
            set
            {
                if (selectedCadeira != value)
                {
                    selectedCadeira = value;
                    OnPropertyChanged("selectedCadeira");
                }
            }
        }
        public ObservableCollection<CadeiraDTO> Cadeiras { get; set; }
        public async Task<string> CadastrarEditar(CadeiraDTO cadeira, EnumOperacoes operacao)
        {
            var results = Validations.Validate(cadeira);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    if (item.ErrorCode == statusCode.DescricaoCaractereMinimo11 ||
                        item.ErrorCode == statusCode.DescricaoIsNotNullOrEmpty ||
                        item.ErrorCode == statusCode.DescricaoNotCaracteresEspecial)
                        return "Descrição inválida";
                    if (operacao == EnumOperacoes.Editar)
                        if (item.ErrorCode == statusCode.IdDeveSerInformado)
                            return "Id da cadeira deve ser informado";
                }
            }
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Salvando");
            var retorno = await cadeiraService.inserirOuAlterar(cadeira, operacao);
            load.Dismiss();
            return retorno;
        }
        public async Task Carregar()
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Carregando");
            var cadeiras = await cadeiraService.listarTodos();
            this.Cadeiras = new ObservableCollection<CadeiraDTO>(cadeiras);
            OnPropertyChanged("Cadeiras");
            Total = this.Cadeiras.Count;
            OnPropertyChanged("Total");
            load.Dismiss();
        }
        public async Task<string> Apagar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Id invalido";

            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Apagando");
            var resultado = await cadeiraService.apagar(id);
            load.Dismiss();
            return resultado;
        }
        public async Task CarregarCadeiraLivres()
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Carregando");
            var cadeiras = await cadeiraService.CadeiraLivre();
            this.Cadeiras = new ObservableCollection<CadeiraDTO>(cadeiras);
            OnPropertyChanged("Cadeiras");
            Total = this.Cadeiras.Count;
            OnPropertyChanged("Total");
            load.Dismiss();
        }
    }
}