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
    public class DocenteViewModel : BaseViewModel
    {
        private CadeiraDTO CadeiraSelecionada = null;
        public int Total { get; private set; }
        public CadeiraDTO RetornaCadeiraSelecionada
        {
            get { return CadeiraSelecionada; }
            set
            {
                if (CadeiraSelecionada != value)
                {
                    CadeiraSelecionada = value;
                }
            }
        }
        ICadeira clienteCadeira;
        IDocente clienteDocente;
        IUsuario clienteUsuario;
        public DocenteViewModel(IDocente docente = null, IUsuario usuario = null, ICadeira cadeira = null)
        {
            clienteDocente = docente ?? Locator.Current.GetService<IDocente>();
            clienteCadeira = cadeira ?? Locator.Current.GetService<ICadeira>();
            clienteUsuario = usuario ?? Locator.Current.GetService<IUsuario>();
            Task.Run(async () => await Carregar());
        }
        public ObservableCollection<UsuarioDTO> Docentes { get; set; }
        public ObservableCollection<CadeiraDTO> Cadeiras { get; set; }
        public async Task<string> Cadastrar(UsuarioDTO docente, List<CadeiraDTO> cadeiras)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Salvando");
            var retorno = await clienteDocente.Cadastrar(docente, cadeiras);
            load.Dismiss();
            return retorno;
        }
        public async Task<string> Alterar(UsuarioDTO docente, List<CadeiraDTO> cadeiras)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Actualizando");
            var retorno = await clienteDocente.Alterar(docente, cadeiras);
            load.Dismiss();
            return retorno;
        }
        public async Task Carregar()
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Carregando");
            var docentes = await clienteDocente.ListarTodos();
            Docentes = new ObservableCollection<UsuarioDTO>(docentes);
            OnPropertyChanged("Docentes");
            Total = Docentes.Count;
            OnPropertyChanged("Total");

            var cadeira = await clienteCadeira.listarTodos();
            Cadeiras = new ObservableCollection<CadeiraDTO>(cadeira);
            OnPropertyChanged("Cadeiras");

            load.Dismiss();
        }

        public async Task<UsuarioDTO> MelhorAluno(string keyMelhorAluno)
        {
            var usuario = await clienteUsuario.PesquisarPorKey(keyMelhorAluno);
            return usuario;
        }

        public async Task<List<CadeiraDTO>> MostrarCadeira(string key)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Carregando");
            var cadeiras = await clienteCadeira.MostrarPorID(key);
            load.Dismiss();
            return cadeiras;
        }
        public async Task<string> Apagar(UsuarioDTO key)
        {
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Apagando");
            var resultado = await clienteDocente.Apagar(key);
            load.Dismiss();
            return resultado;
        }
        public async Task<List<NotasDTO>> mostrarNotas(string keycadeira)
        {
            var resultado = await clienteDocente.MostrarNotas(keycadeira);
            return resultado;
        }
        public async Task<List<NotasDTO>> mostrarNotas()
        {
            var resultado = await clienteDocente.MostrarNotas();
            return resultado;
        }
    }
}
