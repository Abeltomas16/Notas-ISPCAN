using IspcaNotas.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface IDocente
    {
        Task<string> Cadastrar(UsuarioDTO entidade,List<CadeiraDTO> cadeiras);
        Task<string> Alterar(UsuarioDTO entidade, List<CadeiraDTO> cadeiras);
        Task<string> Apagar(UsuarioDTO usuarioDTO);
        Task<List<UsuarioDTO>> ListarTodos();
        Task<List<NotasDTO>> MostrarNotas(string keyCadeira);
        Task<UsuarioDTO> Pesquisar(string token);
    }
}
