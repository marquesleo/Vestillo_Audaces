using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OperacaoFaccao", "OperacaoFaccao")]
    public class OperacaoFaccaoView : OperacaoFaccao
    {
        [Vestillo.NaoMapeado]
        public string FaccaoNome { get; set; }
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
