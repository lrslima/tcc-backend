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

        public async Task<int> CadastrarVitima(VitimaViewModel vitimaViewModel)
        {
            try
            {
                // CPF caso informado será validado
                if (!String.IsNullOrEmpty(vitimaViewModel.cpf.Trim()))
                {
                    if (!Validacao.ValidaCPF.IsCpf(vitimaViewModel.cpf.Trim()))
                    {
                        throw new Exception("CPF da vítima informado é inválido");
                    }
                }

                // RG caso informado será validado
                if (!String.IsNullOrEmpty(vitimaViewModel.rg.Trim()))
                {
                    if (!Validacao.ValidaRG.IsRg(vitimaViewModel.rg.Trim()))
                    {
                        throw new Exception("RG da vítima informado é inválido");
                    }
                }

                Vitima vitima = new Vitima();
                vitima.Nome = vitimaViewModel.nome;
                vitima.SobreNome = vitimaViewModel.sobrenome;
                vitima.CPF = vitimaViewModel.rg;
                vitima.Rua = vitimaViewModel.endereco_rua;
                vitima.Cidade = vitimaViewModel.endereco_cidade;
                vitima.Estado = vitimaViewModel.endereco_estado;
                vitima.Fotografia = vitimaViewModel.imagem;

                _context.Add(vitima);
                _context.SaveChanges();

               // var myEntity = _context.Pessoa.Find(vitima);

                return await Task.FromResult(vitima.Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
