using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class NotaEntradaView
    {
       
        public int Id { get; set; }       
        public int IdEmpresa { get; set; }
        public int? idtransportadora { get; set; }
        public int? idAlmoxarifado { get; set; }
        public String Referencia { get; set; }
        public int Serie { get; set; }
        public string Numero { get; set; }
        public int IdColaborador { get; set; }
        public string RefColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public int? IdPedido { get; set; }        
        public int StatusNota { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataEmissao { get; set; }
        public decimal Frete { get; set; }
        public decimal Seguro { get; set; }
        public decimal Despesa { get; set; }
        public decimal DescontoPercent { get; set; }
        public decimal ValDesconto { get; set; }
        public string Observacao { get; set; }
        public decimal Descontoitem { get; set; }
        public decimal Baseicms { get; set; }
        public decimal Valoricms { get; set; }
        public decimal Baseipi { get; set; }
        public decimal Valoripi { get; set; }
        public decimal Totalnota { get; set; }
        public decimal TotalItens { get; set; }
        public decimal TotalProdutos { get; set; }
        public decimal DescontoValor { get; set; }
        public string IdsNfe { get; set; }
        public int TotalmenteDevolvida { get; set; }  //0=> não 1=> sim
        public int PossuiDevolucao { get; set; }  //0=> não 1=> sim
        public int? IdAntigo { get; set; }
        public int ValidadoParaEmissao { get; set; }

    }
}
