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
    public class MensagemModeradaService : IMensagemModeradaService
    {
        private readonly CondolenciaContext _context;

        public MensagemModeradaService(CondolenciaContext context)
        {
            _context = context;
        }

        public async Task<int> SalvarMensagemModeracao(MensagemModeradaViewModel mensagemModeradaViewModel)
        {
            try
            {
                MensagemModerada mensagemModerada = new MensagemModerada();
                mensagemModerada.IdMensagem = mensagemModeradaViewModel.IdMensagem;
                mensagemModerada.IdAlteradoPor = mensagemModeradaViewModel.IdAlteradoPor;
                mensagemModerada.Status = mensagemModeradaViewModel.Status;
                mensagemModerada.DataAcao = DateTime.Now;

                _context.Add(mensagemModerada);
                _context.SaveChanges();

                return await Task.FromResult(mensagemModerada.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
