using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100, ErrorMessage = "O campo Nome suporta no máximo {1} caracteres"), MinLength(3)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O campo Nome suporta no máximo {1} caracteres"), MinLength(3)]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "O campo Email suporta no máximo {1} caracteres"), MinLength(3)]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        public int Ativo { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa conter mais de {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "Senhas digitadas não são iguais, verifique.")]
        public string ConfirmarSenha { get; set; }

        public int TipoUsuario { get; set; }

    }
}
