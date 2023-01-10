using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroOrdemProducao
    {
        public List<int> OrdensProducao { get; set; }
        public List<int> Produtos { get; set; }
        public List<int> Cores { get; set; }
        public List<int> Tamanhos { get; set; }
        public List<int> Grupo { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Catalogo { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Setor { get; set; }
        public List<int> Almoxarifado { get; set; }

        public string DaEmissao { get; set; }
        public string AteEmissao  { get; set; }
        public string DoAno { get; set; }
        public string AteAno { get; set; }
        public string Ordenar { get; set; }
        public string Agrupar { get; set; }

        public int Exibir { get; set; }
        public int Relatorio { get; set; }

        public bool NaoLiberada { get; set; }
        public bool Liberada { get; set; }
        public bool Finalizada { get; set; }  
        public bool NotaEntradaFaccao { get; set; }
    }
}
