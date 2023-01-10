using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("ItemLiberacaoOrdemProducao")]
    public class ItemLiberacaoOrdemProducaoView : ItemLiberacaoOrdemProducao
    {
        [Vestillo.NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string CorDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string OrdemProducaoReferencia { get; set; }
    }
}
