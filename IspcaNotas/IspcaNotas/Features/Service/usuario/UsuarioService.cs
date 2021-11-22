﻿using Firebase.Auth;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace IspcaNotas.Features.Service.usuario
{
    public class UsuarioService : IUsuario
    {
        FirebaseClient dbCliente { get; } = Locator.Current.GetService<FirebaseClient>();
        ILogin dbLogin { get; } = Locator.Current.GetService<ILogin>();
        public async Task<string> Cadastrar(UsuarioDTO entidade)
        {
            string token = await dbLogin.SignUp(entidade.Email, entidade.Senha, entidade.Name);
            entidade.Token = token;
            var key = await dbCliente.Child("usuario")
                      .PostAsync(entidade);
            //return key.Key;
            return token;
        }
        public async Task<string> Alterar(UsuarioDTO entidade, string chave)
        {
            entidade.Key = null;
            await dbCliente.Child("usuario")
                       .Child(chave)
                        .PutAsync(entidade);
            //return key.Key;
            return "Usúario Alterado com sucesso";
        }
        public async Task<List<UsuarioDTO>> ListarTodos()
        {
            var dados = (await dbCliente.Child("usuario")
                        .OnceAsync<UsuarioDTO>())
                        .Select(x => new UsuarioDTO
                        {
                            Key = x.Key,
                            Name = x.Object.Name,
                            Categoria = x.Object.Categoria,
                            Email = x.Object.Email,
                            Senha = x.Object.Senha,
                            Telefone = x.Object.Telefone,
                            Token = x.Object.Token
                        });
            return dados.ToList();
        }
        public async Task<string> Apagar(UsuarioDTO usuarioDTO)
        {
            await dbLogin.DeleteAccount(usuarioDTO.Email, usuarioDTO.Senha);
            await dbCliente.Child("usuario")
                           .Child(usuarioDTO.Key)
                           .DeleteAsync();

            return "Usúario apagado com sucesso !";
        }

        public async Task<UsuarioDTO> Pesquisar(string token)
        {
            var dados = (await dbCliente.Child("usuario")
                        .OnceAsync<UsuarioDTO>())
                        .Where(y => y.Object.Token == token).FirstOrDefault();


            return dados.Object;
        }
    }
}

