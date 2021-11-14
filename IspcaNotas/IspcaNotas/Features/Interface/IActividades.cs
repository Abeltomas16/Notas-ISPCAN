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
        Task<string> Criar(ActividadeDTO actividade);
    }
}
