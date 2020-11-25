using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Models
{
    [Table("Mensagem")]
    public class Mensagem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pessoa")]
        [Column(Order = 1)]
        public int IdPessoa { get; set; }

        [ForeignKey("Vitima")]
        [Column(Order = 2)]
        public int IdVitima { get; set; }

        [Required]
        [MaxLength(255), MinLength(3)]
        public string Texto { get; set; }

        public string Status { get; set; }

        public string Sentimento { get; set; }

        public int Privacidade { get; set; }

        public bool PoliticaPrivacidade { get; set; }

        public Byte[] QrCode { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
