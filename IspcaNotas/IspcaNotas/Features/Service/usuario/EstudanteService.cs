using IspcaNotas.Features.Interface;
using IspcaNotas.Model;
using Splat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Service.usuario
{
    public class EstudanteService : IEstudante
    {
        INotas notasService;
        public EstudanteService(INotas notas=null)
        {
            notasService = notas ?? Locator.Current.GetService<INotas>();
        }
        public async Task<List<NotasCadeirasDocente>> ListarNotas(string Keyaluno)
        {
            var notas = await notasService.listarPorAluno(Keyaluno);
            return notas;
        }
    }
}
