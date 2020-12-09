using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Models
{
    [Table("Depoimento")]
    public class Depoimento
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DataCriacao { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string SobreNome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Rua { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Profissao { get; set; }
        [Required]
        public string Email { get; set; }
        public string Sentimento { get; set; }
        public string Privacidade { get; set; }
        [Required]
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
    }
}
