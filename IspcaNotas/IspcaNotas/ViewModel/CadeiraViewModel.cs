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
        ICadeira cadeiraService;
        public CadeiraViewModel()
        {
            cadeiraService = Locator.Current.GetService<ICadeira>();
            Task.Run(async () => await Carregar());
        }

        //public CadeiraViewModel(string Tela, Collection<CadeiraDTO> cadeiras)
        //{
        //    //Depois de pegar todas cadeiras, seleciono todas em que um pof leciona
        //    selectedCadeira = new ObservableCollection<object>();
        //    Busy = false;
        //    var tarefaBusca = Task.Run(() => Carregar());
        //    tarefaBusca.ContinueWith(
        //       tarefaAnterior =>
        //       {
        //           for (int i = 0; i < Cadeiras.Count; i++)
        //           {
        //               for (int j = 0; j < cadeiras.Count; j++)
        //               {
        //                   if (Cadeiras[i].IDCadeira == cadeiras[j].IDCadeira)
        //                       SelectedCadeira.Add(Cadeiras[i]);
        //               }
        //           }
        //       },
        //       TaskContinuationOptions.OnlyOnRanToCompletion);
        //}
       
        
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
            try
            {
                Busy = true;
                var cadeiras = await cadeiraService.listarTodos();
                this.Cadeiras = new ObservableCollection<CadeiraDTO>(cadeiras);
                OnPropertyChanged("Cadeiras");
                Total = this.Cadeiras.Count;
                OnPropertyChanged("Total");
                Busy = false;
            }
            catch (Exception erro)
            {
                Busy = false;
                Console.WriteLine(erro);
                throw;
            }

        }
        public async Task<string> Apagar(string id)
        {
            Busy = true;
            var resultado = await cadeiraService.apagar(id);
            Busy = false;
            return resultado;
        }
        public void CarregarCadeiraLivres()
        {
            Busy = true;
            var cadeiras = new List<CadeiraDTO>();// await cadeiraService.CadeiraLivre();
            this.Cadeiras = new ObservableCollection<CadeiraDTO>(cadeiras);
            OnPropertyChanged("Cadeiras");
            Total = this.Cadeiras.Count;
            OnPropertyChanged("Total");
            Busy = false;
        }
    }

}