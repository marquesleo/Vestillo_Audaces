using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ProdutoFornecedorPrecoView
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdFornecedor { get; set; }
        public string RefFornecedor {get; set; }
        public string NomeFornecedor { get; set; }
        public decimal PrecoFornecedor { get; set; }
        public decimal MediaPrecoFornecedor { get; set; }
        public int? IdCor { get; set; }
        public string NomeCor { get; set; }
        public decimal PrecoCor { get; set; }
        public int? IdTamanho { get; set; }
        public string NomeTamanho { get; set; }
        public decimal PrecoTamanho { get; set; }
        public int TipoCalculoPreco { get; set; }
        public int TipoCustoFornecedor { get; set; }

    }
}
