using IspcaNotas.Features.Enums;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.ViewModel
{
    public class CadeiraViewModel : BaseViewModel
    {
        private bool IsBusyCadeira;
        //Total de cadeiras existentes
        public int Total { get; private set; }
        public bool Busy
        {
            get { return IsBusyCadeira; }
            set
            {
                if (IsBusyCadeira != value)
                {
                    IsBusyCadeira = value;
                    OnPropertyChanged("Busy");
                }
            }
        }
        ICadeira cadeiraService = Locator.Current.GetService<ICadeira>();
        public CadeiraViewModel(string Tela)
        {

            Busy = false;
            if (Tela == "CadeirasMostrar")
                Task.Run(async () => await CarregarCadeiraLivres());
            else  //QUANDO A TELA NÃO FOR CADEIRA MOSTRAR, LOGO É A TELA DE INSERÇÃO DAS CADEIRAS
                Task.Run(async () => await Carregar());
        }
        public CadeiraViewModel(string Tela, List<CadeiraDTO> cadeiras)
        {
            //Depois de pegar todas cadeiras, seleciono todas em que um pof leciona
            selectedCadeira = new ObservableCollection<object>();
            Busy = false;
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
            Busy = true;
            var retorno = await cadeiraService.inserirOuAlterar(cadeira, operacao);
            Busy = false;
            return retorno;
        }
        public async Task Carregar()
        {

                Busy = true;
                var cadeiras = await cadeiraService.listarTodos();
                this.Cadeiras = new ObservableCollection<CadeiraDTO>(cadeiras);
                OnPropertyChanged("Cadeiras");
                Total = this.Cadeiras.Count;
                OnPropertyChanged("Total");
                Busy = false;


        }
        public async Task<string> Apagar(string id)
        {
            Busy = true;
            var resultado = await cadeiraService.apagar(id);
            Busy = false;
            return resultado;
        }
        public async Task CarregarCadeiraLivres()
        {
            Busy = true;
            var cadeiras = await cadeiraService.CadeiraLivre();
            this.Cadeiras = new ObservableCollection<CadeiraDTO>(cadeiras);
            OnPropertyChanged("Cadeiras");
            Total = this.Cadeiras.Count;
            OnPropertyChanged("Total");
            Busy = false;
        }
    }

}