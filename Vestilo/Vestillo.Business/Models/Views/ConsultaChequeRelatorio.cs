using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Business.Models
{
    public class ConsultaChequeRelatorio
    {
        
        public int Tipo { get; set; }

        [GridColumn("Tipo")]
        public string DescTipo
        {
            get
            {
                switch (Tipo)
                {
                    case 1:
                        return "Cliente";
                    case 2:
                        return "Empresa";
                    default:
                        return "Não Informado";
                }
            }
        }

        public int Status { get; set; }

        [GridColumn("Status")]
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
                        return "Não Informado";
                }
            }
        }

        [GridColumn("Banco")]
        public string BancoCheque { get; set; }
        [GridColumn("Agencia")]
        public string AgenciaCheque { get; set; }
        [GridColumn("Conta")]
        public string ContaCheque { get; set; }
        [GridColumn("Número")]
        public string Numero { get; set; }
        [GridColumn("Valor(R$)")]
        public Decimal Valor { get; set; }
        [GridColumn("Emissão")]
        public DateTime Emissao { get; set; }
        [GridColumn("Vencimento")]
        public DateTime Vencimento { get; set; }
        [GridColumn("Compensação")]
        public DateTime? Compensacao { get; set; }
        [GridColumn("Nome do Cliente")]
        public string NomeCliente { get; set; }

        public int DeTerceiro { get; set; }

        [GridColumn("Cheque de 3º")]
        public string DescTerceiro
        {
            get
            {
                switch (DeTerceiro)
                {
                    case 0:
                        return "Não";
                    case 1:
                        return "Sim";
                    default:
                        return "Não Informado";
                }
            }
        }

        [GridColumn("Banco Movimentação")]
        public int IdBanco { get; set; }
        [GridColumn("Descrição Banco")]
        public string BancoDescricao { get; set; }
        [GridColumn("Agência Movimentação")]
        public string BancoAgencia { get; set; }
        [GridColumn("Conta Movimentação")]
        public string BancoConta { get; set; }
        [GridColumn("Utilizado Em")]
        public string UtilizadoEm { get; set; }
        [GridColumn("Conta Pagar/Receber")]
        public string TituloUtilizado { get; set; }

    }
}
