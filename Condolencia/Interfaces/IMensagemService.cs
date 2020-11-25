using Condolencia.DTOs;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Interfaces
{
    public interface IMensagemService
    {
        Task<Mensagem> RegistrarMensagem(MensagemRegistrar mensagemViewModel);
        
        void AlterarStatus(StatusMensagemViewModel statusViewModel, MensagemRegistrar mensagemViewModel);

        Task<List<MensagemRegistrar>> GetAllMensagens();
        
        Task<List<MensagemRegistrar>> GetMensagem(int idMensagem);
    }
}
