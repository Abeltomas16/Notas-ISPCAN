using IspcaNotas.Features.Interface;
using Splat;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IspcaNotas.Model;
using XF.Material.Forms.UI.Dialogs;
using IspcaNotas.Commom.Validation;
using IspcaNotas.Commom.Resources;

namespace IspcaNotas.ViewModel
{
    public class ActividadesViewModel : BaseViewModel
    {
        IActividades actividades;
        ActividadesValidator Validations;
        public ActividadesViewModel(IActividades actividades = null)
        {
            this.actividades = actividades ?? Locator.Current.GetService<IActividades>();
            Validations = Locator.Current.GetService<ActividadesValidator>();
            Task.Run(async () => await Carregar());
        }

        public ObservableCollection<ActividadeDTO> Actividades { get; set; }
        public async Task<string> Cadastrar(ActividadeDTO actividade)
        {
            var results = Validations.Validate(actividade);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    if (item.ErrorCode == statusCode.DescricaoIsNotNullOrEmpty ||
                        item.ErrorCode == statusCode.NaoPodeSerApenasNumero)
                        return "Descrição inválida";
                }
            }

            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Salvando");
            var retorno = await actividades.Cadastrar(actividade);
            load.Dismiss();
            return retorno;
        }
        public async Task<string> Editar(ActividadeDTO actividade)
        {
            var results = Validations.Validate(actividade);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    if (item.ErrorCode == statusCode.DescricaoIsNotNullOrEmpty ||
                      item.ErrorCode == statusCode.NaoPodeSerApenasNumero)
                        return "Descrição inválida";

                    if (item.ErrorCode == statusCode.IdDeveSerInformado)
                        return "Id deve ser informado";
                }
            }
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
            if (string.IsNullOrEmpty(key))
                return "Id deve ser informado";
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Apagando");
            var resultado = await actividades.Apagar(key);
            load.Dismiss();
            return resultado;
        }
    }
}
