using Condolencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.DTOs
{
    public class PublicarMensagem
    {

        public int Nome { get; set; }

        public int Sobrenome { get; set; }

        public int Endereco { get; set; }

        public string Texto { get; set; }

        public Pessoa Pessoa { get; set; }

    }
}
