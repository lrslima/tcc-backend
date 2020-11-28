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
                // CPF caso informado será validado
                if (!String.IsNullOrEmpty(pessoaViewModel.cpf.Trim()))
                {
                    if (!Validacao.ValidaCPF.IsCpf(pessoaViewModel.cpf.Trim()))
                    {
                        throw new Exception("CPF do homenageante informado é inválido");
                    }
                }

                // RG caso informado será validado
                if (!String.IsNullOrEmpty(pessoaViewModel.rg.Trim()))
                {
                    if (!Validacao.ValidaRG.IsRg(pessoaViewModel.rg.Trim()))
                    {
                        throw new Exception("RG do homenageante informado é inválido");
                    }
                }

                // E-mail caso informado será validado
                if (!String.IsNullOrEmpty(pessoaViewModel.email.Trim()))
                {
                    if (!Validacao.ValidaEmail.IsEmail(pessoaViewModel.email.Trim()))
                    {
                        throw new Exception("E-mail informado é inválido");
                    }
                }

                Pessoa pessoa = new Pessoa();
                pessoa.Nome = pessoaViewModel.nome;
                pessoa.SobreNome = pessoaViewModel.sobrenome;
                pessoa.CPF = pessoaViewModel.cpf.Trim();
                pessoa.RG = pessoaViewModel.rg.Trim();
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
