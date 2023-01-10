using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class PacoteProducaoHeaderView
    {
        [Vestillo.NaoMapeado]
        public DateTime Emissao { get; set; }
        [Vestillo.NaoMapeado]
        public DateTime PrevisaoEntrada { get; set; }
        [Vestillo.NaoMapeado]
        public string OrdemProducaoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public List<PacoteProducaoView> Pacotes 
        { 
            get
            {
                List<PacoteProducaoView> pacotes = new List<PacoteProducaoView>();
                PacotesSubHeader.ForEach(p =>
                {
                    pacotes.AddRange(p.Pacotes);
                });
                return pacotes;
            }
        }
        public List<PacoteProducaoSubHeaderView> PacotesSubHeader { get; set; }
        public decimal Quantidade
        {
            get
            {
                return Pacotes.Count;
            }
        }
    }
}
