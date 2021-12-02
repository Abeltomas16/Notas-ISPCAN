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
        public ActividadesViewModel(IActividades actividades = null)
        {
            this.actividades = actividades ?? Locator.Current.GetService<IActividades>();
            Task.Run(async () => await Carregar());
        }

        public ObservableCollection<ActividadeDTO> Actividades { get; set; }
        public async Task<string> Cadastrar(ActividadeDTO actividade)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Salvando");
            var retorno = await actividades.Cadastrar(actividade);
            load.Dismiss();
            return retorno;
        }

        public async Task<string> Editar(ActividadeDTO actividade)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Editando");
            var retorno = await actividades.Alterar(actividade, actividade.IDActividade);
            load.Dismiss();
            return retorno;
        }

        public async Task Carregar()
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Caregando");
            var _actividades = await this.actividades.listarTodos();
            load.Dismiss();

            Actividades = new ObservableCollection<ActividadeDTO>(_actividades);
            Console.WriteLine(Actividades.Count.ToString());
            OnPropertyChanged("Actividades");
        }

        public async Task<string> Apagar(string key)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Apagando");
            var resultado = await actividades.Apagar(key);
            load.Dismiss();
            return resultado;
        }
    }
}
