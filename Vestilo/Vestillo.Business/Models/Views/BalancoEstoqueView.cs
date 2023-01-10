using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BalancoEstoque")]
    public class BalancoEstoqueView : BalancoEstoque
    {
        [NaoMapeado]
        public string AlmoxarifadoDescricao { get; set; }
        [NaoMapeado]
        public string StatusDescricao
        {
            get
            {
                if(base.Status == 1)
                    return "Finalizado";
                
                return "Em Processo";
            }
        }

        [NaoMapeado]
        public string NomeUsuario { get; set; }

    }
}
