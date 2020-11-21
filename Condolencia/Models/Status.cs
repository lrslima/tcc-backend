﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Models
{
    [Table("Status")]
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage="O campo descrição suporta no máximo {1} caracteres"), MinLength(3)]
        public string Descricao { get; set; }

        [DefaultValue(1)]
        public int Ativo { get; set; }
    }
}
