
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("NfeItens", "NfeItens")]
    public class FatNfeItens
    {
        [Chave]
        public int Id { get; set; }
        public int IdNfe { get; set; }
        public int IdTipoMov { get; set; }
        public int NumItem { get; set; }
        public int CalculaIcms { get; set; }
        public int IntegraIpi { get; set; }
        public int? IdItemNoPedido { get; set; }
        public int iditem { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Qtddevolvida { get; set; }
        public decimal QtdAcimaDaOp { get; set; }
        public decimal Preco { get; set; }
        public decimal total { get; set; }
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
        public decimal icmsintestadual  { get; set; }      
        public decimal icmsestadodestino  { get; set; }   
        public decimal icmsdiferenca   { get; set; }      
        public decimal partemissor  { get; set; }        
        public decimal partdest  { get; set; }           
        public decimal alqfcp  { get; set; }            
        public decimal valorbcicmsdest  { get; set; }     
        public decimal valoricmsintestadual  { get; set; }
        public decimal valoricmsdiferenca  { get; set; }  
        public decimal valorpartemissor   { get; set; }   
        public decimal valorpartdest   { get; set; }
        public decimal basefcpestado { get; set; } // Novo 4.0
        public decimal Valorfcpestado { get; set; } // Novo 4.0
        public decimal valorfcp { get; set; }
        public string RefPedidoCliente { get; set; }
        public string SeqPedCliente { get; set; }

        public decimal PercDevolvido { get; set; } // Novo 4.0
        public decimal VrIpiDevolvido { get; set; }


    }
}