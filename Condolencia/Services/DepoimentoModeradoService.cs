using Condolencia.Data;
using Condolencia.DTOs;
using Condolencia.Interfaces;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Services
{
    public class DepoimentoModeradoService : IDepoimentoModeradoService
    {
        private readonly CondolenciaContext _context;

        public DepoimentoModeradoService(CondolenciaContext context)
        {
            _context = context;
        }

        public async Task<DepoimentoModerado> SalvarDepoimentoModeracao(DepoimentoModeradoViewModel depoimentoModeradoViewModel)
        {
            try
            {
                DepoimentoModerado depoimentoModerado = new DepoimentoModerado();
                depoimentoModerado.IdDepoimento = depoimentoModeradoViewModel.IdDepoimento;
                depoimentoModerado.IdAlteradoPor = depoimentoModeradoViewModel.IdAlteradoPor;
                depoimentoModerado.Status = depoimentoModeradoViewModel.Status;
                depoimentoModerado.DataAcao = DateTime.Now;

                //_context.Add(mensagemModerada);
                //_context.SaveChanges();

                return await Task.FromResult(depoimentoModerado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
