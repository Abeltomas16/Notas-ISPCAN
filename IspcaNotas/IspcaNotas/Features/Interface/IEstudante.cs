﻿using IspcaNotas.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IspcaNotas.Features.Interface
{
    public  interface IEstudante
    {
        Task<List<NotasCadeirasDocente>> ListarNotas(string Keyaluno);
    }
}
