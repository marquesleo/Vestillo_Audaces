using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroControlePacote
    {
        public List<int> Ordem { get; set; }
        public List<int> Pacotes { get; set; }
        public List<int> Produtos { get; set; }
        public string DaEmissao { get; set; }
        public string AteEmissao { get; set; }
        public bool Concluido { get; set; }
        public bool Aberto { get; set; }
        public bool Producao { get; set; }
        public bool Finalizado { get; set; }
    }
}
