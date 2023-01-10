using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ProdutoMaterial", "Produto X Material")]
    public class ProdutoMaterial
    {
        [Chave]
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdMaterial { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Custo { get; set; }
    }
}
