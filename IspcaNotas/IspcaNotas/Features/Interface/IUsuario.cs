using IspcaNotas.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface IUsuario
    {
        Task<string> Cadastrar(UsuarioDTO entidade);
        Task<string> Alterar(UsuarioDTO entidade, string key);
        Task<string> Apagar(UsuarioDTO usuarioDTO);
        Task<List<UsuarioDTO>> ListarTodos();
        Task<UsuarioDTO> Pesquisar(string token);
        Task<UsuarioDTO> PesquisarPorKey(string keyUsuario);
        Task<string> AlterarEmail(string newEmail);
        Task<string> AlterarSenha(string newSenha);
    }
}
