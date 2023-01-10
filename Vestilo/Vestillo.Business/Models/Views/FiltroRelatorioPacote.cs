using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelatorioPacote
    {
        public List<int> OrdensProducao { get; set; }
        public List<int> Produtos { get; set; }
        public List<int> Cores { get; set; }
        public List<int> Tamanhos { get; set; }
        public List<int> Pacote { get; set; }

        public string DaEmissao { get; set; }
        public string AteEmissao { get; set; }
        public string DaEntrada { get; set; }
        public string AteEntrada { get; set; }
        public string DaSaida { get; set; }
        public string AteSaida { get; set; }
        public string Ordenar { get; set; }

        public bool Aberto { get; set; }
        public bool Producao { get; set; }
        public bool Concluido { get; set; }
        public bool Finalizado { get; set; }  
        public bool RetiraPacotesFaccao { get; set;}
        public bool Agrupado {get; set;}
    }
}
