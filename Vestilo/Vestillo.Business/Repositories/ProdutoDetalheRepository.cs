using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;
using System.Data;

namespace Vestillo.Business.Repositories
{
    public class ProdutoDetalheRepository : GenericRepository<ProdutoDetalhe>
    {
        private class Existentes
        {
            public int IdNfe { get; set; }
            public int IdNotaEntrada { get; set; }
            public int IdPedidoVenda { get; set; }
            public int IdPedidoCompra { get; set; }
            public int IdEstoque { get; set; }
            public int IdMovEstoque { get; set; }
        }

        public ProdutoDetalheRepository()
            : base(new DapperConnection<ProdutoDetalhe>())
        {
        }

        public ProdutoDetalheView GetViewByProduto(int produtoId)
        {
            var cn = new DapperConnection<ProdutoDetalheView>();
            var p = new ProdutoDetalheView();
            cn.ExecuteToModel("IdProduto = " + produtoId.ToString(), ref p);
            return p;
        }

        public IEnumerable<ProdutoDetalheView> GetListViewByProduto(int produtoId, int ativo, int ExibeSemuso = 0)
        {
            string sql;
            string sql2;

            if (ativo == 1) //traz somente itens da grade ativos
                sql = " AND produtodetalhes.Inutilizado = 0";
            else
                sql = "";

            if (ExibeSemuso == 1)
            {
                if (VestilloSession.UsaOrdenacaoFixa)
                {
                    sql2 = "  ORDER BY cores.Abreviatura,tamanhos.Abreviatura ";
                }
                else
                {
                    sql2 = "  ORDER BY cores.descricao,tamanhos.id ";
                }
                    
            }
            else
            {
                if (VestilloSession.UsaOrdenacaoFixa)
                {
                    sql2 = " AND (cores.Id != 999 AND tamanhos.Id != 999) ORDER BY cores.Abreviatura,tamanhos.Abreviatura ";
                }
                else
                {
                    sql2 = " AND (cores.Id != 999 AND tamanhos.Id != 999) ORDER BY cores.descricao,tamanhos.id ";
                }
                    
            }
            

            var cn = new DapperConnection<ProdutoDetalheView>();
            var p = new ProdutoDetalheView();
            return cn.ExecuteStringSqlToList(p, "SELECT produtodetalhes.*,cores.abreviatura AS AbvCor, cores.descricao AS DescCor, " +
                "tamanhos.abreviatura AS AbvTamanho, tamanhos.descricao AS DescTamanho, produtos.referencia as RefProduto,produtos.Descricao as DescProduto " +
                "FROM produtodetalhes INNER JOIN cores ON cores.Id = produtodetalhes.IdCor INNER JOIN tamanhos ON tamanhos.Id = produtodetalhes.IdTamanho " +
                "INNER JOIN produtos ON produtos.Id = produtodetalhes.IdProduto WHERE  IdProduto = " + produtoId.ToString() + sql.ToString() + sql2.ToString());

                

        }

        public ProdutoDetalhe GetByProduto(int ProdutoId)
        {
            var ProdutoGrade = new ProdutoDetalhe();
            _cn.ExecuteToModel("IdProduto = " + ProdutoId.ToString(), ref ProdutoGrade);
            return ProdutoGrade;
        }

        public IEnumerable<ProdutoDetalhe> GetListByProduto(int ProdutoId, int ativo)
        {
            string sqlIN = "(" + ProdutoId + ")";
            var p = new ProdutoDetalhe();
            string sql = "";
            if (ativo == 1) //traz somente itens da grade ativos
                sql = " AND produtodetalhes.Inutilizado = 0";
            else
                sql = "";

            return _cn.ExecuteStringSqlToList(p, "SELECT produtodetalhes.*,cores.abreviatura AS AbvCor, cores.descricao AS DescCor, tamanhos.abreviatura AS AbvTamanho, tamanhos.descricao AS DescTamanho FROM produtodetalhes INNER JOIN cores ON cores.Id = produtodetalhes.IdCor INNER JOIN tamanhos ON tamanhos.Id = produtodetalhes.IdTamanho  WHERE  IdProduto IN " + sqlIN + sql.ToString());
        }

