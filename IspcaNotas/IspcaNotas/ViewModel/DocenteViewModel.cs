using IspcaNotas.Commom.Resources;
using IspcaNotas.Commom.Validation;
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
        Usuariovalidator Validations;
        CadeiraValidator ValidationsCadeira;
        public DocenteViewModel(IDocente docente = null, IUsuario usuario = null, ICadeira cadeira = null)
        {
            clienteDocente = docente ?? Locator.Current.GetService<IDocente>();
            clienteCadeira = cadeira ?? Locator.Current.GetService<ICadeira>();
            clienteUsuario = usuario ?? Locator.Current.GetService<IUsuario>();
            ValidationsCadeira = Locator.Current.GetService<CadeiraValidator>();
            Validations = Locator.Current.GetService<Usuariovalidator>();
            Task.Run(async () => await Carregar());
        }
        public ObservableCollection<UsuarioDTO> Docentes { get; set; }
        public ObservableCollection<CadeiraDTO> Cadeiras { get; set; }
        public async Task<string> Cadastrar(UsuarioDTO docente, List<CadeiraDTO> cadeiras)
        {
            string retornos = ValidarInsert(docente, cadeiras);
            if (retornos != null) return retornos;
            var load = await MaterialDialog.Instance.LoadingDialogAsync(message: "Salvando");
            var retorno = await clienteDocente.Cadastrar(docente, cadeiras);
            load.Dismiss();
            return retorno;
        }
        public async Task<string> Alterar(UsuarioDTO docente, List<CadeiraDTO> cadeiras)
        {
            string retornos = ValidarUpdate(docente, cadeiras);
            if (retornos != null) return retornos;
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
            string retorno = ValidarDelete(key);
            if (retorno != null) return retorno;
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
        public string ValidarInsert(UsuarioDTO docente, List<CadeiraDTO> cadeiras)
        {
            string retorno = null;
            var results = Validations.Validate(docente);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    if (item.ErrorCode == statusCode.NomeIsNullOrEmpty ||
                        item.ErrorCode == statusCode.NomeNotCaractereEspecial)
                        retorno = "Nome inválido";
                    if (item.ErrorCode == statusCode.TelefoneIsNotNullOrEmpty)
                        retorno = "Telefone inválido";
                    if (item.ErrorCode == statusCode.TelefoneCaractereMinimo)
                        retorno = "Telefone requer 9 digítos";
                    if (item.ErrorCode == statusCode.EmailInvalid)
                        retorno = "Email inválido";
                    if (item.ErrorCode == statusCode.CategoriaInvalido)
                        retorno = "categoria inválida";
                    if (item.ErrorCode == statusCode.SenhaIsNullOrEmpty)
                        retorno = "Informe a senha";
                    if (item.ErrorCode == statusCode.SenhaCaractereMinimo)
                        retorno = "A senha tem que 4 caracteres no minímo";
                }
            }
            if (cadeiras.Count > 0)
            {
                foreach (var item in cadeiras)
                {
                    var results2 = ValidationsCadeira.Validate(item);
                    if (!results2.IsValid)
                    {
                        foreach (var item2 in results2.Errors)
                        {
                            if (item2.ErrorCode == statusCode.IdDeveSerInformado)
                                retorno = "Informe o id";
                        }
                    }
                }
            }
            return retorno;
        }
        public string ValidarUpdate(UsuarioDTO docente, List<CadeiraDTO> cadeiras)
        {
            string retorno = null;
            var results = Validations.Validate(docente);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    if (item.ErrorCode == statusCode.NomeIsNullOrEmpty ||
                        item.ErrorCode == statusCode.NomeNotCaractereEspecial)
                        retorno = "Nome inválido";
                    if (item.ErrorCode == statusCode.TelefoneIsNotNullOrEmpty)
                        retorno = "Telefone inválido";
                    if (item.ErrorCode == statusCode.TelefoneCaractereMinimo)
                        retorno = "Telefone requer 9 digítos";
                    if (item.ErrorCode == statusCode.EmailInvalid)
                        retorno = "Email inválido";
                    if (item.ErrorCode == statusCode.CategoriaInvalido)
                        retorno = "categoria inválida";
                    if (item.ErrorCode == statusCode.SenhaIsNullOrEmpty)
                        retorno = "Informe a senha";
                    if (item.ErrorCode == statusCode.SenhaCaractereMinimo)
                        retorno = "A senha tem que 4 caracteres no minímo";
                    if (item.ErrorCode == statusCode.IdDeveSerInformado)
                        retorno = "Informe o id";
                    if (item.ErrorCode == statusCode.IdDeveSerInformado)
                        retorno = "Informe o token";
                }
            }
            if (cadeiras.Count > 0)
            {
                foreach (var item in cadeiras)
                {
                    var results2 = ValidationsCadeira.Validate(item);
                    if (!results2.IsValid)
                    {
                        foreach (var item2 in results2.Errors)
                        {
                            if (item2.ErrorCode == statusCode.IdDeveSerInformado)
                                retorno = "Informe o id";
                        }
                    }
                }
            }
            return retorno;
        }
        public string ValidarDelete(UsuarioDTO key)
        {
            string retorno = null;
            var results = Validations.Validate(key);
            if (!results.IsValid)
            {
                foreach (var item in results.Errors)
                {
                    if (item.ErrorCode == statusCode.EmailInvalid)
                        retorno = "Email inválido";
                    if (item.ErrorCode == statusCode.CategoriaInvalido)
                        retorno = "categoria inválida";
                    if (item.ErrorCode == statusCode.SenhaIsNullOrEmpty)
                        retorno = "Informe a senha";
                    if (item.ErrorCode == statusCode.SenhaCaractereMinimo)
                        retorno = "A senha tem que 4 caracteres no minímo";
                    if (item.ErrorCode == statusCode.IdDeveSerInformado)
                        retorno = "Informe o id";
                    if (item.ErrorCode == statusCode.IdDeveSerInformado)
                        retorno = "Informe o token";
                }
            }
            return retorno;
        }
    }
}
