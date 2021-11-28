using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface ILogin
    {
        Task<string> SignIn(string email, string password);
        Task<string> SignUp(string email, string password, string displayname);
        Task<string> UpdateEmail(string newEmail);
        Task<string> UpdateSenha(string newSenha);
        Task DeleteAccount(string email, string password);

    }
}
