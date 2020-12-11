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
    public class DepoimentoRegistrar
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Profissao { get; set; }
        public string Email { get; set; }
        public string Sentimento { get; set; }
        public string Privacidade { get; set; }
        public bool PoliticaPrivacidade { get; set; }
        public Byte[] Fotografia { get; set; }
        public string Texto { get; set; }
        public string Status { get; set; }
        public Byte[] QrCode { get; set; }
        public string NomeCompleto
        {
            get
            {
                return Nome + " " + SobreNome;
            }
        }
        //public int codigoErro { get; set; }
        //public string mensagemErro { get; set; }
    }
}
