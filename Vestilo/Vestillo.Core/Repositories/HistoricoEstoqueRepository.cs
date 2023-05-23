
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class DadosItem
    {
        public int UltimoIdItem { get; set; }        
    }

    public class HistoricoEstoqueRepository : GenericRepository<HistoricoEstoqueView>
    {
        public IEnumerable<HistoricoEstoqueView> ListRelatorio(DateTime DaData, DateTime AteData,  bool Agrupado = false)
        {

            var Valor1 = "'" + DaData.ToString("yyyy-MM-dd") + "' AND '" + AteData.ToString("yyyy-MM-dd") + "'";
              

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT DataMovimento,produtos.id as ProdId,produtos.Referencia as ProdReferencia,produtos.Descricao as ProdDescricao,cores.id as CorId,cores.Abreviatura as CorAbreviatura,tamanhos.id as TamanhoId,tamanhos.Abreviatura as TamAbreviatura,usuarios.Nome as UsuarioNome,Saida,Entrada,movimentacaoestoque.Observacao as MovObservacao, ");
            sql.AppendLine(" Segmentos.descricao as Segmento,almoxarifados.id as IdAlmoxarifado,almoxarifados.descricao as AlmoxarifadoExibido,Catalogo.descricao as Catalogo,   ");
            sql.AppendLine(" UM.abreviatura as UnidMedida ");
            sql.AppendLine(" FROM movimentacaoestoque   ");
            sql.AppendLine("	INNER JOIN ESTOQUE ON ESTOQUE.Id = movimentacaoestoque.EstoqueId ");
            sql.AppendLine("	INNER JOIN produtos ON produtos.Id = ESTOQUE.ProdutoId ");
            sql.AppendLine("	LEFT JOIN cores on cores.Id = ESTOQUE.CorId ");
            sql.AppendLine("	LEFT JOIN tamanhos on tamanhos.Id = estoque.TamanhoId ");
            sql.AppendLine("	INNER JOIN usuarios on usuarios.Id = movimentacaoestoque.UsuarioId ");
            sql.AppendLine("	INNER JOIN almoxarifados on almoxarifados.id = ESTOQUE.AlmoxarifadoId ");
            sql.AppendLine("	LEFT JOIN catalogo on catalogo.Id = produtos.IdCatalogo ");
            sql.AppendLine("	LEFT JOIN segmentos on segmentos.id = produtos.Idsegmento ");
            sql.AppendLine("	LEFT JOIN unidademedidas UM on UM.id = produtos.IdUniMedida  ");

            sql.AppendLine("WHERE " );
            sql.AppendLine("  (almoxarifados.idempresa IS NULL OR almoxarifados.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ") AND ");
            sql.AppendLine("  (movimentacaoestoque.Observacao like 'ENTRADA%' OR movimentacaoestoque.Observacao like 'SAÍDA%' )");
            sql.AppendLine(" AND SUBSTRING(movimentacaoestoque.DataMovimento,1,10) BETWEEN " + Valor1);

            if (Agrupado)
            {
                sql.AppendLine(" ORDER by ESTOQUE.ProdutoId,ESTOQUE.CorId,estoque.TamanhoId ");
            }
            else
            {
                sql.AppendLine(" ORDER by DATE(DataMovimento), ESTOQUE.ProdutoId,ESTOQUE.CorId,estoque.TamanhoId ");
            }
           

            return VestilloConnection.ExecSQLToListWithNewConnection<HistoricoEstoqueView>(sql.ToString());
        }

        public IEnumerable<HistoricoEstoqueView> ListPlanilha(DateTime DaData, DateTime AteData, int idAlmoxarifado)
        {

            var intervalo = "'" + DaData.ToString("yyyy-MM-dd") + "' AND '" + AteData.ToString("yyyy-MM-dd") + "'";



            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT produtodetalhes.IdProduto, produtodetalhes.referenciagradecli as ProdDescricao,  ");
            sql.AppendLine(" (Entrada) AS Entrada, 'ENTRADA - LANÇAMENTO MANUAL' AS MovObservacao, almoxarifados.Descricao AS Almoxarifado, ");
            sql.AppendLine("	ROUND((fichatecnicadomaterial.total + fichafaccao.valorPeca),2) as Valor");
            sql.AppendLine(" FROM movimentacaoestoque   ");
            sql.AppendLine("	INNER JOIN ESTOQUE ON ESTOQUE.Id = movimentacaoestoque.EstoqueId ");
            sql.AppendLine("	INNER JOIN produtos ON produtos.Id = ESTOQUE.ProdutoId ");
            sql.AppendLine("	INNER JOIN cores on cores.Id = ESTOQUE.CorId ");
            sql.AppendLine("	INNER JOIN tamanhos on tamanhos.Id = estoque.TamanhoId ");
            sql.AppendLine("	INNER JOIN produtodetalhes ON produtodetalhes.IdProduto = produtos.Id AND produtodetalhes.Idcor = cores.Id AND  produtodetalhes.Idtamanho =  tamanhos.Id ");            
            sql.AppendLine("	INNER JOIN almoxarifados on almoxarifados.id = ESTOQUE.AlmoxarifadoId ");
            sql.AppendLine("	INNER JOIN fichatecnicadomaterial on fichatecnicadomaterial.ProdutoId = produtos.Id");
            sql.AppendLine("	INNER JOIN fichatecnica ON fichatecnica.ProdutoId = produtos.Id");
            sql.AppendLine("	LEFT JOIN fichafaccao ON fichafaccao.idFicha = fichatecnica.Id");

            sql.AppendLine("WHERE ");
            sql.AppendLine("  (produtos.TipoItem = 0) AND ");
            sql.AppendLine("  (almoxarifados.id = " + idAlmoxarifado.ToString() + ") AND ");
            sql.AppendLine("  (movimentacaoestoque.Observacao like 'ENTRADA%' )");
            sql.AppendLine(" AND SUBSTRING(movimentacaoestoque.DataMovimento,1,10) BETWEEN " + intervalo);
            sql.AppendLine(" GROUP BY ESTOQUE.ProdutoId,ESTOQUE.CorId,estoque.TamanhoId " );
            sql.AppendLine(" HAVING Entrada > 0 ");

            sql.AppendLine(" ORDER by ESTOQUE.ProdutoId,ESTOQUE.CorId,estoque.TamanhoId ");
            


            return VestilloConnection.ExecSQLToListWithNewConnection<HistoricoEstoqueView>(sql.ToString());
        }

        public IEnumerable<UltimoPrecoMaterialView> UltimoPrecoMaterial(List<int> ids)
        {
            string SQlItens = String.Empty;
            List<int> Ultimosids = new List<int>();
            foreach (var item in ids)
            {
                SQlItens = " SELECT MAX(notaentradaitens.id) as UltimoIdItem FROM notaentradaitens WHERE notaentradaitens.iditem = " + item;
                var Dados =  VestilloConnection.ExecSQLToListWithNewConnection<DadosItem>(SQlItens.ToString());
                if(Dados !=null)
                {
                    foreach (var itemIds in Dados)
                    {
                        if(itemIds.UltimoIdItem > 0)
                        {
                            Ultimosids.Add(itemIds.UltimoIdItem);
                        }
                        
                    }
                }
            }

            StringBuilder sql = new StringBuilder();
            if (Ultimosids.Count > 0)
            {
                sql.AppendLine(" SELECT produtos.Referencia as ReferenciaProduto,produtos.Descricao as DescricaoProduto,  ");
                sql.AppendLine(" notaentradaitens.preco as preco,notaentrada.numero as NumeroNota,notaentrada.DataInclusao as DataInclusao ");
                sql.AppendLine(" FROM notaentradaitens   ");
                sql.AppendLine(" INNER JOIN notaentrada ON  notaentrada.Id = notaentradaitens.IdNota   ");
                sql.AppendLine(" INNER JOIN produtos ON produtos.Id = notaentradaitens.iditem   ");
                sql.AppendLine("WHERE ");
                sql.AppendLine(" notaentradaitens.Id IN (" + string.Join(",", Ultimosids.ToArray()) + ")");
            }
            else
            {
                sql.AppendLine(" SELECT *  from notaentradaitens WHERE notaentradaitens.id = 0 ");
            }
           
         

            return VestilloConnection.ExecSQLToListWithNewConnection<UltimoPrecoMaterialView>(sql.ToString());
        }
    }
}
