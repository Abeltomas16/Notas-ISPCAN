using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace IspcaNotas.ViewModel
{
    public class NotasViewModel : BaseViewModel
    {
        INotas notasService;
        public NotasViewModel(string estudantekey, INotas notas = null)
        {
            notasService = notas ?? Locator.Current.GetService<INotas>();
            Cadastrar = new Command(() => Salvar());
            cadeiraKey = Application.Current.Properties["IDCadeira"].ToString();
            EstudanteKey = estudantekey;
            Task.Run(async () => await CarregaNotas(estudantekey));
        }

        private async Task CarregaNotas(string estudantekey)
        {
            NotasDTO nota = await notasService.listarPorCadeira(cadeiraKey, estudantekey);
            IDNota = nota.Key ?? null;
            _Nota1 = nota.Nota1 ?? null;
            _Nota2 = nota.Nota2 ?? null;
            OnPropertyChanged("_Nota1");
            OnPropertyChanged("_Nota2");
        }

        private string EstudanteKey { get; set; }
        private string cadeiraKey { get; set; }
        public string _Nota1 { get; set; }
        public string _Nota2 { get; set; }
        private string IDNota { get; set; }
        public ICommand Cadastrar { get; set; }

        private async void Salvar()
        {
            string retorno = string.Empty;
            NotasDTO notas = new NotasDTO
            {
                Nota1 = _Nota1,
                Nota2 = _Nota2,
                KeyCadeira = cadeiraKey,
                KeyAluno = EstudanteKey
            };

            if (string.IsNullOrEmpty(IDNota))
            {
                retorno = await notasService.Cadastrar(notas);
            }
            else
            {
                notas.Key = IDNota;
                retorno = await notasService.Alterar(notas);
            }

            await MaterialDialog.Instance.SnackbarAsync(message: retorno, actionButtonText: "Ok", msDuration: MaterialSnackbar.DurationLong,
                  new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration
                  {
                      BackgroundColor = Color.Orange,
                      MessageTextColor = Color.Black
                  });
        }
    }
}