        public IEnumerable<ProdutoDetalheView> GetByDetalheCodBarras(string codBarras)
        {
            //var cn = new DapperConnection<ProdutoDetalheView>();
            //var p = new ProdutoDetalheView();
            //cn.ExecuteToModel("codbarras = " + codBarras, ref p);
            //return p;

            var SQL = new Select()
                .Campos("produtodetalhes.*,cores.abreviatura AS AbvCor, cores.descricao AS DescCor, " +
                        "tamanhos.abreviatura AS AbvTamanho, tamanhos.descricao AS DescTamanho, produtos.referencia as RefProduto, " +
                        "produtos.Descricao as DescProduto ")
                .From("produtodetalhes")
                .InnerJoin("cores", "cores.Id = produtodetalhes.IdCor")
                .InnerJoin("tamanhos", "tamanhos.Id = produtodetalhes.IdTamanho")
                .InnerJoin("produtos", "produtos.Id = produtodetalhes.IdProduto")
                .Where("codbarras = '" + codBarras.ToString() + "' and " + FiltroEmpresa("IdEmpresa"));


            var cn = new DapperConnection<ProdutoDetalheView>();
            var p = new ProdutoDetalheView();
            return cn.ExecuteStringSqlToList(p, SQL.ToString());

        }

        public void AtualizarGrade(ProdutoGradeView grade)
        {

            if (grade.Checked == false || grade.GradeId > 0)
            {
                string sql = "UPDATE produtodetalhes SET Inutilizado = " + (grade.Checked ? "1" : "0").ToString() + " WHERE Id = " + grade.GradeId.ToString();
                _cn.ExecuteNonQuery(sql);
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO produtodetalhes (IdProduto, IdTamanho, Idcor, Inutilizado) VALUES");
                sql.AppendLine("(" + grade.ProdutoId.ToString() + ", " + grade.TamanhoId.ToString() + ", " + grade.CorId.ToString() + ", 0)");
                sql.AppendLine(";SELECT (LAST_INSERT_ID()) AS ID");

                DataTable dt = _cn.ExecuteToDataTable(sql.ToString());

                grade.GradeId = Convert.ToInt32(dt.Rows[0]["Id"]);
            }
        }

        public IEnumerable<ProdutoGradeView> GetGradeParaEdicao(int[] produtos, int[] cores, int[] tamanhos, int pagina, int registrosPorPagina, int tipoItem, out int totalRegistros)
        {
            StringBuilder sql = new StringBuilder();

            if (pagina > 0)
                pagina--;

            if (registrosPorPagina == 0)
                registrosPorPagina = 100;

            sql.AppendLine("SELECT SQL_CALC_FOUND_ROWS ");
            //sql.AppendLine("		(SELECT Id FROM ProdutoDetalhes PD WHERE PD.IdProduto = P.Id AND PD.Idcor = C.Id AND PD.IdTamanho = T.Id AND PD.Inutilizado = 0 LIMIT 1) AS GradeId, ");
            sql.AppendLine("		0 AS Checked,");
            sql.AppendLine("		0 AS GradeId,");
            sql.AppendLine("		P.Id AS ProdutoId,");
            sql.AppendLine("		P.Referencia AS ProdutoReferencia, ");
            sql.AppendLine("        P.Descricao AS ProdutoDescricao, ");
            sql.AppendLine("		T.Id AS TamanhoId, ");
            sql.AppendLine("		T.Abreviatura AS TamanhoAbreviatura, ");
            sql.AppendLine("		C.Id AS CorId, ");
            sql.AppendLine("		C.Abreviatura AS CorAbreviatura");
            sql.AppendLine("FROM 	Cores AS C, Tamanhos AS T, Produtos AS P");
            sql.AppendLine("WHERE 	P.Ativo = 1");
            sql.AppendLine("		AND T.Ativo = 1");
            sql.AppendLine("		AND C.Ativo = 1");
            sql.AppendLine("		AND" + FiltroEmpresa("P.Idempresa"));

            if (tipoItem >= 0)
                sql.AppendLine("        AND (P.TipoItem = " + tipoItem.ToString() + ")");

            if (produtos != null && produtos.Length > 0)
                sql.AppendLine("        AND P.Id IN (" + string.Join(", ", produtos) + ")");

            if (cores != null && cores.Length > 0)
                sql.AppendLine("        AND C.Id IN (" + string.Join(", ", cores) + ")");

            if (tamanhos != null && tamanhos.Length > 0)
                sql.AppendLine("        AND T.Id IN (" + string.Join(", ", tamanhos) + ")");


            sql.AppendLine("ORDER BY P.Referencia, T.Abreviatura, C.Abreviatura");
            sql.AppendLine("LIMIT " + (pagina * registrosPorPagina).ToString() + ", " + registrosPorPagina.ToString());
            sql.AppendLine(";SELECT FOUND_ROWS() AS Qtd;");

            var cn = new DapperConnection<ProdutoGradeView>();
            var p = new ProdutoGradeView();
            return cn.ExecuteStringSqlToList(p, sql.ToString(), out totalRegistros);
        }

