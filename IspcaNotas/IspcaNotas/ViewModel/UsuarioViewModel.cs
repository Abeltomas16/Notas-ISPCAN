using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace IspcaNotas.ViewModel
{
    public class UsuarioViewModel : BaseViewModel
    {
        IUsuario Idata;
        public UsuarioViewModel(IUsuario usuario = null)
        {
            Idata = usuario ?? Locator.Current.GetService<IUsuario>();
            Carregar();
        }
        public int Total { get; private set; }

        public ObservableCollection<UsuarioDTO> Estudantes { get; set; }
        public async void Carregar()
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Carregando");
            var estudantes = await Idata.ListarTodos();
            Estudantes = new ObservableCollection<UsuarioDTO>(estudantes.Where(y => y.Categoria == "Estudante"));
            OnPropertyChanged("Estudantes");
            Total = Estudantes.Count;
            OnPropertyChanged("Total");
            load.Dismiss();
        }
        public async Task<string> Cadastrar(UsuarioDTO usuario)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Salvando");
            var retorno = await Idata.Cadastrar(usuario);
            load.Dismiss();
            return retorno;
        }
        public async Task<string> Alterar(UsuarioDTO usuario)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Actualizando");
            var retorno = await Idata.Alterar(usuario, usuario.Key);
            load.Dismiss();
            return retorno;
        }
        public async Task<string> Apagar(UsuarioDTO usuarioDTO)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Apagando");
            var resultado = await Idata.Apagar(usuarioDTO);
            load.Dismiss();
            return resultado;
        }
        public async Task<string> UpdateEmail(string newEmail)
        {
            string retorno = await Idata.AlterarEmail(newEmail);
            return retorno;
        }
        public async Task<string> UpdateSenha(string newSenha)
        {
            string retorno = await Idata.AlterarSenha(newSenha);
            return retorno;
        }
    }
}
