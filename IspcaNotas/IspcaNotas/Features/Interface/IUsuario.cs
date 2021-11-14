using IspcaNotas.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface IUsuario
    {
        Task<string> criar(UsuarioDTO entidade);
    }
}
