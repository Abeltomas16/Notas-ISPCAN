using IspcaNotas.Features.Interface;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using IspcaNotas.Model;
namespace IspcaNotas.ViewModel
{
    public class ActividadesViewModel : BaseViewModel
    {
        IActividades actividades;
        private bool IsBusyCadeira;
        //Variavél base para o Activiy indicator
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

        public ActividadesViewModel(IActividades actividades = null)
        {
            Busy = false;
            this.actividades = actividades ?? Locator.Current.GetService<IActividades>();
            Carregar();
        }

        public ObservableCollection<ActividadeDTO> Actividades { get; set; }
        /*public async Task<string> CadastrarEditar(ActividadesDTO actividades, EnumOperacoes operacao)
        {
            Busy = true;
            ActividadesNegocios actividadesNegocios = new ActividadesNegocios();
            var retorno = await actividadesNegocios.CadastrarEditar(actividades, operacao);
            Busy = false;
            return retorno;
        }*/

        public async void Carregar()
        {
            Busy = true;
            var _actividades = await this.actividades.listarTodos();
            Actividades = new ObservableCollection<ActividadeDTO>(_actividades);
            Console.WriteLine(Actividades.Count.ToString());
            OnPropertyChanged("Actividades");
            Busy = false;

        }

        /*public async Task<string> Apagar(int id)
        {
            Busy = true;
            ActividadesNegocios actividadesNegocios = new ActividadesNegocios();
            var resultado = await actividadesNegocios.Apagar(id);
            Busy = false;
            return resultado;
        }*/
    }
}
