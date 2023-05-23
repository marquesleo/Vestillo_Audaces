using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("MetaVendedores")]
    public class VendedorMetaView : VendedorMeta
    {
        [NaoMapeado]
        public string NomeMes
        {
            get
            {
                return Mes.ToString();
            }
        }
    }
}
