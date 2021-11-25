using Firebase.Database;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;

namespace IspcaNotas.Features.Service.Actividades
{
    public class ActividadesService : IActividades
    {
        FirebaseClient user { get; } = Locator.Current.GetService<FirebaseClient>();

        public async Task<string> Alterar(ActividadeDTO entidade, string key)
        {
            entidade.IDActividade = null;
            await user.Child("actividades")
                       .Child(key)
                       .PutAsync(entidade);
            return "Actividade alterada com sucesso";
        }

        public async Task<string> Apagar(string key)
        {
            await user.Child("actividades")
                        .Child(key)
                        .DeleteAsync();
            return "Actividade apagada com sucesso !";
        }

        public async Task<string> Cadastrar(ActividadeDTO actividade)
        {
            var key = await user.Child("actividades")
                         .PostAsync(actividade);
           // return key.Key;
            return "Actividade cadastrada com sucesso !";
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
