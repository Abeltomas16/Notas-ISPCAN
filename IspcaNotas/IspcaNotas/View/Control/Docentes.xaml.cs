﻿using IspcaNotas.Features.Collections;
using IspcaNotas.Features.Enums;
using IspcaNotas.Model;
using IspcaNotas.ViewModel;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Docentes : ContentPage
    {
        EnumAdmCRUD EnumDocente;
        UsuarioDTO docenteCurrent = null;
        DocenteViewModel DocentesViewModel { get; } = Locator.Current.GetService<DocenteViewModel>();
        List<CadeiraDTO> cadeirasDoDocente = null;
        public Docentes()
        {
            InitializeComponent();
            try
            {
                EnumDocente = EnumAdmCRUD.Cadastrar;
                BindingContext = DocentesViewModel;
            }
            catch (Exception erro)
            {
                DisplayAlert("Info", erro.Message, "Ok");
            }
        }

        private async void btSalvarEditar_Clicked(object sender, EventArgs e)
        {
            string resultado = string.Empty;
            if (CadeirasDocente.Cadeiras.Count <= 0)
                return;

            UsuarioDTO docente = new UsuarioDTO()
            {
                Name = txtNome.Text,
                Telefone = txtPhone.Text,
                Senha = txtSenha.Text,
                Email = txtEmail.Text,
                Categoria = "Professor"
            };
            List<CadeiraDTO> cadeiras = CadeirasDocente.Cadeiras;
            try
            {
                EnumOperacoes operacao = EnumOperacoes.Cadastrar;

                if (EnumDocente == EnumAdmCRUD.Cadastrar)
                    operacao = EnumOperacoes.Cadastrar;
                else
                {
                    if (docenteCurrent == null)
                        return;
                    docente.Key = docenteCurrent.Key;
                    docente.Token = docenteCurrent.Token;
                    operacao = EnumOperacoes.Editar;
                }
                if (operacao == EnumOperacoes.Cadastrar)
                    resultado = await DocentesViewModel.Cadastrar(docente, cadeiras);
                else if (operacao == EnumOperacoes.Editar)
                    resultado = await DocentesViewModel.Alterar(docente, cadeiras);

                DocentesViewModel.Carregar();
                await DisplayActionSheet("Resultados", null, "Ok");
            }
            catch (Exception erro)
            {
                if (DocentesViewModel.Busy)
                    DocentesViewModel.Busy = false;
                await DisplayAlert("Info", erro.Message, "Ok");
            }
            finally
            {
                btCancelarEditar_Clicked(null, EventArgs.Empty);
                if (DocentesViewModel.Busy)
                    DocentesViewModel.Busy = false;
            }
        }

        private void btCancelarEditar_Clicked(object sender, EventArgs e)
        {
            btCancelarEditar.IsVisible = false;
            EnumDocente = EnumAdmCRUD.Cadastrar;
            btSalvarEditar.Text = "Salvar";
            AddCadeiras.Text = "Adicionar cadeiras";
            LimparCampo();
        }
        private void LimparCampo()
        {
            txtNome.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtEmail.Text = string.Empty;

            viewcadeirasResumo.IsVisible = false;
            CadeirasDocente.Cadeiras = new List<CadeiraDTO>();
        }

        private async void ViewDocentes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = await DisplayActionSheet("Acção", "Cancelar", null, new string[] { "Editar", "Apagar" });
            if (result == null || result == "Cancelar") return;
            docenteCurrent = e.CurrentSelection[0] as UsuarioDTO;
            if (result.Equals("Editar"))
            {
                cadeirasDoDocente = await DocentesViewModel.MostrarCadeira(docenteCurrent.Token);
                txtNome.Text = docenteCurrent.Name;
                txtPhone.Text = docenteCurrent.Telefone;
                txtSenha.Text = docenteCurrent.Senha;
                txtEmail.Text = docenteCurrent.Email;

                btCancelarEditar.IsVisible = true;
                EnumDocente = EnumAdmCRUD.Editar;
                btSalvarEditar.Text = "Actualizar";
                AddCadeiras.Text = "Ver cadeiras";
            }
            else if (result.Equals("Apagar"))
            {
                /*try
                {
                    var questao = await DisplayAlert("Notas", "Tens certeza que pretendes Apagar o docente selecionado?", "Sim", "Cancelar");
                    if (!questao)
                        return;

                    var resultado = await DocentesViewModel.Apagar(docenteCurrent.IDDocenteUsuario.IDUsuario.Value);
                    DocentesViewModel.Carregar();
                    await DisplayAlert("Info", resultado, "Ok");
                }
                catch (Exception erro)
                {
                    if (DocentesViewModel.Busy)
                        DocentesViewModel.Busy = false;
                    await DisplayAlert("Info", erro.Message, "Ok");
                }
                finally
                {
                    if (DocentesViewModel.Busy)
                        DocentesViewModel.Busy = false;
                }*/
                Console.WriteLine("apagar");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (CadeirasDocente.Cadeiras.Count > 0)
            {
                viewcadeirasResumo.IsVisible = true;
                viewcadeirasResumo.ItemsSource = CadeirasDocente.Cadeiras;
            }
        }
        private async void AddCadeiras_Clicked(object sender, EventArgs e)
        {
            if (EnumDocente == EnumAdmCRUD.Cadastrar)
                await Navigation.PushAsync(new CadeirasMostrar());
            else
            {
                if (cadeirasDoDocente == null)
                    return;

                await Navigation.PushAsync(new CadeirasMostrar(cadeirasDoDocente));
            }
        }
    }
}