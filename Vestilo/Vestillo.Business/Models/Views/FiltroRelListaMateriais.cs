using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FiltroRelListaMateriais
    {
        public enum FiltroSaldoGeral
        {
            SemFiltro = 0,
            MaiorZero = 1,
            MenorZero = 2, 
            MenorIgualZero = 3,
            DiferenteZero = 4,
            IgualZero = 5
        }

        public FiltroSaldoGeral Saldo1 { get; set; }
        public FiltroSaldoGeral Saldo2 { get; set; }
        public FiltroSaldoGeral Saldo3 { get; set; }
        public List<int> idsOrdem { get; set; }
        public List<int> idsMateriais { get; set; }
        public List<int> idsCor { get; set; }
        public List<int> idsTamanho { get; set; }
        public int StatusOrdem { get; set; }
        public DateTime DataPedidoCompra { get; set; }
        public int idAlmoxarifado { get; set; }
        public bool TodosMateriais { get; set; }
        public List<int> Grupo { get; set; }
    }
}
