﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class TamanhoView : Tamanho
    {
        [NaoMapeado]
        public decimal Estoque { get; set; }
    }
}
