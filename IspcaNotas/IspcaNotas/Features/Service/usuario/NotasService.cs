using Firebase.Database;
using Firebase.Database.Query;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Service.usuario
{
    public class NotasService : INotas
    {
        FirebaseClient dbCliente { get; } = Locator.Current.GetService<FirebaseClient>();
        public async Task<string> Alterar(NotasDTO notas)
        {
            string key = notas.Key;
            notas.Key = null;
            await dbCliente.Child("notas")
                            .Child(key)
                            .PutAsync(notas);
            return "Nota Alterada";
        }

        public async Task<string> Cadastrar(NotasDTO notas)
        {
            await dbCliente.Child("notas")
                 .PostAsync(notas);
            return "Nota cadastrada";
        }

        public async Task<NotasDTO> listarPorCadeira(string keycadeira, string keyEstudante)
        {
            var Nota = (await dbCliente.Child("notas")
                            .OnceAsync<NotasDTO>())
                            .Select(z => new NotasDTO
                            {
                                Key = z.Key,
                                KeyAluno = z.Object.KeyAluno,
                                KeyCadeira = z.Object.KeyCadeira,
                                Nota1 = z.Object.Nota1,
                                Nota2 = z.Object.Nota2

                            })
                            .Where(x => x.KeyAluno == keyEstudante && x.KeyCadeira == keycadeira)
                            .FirstOrDefault();
            return Nota;
        }
        public async Task<List<NotasDTO>> listarPorAluno(string keyEstudante)
        {
            var Notas = (await dbCliente.Child("notas")
                            .OnceAsync<NotasDTO>())
                            .Select(z => new NotasDTO
                            {
                                Key = z.Key,
                                KeyAluno = z.Object.KeyAluno,
                                KeyCadeira = z.Object.KeyCadeira,
                                Nota1 = z.Object.Nota1,
                                Nota2 = z.Object.Nota2

                            })
                            .Where(x => x.KeyAluno == keyEstudante);
            return Notas.ToList();
        }
    }
}
