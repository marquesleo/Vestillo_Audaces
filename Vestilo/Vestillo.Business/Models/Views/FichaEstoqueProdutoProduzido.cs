using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FichaEstoqueProdutoProduzido
    {
        public List<int> Produtos { get; set; }
        public List<int> Colecao { get; set; }
        public List<int> Segmento { get; set; }
        public List<int> Cor { get; set; }
        public List<int> Tamanho { get; set; }
        public List<int> Almoxarifado { get; set; }
        public List<int> Grupo { get; set; }
        public int Tabela { get; set; }
        public string DoAno { get; set; }
        public string AteAno { get; set; }
        public string Agrupar { get; set; }
        public bool Valorizar { get; set; }
        public bool DiferenteDeZero { get; set; }
        public bool SemLiberacao { get; set; }
        public bool MenorIgualZero { get; set; }
        public bool ExibirInutilizados { get; set; }
    }
}
