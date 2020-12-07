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

        public async Task<PessoaViewModel> CadastrarPessoa(PessoaViewModel pessoaViewModel)
        {
            try
            {
                if ((String.IsNullOrEmpty(pessoaViewModel.cpf.Trim())) && (String.IsNullOrEmpty(pessoaViewModel.rg.Trim())))
                {
                    pessoaViewModel.codigoErro = 1;
                    pessoaViewModel.mensagemErro = "CPF ou RG do homenageante é obrigatório";
                    return await Task.FromResult(pessoaViewModel);
                    //throw new Exception("CPF ou RG do homenageante é obrigatório");
                }

                // CPF caso informado será validado
                if (!String.IsNullOrEmpty(pessoaViewModel.cpf.Trim()))
                {
                    if (!Validacao.ValidaCPF.IsCpf(pessoaViewModel.cpf.Trim()))
                    {
                        pessoaViewModel.codigoErro = 2;
                        pessoaViewModel.mensagemErro = "CPF do homenageante informado é inválido";
                        return await Task.FromResult(pessoaViewModel);
                        //throw new Exception("CPF do homenageante informado é inválido");
                    }
                }

                // RG caso informado será validado
                //if (!String.IsNullOrEmpty(pessoaViewModel.rg.Trim()))
                //{
                //    if (!Validacao.ValidaRG.IsRg(pessoaViewModel.rg.Trim()))
                //    {
                //        pessoaViewModel.codigoErro = 3;
                //        pessoaViewModel.mensagemErro = "RG do homenageante informado é inválido";
                //        return await Task.FromResult(pessoaViewModel);
                //        //throw new Exception("RG do homenageante informado é inválido");
                //    }
                //}

                // E-mail caso informado será validado
                if (!String.IsNullOrEmpty(pessoaViewModel.email.Trim()))
                {
                    if (!Validacao.ValidaEmail.IsEmail(pessoaViewModel.email.Trim()))
                    {
                        pessoaViewModel.codigoErro = 4;
                        pessoaViewModel.mensagemErro = "E-mail informado é inválido";
                        return await Task.FromResult(pessoaViewModel);
                        //throw new Exception("E-mail informado é inválido");
                    }
                }

                return await Task.FromResult(pessoaViewModel);
            }
            catch (Exception ex)
            {
                return await Task.FromException<PessoaViewModel>(ex);
            }
        }
    }
}
