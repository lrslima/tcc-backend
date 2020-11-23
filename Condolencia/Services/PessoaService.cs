using Condolencia.Data;
using Condolencia.DTOs;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Services
{
    public class PessoaService
    {
        private readonly CondolenciaContext _context;

        public PessoaService(CondolenciaContext context)
        {
            _context = context;
        }

        public Task<bool> SalvaPessoa(PublicarMensagem publicarMensagem)
        {

            Pessoa pessoa = new Pessoa();
            pessoa = publicarMensagem.Pessoa;

            _context.Add(pessoa);
            _context.SaveChanges();
  
            return Task.FromResult(true);

        }
    }
}
