using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BalanceamentoProduto", "Balanceamento de Produto")]
    public class BalanceamentoProduto
    {
        [Chave]
        public int Id { get; set; }
        public int BalanceamentoId { get; set; }
        public int SetorId { get; set; }
        public int ProdutoId { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? Tempo { get; set; }
        public decimal? TempoPacote { get; set; }
    }
}
