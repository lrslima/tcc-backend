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
    public class PessoaService : IPessoaService
    {
        private readonly CondolenciaContext _context;

        public PessoaService(CondolenciaContext context)
        {
            _context = context;
        }

        public async Task<int> CadastrarPessoa(PessoaViewModel pessoaViewModel)
        {
            try
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Nome = pessoaViewModel.nome;
                pessoa.SobreNome = pessoaViewModel.sobrenome;
                pessoa.CPF = pessoaViewModel.cpf;
                pessoa.RG = pessoaViewModel.rg;
                pessoa.Email = pessoaViewModel.email;

                _context.Add(pessoa);
                _context.SaveChanges();

                return await Task.FromResult(pessoa.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
