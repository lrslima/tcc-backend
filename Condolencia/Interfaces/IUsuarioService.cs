﻿using Condolencia.DTOs;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioViewModel> GetUsuariosLoginAsync(string email, string senha);
        Task<bool> CadastrarModerador(UsuarioViewModel usuario);

        Task<List<Usuario>> ListaModeradores();

    }
}
