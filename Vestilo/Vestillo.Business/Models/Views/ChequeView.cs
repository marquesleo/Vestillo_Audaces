using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Cheques")]
    public class ChequeView : Cheque
    {
        [NaoMapeado]
        public string NumTitulo { get; set; }

        [NaoMapeado]
        public string NomeCliente { get; set; }

        [NaoMapeado]
        public decimal PercentualJuros { get; set; }

        [NaoMapeado]
        public string ObsCompensacao { get; set; }

        [NaoMapeado]
        public string ObsDevolucao { get; set; }

        [NaoMapeado]
        public string TipoEmitenteChequeDesc { get; set; }

        

        [NaoMapeado]
        public IEnumerable<HistoricoChequeView> Historico { get; set; }

        [NaoMapeado]
        public string DescricaoStatus
        {
            get
            {
                switch ((Cheque.enumStatus)Status)
                {
                    case Cheque.enumStatus.Incluido:
                        return "Incluído";
                    case Cheque.enumStatus.Compensado:
                        return "Compensado";
                    case Cheque.enumStatus.Devolvido:
                        return "Devolvido";
                    case Cheque.enumStatus.Prorrogado:
                        return "Prorrogado";
                    case Cheque.enumStatus.Resgatado:
                        return "Resgatado";
                    default:
                        return "";
                }
            }
        }
    }
}
