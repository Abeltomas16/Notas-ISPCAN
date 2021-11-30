using IspcaNotas.Features.Interface;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using IspcaNotas.Model;
using XF.Material.Forms.UI.Dialogs;

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
            Task.Run(async () => await Carregar());
        }

        public ObservableCollection<ActividadeDTO> Actividades { get; set; }
        public async Task<string> Cadastrar(ActividadeDTO actividade)
        {
            Busy = true;
            var retorno = await actividades.Cadastrar(actividade);
            Busy = false;
            return retorno;
        }

        public async Task<string> Editar(ActividadeDTO actividade)
        {
            Busy = true;
            var retorno = await actividades.Alterar(actividade, actividade.IDActividade);
            Busy = false;
            return retorno;
        }

        public async Task Carregar()
        {
            Busy = true;
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Caregando");
            var _actividades = await this.actividades.listarTodos();
            load.Dismiss();


            Actividades = new ObservableCollection<ActividadeDTO>(_actividades);
            Console.WriteLine(Actividades.Count.ToString());
            OnPropertyChanged("Actividades");
            Busy = false;
        }

        public async Task<string> Apagar(string key)
        {
            Busy = true;
            var resultado = await actividades.Apagar(key);
            Busy = false;
            return resultado;
        }
    }
}
