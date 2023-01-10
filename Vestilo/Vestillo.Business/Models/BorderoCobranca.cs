using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BorderoCobranca", "Bordero")]
    public class BorderoCobranca
    {
        public enum StatusBordero
        {
            Incluido = 1,
            Baixado = 2
        }

        [NaoMapeado]
        public string StatusDescricao
        {
            get
            {
                switch (Status)
                {
                    case StatusBordero.Incluido:
                        return "Incluído";
                    case StatusBordero.Baixado:
                        return "Baixado";
                    default:
                        return "";
                }
            }
        }

        [Chave]
        public int Id { get; set; }
        [Contador("BorderoCobranca")]
        [RegistroUnico]
        public string Referencia { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public StatusBordero Status { get; set; }
        public int BancoId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataPrevisaoAcerto { get; set; }
        public DateTime? DataAcerto { get; set; }
        public decimal TaxaBanco { get; set; }
        public decimal ValorBoleto { get; set; }
        public int? CobradorId { get; set; }
        public string Observacao { get; set; }
        public int NaturezaFinanceiraId { get; set; }
        public int TipoCobranca { get; set; }
        public decimal ValorReceber { get; set; }

        [NaoMapeado]
        public List<BorderoCobrancaDocumento> Documentos { get; set; }
    }

    [Tabela("BorderoCobrancaDocumentos")]
    public class BorderoCobrancaDocumento
    {
        [Chave]
        public int Id { get; set; }
        public int BorderoCobrancaId { get; set; }
        public int? ContasReceberId { get; set; }
        public int? ChequeId { get; set; }
    }
}
