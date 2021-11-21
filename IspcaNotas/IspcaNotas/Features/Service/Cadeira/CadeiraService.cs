using Firebase.Database;
using Firebase.Database.Query;
using IspcaNotas.Features.Enums;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Service.Cadeira
{
    public class CadeiraService : ICadeira
    {
        FirebaseClient db { get; } = Locator.Current.GetService<FirebaseClient>();

        public async Task<string> inserirOuAlterar(CadeiraDTO cadeira, EnumOperacoes operacoes)
        {
            if (operacoes == EnumOperacoes.Cadastrar)
            {
                var insert = await db.Child("cadeira")
                              .PostAsync(cadeira);
                string key = insert.Key;
                return key;
            }

            await db.Child("cadeira")
                            .Child(cadeira.IDCadeira)
                            .PutAsync(cadeira);
            return "Ok";
        }
        public async Task<string> apagar(string key)
        {
            await db.Child("cadeira")
                         .Child(key)
                         .DeleteAsync();

            return "Ok";
        }

        public async Task<List<CadeiraDTO>> listarTodos()
        {
            var cadeiras = (await db.Child("cadeira")
                                .OnceAsync<CadeiraDTO>()
                         ).Select(x => new CadeiraDTO
                         {
                             IDCadeira = x.Key,
                             name = x.Object.name
                         });

            return cadeiras.ToList();
        }
    }
}
