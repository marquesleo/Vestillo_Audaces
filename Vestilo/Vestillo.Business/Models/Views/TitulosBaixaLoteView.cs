using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class TitulosBaixaLoteView
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
        public string NumTitulo { get; set; }
        public string Prefixo { get; set; }
        public string Parcela { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }
        public decimal Saldo { get; set; }
        public string RefColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public string TipoCobrancaDescricao { get; set; }
        public string NomeBanco { get; set; }
        public int Status { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? ValorPago { get; set; }
        public int BancoMovimentacaoId { get; set; }
        public int? ColaboradorId { get; set; }
        public int BaixaId { get; set; }

        [NaoMapeado]
        public string DescStatus
        {
            get
            {
                return ContasPagarView.GetDescStatus(Status);
            }
        }
    }
}
