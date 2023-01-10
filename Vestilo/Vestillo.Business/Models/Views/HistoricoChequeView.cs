using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("HistoricoChequeView")]
    public class HistoricoChequeView : HistoricoCheque
    {
        [NaoMapeado]
        public string NomeUsuario { get; set; }
        [NaoMapeado]
        public string DescricaoStatus 
        {
            get
            {
                switch ((Cheque.enumStatus)Status)
                {
                    case  Cheque.enumStatus.Incluido:
                        return "Incluído";
                    case Cheque.enumStatus.Compensado:
                        return "Compensado";
                    case Cheque.enumStatus.Prorrogado:
                        return "Prorrogado";
                    case Cheque.enumStatus.Devolvido:
                        return "Devolvido";
                    case Cheque.enumStatus.Resgatado:
                        return "Resgatado";
                    default:
                        return "";
                }
            }
        }
    }
}
