using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class OperacaoFaccaoRepository : GenericRepository<OperacaoFaccao>
    {
        public OperacaoFaccaoRepository()
            : base(new DapperConnection<OperacaoFaccao>())
        {
        }

        public IEnumerable<OperacaoFaccaoView> GetByIdView(int id)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * ");
            SQL.AppendLine("FROM 	OperacaoFaccao");
            SQL.AppendLine("WHERE id = " + id);
            SQL.AppendLine("ORDER BY Id DESC");

            return cn.ExecuteStringSqlToList(new OperacaoFaccaoView(), SQL.ToString());
        }

        public OperacaoFaccaoView GetByCupom(int PacoteId, int OperacaoId, string sequencia)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	* ");
            SQL.AppendLine("FROM 	OperacaoFaccao");
            SQL.AppendLine("WHERE PacoteId = " + PacoteId  + " AND OperacaoId = " + OperacaoId + " AND Sequencia = " + sequencia);
            var operacao = new OperacaoFaccaoView();
            cn.ExecuteToModel(ref operacao, SQL.ToString());
            return operacao;
        }

        public int GetQuantidadeLancadaPorPacote(int PacoteId)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	* ");
            SQL.AppendLine("FROM 	OperacaoFaccao");
            SQL.AppendLine("WHERE PacoteId = " + PacoteId);

            var result = cn.ExecuteStringSqlToList(new OperacaoFaccaoView(), SQL.ToString());
            if ( result != null){
                return result.Count();
            }

            return 0;
        }

        public IEnumerable<OperacaoFaccaoView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	oo.*, gp.titulocupom as OperacaoDescricao, gp.Sequencia as PacoteSequencia, pa.Referencia as PacoteReferencia,");
            SQL.AppendLine("Concat(p.Referencia, ' - ', p.Descricao) as ProdutoDescricao, p.Referencia as ProdutoReferencia, pa.quantidade as Quantidade, (gp.Tempo*pa.quantidade + p.TempoPacote) as Tempo");
            SQL.AppendLine("FROM 	operacaofaccao oo");
            SQL.AppendLine("INNER JOIN colaboradores f ON f.id = oo.FaccaoId");
            SQL.AppendLine("INNER JOIN pacotes pa ON pa.id = oo.PacoteId");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id AND oo.Sequencia = gp.Sequencia)");
            SQL.AppendLine("INNER JOIN produtos p ON p.id = pa.ProdutoId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND oo.data = '" + data.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY oo.Id DESC");

            return cn.ExecuteStringSqlToList(new OperacaoFaccaoView(), SQL.ToString());
        }

        public IEnumerable<OperacaoFaccaoView> GetByFuncionarioIdEDatas(int funcId, DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	oo.*, gp.titulocupom as OperacaoDescricao, gp.Sequencia as PacoteSequencia, pa.Referencia as PacoteReferencia,");
            SQL.AppendLine("p.Descricao as ProdutoDescricao, p.Referencia as ProdutoReferencia, pa.quantidade as Quantidade, (gp.Tempo*pa.quantidade + p.TempoPacote) as Tempo");
            SQL.AppendLine("FROM 	operacaofaccao oo");
            SQL.AppendLine("INNER JOIN colaboradores f ON f.id = oo.FaccaoId");
            SQL.AppendLine("INNER JOIN pacotes pa ON pa.id = oo.PacoteId");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id  AND oo.Sequencia = gp.Sequencia)");
            SQL.AppendLine("INNER JOIN produtos p ON p.id = pa.ProdutoId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine(" AND oo.data BETWEEN '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("ORDER BY oo.Id DESC");

            return cn.ExecuteStringSqlToList(new OperacaoFaccaoView(), SQL.ToString());
        }

        public OperacaoFaccaoView GetByData(DateTime data)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	oo.*, gp.titulocupom as OperacaoDescricao, gp.Sequencia as PacoteSequencia, pa.Referencia as PacoteReferencia,");
            SQL.AppendLine("p.Descricao as ProdutoDescricao, p.Referencia as ProdutoReferencia, pa.quantidade as Quantidade, SUM(gp.Tempo*pa.quantidade + p.TempoPacote) as Tempo");
            SQL.AppendLine("FROM 	OperacaoFaccao oo");
            SQL.AppendLine("INNER JOIN Colaboradores f ON f.id = oo.FaccaoId");
            SQL.AppendLine("INNER JOIN pacotes pa ON pa.id = oo.PacoteId");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id  AND oo.Sequencia = gp.Sequencia)");
            SQL.AppendLine("INNER JOIN produtos p ON p.id = pa.ProdutoId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" oo.data = '" + data.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("GROUP BY oo.data");
            SQL.AppendLine("ORDER BY oo.Id DESC");

            var operacao = new OperacaoFaccaoView();
            cn.ExecuteToModel(ref operacao, SQL.ToString());
            return operacao;
        }

        public OperacaoFaccaoView GetByYear()
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	oo.*, gp.titulocupom as OperacaoDescricao, gp.Sequencia as PacoteSequencia, pa.Referencia as PacoteReferencia,");
            SQL.AppendLine("p.Descricao as ProdutoDescricao, p.Referencia as ProdutoReferencia, pa.quantidade as Quantidade, SUM(gp.Tempo*pa.quantidade + p.TempoPacote) as Tempo");
            SQL.AppendLine("FROM 	OperacaoFaccao oo");
            SQL.AppendLine("INNER JOIN colaboradores f on f.id = oo.FaccaoId");
            SQL.AppendLine("INNER JOIN pacotes pa ON pa.id = oo.PacoteId");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id  AND oo.Sequencia = gp.Sequencia)");
            SQL.AppendLine("INNER JOIN produtos p ON p.id = pa.ProdutoId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" YEAR(oo.data) = '" + DateTime.Today.ToString("yyyy") + "'");
            SQL.AppendLine("GROUP BY YEAR(oo.data)");
            SQL.AppendLine("ORDER BY oo.Id DESC");

            var operacao = new OperacaoFaccaoView();
            cn.ExecuteToModel(ref operacao, SQL.ToString());
            return operacao;
        }

        public OperacaoFaccaoView GetByDatas(DateTime daData, DateTime ateData)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	oo.*, gp.titulocupom as OperacaoDescricao, gp.Sequencia as PacoteSequencia, pa.Referencia as PacoteReferencia,");
            SQL.AppendLine("p.Descricao as ProdutoDescricao, p.Referencia as ProdutoReferencia, pa.quantidade as Quantidade, SUM(gp.Tempo*pa.quantidade + p.TempoPacote) as Tempo");
            SQL.AppendLine("FROM 	OperacaoFaccao oo");
            SQL.AppendLine("INNER JOIN colaboradores f ON f.id = oo.Faccaoid");
            SQL.AppendLine("INNER JOIN pacotes pa ON pa.id = oo.PacoteId");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id  AND oo.Sequencia = gp.Sequencia)");
            SQL.AppendLine("INNER JOIN produtos p ON p.id = pa.ProdutoId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" oo.data BETWEEN '" + daData.ToString("yyyy-MM-dd") + "' AND '" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine("GROUP BY YEAR(oo.data)");
            SQL.AppendLine("ORDER BY oo.Id DESC");

            var operacao = new OperacaoFaccaoView();
            cn.ExecuteToModel(ref operacao, SQL.ToString());
            return operacao;
        }

        public IEnumerable<OperacaoFaccaoView> GetByFuncionario(int funcId)
        {
            var cn = new DapperConnection<OperacaoFaccaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	oo.*, gp.titulocupom as OperacaoDescricao, gp.Sequencia as PacoteSequencia, pa.Referencia as PacoteReferencia,");
            SQL.AppendLine("p.Descricao as ProdutoDescricao, p.Referencia as ProdutoReferencia, pa.quantidade as Quantidade, (gp.Tempo*pa.quantidade + p.TempoPacote) as Tempo");
            SQL.AppendLine("FROM 	OperacaoFaccao oo");
            SQL.AppendLine("INNER JOIN colaboradores f ON f.id = oo.FaccaoId");
            SQL.AppendLine("INNER JOIN pacotes pa ON pa.id = oo.PacoteId");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id  AND oo.Sequencia = gp.Sequencia)");
            SQL.AppendLine("INNER JOIN produtos p ON p.id = pa.ProdutoId");
            SQL.AppendLine("WHERE f.id = " + funcId);
            SQL.AppendLine("ORDER BY oo.Id DESC");

            return cn.ExecuteStringSqlToList(new OperacaoFaccaoView(), SQL.ToString());
        }
    }
}
