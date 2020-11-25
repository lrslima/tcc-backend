using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.DTOs
{
    public class StatusMensagemViewModel
    {
        public int IdMensagem { get; set; }

        public DateTime DataAcao { get; set; }

        public string Status { get; set; }

        public int IdAlteradoPor { get; set; }
    }
}
