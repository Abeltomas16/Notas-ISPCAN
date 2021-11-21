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
    public class UsuarioViewModel : BaseViewModel
    {
        IUsuario Idata;
        public UsuarioViewModel(IUsuario usuario = null)
        {
            Idata = usuario ?? Locator.Current.GetService<IUsuario>();
            Busy = false;
            Carregar();
        }
        private bool IsBusyEstudante;
        public int Total { get; private set; }
        public bool Busy
        {
            get { return IsBusyEstudante; }
            set
            {
                if (IsBusyEstudante != value)
                {
                    IsBusyEstudante = value;
                    OnPropertyChanged("Busy");
                }
            }
        }
        public ObservableCollection<UsuarioDTO> Estudantes { get; set; }
        public async void Carregar()
        {
            Busy = true;
            var estudantes = await Idata.ListarTodos();
            Estudantes = new ObservableCollection<UsuarioDTO>(estudantes);
            OnPropertyChanged("Estudantes");
            Total = Estudantes.Count;
            OnPropertyChanged("Total");
            Busy = false;
        }
        public async Task<string> Cadastrar(UsuarioDTO usuario)
        {
            Busy = true;
            var retorno = await Idata.Cadastrar(usuario);
            Busy = false;
            return retorno;
        }
        public async Task<string> Alterar(UsuarioDTO usuario)
        {
            Busy = true;
            var retorno = await Idata.Alterar(usuario, usuario.Key);
            Busy = false;
            return retorno;
        }
        public async Task<string> Apagar(string token, string key)
        {
            Busy = true;
            var resultado = await Idata.Apagar(token, key);
            Busy = false;
            return resultado;
        }
    }
}
