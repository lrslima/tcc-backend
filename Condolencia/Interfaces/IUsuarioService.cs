using Condolencia.DTOs;
using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Interfaces
{
    public interface IUsuarioService
    {
        //Task<List<Usuario>> GetUsuariosAdminAsync();
        Task<UsuarioViewModel> GetUsuariosLoginAsync(string email, string senha);
        //Task<List<UsuarioViewModel>> GetMensagemByTipo(int tipoUsuario);
    }
}
