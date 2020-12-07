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
    public class VitimaService : IVitimaService
    {
        private readonly CondolenciaContext _context;

        public VitimaService(CondolenciaContext context)
        {
            _context = context;
        }

        public async Task<VitimaViewModel> CadastrarVitima(VitimaViewModel vitimaViewModel)
        {
            try
            {
                // CPF caso informado será validado
                if (!String.IsNullOrEmpty(vitimaViewModel.cpf.Trim()))
                {
                    if (!Validacao.ValidaCPF.IsCpf(vitimaViewModel.cpf.Trim()))
                    {
                        vitimaViewModel.codigoErro = 1;
                        vitimaViewModel.mensagemErro = "CPF da vítima informado é inválido";
                        return await Task.FromResult(vitimaViewModel);
                        //throw new Exception("CPF da vítima informado é inválido");
                    }
                }

                // RG caso informado será validado
                //if (!String.IsNullOrEmpty(vitimaViewModel.rg.Trim()))
                //{
                //    if (!Validacao.ValidaRG.IsRg(vitimaViewModel.rg.Trim()))
                //    {
                //        throw new Exception("RG da vítima informado é inválido");
                //    }
                //}

                Vitima vitima = new Vitima();
                vitima.Nome = vitimaViewModel.nome;
                vitima.SobreNome = vitimaViewModel.sobrenome;
                vitima.CPF = vitimaViewModel.cpf.Trim();
                vitima.RG = vitimaViewModel.rg.Trim();
                vitima.Rua = vitimaViewModel.endereco_rua;
                vitima.Cidade = vitimaViewModel.endereco_cidade;
                vitima.Estado = vitimaViewModel.endereco_estado;
                vitima.Fotografia = vitimaViewModel.imagem;

                return await Task.FromResult(vitimaViewModel);
            }
            catch (Exception ex)
            {
                return await Task.FromException<VitimaViewModel>(ex);
            }
        }
    }
}
