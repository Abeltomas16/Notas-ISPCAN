using Firebase.Database;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Service.usuario
{
    public class NotasService : INotas
    {
        FirebaseClient dbCliente { get; } = Locator.Current.GetService<FirebaseClient>();
        public Task<string> Alterar(NotasDTO entidade)
        {
            throw new NotImplementedException();

        }

        public Task<string> Cadastrar(NotasDTO actividade)
        {
            throw new NotImplementedException();
        }

        public Task<List<NotasDTO>> listarPorCadeira(string keycadeira)
        {
            throw new NotImplementedException();
        }
    }
}
