﻿using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.ViewModel
{
    public class DocenteViewModel : BaseViewModel
    {
        private CadeiraDTO CadeiraSelecionada = null;
        private bool IsBusyEstudante;
        public int Total { get; private set; }
        public bool Busy
        {
            get { return IsBusyEstudante; }
            set
            {
                if (IsBusyEstudante != value)
                {
                    IsBusyEstudante = value;
                    OnPropertyChanged("Busy");
                }
            }
        }
        public CadeiraDTO RetornaCadeiraSelecionada
        {
            get { return CadeiraSelecionada; }
            set
            {
                if (CadeiraSelecionada != value)
                {
                    CadeiraSelecionada = value;
                }
            }
        }
        ICadeira clienteCadeira;
        IDocente clienteDocente;
        public DocenteViewModel(IDocente docente=null, ICadeira cadeira=null)
        {
            clienteDocente = docente ?? Locator.Current.GetService<IDocente>();
            clienteCadeira = cadeira ?? Locator.Current.GetService<ICadeira>();
            Busy = false;
            Carregar();
        }
        public ObservableCollection<UsuarioDTO> Docentes { get; set; }
        public ObservableCollection<CadeiraDTO> Cadeiras { get; set; }
        public async Task<string> Cadastrar(UsuarioDTO docente, List<CadeiraDTO> cadeiras)
        {
            Busy = true;
            var retorno = await clienteDocente.Cadastrar(docente, cadeiras);
            Busy = false;
            return retorno;
        }
      /*  public async Task<List<string>> Editar(UsuarioDTO docente, List<CadeiraDTO> cadeiras, string key)
        {
           Busy = true;
            var retorno = await clienteDocente.Alterar(docente, cadeiras, key);
            Busy = false;
            return retorno;
        }*/
        public async void Carregar()
        {
            Busy = true;
            var docentes = await clienteDocente.ListarTodos();
            Docentes = new ObservableCollection<UsuarioDTO>(docentes);
            OnPropertyChanged("Docentes");
            Total = Docentes.Count;
            OnPropertyChanged("Total");

            var cadeira = await clienteCadeira.listarTodos();
            Cadeiras = new ObservableCollection<CadeiraDTO>(cadeira);
            OnPropertyChanged("Cadeiras");

            Busy = false;
        }
        /*  public async Task<Collection<Cadeira>> MostrarCadeira(int id)
          {
              Busy = true;
              CadeiraNegocios cadeiraNegocios = new CadeiraNegocios();
              var cadeiras = await cadeiraNegocios.MostrarPorID(id);
              Busy = false;
              return cadeiras;
          }*/
        public async Task<string> Apagar(UsuarioDTO key)
        {
            Busy = true;
            var resultado = await clienteDocente.Apagar(key);
            Busy = false;
            return resultado;
        }
    }
}