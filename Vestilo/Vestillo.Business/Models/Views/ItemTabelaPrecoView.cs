using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ItemTabelaPrecoView
    {
        public int Id { get; set; }
        public int TabelaPrecoId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public decimal CustoMedio { get; set; }
        public decimal PrecoSugerido { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal Lucro { get; set; }
        public decimal ProdutoPrecoCompra { get; set; }
        public decimal ProdutoIpi { get; set; }
        public decimal ProdutoIcms { get; set; }
        public decimal ProdutoLucro { get; set; }
        public int ProdutoTipoCustoFornecedor { get; set; }
        public int? ProdutoTipoCalculoPreco { get; set; }
        public string Cor { get; set; }
        public int CorId { get; set; }
        public string Tamanho { get; set; }
        public int TamanhoId { get; set; }
        public decimal Estoque { get; set; }
        public byte[] imagem { get; set; }
        public string Colecao { get; set; }
        public string Catalogo { get; set; }
        public string UM { get; set; }
        public int Inutilizado { get; set; }
      
        public ItemTabelaPreco ToItemTabelaPreco
        {
            get
            {
                var item = new ItemTabelaPreco();
                item.Id = this.Id;
                item.ProdutoId = this.ProdutoId;
                item.TabelaPrecoId = this.TabelaPrecoId;
                item.CustoMedio = this.CustoMedio;
                item.PrecoSugerido = this.PrecoSugerido;
                item.PrecoVenda = this.PrecoVenda;
                item.Lucro = this.Lucro;
                return item;
            }
        }
    }
}
