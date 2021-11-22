using IspcaNotas.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface IDocente
    {
        Task<string> Cadastrar(UsuarioDTO entidade,List<CadeiraDTO> cadeiras);
        Task<string> Alterar(UsuarioDTO entidade, List<CadeiraDTO> cadeiras, string key);
        Task<string> Apagar(UsuarioDTO usuarioDTO);
        Task<List<UsuarioDTO>> ListarTodos();
        Task<UsuarioDTO> Pesquisar(string token);
    }
}
