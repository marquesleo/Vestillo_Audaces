using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("contaspagar")]
    public class ContasPagarView : ContasPagar
    {
        [NaoMapeado]
        public string TipoCobrancaDescricao { get; set; }
        [NaoMapeado]
        public string TipoDocumento { get; set; }
        [NaoMapeado]
        public string NomeBanco { get; set; }
        [NaoMapeado]
        public string RefFornecedor { get; set; }
        [NaoMapeado]
        public decimal ValorPagoDinheiro { get; set; }
        [NaoMapeado]
        public decimal ValorPagoCheque { get; set; }
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
                    case (int)enumStatusContasPagar.Aberto:
                        return "Aberto";
                    case (int)enumStatusContasPagar.Baixa_Parcial:
                        return "Baixa Parcial";
                    case (int)enumStatusContasPagar.Baixa_Total:
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

        [NaoMapeado]
        public bool BoolAtivo
        {
            get
            {
                return this.Ativo > 0;
            }
        }

        public static string GetDescStatus(int status)
        {
            switch (status)
            {
                case (int)enumStatusContasPagar.Aberto:
                    return "Aberto";
                case (int)enumStatusContasPagar.Baixa_Parcial:
                    return "Baixa Parcial";
                case (int)enumStatusContasPagar.Baixa_Total:
                    return "Baixado";
                default:
                    return "";
            }   
        }
        [NaoMapeado]
        public string RazaoSocialColaborador { get; set; }
        [NaoMapeado]
        public string CNPJCPF { get; set; }
    }
}
