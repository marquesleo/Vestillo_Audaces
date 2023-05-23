using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("contasreceber")]
    public class ContasReceberView : ContasReceber
    {
        [NaoMapeado]
        public string TipoCobrancaDescricao {get;set;}
        [NaoMapeado]
        public string TipoDocumento { get; set; }
        [NaoMapeado]
        public string NomeBanco { get; set; }
        [NaoMapeado]
        public string RefCliente { get; set; }
        [NaoMapeado]
        public string RazaoSocialColaborador { get; set; }
        [NaoMapeado]
        public string CNPJCPF { get; set; }
        [NaoMapeado]
        public decimal ValorPagoDinheiro { get; set; }
        [NaoMapeado]
        public decimal ValorPagoCheque { get; set; }
        [NaoMapeado]
        public decimal SaldoDevedor { get; set; }
        [NaoMapeado]
        public int PossuiBoleto { get; set; }
        [NaoMapeado]
        public int BancoPortador { get; set; }
        [NaoMapeado]
        public string SimNao { get; set; }
        [NaoMapeado]
        public int RemessaGerada { get; set; }
        [NaoMapeado]
        public string Cidade { get; set; }
        [NaoMapeado]
        public string Estado { get; set; }
        [NaoMapeado]
        public string Rota { get; set; }


        [NaoMapeado]
        public decimal ValorRestante
        {
            get
            {
                return ((ValorParcela  + Juros - Desconto) - ValorPago );
            }
        }
        [NaoMapeado]
        public string DescStatus
        {
            get
            {
                switch (Status)
                {
                    case (int)enumStatusContasReceber.Aberto:
                        return "Aberto";
                    case (int)enumStatusContasReceber.Baixa_Parcial:
                        return "Baixa Parcial";
                    case (int)enumStatusContasReceber.Baixa_Total:
                        return "Baixado";
                    case (int)enumStatusContasReceber.Negociado:
                        return "Negociado";
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

        [NaoMapeado]
        public bool BoolAtivo
        {
            get
            {
                return this.Ativo > 0;
            }
        }

        public int? IdAdmCartao { get; set; }
        public string Parcela { get; set; }
    }
}
