using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("creditoscliente", "Créditos Cliente")]
    public class CreditosClientes
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }        
        /// <summary>
        /// 1 = Aberto | 2 = Quitado
        /// </summary>
        public int Status { get; set; }        
        public DateTime dataemissao { get; set; }
        public DateTime? dataquitacao { get; set; }
        public decimal valor { get; set; }
        public int? idNotaFat { get; set; }
        public int? idnotaconsumidor { get; set; }
        public int? idDevolucaoItens { get; set; }   
        public int idcolaborador { get; set; }     
        public bool Ativo { get; set; }
        public string ObsInclusao { get; set; }
        public string ObsQuitacao { get; set; }
        public int? ContasReceberBaixaId { get; set; }
        public int? GeradoPeloContasReceber { get; set; }
        public int? ContasReceberQueGerouCreditoId { get; set; }
        public int InclusaoManual { get; set; }
        public string ObsGeral { get; set; }
        public int? IdNfceQuitado { get; set; }
    }
}
