﻿using Firebase.Database;
using Firebase.Database.Query;
using IspcaNotas.Features.Enums;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                return "Cadeira cadastrada com sucesso !";
            }

            await db.Child("cadeira")
                            .Child(cadeira.IDCadeira)
                            .PutAsync(cadeira);
            return "Cadeira alterada com sucesso !";
        }
        public async Task<string> apagar(string key)
        {
            await db.Child("cadeira")
                         .Child(key)
                         .DeleteAsync();

            return "Cadeira apagada com sucesso !";
        }

        public async Task<List<CadeiraDTO>> listarTodos()
        {
            var cadeiras = (await db.Child("cadeira")
                                .OnceAsync<CadeiraDTO>()
                         ).Select(x => new CadeiraDTO
                         {
                             IDCadeira = x.Key,
                             Name = x.Object.Name
                         });

            return cadeiras.ToList();
        }
        public async Task DocenteCadeira(CadeiraDTO cadeira, string tokenDocente)
        {
            cadeira.Docente = tokenDocente;
            await db.Child("cadeira")
                            .Child(cadeira.IDCadeira)
                          .PutAsync(cadeira);
        }

        public async Task<List<CadeiraDTO>> CadeiraLivre()
        {
            var cadeiras = (await db.Child("cadeira")
                     .OnceAsync<CadeiraDTO>()
              ).Select(x => new CadeiraDTO
              {
                  IDCadeira = x.Key,
                  Name = x.Object.Name,
                  Docente = x.Object.Docente
              }).Where(y => y.Docente == null);

            return cadeiras.ToList();
        }

        public async Task<List<CadeiraDTO>> MostrarPorID(string token)
        {
            var cadeiras = (await db.Child("cadeira")
                                    .OnceAsync<CadeiraDTO>()
                                         ).Select(x => new CadeiraDTO
                                         {
                                             IDCadeira = x.Key,
                                             Name = x.Object.Name,
                                             Docente = x.Object.Docente
                                         }).Where(y => y.Docente == token);
            return cadeiras.ToList();
        }

        public async Task<string> apagarCadeiraProf(string token)
        {
            List<Task> tarefas = new List<Task>();
            var cadeirasProd = await this.MostrarPorID(token);
            foreach (CadeiraDTO item in cadeirasProd)
                tarefas.Add(this.removerDocente(item));
            await Task.WhenAll(tarefas);
            return "Cadeiras removidas";
        }

        public async Task removerDocente(CadeiraDTO key)
        {
            key.Docente = null;
            await db.Child("cadeira")
                     .Child(key.IDCadeira)
                     .PutAsync(key);
        }
    }
}
