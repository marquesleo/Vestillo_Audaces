using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FichaTecnicaRelatorioFiltro
    {
        public List<int> Produtos { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Catalogo { get; set; }
        public string DoAno { get; set; }
        public string AteAno { get; set; }

        public bool ImagemProduto { get; set; }
        public bool DetalhamentoOperacao { get; set; }
        public bool ImprimeMateria { get; set; }
        public bool SomenteMateria { get; set; }
        public bool ImprimeListagem { get; set; }
    }
}
