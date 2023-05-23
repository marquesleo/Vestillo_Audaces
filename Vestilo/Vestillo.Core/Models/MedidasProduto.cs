
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("MedidasProduto")]
    public class MedidasProduto : IModel
    {
        [Chave]
        public int Id { get; set; }
        public int DescricaoMedidaId { get; set; }
        public int ProdutoId { get; set; }
        public int TamanhoId { get; set; }
        public decimal Medida { get; set; }
        public string Tolerancia { get; set; }        
    }
}
