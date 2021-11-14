using Firebase.Auth;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Service.usuario
{
    public class UsuarioService : IUsuario
    {
        FirebaseClient user { get; } = Locator.Current.GetService<FirebaseClient>();
        public async Task<string> criar(UsuarioDTO entidade)
        {
            var key = await user.Child("usuario")
                      .PostAsync(entidade);

            return key.Key;
        }
    }
}

