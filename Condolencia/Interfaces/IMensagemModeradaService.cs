﻿using Condolencia.DTOs;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Interfaces
{
    public interface IMensagemModeradaService
    {
        Task<int> CadastrarModeracao(MensagemModeradaViewModel mensagemModeradaViewModel);
    }
}