using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Repositories;
using Vestillo.Connection;
using Vestillo.Business.Models.Views;
using Vestillo.Business;
using System.Data;
using Vestillo.Business.Service;

namespace Vestillo.Business.Repositories
{
    public class PacoteProducaoRepository: GenericRepository<PacoteProducao>
    {
        public PacoteProducaoRepository()
            : base(new DapperConnection<PacoteProducao>())
        {
        }

        public List<PacoteProducaoView> GetByView()
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("seg.descricao as ProdutoSegmento,");            
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.observacao as ObservacaoOrdem,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("g.data as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            //SQL.AppendLine("(SUM(IF(oo.Data > '1-1-0001', 1, 0))/COUNT(gp.Id)) as Concluido,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");      
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");

            // ALEX 22-12-2020 SQL.AppendLine("INNER JOIN grupooperacoes gp ON gp.GrupoPacoteId = g.Id");

            SQL.AppendLine("LEFT JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("LEFT JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("LEFT JOIN segmentos seg ON pr.Idsegmento = seg.id");
            //SQL.AppendLine("LEFT JOIN operacaooperadora OO ON OO.PacoteId = PA.Id && oo.OperacaoId = gp.OperacaoPadraoId");
            SQL.AppendLine(" WHERE" + FiltroEmpresa(" op.EmpresaId "));
            SQL.AppendLine("GROUP BY pa.id");
            SQL.AppendLine("ORDER BY pa.Referencia DESC");

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString()).ToList();
        }

        public IEnumerable<PacoteProducaoView> GetPacotesBrowse(FiltroRelatorioPacote filtro) // ALEX 22-12-2020
        {
            var cn = new DapperConnection<PacoteProducaoView>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("seg.descricao as ProdutoSegmento,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("gp.data as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("g.abreviatura as GrupoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("op.AlmoxarifadoId as AlmoxarifadoId,");
            SQL.AppendLine("a.descricao as AlmoxarifadoDescricao,");
            SQL.AppendLine("op.observacao as ObservacaoOrdem");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("INNER JOIN grupopacote gp  ON gp.id = pa.grupopacoteId");            
            SQL.AppendLine("LEFT JOIN grupoprodutos g ON pr.idgrupo = g.id");
            SQL.AppendLine("LEFT JOIN segmentos seg ON pr.Idsegmento = seg.id");
            //SQL.AppendLine("LEFT JOIN operacaooperadora OO ON OO.PacoteId = PA.Id && oo.OperacaoId = go.OperacaoPadraoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" op.EmpresaId "));

            if (filtro.OrdensProducao != null && filtro.OrdensProducao.Count() > 0)
                SQL.AppendLine(" AND iop.OrdemProducaoId IN (" + string.Join(", ", filtro.OrdensProducao) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine("        AND iop.ProdutoId IN (" + string.Join(", ", filtro.Produtos) + ")");

            if (filtro.Cores != null && filtro.Cores.Count() > 0)
                SQL.AppendLine("        AND iop.CorId IN (" + string.Join(", ", filtro.Cores) + ")");

            if (filtro.Tamanhos != null && filtro.Tamanhos.Count() > 0)
                SQL.AppendLine("        AND iop.TamanhoId IN (" + string.Join(", ", filtro.Tamanhos) + ")");

            if (filtro.Pacote != null && filtro.Pacote.Count() > 0)
                SQL.AppendLine("        AND pa.Id IN (" + string.Join(", ", filtro.Pacote) + ")");

            if (filtro.DaEmissao != "" || filtro.AteEmissao != "")
                SQL.AppendLine("        AND DATE(gp.Data) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");

            if (filtro.DaEntrada != "" || filtro.AteEntrada != "")
                SQL.AppendLine("        AND DATE(pa.DataEntrada) BETWEEN  '" + filtro.DaEntrada + "' AND '" + filtro.AteEntrada + "' ");

            if (filtro.DaSaida != "" || filtro.AteSaida != "")
                SQL.AppendLine("        AND DATE(pa.DataSaida) BETWEEN  '" + filtro.DaSaida + "' AND '" + filtro.AteSaida + "' ");

            if (filtro.Aberto || filtro.Producao || filtro.Concluido || filtro.Finalizado)
            {
                SQL.AppendLine(" AND (");

                if (filtro.Aberto)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Aberto);

                if (filtro.Aberto && filtro.Producao)
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Producao);
                else if (filtro.Producao)
                    SQL.Append("pa.Status = " + (int)enumStatusPacotesProducao.Producao);

                if (filtro.Concluido && (filtro.Producao || filtro.Aberto))
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Concluido);
                else if (filtro.Concluido)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Concluido);

                if (filtro.Finalizado && (filtro.Producao || filtro.Aberto || filtro.Concluido))
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);
                else if (filtro.Finalizado)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);

