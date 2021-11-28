using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace IspcaNotas.ViewModel
{
    public class LoginViewModel
    {
        private string Senha = string.Empty;
        private string Email = string.Empty;
        public string _email
        {
            set { Email = value; }
        }
        public string _senha
        {
            set { Senha = value; }
        }
        IRouting routing;
        ILogin LoginNegocios;
        IUsuario usuarioService;
        ICadeira cadeiraService;
        public ICommand ExecuteSignIn { get; }
        public LoginViewModel()
        {
            LoginNegocios = Locator.Current.GetService<ILogin>();
            this.routing = Locator.Current.GetService<IRouting>();
            this.usuarioService = Locator.Current.GetService<IUsuario>();
            this.cadeiraService = Locator.Current.GetService<ICadeira>();
            ExecuteSignIn = new Command(() => ValidarEntrada(this.Email, this.Senha));
        }

        internal async void ValidarEntrada(string email, string senha)
        {

            try
            {
                var resultado = await LoginNegocios.SignIn(email, Senha);
                if (resultado != null)
                {
                    UsuarioDTO categoria = await usuarioService.Pesquisar(resultado);
                    Application.Current.Properties["NomeUsuario"] = categoria.Name;
                    if (categoria.Categoria == "Estudante")
                        await routing.NavigateTo("///main");
                    else if (categoria.Categoria == "Administrador")
                        await routing.NavigateTo("///admin");
                    else if (categoria.Categoria == "Professor")
                    {
                        //carregar todas cadeiras que ele leciona
                        var cadeiras = await cadeiraService.MostrarPorID(categoria.Token);
                        if (cadeiras.Count <= 0)
                            throw new Exception("O Srº Professor Ainda não leciona uma disciplina, por essa razão não vai poder efectuar o login, Obrigado pela comprensão!");
                        else if (cadeiras.Count == 1)
                        {
                            Application.Current.Properties["IDCadeira"] = cadeiras.FirstOrDefault().IDCadeira;
                            Application.Current.Properties["Nomecadeira"] = cadeiras.FirstOrDefault().Name;
                        }
                        else
                        {
                            //var cadeira= cadeiras.RemoveAll(x => x.Name != resultadoAction);
                            var resultadoAction= await XF.Material.Forms.UI.Dialogs.MaterialDialog.Instance.SelectActionAsync(title: "Seleciona uma Cadeira para continuares",
                                                                                                          actions: cadeiras.Select(x => x.Name).ToArray());
                        }
                        await routing.NavigateTo("///professor");
                    }
                    // 
                }


            }
            catch (Firebase.Auth.FirebaseAuthException erro)
            {
                Console.WriteLine(erro.ResponseData);
                await XF.Material.Forms.UI.Dialogs.MaterialDialog.Instance.SnackbarAsync(erro.Message, XF.Material.Forms.UI.Dialogs.MaterialSnackbar.DurationShort);
            }
        }

    }
}
