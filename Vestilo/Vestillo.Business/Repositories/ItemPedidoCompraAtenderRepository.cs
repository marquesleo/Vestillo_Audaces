
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ItemPedidoCompraAtenderRepository: GenericRepository<ItemPedidoCompraAtender>
    {
        public ItemPedidoCompraAtenderRepository() : base(new DapperConnection<ItemPedidoCompraAtender>())
        {
        }
              

        public IEnumerable<ItemPedidoCompraAtenderView> GetByItensPorFornecedor(int fornecedorId,int JaSelecionado)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("I.ProdutoId AS iditem,");
            SQL.AppendLine("I.TamanhoId AS IdTamanho,");
            SQL.AppendLine("I.CorId AS IdCor,");
            SQL.AppendLine("I.Id AS ItemPedidoId,");
            SQL.AppendLine("(I.Qtd - I.QtdAtendida) as QtdAtender,");           
            SQL.AppendLine("PED.AlmoxarifadoId,");
            SQL.AppendLine("P.Referencia AS RefProduto,");
            SQL.AppendLine("P.Descricao AS DescProduto,");
            SQL.AppendLine("T.Descricao AS DescTamanho,");
            SQL.AppendLine("C.Descricao AS DescCor,");
            SQL.AppendLine("PED.FornecedorId AS IdFornecedor,");
            SQL.AppendLine("PED.referencia as RefPedido,");
            SQL.AppendLine("PED.Id as PedidoCompraId,");            
            SQL.AppendLine(" PED.dataemissao as DataEmissao,");                  
            SQL.AppendLine(" I.TipoMovimentacaoId as TipoMovimentacaoId,");  
            SQL.AppendLine("tm.referencia as RefTipoMovimentacao,");
            SQL.AppendLine("tm.descricao as DescricaoTipoMovimentacao ");            
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("INNER JOIN pedidocompra PED ON PED.Id = I.PedidoCompraId");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN TipoMovimentacoes tm ON tm.id = I.TipoMovimentacaoId");
            SQL.AppendLine("WHERE	");            
            SQL.AppendLine(" PED.FornecedorId = ");
            SQL.Append(fornecedorId);
            if (JaSelecionado == 1)
            {
                SQL.AppendLine(" AND  I.Selecionado <> 1 ");
                
            }


            SQL.Append(" AND (I.Qtd - I.QtdAtendida) > 0 ");
            SQL.Append(" AND (PED.Status = 1 OR PED.Status = 2) ");   
            SQL.AppendLine("ORDER BY PED.dataemissao, PED.Referencia ");
            var cn = new DapperConnection<ItemPedidoCompraAtenderView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoCompraAtenderView(), SQL.ToString());
        }


        public ItemPedidoCompraAtender GetByPedidoDeCompraDoItem(int IdDoItemNoPedido)
        {
            var cn = new DapperConnection<ItemPedidoCompraAtender>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM itenspedidocompra where Id = ");
            SQL.Append(IdDoItemNoPedido);

            ItemPedidoCompraAtender ret = new ItemPedidoCompraAtender();

            cn.ExecuteToModel(ref ret, SQL.ToString());

           

            return ret;
        }

        public IEnumerable<ItemPedidoCompraAtender> GetByPedidoCompra(int pedidoCompraId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM itenspedidocompra where pedidocompraid = ");
            SQL.Append(pedidoCompraId);

            var cn = new DapperConnection<ItemPedidoCompraAtender>();
            return cn.ExecuteStringSqlToList(new ItemPedidoCompraAtender(), SQL.ToString());

          
        }


        public void UpdateJaSelecionado(int IdPedido, int IdItem, int IdCor, int IdTamanho, int ItemPedidoId,int SimNao)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine(" UPDATE itenspedidocompra SET ");
            SQL.AppendLine(" Selecionado =  ");
            SQL.Append(SimNao);
            SQL.AppendLine(" WHERE PedidoCompraId = ");
            SQL.Append(IdPedido);
            SQL.AppendLine(" AND  ProdutoId = ");
            SQL.Append(IdItem);
            if (IdCor != 0)
            {
                SQL.AppendLine(" AND  CorId = ");
                SQL.Append(IdCor);
            }
            if (IdTamanho != 0)
            {
                SQL.AppendLine(" AND  TamanhoId = ");
                SQL.Append(IdTamanho);
            }            
            SQL.AppendLine(" AND  Id = ");
            SQL.Append(ItemPedidoId);

            _cn.ExecuteNonQuery(SQL.ToString());
        }



       
    }
}
