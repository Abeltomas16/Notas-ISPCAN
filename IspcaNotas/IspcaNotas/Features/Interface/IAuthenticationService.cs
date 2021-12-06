using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface IAuthenticationService
    {
       Task<bool> IsLogged();
    }
}
