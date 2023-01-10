using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("creditofornecedor")]
    public class CreditoFornecedor
    {
        public enum StatusCredito
        {
            Indefinido = 0,
            Aberto = 1,
            Quitado = 2
        }

        [Chave]
        public int Id { get; set; }
        [OrderByColumn(true)]
        public DateTime DataEmissao { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int IdFornecedor { get; set; }
        public int? IdDevolucao { get; set; }
        /// <summary>
        /// 1 - Aberto
        /// 2 - Quitado
        /// </summary>
        public StatusCredito Status { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        public DateTime? DataQuitacao { get; set; }
        public string Obs { get; set; }
        public string ObsQuitacao { get; set; }
        /// <summary>
        /// Id da baixa do contas a pagar que o crédito foi utilizado
        /// </summary>
        public int? IdContasPagarBaixa { get; set; }
        /// <summary>
        /// Id da baixa do contas a pagar que gerou o crédito com o fornecedor
        /// </summary>
        public int? IdContasPagarBaixaQueGerou { get; set; }
        public int InclusaoManual { get; set; }
        public string ObsGeral { get; set; }
    }

    public class CreditoFornecedorView     
    {
        public int Id { get; set; }
        public int Status { get; set; }

        public string DescricaoStatus
        {
            get
            {
                if (Status == 1)
                    return "Aberto";
                else if (Status == 2)
                    return "Quitado";
                else
                    return "";
            }
        }
        public string FornecedorReferencia { get; set; }
        public string FornecedorNome { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public int InclusaoManual { get; set; }
        public string ObsGeral { get; set; }

    }
}
