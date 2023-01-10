using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OperacaoOperadora", "OperacaoOperadora")]
    public class OperacaoOperadoraView : OperacaoOperadora
    {
        [Vestillo.NaoMapeado]
        public string FuncionarioNome { get; set; }
        [Vestillo.NaoMapeado]
        public string OperacaoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Tempo { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Quantidade { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string PacoteReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string PacoteSequencia { get; set; }
    }
}
