using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroFichaTecnica
    {
        public List<int> Produtos { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Catalogo { get; set; }
        public List<int> Grupo { get; set; }

        public int Ordenar { get; set; }

        public string DoAno { get; set; }
        public string AteAno { get; set; }

        public bool ImprimeImagem { get; set; }
        public bool DetalhamentoOperacoes { get; set; }
        public bool Listagem { get; set; }
        public bool ImprimeMateria { get; set; }
        public bool SomenteMateria { get; set; }
        public bool OcultaPreco { get; set; }
        public bool OcultaTempo { get; set; }
        public bool UsaCustoFaccao { get; set; }

    }
}
