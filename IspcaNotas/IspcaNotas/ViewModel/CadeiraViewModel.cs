using IspcaNotas.Features.Enums;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace IspcaNotas.ViewModel
{
    public class CadeiraViewModel : BaseViewModel
    {
        //Total de cadeiras existentes
        public int Total { get; private set; }
        ICadeira cadeiraService = Locator.Current.GetService<ICadeira>();
        public CadeiraViewModel(string Tela)
        {
            if (Tela == "CadeirasMostrar")
                Task.Run(async () => await CarregarCadeiraLivres());
            else  //QUANDO A TELA NÃO FOR CADEIRA MOSTRAR, LOGO É A TELA DE INSERÇÃO DAS CADEIRAS
                Task.Run(async () => await Carregar());
        }
        public CadeiraViewModel(string Tela, List<CadeiraDTO> cadeiras)
        {
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