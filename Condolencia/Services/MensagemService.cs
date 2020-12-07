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
            Pessoa pessoa = new Pessoa();
            Vitima vitima = new Vitima();
            string assunto = "Mensagem entregue";
            string htmlString = "";

            try
            {
                // Inclusão da Pessoa que está registrando a condolência
                await _pessoaService.CadastrarPessoa(mensagemViewModel.Pessoa);
                if (mensagemViewModel.Pessoa.codigoErro > 0)
                {
                    // Retornar dados inconsistentes
                    return await Task.FromResult(mensagemViewModel);
                }

                // Inclusão da Vítima que está sendo homenageada
                await _vitimaService.CadastrarVitima(mensagemViewModel.Vitima);
                if (mensagemViewModel.Vitima.codigoErro > 0)
                {
                    // Retornar dados inconsistentes
                    return await Task.FromResult(mensagemViewModel);
                }

                // Inclusão da mensagem
                Mensagem mensagem = new Mensagem();
                mensagem.Texto = mensagemViewModel.texto;
                mensagem.Status = "Pendente";
                mensagem.Sentimento = mensagemViewModel.Pessoa.sentimento;
                if (!String.IsNullOrEmpty(mensagemViewModel.privacidade.ToString().Trim()))
                {
                    mensagem.Privacidade = mensagemViewModel.privacidade;
                }
                else
                {
                    mensagem.Privacidade = 4;
                }

                mensagem.PoliticaPrivacidade = mensagemViewModel.politica_privacidade;
                mensagem.DataCriacao = DateTime.Now;
                mensagem.QrCode = null;

                // Salvar inclusão de Pessoa
                pessoa.Nome = mensagemViewModel.Pessoa.nome;
                pessoa.SobreNome = mensagemViewModel.Pessoa.sobrenome;
                pessoa.CPF = mensagemViewModel.Pessoa.cpf.Trim();
                pessoa.RG = mensagemViewModel.Pessoa.rg.Trim();
                pessoa.Email = mensagemViewModel.Pessoa.email;
                _condolenciaContext.Pessoa.Add(pessoa);
                await _condolenciaContext.SaveChangesAsync();
                mensagem.IdPessoa = pessoa.Id;

                // Salvar inclusão de Vítima
                vitima.Nome = mensagemViewModel.Vitima.nome;
                vitima.SobreNome = mensagemViewModel.Vitima.sobrenome;
                vitima.CPF = mensagemViewModel.Vitima.cpf.Trim();
                vitima.RG = mensagemViewModel.Vitima.rg.Trim();
                vitima.Rua = mensagemViewModel.Vitima.endereco_rua;
                vitima.Cidade = mensagemViewModel.Vitima.endereco_cidade;
                vitima.Estado = mensagemViewModel.Vitima.endereco_estado;
                vitima.Fotografia = mensagemViewModel.Vitima.imagem;
                _condolenciaContext.Vitima.Add(vitima);
                await _condolenciaContext.SaveChangesAsync();
                mensagem.IdVitima = vitima.Id;

                _condolenciaContext.Add(mensagem);
                _condolenciaContext.SaveChanges();

                // Enviar mensagem
                htmlString = @"<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" + Environment.NewLine;
                htmlString = htmlString + @"<html xmlns = 'http://www.w3.org/1999/xhtml'>" + Environment.NewLine;
                htmlString = htmlString + @"    <head>" + Environment.NewLine;
                htmlString = htmlString + @"        <meta http - equiv = 'Content -Type' content = 'text/html; charset=UTF-8' />" + Environment.NewLine;
                htmlString = htmlString + @"    </head>" + Environment.NewLine;
                htmlString = htmlString + @"    <body>" + Environment.NewLine;
                htmlString = htmlString + @"        <table border = '0' cellpadding = '0' cellspacing = '0' height = '100%' width = '100%' id = 'bodyTable' style = 'font-family: Verdana, Arial, sans-serif;'>" + Environment.NewLine;
                htmlString = htmlString + @"            <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                <td align = 'center' valign = 'top'>" + Environment.NewLine;
                htmlString = htmlString + @"                    <table border = '0' cellpadding = '20' cellspacing = '0' width = '700' id = 'emailContainer'>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <img src = 'https://memorialavarc.com.br/Images/logoPQ.png' alt = 'Avarc Logo' width = '150'>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #2abcf7; padding: 30px 15px;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'color: #fff; font-weight: bold; font-size: 24px; margin: 0;'> Obrigado por deixar sua mensagem! </p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 0 0;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 26px; margin: 0;'> Ol&aacute;, " + pessoa.Nome + " " + pessoa.SobreNome + @"! </p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'font-size: 15px; line-height: 20px; padding-bottom: 35px; margin: 0;'>Falta muito pouco para que sua condol&ecirc;ncia seja publicada. </br>" + Environment.NewLine;
                htmlString = htmlString + @"Nesse momento, ela est&aacute; sob an&aacute;lise dos nossos moderadores. </br>" + Environment.NewLine;
                htmlString = htmlString + @"Em breve concluiremos a an&aacute;lise e voc&ecirc; poder&aacute; compartilh&aacute;-la </br>" + Environment.NewLine;
                htmlString = htmlString + @"com seus familiares e amigos. </p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #f8f8f8; padding: 50px 15px;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'color: #395b77; font-size: 18px; margin: 0; display: inline-block; vertical-align: middle;'> Status da Condol&ecirc;ncia: </p>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 21px; margin: 0; display: inline-block; vertical-align: middle;'> Pendente </p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 20px 0;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'font-size: 14px; margin: 0; margin-bottom: 5px;'> Abra&ccedil;os,</p>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'font-size: 14px; margin: 0;'> Equipe memorial Avarc</p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                    </table>" + Environment.NewLine;
                htmlString = htmlString + @"                </td>" + Environment.NewLine;
                htmlString = htmlString + @"            </tr>" + Environment.NewLine;
                htmlString = htmlString + @"        </table>" + Environment.NewLine;
                htmlString = htmlString + @"    </body>" + Environment.NewLine;
                htmlString = htmlString + @"</html> ";

                await _emailService.SendEmailAsync(pessoa.Email, assunto, htmlString);

                var retorno = await GetMensagem(mensagem.Id);
                return await Task.FromResult(retorno);
            }
            catch (Exception ex)
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

                string htmlString = "";

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
                    htmlString = @"<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" + Environment.NewLine;
                    htmlString = htmlString + @"<html xmlns = 'http://www.w3.org/1999/xhtml'>" + Environment.NewLine;
                    htmlString = htmlString + @"    <head>" + Environment.NewLine;
                    htmlString = htmlString + @"        <meta http - equiv = 'Content -Type' content = 'text/html; charset=UTF-8' />" + Environment.NewLine;
                    htmlString = htmlString + @"    </head>" + Environment.NewLine;
                    htmlString = htmlString + @"    <body>" + Environment.NewLine;
                    htmlString = htmlString + @"        <table border = '0' cellpadding = '0' cellspacing = '0' height = '100%' width = '100%' id = 'bodyTable' style = 'font-family: Verdana, Arial, sans-serif;'>" + Environment.NewLine;
                    htmlString = htmlString + @"            <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                <td align = 'center' valign = 'top'>" + Environment.NewLine;
                    htmlString = htmlString + @"                    <table border = '0' cellpadding = '20' cellspacing = '0' width = '700' id = 'emailContainer'>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <img src = 'https://memorialavarc.com.br/Images/logoPQ.png' alt = 'Avarc Logo' width = '150'>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #2abcf7; padding: 30px 15px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #fff; font-weight: bold; font-size: 24px; margin: 0;'> Sua mensagem foi aprovada! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 0 0;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 26px; margin: 0;'> Ol&aacute;, " + pessoa.Nome + " " + pessoa.SobreNome + @"! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 15px; line-height: 20px; padding-bottom: 35px; margin: 0;'>Sua condol&ecirc;ncia foi aprovada e publicada. </br>Para acessar, clique no link abaixo:</p>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <a href='https://avarc.vercel.app/condolencia/" + mensagemModeradaViewModel.IdMensagem + @"' target = '_blank' style = 'color: #474cdc; font-size: 14px;'> Link da Condol&ecirc;ncia</a>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #f8f8f8; padding: 50px 15px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #395b77; font-size: 18px; margin: 0; display: inline-block; vertical-align: middle;'> Status da Condol&ecirc;ncia: </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 21px; margin: 0; display: inline-block; vertical-align: middle;'> Aprovada </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 20px 0;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 14px; margin: 0; margin-bottom: 5px;'> Abra&ccedil;os,</p>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 14px; margin: 0;'> Equipe memorial Avarc</p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                    </table>" + Environment.NewLine;
                    htmlString = htmlString + @"                </td>" + Environment.NewLine;
                    htmlString = htmlString + @"            </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"        </table>" + Environment.NewLine;
                    htmlString = htmlString + @"    </body>" + Environment.NewLine;
                    htmlString = htmlString + @"</html> ";

                    //htmlString = @" < html>
                    //  <body>
                    //  <p>Olá " + pessoa.Nome + " " + pessoa.SobreNome + @"</p>
                    //  <p>Sua condolência foi aprovada e já está publicada. Você poderá acessar a condolência através deste QR code.</p>
                    //  <p><br><img src= 'https://www.opememorial.net/api/QRCode/IdCondolencia?idCondolencia=" + mensagemModeradaViewModel.IdMensagem + @"' class='CToWUd a6T' tabindex='0'/></br></p>
                    //  <p>Caso não consiga ler o QR code, poderá acessar a condolência clicando <a href='https://avarc.vercel.app/condolencia/" + mensagemModeradaViewModel.IdMensagem + @"'> neste link</a></p>                      
                    //  </body>
                    //  </html>
                    // ";
                }
                else
                {
                    htmlString = @"<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" + Environment.NewLine;
                    htmlString = htmlString + @"<html xmlns = 'http://www.w3.org/1999/xhtml'>" + Environment.NewLine;
                    htmlString = htmlString + @"    <head>" + Environment.NewLine;
                    htmlString = htmlString + @"        <meta http - equiv = 'Content -Type' content = 'text/html; charset=UTF-8' />" + Environment.NewLine;
                    htmlString = htmlString + @"    </head>" + Environment.NewLine;
                    htmlString = htmlString + @"    <body>" + Environment.NewLine;
                    htmlString = htmlString + @"        <table border = '0' cellpadding = '0' cellspacing = '0' height = '100%' width = '100%' id = 'bodyTable' style = 'font-family: Verdana, Arial, sans-serif;'>" + Environment.NewLine;
                    htmlString = htmlString + @"            <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                <td align = 'center' valign = 'top'>" + Environment.NewLine;
                    htmlString = htmlString + @"                    <table border = '0' cellpadding = '20' cellspacing = '0' width = '700' id = 'emailContainer'>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <img src = 'https://memorialavarc.com.br/Images/logoPQ.png' alt = 'Avarc Logo' width = '150'>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #2abcf7; padding: 30px 15px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #fff; font-weight: bold; font-size: 24px; margin: 0;'> Sua mensagem foi reprovada! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 0 0;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 26px; margin: 0;'> Ol&aacute;, " + pessoa.Nome + " " + pessoa.SobreNome + @"! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 15px; line-height: 20px; padding-bottom: 35px; margin: 0;'>Infelizmente n&atilde;o conseguimos publicar sua mensagem. </br>" + Environment.NewLine;
                    htmlString = htmlString + @"Ela pode conter palavras ofensivas ou conte&uacute;do inapropriado. </br>" + Environment.NewLine;
                    htmlString = htmlString + @"Fique tranquilo(a). Voc&ecirc; pode enviar uma nova condol&ecirc;ncia, mas </br>" + Environment.NewLine;
                    htmlString = htmlString + @"evite conte&uacute;dos sens&iacute;veis para sua mensagem n&atilde;o ser reprovada. </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #f8f8f8; padding: 50px 15px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #395b77; font-size: 18px; margin: 0; display: inline-block; vertical-align: middle;'> Status da Condol&ecirc;ncia: </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 21px; margin: 0; display: inline-block; vertical-align: middle;'> Reprovada </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 20px 0;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 14px; margin: 0; margin-bottom: 5px;'> Abra&ccedil;os,</p>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 14px; margin: 0;'> Equipe memorial Avarc</p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                    </table>" + Environment.NewLine;
                    htmlString = htmlString + @"                </td>" + Environment.NewLine;
                    htmlString = htmlString + @"            </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"        </table>" + Environment.NewLine;
                    htmlString = htmlString + @"    </body>" + Environment.NewLine;
                    htmlString = htmlString + @"</html> ";
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
                                     }).OrderByDescending(i => i.Id).ToList();

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
                                    }).Where(i => i.status == status).OrderByDescending(o => o.Id).ToList();

                return await Task.FromResult(listMensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MensagemRegistrar>> GetQrCode()
        {
            try
            {
                var listMensagem = (from mensagem in _condolenciaContext.Mensagem
                                    select new MensagemRegistrar
                                    {
                                        Id = mensagem.Id,
                                        status = mensagem.Status,
                                        texto = mensagem.Texto,
                                        qrCode = mensagem.QrCode
                                    }).Where(i => i.status == "Aprovado").OrderByDescending(o => o.Id).ToList();

                return await Task.FromResult(listMensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
