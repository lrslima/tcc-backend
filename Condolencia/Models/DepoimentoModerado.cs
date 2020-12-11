﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.Models
{
    [Table("DepoimentoModerado")]
    public class DepoimentoModerado
    {
        [Key]
        public int Id { get; set; }

        public int IdDepoimento { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DataAcao { get; set; }
        
        [Required]
        public string Status { get; set; }

        public int IdAlteradoPor { get; set; }
    }
}