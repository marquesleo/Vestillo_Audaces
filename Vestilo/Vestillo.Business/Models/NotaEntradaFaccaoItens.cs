
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("NotaEntradaFaccaoItens", "NotaEntradaFaccaoItens")]
    public class NotaEntradaFaccaoItens
    {
        [Chave]
        public int Id { get; set; }
        public int IdNota { get; set; }
        public int IdTipoMov { get; set; }
        public int NumItem { get; set; }
        public int CalculaIcms { get; set; }
        public int CalculaIpi { get; set; }
        public int? IdItemNaOrdem { get; set; }
        public int iditem { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public decimal QuantidadeOp { get; set; }
        public decimal quantidadeEntrega { get; set; }
        public decimal quantidadeProduzida { get; set; }
        public decimal quantidadeAvaria { get; set; }
        public decimal quantidadeDefeito { get; set; }
        public decimal Qtddevolvida { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
        public decimal DescontoRateado { get; set; }
        public decimal BaseIcmsRateado { get; set; }
        public decimal Percenticms { get; set; }
        public decimal ValorIcmsRateado { get; set; }
        public decimal BaseIpiRateado { get; set; }
        public decimal Percentipi { get; set; }
        public decimal ValorIpiRateado { get; set; }
        public decimal DespesaRateio { get; set; }
        public decimal FreteRateio { get; set; }
        public decimal SeguroRateio { get; set; }
    }

    public class NotaEntradaFaccaoItensView: NotaEntradaFaccaoItens
    {        
        public string NumNota { get; set; }
        public string referencia { get; set; }
        public string DescProduto { get; set; }
        public string DescCor { get; set; }
        public string DescTamanho { get; set; }
        public string AbreviaturaCores { get; set; }
        public string AbreviaturaTamanho { get; set; }
        public string unidade { get; set; }               
    }

    /*
        public decimal Totalproduzido { get; set; }
        public decimal Totalavaria { get; set; }
        public decimal Totaldefeito { get; set; }
     
     */
}