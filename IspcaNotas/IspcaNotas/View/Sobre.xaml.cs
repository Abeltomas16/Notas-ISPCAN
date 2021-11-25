using IspcaNotas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sobre : ContentPage
    {
        public Sobre()
        {
            InitializeComponent();
            lblSobre.Text = "Aplicativo com objectivo de maximizar a publicação das notas permitindo aos " +
                             "estudantes conhecerem os seus resultados a partir de qualquer local.";

            List<UsuarioDTO> usuarios = new List<UsuarioDTO>()
            {
                new UsuarioDTO()
                {
                     Name="ADILSON HEBO"
                },new UsuarioDTO
                {
                    Name="ABILIO JAMBA"
                }, new UsuarioDTO()
                {
                    Name="ANCÉLIO CABINGA"
                }, new UsuarioDTO()
                {
                    Name="ADRIANO JOSÉ"
                }, new UsuarioDTO()
                {
                    Name="JULIÃO FERNANDES"
                }, new UsuarioDTO()
                {
                    Name="MANUEL DE DEUS"
                }, new UsuarioDTO()
                {
                    Name="FERNANDA ANTÓNIO"
                }, new UsuarioDTO()
                {
                    Name="FERNANDES MANUEL"
                }, new UsuarioDTO()
                {
                    Name="GRACIETH PASCOAL"
                },
                new UsuarioDTO
                {
                    Name="ILÍDIO NHANGA"
                },
                new UsuarioDTO
                {
                    Name="EDSON GOLOME"
                },
                new UsuarioDTO
                {
                    Name="MARTINS VUNG"
                },
                new UsuarioDTO
                {
                    Name="DANIEL GONÇALVES"
                },
                new UsuarioDTO
                {
                    Name="EDGAR MARCOS"
                },
                new UsuarioDTO
                {
                    Name="SALVADOR BRAVO"
                },
                new UsuarioDTO
                {
                    Name="NOÉ CAMPOS"
                },
                new UsuarioDTO
                {
                    Name="JOÃO FILIPE"
                },
                new UsuarioDTO
                {
                    Name="GELSON GASPAR"
                }
            };
            usuarios.Sort(delegate (UsuarioDTO u1, UsuarioDTO p2)
            {
                return u1.Name.CompareTo(p2.Name);
            });
            EstudantesCollectionView.ItemsSource = usuarios;
        }
    }
}