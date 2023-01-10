
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("NotaEntradaItens", "NotaEntradaItens")]
    public class NotaEntradaItens
    {
        [Chave]
        public int Id { get; set; }
        public int IdNota { get; set; }
        public int IdTipoMov { get; set; }
        public int NumItem { get; set; }
        public int CalculaIcms { get; set; }
        public int CalculaIpi { get; set; }
        public int? IdItemNoPedido { get; set; }
        public int iditem { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Excedente { get; set; }
        public decimal Qtddevolvida { get; set; }
        public decimal Preco { get; set; }
        public decimal total { get; set; }        
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
}