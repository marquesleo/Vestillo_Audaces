using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BalancoEstoqueItens", "BalancoEstoqueItens")]
    public class BalancoEstoqueItens
    {
        [Chave]
        public int Id { get; set; }
        public int BalancoEstoqueId { get; set; }
        public int ProdutoId { get; set; }
        public int? CorId { get; set; }
        public int? TamanhoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Divergencia { get; set; }
        public int? CatalogoId { get; set; }
        public int? ColecaoId { get; set; }
    }
}
