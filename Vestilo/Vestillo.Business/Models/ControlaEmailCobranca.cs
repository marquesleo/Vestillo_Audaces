using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("controlaemailcobranca", "controla email cobranca")]
    public class ControlaEmailCobranca
    {
        public enum enumTipo
        {
            AntesDoVencimento = 0,
            NoVencimento = 1,
            AposVencimento = 2,
            AcimaDe30Dias = 3
        }

        [Chave]
        public int Id { get; set; }
        public int Dias { get; set; }
        public int TipoEnvio { get; set; }        
        public string TextoEmail { get; set; }
        public bool Ativo { get; set; }
    }

    public class ControlaEmailCobrancaView
    {
        public int Id { get; set; }
        public int Dias { get; set; }
        public int TipoEnvio { get; set; }

        public string DescricaoTipoEnvio
        {
            get
            {
                if (TipoEnvio == 0)
                    return "Antes Do Vencimento";
                else if (TipoEnvio == 1)
                    return "No Vencimento";
                else if (TipoEnvio == 2)
                    return "Após o Vencimento";
                else if (TipoEnvio == 3)
                    return "Acima de 30 dias do Vencimento";
                else
                    return "";
            }
        }
        public string TextoEmail { get; set; }
        public bool Ativo { get; set; }
    }
}
