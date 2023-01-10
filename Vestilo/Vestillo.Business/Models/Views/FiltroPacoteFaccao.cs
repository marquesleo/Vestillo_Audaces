using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroPacoteFaccao
    {
        public List<int> Faccao { get; set; }
        public List<int> OrdemProducao { get; set; }
        public List<int> Produtos { get; set; }
        public List<int> Cor { get; set; }
        public List<int> Tamanho { get; set; }
        public List<int> Pacote { get; set; }

        public int Ordenar { get; set; }
        public int BuscarPor { get; set; }

        public string DaEmissao { get; set; }
        public string AteEmissao { get; set; }

        public bool Finalizacao { get; set; }
        public string DaFinalizacao { get; set; }
        public string AteFinalizacao { get; set; }


        public bool TotalPecas { get; set; }
        public bool BuscaPreco { get; set; }
    }
}