        public IEnumerable<ProdutoDetalhe> GetAtivos(int[] produtos, int[] cores, int[] tamanhos)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT PD.* ");
            sql.AppendLine("FROM ProdutoDetalhes PD ");
            sql.AppendLine("    INNER JOIN Produtos P ON P.Id = PD.IdProduto ");
            sql.AppendLine("WHERE PD.Inutilizado = 0 AND P.ativo = 1 AND" + FiltroEmpresa("P.Idempresa"));
            sql.AppendLine("AND PD.IdProduto IN (" + string.Join(", ", produtos) + ")");
            sql.AppendLine("AND PD.Idcor IN (" + string.Join(", ", cores) + ")");
            sql.AppendLine("AND PD.IdTamanho IN (" + string.Join(", ", tamanhos) + ")");

            return _cn.ExecuteStringSqlToList(new ProdutoDetalhe(), sql.ToString());
        }

        public IEnumerable<ProdutoDetalhe> GetCoresDoProdutoETamanho(int produto, int cor)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT PD.* ");
            sql.AppendLine("FROM ProdutoDetalhes PD ");
            sql.AppendLine("    INNER JOIN Produtos P ON P.Id = PD.IdProduto ");
            sql.AppendLine("WHERE PD.Inutilizado = 0  AND" + FiltroEmpresa("P.Idempresa"));
            sql.AppendLine("AND PD.IdProduto =" + produto);
            sql.AppendLine("AND PD.Idcor=" + cor);
            return _cn.ExecuteStringSqlToList(new ProdutoDetalhe(), sql.ToString());
        }

        public ProdutoDetalhe GetByGrade(int produtoId, int cor, int tamanho)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT PD.* ");
            sql.AppendLine("FROM ProdutoDetalhes PD ");
            sql.AppendLine("    INNER JOIN Produtos P ON P.Id = PD.IdProduto ");
            sql.AppendLine("WHERE " + FiltroEmpresa("P.Idempresa"));
            sql.AppendLine("AND PD.IdProduto =" + produtoId);
            sql.AppendLine("AND PD.Idcor=" + cor);
            sql.AppendLine("AND PD.Idtamanho=" + tamanho);
            ProdutoDetalhe grade = new ProdutoDetalhe();
            _cn.ExecuteToModel(ref grade, sql.ToString());
            return grade;
        }

        public bool VerificarCorUnica(int produtoId, int cor)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT IFNULL(max(PD.CorUnica), 0)  as unico ");
            sql.AppendLine("FROM ProdutoDetalhes PD ");
            sql.AppendLine("    INNER JOIN Produtos P ON P.Id = PD.IdProduto ");
            sql.AppendLine("WHERE PD.Inutilizado = 0  AND" + FiltroEmpresa("P.Idempresa"));
            sql.AppendLine("AND PD.IdProduto =" + produtoId);
           // sql.AppendLine("AND PD.Idcor=" + cor);
             Unico unico = new Unico();
            var cn = new DapperConnection<Unico>();
            cn.ExecuteToModel(ref unico, sql.ToString());
            return unico.unico;
        }

        public bool VerificarTamanhoUnico(int produtoId, int tamanho)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT IFNULL(max(PD.TamanhoUnico), 0) as unico ");
            sql.AppendLine("FROM ProdutoDetalhes PD ");
            sql.AppendLine("    INNER JOIN Produtos P ON P.Id = PD.IdProduto ");
            sql.AppendLine("WHERE PD.Inutilizado = 0  AND" + FiltroEmpresa("P.Idempresa"));
            sql.AppendLine("AND PD.IdProduto =" + produtoId);
           // sql.AppendLine("AND PD.Idtamanho=" + tamanho);
            Unico unico = new Unico();
            var cn = new DapperConnection<Unico>();
            cn.ExecuteToModel(ref unico, sql.ToString());
            return unico.unico;
        }

        public IEnumerable<ProdutoDetalhe> GetTamanhosDoProdutoECor(int produto, int Tamanho)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT PD.* ");
            sql.AppendLine("FROM ProdutoDetalhes PD ");
            sql.AppendLine("    INNER JOIN Produtos P ON P.Id = PD.IdProduto ");
            sql.AppendLine("WHERE PD.Inutilizado = 0 AND P.ativo = 1 AND" + FiltroEmpresa("P.Idempresa"));
            sql.AppendLine("AND PD.IdProduto =" + produto);
            sql.AppendLine("AND PD.IdTamanho = " + Tamanho);
            return _cn.ExecuteStringSqlToList(new ProdutoDetalhe(), sql.ToString());
        }

        public bool GradeUsada(int idProduto, int idCor, int idTamanho)
        {
            bool JaFoiUsado = false;
            var cn = new DapperConnection<Existentes>();
            int IdNfe = 0;
            int IdNotaEntrada = 0;
            int IdPedidoVenda = 0;
            int IdPedidoCompra = 0;
            int IdEstoque = 0;
            int IdMovEstoque = 0;
            string SQL = String.Empty;

            var c1 = new Existentes();

            SQL = "select IFNULL(nfeitens.id,0) as IdNfe from nfeitens WHERE nfeitens.idcor = "+ idCor +" and nfeitens.iditem = "+ idProduto + " and  nfeitens.idtamanho = " + idTamanho + " limit 1 ";
            var dados1 = cn.ExecuteStringSqlToList(c1, SQL);
            SQL = " select IFNULL(notaentradaitens.id,0) as IdNotaEntrada from notaentradaitens WHERE notaentradaitens.idcor = "+ idCor + "  and notaentradaitens.iditem = " + idProduto + "  and notaentradaitens.idtamanho = " + idTamanho + " limit 1 ";
            var dados2 = cn.ExecuteStringSqlToList(c1, SQL);
            SQL = " select IFNULL(itenspedidovenda.id,0) as IdPedidoVenda from itenspedidovenda WHERE itenspedidovenda.CorId = "+ idCor + "  and itenspedidovenda.TamanhoId = " + idTamanho + "  and itenspedidovenda.ProdutoId = " + idProduto + " limit 1 ";
            var dados3 = cn.ExecuteStringSqlToList(c1, SQL);
            SQL = " select IFNULL(itenspedidocompra.Id,0) as IdPedidoCompra from itenspedidocompra WHERE itenspedidocompra.CorId = " + idCor + "  and itenspedidocompra.TamanhoId = " + idTamanho + "  and itenspedidocompra.ProdutoId = " + idProduto + " limit 1 ";
            var dados4 = cn.ExecuteStringSqlToList(c1, SQL);
            SQL = " select IFNULL(estoque.Id,0) as IdEstoque from estoque WHERE estoque.CorId = " + idCor + "  and estoque.TamanhoId = " + idTamanho + "  and estoque.ProdutoId = " + idProduto + " AND estoque.Saldo <> 0 AND estoque.Empenhado <> 0 limit 1 ";
            var dados5 = cn.ExecuteStringSqlToList(c1, SQL);
            SQL = " select COUNT(IFNULL(movimentacaoestoque.id, 0)) as IdMovEstoque from movimentacaoestoque INNER JOIN estoque ON estoque.Id = movimentacaoestoque.EstoqueId WHERE estoque.CorId = " + idCor + "  and estoque.TamanhoId = " + idTamanho + "  and estoque.ProdutoId = " + idProduto ;
            var dados6 = cn.ExecuteStringSqlToList(c1, SQL);

            foreach (var item in dados1)
            {
                IdNfe = item.IdNfe;
            }

            foreach (var item in dados2)
            {
                IdNotaEntrada = item.IdNotaEntrada;
            }
            foreach (var item in dados3)
            {
                IdPedidoVenda = item.IdPedidoVenda;
            }
            foreach (var item in dados4)
            {
                IdPedidoCompra = item.IdPedidoCompra;
            }
            foreach (var item in dados5)
            {
                IdEstoque = item.IdEstoque;
            }
            foreach (var item in dados6)
            {
                IdMovEstoque = item.IdMovEstoque;
            }

            if (IdNfe > 0 || IdNotaEntrada > 0 || IdPedidoVenda > 0 || IdPedidoCompra > 0 || IdEstoque > 0 || IdMovEstoque > 0)
            {
                JaFoiUsado = true;
            }
            return JaFoiUsado;

        }


        public void InclusaoDeGradeEstoque(IEnumerable<ProdutoDetalhe> Grade,int Almoxarifado)
        {
            foreach (var item in Grade)
            {
                int Produto = item.IdProduto;
                int Cor = item.Idcor;
                int Tamanho = item.IdTamanho;
                string SQL = String.Empty;
                var cn = new DapperConnection<Estoque>();




                SQL = "SELECT * FROM estoque WHERE estoque.ProdutoId = " + Produto + " AND estoque.CorId = " + Cor + " AND estoque.TamanhoId = " + Tamanho;
                var dados = cn.ExecuteStringSqlToList(new Estoque(), SQL);
                if(dados == null || dados.Count() <= 0)
                {
                    SQL = "insert into estoque " +
                          " (AlmoxarifadoId, ProdutoId, CorId, TamanhoId, " +
                          " Saldo,Empenhado) " +
                          " values (" + 
                          Almoxarifado + "," + Produto + "," + Cor + "," + Tamanho + "," +
                          " 0, 0)";
                    cn.ExecuteNonQuery(SQL);

                }


            }

        }

        public void InclusaoDeGradeUnicaEstoque(int ProdutoId,int CorId,int TamanhoId, int Almoxarifado)
        {
            
                int Produto = ProdutoId;
                int Cor = CorId;
                int Tamanho = TamanhoId;
                string SQL = String.Empty;
                var cn = new DapperConnection<Estoque>();




                SQL = "SELECT * FROM estoque WHERE estoque.ProdutoId = " + Produto + " AND estoque.CorId = " + Cor + " AND estoque.TamanhoId = " + Tamanho;
                var dados = cn.ExecuteStringSqlToList(new Estoque(), SQL);
                if (dados == null || dados.Count() <= 0)
                {
                    SQL = "insert into estoque " +
                          " (AlmoxarifadoId, ProdutoId, CorId, TamanhoId, " +
                          " Saldo,Empenhado) " +
                          " values (" +
                          Almoxarifado + "," + Produto + "," + Cor + "," + Tamanho + "," +
                          " 0, 0)";
                    cn.ExecuteNonQuery(SQL);

                }


            

        }

        public void ExclusaoDeGradeEstoque(int ProdutoId, int Almoxarifado)
        {
            string SQLmov = String.Empty;
            var cnMov = new DapperConnection<MovimentacaoEstoque>();

            SQLmov = " DELETE FROM movimentacaoestoque WHERE EstoqueId IN " +
                  " (SELECT e.id FROM estoque e " +
                  " LEFT JOIN produtodetalhes ON produtodetalhes.IdProduto = e.ProdutoId AND produtodetalhes.IdCor = e.CorId AND produtodetalhes.IdTamanho = e.TamanhoId" +
                  " WHERE e.saldo = 0 AND e.produtoid = " + ProdutoId + " AND e.AlmoxarifadoId = " + Almoxarifado +
                  " AND produtodetalhes.Id IS NULL) ";

            cnMov.ExecuteNonQuery(SQLmov);

            string SQL = String.Empty;
            var cn = new DapperConnection<Estoque>();

            SQL = " DELETE FROM estoque " +
                  " WHERE estoque.saldo = 0 AND estoque.produtoid = 5963 AND estoque.AlmoxarifadoId = 1 AND  " +
                  " ifnull((SELECT produtodetalhes.id from produtodetalhes where produtodetalhes.IdProduto = estoque.ProdutoId AND produtodetalhes.IdCor = estoque.CorId AND produtodetalhes.IdTamanho = estoque.TamanhoId), 0 ) = 0 ";

            cn.ExecuteNonQuery(SQL);
        }


        public IEnumerable<Cor> GetCoresDoProduto(int Idproduto)
        {
            var cn = new DapperConnection<Cor>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select produtodetalhes.Idcor as Id,cores.Descricao from produtodetalhes  ");
            sql.AppendLine("INNER JOIN cores ON cores.Id = produtodetalhes.Idcor ");
            sql.AppendLine(" where produtodetalhes.IdProduto = " + Idproduto + " group by produtodetalhes.Idcor ");
            sql.AppendLine("order by cores.Descricao");
            return cn.ExecuteStringSqlToList(new Cor(), sql.ToString());
        }


        public IEnumerable<Tamanho> GetTamanhosDoProduto(int Idproduto)
        {
            var cn = new DapperConnection<Tamanho>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select produtodetalhes.IdTamanho as Id,tamanhos.Descricao from produtodetalhes  ");
            sql.AppendLine("INNER JOIN tamanhos ON tamanhos.Id = produtodetalhes.IdTamanho ");
            sql.AppendLine(" where produtodetalhes.IdProduto = " + Idproduto + " group by produtodetalhes.IdTamanho ");
            sql.AppendLine("order by tamanhos.Id");
            return cn.ExecuteStringSqlToList(new Tamanho(), sql.ToString());
        }

    }

    public class Unico {
        public bool unico { get; set; }
    }
}