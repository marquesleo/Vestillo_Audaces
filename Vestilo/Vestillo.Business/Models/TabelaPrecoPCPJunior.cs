using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TabelaPrecoPCPJunior", "Tabela de Preco")]
    public class TabelaPrecoPCPJunior
    {
        [Chave]
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public decimal Lucro{ get; set; }
        public decimal Comissao { get; set; }
        public decimal Tabela { get; set; }
        public decimal Simples { get; set; }
    }
}
