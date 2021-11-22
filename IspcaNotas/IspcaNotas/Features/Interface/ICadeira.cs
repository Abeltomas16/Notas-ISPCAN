using IspcaNotas.Features.Enums;
using IspcaNotas.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface ICadeira
    {
        Task<string> inserirOuAlterar(CadeiraDTO cadeira, EnumOperacoes operacoes);
        Task<string> apagar(string key);
        Task<List<CadeiraDTO>> listarTodos();
        Task<List<CadeiraDTO>> CadeiraLivre();
        Task DocenteCadeira(CadeiraDTO cadeira, string tokenDocente);
    }
}
