using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("produtoFornecedorPrecos", "produtoFornecedorPrecos")]
    public class ProdutoFornecedorPreco
    {

        [Chave]
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdFornecedor { get; set; }
        public decimal PrecoFornecedor { get; set; }
        public int? IdCor { get; set; }
        public int? IdTamanho { get; set; }
        public decimal PrecoCor { get; set; }
        public decimal PrecoTamanho { get; set; }
        
    }
}