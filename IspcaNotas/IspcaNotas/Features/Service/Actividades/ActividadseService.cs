using Firebase.Database;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using Firebase.Database.Query;

namespace IspcaNotas.Features.Service.Actividades
{
    public class ActividadseService : IActividades
    {
        FirebaseClient user { get; } = Locator.Current.GetService<FirebaseClient>();

        public async Task<string> Criar(ActividadeDTO actividade)
        {
            var key = await user.Child("actividades")
                         .PostAsync(actividade);
            return key.Key;
        }

        public async Task<List<ActividadeDTO>> listarTodos()
        {
            var actividades = await user.Child("actividades")
                                   .OnceAsync<ActividadeDTO>();
            var lista = actividades.Select(item => new ActividadeDTO
            {
                Descricao = item.Object.Descricao,
                DataCadastro = item.Object.DataCadastro,
                IMGURL = item.Object.IMGURL,
                IDActividade = item.Key
            });

            return lista.ToList();
        }
    }
}
