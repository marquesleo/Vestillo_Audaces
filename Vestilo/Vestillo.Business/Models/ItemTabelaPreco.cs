using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ItensTabelaPreco", "ItensTabelaPreco")]
    public class ItemTabelaPreco
    {
        [Chave]
        public int Id { get; set; }
        public int TabelaPrecoId { get; set; }
        public int ProdutoId { get; set; }
        public decimal CustoMedio { get; set; }
        public decimal PrecoSugerido { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal Lucro { get; set; }
    }
}
