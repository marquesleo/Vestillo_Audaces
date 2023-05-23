using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class Titulo : IModel
    {
        public class ClientesTitulo
        {
            public int Id { get; set; }
            [GridColumn("Ref. Cliente")]
            public string RefCliente { get; set; }
            [GridColumn("Cliente")]
            public string NomeCliente { get; set; }
        }

        public enum enumStatus
        {
            Aberto = 1,
            Baixa_Parcial = 2,
            Baixa_Total = 3
        }

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public int Status { get; set; }
        [GridColumn("Titulo")]
        public string NumTitulo { get; set; }
        [GridColumn("Parcela")]
        public string Parcela { get; set; }
        [GridColumn("Prefixo")]
        public string Prefixo { get; set; }
        [GridColumn("Ref. Cliente")]
        public string RefCliente { get; set; }
        [GridColumn("Cliente")]
        public string NomeCliente { get; set; }
        [GridColumn("Data Emissão")]
        public DateTime DataEmissao { get; set; }
        [GridColumn("Data Vencimento")]
        public DateTime DataVencimento { get; set; }
        [GridColumn("Valor")]
        public decimal ValorParcela { get; set; }
        [GridColumn("Valor Pago")]
        public decimal ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }
        
        public decimal Saldo { get; set; }
        public int? IdFatNfe { get; set; }
        public int? IdNotaConsumidor { get; set; }
        public int? IdPedidoVenda { get; set; }
        public int? IdCliente { get; set; }
        public int? IdNaturezaFinanceira { get; set; }
        public int? IdBanco { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdVendedor { get; set; }
        public int? IdTipoCobranca { get; set; }
        public int Ativo { get; set; }
        [GridColumn("Desconto")]
        public decimal Desconto { get; set; }
        [GridColumn("Juros")]
        public decimal Juros { get; set; }
        public decimal ValorPagoDia { get; set; }
        [GridColumn("Nota Fiscal")]
        public string NotaFiscal { get; set; }
        [GridColumn("Débito Antigo")]
        public string DebitoAntigo { get; set; }
        [GridColumn("Observação")]
        public string Obs { get; set; }
        [GridColumn("Possui Atividade Cobrança")]
        public string SimNao { get; set; }
        public int? IdTituloPai { get; set; }
        public string SisCob { get; set; }
        public string NumeroCartao { get; set; }
        public int? IdAdmCartao { get; set; }

        public decimal ValorRestante
        {
            get
            {
                return ((ValorParcela + Juros - Desconto) - ValorPago);
            }
        }

        public string DescStatus
        {
            get
            {
                switch (Status)
                {
                    case (int)enumStatus.Aberto:
                        return "Aberto";
                    case (int)enumStatus.Baixa_Parcial:
                        return "Baixa Parcial";
                    case (int)enumStatus.Baixa_Total:
                        return "Baixado";
                    default:
                        if (ValorPago == 0)
                            return "Aberto";

                        if (ValorRestante > 0)
                            return "Baixa Parcial";

                        if (ValorRestante == 0 || (ValorPago > (ValorParcela + Juros - Desconto)))
                            return "Baixado";

                        return "";
                }
            }
        }
    }
}
