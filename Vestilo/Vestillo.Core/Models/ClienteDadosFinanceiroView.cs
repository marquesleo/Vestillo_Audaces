using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class ClienteDadosFinanceiroView
    {
        public decimal? TotalCompra { get; set; }
        public decimal? MaiorCompra { get; set; }
        public DateTime? UltimaCompra { get; set; }
        public DateTime? DataPrimeiraCompra { get; set; }
        public decimal? SaldoDevedor { get; set; }
        public decimal? TotalPago { get; set; }

        public IEnumerable<Titulos> PagamentosEmAberto { get; set; }
        public IEnumerable<Parcelas> ParcelasEmAberto { get; set; }
        public IEnumerable<Parcelas> ParcelasPagas { get; set; }
        public IEnumerable<ChequeCliente> Cheques { get; set; }

        public class Titulos
        {
            public string TipoDocumento { get; set; }
            public decimal? TotalPago { get; set; }
            public decimal? TotalAberto { get; set; } 
        }

        public class Parcelas
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Prefixo { get; set; }
            public string Parcela { get; set; }
            public string TipoDocumento { get; set; }
            public DateTime? Vencimento { get; set; }
            public DateTime? Emissao { get; set; }
            public DateTime? Pagamento { get; set; }
            public decimal? ValorPago { get; set; }
            public decimal? ValorParcela { get; set; }
            public decimal? Saldo { get; set; }
            public string Situacao { get; set; }
            public int DiasAtraso { get; set; }
        }

        public class ChequeCliente
        {
            public int Id { get; set; }
            public string NumeroCheque { get; set; }
            public decimal Valor { get; set; }
            public DateTime DataVencimento { get; set; }
            public int Status { get; set; }
            public string DescStatus
            {
                get
                {
                    switch (Status)
                    {
                        case 1:
                            return "Incluído";
                        case 2:
                            return "Compensado";
                        case 3:
                            return "Devolvido";
                        case 4:
                            return "Prorrogado";
                        case 5:
                            return "Resgatado";
                        default:
                            return "";
                    }
                }
            }
        }

    }
}
