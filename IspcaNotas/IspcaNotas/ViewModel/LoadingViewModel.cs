using System;
using System.Collections.Generic;
using System.Text;

namespace IspcaNotas.ViewModel
{
    public class LoadingViewModel
    {
        public void Init()
        {
            var isAuthenticated = false;
            if (isAuthenticated)
            {
                Console.WriteLine("errado");
            }
            else
            {
                Console.WriteLine("certo");
            }
        }
    }
}
