using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class PacoteProducaoSubHeaderView
    {
        [Vestillo.NaoMapeado]
        public int ItemOrdemProducao { get; set; }
        [Vestillo.NaoMapeado]
        public string OrdemProducaoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public int QtdPacotes
        {
            get
            {
                return Pacotes.Count;
            }
        }
        [Vestillo.NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public int TamanhoId { get; set; }
        [Vestillo.NaoMapeado]
        public string CorDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Quantidade { get; set; }
        [Vestillo.NaoMapeado]
        public decimal QuantidadeTotal
        {
            get
            {
                return Quantidade * QtdPacotes;
            }
        }
        [Vestillo.NaoMapeado]
        public decimal QuantidadeOP { get; set; }
        [Vestillo.NaoMapeado]
        public List<PacoteProducaoView> Pacotes { get; set; }
    }
}
