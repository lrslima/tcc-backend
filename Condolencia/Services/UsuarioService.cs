﻿using Condolencia.Data;
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

        public Task<bool> CadastrarModerador(UsuarioViewModel usuario)
        {
            try
            {
                // Validar se o e-mail já foi cadastrado anteriormente
                var existeEmail = _context.Usuario.Where(x => x.Email.ToUpper().Trim() == usuario.Email.ToUpper().Trim()).Count();

                if (existeEmail == 0)
                {
                    if (!Validacao.ValidaEmail.IsEmail(usuario.Email.Trim()))
                    {
                        return Task.FromResult(false);
                    }

                    Usuario user = new Usuario();
                    user.Nome = usuario.Nome;
                    user.Sobrenome = usuario.Sobrenome;
                    user.Senha = usuario.Senha;
                    user.ConfirmarSenha = usuario.Senha;
                    user.TipoUsuario = 2;
                    user.Ativo = 1;
                    user.Email = usuario.Email.Trim();

                    _context.Usuario.Add(user);
                    _context.SaveChanges();

                    return Task.FromResult(true);
                }
                else
                {
                    throw new Exception("Já existe usuário cadastrado com o e-mail informado");
                }
            }
            catch (Exception ex)
            {
                return Task.FromException<bool>(ex);
            }
        }

        public Task<List<Usuario>> ListaModeradores()
        {
            try
            {
                var result = _context.Usuario.Where(x => x.TipoUsuario == 2).ToList();
                result.ForEach(usuario =>
                {
                    usuario.Senha = "*****";
                });

                return Task.FromResult(result);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UsuarioViewModel> GetUsuariosLoginAsync(string email, string senha)
        {
            try
            {
                var listUser = (from usuario in _context.Usuario
                                select new Usuario
                                {
                                    Id = usuario.Id,
                                    Nome = usuario.Nome,
                                    Sobrenome = usuario.Sobrenome,
                                    Email = usuario.Email,
                                    Senha = usuario.Senha,

                                }).Where(i =>
                                           i.Email == email &&
                                           i.Senha == senha).ToList().FirstOrDefault();

                UsuarioViewModel usuarioRet = new UsuarioViewModel();

                if (listUser != null)
                {
                    usuarioRet.id = listUser.Id;
                    usuarioRet.Nome = listUser.Nome;
                    usuarioRet.Autorizado = true;
                }
                else
                {
                    usuarioRet.Autorizado = false;
                }

                return await Task.FromResult(usuarioRet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
