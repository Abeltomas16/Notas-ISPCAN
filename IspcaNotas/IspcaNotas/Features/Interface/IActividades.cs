using IspcaNotas.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface IActividades
    {
        Task<List<ActividadeDTO>> listarTodos();
        Task<string> Cadastrar(ActividadeDTO actividade);
        Task<string> Alterar(ActividadeDTO entidade, string key);
        Task<string> Apagar(string key);

    }
}
