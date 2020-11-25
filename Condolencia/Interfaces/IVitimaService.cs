using Condolencia.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Interfaces
{
    public interface IVitimaService
    {
        Task<int> CadastrarVitima(VitimaViewModel vitimaViewModel);
    }
}
