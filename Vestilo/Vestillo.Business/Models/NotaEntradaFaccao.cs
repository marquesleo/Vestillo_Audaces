using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
   
    [Tabela("notaentradafaccao", "Nota de Entrada Facção")]
    public class NotaEntradaFaccao
    {

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int? idtransportadora { get; set; }
        public int? idAlmoxarifado { get; set; }
        public int Serie { get; set; }
        public int IdColaborador { get; set; }
        public int IdOrdemProducao { get; set; }
        [Contador("NotaEntradaFaccao")]
        [RegistroUnico]
        public String Referencia { get; set; }
        public string Numero { get; set; }
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
        public int TotalmenteDevolvida { get; set; }  //0=> não 1=> sim
        public int PossuiDevolucao { get; set; }  //0=> não 1=> sim                


        [NaoMapeado]
        public IEnumerable<NotaEntradaFaccaoItens> ItensNota { get; set; }

        [NaoMapeado]
        public IEnumerable<ContasPagar> ParcelasCtp { get; set; }

    }

    public class NotaEntradaFaccaoView : NotaEntradaFaccao
    {  
        public string RefColaborador { get; set; }
        public string NomeColaborador { get; set; }
    }
}