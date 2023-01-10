using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class ItemPedidoCompraRepository: GenericRepository<ItemPedidoCompra>
    {
        public ItemPedidoCompraRepository()
            : base(new DapperConnection<ItemPedidoCompra>())
        {
        }

        public IEnumerable<ItemPedidoCompraView> GetByPedido(int pedidoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.*,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao,");
            SQL.AppendLine("T.Abreviatura AS TamanhoAbreviatura,");
            SQL.AppendLine("C.Abreviatura AS CorAbreviatura,");
            SQL.AppendLine("UM.Abreviatura AS UMDescricao,");
            SQL.AppendLine("UM2.Abreviatura AS UMDescricao2,");
            SQL.AppendLine("tm.referencia as RefTipoMovimentacao,");
            SQL.AppendLine("tm.descricao as DescricaoTipoMovimentacao");
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN TipoMovimentacoes tm ON tm.id = I.TipoMovimentacaoId");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas UM ON UM.id = P.IdUniMedida");
            SQL.AppendLine("LEFT JOIN UnidadeMedidas UM2 ON UM2.id = P.IdSegUniMedida");
            SQL.AppendLine("WHERE	I.PedidoCompraId = ");
            SQL.Append(pedidoId);

            var cn = new DapperConnection<ItemPedidoCompraView>();
            return cn.ExecuteStringSqlToList(new ItemPedidoCompraView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoCompraView> GetByMaterialEOrdem(OrdemCompra material, List<int> idsOdens)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.Id, I.PedidoCompraId,");
            SQL.AppendLine("I.TipoMovimentacaoId, I.ProdutoId,");
            SQL.AppendLine("I.TamanhoId, I.CorId,");
            SQL.AppendLine("(I.Qtd) AS Qtd, (I.QtdAtendida) AS QtdAtendida,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao");
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("LEFT JOIN pedidocompraordemproducao PO ON PO.PedidoCompraId = I.PedidoCompraId");
            SQL.AppendLine("WHERE	I.ProdutoId = " + material.MateriaPrimaId);
            SQL.Append(" AND I.CorId = " + material.CorId);
            SQL.Append(" AND I.TamanhoId = " + material.TamanhoId);
            SQL.Append(" AND  PC.Status <> 4 ");

            if (idsOdens != null && idsOdens.Count() > 0)
                SQL.Append(" AND PO.OrdemProducaoId IN (" + string.Join(", ", idsOdens) + ") ");

            SQL.AppendLine(" GROUP BY I.id");

            var cn = new DapperConnection<ItemPedidoCompraView>();

            return cn.ExecuteStringSqlToList(new ItemPedidoCompraView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoCompraView> GetByMaterial(OrdemCompra material)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.Id, I.PedidoCompraId,");
            SQL.AppendLine("I.TipoMovimentacaoId, I.ProdutoId,");
            SQL.AppendLine("I.TamanhoId, I.CorId,");
            SQL.AppendLine("(I.Qtd) AS Qtd, (I.QtdAtendida) AS QtdAtendida,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao");
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("WHERE	I.ProdutoId = " + material.MateriaPrimaId);
            SQL.Append(" AND I.CorId = " + material.CorId);
            SQL.Append(" AND I.TamanhoId = " + material.TamanhoId);
            SQL.Append(" AND I.Qtd - I.QtdAtendida > 0 AND  PC.Status <> 4");
            SQL.AppendLine(" GROUP BY I.id");


            var cn = new DapperConnection<ItemPedidoCompraView>();

            return cn.ExecuteStringSqlToList(new ItemPedidoCompraView(), SQL.ToString());
        }

        public IEnumerable<ItemPedidoCompraView> GetByMaterialGestaoCompra(GestaoOrdemCompra material)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT I.Id, I.PedidoCompraId,");
            SQL.AppendLine("I.TipoMovimentacaoId, I.ProdutoId,");
            SQL.AppendLine("I.TamanhoId, I.CorId,");
            SQL.AppendLine("(I.Qtd) AS Qtd, (I.QtdAtendida) AS QtdAtendida,");
            SQL.AppendLine("P.Descricao AS ProdutoDescricao,");
            SQL.AppendLine("P.Referencia AS ProdutoReferencia,");
            SQL.AppendLine("T.Descricao AS TamanhoDescricao,");
            SQL.AppendLine("C.Descricao AS CorDescricao");
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN pedidoCompra PC ON PC.Id = I.PedidoCompraId");
            SQL.AppendLine("INNER JOIN produtos P ON P.Id = I.ProdutoId	");
            SQL.AppendLine("LEFT JOIN tamanhos T ON T.Id = I.TamanhoId ");
            SQL.AppendLine("LEFT JOIN cores C ON C.Id = I.CorId ");
            SQL.AppendLine("WHERE	I.ProdutoId = " + material.MateriaPrimaId);
            SQL.Append(" AND I.CorId = " + material.CorId);
            SQL.Append(" AND I.TamanhoId = " + material.TamanhoId);
            SQL.Append(" AND I.Qtd - I.QtdAtendida > 0 AND  PC.Status <> 4");
            SQL.AppendLine(" GROUP BY I.id");

            var cn = new DapperConnection<ItemPedidoCompraView>();

            return cn.ExecuteStringSqlToList(new ItemPedidoCompraView(), SQL.ToString());
        }

        public IEnumerable<NotaEntrada> GetByNFe(List<ItemPedidoCompraView> compras)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT NE.Referencia");
            SQL.AppendLine("FROM 	itenspedidoCompra I");
            SQL.AppendLine("INNER JOIN notaentradaitens NEI ON I.Id = NEI.IdItemNoPedido");
            SQL.AppendLine("INNER JOIN notaentrada NE ON NE.Id = NEI.IdNota");
            SQL.AppendLine("WHERE	I.Id IN " );
            SQL.AppendLine(" (" + string.Join(", ", compras.Select(c => c.Id)) + ")");
            SQL.AppendLine("GROUP BY NEI.IdNota");

            var cn = new DapperConnection<NotaEntrada>();

            return cn.ExecuteStringSqlToList(new NotaEntrada(), SQL.ToString());
        }


        public IEnumerable<NotaEntrada> GetByNotaEntrada(int IdMatPrima, int IdCor,int IdTamanho, DateTime DaInclusao, DateTime AteInclusao)
        {
            var Valor = "'" + DaInclusao.ToString("yyyy-MM-dd") + "' AND '" + AteInclusao.ToString("yyyy-MM-dd") + "'";

            var SQL = new StringBuilder();
            SQL.AppendLine("select * FROM notaentradaitens");
            SQL.AppendLine("INNER JOIN notaentrada ON notaentrada.id = notaentradaitens.IdNota");
            SQL.AppendLine("WHERE notaentradaitens.iditem = " + IdMatPrima);
            SQL.AppendLine("AND notaentradaitens.idcor = " + IdCor);
            SQL.AppendLine("AND  notaentradaitens.idtamanho = " + IdTamanho);
            SQL.AppendLine("AND   SUBSTRING(notaentrada.DataInclusao, 1, 10) BETWEEN " + Valor );
            SQL.AppendLine("GROUP BY notaentradaitens.IdNota");
                 

            var cn = new DapperConnection<NotaEntrada>();

            return cn.ExecuteStringSqlToList(new NotaEntrada(), SQL.ToString());
        }

       
    }
}
