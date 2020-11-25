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
    public class UsuarioService : IUsuarioService
    {
        private readonly CondolenciaContext _context;
        public UsuarioService(CondolenciaContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetUsuariosAdminAsync(string email, string senha)
        {
            try
            {
                var listAdmin = (from usuario in _context.Usuario
                                 select new Usuario
                                 {
                                     Id = usuario.Id,
                                     Nome = usuario.Nome,
                                     Sobrenome = usuario.Sobrenome,
                                     Email = usuario.Email,
                                     Senha = usuario.Senha,

                                 }).Where(i =>
                                            i.Email == email &&
                                            i.Senha == senha &&
                                            i.TipoUsuario == 1).ToList();

                return await Task.FromResult(listAdmin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
