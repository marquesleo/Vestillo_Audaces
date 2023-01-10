using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FichaEstoqueMateriaPrima
    {
        public List<int> Produtos { get; set; }
        public List<int> Cor { get; set; }
        public List<int> Tamanho { get; set; }
        public List<int> Almoxarifado { get; set; }
        public List<int> Segmento { get; set; }
        public string Agrupar { get; set; }
        public bool Valorizar { get; set; }
        public bool DiferenteDeZero { get; set; }
        public int ValorizarPor { get; set; }
        public int Relatorio { get; set; }
        public int OrdenarPor { get; set; }
        public int TipoCalculoCusto { get; set; }
    }
}
