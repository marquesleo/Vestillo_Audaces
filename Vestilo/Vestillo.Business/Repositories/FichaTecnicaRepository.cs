using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Connection;
using System.Data;
using Vestillo.Lib;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Repositories
{
    public class FichaTecnicaRepository : GenericRepository<FichaTecnica>
    {
        public FichaTecnicaRepository() : base(new DapperConnection<FichaTecnica>())
        {

        }

        public IEnumerable<FichaTecnicaView> GetAllView()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("        F.*,");
            sql.AppendLine("        o.Observacao AS Observacao, ");
            sql.AppendLine("        P.Referencia AS ProdutoReferencia,");
            sql.AppendLine("        P.Descricao AS ProdutoDescricao,");
            sql.AppendLine("        C.descricao AS Colecao,");
            sql.AppendLine("        GP.descricao AS GrupoProdutoDescricao,");
            sql.AppendLine("        S.descricao AS Segmento, ");
            sql.AppendLine("        U.Nome AS NomeUsuario, ");
            sql.AppendLine("        IFNULL((SELECT valorPeca FROM fichafaccao WHERE idFicha = F.Id ORDER BY fichafaccao.Id LIMIT 1),0) AS ValorFaccao");
            sql.AppendLine("FROM FichaTecnica AS F");
            sql.AppendLine("    INNER JOIN Produtos AS P ON P.Id = F.ProdutoId");
            sql.AppendLine("    LEFT JOIN Colecoes AS C ON C.Id = P.Idcolecao");
            sql.AppendLine("    LEFT JOIN grupoprodutos AS GP ON GP.id = P.IdGrupo");
            sql.AppendLine("    LEFT JOIN segmentos AS S ON S.id = P.Idsegmento");
            sql.AppendLine("    LEFT JOIN observacaoproduto AS o ON p.id = o.ProdutoId");
            sql.AppendLine("    LEFT JOIN usuarios AS u ON u.id = f.UserId");
            sql.AppendLine("WHERE " + FiltroEmpresa("","F"));
            sql.AppendLine("ORDER BY P.Referencia");
            
            var cn = new DapperConnection<FichaTecnicaView>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaView(), sql.ToString());
        }

        public FichaTecnica GetByProduto(int produtoId)
        {
            FichaTecnica result = new FichaTecnica();
            _cn.ExecuteToModel("ProdutoId = " + produtoId.ToString() + " AND " + FiltroEmpresa(), ref result);
            return result;
        }

        public IEnumerable<FichaTecnicaOperacaoProdutoView> GetByOperacoesProduto(int operacaoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("        F.TempoTotal,");
            sql.AppendLine("        F.CustoFaccao,");
            sql.AppendLine("        O.DescricaoOperacao,");
            sql.AppendLine("        F.ProdutoId,");
            sql.AppendLine("        P.Referencia AS ProdutoReferencia,");
            sql.AppendLine("		P.Descricao AS ProdutoDescricao,");
            sql.AppendLine("    	G.descricao AS ProdutoGrupoDescricao,");
            sql.AppendLine("    	G.Id AS ProdutoGrupoId,");
            sql.AppendLine("        O.*,");
            sql.AppendLine("		S.Descricao AS SetorDescricao,");
            sql.AppendLine("        M.MovimentosDaOperacaoId,");
            sql.AppendLine("        MO.Referencia AS MovimentosDaOperacaReferencia,");
            sql.AppendLine("        M.TempoMovimento,");
            sql.AppendLine("   		OP.Descricao AS Maquina,");
            sql.AppendLine("   		OP.Manual AS Manual");
            sql.AppendLine("FROM FichaTecnica F");
            sql.AppendLine("	INNER JOIN FichaTecnicaOperacao O ON O.FichaTecnicaId = F.Id");
            sql.AppendLine("	INNER JOIN Produtos P ON P.Id = F.ProdutoId");
            sql.AppendLine("	INNER JOIN setores S ON S.Id = O.SetorId");
            sql.AppendLine("    INNER JOIN operacaopadrao OP ON OP.Id = O.OperacaoPadraoId");
            sql.AppendLine("	LEFT JOIN grupoprodutos G ON G.id = P.IdGrupo");
            sql.AppendLine("	LEFT JOIN fichatecnicaoperacaomovimento M ON M.FichaTecnicaOperacaoId = O.Id");
            sql.AppendLine("    LEFT JOIN movimentosdaoperacao MO ON MO.Id = M.MovimentosDaOperacaoId");
            sql.AppendLine("WHERE 	F.Ativo = 1 AND O.OperacaoPadraoId = " + operacaoId);
            sql.AppendLine("        AND " + FiltroEmpresa("", "F"));

            sql.AppendLine("ORDER BY P.Referencia, O.Numero, M.Id");

            List<FichaTecnicaOperacaoProdutoView> result = new List<FichaTecnicaOperacaoProdutoView>();

            DataTable dt = _cn.ExecuteToDataTable(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                FichaTecnicaOperacaoProdutoView operacao = null;
                int fichaOperacaoId = 0;

                foreach (DataRow row in dt.Rows)
                {
                    

                    bool temMovimento = false;

                    if (fichaOperacaoId != row["Id"].ToInt())
                    {
                        //operacaoId = row["Id"].ToInt();
                        fichaOperacaoId = row["Id"].ToInt();

                        operacao = new FichaTecnicaOperacaoProdutoView();
                        operacao.CapacidadePecas = row["CapacidadePecas"].ToInt();
                        operacao.Ativo = true;
                        operacao.Id = fichaOperacaoId;
                        operacao.ProdutoDescricao = row["ProdutoDescricao"].ToString();
                        operacao.ProdutoReferencia = row["ProdutoReferencia"].ToString();
                        operacao.Numero = row["Numero"].ToInt();
                        operacao.OperacaoPadraoDescricao = row["OperacaoPadraoDescricao"].ToString();
                        operacao.OperacaoPadraoId = row["OperacaoPadraoId"].ToInt();
                        operacao.Pontadas = row["Pontadas"].ToDecimal();
                        operacao.SetorDescricao = row["SetorDescricao"].ToString();
                        operacao.SetorId = row["SetorId"].ToInt();
                        operacao.TempoCalculado = row["TempoCalculado"].ToDecimal();
                        operacao.TempoCosturaComAviamento = row["TempoCosturaComAviamento"].ToDecimal();
                        operacao.TempoCosturaSemAviamento = row["TempoCosturaSemAviamento"].ToDecimal();
                        operacao.TempoCronometrado = row["TempoCronometrado"].ToDecimal();
                        operacao.TempoEmSegundos = row["TempoEmSegundos"].ToDecimal();
                        operacao.UtilizaAviamento = Convert.ToBoolean(row["UtilizaAviamento"]);
                        operacao.Maquina = row["Maquina"].ToString();
                        operacao.DescricaoOperacao = row["DescricaoOperacao"].ToString();
                        operacao.Manual = row["Manual"].ToInt() == 1 ? true : false;
                        operacao.Movimentos = new List<FichaTecnicaOperacaoMovimentoView>();
                        result.Add(operacao);
                        temMovimento = (row["MovimentosDaOperacaoId"].ToInt() > 0);
                    }
                    else
                    {
                        temMovimento = true;
                    }

                    if (temMovimento)
                    {
                        FichaTecnicaOperacaoMovimentoView movimento = new FichaTecnicaOperacaoMovimentoView();
                        movimento.FichaTecnicaOperacaoId = operacao.Id;
                        movimento.MovimentosDaOperacaoId = row["MovimentosDaOperacaoId"].ToInt();
                        movimento.MovimentosDaOperacaoReferencia = row["MovimentosDaOperacaReferencia"].ToString();
                        movimento.TempoMovimento = row["TempoMovimento"].ToDecimal();
                        operacao.Movimentos.Add(movimento);
                    }

                }
            }

            return result;
        }

        public IEnumerable<FichaTecnicaView> GetByFiltros(int[] produtosIds, int[] operacoesIds, string titulo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("        F.TempoTotal,");
            sql.AppendLine("        F.CustoFaccao,");
            sql.AppendLine("        O.DescricaoOperacao,");
            sql.AppendLine("        F.ProdutoId,");
            sql.AppendLine("        P.Referencia AS ProdutoReferencia,");
            sql.AppendLine("		P.Descricao AS ProdutoDescricao,");
            sql.AppendLine("    	G.descricao AS ProdutoGrupoDescricao,");
            sql.AppendLine("    	G.Id AS ProdutoGrupoId,");
            sql.AppendLine("        O.*,");
            sql.AppendLine("		S.Descricao AS SetorDescricao,");
            sql.AppendLine("        M.MovimentosDaOperacaoId,");
            sql.AppendLine("        MO.Referencia AS MovimentosDaOperacaReferencia,");
            sql.AppendLine("        M.TempoMovimento,");
            sql.AppendLine("   		OP.Referencia AS Maquina,");
            sql.AppendLine("   		OP.Manual AS Manual");
            sql.AppendLine("FROM FichaTecnica F");
            sql.AppendLine("	INNER JOIN FichaTecnicaOperacao O ON O.FichaTecnicaId = F.Id");
            sql.AppendLine("	INNER JOIN Produtos P ON P.Id = F.ProdutoId");
            sql.AppendLine("	INNER JOIN setores S ON S.Id = O.SetorId");
            sql.AppendLine("    INNER JOIN operacaopadrao OP ON OP.Id = O.OperacaoPadraoId");
            sql.AppendLine("	LEFT JOIN grupoprodutos G ON G.id = P.IdGrupo");
            sql.AppendLine("	LEFT JOIN fichatecnicaoperacaomovimento M ON M.FichaTecnicaOperacaoId = O.Id");
            sql.AppendLine("    LEFT JOIN movimentosdaoperacao MO ON MO.Id = M.MovimentosDaOperacaoId");
            sql.AppendLine("WHERE 	F.Ativo = 1");
            sql.AppendLine("        AND " + FiltroEmpresa("", "F"));
            
            if (! string.IsNullOrWhiteSpace(titulo))
                sql.AppendLine("		AND (LOWER(O.OperacaoPadraoDescricao) like '%" + titulo.ToLower() + "%') ");

            if (operacoesIds != null)
                sql.AppendLine("		AND (O.OperacaoPadraoId IN (" + string.Join(", ", operacoesIds) + ")) ");
            
            
            sql.AppendLine("		AND F.ProdutoId IN (" + string.Join(", ", produtosIds) + ")");

            sql.AppendLine("ORDER BY P.Referencia, O.Numero, M.Id");

            List<FichaTecnicaView> result = new List<FichaTecnicaView>();

            DataTable dt = _cn.ExecuteToDataTable(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                int produtoId = 0;
                int operacaoId = 0;

                FichaTecnicaView ficha = null;
                FichaTecnicaOperacaoView operacao = null;

                foreach (DataRow row in dt.Rows)
                {
                    if (produtoId != row["ProdutoId"].ToInt())
                    {
                        produtoId = row["ProdutoId"].ToInt();
                        ficha = new FichaTecnicaView();
                        ficha.Id = row["FichaTecnicaId"].ToInt();
                        ficha.GrupoProdutoId = row["ProdutoGrupoId"].ToInt();
                        ficha.GrupoProdutoDescricao = row["ProdutoGrupoDescricao"].ToString();
                        ficha.Ativo = true;
                        ficha.ProdutoId = row["ProdutoId"].ToInt();
                        ficha.ProdutoDescricao = row["ProdutoDescricao"].ToString();
                        ficha.ProdutoReferencia = row["ProdutoReferencia"].ToString();
                        ficha.TempoTotal = row["TempoTotal"].ToDecimal();
                        ficha.CustoFaccao = row["CustoFaccao"].ToDecimal();
                        ficha.Operacoes = new List<FichaTecnicaOperacaoView>();
                        result.Add(ficha);
                    }

                    bool temMovimento = false;

                    if (operacaoId != row["Id"].ToInt())
                    {
                        operacaoId = row["Id"].ToInt();

                        operacao = new FichaTecnicaOperacaoView();
                        operacao.FichaTecnicaId = ficha.Id;
                        operacao.CapacidadePecas = row["CapacidadePecas"].ToInt();
                        operacao.Ativo = true;
                        operacao.Id = operacaoId;
                        operacao.Numero = row["Numero"].ToInt();
                        operacao.OperacaoPadraoDescricao = row["OperacaoPadraoDescricao"].ToString();
                        operacao.OperacaoPadraoId = row["OperacaoPadraoId"].ToInt();
                        operacao.Pontadas = row["Pontadas"].ToDecimal();
                        operacao.SetorDescricao = row["SetorDescricao"].ToString();
                        operacao.SetorId = row["SetorId"].ToInt();
                        operacao.BalanceamentoId = row["BalanceamentoId"].ToInt();
                        operacao.TempoCalculado = row["TempoCalculado"].ToDecimal();
                        operacao.TempoCosturaComAviamento = row["TempoCosturaComAviamento"].ToDecimal();
                        operacao.TempoCosturaSemAviamento = row["TempoCosturaSemAviamento"].ToDecimal();
                        operacao.TempoCronometrado = row["TempoCronometrado"].ToDecimal();
                        operacao.TempoEmSegundos = row["TempoEmSegundos"].ToDecimal();
                        operacao.UtilizaAviamento = Convert.ToBoolean(row["UtilizaAviamento"]);
                        operacao.Maquina = row["Maquina"].ToString();
                        operacao.DescricaoOperacao = row["DescricaoOperacao"].ToString();
                        operacao.Manual = row["Manual"].ToInt() == 1 ? true:false;
                        operacao.Movimentos = new List<FichaTecnicaOperacaoMovimentoView>();
                        ficha.Operacoes.Add(operacao);

                        temMovimento = (row["MovimentosDaOperacaoId"].ToInt() > 0);    
                    }
                    else
                    {
                        temMovimento = true;
                    }

                    if (temMovimento)
                    {
                        FichaTecnicaOperacaoMovimentoView movimento = new FichaTecnicaOperacaoMovimentoView();
                        movimento.FichaTecnicaOperacaoId = operacao.Id;
                        movimento.MovimentosDaOperacaoId = row["MovimentosDaOperacaoId"].ToInt();
                        movimento.MovimentosDaOperacaoReferencia = row["MovimentosDaOperacaReferencia"].ToString();
                        movimento.TempoMovimento = row["TempoMovimento"].ToDecimal();
                        operacao.Movimentos.Add(movimento);
                    }

                }
            }

            return result;
        }

        public IEnumerable<FichaTecnicaView> GetByFiltrosProdutos(int produto)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine("        F.TempoTotal,");
            sql.AppendLine("        F.CustoFaccao,");
            sql.AppendLine("        O.DescricaoOperacao,");
            sql.AppendLine("        F.ProdutoId,");
            sql.AppendLine("        P.Referencia AS ProdutoReferencia,");
            sql.AppendLine("		P.Descricao AS ProdutoDescricao,");
            sql.AppendLine("    	G.descricao AS ProdutoGrupoDescricao,");
            sql.AppendLine("    	G.Id AS ProdutoGrupoId,");
            sql.AppendLine("        O.*,");
            sql.AppendLine("		S.Descricao AS SetorDescricao,");
            sql.AppendLine("        M.MovimentosDaOperacaoId,");
            sql.AppendLine("        MO.Referencia AS MovimentosDaOperacaReferencia,");
            sql.AppendLine("        M.TempoMovimento,");
            sql.AppendLine("   		OP.Descricao AS Maquina,");
            sql.AppendLine("   		OP.Manual AS Manual");
            sql.AppendLine("FROM FichaTecnica F");
            sql.AppendLine("	INNER JOIN FichaTecnicaOperacao O ON O.FichaTecnicaId = F.Id");
            sql.AppendLine("	INNER JOIN Produtos P ON P.Id = F.ProdutoId");
            sql.AppendLine("	INNER JOIN setores S ON S.Id = O.SetorId");
            sql.AppendLine("    INNER JOIN operacaopadrao OP ON OP.Id = O.OperacaoPadraoId");
            sql.AppendLine("	LEFT JOIN grupoprodutos G ON G.id = P.IdGrupo");
            sql.AppendLine("	LEFT JOIN fichatecnicaoperacaomovimento M ON M.FichaTecnicaOperacaoId = O.Id");
            sql.AppendLine("    LEFT JOIN movimentosdaoperacao MO ON MO.Id = M.MovimentosDaOperacaoId");
            sql.AppendLine("WHERE 	F.Ativo = 1");
            sql.AppendLine("        AND " + FiltroEmpresa("", "F"));

            sql.AppendLine("		AND F.ProdutoId = " + produto + "");

            sql.AppendLine("ORDER BY P.Referencia, O.Numero, M.Id");

            List<FichaTecnicaView> result = new List<FichaTecnicaView>();

            DataTable dt = _cn.ExecuteToDataTable(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                int produtoId = 0;
                int operacaoId = 0;

                FichaTecnicaView ficha = null;
                FichaTecnicaOperacaoView operacao = null;

                foreach (DataRow row in dt.Rows)
                {
                    if (produtoId != row["ProdutoId"].ToInt())
                    {
                        produtoId = row["ProdutoId"].ToInt();
                        ficha = new FichaTecnicaView();
                        ficha.Id = row["FichaTecnicaId"].ToInt();
                        ficha.GrupoProdutoId = row["ProdutoGrupoId"].ToInt();
                        ficha.GrupoProdutoDescricao = row["ProdutoGrupoDescricao"].ToString();
                        ficha.Ativo = true;
                        ficha.ProdutoId = row["ProdutoId"].ToInt();
                        ficha.ProdutoDescricao = row["ProdutoDescricao"].ToString();
                        ficha.ProdutoReferencia = row["ProdutoReferencia"].ToString();
                        ficha.TempoTotal = row["TempoTotal"].ToDecimal();
                        ficha.CustoFaccao = row["CustoFaccao"].ToDecimal();
                        ficha.Operacoes = new List<FichaTecnicaOperacaoView>();
                        result.Add(ficha);
                    }

                    bool temMovimento = false;

                    if (operacaoId != row["Id"].ToInt())
                    {
                        operacaoId = row["Id"].ToInt();

                        operacao = new FichaTecnicaOperacaoView();
                        operacao.FichaTecnicaId = ficha.Id;
                        operacao.CapacidadePecas = row["CapacidadePecas"].ToInt();
                        operacao.Ativo = true;
                        operacao.Id = operacaoId;
                        operacao.Numero = row["Numero"].ToInt();
                        operacao.OperacaoPadraoDescricao = row["OperacaoPadraoDescricao"].ToString();
                        operacao.OperacaoPadraoId = row["OperacaoPadraoId"].ToInt();
                        operacao.Pontadas = row["Pontadas"].ToDecimal();
                        operacao.SetorDescricao = row["SetorDescricao"].ToString();
                        operacao.SetorId = row["SetorId"].ToInt();
                        operacao.TempoCalculado = row["TempoCalculado"].ToDecimal();
                        operacao.TempoCosturaComAviamento = row["TempoCosturaComAviamento"].ToDecimal();
                        operacao.TempoCosturaSemAviamento = row["TempoCosturaSemAviamento"].ToDecimal();
                        operacao.TempoCronometrado = row["TempoCronometrado"].ToDecimal();
                        operacao.TempoEmSegundos = row["TempoEmSegundos"].ToDecimal();
                        operacao.UtilizaAviamento = Convert.ToBoolean(row["UtilizaAviamento"]);
                        operacao.Maquina = row["Maquina"].ToString();
                        operacao.DescricaoOperacao = row["DescricaoOperacao"].ToString();
                        operacao.Manual = row["Manual"].ToInt() == 1 ? true : false;
                        operacao.Movimentos = new List<FichaTecnicaOperacaoMovimentoView>();
                        ficha.Operacoes.Add(operacao);

                        temMovimento = (row["MovimentosDaOperacaoId"].ToInt() > 0);
                    }
                    else
                    {
                        temMovimento = true;
                    }

                    if (temMovimento)
                    {
                        FichaTecnicaOperacaoMovimentoView movimento = new FichaTecnicaOperacaoMovimentoView();
                        movimento.FichaTecnicaOperacaoId = operacao.Id;
                        movimento.MovimentosDaOperacaoId = row["MovimentosDaOperacaoId"].ToInt();
                        movimento.MovimentosDaOperacaoReferencia = row["MovimentosDaOperacaReferencia"].ToString();
                        movimento.TempoMovimento = row["TempoMovimento"].ToDecimal();
                        operacao.Movimentos.Add(movimento);
                    }

                }
            }

            return result;
        }


        public FichaTecnicaView GetByIdView(int id)
        {
            StringBuilder sql = new StringBuilder();
            var cn = new DapperConnection<FichaTecnicaView>();

            sql.AppendLine("SELECT ");
            sql.AppendLine("        F.TempoTotal,");
            sql.AppendLine("        F.CustoFaccao,");
            sql.AppendLine("        O.DescricaoOperacao,");
            sql.AppendLine("        F.ProdutoId,");
            sql.AppendLine("        P.Referencia AS ProdutoReferencia,");
            sql.AppendLine("		P.Descricao AS ProdutoDescricao,");
            sql.AppendLine("    	G.descricao AS ProdutoGrupoDescricao,");
            sql.AppendLine("    	G.Id AS ProdutoGrupoId,");
            sql.AppendLine("        O.*,");
            sql.AppendLine("		S.Descricao AS SetorDescricao,");
            sql.AppendLine("        M.MovimentosDaOperacaoId,");
            sql.AppendLine("        MO.Referencia AS MovimentosDaOperacaReferencia,");
            sql.AppendLine("        M.TempoMovimento,");
            sql.AppendLine("   		OP.Descricao AS Maquina");
            sql.AppendLine("FROM FichaTecnica F");
            sql.AppendLine("	INNER JOIN FichaTecnicaOperacao O ON O.FichaTecnicaId = F.Id");
            sql.AppendLine("	INNER JOIN Produtos P ON P.Id = F.ProdutoId");
            sql.AppendLine("	INNER JOIN setores S ON S.Id = O.SetorId");
            sql.AppendLine("    INNER JOIN operacaopadrao OP ON OP.Id = O.OperacaoPadraoId");
            sql.AppendLine("	LEFT JOIN grupoprodutos G ON G.id = P.IdGrupo");
            sql.AppendLine("	LEFT JOIN fichatecnicaoperacaomovimento M ON M.FichaTecnicaOperacaoId = O.Id");
            sql.AppendLine("    LEFT JOIN movimentosdaoperacao MO ON MO.Id = M.MovimentosDaOperacaoId");
            sql.AppendLine("WHERE 	F.Ativo = 1");
            sql.AppendLine("        AND " + FiltroEmpresa("", "F"));


            sql.AppendLine("		AND F.Id = " + id);

            sql.AppendLine("ORDER BY P.Referencia, O.Numero, M.Id");

            FichaTecnicaView result = new FichaTecnicaView();


            DataTable dt = _cn.ExecuteToDataTable(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                int produtoId = 0;
                int operacaoId = 0;

                FichaTecnicaView ficha = null;
                FichaTecnicaOperacaoView operacao = null;

                foreach (DataRow row in dt.Rows)
                {
                    if (produtoId != row["ProdutoId"].ToInt())
                    {
                        produtoId = row["ProdutoId"].ToInt();
                        ficha = new FichaTecnicaView();
                        ficha.Id = row["FichaTecnicaId"].ToInt();
                        ficha.GrupoProdutoId = row["ProdutoGrupoId"].ToInt();
                        ficha.GrupoProdutoDescricao = row["ProdutoGrupoDescricao"].ToString();
                        ficha.Ativo = true;
                        ficha.ProdutoId = row["ProdutoId"].ToInt();
                        ficha.ProdutoDescricao = row["ProdutoDescricao"].ToString();
                        ficha.ProdutoReferencia = row["ProdutoReferencia"].ToString();
                        ficha.TempoTotal = row["TempoTotal"].ToDecimal();
                        ficha.CustoFaccao = row["CustoFaccao"].ToDecimal();
                        ficha.Operacoes = new List<FichaTecnicaOperacaoView>();
                        result = ficha;
                    }

                    bool temMovimento = false;

                    if (operacaoId != row["Id"].ToInt())
                    {
                        operacaoId = row["Id"].ToInt();

                        operacao = new FichaTecnicaOperacaoView();
                        operacao.FichaTecnicaId = ficha.Id;
                        operacao.CapacidadePecas = row["CapacidadePecas"].ToInt();
                        operacao.Ativo = true;
                        operacao.Id = operacaoId;
                        operacao.Numero = row["Numero"].ToInt();
                        operacao.OperacaoPadraoDescricao = row["OperacaoPadraoDescricao"].ToString();
                        operacao.OperacaoPadraoId = row["OperacaoPadraoId"].ToInt();
                        operacao.Pontadas = row["Pontadas"].ToDecimal();
                        operacao.SetorDescricao = row["SetorDescricao"].ToString();
                        operacao.SetorId = row["SetorId"].ToInt();
                        operacao.TempoCalculado = row["TempoCalculado"].ToDecimal();
                        operacao.TempoCosturaComAviamento = row["TempoCosturaComAviamento"].ToDecimal();
                        operacao.TempoCosturaSemAviamento = row["TempoCosturaSemAviamento"].ToDecimal();
                        operacao.TempoCronometrado = row["TempoCronometrado"].ToDecimal();
                        operacao.TempoEmSegundos = row["TempoEmSegundos"].ToDecimal();
                        operacao.UtilizaAviamento = Convert.ToBoolean(row["UtilizaAviamento"]);
                        operacao.Maquina = row["Maquina"].ToString();
                        operacao.DescricaoOperacao = row["DescricaoOperacao"].ToString();
                        operacao.Movimentos = new List<FichaTecnicaOperacaoMovimentoView>();
                        ficha.Operacoes.Add(operacao);

                        temMovimento = (row["MovimentosDaOperacaoId"].ToInt() > 0);
                    }
                    else
                    {
                        temMovimento = true;
                    }

                    if (temMovimento)
                    {
                        FichaTecnicaOperacaoMovimentoView movimento = new FichaTecnicaOperacaoMovimentoView();
                        movimento.FichaTecnicaOperacaoId = operacao.Id;
                        movimento.MovimentosDaOperacaoId = row["MovimentosDaOperacaoId"].ToInt();
                        movimento.MovimentosDaOperacaoReferencia = row["MovimentosDaOperacaReferencia"].ToString();
                        movimento.TempoMovimento = row["TempoMovimento"].ToDecimal();
                        operacao.Movimentos.Add(movimento);
                    }

                }
            }

            return result;
        }

        public IEnumerable<FichaTecnicaRelatorio> GetAllViewByFiltro(FiltroFichaTecnica filtro)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  ");
            SQL.AppendLine("P.id as ProdutoId,P.Descricao AS DescProduto, P.Referencia AS RefProduto, ");
            SQL.AppendLine("  T.Abreviatura AS DescTamanho, T.Id as TamanhoId, ");//I.imagem as Imagem,
            SQL.AppendLine("F.id as FichaId, o.Observacao as ObsFicha, FM.Id as FichaMaterialID, ");
            SQL.AppendLine("P.QtdPacote , P.TempoPacote, F.TempoTotal, FM.total as CustoMaterial,F.QuebraManual as Quebra, FM.QuebraManual as  QuebraMaterial,");
            SQL.AppendLine("(SELECT Count(O.id) FROM FichaTecnicaOperacao O WHERE O.FichaTecnicaId = F.Id) as QtdOperacoes");
            SQL.AppendLine("FROM 	fichatecnica F");
            SQL.AppendLine("    INNER JOIN produtos P ON P.id = F.ProdutoId");
            SQL.AppendLine("    INNER JOIN produtodetalhes PD ON P.id = PD.IdProduto AND PD.Inutilizado = 0");
            SQL.AppendLine("    LEFT JOIN fichatecnicadomaterial FM ON P.id = FM.ProdutoId");
            SQL.AppendLine("    LEFT JOIN Tamanhos T ON T.Id = PD.IdTamanho ");
            SQL.AppendLine("    LEFT JOIN observacaoproduto o ON o.ProdutoId = P.id ");
            //SQL.AppendLine("LEFT JOIN imagens i ON (i.idProduto = p.Id)");
            //SQL.AppendLine(" where F.ProdutoId in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");
            SQL.AppendLine("WHERE " + FiltroEmpresa("", "F"));

            if (filtro.Produtos != null && filtro.Produtos.Count > 0)
                SQL.AppendLine(" AND P.id in (" + string.Join(",", filtro.Produtos.ToArray()) + ")");

            if (filtro.Catalogo != null && filtro.Catalogo.Count > 0)
                SQL.AppendLine(" AND P.idCatalogo in (" + string.Join(",", filtro.Catalogo.ToArray()) + ")");

            if (filtro.Grupo != null && filtro.Grupo.Count > 0)
                SQL.AppendLine(" AND P.idGrupo in (" + string.Join(",", filtro.Grupo.ToArray()) + ")");

            if (filtro.Segmento != null && filtro.Segmento.Count > 0)
                SQL.AppendLine(" AND P.idSegmento in (" + string.Join(",", filtro.Segmento.ToArray()) + ")");

            if (filtro.Colecao != null && filtro.Colecao.Count > 0)
                SQL.AppendLine(" AND P.idColecao in (" + string.Join(",", filtro.Colecao.ToArray()) + ")");

            if ((filtro.DoAno != "" && filtro.DoAno != "0000") || (filtro.AteAno != "" && filtro.AteAno != "9999"))
                SQL.AppendLine("        AND p.ano BETWEEN  '" + filtro.DoAno + "' AND '" + filtro.AteAno + "' ");
           

            if(filtro.Listagem)
                SQL.Append("GROUP BY P.Id");
            else
                SQL.AppendLine("GROUP BY P.Id, PD.IdTamanho");

            if (filtro.Ordenar == 0)
            {
                SQL.AppendLine(" ORDER BY P.Referencia");
            }
            else
            {
                SQL.AppendLine(" ORDER BY P.Descricao");
            }

            var cn = new DapperConnection<FichaTecnicaRelatorio>();
            return cn.ExecuteStringSqlToList(new FichaTecnicaRelatorio(), SQL.ToString());
        }

        public void Update(FichaTecnica ficha)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE fichatecnica SET ");
            SQL.AppendLine("Ativo = ");
            SQL.Append(ficha.Ativo);
            //SQL.AppendLine(", UtilizaQuebra = ");
            //SQL.Append(ficha.UtilizaQuebra);
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(ficha.Id);

            _cn.ExecuteNonQuery(SQL.ToString());
        }
    }
}
