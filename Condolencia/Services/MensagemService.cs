﻿using Condolencia.Services;
using Microsoft.AspNetCore.Mvc;
using Condolencia.Data;
using Condolencia.DTOs;
using Condolencia.Interfaces;
using Condolencia.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Services
{
    public class MensagemService : IMensagemService
    {
        private readonly CondolenciaContext _condolenciaContext;
        private readonly IPessoaService _pessoaService;
        private readonly IVitimaService _vitimaService;
        private readonly IEmailService _emailService;
        private readonly IMensagemModeradaService _mensagemModeradaService;

        public MensagemService(CondolenciaContext condolenciaContext, IPessoaService pessoaService, IVitimaService vitimaService, IEmailService emailService)
        {
            _condolenciaContext = condolenciaContext;
            _pessoaService = pessoaService;
            _vitimaService = vitimaService;
            _emailService = emailService;
        }

        public async Task<Mensagem> RegistrarMensagem(MensagemRegistrar mensagemViewModel)
        {
            try
            {

                var idPessoa = await _pessoaService.CadastrarPessoa(mensagemViewModel.Pessoa);
                var idVitima = await _vitimaService.CadastrarVitima(mensagemViewModel.Vitima);

                Mensagem mensagem = new Mensagem();
                mensagem.IdPessoa = idPessoa;
                mensagem.IdVitima = idVitima;
                mensagem.Texto = mensagemViewModel.texto;
                mensagem.Status = "Pendente";
                mensagem.Sentimento = mensagemViewModel.Pessoa.sentimento;
                mensagem.Privacidade = mensagemViewModel.privacidade;
                mensagem.PoliticaPrivacidade = mensagemViewModel.politica_privacidade;
                mensagem.DataCriacao = DateTime.Now;
                mensagem.QrCode = null;

                _condolenciaContext.Add(mensagem);
                _condolenciaContext.SaveChanges();

                return await Task.FromResult(mensagem);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async void AlterarStatus(MensagemModeradaViewModel mensagemModeradaViewModel)
        {
            try
            {
                string assunto = "Mensagem reprovada pelo moderador";
                // incluir moderção na tabela de moderação ------ var idModeracao = await _ModeracaoMensagemService.AlterarStatus(statusViewModel.Status);
                var Moderacao = await _mensagemModeradaService.CadastrarModeracao(mensagemModeradaViewModel);

                // Verificar se Aprovado gerar o QR code
                string stringBase64 = string.Empty;
                Byte[] imagem = null;
                if (mensagemModeradaViewModel.Status.Trim().Equals("Aprovado", StringComparison.OrdinalIgnoreCase))
                {
                    assunto = "Mensagem aprovada pelo moderador";
                    imagem = QRCodeService.GenerateByteArray($"https://avarc.vercel.app");
                    stringBase64 = Convert.ToBase64String(imagem);
                }
                
                // alterar status e incluir o QR code na tabela mensagem 
                var mensagem = await _condolenciaContext.Mensagem.FindAsync(mensagemModeradaViewModel.IdMensagem);
                mensagem.Status = mensagemModeradaViewModel.Status;
                mensagem.QrCode = imagem;

                _condolenciaContext.Add(mensagem);
                _condolenciaContext.SaveChanges();

                //return await Task.FromResult(mensagem);

                // Formatar o corpo do e-mail ??? aqui ou no método de envio de e-mail
                // Enviar mensagem
                var pessoa = await _condolenciaContext.Pessoa.FindAsync(mensagem.IdPessoa);
                await _emailService.SendEmailAsync(pessoa.Email, assunto, "teste");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///exemplo
        //método de alterar status
        //public async void AlterarStatus(MensagemRegistrar mensagemViewModel)
        //{

        //    //chamada do metodo de email
        //    await _emailService.SendEmailAsync(mensagemViewModel.Pessoa.email, "teste", "teste");
        //}


        public async Task<List<MensagemRegistrar>> GetAllMensagens()
        {
            try
            {
                var listMensagens = (from mensagem in _condolenciaContext.Mensagem
                                     from pessoa in _condolenciaContext.Pessoa
                                     from vitima in _condolenciaContext.Vitima
                                     select new MensagemRegistrar
                                     {
                                         status = mensagem.Status,
                                         texto = mensagem.Texto,
                                         politica_privacidade = mensagem.PoliticaPrivacidade,
                                         privacidade = mensagem.Privacidade,
                                         Pessoa = new PessoaViewModel
                                         {
                                             nome = pessoa.Nome,
                                             sobrenome = pessoa.SobreNome,
                                             cpf = pessoa.CPF,
                                             rg = pessoa.RG,
                                             email = pessoa.Email,
                                             sentimento = mensagem.Sentimento
                                         },
                                         Vitima = new VitimaViewModel
                                         {
                                             nome = vitima.Nome,
                                             sobrenome = vitima.SobreNome,
                                             cpf = vitima.CPF,
                                             rg = vitima.RG,
                                             endereco_rua = vitima.Rua,
                                             endereco_cidade = vitima.Cidade,
                                             endereco_estado = vitima.Estado,
                                             imagem = vitima.Fotografia
                                         }
                                     }).ToList();

                return await Task.FromResult(listMensagens);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MensagemRegistrar>> GetMensagem(int idMensagem)
        {
            try
            {
                var listMensagem = (from mensagem in _condolenciaContext.Mensagem
                                    from pessoa in _condolenciaContext.Pessoa
                                    from vitima in _condolenciaContext.Vitima
                                    select new MensagemRegistrar
                                    {
                                        Id = mensagem.Id,
                                        status = mensagem.Status,
                                        texto = mensagem.Texto,
                                        politica_privacidade = mensagem.PoliticaPrivacidade,
                                        privacidade = mensagem.Privacidade,
                                        Pessoa = new PessoaViewModel
                                        {
                                            nome = pessoa.Nome,
                                            sobrenome = pessoa.SobreNome,
                                            cpf = pessoa.CPF,
                                            rg = pessoa.RG,
                                            email = pessoa.Email,
                                            sentimento = mensagem.Sentimento
                                        },
                                        Vitima = new VitimaViewModel
                                        {
                                            nome = vitima.Nome,
                                            sobrenome = vitima.SobreNome,
                                            cpf = vitima.CPF,
                                            rg = vitima.RG,
                                            endereco_rua = vitima.Rua,
                                            endereco_cidade = vitima.Cidade,
                                            endereco_estado = vitima.Estado,
                                            imagem = vitima.Fotografia
                                        }
                                    }).Where(i => i.Id == idMensagem).ToList();

                return await Task.FromResult(listMensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
