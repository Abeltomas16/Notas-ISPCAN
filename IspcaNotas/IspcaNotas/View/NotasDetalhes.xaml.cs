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

namespace IspcaNotas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotasDetalhes : ContentPage
    {
        NotasViewModel _notasViewModel = Locator.Current.GetService<NotasViewModel>();
        public NotasDetalhes(UsuarioDTO estudante)
        {
            InitializeComponent();

            BindingContext = _notasViewModel;
            NomeEstudante.Text = estudante.Name;
            NomeCadeira.Text = Application.Current.Properties["Nomecadeira"].ToString();
            NomeProf.Text = Application.Current.Properties["NomeUsuario"].ToString();
        }

    }
}