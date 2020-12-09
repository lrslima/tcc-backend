using Condolencia.DTOs;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Interfaces
{
    public interface IDepoimentoService
    {
        Task<DepoimentoRegistrar> RegistrarDepoimento(DepoimentoRegistrar depoimentoViewModel);
        
        Task<DepoimentoRegistrar> AlterarStatus(DepoimentoModeradoViewModel depoimentoModerado);

        Task<List<DepoimentoRegistrar>> GetAllDepoimentos();
        
        Task<DepoimentoRegistrar> GetDepoimento(int idDepoimento);

        Task<List<DepoimentoRegistrar>> GetDepoimentoByStatus(string status);

        Task<List<DepoimentoRegistrar>> GetQrCode();
    }
}
