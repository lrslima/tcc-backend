﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Models
{
    [Table("MensagemModerada")]
    public class MensagemModerada
    {
        [Key]
        public int Id { get; set; }

        public int IdMensagem { get; set; }
        
        public DateTime DataAcao { get; set; }
        
        [Required]
        public string Status { get; set; }
    }
}
