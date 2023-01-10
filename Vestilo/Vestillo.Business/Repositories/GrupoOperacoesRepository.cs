using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class GrupoOperacoesRepository: GenericRepository<GrupoOperacoes>
    {
        public GrupoOperacoesRepository()
            : base(new DapperConnection<GrupoOperacoes>())
        {
        }

        public List<GrupoOperacoes> GetListOperacaoPorProduto(int produtoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT FO.OperacaoPadraoDescricao AS TituloCupom, FO.Numero AS Sequencia,");
            sql.AppendLine("FO.OperacaoPadraoId AS OperacaoPadraoId, FO.SetorId AS SetorId, FO.BalanceamentoId as BalanceamentoId, FO.TempoCalculado AS TempoCalculado, FO.TempoCronometrado as TempoCronometrado");
            sql.AppendLine(" FROM produtos P");
            sql.AppendLine("INNER JOIN fichatecnica FT ON FT.ProdutoId = p.Id");
            sql.AppendLine("INNER JOIN fichatecnicaoperacao FO ON FO.FichatecnicaId = FT.Id ");
            sql.AppendLine("WHERE P.Id = " + produtoId);

            var cn = new DapperConnection<GrupoOperacoes>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoes(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoes> GetListOperacaoPorOperacaoPadraoEProduto(int operacao, int produtoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT go.*");
            sql.AppendLine(" FROM grupooperacoes go ");
            sql.AppendLine("INNER JOIN pacotes p ON go.grupopacoteId = p.grupopacoteId");
            sql.AppendLine("WHERE go.operacaopadraoId = " + operacao);
            sql.AppendLine("AND p.produtoId = " + produtoId);
            sql.AppendLine("AND p.status = 0");
            sql.AppendLine(" GROUP BY go.id");

            var cn = new DapperConnection<GrupoOperacoes>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoes(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoes> GetListByOperacaoeProduto(int operacao, int produtoId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT go.*");
            sql.AppendLine(" FROM grupooperacoes go ");
            sql.AppendLine("INNER JOIN pacotes p ON go.grupopacoteId = p.grupopacoteId");
            sql.AppendLine("WHERE go.operacaopadraoId = " + operacao);
            sql.AppendLine("AND p.produtoId = " + produtoId);
            sql.AppendLine("AND (p.DataEntrada BETWEEN DATE_SUB(NOW(), INTERVAL " + VestilloSession.DiasFichaTecnica +  " DAY) AND NOW()");
            sql.AppendLine("OR p.DataEntrada IS NULL)");
            sql.AppendLine(" GROUP BY p.id, go.id");

            var cn = new DapperConnection<GrupoOperacoes>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoes(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoes> GetListByGrupoPacote(int grupoPacoteId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT *");
            sql.AppendLine(" FROM grupooperacoes ");
            sql.AppendLine("WHERE grupopacoteId = " + grupoPacoteId);

            var cn = new DapperConnection<GrupoOperacoes>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoes(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoesView> GetListByGrupoPacoteView(int grupoPacoteId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, OP.Referencia AS OperacaoPadraoReferencia, PR.Referencia AS ProdutoReferencia, PR.Descricao AS ProdutoDescricao,");
            sql.AppendLine("PA.referencia AS PacoteReferencia, PA.id AS PacoteId, OD.Referencia AS OrdemProducaoReferencia, TA.Abreviatura AS TamanhoDescricao, CO.Abreviatura AS CorDescricao, 0 as sequenciaQuebra, FT.QuebraManual, ");
            sql.AppendLine("PA.quantidade, SUM(GO.tempo*PA.quantidade + PR.tempopacote) as TempoTotal, SUM(GO.Tempo + PR.tempopacote/PA.quantidade) as TempoUnitario, FT.UtilizaQuebra AS Quebra, GO.SetorId AS SetorId, 1 as QtdOperacao");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN operacaopadrao OP ON OP.id = GO.operacaopadraoid");          
            sql.AppendLine("INNER JOIN cores CO ON CO.id = PA.corid");
            sql.AppendLine("INNER JOIN tamanhos TA ON TA.id = PA.tamanhoid");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoid");
            sql.AppendLine("LEFT JOIN fichatecnica FT ON PR.id = FT.produtoid ");
            sql.AppendLine("LEFT JOIN fichatecnicaoperacao FO ON FT.id = FO.FichaTecnicaId AND OP.id = FO.OperacaoPadraoid AND GO.Sequencia = FO.Numero");
            sql.AppendLine("LEFT JOIN itensordemproducao IOD ON IOD.id = PA.itemordemproducaoid");
            sql.AppendLine("LEFT JOIN ordemproducao OD ON OD.id = IOD.OrdemProducaoId");
            sql.AppendLine("WHERE GP.id = " + grupoPacoteId);
            sql.AppendLine("GROUP BY PA.id, GO.sequencia");
            sql.AppendLine("ORDER BY  PA.id, CAST(GO.sequencia AS decimal(10,0)) ");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoesView> GetListByGrupoPacoteVisualizar(int grupoPacoteId, int pacoteId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, ");
            sql.AppendLine("PA.quantidade, (GO.tempo*PA.quantidade + PR.TempoPacote) as TempoTotal,");
            sql.AppendLine("IF(ISNULL(OO.Data), OF.Data, OO.Data) AS DataConclusao, FU.Nome AS Funcionario, F.nome AS Faccao");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN pacotes PA ON GO.grupopacoteId = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoId");
            sql.AppendLine("LEFT JOIN operacaooperadora OO ON OO.PacoteId = PA.Id && OO.OperacaoId = GO.OperacaoPadraoId && OO.Sequencia = GO.Sequencia");
            sql.AppendLine("LEFT JOIN funcionarios FU ON FU.Id = OO.FuncionarioId");
            sql.AppendLine("LEFT JOIN operacaofaccao OF ON OF.PacoteId = PA.Id && OF.OperacaoId = GO.OperacaoPadraoId && OF.Sequencia = GO.Sequencia");
            sql.AppendLine("LEFT JOIN colaboradores F ON F.Id = OF.FaccaoId");
            sql.AppendLine("WHERE GO.grupopacoteId = " + grupoPacoteId + " AND PA.id = " + pacoteId);
            sql.AppendLine("ORDER BY CAST(GO.Sequencia AS unsigned)");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoesView> GetListByPacotesView(List<int> pacotesId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, OP.Referencia AS OperacaoPadraoReferencia, PR.Referencia AS ProdutoReferencia, PR.Descricao AS ProdutoDescricao,");
            sql.AppendLine("PA.referencia AS PacoteReferencia, PA.id AS PacoteId, OD.Referencia AS OrdemProducaoReferencia, TA.Abreviatura AS TamanhoDescricao, CO.Abreviatura AS CorDescricao, 0 as sequenciaQuebra, FT.QuebraManual,");
            sql.AppendLine("PA.quantidade, SUM(GO.tempo*PA.quantidade + PR.TempoPacote) as TempoTotal, SUM(GO.Tempo) as TempoUnitario, FT.UtilizaQuebra AS Quebra, GO.SetorId AS SetorId, 1 as QtdOperacao");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN operacaopadrao OP ON OP.id = GO.operacaopadraoid");
            sql.AppendLine("INNER JOIN cores CO ON CO.id = PA.corid");
            sql.AppendLine("INNER JOIN tamanhos TA ON TA.id = PA.tamanhoid");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoid");
            sql.AppendLine("LEFT JOIN fichatecnica FT ON PR.id = FT.produtoid ");
            sql.AppendLine("LEFT JOIN fichatecnicaoperacao FO ON FT.id = FO.FichaTecnicaId AND OP.id = FO.OperacaoPadraoid AND GO.Sequencia = FO.Numero");
            sql.AppendLine("LEFT JOIN itensordemproducao IOD ON IOD.id = PA.itemordemproducaoid");
            sql.AppendLine("LEFT JOIN ordemproducao OD ON OD.id = IOD.OrdemProducaoId");
            sql.AppendLine("WHERE PA.id in ( " + string.Join(", ", pacotesId) + ")");
            sql.AppendLine("GROUP BY PA.id, GO.sequencia");
            sql.AppendLine("ORDER BY  PA.id, CAST(GO.sequencia AS decimal(10,0)) ");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoesView> GetListByPacotesViewSemFicha(List<int> pacotesId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, OP.Referencia AS OperacaoPadraoReferencia, PR.Referencia AS ProdutoReferencia, PR.Descricao AS ProdutoDescricao,");
            sql.AppendLine("PA.referencia AS PacoteReferencia, PA.id AS PacoteId, OD.Referencia AS OrdemProducaoReferencia, TA.Abreviatura AS TamanhoDescricao, CO.Abreviatura AS CorDescricao,");
            sql.AppendLine("PA.quantidade, SUM(GO.tempo*PA.quantidade + PR.TempoPacote) as TempoTotal, SUM(GO.Tempo) as TempoUnitario, Count(GO.id) as QtdOperacao");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN operacaopadrao OP ON OP.id = GO.operacaopadraoid");
            sql.AppendLine("INNER JOIN itensordemproducao IOD ON IOD.id = PA.itemordemproducaoid");
            sql.AppendLine("INNER JOIN cores CO ON CO.id = IOD.corid");
            sql.AppendLine("INNER JOIN tamanhos TA ON TA.id = IOD.tamanhoid");
            sql.AppendLine("INNER JOIN ordemproducao OD ON OD.id = IOD.OrdemProducaoId");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoid");
            sql.AppendLine("WHERE PA.id in ( " + string.Join(", ", pacotesId) + ")");
            sql.AppendLine("GROUP BY PA.id");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, OP.Referencia AS OperacaoPadraoReferencia, PR.Referencia AS ProdutoReferencia, PR.Descricao AS ProdutoDescricao,");
            sql.AppendLine("PA.referencia AS PacoteReferencia, PA.id AS PacoteId, OD.Referencia AS OrdemProducaoReferencia, TA.Abreviatura AS TamanhoDescricao, CO.Abreviatura AS CorDescricao,");
            sql.AppendLine("PA.quantidade, GO.Tempo AS TempoUnitario, PR.TempoPacote as TempoPacote");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN operacaopadrao OP ON OP.id = GO.operacaopadraoid");  
            sql.AppendLine("INNER JOIN cores CO ON CO.id = PA.corid");
            sql.AppendLine("INNER JOIN tamanhos TA ON TA.id = PA.tamanhoid");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoid");
            sql.AppendLine("LEFT JOIN itensordemproducao IOD ON IOD.id = PA.itemordemproducaoid");
            sql.AppendLine("LEFT JOIN ordemproducao OD ON OD.id = IOD.OrdemProducaoId");         
            //sql.AppendLine("INNER JOIN fichatecnica FT ON PR.id = FT.produtoid ");
            //sql.AppendLine("INNER JOIN fichatecnicaoperacao FO ON FT.id = FO.FichaTecnicaId AND OP.id = FO.OperacaoPadraoid AND GO.Sequencia = FO.Numero");
            sql.AppendLine("WHERE PA.referencia = " + pacoteRef);
            sql.AppendLine("GROUP BY PA.id, GO.sequencia");
            sql.AppendLine("ORDER BY CAST(GO.sequencia AS decimal(10,0))");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }

        public int GetOperacoesPacote(int pacoteId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*");
         
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("WHERE PA.id = " + pacoteId);


            var cn = new DapperConnection<GrupoOperacoesView>();
            var grupos = cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
            if (grupos != null)
            {
                return grupos.Count();
            }

            return 0;
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, int setorId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, OP.Referencia AS OperacaoPadraoReferencia, PR.Referencia AS ProdutoReferencia, PR.Descricao AS ProdutoDescricao,");
            sql.AppendLine("PA.referencia AS PacoteReferencia, PA.id AS PacoteId, OD.Referencia AS OrdemProducaoReferencia, TA.Abreviatura AS TamanhoDescricao, CO.Abreviatura AS CorDescricao,");
            sql.AppendLine("PA.quantidade,  GO.Tempo AS TempoUnitario, PR.TempoPacote as TempoPacote");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN operacaopadrao OP ON OP.id = GO.operacaopadraoid");
            sql.AppendLine("INNER JOIN itensordemproducao IOD ON IOD.id = PA.itemordemproducaoid");
            sql.AppendLine("INNER JOIN cores CO ON CO.id = IOD.corid");
            sql.AppendLine("INNER JOIN tamanhos TA ON TA.id = IOD.tamanhoid");
            sql.AppendLine("INNER JOIN ordemproducao OD ON OD.id = IOD.OrdemProducaoId");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoid");
            //sql.AppendLine("INNER JOIN fichatecnica FT ON PR.id = FT.produtoid ");
            //sql.AppendLine("INNER JOIN fichatecnicaoperacao FO ON FT.id = FO.FichaTecnicaId AND OP.id = FO.OperacaoPadraoid AND GO.Sequencia = FO.Numero");
            sql.AppendLine("WHERE PA.referencia = " + pacoteRef + " AND GO.SetorId = " + setorId);
            sql.AppendLine("GROUP BY PA.id, GO.sequencia");
            //sql.AppendLine("ORDER BY GO.id");
            sql.AppendLine("ORDER BY CAST(GO.sequencia AS decimal(10,0))");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, string sequencia)
        {
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, OP.Referencia AS OperacaoPadraoReferencia, PR.Referencia AS ProdutoReferencia, PR.Descricao AS ProdutoDescricao,");
            sql.AppendLine("PA.referencia AS PacoteReferencia, PA.id AS PacoteId, OD.Referencia AS OrdemProducaoReferencia, TA.Abreviatura AS TamanhoDescricao, CO.Abreviatura AS CorDescricao,");
            sql.AppendLine("PA.quantidade,  GO.Tempo AS TempoUnitario, PR.TempoPacote as TempoPacote");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN operacaopadrao OP ON OP.id = GO.operacaopadraoid");
            sql.AppendLine("INNER JOIN itensordemproducao IOD ON IOD.id = PA.itemordemproducaoid");
            sql.AppendLine("INNER JOIN cores CO ON CO.id = IOD.corid");
            sql.AppendLine("INNER JOIN tamanhos TA ON TA.id = IOD.tamanhoid");
            sql.AppendLine("INNER JOIN ordemproducao OD ON OD.id = IOD.OrdemProducaoId");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoid");
            sql.AppendLine("WHERE PA.referencia = " + pacoteRef);
            if (!string.IsNullOrEmpty(sequencia))
            {
                var sequencias = sequencia.Split(';'); //string com intervalo da sequencia
                sql.AppendLine(" AND GO.sequencia >= " + sequencias[0]);
                if (sequencias[1] != "0") //não são operações finais
                    sql.AppendLine(" AND GO.sequencia < " + sequencias[1]);
            }           
            
            sql.AppendLine("GROUP BY PA.id, GO.sequencia");
            sql.AppendLine("ORDER BY CAST(GO.sequencia AS decimal(10,0))");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }

        public GrupoOperacoesView GetListByPacoteESequneciaView(string pacoteRef, string sequencia)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, OP.Referencia AS OperacaoPadraoReferencia, PR.Referencia AS ProdutoReferencia, PR.Descricao AS ProdutoDescricao,");
            sql.AppendLine("PA.referencia AS PacoteReferencia, PA.id AS PacoteId, OD.Referencia AS OrdemProducaoReferencia, TA.Abreviatura AS TamanhoDescricao, CO.Abreviatura AS CorDescricao,");
            sql.AppendLine("PA.quantidade, PA.Status AS PacoteStatus, PA.Id as PacoteId, PR.TempoPacote as TempoPacote");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN grupopacote GP ON GP.id = GO.grupopacoteId");
            sql.AppendLine("INNER JOIN pacotes PA ON GP.id = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN operacaopadrao OP ON OP.id = GO.operacaopadraoid");
            sql.AppendLine("INNER JOIN cores CO ON CO.id = PA.corid");
            sql.AppendLine("INNER JOIN tamanhos TA ON TA.id = PA.tamanhoid");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoid");
            sql.AppendLine("LEFT JOIN itensordemproducao IOD ON IOD.id = PA.itemordemproducaoid");
            sql.AppendLine("LEFT JOIN ordemproducao OD ON OD.id = IOD.OrdemProducaoId");
            //sql.AppendLine("INNER JOIN fichatecnica FT ON PR.id = FT.produtoid ");
            //sql.AppendLine("INNER JOIN fichatecnicaoperacao FO ON FT.id = FO.FichaTecnicaId AND OP.id = FO.OperacaoPadraoid AND GO.Sequencia = FO.Numero");
            sql.AppendLine("WHERE PA.referencia = " + pacoteRef + " AND GO.sequencia = " + sequencia);

            var cn = new DapperConnection<GrupoOperacoesView>();
            GrupoOperacoesView grupo = new GrupoOperacoesView();
            cn.ExecuteToModel(ref grupo, sql.ToString());

            return grupo;
        }

        public List<GrupoOperacoesView> GetListByGrupoPacoteBalanco(int grupoPacoteId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT GO.*, ");
            sql.AppendLine("PA.quantidade, (GO.tempo*PA.quantidade + PR.TempoPacote) as TempoTotal,");
            sql.AppendLine("IF(ISNULL(OO.Data), OF.Data, OO.Data) AS DataConclusao, FU.Nome AS Funcionario, F.nome AS Faccao");
            sql.AppendLine(" FROM grupooperacoes GO ");
            sql.AppendLine("INNER JOIN pacotes PA ON GO.grupopacoteId = PA.grupopacoteId");
            sql.AppendLine("INNER JOIN produtos PR ON PR.id = PA.produtoId");
            sql.AppendLine("LEFT JOIN operacaooperadora OO ON OO.PacoteId = PA.Id && OO.OperacaoId = GO.OperacaoPadraoId && OO.Sequencia = GO.Sequencia");
            sql.AppendLine("LEFT JOIN funcionarios FU ON FU.Id = OO.FuncionarioId");
            sql.AppendLine("LEFT JOIN operacaofaccao OF ON OF.PacoteId = PA.Id && OF.OperacaoId = GO.OperacaoPadraoId && OF.Sequencia = GO.Sequencia");
            sql.AppendLine("LEFT JOIN colaboradores F ON F.Id = OF.FaccaoId");
            sql.AppendLine("WHERE GO.grupopacoteId = " + grupoPacoteId);
            sql.AppendLine("ORDER BY CAST(GO.Sequencia AS unsigned)");

            var cn = new DapperConnection<GrupoOperacoesView>();
            return cn.ExecuteStringSqlToList(new GrupoOperacoesView(), sql.ToString()).ToList();
        }
    }
}
