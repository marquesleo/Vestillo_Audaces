using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ConsultaMovimentacaoEstoqueView
    {
        public DateTime DataMovimento { get; set; }
        public string UsuarioNome { get; set; }
        public decimal Saldo { get; set; }
        public string TamanhoDescricao { get; set; }
        public string ProdutoDescricao { get; set; }
        public string ProdutoReferencia { get; set; }
        public string CorDescricao { get; set; }
        public string CorAbreviacao { get; set; }
        public string TamanhoAbreviacao { get; set; }
        public string UnidMedidaAbreviatura { get; set; }
        public decimal? Entrada { get; set; }
        public decimal? Saida { get; set; }
        public string Observacao { get; set; }
        
    }
}
