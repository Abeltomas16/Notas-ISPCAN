﻿using IspcaNotas.Features.Enums;
using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using IspcaNotas.ViewModel;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace IspcaNotas.View.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Estudantes : ContentPage
    {
        EnumAdmCRUD EnumEstudantes;
        UsuarioDTO estudanteCurrent = null;
        UsuarioViewModel estudantesViewModel = Locator.Current.GetService<UsuarioViewModel>();
        public Estudantes()
        {
            InitializeComponent();
            EnumEstudantes = EnumAdmCRUD.Cadastrar;
            try
            {
                BindingContext = estudantesViewModel;
            }
            catch (Exception erro)
            {
                MaterialDialog.Instance.SnackbarAsync(message: erro.Message, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                   new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                   {
                       BackgroundColor = Color.Orange,
                       MessageTextColor = Color.Black
                   });
            }
        }

        private async void ViewEstudante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var result = await DisplayActionSheet("Acção", "Cancelar", null, new string[] { "Editar", "Apagar" });
            if (result == null || result == "Cancelar") return;
            estudanteCurrent = e.CurrentSelection[0] as UsuarioDTO;
            try
            {
                if (result.Equals("Editar"))
                {
                    txtNome.Text = estudanteCurrent.Name;
                    txtPhone.Text = estudanteCurrent.Telefone;
                    txtSenha.Text = estudanteCurrent.Senha;
                    txtEmail.Text = estudanteCurrent.Email;

                    btCancelar.IsVisible = true;
                    EnumEstudantes = EnumAdmCRUD.Editar;
                    btSalvar.Text = "Actualizar";
                    txtEmail.IsEnabled = false;
                    txtSenha.IsEnabled = false;
                }
                else if (result.Equals("Apagar"))
                {
                    var questao = await DisplayAlert("Notas", "Tens certeza que pretendes Apagar o estudante selecionado?", "Sim", "Cancelar");
                    if (!questao)
                        return;

                    var resultado = await estudantesViewModel.Apagar(estudanteCurrent);
                    estudantesViewModel.Carregar();
                    await MaterialDialog.Instance.SnackbarAsync(message: resultado, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                            new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                            {
                                BackgroundColor = Color.Orange,
                                MessageTextColor = Color.Black
                            });
                }
            }
            catch (Exception erro)
            {
                if (estudantesViewModel.Busy)
                    estudantesViewModel.Busy = false;

                await MaterialDialog.Instance.SnackbarAsync(message: erro.Message, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                    new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                    {
                        BackgroundColor = Color.Orange,
                        MessageTextColor = Color.Black
                    });
            }
            finally
            {
                if (estudantesViewModel.Busy)
                    estudantesViewModel.Busy = false;
            }
        }
        private void LimparCampo()
        {
            txtNome.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }


        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            btCancelar.IsVisible = false;
            EnumEstudantes = EnumAdmCRUD.Cadastrar;
            btSalvar.Text = "Salvar";
            txtEmail.IsEnabled = true;
            txtSenha.IsEnabled = true;
            LimparCampo();
        }

        private async void btSalvar_Clicked(object sender, EventArgs e)
        {
            UsuarioDTO usuario = new UsuarioDTO()
            {
                Name = txtNome.Text,
                Telefone = txtPhone.Text,
                Senha = txtSenha.Text,
                Email = txtEmail.Text,
                Categoria = "Estudante"
            };

            try
            {
                EnumOperacoes operacao = EnumOperacoes.Cadastrar;
                string resultado = string.Empty;

                if (EnumEstudantes == EnumAdmCRUD.Cadastrar)
                    operacao = EnumOperacoes.Cadastrar;
                else
                {
                    if (estudanteCurrent == null)
                        return;
                    usuario.Token = estudanteCurrent.Token;
                    usuario.Key = estudanteCurrent.Key;
                    operacao = EnumOperacoes.Editar;
                }

                if (operacao == EnumOperacoes.Cadastrar)
                    resultado = await estudantesViewModel.Cadastrar(usuario);
                else
                    resultado = await estudantesViewModel.Alterar(usuario);


                estudantesViewModel.Carregar();

                await MaterialDialog.Instance.SnackbarAsync(message: resultado, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                    new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                    {
                        BackgroundColor = Color.Orange,
                        MessageTextColor = Color.Black
                    });
            }
            catch (Exception erro)
            {
                if (estudantesViewModel.Busy)
                    estudantesViewModel.Busy = false;

                await MaterialDialog.Instance.SnackbarAsync(message: erro.Message, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                    new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                    {
                        BackgroundColor = Color.Orange,
                        MessageTextColor = Color.Black
                    });
            }
            finally
            {
                btCancelar_Clicked(null, EventArgs.Empty);

                if (estudantesViewModel.Busy)
                    estudantesViewModel.Busy = false;
            }
        }
    }
}