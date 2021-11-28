using IspcaNotas.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface INotas
    {
        Task<List<NotasDTO>> listarPorCadeira(string keycadeira);
        Task<string> Cadastrar(NotasDTO actividade);
        Task<string> Alterar(NotasDTO entidade);
    }
}
