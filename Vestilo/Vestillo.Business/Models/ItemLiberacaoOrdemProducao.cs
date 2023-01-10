using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
     [Vestillo.Tabela("ItemLiberacaoOrdemProducao", "Item Liberaca Ordem de Produção")]
    public class ItemLiberacaoOrdemProducao
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int OrdemProducaoId { get; set; }
        public int ItemOrdemProducaoId { get; set; }
        public int ProdutoId { get; set; }
        public int? CorId { get; set; }
        public int? TamanhoId { get; set; }
        public decimal Quantidade { get; set; }
    }
}
