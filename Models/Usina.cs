﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeUsinas.Models
{
    public class Usina
    {
        public int ID { get; set; }
        public string UC { get; set; }
        public bool Ativo { get; set; }
        public int IDFornecedor { get; set; }
    }
}
