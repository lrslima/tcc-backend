using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.DTOs
{
    public class PessoaViewModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string email { get; set; }
        public string sentimento { get; set; }
        public string NomeCompleto
        {
            get
            {
                return nome + " " + sobrenome;
            }
        }
        public int codigoErro { get; set; }
        public string mensagemErro { get; set; }
    }
}
