using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class OrdemProducaoStatusRel : ItemOrdemProducao
    {

        public int OrdemProducaoId { get; set; }
        public int ProdutoId { get; set; }
        public int? CorId { get; set; }
        public int? TamanhoId { get; set; }
        public decimal Tempo { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QuantidadeAtendida { get; set; }
        public decimal QuantidadeProduzida { get; set; }

        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public string TamanhoDescricao { get; set; }
        public string CorDescricao { get; set; }
        public string ReferenciaPedidoVenda { get; set; }
        public int LiberacaoItemId { get; set; }
        public string OrdemProducaoReferencia { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Corte { get; set; }
        public string PedidoReferencia { get; set; }
        public DateTime? Liberacao { get; set; }
        public DateTime? Pacote { get; set; }
        public DateTime? Producao { get; set; }
        public DateTime? Estoque { get; set; }
        public DateTime? DataSetor { get; set; }
        public int Setor { get; set; }

        public string SetorAbreviatura { get; set; }
        public string SetorDescricao { get; set; }

        public decimal QtdNaoLiberada { get; set; }
        public decimal QtdLiberada { get; set; }
        public decimal QtdPacote { get; set; }
        public decimal QtdProducao { get; set; }
        public decimal QtdEstoque { get; set; }
        public decimal QtdDefeito { get; set; }
    }
}
