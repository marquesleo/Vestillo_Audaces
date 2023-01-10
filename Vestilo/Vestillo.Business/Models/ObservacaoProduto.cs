using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Tabela("ObservacaoProduto", "Observação Relacionada ao Produto")]
    public class ObservacaoProduto
    {       
        [Chave]
        public int Id { get; set; }        
        public int ProdutoId { get; set; }
        public string Observacao { get; set; }

    }
}
