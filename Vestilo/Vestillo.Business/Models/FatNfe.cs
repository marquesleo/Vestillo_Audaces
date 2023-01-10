using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;

namespace Vestillo.Business.Models
{
    [Tabela("Nfe", "Nfe")]    
    public class FatNfe
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int? idtransportadora { get; set; }
        public int? idAlmoxarifado { get; set; }
        public int? idvendedor { get; set; }
        public int? idvendedor2 { get; set; }
        public int? idTabela { get; set; }
        public int Tipo { get; set; }
        public int Serie { get; set; }   
        public int IdColaborador { get; set; }
        public int? IdPedido { get; set; }
        public int? idDocEntrada { get; set; }
        public int? idNfe { get; set; }
        public int? idNfce { get; set; }    
        public int? idOrdemProducao { get; set; }
        [Contador("FatNfe")]
        [RegistroUnico]
        public String Referencia { get; set; }
        public int NotaComplementar { get; set; }  
        public string Numero { get; set; }
        public string refpedidocliente { get; set; }       
        public int StatusNota { get; set; }
        public DateTime? DataInclusao { get; set; } 
        public DateTime?  DataEmissao  { get; set; } 
        public DateTime? DataSaida { get; set; }
        public DateTime? DataPrograma { get; set; }
        public DateTime? Horaemissao { get; set; }
        public decimal Frete { get; set; }        
        public decimal Seguro { get; set; }
        public decimal Despesa { get; set; }
        public decimal DescontoPercent { get; set; }
        public decimal ValDesconto { get; set; }        
        public string Observacao { get; set; }   
        public string Xmlassinado { get; set; }
        public string LogDeEnvio { get; set; }
        public string Idnotaassinada { get; set; }
        public string Recibonota { get; set; }
        public string Datarecibo { get; set; }
        public string Statusxml { get; set; }
        public string Recebidasefaz { get; set; }
        public string Protocolosefaz { get; set; }
        public decimal Descontoitem { get; set; }
        public decimal Baseicms { get; set; }
        public decimal Valoricms { get; set; }
        public decimal Totalnota { get; set; }             
        public decimal Valorpartemissor { get; set; }
        public decimal Valorpartdest { get; set; }
        public decimal Valorfcp { get; set; }
        public decimal basefcpestado { get; set; } // Novo 4.0
        public decimal Valorfcpestado { get; set; } // Novo 4.0
        public decimal TotalItens { get; set; }
        public decimal TotalProdutos { get; set; }
        public decimal DescontoValor { get; set; }
        public decimal Bonificado { get; set; }
        public decimal Pis { get; set; }
        public decimal Cofins { get; set; }
        public decimal Sefaz { get; set; }
        public decimal ValorBonificado { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal ValorSefaz { get; set; }
        public decimal valortributofederal { get; set; }
        public decimal valortribestado { get; set; }  
        public decimal TotalDescontoSuframa { get; set; }
        public int TipoFrete { get; set; }
        public string PlacaVeiculo { get; set; }
        public int  idUfVeiculo { get; set; }
        public string Volumes { get; set; }
        public string Especie { get; set; }
        public string Marca { get; set; }
        public decimal PesoBruto { get; set; }
        public decimal PesoLiquido { get; set; }
        public string InscricaoSuframa { get; set; }
        public string IdsNfe { get; set; }
        public int? idUfEmbarque { get; set; }
        public string LocalEmbarque { get; set; }
        public decimal ValorDinheiro { get; set; }
        public decimal ValorCartaoCredito { get; set; }
        public decimal ValorCartaoDebito { get; set; }
        public decimal ValorCheque { get; set; }
        public int TotalmenteDevolvida { get; set; }  //0=> não 1=> sim
        public int PossuiDevolucao { get; set; }  //0=> não 1=> sim
        public int? IdAntigo { get; set; }
        public int ValidadoParaEmissao { get; set; }
        public string NumPedido { get; set; }
        public decimal ComissaoVendedor { get; set; }
        public decimal ComissaoVendedor2 { get; set; }
        public string IdsCreditos { get; set; }
        public int Denegada { get; set; } //0=> não 1=> sim
        public decimal VrIpiDevolvido { get; set; }
        public decimal ValorIpi { get; set; }
        public string PedidosIds { get; set; }
        public string PedidosRefs { get; set; }
        public int OpcaoTabelaPreco { get; set; }
        public decimal PercentAval { get; set; }
        public decimal ValorAval { get; set; }
        public bool avalRateado { get; set; }
        public decimal DescNfce { get; set; }
        public int EmpresaTrocada { get; set; }
        public string NomeEmpresaTrocada { get; set; }
        /// <summary>
        /// Id do tipo de negócio - Utilizado pela dechelles
        /// </summary>
        public int? TipoNegocioId { get; set; }

        [NaoMapeado]
        public IEnumerable<FatNfeItens> ItensNota { get; set; }

        [NaoMapeado]
        public IEnumerable<MovimentacaoEstoque> ItensMovimentacaoEstoque { get; set; }
        
        [NaoMapeado]
        public IEnumerable<ContasReceber> ParcelasCtr { get; set; }
    }
}