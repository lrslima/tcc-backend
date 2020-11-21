using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Condolencia.Models;

namespace Condolencia.Data
{
    public class CondolenciaContext : DbContext
    {
        public CondolenciaContext (DbContextOptions<CondolenciaContext> options)
            : base(options)
        {
        }

        public DbSet<Condolencia.Models.Pessoa> Pessoa { get; set; }

        public DbSet<Condolencia.Models.Vitima> Vitima { get; set; }

        public DbSet<Condolencia.Models.Usuario> Usuario { get; set; }

        public DbSet<Condolencia.Models.Status> Status { get; set; }

        public DbSet<Condolencia.Models.Privacidade> Privacidade { get; set; }

        public DbSet<Condolencia.Models.Sentimento> Sentimento { get; set; }

        public DbSet<Condolencia.Models.MensagemModerada> MensagemModerada { get; set; }

        public DbSet<Condolencia.Models.Mensagem> Mensagem { get; set; }
    }
}
