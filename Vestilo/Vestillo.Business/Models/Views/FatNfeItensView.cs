using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FatNfeItensView 
    {
        public int Id { get; set; }
        public int IdNfe { get; set; }
        public int IdTipoMov { get; set; }
        public int IdItemNoPedido { get; set; } 
        public int NumItem { get; set; }
        public int CalculaIcms { get; set; }
        public int IntegraIpi { get; set; }
        public int iditem { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public string codbarrasgrade { get; set;}
        public string codbarrasunico { get; set; }
        public string ncm { get; set;}
        public string cest { get; set; }
        public string draw { get; set; }
        public int origem { get; set; }
        public string unidade { get; set; }
        public string cst { get; set; }
        public string fci { get; set; }
        public string parcelaimp { get; set; }
        public string ci { get; set; }
        public string RefCfop { get; set; }
        public string DescCfop { get; set; }
        public string referencia { get; set; }
        public string referenciacli { get; set; }
        public string referenciagradecli { get; set;}
        public string RefProduto { get; set; }
        public string DescProduto { get; set; }
        public string NumLinha { get; set; }
        public string DescCor { get; set; }
        public string DescTamanho { get; set; }
        public string RefCor { get; set; }
        public string RefTamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Qtddevolvida { get; set; }
        public decimal QtdAcimaDaOp { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
        public decimal PercentIpi { get; set; }
        public decimal BaseIpi { get; set; }
        public decimal ValorIpi { get; set; }
        public decimal DescontoBonificado { get; set; }
        public decimal DescontoPis { get; set; }
        public decimal DescontoCofins { get; set; }
        public decimal DescontoSefaz { get; set; }
        public decimal DescontoRateado { get; set; }
        public decimal BaseIcmsRateado { get; set; }
        public decimal Percenticms { get; set; }
        public decimal ValorIcmsRateado { get; set; }
        public decimal DespesaRateio { get; set; }
        public decimal FreteRateio { get; set; }
        public decimal SeguroRateio { get; set; }
        public decimal percentualreducaoicms { get; set;}
        public decimal percentualDiferimento { get; set; }
        public decimal percentualDiferimentoFcp { get; set; }
        public int csosn { get; set; }
        public int Desoneracao { get; set; }
        public string CodBeneficio { get; set; }
        
        public decimal creditoicms {get; set;}
        public string  enquadracofins { get; set; }
        public string enquadrapis { get; set; }

        public string cstipi { get; set; }
        public string enquadraipi { get; set; }

        public decimal icmsintestadual { get; set; }
        public decimal icmsestadodestino { get; set; }
        public decimal icmsdiferenca { get; set; }
        public decimal partemissor { get; set; }
        public decimal partdest { get; set; }
        public decimal alqfcp { get; set; }
        public decimal valorbcicmsdest { get; set; }
        public decimal valoricmsintestadual { get; set; }
        public decimal valoricmsdiferenca { get; set; }
        public decimal valorpartemissor { get; set; }
        public decimal valorpartdest { get; set; }
        public decimal basefcpestado { get; set; } // Novo 4.0
        public decimal Valorfcpestado { get; set; } // Novo 4.0
        public decimal valorfcp { get; set; }
        public string RefPedidoCliente { get; set; }
        public string SeqPedCliente { get; set; }

        public decimal PercDevolvido { get; set; } // Novo 4.0
        public decimal VrIpiDevolvido { get; set; }

        public bool Salvo { get; set; }
            
    }
}
