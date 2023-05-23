
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("ProdutoFicha")]
    public class ProdutoFicha : IModel
    {
        [Chave]
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int MaterialId { get; set; }
        public decimal quantidade { get; set; }
        public decimal custo { get; set; }
        public decimal preco { get; set; }
        public decimal valor { get; set; }
        public int Numero { get; set; }
    }
}
