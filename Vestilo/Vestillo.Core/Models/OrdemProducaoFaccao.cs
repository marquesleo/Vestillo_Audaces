
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models.Views
{
    public class OrdemProducaoFaccao
    {
        public int IdOrdem { get; set; }
        public int IdFaccao { get; set; }
        public int IdItem { get; set; }
        public string RefOrdem {get; set;}
        public string Faccao {get; set;}
        public string RefItem {get; set;}
        public string DescricaoItem { get; set; }
        public int IdCor { get; set; }
        public string Cor {get; set;}
        public int IdTamanho { get; set; }        
        public string Tamanho {get; set;}
        public decimal Quantidade {get; set;}
        public decimal Valor {get; set;}
        public decimal Total {get; set;}
    }
}

