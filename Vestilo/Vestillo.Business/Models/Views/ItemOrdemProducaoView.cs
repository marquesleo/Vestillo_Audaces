using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("ItensOrdemProducao")]
    public class ItemOrdemProducaoView : ItemOrdemProducao
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
        public string ReferenciaPedidoVenda { get; set; }
        [Vestillo.NaoMapeado]
        public int LiberacaoItemId { get; set; }
        [Vestillo.NaoMapeado]
        public string OrdemProducaoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public DateTime DataEntrada { get; set; }
        [Vestillo.NaoMapeado]
        public string PedidoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string QtdPacote { get; set; }
        [Vestillo.NaoMapeado]
        public string StatusPacote { get; set; }
        [Vestillo.NaoMapeado]
        public int AlmoxarifadoId { get; set; }
        

    }
}
