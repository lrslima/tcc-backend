using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Condolencia.Models;

namespace Condolencia.DTOs
{
    public class MensagemRegistrar
    {
        public int Id { get; set; }
        
        public string status { get; set; }
        
        public string texto { get; set; }
        
        public bool politica_privacidade { get; set; }

        public int privacidade { get; set; }

        public DateTime Data { get; set; }

        public PessoaViewModel Pessoa { get; set; }

        public VitimaViewModel Vitima { get; set; }

    }
}
