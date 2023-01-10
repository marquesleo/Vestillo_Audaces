using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
  

    public class OrdemAutomatizadaView
    {
        public int NumItem { get; set; }
        public string Id { get; set; }
        public string AlmoxarifadoReferencia { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public int TamanhoId { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public int CorId { get; set; }
        public string CorAbreviatura { get; set; }
        public string CorReferencia { get; set; }
        public decimal Fisico { get; set; }
        public decimal Empenhado { get; set; }
        public decimal Disponivel { get; set; }
        public int Inutilizado { get; set; }

        public decimal ProduzidoParaEstoque { get; set; }
        public decimal ProduzidoParaPedido { get; set; }
        public decimal PedidoSemEmpenho { get; set; }
        public decimal PedidoSemLiberacao { get; set; }
        public decimal SaldoPositivo { get; set; }
        public decimal SaldoNegativo { get; set; }
        public decimal SaldoSemLiberacao { get; set; }      
        

        public string Colecao { get; set; }
        public string Grupo { get; set; }

        public decimal QuantidadeProducao { get; set; }
        public decimal QuantidadeProduzida { get; set; }
        public decimal QuantidadeParaOrdem { get; set; }

        public int IdNovaOp { get; set; }
        public string  NumNovaOp { get; set; }

    }
}
