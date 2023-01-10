
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;


namespace Vestillo.Business.Repositories
{
    public class CreditoFornecedorRepository: GenericRepository<CreditoFornecedor>
    {
        public CreditoFornecedorRepository()
            : base(new DapperConnection<CreditoFornecedor>())
        {
        }

        public IEnumerable<CreditoFornecedor> GetByContasPagarBaixa(int contasPagarBaixaId)
        {
            return _cn.ExecuteToList(new CreditoFornecedor(), "IdContasPagarBaixa = " + contasPagarBaixaId.ToString());
        }

        public IEnumerable<CreditoFornecedor> GetByContasPagarBaixaQueGerou(int contasPagarBaixaId)
        {
            return _cn.ExecuteToList(new CreditoFornecedor(), "IdContasPagarBaixaQueGerou = " + contasPagarBaixaId.ToString());
        }

        public IEnumerable<CreditoFornecedorView> GetAllView()
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	C.*,");
            sql.AppendLine("	F.nome AS FornecedorNome,");
            sql.AppendLine("	F.referencia AS FornecedorReferencia");
            sql.AppendLine("FROM 	creditofornecedor C ");
            sql.AppendLine("    INNER JOIN Colaboradores F ON F.id = C.IdFornecedor");
            sql.AppendLine("WHERE " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("ORDER BY C.DataEmissao DESC");

            var cn = new DapperConnection<CreditoFornecedorView>();
            return cn.ExecuteStringSqlToList(new CreditoFornecedorView(), sql.ToString());
        }

        public IEnumerable<CreditoFornecedorView> GetFiltro(string fornecedor)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	C.*,");
            sql.AppendLine("	F.nome AS FornecedorNome,");
            sql.AppendLine("	F.referencia AS FornecedorReferencia");
            sql.AppendLine("FROM 	creditofornecedor C ");
            sql.AppendLine("    INNER JOIN Colaboradores F ON F.id = C.IdFornecedor");
            sql.AppendLine("WHERE " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine(" AND (F.Nome LIKE '%" + (fornecedor ?? "").Trim() + "%' OR F.referencia LIKE '%" + (fornecedor ?? "").Trim() + "%') ");
            sql.AppendLine("  AND C.Ativo = 1  ");
            sql.AppendLine("ORDER BY C.DataEmissao DESC");

            var cn = new DapperConnection<CreditoFornecedorView>();
            return cn.ExecuteStringSqlToList(new CreditoFornecedorView(), sql.ToString());
        }


        public CreditoFornecedorView GetViewById(int id)
        {
            var cn = new DapperConnection<CreditoFornecedorView>();
            var p = new CreditoFornecedorView();
            cn.ExecuteToModel(ref p, id);
            return p;
        }

        public decimal GetByAteData(DateTime data)
        {
            var cn = new DapperConnection<CreditoFornecedor>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) AS Valor");
            SQL.AppendLine("FROM	creditofornecedor AS C");
            SQL.AppendLine("WHERE DATE(C.DataEmissao) <= '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));

            var cr = new CreditoFornecedor();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Valor;
        }

        public decimal GetByData(DateTime data)
        {
            var cn = new DapperConnection<CreditoFornecedor>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) AS Valor");
            SQL.AppendLine("FROM	creditofornecedor AS C");
            SQL.AppendLine("WHERE DATE(C.DataEmissao) = '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));

            var cr = new CreditoFornecedor();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.Valor;
        }

        public List<CreditoFornecedorView> GetByDataConsulta(DateTime data)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	C.*,");
            sql.AppendLine("	F.nome AS FornecedorNome,");
            sql.AppendLine("	F.referencia AS FornecedorReferencia");
            sql.AppendLine("FROM 	creditofornecedor C ");
            sql.AppendLine("    INNER JOIN Colaboradores F ON F.id = C.IdFornecedor");
            sql.AppendLine("WHERE DATE(C.DataEmissao) = '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND  C.Ativo = 1 ");
            sql.AppendLine("        AND " + FiltroEmpresa("", "C"));
            sql.AppendLine("ORDER BY C.DataEmissao");

            var cn = new DapperConnection<CreditoFornecedorView>();
            return cn.ExecuteStringSqlToList(new CreditoFornecedorView(), sql.ToString()).ToList();
        }

        public List<CreditoFornecedorView> GetByAteDataConsulta(DateTime data)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	C.*,");
            sql.AppendLine("	F.nome AS FornecedorNome,");
            sql.AppendLine("	F.referencia AS FornecedorReferencia");
            sql.AppendLine("FROM 	creditofornecedor C ");
            sql.AppendLine("    INNER JOIN Colaboradores F ON F.id = C.IdFornecedor");
            sql.AppendLine("WHERE DATE(C.DataEmissao) <= '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.Ativo = 1 ");
            sql.AppendLine("        AND " + FiltroEmpresa("", "C"));
            sql.AppendLine("ORDER BY C.DataEmissao");

            var cn = new DapperConnection<CreditoFornecedorView>();
            return cn.ExecuteStringSqlToList(new CreditoFornecedorView(), sql.ToString()).ToList();
        }

        public int GetCreditoFornecedorPelaNota(int idNota)
        {

            var cn = new DapperConnection<CreditoFornecedor>();

            var cr = new CreditoFornecedor();

            string SQL = "SELECT * from creditofornecedor WHERE creditofornecedor.IdDevolucao = " + idNota;
            cn.ExecuteToModel(ref cr, SQL.ToString());

            if(cr.Id != null)
            {
                return cr.Id;
            }
            else
            {
                return 0;
            }
        }

        public void DeletaParaIncluir(int IdDevolucao)
        {
            var cn = new DapperConnection<CreditoFornecedor>();
            

            string SQL = "DELETE from creditofornecedor WHERE creditofornecedor.IdDevolucao = " + IdDevolucao;
            cn.ExecuteNonQuery(SQL);

        }



    }
}

