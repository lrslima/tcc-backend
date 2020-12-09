using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.DTOs
{
    public class DepoimentoModeradoViewModel
    {
        public int IdDepoimento { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime DataAcao { get; set; }

        public string Status { get; set; }
        
        public int IdAlteradoPor { get; set; }
    }
}
