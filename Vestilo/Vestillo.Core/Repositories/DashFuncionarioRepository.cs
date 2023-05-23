using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class DashFuncionarioRepository : GenericRepository<MetaXProduzidoFuncionario>
    {
        public MetaXProduzidoFuncionario GetMetaXProduzidoEmpresa(List<int> idsOperadoras, string mes, string ano, int empresaLogada,
                                                                        PercentuaisGeraisFuncionario percentuais )
        {
            MetaXProduzidoFuncionario metaProduzido = new MetaXProduzidoFuncionario();

            //calcula a meta
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT sum(f.MinutosDia) AS MinDias ");
            sql.AppendLine("FROM funcionarios f ");
            sql.AppendLine(" WHERE f.ativo = 1 and f.EmpresaId = " + empresaLogada);

            DataTable funcionario = VestilloConnection.ExecToDataTable(sql.ToString());
            decimal minDias = 0;

            if (funcionario.Rows.Count > 0)
            {
                minDias = Convert.ToDecimal(funcionario.Rows[0]["MinDias"]);
            }

            StringBuilder sqlEmpresa = new StringBuilder();
            sqlEmpresa.AppendLine("SELECT diasuteis ");
            sqlEmpresa.AppendLine("FROM percentuaisempresas ");
            sqlEmpresa.AppendLine(" WHERE EmpresaId = " + empresaLogada);

            DataTable empresa = VestilloConnection.ExecToDataTable(sqlEmpresa.ToString());
            int diasUteis = 0;

            if (empresa.Rows.Count > 0)
            {
                diasUteis = Convert.ToInt32(empresa.Rows[0]["diasuteis"]);
            }

            decimal minMes = (minDias / idsOperadoras.Count()) * diasUteis;

            metaProduzido.Meta = minMes * ((percentuais.Eficiencia + percentuais.Aproveitamento + percentuais.Presenca) / 100);

            //calcula min. produzidos
            var tempoOcorrencia = GetTempoOcorrenciaEmpresa( mes, ano).ToList();

            decimal ocorrencia2 = 0;
            if(tempoOcorrencia.Count > 0 && tempoOcorrencia != null)
                ocorrencia2 = tempoOcorrencia.FindAll(t => t.Tipo == 2).Sum(t => t.Tempo);

            decimal tempoOperacao = GetTempoOperacaoEmpresa( mes, ano);
            decimal tempoFuncionario = GetTempoFuncionarioEmpresa( mes, ano);

            decimal minutosProduzidos = ((tempoOperacao + tempoFuncionario) - ocorrencia2);            

            metaProduzido.MinProduzidos = minutosProduzidos / idsOperadoras.Count();

            return metaProduzido;
        }

        public MetaXProduzidoFuncionario GetMetaXProduzidoFuncionario (List<int> idsOperadoras, string mes, string ano, int empresaLogada,
                                                                        PercentuaisGeraisFuncionario percentuais )
        {
            MetaXProduzidoFuncionario metaProduzido = new MetaXProduzidoFuncionario();
            
            //calcula a meta
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT sum(f.MinutosDia) AS MinDias ");
            sql.AppendLine("FROM funcionarios f ");
            sql.AppendLine(" WHERE f.Id IN ( " + string.Join(",", idsOperadoras) + ") ");

            DataTable funcionario = VestilloConnection.ExecToDataTable(sql.ToString());
            decimal minDias = 0;

            if(funcionario.Rows.Count > 0)
            {
                minDias += Convert.ToDecimal(funcionario.Rows[0]["MinDias"]);
            }
            
            StringBuilder sqlEmpresa = new StringBuilder();
            sqlEmpresa.AppendLine("SELECT diasuteis ");
            sqlEmpresa.AppendLine("FROM percentuaisempresas ");
            sqlEmpresa.AppendLine(" WHERE EmpresaId = " + empresaLogada);

            DataTable empresa = VestilloConnection.ExecToDataTable(sqlEmpresa.ToString());
            int diasUteis = 0;

            if (empresa.Rows.Count > 0)
            {
                diasUteis = Convert.ToInt32(empresa.Rows[0]["diasuteis"]);
            }

            decimal minMes = (minDias / idsOperadoras.Count()) * diasUteis;

            metaProduzido.Meta = minMes * ((percentuais.Eficiencia + percentuais.Aproveitamento + percentuais.Presenca) / 100);

            //calcula min. produzidos
            var tempoOcorrencia = GetTempoOcorrencia(idsOperadoras, mes, ano, false).ToList();
            decimal minProduzidosTotais = 0;

            foreach(int id in idsOperadoras)
            {
                decimal ocorrencia2 = 0;
                decimal tempoOperacao = GetTempoOperacao(id.ToString(), mes, ano);
                decimal tempoFuncionario = GetTempoFuncionario(id.ToString(), mes, ano);

                if (tempoOcorrencia.Count > 0 && tempoOcorrencia != null)
                    ocorrencia2 = tempoOcorrencia.FindAll(t => t.FuncionarioId == id && t.Tipo == 2).Sum(t => t.Tempo);

                decimal minutosProduzidos = ((tempoOperacao + tempoFuncionario) - ocorrencia2);

                minProduzidosTotais += minutosProduzidos;
            }

            metaProduzido.MinProduzidos = minProduzidosTotais / idsOperadoras.Count();

            return metaProduzido;
        }

        public PercentuaisGeraisFuncionario GetPercentuaisGeraisFuncionario(List<int> idsOperadoras, string mes, string ano)
        {
            PercentuaisGeraisFuncionario percentuais = new PercentuaisGeraisFuncionario();
            var tempoOcorrencia = GetTempoOcorrencia(idsOperadoras, mes, ano, false).ToList();

            decimal eficienciaTotal = 0;
            decimal aproveitamentoTotal = 0;
            decimal presencaTotal = 0;
            decimal produtividadeTotal = 0;

            foreach (int id in idsOperadoras)
            {
                decimal eficiencia = 0;
                decimal aproveitamento = 0;
                decimal presenca = 0;
                decimal produtividade = 0;
                decimal ocorrencia0 = 0;
                decimal ocorrencia1 = 0;
                decimal ocorrencia2 = 0;
                decimal ocorrencia3 = 0;

                if (tempoOcorrencia.Count > 0 && tempoOcorrencia != null)
                {
                    ocorrencia0 = tempoOcorrencia.FindAll(t => t.FuncionarioId == id && t.Tipo == 0).Sum(t => t.Tempo);
                    ocorrencia1 = tempoOcorrencia.FindAll(t => t.FuncionarioId == id && t.Tipo == 1).Sum(t => t.Tempo);
                    ocorrencia2 = tempoOcorrencia.FindAll(t => t.FuncionarioId == id && t.Tipo == 2).Sum(t => t.Tempo);
                    ocorrencia3 = tempoOcorrencia.FindAll(t => t.FuncionarioId == id && t.Tipo == 3).Sum(t => t.Tempo);
                }                    

                decimal jornada = GetJornada(id.ToString(), mes, ano);
                decimal tempoOperacao = GetTempoOperacao(id.ToString(), mes, ano);
                decimal tempoFuncionario = GetTempoFuncionario(id.ToString(), mes, ano);

                decimal minutosProduzidos = ((tempoOperacao + tempoFuncionario) - ocorrencia2);
                decimal tempoUtil = (jornada - ocorrencia0 - ocorrencia1 + ocorrencia3);

                if(tempoUtil > 0)
                  eficiencia = (minutosProduzidos * 100 / tempoUtil);

                if(jornada > 0)
                {
                    aproveitamento = ((jornada - ocorrencia0) * 100 / jornada);
                    presenca = ((jornada - ocorrencia1) * 100 / jornada);
                    produtividade = (minutosProduzidos / jornada) * 100;
                }

                eficienciaTotal += eficiencia;
                aproveitamentoTotal += aproveitamento;
                produtividadeTotal += produtividade;
                presencaTotal += presenca;
            }
            percentuais.Eficiencia = eficienciaTotal / idsOperadoras.Count();
            percentuais.Aproveitamento = aproveitamentoTotal / idsOperadoras.Count();
            percentuais.Presenca = presencaTotal / idsOperadoras.Count();
            percentuais.Produtividade = produtividadeTotal / idsOperadoras.Count();

            return percentuais;

        }

        public Decimal GetProdutividadeEmpresa(int qtdFuncionario, string mes, string ano, decimal minProduzidos)
        {
            decimal jornada = GetJornadaEmpresa(mes, ano);

            decimal produtividade = 0;

            if (jornada > 0)
            {
                jornada = jornada / qtdFuncionario;

                produtividade = (minProduzidos / jornada) * 100;

            }
            return produtividade;
        }

        public IEnumerable<TempoOcorrenciaFuncionario> GetTempoOcorrencia(List<int> idsFuncionario, string mes, string ano, bool agrupaDescricao) // não mexer pois é utilizada em outras funções
        {

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT f.id as FuncionarioId, f.Nome as NomeFuncionario, SUM(of.tempo) as Tempo, o.tipo as Tipo, o.descricao as Descricao ");
            sql.AppendLine(" FROM ocorrenciafuncionario of ");
            sql.AppendLine("	INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId ");
            sql.AppendLine("    INNER JOIN funcionarios f ON of.funcionarioid = f.id	");
            sql.AppendLine(" WHERE 	f.Id IN ( " + string.Join(",", idsFuncionario) + ") ");
            sql.AppendLine("		AND MONTH(of.data) = " + mes + " AND YEAR(of.data) = " + ano);

            if (agrupaDescricao)
                sql.AppendLine(" GROUP BY f.id, o.tipo, o.descricao");
            else
                sql.AppendLine(" GROUP BY f.id, o.tipo");

            sql.AppendLine("ORDER BY o.tipo");

            return VestilloConnection.ExecSQLToListWithNewConnection<TempoOcorrenciaFuncionario>(sql.ToString());

        }

        public IEnumerable<TempoOcorrenciaFuncionario> GetTempoOcorrenciaEmpresa(string mes, string ano)
        {

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT f.id as FuncionarioId, f.Nome as NomeFuncionario, SUM(of.tempo) as Tempo, o.tipo as Tipo, o.descricao as Descricao ");
            sql.AppendLine(" FROM ocorrenciafuncionario of ");
            sql.AppendLine("	INNER JOIN ocorrencias o ON o.id = of.OcorrenciaId ");
            sql.AppendLine("    INNER JOIN funcionarios f ON of.funcionarioid = f.id	");
            sql.AppendLine(" WHERE f.ativo = 1 AND");
            sql.AppendLine(" MONTH(of.data) = " + mes + " AND YEAR(of.data) = " + ano);
            sql.AppendLine(" GROUP BY o.tipo");
            sql.AppendLine(" ORDER BY o.tipo");

            return VestilloConnection.ExecSQLToListWithNewConnection<TempoOcorrenciaFuncionario>(sql.ToString());

        }

        public decimal GetJornada(string id, string mes, string ano)
        {
            decimal tempo = 0;
            DataTable jornada = VestilloConnection.ExecToDataTable("SELECT SUM(p.jornada) as jornada  " +
                                                  " FROM produtividade p  " +
                                                  " WHERE MONTH(p.data) = " + mes + " AND YEAR(p.data) = " + ano +
                                                  " AND p.FuncionarioId = " + id + " GROUP BY FuncionarioId ");
            if (jornada.Rows.Count > 0)
            {
                DataRow row = jornada.Rows[0];
                if (row["jornada"] != null)
                tempo = Convert.ToDecimal(row["jornada"]);
            }

            return tempo;
        }

        public decimal GetJornadaEmpresa(string mes, string ano)
        {
            decimal tempo = 0;
            DataTable jornada = VestilloConnection.ExecToDataTable("SELECT SUM(p.jornada) as jornada  " +
                                                  " FROM produtividade p  " +
                                                  " INNER JOIN funcionarios f ON p.FuncionarioId = f.id  " +
                                                  " WHERE f.ativo = 1 AND MONTH(p.data) = " + mes + " AND YEAR(p.data) = " + ano );
            if (jornada.Rows.Count > 0)
            {
                DataRow row = jornada.Rows[0];
                if (row["jornada"] != null)
                    tempo = Convert.ToDecimal(row["jornada"]);
            }

            return tempo;
        }

        public decimal GetTempoOperacao(string id, string mes, string ano)
        {
            decimal tempo = 0;
            DataTable tempoOperacao = VestilloConnection.ExecToDataTable("SELECT SUM(gp.Tempo*pa.quantidade + p.TempoPacote) as tempoOperacao " +
                                                  " FROM operacaooperadora oo  " +
                                                  " INNER JOIN pacotes pa ON pa.id = oo.PacoteId  " +
                                                  " INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId  " +
                                                  " INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id  AND oo.Sequencia = gp.Sequencia)  " +
                                                  " INNER JOIN produtos p ON p.id = pa.ProdutoId  " +
                                                  " WHERE MONTH(oo.data) = " + mes + " AND YEAR(oo.data) = " + ano +
                                                  " AND oo.FuncionarioId = " + id + " GROUP BY oo.FuncionarioId");

            if (tempoOperacao.Rows.Count > 0)
            {
                DataRow row = tempoOperacao.Rows[0];
                if (row["tempoOperacao"] != null)
                    tempo = Convert.ToDecimal(row["tempoOperacao"]);
            }

            return tempo;
        }

        public decimal GetTempoFuncionario(string id, string mes, string ano)
        {
            decimal tempo = 0;
            DataTable tempoFuncionario = VestilloConnection.ExecToDataTable("SELECT  SUM(Tempo) as tempoFuncionario  " +
                                                  " FROM tempofuncionario " +
                                                  " WHERE MONTH(data) = " + mes + " AND YEAR(data) = " + ano +
                                                  " AND funcionarioId = " + id + " GROUP BY funcionarioId ");
            if (tempoFuncionario.Rows.Count > 0)
            {
                DataRow row = tempoFuncionario.Rows[0];
                if (row["tempoFuncionario"] != null)
                    tempo = Convert.ToDecimal(row["tempoFuncionario"]);
            }

            return tempo;

        }

        public decimal GetTempoOperacaoEmpresa(string mes, string ano)
        {
            decimal tempo = 0;
            DataTable tempoOperacao = VestilloConnection.ExecToDataTable("SELECT SUM(gp.Tempo*pa.quantidade + p.TempoPacote) as tempoOperacao " +
                                                  " FROM operacaooperadora oo  " +
                                                  " INNER JOIN pacotes pa ON pa.id = oo.PacoteId  " +
                                                  " INNER JOIN grupopacote g ON g.id = pa.GrupoPacoteId  " +
                                                  " INNER JOIN grupooperacoes gp ON (gp.OperacaoPadraoid = oo.OperacaoId AND gp.GrupoPacoteId = g.Id  AND oo.Sequencia = gp.Sequencia)  " +
                                                  " INNER JOIN produtos p ON p.id = pa.ProdutoId  " +
                                                  " INNER JOIN funcionarios f ON oo.FuncionarioId = f.id  " +
                                                  " WHERE f.ativo = 1 AND MONTH(oo.data) = " + mes + " AND YEAR(oo.data) = " + ano );

            if (tempoOperacao.Rows.Count > 0)
            {
                DataRow row = tempoOperacao.Rows[0];
                if(row["tempoOperacao"] != null)
                    tempo = Convert.ToDecimal(row["tempoOperacao"]);
            }

            return tempo;
        }

        public decimal GetTempoFuncionarioEmpresa(string mes, string ano)
        {
            decimal tempo = 0;
            DataTable tempoFuncionario = VestilloConnection.ExecToDataTable("SELECT  SUM(tf.Tempo) as tempoFuncionario  " +
                                                  " FROM tempofuncionario tf" +
                                                  " INNER JOIN funcionarios f ON tf.funcionarioId = f.id  " +
                                                  " WHERE f.ativo = 1 AND MONTH(data) = " + mes + " AND YEAR(data) = " + ano );
            if (tempoFuncionario.Rows.Count > 0)
            {
                DataRow row = tempoFuncionario.Rows[0];
                if (row["tempoFuncionario"] != null)
                    tempo = Convert.ToDecimal(row["tempoFuncionario"]);
            }

            return tempo;

        }

        public IndicadoresFuncionario GetIndicadoresQuantitativos(List<int> idsFuncionario, string mes, string ano)
        {
            IndicadoresFuncionario indicadores = new IndicadoresFuncionario();
            indicadores.PacotesCriados = 0;
            indicadores.OperacoesLancadas = 0;
            indicadores.PecasProduzidas = 0;
            indicadores.Defeitos = 0;

            DataTable pacotesCriados = VestilloConnection.ExecToDataTable(" SELECT COUNT(*) as pacotes FROM pacotes  " +
                                                  " INNER JOIN grupopacote ON pacotes.grupopacoteid = grupopacote.id " +
                                                  " WHERE grupopacote.empresaid = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + " AND MONTH(grupopacote.data) = " + mes +" AND YEAR(grupopacote.data) = "+ ano );
            if (pacotesCriados.Rows.Count > 0)
            {
                DataRow row = pacotesCriados.Rows[0];
                if (row["pacotes"] != null)
                    indicadores.PacotesCriados = Convert.ToInt32(row["pacotes"]);
            }

            DataTable operacoes = VestilloConnection.ExecToDataTable(" SELECT COUNT(*) as operacoes FROM operacaooperadora oo  " +
                                                  " INNER JOIN pacotes p ON p.Id = oo.PacoteId " +
                                                  " INNER JOIN grupopacote ON p.grupopacoteid = grupopacote.id " +
                                                  " WHERE grupopacote.empresaid = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + 
                                                  " AND MONTH(grupopacote.data) = " + mes + " AND YEAR(grupopacote.data) = " + ano + " AND oo.FuncionarioId IN ( " + string.Join(",", idsFuncionario) + ") " );
            if (operacoes.Rows.Count > 0)
            {
                DataRow row = operacoes.Rows[0];
                if (row["operacoes"] != null)
                    indicadores.OperacoesLancadas = Convert.ToInt32(row["operacoes"]);
            }

            DataTable pecasDefeito = VestilloConnection.ExecToDataTable(" SELECT SUM(quantidadealternativa + quantidade - qtddefeito) as pecas, SUM(qtddefeito) as defeito FROM pacotes  " +
                                                  " INNER JOIN grupopacote ON pacotes.grupopacoteid = grupopacote.id " +
                                                  " WHERE grupopacote.empresaid = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + 
                                                  " AND MONTH(grupopacote.data) = " + mes + " AND YEAR(grupopacote.data) = " + ano + " AND pacotes.status = 6 ");
            if (pecasDefeito.Rows.Count > 0)
            {
                DataRow row = pecasDefeito.Rows[0];
                if (row["pecas"] != null)
                    indicadores.PecasProduzidas = Convert.ToInt32(row["pecas"]);
                if (row["defeito"] != null)
                    indicadores.Defeitos = Convert.ToInt32(row["defeito"]);
            }            

            return indicadores;

        }
        

    }
}
