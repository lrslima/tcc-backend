﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condolencia.DTOs
{
    public class VitimaViewModel
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string endereco_rua { get; set; }
        public string endereco_cidade { get; set; }
        public string endereco_estado { get; set; }
        public Byte[] imagem { get; set; }
    }
}