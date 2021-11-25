using Firebase.Database;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Service.usuario
{
    public class DocenteService : IDocente
    {
        FirebaseClient dbCliente { get; } = Locator.Current.GetService<FirebaseClient>();
        IUsuario dbLogin { get; } = Locator.Current.GetService<IUsuario>();
        ICadeira dbCadeira { get; } = Locator.Current.GetService<ICadeira>();
        public async Task<string> Cadastrar(UsuarioDTO entidade, List<CadeiraDTO> cadeiras)
        {
            List<Task> tarefas = new List<Task>();
            string retorno = await dbLogin.Cadastrar(entidade);
            foreach (CadeiraDTO item in cadeiras)
                tarefas.Add(dbCadeira.DocenteCadeira(item, retorno));
            await Task.WhenAll(tarefas);
            return "Docente cadastrado com sucesso";
        }
        public async Task<string> Alterar(UsuarioDTO entidade, List<CadeiraDTO> cadeiras)
        {
            await dbLogin.Alterar(entidade, entidade.Key);
            await dbCadeira.apagarCadeiraProf(entidade.Token);
            List<Task> tarefas = new List<Task>();

            foreach (CadeiraDTO item in cadeiras)
                tarefas.Add(dbCadeira.DocenteCadeira(item, entidade.Token));
            await Task.WhenAll(tarefas);
            return "Docente alterado com sucesso";
        }
        public async Task<List<UsuarioDTO>> ListarTodos()
        {
            var dados = (await dbLogin.ListarTodos()).Where(y => y.Categoria == "Professor");
            return dados.ToList();
        }
        public async Task<string> Apagar(UsuarioDTO usuarioDTO)
        {
            string retorno = await dbLogin.Apagar(usuarioDTO);
            return retorno;
        }

        public async Task<UsuarioDTO> Pesquisar(string token)
        {
            var dados = await dbLogin.Pesquisar(token);
            return dados;
        }
    }
}

