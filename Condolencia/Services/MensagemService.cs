using Condolencia.DTOs;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Services
{
    public class MensagemService
    {

        public Task<Mensagem> FormatarMensagem(PublicarMensagem publicarMensagem)
        {
            Mensagem mensagemValida = new Mensagem();


            mensagemValida.Texto = publicarMensagem.Texto;

            return Task.FromResult(mensagemValida);
        }
    }
}
