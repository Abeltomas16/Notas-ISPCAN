using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public interface IRouting
    {
        Task GoBack();
        Task GoBackModal();
        Task NavigateTo(string route);
    }
}