                SQL.Append(")");
            }
            else
            {
                SQL.AppendLine(" AND pa.Status = -1");
            }

            SQL.AppendLine(" GROUP BY pa.id, iop.OrdemProducaoId, iop.ProdutoId, iop.CorId");

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString());
        }

        public List<PacoteProducaoView> GetByViewId(List<int> id)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("g.data as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("WHERE  pa.id IN (" + id + ")" + " AND" +  FiltroEmpresa(" op.EmpresaId "));

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString()).ToList();
        }

        public PacoteProducaoView GetByIdView(int id)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("gp.data as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("op.AlmoxarifadoId as AlmoxarifadoId,");
            SQL.AppendLine("a.descricao as AlmoxarifadoDescricao,");
            SQL.AppendLine("gp.Usuario as Usuario");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("LEFT JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("LEFT JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("INNER JOIN grupopacote gp ON gp.id = pa.grupopacoteid");
            SQL.AppendLine("WHERE pa.id = " + id + " AND " + FiltroEmpresa(" op.EmpresaId "));

            var pacote = new PacoteProducaoView();
            cn.ExecuteToModel(ref pacote, SQL.ToString());

            return pacote;
        }


        public List<PacoteProducaoView> GetByData(DateTime data)
        {
            decimal quantidade = 0;
            decimal qtddefeito = 0;
            decimal quantidadealternativa = 0;

            var cn = new DapperConnection<PacoteProducaoView>();           
            var SQL = new StringBuilder();

            SQL.AppendLine("SELECT 	quantidade, qtddefeito,quantidadealternativa,SUM(pa.quantidade * gp.tempo  + pr.tempopacote) as TempoPacote ");
            SQL.AppendLine("FROM 	pacotes pa " );
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId ");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON gp.GrupoPacoteId = g.Id ");
            SQL.AppendLine("  INNER JOIN fichatecnica ON fichatecnica.ProdutoId = pa.produtoid ");
            SQL.AppendLine(" WHERE pa.status = 6 and (pa.EntradaFaccao = 0 or pa.EntradaFaccao IS NULL) AND  date(pa.datasaida) = '" + data.ToString("yyyy-MM-dd") + "'" + " AND " + FiltroEmpresa(" fichatecnica.EmpresaId "));
            SQL.AppendLine(" GROUP BY pa.id");
           
           
            

            /*
            SQL.AppendLine("SELECT 	SUM(pa.quantidade) as quantidade, SUM(pa.qtddefeito) as qtddefeito, SUM(pa.quantidadealternativa) as quantidadealternativa, SUM(pa.quantidade * gp.tempo) as TempoPacote");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes gp ON gp.GrupoPacoteId = g.Id");
            SQL.AppendLine("INNER JOIN operacaooperadora on operacaooperadora.PacoteId = pa.id");
            SQL.AppendLine("INNER JOIN funcionarios on funcionarios.Id = operacaooperadora.FuncionarioId");            
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" SUBSTRING(operacaooperadora.data,1,10) = '" + data.ToString("yyyy-MM-dd")  +  "'" + " AND " + FiltroEmpresa(" funcionarios.EmpresaId "));
             * */
            /*
            SQL.AppendLine("GROUP BY pa.id");
            SQL.AppendLine("ORDER BY pa.Id DESC");
            */


            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString()).ToList();
        }

        public List<PacoteProducaoView> GetByOrdemIdView(int ordemId)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("op.dataemissao as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("op.AlmoxarifadoId as AlmoxarifadoId,");
            SQL.AppendLine("a.descricao as AlmoxarifadoDescricao");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("WHERE op.id = " + ordemId + " AND " + FiltroEmpresa(" op.EmpresaId "));

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString()).ToList();
        }

        public PacoteProducaoView GetByViewReferencia(string referencia)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("op.dataemissao as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("op.AlmoxarifadoId as AlmoxarifadoId,");
            SQL.AppendLine("a.descricao as AlmoxarifadoDescricao");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("LEFT JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("LEFT JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("LEFT JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("WHERE pa.referencia = " + referencia + " AND " + FiltroEmpresa(" op.EmpresaId "));

            PacoteProducaoView pacote = new PacoteProducaoView();
            cn.ExecuteToModel(ref pacote, SQL.ToString());
            return pacote;
        }

        public PacoteProducaoView GetByViewReferenciaJunior(string referencia)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("WHERE pa.referencia = " + referencia);

            PacoteProducaoView pacote = new PacoteProducaoView();
            cn.ExecuteToModel(ref pacote, SQL.ToString());
            return pacote;
        }

        public IEnumerable<PacoteProducaoView> GetByListViewReferencia(string referencia)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("op.dataemissao as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("op.AlmoxarifadoId as AlmoxarifadoId,");
            SQL.AppendLine("a.descricao as AlmoxarifadoDescricao");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            if (referencia != null && referencia != "")
            {
                SQL.AppendLine("WHERE pa.referencia like '%" + referencia + "%'" + " AND " + FiltroEmpresa(" op.EmpresaId "));
            }
            else
            {
                SQL.AppendLine("WHERE " + FiltroEmpresa(" op.EmpresaId "));
            }

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString());
        }

        public IEnumerable<PacoteProducaoView> GetPacotesRelatorio(FiltroRelatorioPacote filtro)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
           
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("seg.descricao as ProdutoSegmento,");   
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("gp.data as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("g.abreviatura as GrupoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("op.AlmoxarifadoId as AlmoxarifadoId,");
            SQL.AppendLine("a.descricao as AlmoxarifadoDescricao,");
            SQL.AppendLine("SUM(go.tempo*pa.quantidade + pr.tempopacote) as TempoTotal,");
            //SQL.AppendLine("(SUM(IF(oo.Data > '1-1-0001', 1, 0))/COUNT(gp.Id)) as Concluido,");
            SQL.AppendLine("SUM(go.tempo) as TempoUnitario");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("INNER JOIN grupopacote gp  ON gp.id = pa.grupopacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes go ON gp.id = go.grupopacoteId");
            SQL.AppendLine("LEFT JOIN grupoprodutos g ON pr.idgrupo = g.id");
            SQL.AppendLine("LEFT JOIN segmentos seg ON pr.Idsegmento = seg.id");
            //SQL.AppendLine("LEFT JOIN operacaooperadora OO ON OO.PacoteId = PA.Id && oo.OperacaoId = go.OperacaoPadraoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" op.EmpresaId "));

            if (filtro.OrdensProducao != null && filtro.OrdensProducao.Count() > 0)
            SQL.AppendLine(" AND iop.OrdemProducaoId IN (" + string.Join(", ", filtro.OrdensProducao) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine("        AND iop.ProdutoId IN (" + string.Join(", ", filtro.Produtos) + ")");

            if (filtro.Cores != null && filtro.Cores.Count() > 0)
                SQL.AppendLine("        AND iop.CorId IN (" + string.Join(", ", filtro.Cores) + ")");

            if (filtro.Tamanhos != null && filtro.Tamanhos.Count() > 0)
                SQL.AppendLine("        AND iop.TamanhoId IN (" + string.Join(", ", filtro.Tamanhos) + ")");

            if (filtro.Pacote != null && filtro.Pacote.Count() > 0)
                SQL.AppendLine("        AND pa.Id IN (" + string.Join(", ", filtro.Pacote) + ")");

            if (filtro.DaEmissao != "" || filtro.AteEmissao != "")
                SQL.AppendLine("        AND DATE(gp.Data) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");

            if (filtro.DaEntrada != "" || filtro.AteEntrada != "")
                SQL.AppendLine("        AND DATE(pa.DataEntrada) BETWEEN  '" + filtro.DaEntrada + "' AND '" + filtro.AteEntrada + "' ");

            if (filtro.DaSaida != "" || filtro.AteSaida != "")
                SQL.AppendLine("        AND DATE(pa.DataSaida) BETWEEN  '" + filtro.DaSaida + "' AND '" + filtro.AteSaida + "' ");

            if (filtro.RetiraPacotesFaccao)
            {
                SQL.AppendLine(" AND entradafaccao <=0  ");
            }

            if (filtro.Aberto || filtro.Producao || filtro.Concluido || filtro.Finalizado)
            {
                SQL.AppendLine(" AND (");

                if (filtro.Aberto)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Aberto);

                if (filtro.Aberto && filtro.Producao)
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Producao);
                else if (filtro.Producao)
                    SQL.Append("pa.Status = " + (int)enumStatusPacotesProducao.Producao);

                if (filtro.Concluido && (filtro.Producao || filtro.Aberto))
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Concluido);
                else if (filtro.Concluido)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Concluido);

                if (filtro.Finalizado && (filtro.Producao || filtro.Aberto || filtro.Concluido))
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);
                else if (filtro.Finalizado)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);

                SQL.Append(")");
            }
            else
            {
                SQL.AppendLine(" AND pa.Status = -1");
            }

           
            SQL.AppendLine(" GROUP BY pa.id, iop.OrdemProducaoId, iop.ProdutoId, iop.CorId");
           
            

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString());
        }

        public IEnumerable<ControlePacoteView> GetControlePacotesRelatorio(FiltroControlePacote filtro)
        {
            var cn = new DapperConnection<ControlePacoteView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.referencia AS OrdemProducaoReferencia, ");
            SQL.AppendLine("pa.referencia AS PacoteNumero, ");
            SQL.AppendLine("gp.data AS DataEmissao, ");
            SQL.AppendLine("pr.referencia AS ProdutoReferencia, ");
            SQL.AppendLine("c.abreviatura AS CorAbreviatura, ");
            SQL.AppendLine("t.abreviatura AS TamanhoAbreviatura, ");
            SQL.AppendLine("pa.quantidade AS Qtd, ");
            SQL.AppendLine("go.sequencia AS Sequencia, ");
            SQL.AppendLine("go.titulocupom AS Operacao, ");
            SQL.AppendLine("o.Referencia AS Maquina, ");
            SQL.AppendLine("SUM(go.tempo*pa.quantidade + pr.tempopacote) AS TempoTotal, ");
            SQL.AppendLine("SUM(go.tempo) AS TempoUnitario, ");
            SQL.AppendLine("IF(OO.Data = NULL, of.data, oo.data) AS DataConclusao, ");
            SQL.AppendLine("IF(FU.Nome = NULL, F.nome, FU.Nome)  AS Funcionario ");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("INNER JOIN grupopacote gp  ON gp.id = pa.grupopacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes go ON gp.id = go.grupopacoteId");
            SQL.AppendLine("INNER JOIN operacaopadrao o ON o.id = go.operacaopadraoid");
            SQL.AppendLine("LEFT JOIN operacaooperadora OO ON OO.PacoteId = PA.Id && OO.OperacaoId = GO.OperacaoPadraoId && OO.Sequencia = GO.Sequencia");
            SQL.AppendLine("LEFT JOIN funcionarios FU ON FU.Id = OO.FuncionarioId");
            SQL.AppendLine("LEFT JOIN operacaofaccao OF ON OF.PacoteId = PA.Id && OF.OperacaoId = GO.OperacaoPadraoId && OF.Sequencia = GO.Sequencia");
            SQL.AppendLine("LEFT JOIN colaboradores F ON F.Id = OF.FaccaoId");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" a.IdEmpresa "));

            if (filtro.Ordem != null && filtro.Ordem.Count() > 0)
                SQL.AppendLine(" AND iop.OrdemProducaoId IN (" + string.Join(", ", filtro.Ordem) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine("        AND iop.ProdutoId IN (" + string.Join(", ", filtro.Produtos) + ")");            

            if (filtro.Pacotes != null && filtro.Pacotes.Count() > 0)
                SQL.AppendLine("        AND pa.Id IN (" + string.Join(", ", filtro.Pacotes) + ")");

            if (filtro.DaEmissao != "" || filtro.AteEmissao != "")
                SQL.AppendLine("        AND DATE(gp.Data) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");

            if (filtro.Aberto || filtro.Producao || filtro.Concluido || filtro.Finalizado)
            {
                SQL.AppendLine(" AND (");

                if (filtro.Aberto)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Aberto);

                if (filtro.Aberto && filtro.Producao)
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Producao);
                else if (filtro.Producao)
                    SQL.Append("pa.Status = " + (int)enumStatusPacotesProducao.Producao);

                if (filtro.Concluido && (filtro.Producao || filtro.Aberto))
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Concluido);
                else if (filtro.Concluido)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Concluido);

                if (filtro.Finalizado && (filtro.Producao || filtro.Aberto || filtro.Concluido))
                    SQL.Append(" OR pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);
                else if (filtro.Finalizado)
                    SQL.Append(" pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);

                SQL.Append(")");
            }            

            SQL.AppendLine(" GROUP BY go.id, pa.id");
            SQL.AppendLine(" ORDER BY gp.data, pa.Referencia, pr.Referencia, CAST(go.sequencia as UNSIGNED)");

            return cn.ExecuteStringSqlToList(new ControlePacoteView(), SQL.ToString());
        }

        public IEnumerable<PacoteProducaoView> GetByFiltroBalanceamento(FiltroRelatorioPacote filtro)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("go.balanceamentoid,");
            SQL.AppendLine("SUM(go.tempo*pa.quantidade + pr.tempopacote) as TempoTotal");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN grupopacote gp  ON gp.id = pa.grupopacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes go ON gp.id = go.grupopacoteId");
            SQL.AppendLine("LEFT OUTER JOIN operacaooperadora oo ON oo.PacoteId = pa.Id && oo.OperacaoId = go.OperacaoPadraoId && oo.Sequencia = go.Sequencia");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" op.EmpresaId "));

            if (filtro.OrdensProducao != null && filtro.OrdensProducao.Count() > 0)
                SQL.AppendLine(" AND iop.OrdemProducaoId IN (" + string.Join(", ", filtro.OrdensProducao) + ")");

            if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                SQL.AppendLine("        AND iop.ProdutoId IN (" + string.Join(", ", filtro.Produtos) + ")");


            if (filtro.Pacote != null && filtro.Pacote.Count() > 0)
                SQL.AppendLine("        AND pa.Id IN (" + string.Join(", ", filtro.Pacote) + ")");

            SQL.AppendLine("        AND pa.Status <> 5 AND pa.Status <> 6 AND oo.id is null");
            SQL.AppendLine(" GROUP BY go.balanceamentoid, pa.Status");

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString());
        }

        public IEnumerable<PacoteFuncionario> GetPacotesFuncionarioRelatorio(List<int> pacotes, List<int> funcionarios, string DaData, string AteData)
        {
            var cn = new DapperConnection<PacoteFuncionario>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.referencia as PacoteReferencia,");
            SQL.AppendLine("f.Nome as Funcionario,");
            SQL.AppendLine("oo.Data as Data,");
            SQL.AppendLine("go.TituloCupom as Operacao,");
            SQL.AppendLine("op.Referencia as Maquina");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN grupopacote gp  ON gp.id = pa.grupopacoteId");
            SQL.AppendLine("INNER JOIN grupooperacoes go ON gp.id = go.grupopacoteId");
            SQL.AppendLine("INNER JOIN operacaopadrao op ON op.id = go.operacaopadraoid");
            SQL.AppendLine("LEFT JOIN operacaooperadora OO ON OO.PacoteId = PA.Id && oo.OperacaoId = go.OperacaoPadraoId && oo.Sequencia = GO.sequencia");
            SQL.AppendLine("LEFT JOIN funcionarios f ON f.id = oo.FuncionarioId");
            SQL.AppendLine("WHERE 1=1 "); //+ FiltroEmpresa(" ")
            
            if (pacotes != null && pacotes.Count() > 0)
                SQL.AppendLine(" AND pa.id IN (" + string.Join(", ", pacotes) + ")");

            if (funcionarios != null && funcionarios.Count() > 0)
                SQL.AppendLine("        AND f.id IN (" + string.Join(", ", funcionarios) + ")");

            
            if (DaData != "" || AteData != "")
                SQL.AppendLine("        AND DATE(oo.Data) BETWEEN  '" + DaData + "' AND '" + AteData + "' ");

            //SQL.AppendLine(" GROUP BY pa.id, iop.OrdemProducaoId, iop.ProdutoId, iop.CorId");

            return cn.ExecuteStringSqlToList(new PacoteFuncionario(), SQL.ToString());
        }

        public IEnumerable<PacoteFaccao> GetPacotesFaccaoRelatorio(FiltroPacoteFaccao filtro)
        {
            var cn = new DapperConnection<PacoteFaccao>(); 

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.referencia as Pacote,");
            SQL.AppendLine("gp.Data as DataEmissao,");
            SQL.AppendLine("p.referencia as ProdutoReferencia,");
            SQL.AppendLine("f.nome as NomeFaccao,");
            SQL.AppendLine("IFNULL(ff.valorPeca, 0) as PrecoUnitario,");
            SQL.AppendLine("go.TituloCupom as Operacao,");
            SQL.AppendLine("pa.dataentrada as DataEntrada,");
            SQL.AppendLine("pa.datasaida as DataSaida, "); 
            SQL.AppendLine("orp.referencia as OrdemProducao, ");
            SQL.AppendLine("c.abreviatura as Cor, ");
            SQL.AppendLine("t.abreviatura as Tamanho, ");
            SQL.AppendLine("pa.qtddefeito as Defeito, ");
            SQL.AppendLine("SUM(go.tempo * pa.qtddefeito) as TempoDefeito, ");

            if (VestilloSession.FinalizaPacoteFaccao)
            {
                SQL.AppendLine("pa.qtdproduzida - pa.qtddefeito as Pecas, ");
                SQL.AppendLine("pa.qtdproduzida as TotalPecas, ");
                SQL.AppendLine("SUM(go.tempo * (pa.qtdproduzida - pa.qtddefeito)) as TempoPecas, ");
                SQL.AppendLine("SUM(go.tempo * pa.qtdproduzida + p.tempopacote) as TempoTotal, ");

                SQL.AppendLine("CASE WHEN pa.qtdproduzida - PA.QTDDEFEITO <= 0 THEN 0");
                SQL.AppendLine("ELSE SUM(f.valorminuto * (go.tempo * (pa.qtdproduzida - pa.qtddefeito) + p.tempopacote))");
                SQL.AppendLine("END as ValorTotal");
            }
            else
            {
                SQL.AppendLine("pa.quantidade - pa.qtddefeito as Pecas, ");
                SQL.AppendLine("pa.quantidade as TotalPecas, ");
                SQL.AppendLine("SUM(go.tempo * (pa.quantidade - pa.qtddefeito)) as TempoPecas, ");
                SQL.AppendLine("SUM(go.tempo * pa.quantidade + p.tempopacote) as TempoTotal, ");

                SQL.AppendLine("CASE WHEN pa.quantidade - PA.QTDDEFEITO <= 0 THEN 0");
                SQL.AppendLine("ELSE SUM(f.valorminuto * (go.tempo * (pa.quantidade - pa.qtddefeito) + p.tempopacote))");
                SQL.AppendLine("END as ValorTotal");
            }

            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos p  ON p.id = pa.produtoId");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("INNER JOIN grupopacote gp  ON gp.id = pa.grupopacoteId");
            SQL.AppendLine("inner JOIN operacaofaccao OF ON OF.PacoteId = PA.Id");
            SQL.AppendLine("inner JOIN grupooperacoes go ON gp.id = go.grupopacoteId ");
            if (!filtro.TotalPecas)
            {
                SQL.AppendLine(" && OF.OperacaoId = go.OperacaoPadraoId");
            }
            SQL.AppendLine("inner JOIN operacaopadrao op ON op.id = go.operacaopadraoid");
            SQL.AppendLine("left JOIN fichafaccao ff ON ff.idfaccao = OF.FaccaoId && op.idFicha = ff.idficha"); 
            SQL.AppendLine("inner JOIN colaboradores f ON f.id = OF.FaccaoId");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao orp ON iop.ordemproducaoid = orp.id");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" op.idempresa "));

            if (filtro.Pacote != null && filtro.Pacote.Count() > 0)
                SQL.AppendLine(" AND pa.id IN (" + string.Join(", ", filtro.Pacote) + ")");

            if ( (filtro.Faccao != null && filtro.Faccao.Count() > 0) &&
                 (!filtro.TotalPecas || (filtro.TotalPecas && !filtro.BuscaPreco)) ) //se o check Buscar Preco da ficha tiver ativado, não filtra por facção
            {
                SQL.AppendLine(" AND OF.FaccaoId IN (" + string.Join(", ", filtro.Faccao) + ")");
            }

            if (filtro.DaEmissao != null || filtro.AteEmissao != null)
                SQL.AppendLine(" AND DATE(gp.data) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");

            if (!filtro.TotalPecas)
            {
                if (filtro.BuscarPor == 0)
                {
                    SQL.AppendLine(" AND (pa.Status = " + (int)enumStatusPacotesProducao.Producao + " OR pa.Status = " + (int)enumStatusPacotesProducao.Finalizado + ")");
                }
                else if (filtro.BuscarPor == 1)
                {
                    SQL.AppendLine(" AND pa.Status = " + (int)enumStatusPacotesProducao.Producao);
                }
                else
                {
                    SQL.AppendLine(" AND pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);
                }
            }
           

            SQL.AppendLine("group by pa.id, f.nome");
            if (!filtro.TotalPecas)
            {
                SQL.Append(", go.id");
            }
            
            switch (filtro.Ordenar)
            {
                case 0:
                    SQL.AppendLine(" ORDER BY f.nome, pa.referencia, OF.sequencia");
                    break;
                case 1:
                    SQL.AppendLine(" ORDER BY pa.referencia, OF.sequencia");
                    break;
                case 2:
                    SQL.AppendLine(" ORDER BY gp.Data, pa.referencia, OF.sequencia");
                    break;
                case -1:
                    SQL.AppendLine(" ORDER BY pa.referencia, OF.sequencia");
                    break;
            }
            

            return cn.ExecuteStringSqlToList(new PacoteFaccao(), SQL.ToString());
        }

        public IEnumerable<PacoteFaccao> GetPacotesFaccaoFinalizados(FiltroPacoteFaccao filtro) //exibir total checked
        {
            var cn = new DapperConnection<PacoteFaccao>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.referencia as Pacote,");
            SQL.AppendLine("gp.Data as DataEmissao,");
            SQL.AppendLine("p.referencia as ProdutoReferencia,");
            SQL.AppendLine("f.nome as NomeFaccao,");
            SQL.AppendLine("IFNULL(ff.valorPeca, 0) as PrecoUnitario,");
            SQL.AppendLine("null as Operacao,");
            SQL.AppendLine("pa.dataentrada as DataEntrada,");
            SQL.AppendLine("pa.datasaida as DataSaida, ");
            SQL.AppendLine("orp.referencia as OrdemProducao, ");
            SQL.AppendLine("c.abreviatura as Cor, ");
            SQL.AppendLine("t.abreviatura as Tamanho, ");
            SQL.AppendLine("pa.qtddefeito as Defeito, ");
            SQL.AppendLine("SUM(go.tempo * pa.qtddefeito) as TempoDefeito, ");

            if (VestilloSession.FinalizaPacoteFaccao)
            {
                SQL.AppendLine("pa.qtdproduzida - pa.qtddefeito as Pecas, ");
                SQL.AppendLine("pa.qtdproduzida as TotalPecas, ");
                SQL.AppendLine("SUM(go.tempo * (pa.qtdproduzida - pa.qtddefeito)) as TempoPecas, ");
                SQL.AppendLine("SUM(go.tempo * pa.qtdproduzida + p.tempopacote) as TempoTotal, ");

                if (filtro.TotalPecas && filtro.BuscaPreco)
                {
                    SQL.AppendLine("CASE WHEN pa.qtdproduzida - PA.QTDDEFEITO <= 0 THEN 0");
                    SQL.AppendLine("ELSE IFNULL(ff.valorPeca, 0) * (pa.qtdproduzida - pa.qtddefeito)");
                    SQL.AppendLine("END as ValorTotal");

                }
                else
                {
                    SQL.AppendLine("CASE WHEN pa.qtdproduzida - PA.QTDDEFEITO <= 0 THEN 0");
                    SQL.AppendLine("ELSE SUM(f.valorminuto * (go.tempo * (pa.qtdproduzida - pa.qtddefeito) + p.tempopacote))");
                    SQL.AppendLine("END as ValorTotal");
                }
            }
            else
            {
                SQL.AppendLine("pa.quantidade - pa.qtddefeito as Pecas, ");
                SQL.AppendLine("pa.quantidade as TotalPecas, ");
                SQL.AppendLine("SUM(go.tempo * (pa.quantidade - pa.qtddefeito)) as TempoPecas, ");
                SQL.AppendLine("SUM(go.tempo * pa.quantidade + p.tempopacote) as TempoTotal, ");

                if (filtro.TotalPecas && filtro.BuscaPreco)
                {
                    SQL.AppendLine("CASE WHEN pa.quantidade - PA.QTDDEFEITO <= 0 THEN 0");
                    SQL.AppendLine("ELSE IFNULL(ff.valorPeca, 0) * (pa.quantidade - pa.qtddefeito)");
                    SQL.AppendLine("END as ValorTotal");

                }
                else
                {
                    SQL.AppendLine("CASE WHEN pa.quantidade - PA.QTDDEFEITO <= 0 THEN 0");
                    SQL.AppendLine("ELSE SUM(f.valorminuto * (go.tempo * (pa.quantidade - pa.qtddefeito) + p.tempopacote))");
                    SQL.AppendLine("END as ValorTotal");
                }
            }            

            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos p  ON p.id = pa.produtoId");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            SQL.AppendLine("INNER JOIN grupopacote gp  ON gp.id = pa.grupopacoteId");
            SQL.AppendLine("inner JOIN grupooperacoes go ON gp.id = go.grupopacoteId");
            SQL.AppendLine("INNER join fichatecnica ft ON ft.produtoId = p.id");
            SQL.AppendLine("LEFT JOIN fichafaccao ff ON ff.idfaccao = pa.entradafaccao && ft.id = ff.idficha");
            SQL.AppendLine("INNER JOIN colaboradores f ON f.id = pa.entradafaccao && f.faccao = 1");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao orp ON iop.ordemproducaoid = orp.id");
            SQL.AppendLine("WHERE " + FiltroEmpresa(" gp.empresaid "));

            if (filtro.Pacote != null && filtro.Pacote.Count() > 0)
                SQL.AppendLine(" AND pa.id IN (" + string.Join(", ", filtro.Pacote) + ")");

            if ((filtro.Faccao != null && filtro.Faccao.Count() > 0) &&
                 (!filtro.TotalPecas || (filtro.TotalPecas && !filtro.BuscaPreco))) //se o check Buscar Preco da ficha tiver ativado, não filtra por facção
            {
                SQL.AppendLine(" AND pa.entradafaccao IN (" + string.Join(", ", filtro.Faccao) + ")");
            }

            if (filtro.DaEmissao != null || filtro.AteEmissao != null)
                SQL.AppendLine(" AND DATE(gp.data) BETWEEN  '" + filtro.DaEmissao + "' AND '" + filtro.AteEmissao + "' ");

            
            SQL.AppendLine(" AND pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);
            

            if (filtro.TotalPecas)
            {
                if (filtro.OrdemProducao != null && filtro.OrdemProducao.Count() > 0)
                    SQL.AppendLine(" AND orp.id IN (" + string.Join(", ", filtro.OrdemProducao) + ")");

                if (filtro.Produtos != null && filtro.Produtos.Count() > 0)
                    SQL.AppendLine(" AND pa.produtoId IN (" + string.Join(", ", filtro.Produtos) + ")");

                if (filtro.Cor != null && filtro.Cor.Count() > 0)
                    SQL.AppendLine(" AND pa.corid IN (" + string.Join(", ", filtro.Cor) + ")");

                if (filtro.Tamanho != null && filtro.Tamanho.Count() > 0)
                    SQL.AppendLine(" AND pa.tamanhoid IN (" + string.Join(", ", filtro.Tamanho) + ")");

                if (filtro.Finalizacao)
                    if (filtro.DaFinalizacao != null || filtro.AteFinalizacao != null)
                        SQL.AppendLine(" AND DATE(pa.datasaida) BETWEEN  '" + filtro.DaFinalizacao + "' AND '" + filtro.AteFinalizacao + "' ");

            }

            SQL.AppendLine("group by pa.id, pa.entradafaccao");

            switch (filtro.Ordenar)
            {
                case 0:
                    SQL.AppendLine(" ORDER BY f.nome, pa.referencia");
                    break;
                case 1:
                    SQL.AppendLine(" ORDER BY pa.referencia");
                    break;
                case 2:
                    SQL.AppendLine(" ORDER BY gp.Data, pa.referencia");
                    break;
                case -1:
                    SQL.AppendLine(" ORDER BY pa.referencia");
                    break;
            }


            return cn.ExecuteStringSqlToList(new PacoteFaccao(), SQL.ToString());
        }

        public IEnumerable<PacoteProducaoView> GetByPacoteRelatorio(string referencia)
        {
            var cn = new DapperConnection<PacoteProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pa.*,");
            SQL.AppendLine("pr.referencia as ProdutoReferencia,");
            SQL.AppendLine("op.referencia as OrdemProducaoReferencia,");
            SQL.AppendLine("op.id as OrdemProducaoId,");
            SQL.AppendLine("op.dataemissao as DataEmissao,");
            SQL.AppendLine("c.abreviatura as CorDescricao,");
            SQL.AppendLine("t.abreviatura as TamanhoDescricao,");
            SQL.AppendLine("pr.Descricao as ProdutoDescricao,");
            SQL.AppendLine("pr.Referencia as ProdutoReferencia,");
            SQL.AppendLine("op.AlmoxarifadoId as AlmoxarifadoId,");
            SQL.AppendLine("a.descricao as AlmoxarifadoDescricao");
            SQL.AppendLine("FROM 	pacotes pa");
            SQL.AppendLine("INNER JOIN produtos pr ON pa.produtoId = pr.id");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON pa.itemordemproducaoid = iop.id");
            SQL.AppendLine("INNER JOIN ordemproducao op ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN almoxarifados a ON op.almoxarifadoid = a.id");
            SQL.AppendLine("INNER JOIN cores c ON pa.corid = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON pa.tamanhoid = t.id");
            if (referencia != null && referencia != "")
                SQL.AppendLine("WHERE pa.referencia like '%" + referencia + "%'");

            return cn.ExecuteStringSqlToList(new PacoteProducaoView(), SQL.ToString());
        }

        public List<PacoteProducao> GetByGrupoPacote(int grupoPacoteId)
        {
            var cn = new DapperConnection<PacoteProducao>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	pacotes");
            SQL.AppendLine("WHERE grupopacoteid = " + grupoPacoteId);

            return cn.ExecuteStringSqlToList(new PacoteProducao(), SQL.ToString()).ToList();
        }

        public void UpdateHeader(int pacoteId)
        {            
            OperacaoFaccaoRepository operacaoFaccaoRepository = new OperacaoFaccaoRepository();
            OperacaoOperadoraRepository operacaoRepository = new OperacaoOperadoraRepository();

            var operacoesFaccao = operacaoFaccaoRepository.GetQuantidadeLancadaPorPacote(pacoteId);
            var operacoesOperadora = operacaoRepository.GetQuantidadeLancadaPorPacote(pacoteId);
            var pacote = GetById(pacoteId);

            var operacoesConcluidas = operacoesFaccao + operacoesOperadora;            

            var operacoes = new GrupoOperacoesRepository().GetOperacoesPacote(pacoteId);
            var SQL = new StringBuilder();
            string concluido = ((decimal)operacoesConcluidas / (decimal)operacoes).ToString().Replace(",", ".");

            SQL.AppendLine("UPDATE pacotes pa  ");
            SQL.AppendLine("SET pa.Concluido = " + concluido);
            if(concluido == "1" && pacote.Status == (int)enumStatusPacotesProducao.Producao)
                SQL.AppendLine(" , pa.Status = " + (int)enumStatusPacotesProducao.Concluido);
            if(concluido != "1" && pacote.Status == (int)enumStatusPacotesProducao.Concluido)
                SQL.AppendLine(" , pa.Status = " + (int)enumStatusPacotesProducao.Producao);
            SQL.AppendLine(" WHERE pa.id = ");
            SQL.Append(pacoteId);
            
            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public int QuaantidadeDePacotes(int ItemId)
        {
            int pacote = 0;
            string Sql = String.Empty;
            DataTable dt = new DataTable();

            Sql = "select count(*) from pacotes where pacotes.itemordemproducaoid = " + ItemId;

            dt = _cn.ExecuteToDataTable(Sql);
            pacote = int.Parse("0" + dt.Rows[0][0].ToString());  
            return pacote;
        }

        public IEnumerable<PacoteFaccaoValorizado> GetByPacoteFaccaoValorizado(DateTime DataInicio, DateTime DataFim, List<int> Faccao, int Tipo)
        {
            PacoteFaccaoValorizado agpDados = new PacoteFaccaoValorizado();
            List<PacoteFaccaoValorizado> Retorno = new List<PacoteFaccaoValorizado>();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            string Faccoes = string.Empty;

            foreach (var item in Faccao)
            {

                Faccoes += item + ",";
            }
            if(Faccoes.Length > 0)
            {
                Faccoes = Faccoes.ToString().Substring(0, Faccoes.Length - 1);
            }
            

            // SQL.AppendLine(" WHERE SUBSTRING(nfe.DataInclusao,1,10) BETWEEN " + Valor);

            var cn = new DapperConnection<PacoteFaccaoValorizado>();

            var SQL = new StringBuilder();
            SQL.AppendLine(" SELECT operacaofaccao.FaccaoId,colaboradores.nome as Faccao,1 as quantidade, ");
            SQL.AppendLine(" operacaofaccao.OperacaoId,operacaopadrao.Referencia,operacaopadrao.Descricao, ");
            SQL.AppendLine("  operacaopadrao.ValorOperacao,operacaofaccao.data as DataLancamento, operacaopadrao.ValorOperacao as total");            
            SQL.AppendLine(" FROM operacaofaccao ");
            SQL.AppendLine(" INNER JOIN operacaopadrao ON operacaopadrao.Id = operacaofaccao.OperacaoId ");
            SQL.AppendLine(" INNER JOIN colaboradores ON colaboradores.id = operacaofaccao.FaccaoId ");
            SQL.AppendLine("WHERE   ");
            if (!String.IsNullOrEmpty(Faccoes))
            {
                SQL.AppendLine(" operacaofaccao.FaccaoId IN (" + Faccoes + ") AND  ");
            }
            
            SQL.AppendLine(" SUBSTRING(operacaofaccao.data,1,10) BETWEEN " + Valor);
            SQL.AppendLine("ORDER BY operacaofaccao.FaccaoId,operacaofaccao.OperacaoId ");



            if (Tipo == 1)
            {
                var dados = cn.ExecuteStringSqlToList(new PacoteFaccaoValorizado(), SQL.ToString());
                int IdFac = 0;
                int IdOpera = 0;

                foreach (var item in dados)
                {
                    if(item.FaccaoId != IdFac || IdOpera != item.OperacaoId)
                    {

                        agpDados = new PacoteFaccaoValorizado();
                    }
                    agpDados.DataLancamento = null;
                    agpDados.Descricao = item.Descricao;
                    agpDados.Faccao = item.Faccao;
                    agpDados.FaccaoId = item.FaccaoId;
                    agpDados.OperacaoId = item.OperacaoId;
                    agpDados.Quantidade += item.Quantidade;
                    agpDados.Referencia = item.Referencia;
                    agpDados.Total += item.Total;
                    agpDados.ValorOperacao = item.ValorOperacao;

                    if (item.FaccaoId != IdFac || IdOpera != item.OperacaoId)
                    {
                        Retorno.Add(agpDados);
                    }

                    IdFac = item.FaccaoId;
                    IdOpera = item.OperacaoId;

                }

               

                return Retorno;

            }
            else
            {
                return cn.ExecuteStringSqlToList(new PacoteFaccaoValorizado(), SQL.ToString());
            }
            


            
        }


        public void ModificaObsPacote(int IdPacote,string NovaObservcao)
        {
            string SQL = String.Empty;

            SQL = "UPDATE pacotes SET pacotes.observacao = " + "'" + NovaObservcao + "'" + " WHERE pacotes.Id = " + IdPacote;
            _cn.ExecuteNonQuery(SQL);

        }

        public void UpdatePacoteLote(int pacoteId,int ItemOrdemProducao,string UsuarioLote,decimal QtdTotal,decimal QtdDefeito, int IdAlmoxarifadoAlternativo, DateTime DataFinalizacao)
        {
            decimal qtdProduzida = QtdTotal + QtdDefeito;

            try
            {
                
                OperacaoFaccaoRepository operacaoFaccaoRepository = new OperacaoFaccaoRepository();
                OperacaoOperadoraRepository operacaoRepository = new OperacaoOperadoraRepository();

                var operacoesFaccao = operacaoFaccaoRepository.GetQuantidadeLancadaPorPacote(pacoteId);
                var operacoesOperadora = operacaoRepository.GetQuantidadeLancadaPorPacote(pacoteId);
                var pacote = GetById(pacoteId);

                var operacoesConcluidas = operacoesFaccao + operacoesOperadora;

                var operacoes = new GrupoOperacoesRepository().GetOperacoesPacote(pacoteId);
                var SQL = new StringBuilder();
                string concluido = ((decimal)operacoesConcluidas / (decimal)operacoes).ToString().Replace(",", ".");

                SQL.AppendLine("UPDATE pacotes pa  ");
                SQL.AppendLine("SET pa.Concluido = " + concluido);
                SQL.AppendLine(" , pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);
                SQL.AppendLine(" , pa.UsuarioLote = " + "'" + UsuarioLote + "'");
                SQL.AppendLine(" , pa.FinalizouLote = 1");
                SQL.AppendLine(" , pa.datasaida = " + "'" + DataFinalizacao.ToString("yyyy-MM-dd") + "'");
                SQL.AppendLine(" , pa.qtddefeito = " + QtdDefeito.ToString().Replace(",", "."));
                if (IdAlmoxarifadoAlternativo > 0)
                {
                    SQL.AppendLine(" , pa.almoxarifadoalternativoid = " + IdAlmoxarifadoAlternativo);
                }
                SQL.AppendLine(" WHERE pa.id = ");
                SQL.Append(pacoteId);

                _cn.ExecuteNonQuery(SQL.ToString());
            }
            catch(Exception ex)
            {
                throw ex;
            }

            try
            {
                var cn = new DapperConnection<ItemOrdemProducao>(); 
                string SQl = String.Empty;
                SQl = "UPDATE itensordemproducao SET QuantidadeProduzida = QuantidadeProduzida + " + qtdProduzida + " WHERE itensordemproducao.id = " + ItemOrdemProducao;
                cn.ExecuteNonQuery(SQl);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public void UpdatePacoteLoteComLancamento(int pacoteId, int ItemOrdemProducao, string PacoteReferencia, string UsuarioLote, decimal QtdTotal, decimal QtdDefeito, int IdAlmoxarifadoAlternativo,int IdFuncionario,DateTime Domingo, DateTime DataFinalizacao)
        {
            decimal qtdProduzida = QtdTotal + QtdDefeito;
            decimal TempoDosPacotes = 0;

            try
            {

                var pacote = GetByIdView(pacoteId);
                List<GrupoOperacoesView> Listaoperacoes = new List<GrupoOperacoesView>();
                Listaoperacoes = new GrupoOperacoesRepository().GetListByGrupoPacoteVisualizar(pacote.GrupoPacoteId, pacote.Id);
                foreach (var itemOperacoes in Listaoperacoes)
                {
                    if(String.IsNullOrEmpty(itemOperacoes.Funcionario) && String.IsNullOrEmpty(itemOperacoes.Faccao))
                    {
                        var LancamentoProd = new OperacaoOperadora();
                        LancamentoProd.Data = Domingo;
                        LancamentoProd.FuncionarioId = IdFuncionario;
                        LancamentoProd.OperacaoId = itemOperacoes.OperacaoPadraoId;
                        LancamentoProd.PacoteId = pacoteId;
                        LancamentoProd.Sequencia = itemOperacoes.Sequencia;
                        LancamentoProd.usuario = UsuarioLote;
                        new OperacaoOperadoraService().GetServiceFactory().Save(ref LancamentoProd);
                        TempoDosPacotes += itemOperacoes.TempoTotal;
                    }
                    
                }

                var Ddprodutividade = new ProdutividadeService().GetServiceFactory().GetByFuncionarioIdEData(IdFuncionario, Domingo);
                var cnPdt = new DapperConnection<ItemOrdemProducao>();
                string SqlPdt = String.Empty;
                if (Ddprodutividade != null)
                {
                    SqlPdt = "UPDATE produtividade SET produtividade.Tempo = produtividade.Tempo + " + TempoDosPacotes.ToString().Replace(",",".") + " WHERE produtividade.FuncionarioId = " + IdFuncionario + " AND produtividade.data = " + "'" + Domingo.ToString("yyyy-MM-dd") + "'";
                    cnPdt.ExecuteNonQuery(SqlPdt);
                }
                else
                {
                    SqlPdt = "INSERT INTO produtividade (Data, FuncionarioId, Tempo,Jornada) values(" + "'" + Domingo.ToString("yyyy-MM-dd") + "'" + "," + IdFuncionario + "," + TempoDosPacotes.ToString().Replace(",", ".") + ",0)";
                    cnPdt.ExecuteNonQuery(SqlPdt);
                }
                




                OperacaoFaccaoRepository operacaoFaccaoRepository = new OperacaoFaccaoRepository();
                OperacaoOperadoraRepository operacaoRepository = new OperacaoOperadoraRepository();

                var operacoesFaccao = operacaoFaccaoRepository.GetQuantidadeLancadaPorPacote(pacoteId);
                var operacoesOperadora = operacaoRepository.GetQuantidadeLancadaPorPacote(pacoteId);
                

                var operacoesConcluidas = operacoesFaccao + operacoesOperadora;

                var operacoes = new GrupoOperacoesRepository().GetOperacoesPacote(pacoteId);
                var SQL = new StringBuilder();
                string concluido = ((decimal)operacoesConcluidas / (decimal)operacoes).ToString().Replace(",", ".");

                SQL.AppendLine("UPDATE pacotes pa  ");
                SQL.AppendLine("SET pa.Concluido = " + concluido);
                SQL.AppendLine(" , pa.Status = " + (int)enumStatusPacotesProducao.Finalizado);
                SQL.AppendLine(" , pa.UsuarioLote = " + "'" + UsuarioLote + "'");
                SQL.AppendLine(" , pa.FinalizouLote = 1");
                SQL.AppendLine(" , pa.datasaida = " + "'" + DataFinalizacao.ToString("yyyy-MM-dd") + "'");
                SQL.AppendLine(" , pa.qtddefeito = " + QtdDefeito.ToString().Replace(",", "."));
                if (IdAlmoxarifadoAlternativo > 0)
                {
                    SQL.AppendLine(" , pa.almoxarifadoalternativoid = " + IdAlmoxarifadoAlternativo);
                }
                SQL.AppendLine(" WHERE pa.id = ");
                SQL.Append(pacoteId);

                _cn.ExecuteNonQuery(SQL.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                var cn = new DapperConnection<ItemOrdemProducao>();
                string SQl = String.Empty;
                SQl = "UPDATE itensordemproducao SET QuantidadeProduzida = QuantidadeProduzida + " + qtdProduzida + " WHERE itensordemproducao.id = " + ItemOrdemProducao;
                cn.ExecuteNonQuery(SQl);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

