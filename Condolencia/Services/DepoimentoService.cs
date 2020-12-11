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
    public class DepoimentoService : IDepoimentoService
    {
        private readonly CondolenciaContext _condolenciaContext;
        private readonly IEmailService _emailService;
        private readonly IDepoimentoModeradoService _depoimentoModeradoService;

        public DepoimentoService(CondolenciaContext condolenciaContext, IEmailService emailService, IDepoimentoModeradoService depoimentoModeradoService)
        {
            _condolenciaContext = condolenciaContext;
            _emailService = emailService;
            _depoimentoModeradoService = depoimentoModeradoService;
        }

        public async Task<DepoimentoRegistrar> RegistrarDepoimento(DepoimentoRegistrar depoimentoViewModel)
        {
            string assunto = "Depoimento entregue";
            string htmlString = "";

            try
            {
                // Validações - CPF ou RG - Ao menos 1 é obrigatório
                if ((String.IsNullOrEmpty(depoimentoViewModel.CPF.Trim())) && (String.IsNullOrEmpty(depoimentoViewModel.RG.Trim())))
                {
                    throw new Exception("CPF ou RG do homenageante é obrigatório");
                }

                // CPF caso informado será validado
                if (!String.IsNullOrEmpty(depoimentoViewModel.CPF.Trim()))
                {
                    if (!Validacao.ValidaCPF.IsCpf(depoimentoViewModel.CPF.Trim()))
                    {
                        throw new Exception("CPF do homenageante informado é inválido");
                    }
                }

                // RG caso informado será validado
                if (!String.IsNullOrEmpty(depoimentoViewModel.RG.Trim()))
                {
                    if (!Validacao.ValidaRG.IsRg(depoimentoViewModel.RG.Trim()))
                    {
                        throw new Exception("RG do homenageante informado é inválido");
                    }
                }

                // E-mail caso informado será validado
                if (!String.IsNullOrEmpty(depoimentoViewModel.Email.Trim()))
                {
                    if (!Validacao.ValidaEmail.IsEmail(depoimentoViewModel.Email.Trim()))
                    {
                        throw new Exception("E-mail informado é inválido");
                    }
                }

                // Inclusão da mensagem
                Depoimento depoimento = new Depoimento();
                depoimento.DataCriacao = DateTime.Now;
                depoimento.Nome = depoimentoViewModel.Nome;
                depoimento.SobreNome = depoimentoViewModel.SobreNome;
                depoimento.CPF = depoimentoViewModel.CPF.Trim();
                depoimento.RG = depoimentoViewModel.RG.Trim();
                depoimento.Rua = depoimentoViewModel.Rua;
                depoimento.Cidade = depoimentoViewModel.Cidade;
                depoimento.Estado = depoimentoViewModel.Estado;
                depoimento.Profissao = depoimentoViewModel.Profissao;
                depoimento.Email = depoimentoViewModel.Email;
                depoimento.Sentimento = depoimentoViewModel.Sentimento;
                if (!String.IsNullOrEmpty(depoimentoViewModel.Privacidade.ToString().Trim()))
                {
                    depoimento.Privacidade = depoimentoViewModel.Privacidade;
                }
                else
                {
                    depoimento.Privacidade = "Público";
                }
                depoimento.PoliticaPrivacidade = depoimentoViewModel.PoliticaPrivacidade;
                depoimento.Fotografia = depoimentoViewModel.Fotografia;
                depoimento.Texto = depoimentoViewModel.Texto;
                depoimento.Status = "Pendente";
                depoimento.QrCode = null;

                // Salvar Depoimento
                _condolenciaContext.Add(depoimento);
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
                htmlString = htmlString + @"                                <p style = 'color: #fff; font-weight: bold; font-size: 24px; margin: 0;'> Obrigado por deixar seu Depoimento! </p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 0 0;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 26px; margin: 0;'> Ol&aacute;, " + depoimento.Nome + " " + depoimento.SobreNome + @"! </p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'font-size: 15px; line-height: 20px; padding-bottom: 35px; margin: 0;'>Falta muito pouco para que seu depoimento seja publicado. </br>" + Environment.NewLine;
                htmlString = htmlString + @"Nesse momento, ele est&aacute; sob an&aacute;lise dos nossos moderadores. </br>" + Environment.NewLine;
                htmlString = htmlString + @"Em breve concluiremos a an&aacute;lise e voc&ecirc; poder&aacute; compartilh&aacute;-lo </br>" + Environment.NewLine;
                htmlString = htmlString + @"com seus familiares e amigos. </p>" + Environment.NewLine;
                htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #f8f8f8; padding: 50px 15px;'>" + Environment.NewLine;
                htmlString = htmlString + @"                                <p style = 'color: #395b77; font-size: 18px; margin: 0; display: inline-block; vertical-align: middle;'> Status do Depoimento: </p>" + Environment.NewLine;
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

                await _emailService.SendEmailAsync(depoimento.Email, assunto, htmlString);

                var retorno = await GetDepoimento(depoimento.Id);
                return await Task.FromResult(retorno);
            }
            catch (Exception ex)
            {
                return await Task.FromException<DepoimentoRegistrar>(ex);
            }

        }

        public async Task<DepoimentoRegistrar> AlterarStatus(DepoimentoModeradoViewModel depoimentoModeradoViewModel)
        {
            try
            {
                var depoimento = await _condolenciaContext.Depoimento.FindAsync(depoimentoModeradoViewModel.IdDepoimento);
                string assunto = "Depoimento reprovado pelo moderador";

                // incluir moderção na tabela de moderação ------ var idModeracao = await _ModeracaoMensagemService.AlterarStatus(statusViewModel.Status);
                var moderacao = await _depoimentoModeradoService.SalvarDepoimentoModeracao(depoimentoModeradoViewModel);

                string htmlString = "";

                // Verificar se Aprovado gerar o QR code
                string stringBase64 = string.Empty;
                Bitmap imagemQRcode;
                Byte[] imagemCode = null;

                if (depoimentoModeradoViewModel.Status.Trim().Equals("Aprovado", StringComparison.OrdinalIgnoreCase))
                {
                    assunto = "Mensagem aprovada pelo moderador";
                    string urlCondolencia = $"https://avarc.vercel.app/condolencia/" + depoimentoModeradoViewModel.IdDepoimento;
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
                    htmlString = htmlString + @"                                <p style = 'color: #fff; font-weight: bold; font-size: 24px; margin: 0;'> Seu depoimento foi aprovado! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 0 0;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 26px; margin: 0;'> Ol&aacute;, " + depoimento.Nome + " " + depoimento.SobreNome + @"! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 15px; line-height: 20px; padding-bottom: 35px; margin: 0;'>Seu depoimento foi aprovado e publicado. </br>Para acessar, clique no link abaixo:</p>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <a href='https://avarc.vercel.app/depoimento/" + depoimentoModeradoViewModel.IdDepoimento + @"' target = '_blank' style = 'color: #474cdc; font-size: 14px;'> Link do Depoimento</a>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #f8f8f8; padding: 50px 15px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #395b77; font-size: 18px; margin: 0; display: inline-block; vertical-align: middle;'> Status do Depoimento: </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 21px; margin: 0; display: inline-block; vertical-align: middle;'> Aprovado </p>" + Environment.NewLine;
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
                    htmlString = htmlString + @"                                <p style = 'color: #fff; font-weight: bold; font-size: 24px; margin: 0;'> Seu depoimento foi reprovado! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px 0 0 0;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #084069; font-weight: bold; font-size: 26px; margin: 0;'> Ol&aacute;, " + depoimento.Nome + " " + depoimento.SobreNome + @"! </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'padding: 50px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'font-size: 15px; line-height: 20px; padding-bottom: 35px; margin: 0;'>Infelizmente n&atilde;o conseguimos publicar seu depoimento. </br>" + Environment.NewLine;
                    htmlString = htmlString + @"Ele pode conter palavras ofensivas ou conte&uacute;do inapropriado. </br>" + Environment.NewLine;
                    htmlString = htmlString + @"Fique tranquilo(a). Voc&ecirc; pode enviar um novo depoimento, mas </br>" + Environment.NewLine;
                    htmlString = htmlString + @"evite conte&uacute;dos sens&iacute;veis para seu depoimento n&atilde;o ser reprovado. </p>" + Environment.NewLine;
                    htmlString = htmlString + @"                            </td>" + Environment.NewLine;
                    htmlString = htmlString + @"                        </tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                        <tr>" + Environment.NewLine;
                    htmlString = htmlString + @"                            <td align = 'center' valign = 'top' style = 'background-color: #f8f8f8; padding: 50px 15px;'>" + Environment.NewLine;
                    htmlString = htmlString + @"                                <p style = 'color: #395b77; font-size: 18px; margin: 0; display: inline-block; vertical-align: middle;'> Status do Depoimento: </p>" + Environment.NewLine;
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
                depoimento.Status = depoimentoModeradoViewModel.Status;
                depoimento.QrCode = imagemCode;

                // Gravar moderação
                _condolenciaContext.Add(moderacao);
                _condolenciaContext.SaveChanges();

                // Atualizar dados condolencia
                _condolenciaContext.Update(depoimento);
                _condolenciaContext.SaveChanges();

                // Enviar mensagem
                await _emailService.SendEmailAsync(depoimento.Email, assunto, htmlString);

                var result = await GetDepoimento(depoimentoModeradoViewModel.IdDepoimento);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return await Task.FromException<DepoimentoRegistrar>(ex);
            }
        }

        public async Task<List<DepoimentoRegistrar>> GetAllDepoimentos()
        {
            try
            {
                var listDepoimento = (from depoimento in _condolenciaContext.Depoimento
                                     select new DepoimentoRegistrar
                                     {
                                         Id = depoimento.Id,
                                         DataCriacao = depoimento.DataCriacao,
                                         Nome = depoimento.Nome,
                                         SobreNome = depoimento.SobreNome,
                                         CPF = depoimento.CPF,
                                         RG = depoimento.RG,
                                         Rua = depoimento.Rua,
                                         Cidade = depoimento.Cidade,
                                         Estado = depoimento.Estado,
                                         Profissao = depoimento.Profissao,
                                         Email = depoimento.Email,
                                         Sentimento = depoimento.Sentimento,
                                         Privacidade = depoimento.Privacidade,
                                         PoliticaPrivacidade = depoimento.PoliticaPrivacidade,
                                         Fotografia = depoimento.Fotografia,
                                         Texto = depoimento.Texto,
                                         Status = depoimento.Status,
                                         QrCode = depoimento.QrCode
                                     }).OrderByDescending(i => i.Id).ToList();

                return await Task.FromResult(listDepoimento);
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<DepoimentoRegistrar>>(ex);
            }
        }

        public async Task<DepoimentoRegistrar> GetDepoimento(int idDepoimento)
        {
            try
            {
                var result = (from depoimento in _condolenciaContext.Depoimento
                              select new DepoimentoRegistrar
                              {
                                  Id = depoimento.Id,
                                  DataCriacao = depoimento.DataCriacao,
                                  Nome = depoimento.Nome,
                                  SobreNome = depoimento.SobreNome,
                                  CPF = depoimento.CPF,
                                  RG = depoimento.RG,
                                  Rua = depoimento.Rua,
                                  Cidade = depoimento.Cidade,
                                  Estado = depoimento.Estado,
                                  Profissao = depoimento.Profissao,
                                  Email = depoimento.Email,
                                  Sentimento = depoimento.Sentimento,
                                  Privacidade = depoimento.Privacidade,
                                  PoliticaPrivacidade = depoimento.PoliticaPrivacidade,
                                  Fotografia = depoimento.Fotografia,
                                  Texto = depoimento.Texto,
                                  Status = depoimento.Status,
                                  QrCode = depoimento.QrCode
                              }).Where(i => i.Id == idDepoimento).ToList().FirstOrDefault();

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return await Task.FromException<DepoimentoRegistrar>(ex);
            }
        }

        public async Task<List<DepoimentoRegistrar>> GetDepoimentoByStatus(string status)
        {
            try
            {
                var listDepoimento = (from depoimento in _condolenciaContext.Depoimento
                                    select new DepoimentoRegistrar
                                    {
                                        Id = depoimento.Id,
                                        DataCriacao = depoimento.DataCriacao,
                                        Nome = depoimento.Nome,
                                        SobreNome = depoimento.SobreNome,
                                        CPF = depoimento.CPF,
                                        RG = depoimento.RG,
                                        Rua = depoimento.Rua,
                                        Cidade = depoimento.Cidade,
                                        Estado = depoimento.Estado,
                                        Profissao = depoimento.Profissao,
                                        Email = depoimento.Email,
                                        Sentimento = depoimento.Sentimento,
                                        Privacidade = depoimento.Privacidade,
                                        PoliticaPrivacidade = depoimento.PoliticaPrivacidade,
                                        Fotografia = depoimento.Fotografia,
                                        Texto = depoimento.Texto,
                                        Status = depoimento.Status,
                                        QrCode = depoimento.QrCode
                                    }).Where(i => i.Status == status).OrderByDescending(o => o.Id).ToList();

                return await Task.FromResult(listDepoimento);
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<DepoimentoRegistrar>> (ex);
            }
        }

        public async Task<List<DepoimentoRegistrar>> GetQrCode()
        {
            try
            {
                var listDepoimento = (from depoimento in _condolenciaContext.Depoimento
                                    select new DepoimentoRegistrar
                                    {
                                        Id = depoimento.Id,
                                        Status = depoimento.Status,
                                        Texto = depoimento.Texto,
                                        QrCode = depoimento.QrCode
                                    }).Where(i => i.Status == "Aprovado").OrderByDescending(o => o.Id).ToList();

                return await Task.FromResult(listDepoimento);
            }
            catch (Exception ex)
            {
                return await Task.FromException<List<DepoimentoRegistrar>> (ex);
            }
        }
    }
}
