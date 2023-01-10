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
    public class BalancoEstoqueRepository : GenericRepository<BalancoEstoque>
    {
        public BalancoEstoqueRepository()
            : base(new DapperConnection<BalancoEstoque>())
        {
        }
        
        public IEnumerable<BalancoEstoqueView> GetBalancoEstoque()
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	B.*, ");
            SQL.AppendLine(" A.Descricao AS AlmoxarifadoDescricao ");
            SQL.AppendLine("FROM 	BalancoEstoque B");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = B.AlmoxarifadoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa("A.IdEmpresa"));
            SQL.AppendLine(" ORDER BY B.DataInicio DESC, A.Descricao, B.Status");

            var cn = new DapperConnection<BalancoEstoqueView>();
            return cn.ExecuteStringSqlToList(new BalancoEstoqueView(), SQL.ToString());
        }

        public IEnumerable<BalancoEstoqueView> GetByAlmoxarifado(int idAlmoxarifado)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	B.*, ");
            SQL.AppendLine(" A.Descricao AS AlmoxarifadoDescricao ");
            SQL.AppendLine("FROM 	BalancoEstoque B");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = B.AlmoxarifadoId");
            SQL.AppendLine("WHERE B.AlmoxarifadoId = " + idAlmoxarifado);
            SQL.AppendLine(" ORDER BY B.DataInicio DESC, A.Descricao, B.Status");

            var cn = new DapperConnection<BalancoEstoqueView>();
            return cn.ExecuteStringSqlToList(new BalancoEstoqueView(), SQL.ToString());
        }

        public BalancoEstoqueView GetByIdView(int idBalanco)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT	B.*, ");
            SQL.AppendLine(" A.Descricao AS AlmoxarifadoDescricao, u.Nome as NomeUsuario ");
            SQL.AppendLine("FROM 	BalancoEstoque B");
            SQL.AppendLine("INNER JOIN Almoxarifados A ON A.Id = B.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN Usuarios u ON B.UserId = u.id");
            SQL.AppendLine("WHERE B.id = " + idBalanco);

            var cn = new DapperConnection<BalancoEstoqueView>();
            BalancoEstoqueView ret = new BalancoEstoqueView();

            cn.ExecuteToModel(ref ret, SQL.ToString());
            return ret;
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaProduto(int almoxarifadoId, int tipoBusca, List<int> id)
        {
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT P.Idcolecao as ColecaoId, P.IdCatalogo as CatalogoId, col.descricao as ColecaoDescricao, cat.descricao as CatalogoDescricao, ");
            SQL.AppendLine(" p.Id AS ProdutoId, p.Referencia AS ProdutoReferencia, p.Descricao AS ProdutoDescricao, ");
            SQL.AppendLine(" c.id AS CorId, c.Abreviatura AS CorAbreviatura, t.id AS TamanhoId, t.Abreviatura AS TamanhoAbreviatura, ");
            SQL.AppendLine(" IFNULL(e.Saldo, 0) AS Saldo, IFNULL(e.Empenhado, 0) AS Empenhado");
            SQL.AppendLine("FROM produtos p ");
            SQL.AppendLine(" LEFT JOIN produtodetalhes pd ON p.id = pd.IdProduto ");
            SQL.AppendLine(" LEFT JOIN colecoes col ON col.id = P.Idcolecao ");
            SQL.AppendLine(" LEFT JOIN catalogo cat ON cat.id = P.IdCatalogo ");
            SQL.AppendLine(" LEFT JOIN cores c ON pd.IdCor = c.id ");
            SQL.AppendLine(" LEFT JOIN tamanhos t ON pd.IdTamanho = t.id ");
            SQL.AppendLine(" LEFT JOIN estoque e ON e.produtoid = p.id AND e.tamanhoid = t.id AND e.corid = c.id AND e.AlmoxarifadoId = " + almoxarifadoId);
            SQL.AppendLine("WHERE ");

            switch (tipoBusca)
            {
                case 0:
                    SQL.AppendLine(" P.Id IN (" + string.Join(",", id.ToArray()) + ")");
                    break;
                case 1:
                    SQL.AppendLine(" P.Idcolecao IN (" + string.Join(",", id.ToArray()) + ")");
                    break;
                case 2:
                    SQL.AppendLine(" P.IdCatalogo IN (" + string.Join(",", id.ToArray()) + ")");
                    break;

            }

            SQL.AppendLine("    AND P.Ativo = 1 ");
            SQL.AppendLine("    AND (IFNULL(C.Id, 0) != 999 AND IFNULL(T.Id, 0) != 999) ");
            SQL.AppendLine("    AND  IFNULL(PD.Inutilizado, 0) = 0 ");

            switch (tipoBusca)
            {
                case 0:
                    SQL.AppendLine(" ORDER BY p.referencia, c.abreviatura, t.id ");
                    break;
                case 1:
                    SQL.AppendLine(" ORDER BY col.Descricao, p.referencia, c.abreviatura, t.id ");
                    break;
                case 2:
                    SQL.AppendLine(" ORDER BY cat.Descricao, p.referencia, c.abreviatura, t.id ");
                    break;

            }
           

            var cn = new DapperConnection<BalancoEstoqueItensView>();
            return cn.ExecuteStringSqlToList(new BalancoEstoqueItensView(), SQL.ToString());
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaByProduto(string busca, bool buscarPorId, int almoxarifadoId)
        {
            var SQL = new StringBuilder();
            var SQLGrade = string.Empty;
            var cnProduto = new DapperConnection<ProdutoDetalhe>();
            bool Temgrade = false;

            if (!buscarPorId)
            {
                SQLGrade = " select * from produtodetalhes " +
                           " INNER JOIN produtos ON produtos.Id = produtodetalhes.IdProduto " +
                           " WHERE produtos.Id = " + "(SELECT produtos.Id FROM produtos LEFT JOIN produtodetalhes ON produtodetalhes.IdProduto = produtos.id " +
                                                       " WHERE produtos.CodBarrasUnico = " + "'" + busca + "'" +
                                                       " OR produtos.Referencia = " + "'" + busca + "'" +
                                                       " OR produtodetalhes.codbarras  = " + "'" + busca + "'"  + 
                                                       " GROUP BY produtos.Id )";

            }                
            else
            {
                SQLGrade = "select * from produtodetalhes " +                           
                           " WHERE produtodetalhes.IdProduto = " + busca;
            }

            var prd = new ProdutoDetalhe();
            cnProduto.ExecuteToModel(ref prd, SQLGrade);
            var dados = prd;

            if(dados != null )
            {
                Temgrade = true;
            }

           SQL.AppendLine("SELECT p.Id AS ProdutoId, p.Referencia AS ProdutoReferencia, p.Descricao AS ProdutoDescricao, ");
            SQL.AppendLine(" c.id AS CorId, c.Abreviatura AS CorAbreviatura, t.id AS TamanhoId, t.Abreviatura AS TamanhoAbreviatura, ");
            SQL.AppendLine(" col.id AS ColecaoId, col.descricao AS ColecaoDescricao, cat.id AS CatalogoId, cat.descricao AS CatalogoDescricao, ");
            SQL.AppendLine(" IFNULL(e.Saldo, 0) AS Saldo, IFNULL(e.Empenhado, 0) AS Empenhado");
            SQL.AppendLine("FROM produtos p ");
            SQL.AppendLine(" LEFT JOIN produtodetalhes pd ON p.id = pd.IdProduto ");
            SQL.AppendLine(" LEFT JOIN catalogo cat ON p.Idcolecao = cat.id ");
            SQL.AppendLine(" LEFT JOIN colecoes col ON p.IdCatalogo = col.id ");
            SQL.AppendLine(" LEFT JOIN cores c ON pd.IdCor = c.id ");
            SQL.AppendLine(" LEFT JOIN tamanhos t ON pd.IdTamanho = t.id ");

            if(Temgrade)
            {
                SQL.AppendLine(" LEFT JOIN estoque e ON e.produtoid = p.id AND e.tamanhoid = t.id AND e.corid = c.id AND e.AlmoxarifadoId = " + almoxarifadoId);
            }
            else
            {
                SQL.AppendLine(" LEFT JOIN estoque e ON e.produtoid = p.id  AND e.AlmoxarifadoId = " + almoxarifadoId);
            }

            SQL.AppendLine("WHERE ");

            if (!buscarPorId)
                SQL.AppendLine(" (PD.CodBarras = '" + busca + "' OR P.CodBarrasUnico = '" + busca + "' OR P.Referencia = '" + busca + "') ");
            else
                SQL.AppendLine(" P.Id = " + busca);

            SQL.AppendLine("    AND P.Ativo = 1 ");
            SQL.AppendLine("    AND (IFNULL(C.Id, 0) != 999 AND IFNULL(T.Id, 0) != 999) ");
            SQL.AppendLine("    AND  IFNULL(PD.Inutilizado, 0) = 0 ");

            SQL.AppendLine(" ORDER BY p.referencia, c.abreviatura, t.id ");

            var cn = new DapperConnection<BalancoEstoqueItensView>();
            return cn.ExecuteStringSqlToList(new BalancoEstoqueItensView(), SQL.ToString());
        }

        public void UpdateEstoqueEmpenho(bool ativar)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE parametros SET ");
            SQL.AppendLine(" Valor = ");
            if(ativar) SQL.Append(" 1 ");
            else SQL.Append(" 2 ");
            SQL.AppendLine(" WHERE Chave = ");
            SQL.Append(" 'ESTOQUE_PEDIDO' ");

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateGridPedido()
        {
            var SQL = new StringBuilder();
            SQL.AppendLine(" UPDATE pedidovenda, ");
            SQL.AppendLine("    (SELECT p.id, ");
            SQL.AppendLine("        sum(ip.Qtd) as QtdPedida, ");
            SQL.AppendLine("        sum(ilp.Qtd) - sum(ilp.QtdFaturada) as QtdLiberada, ");
            SQL.AppendLine("        sum(ilp.QtdEmpenhada) as QtdEmpenhada, ");
            SQL.AppendLine("        sum(ilp.QtdEmpenhada * ip.Preco) as ValorEmpenhadoTotal, ");            
            SQL.AppendLine("        sum((ilp.Qtd - ilp.QtdFaturada) * ip.Preco) as ValorLiberadoTotal ");
            SQL.AppendLine("    FROM pedidovenda p ");
            SQL.AppendLine("    LEFT JOIN itenspedidovenda ip ON p.id = ip.PedidoVendaId ");
            SQL.AppendLine("    LEFT JOIN itensliberacaopedidovenda ilp ON ip.id = ilp.ItemPedidoVendaId ");
            SQL.AppendLine("    GROUP BY p.Id ) as PedidoAtualizado ");
            SQL.AppendLine(" SET pedidovenda.QtdPedida = PedidoAtualizado.QtdPedida, ");
            SQL.AppendLine(" pedidovenda.QtdLiberada = PedidoAtualizado.QtdLiberada, ");
            SQL.AppendLine(" pedidovenda.QtdEmpenhada = PedidoAtualizado.QtdEmpenhada, ");
            SQL.AppendLine(" pedidovenda.ValorEmpenhadoTotal = PedidoAtualizado.ValorEmpenhadoTotal, ");
            SQL.AppendLine(" pedidovenda.ValorLiberadoTotal = PedidoAtualizado.ValorLiberadoTotal ");
            SQL.AppendLine(" WHERE pedidovenda.Id = PedidoAtualizado.id ");

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void AtivarTrigger(BalancoEstoqueItens item, int almoxarifadoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE estoque SET ");
            SQL.AppendLine(" Saldo = Saldo ");
            SQL.AppendLine(" WHERE  ProdutoId = ");
            SQL.Append( item.ProdutoId.ToString() );
            SQL.AppendLine(" AND  CorId = ");
            SQL.Append( item.CorId.ToString() );
            SQL.AppendLine(" AND  TamanhoId = ");
            SQL.Append( item.TamanhoId.ToString() );
            SQL.AppendLine(" AND  AlmoxarifadoId = ");
            SQL.Append( almoxarifadoId.ToString() );

            _cn.ExecuteNonQuery(SQL.ToString());
        }

    }
}
