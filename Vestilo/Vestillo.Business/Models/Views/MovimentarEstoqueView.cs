using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class MovimentarEstoqueView
    {
        public int Key { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public int? TamanhoId { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public int? CorId { get; set; }
        public string CorAbreviatura { get; set; }
        public decimal Saldo { get; set; }
        public decimal Qtd { get; set; }
        public int? IdUniMedida { get; set; }
        public string UnidMedidaAbreviatura { get; set; } 

    }
}
