using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IspcaNotas.ViewModel
{
    public class EstudantesViewModel
    {
        IEstudante estudanteService;
        public EstudantesViewModel(IEstudante estudantes = null)
        {
            estudanteService = estudantes ?? Locator.Current.GetService<IEstudante>();
        }
        public async Task<List<NotasDTO>> listar(string keyEstudante)
        {
            var notas = await estudanteService.ListarNotas(keyEstudante);
            return notas;
        }
    }
}
