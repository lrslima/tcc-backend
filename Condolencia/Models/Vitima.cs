using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Models
{
    [Table("Vitima")]
    public class Vitima
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string SobreNome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Byte[] Fotografia { get; set; }
        public string NomeCompleto
        {
            get
            {
                return Nome +" " + SobreNome;
            }
        }
    }
}
