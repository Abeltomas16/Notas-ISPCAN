using IspcaNotas.Features.Enums;
using IspcaNotas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface ICadeira
    {
        Task<string> inserirOuAlterar(CadeiraDTO cadeira, EnumOperacoes operacoes);
        Task<string> apagar(string key);
        Task removerDocente(CadeiraDTO key);
        Task<string> apagarCadeiraProf(string token);
        Task<List<CadeiraDTO>> listarTodos();
        Task<List<CadeiraDTO>> CadeiraLivre();
        Task<List<CadeiraDTO>> MostrarPorID(string key);
        Task DocenteCadeira(CadeiraDTO cadeira, string tokenDocente);
    }
}
