using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroEstoqueRelatorio
    {
       
        public List<int> Produtos { get; set; }
        public List<int> Cor { get; set; }
        public List<int> Tamanho { get; set; }
        public List<int> Almoxarifado { get; set; }

        public List<int> Grupo { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Catalogo { get; set; }

        public int Tipo { get; set; }
        public bool DiferenteZero { get; set; }
        public bool SoEstoque { get; set; }
        public bool ExibirInutilizados { get; set; }

        public int Agrupar { get; set; }
    }
}
