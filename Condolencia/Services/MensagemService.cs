using Condolencia.Interfaces;
using Condolencia.Services;
using Microsoft.AspNetCore.Mvc;
using Condolencia.Data;
using Condolencia.DTOs;
using Condolencia.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace Condolencia.Services
{
    public class MensagemService : IMensagemService
    {
        private readonly CondolenciaContext _condolenciaContext;
        private readonly IPessoaService _pessoaService;
        private readonly IVitimaService _vitimaService;
        private readonly IEmailService _emailService;
        private readonly IMensagemModeradaService _mensagemModeradaService;

        public MensagemService(CondolenciaContext condolenciaContext, IPessoaService pessoaService, IVitimaService vitimaService, IEmailService emailService, IMensagemModeradaService mensagemModeradaService)
        {
            _condolenciaContext = condolenciaContext;
            _pessoaService = pessoaService;
            _vitimaService = vitimaService;
            _emailService = emailService;
            _mensagemModeradaService = mensagemModeradaService;
    }

    public async Task<MensagemRegistrar> RegistrarMensagem(MensagemRegistrar mensagemViewModel)
        {
            try
            {
                // Inclusão da Pessoa que está registrando a condolência
                var pessoa = await _pessoaService.CadastrarPessoa(mensagemViewModel.Pessoa);

                // Inclusão da Vítima que está sendo homenageada
                var vitima = await _vitimaService.CadastrarVitima(mensagemViewModel.Vitima);
                
                // Inclusão da mensagem
                Mensagem mensagem = new Mensagem();
                mensagem.Texto = mensagemViewModel.texto;
                mensagem.Status = "Pendente";
                mensagem.Sentimento = mensagemViewModel.Pessoa.sentimento;
                mensagem.Privacidade = mensagemViewModel.privacidade;
                mensagem.PoliticaPrivacidade = mensagemViewModel.politica_privacidade;
                //mensagem.DataCriacao = DateTime.Now;
                mensagem.QrCode = null;

                // Salvar inclusão de Pessoa
                _condolenciaContext.Add(pessoa);
                _condolenciaContext.SaveChanges();
                mensagem.IdPessoa = pessoa.Id;

                // Salvar inclusão de Vítima
                _condolenciaContext.Add(vitima);
                _condolenciaContext.SaveChanges();
                mensagem.IdVitima = vitima.Id;

                _condolenciaContext.Add(mensagem);
                _condolenciaContext.SaveChanges();

                var retorno = await GetMensagem(mensagem.Id);
                return await Task.FromResult(retorno);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<MensagemRegistrar> AlterarStatus(MensagemModeradaViewModel mensagemModeradaViewModel)
        {
            try
            {
                var mensagem = await _condolenciaContext.Mensagem.FindAsync(mensagemModeradaViewModel.IdMensagem);
                var pessoa = await _condolenciaContext.Pessoa.FindAsync(mensagem.IdPessoa);

                string assunto = "Mensagem reprovada pelo moderador";
                
                // incluir moderção na tabela de moderação ------ var idModeracao = await _ModeracaoMensagemService.AlterarStatus(statusViewModel.Status);
                var moderacao = await _mensagemModeradaService.SalvarMensagemModeracao(mensagemModeradaViewModel);
                
                string htmlString ="";

                // Verificar se Aprovado gerar o QR code
                string stringBase64 = string.Empty;
                Bitmap imagemQRcode;
                Byte[] imagemCode = null;
                
                if (mensagemModeradaViewModel.Status.Trim().Equals("Aprovado", StringComparison.OrdinalIgnoreCase))
                {
                    assunto = "Mensagem aprovada pelo moderador";
                    string urlCondolencia = $"https://avarc.vercel.app/condolencia/" + mensagemModeradaViewModel.IdMensagem;
                    imagemCode = QRCodeService.GenerateByteArray(urlCondolencia);
                    imagemQRcode = QRCodeService.GenerateImage(urlCondolencia);
                    stringBase64 = Convert.ToBase64String(imagemCode);
                    htmlString = @"<html>
                      <body>
                      <p>Olá " + pessoa.Nome + " " + pessoa.SobreNome + @"</p>
                      <p>Sua condolência foi aprovada e já está publicada. Você poderá acessar a condolência através deste QR code.</p>
                      <p><br><img src= 'https://www.opememorial.net/api/QRCode/IdCondolencia?idCondolencia=" + mensagemModeradaViewModel.IdMensagem  + @"' class='CToWUd a6T' tabindex='0'/></br></p>
                      <p>Caso não consiga ler o QR code, poderá acessar a condolência clicando <a href='https://avarc.vercel.app/condolencia/" + mensagemModeradaViewModel.IdMensagem + @"'> neste link</a></p>                      
                      </body>
                      </html>
                     ";
                }
                else
                {
                    htmlString = @"<html>
                      <body>
                      <p>Olá " + pessoa.Nome + " " + pessoa.SobreNome + @"</p>
                      <p>Sua condolência foi reprovada e não poderá ser publicada.</p>
                      </body>
                      </html>
                     ";
                }
                
                // alterar status e incluir o QR code na tabela mensagem 
                mensagem.Status = mensagemModeradaViewModel.Status;
                mensagem.QrCode = imagemCode;

                // Gravar moderação
                _condolenciaContext.Add(moderacao);
                _condolenciaContext.SaveChanges();

                // Atualizar dados condolencia
                _condolenciaContext.Update(mensagem);
                _condolenciaContext.SaveChanges();

                // Enviar mensagem
                await _emailService.SendEmailAsync(pessoa.Email, assunto, htmlString);

                var result = await GetMensagem(mensagemModeradaViewModel.IdMensagem);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MensagemRegistrar>> GetAllMensagens()
        {
            try
            {
                var listMensagens = (from mensagem in _condolenciaContext.Mensagem
                                     join pessoa in _condolenciaContext.Pessoa on mensagem.IdPessoa equals pessoa.Id
                                     join vitima in _condolenciaContext.Vitima on mensagem.IdVitima equals vitima.Id
                                     select new MensagemRegistrar
                                     {
                                         Id = mensagem.Id,
                                         status = mensagem.Status,
                                         texto = mensagem.Texto,
                                         politica_privacidade = mensagem.PoliticaPrivacidade,
                                         privacidade = mensagem.Privacidade,
                                         Data = mensagem.DataCriacao,
                                         Pessoa = new PessoaViewModel
                                         {
                                             id = pessoa.Id,
                                             nome = pessoa.Nome,
                                             sobrenome = pessoa.SobreNome,
                                             cpf = pessoa.CPF,
                                             rg = pessoa.RG,
                                             email = pessoa.Email,
                                             sentimento = mensagem.Sentimento
                                         },
                                         Vitima = new VitimaViewModel
                                         {
                                             id = vitima.Id,
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

        public async Task<MensagemRegistrar> GetMensagem(int idMensagem)
        {
            try
            {
                var result = (from mensagem in _condolenciaContext.Mensagem
                                    join pessoa in _condolenciaContext.Pessoa on mensagem.IdPessoa equals pessoa.Id
                                    join vitima in _condolenciaContext.Vitima on mensagem.IdVitima equals vitima.Id
                                    select new MensagemRegistrar
                                    {
                                        Id = mensagem.Id,
                                        status = mensagem.Status,
                                        texto = mensagem.Texto,
                                        politica_privacidade = mensagem.PoliticaPrivacidade,
                                        privacidade = mensagem.Privacidade,
                                        Data = mensagem.DataCriacao,
                                        Pessoa = new PessoaViewModel
                                        {
                                            id = pessoa.Id,
                                            nome = pessoa.Nome,
                                            sobrenome = pessoa.SobreNome,
                                            cpf = pessoa.CPF,
                                            rg = pessoa.RG,
                                            email = pessoa.Email,
                                            sentimento = mensagem.Sentimento
                                        },
                                        Vitima = new VitimaViewModel
                                        {
                                            id = vitima.Id,
                                            nome = vitima.Nome,
                                            sobrenome = vitima.SobreNome,
                                            cpf = vitima.CPF,
                                            rg = vitima.RG,
                                            endereco_rua = vitima.Rua,
                                            endereco_cidade = vitima.Cidade,
                                            endereco_estado = vitima.Estado,
                                            imagem = vitima.Fotografia
                                        }
                                    }).Where(i => i.Id == idMensagem).ToList().FirstOrDefault();

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MensagemRegistrar>> GetMensagemByStatus(string status)
        {
            try
            {
                var listMensagem = (from mensagem in _condolenciaContext.Mensagem
                                    join pessoa in _condolenciaContext.Pessoa on mensagem.IdPessoa equals pessoa.Id
                                    join vitima in _condolenciaContext.Vitima on mensagem.IdVitima equals vitima.Id
                                    select new MensagemRegistrar
                                    {
                                        Id = mensagem.Id,
                                        status = mensagem.Status,
                                        texto = mensagem.Texto,
                                        politica_privacidade = mensagem.PoliticaPrivacidade,
                                        privacidade = mensagem.Privacidade,
                                        Pessoa = new PessoaViewModel
                                        {
                                            id = pessoa.Id,
                                            nome = pessoa.Nome,
                                            sobrenome = pessoa.SobreNome,
                                            cpf = pessoa.CPF,
                                            rg = pessoa.RG,
                                            email = pessoa.Email,
                                            sentimento = mensagem.Sentimento
                                        },
                                        Vitima = new VitimaViewModel
                                        {
                                            id = vitima.Id,
                                            nome = vitima.Nome,
                                            sobrenome = vitima.SobreNome,
                                            cpf = vitima.CPF,
                                            rg = vitima.RG,
                                            endereco_rua = vitima.Rua,
                                            endereco_cidade = vitima.Cidade,
                                            endereco_estado = vitima.Estado,
                                            imagem = vitima.Fotografia
                                        }
                                    }).Where(i => i.status == status).ToList();

                return await Task.FromResult(listMensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
