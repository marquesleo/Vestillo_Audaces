using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Nfce","Nfce")]
    public class Nfce
    {
        public enum enumTipoNFCe
        {
            Venda = 0,
            PreVenda = 1
        }

        [Chave]
        public int Id { get; set; }
        [Contador("Nfce")]
        [RegistroUnico]
        public String Referencia { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int? IdTabelaPreco { get; set; }
        public int IdVendedor { get; set; }
        public int? IdAlmoxarifado { get; set; }
        public int IdCliente { get; set; }
        public int? IdPedido { get; set; }
        public int? IdGuia { get; set; }

        public DateTime DataEmissao { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public DateTime? datafaturamento { get; set; }
        public decimal PercDesconto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal DescontoGrid { get; set; }
        public decimal Total { get; set; }
        public string NumeroNfce { get; set; }
        public decimal TotalOriginal { get; set; }
        public decimal TotalDevolvido { get; set; }
        public decimal BaseIcms { get; set; }
        public decimal ValorIcms { get; set; }
        public string Serie { get; set; }
        public string XmlAssinado { get; set; }
        public string NotaAssinadaId { get; set; }
        public string ReciboNota { get; set; }
        public string DataRecibo { get; set; }
        public int RecebidaSefaz { get; set; }
        public int EmitidaContingencia { get; set; }
        public string ProtocoloSefaz { get; set; }
        public decimal TotalTributos { get; set; }
        public int StatusNota { get; set; }
        public decimal Troco { get; set; }
        public decimal ValorDinheiro { get; set; }
        public decimal ValorCartaoCredito { get; set; }
        public decimal ValorCartaoDebito { get; set; }
        public decimal ValorCheque { get; set; }
        public decimal ValorNcc { get; set; }
        public int TotalmenteDevolvida { get; set; }  //0=> não 1=> sim
        public int PossuiDevolucao { get; set; }  //0=> não 1=> sim
        public decimal basefcpestado { get; set; } // Novo 4.0
        public decimal Valorfcpestado { get; set; } // Novo 4.0
        
        public enumTipoNFCe TipoNFCe { get; set; }
        public string Observacao { get; set; }

        [NaoMapeado]
        public IEnumerable<NfceItens> GradeItens { get; set; }
        
        [NaoMapeado]
        public IEnumerable<ContasReceber> ParcelasCtr { get; set; }

        [NaoMapeado]
        public IEnumerable<CreditosClientes> CreditosClientes { get; set; }

        [NaoMapeado]
        public IEnumerable<MovimentacaoEstoque> ItensMovimentacaoEstoque { get; set; }
    }
}
