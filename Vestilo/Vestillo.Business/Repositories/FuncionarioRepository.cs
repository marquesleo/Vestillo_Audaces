using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;
using Vestillo.Core.Models;

namespace Vestillo.Business.Repositories
{
    public class FuncionarioRepository : GenericRepository<Funcionario>
    {
        public FuncionarioRepository()
            : base(new DapperConnection<Funcionario>())
        {

        }

        public IEnumerable<FuncionarioView> GetAllView()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.Id, F.Referencia,F.Nome, F.CPF, F.DataNascimento, F.DataAdmissao, F.DataDemissao, F.Ativo,");
            sql.AppendLine("	C.Descricao AS Cargo, F.EmpresaId, F.UsaCupom");
            sql.AppendLine("FROM 	funcionarios AS F");
            sql.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            sql.AppendLine("ORDER BY F.Nome");

            var cn = new DapperConnection<FuncionarioView>();
            return cn.ExecuteStringSqlToList(new FuncionarioView(), sql.ToString());
        }

        public IEnumerable<Funcionario> GetAllAtivo()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	*");
            sql.AppendLine("FROM 	funcionarios");
            sql.AppendLine("WHERE Ativo = 1");
            sql.AppendLine("ORDER BY Nome");

            var cn = new DapperConnection<Funcionario>();
            return cn.ExecuteStringSqlToList(new Funcionario(), sql.ToString());
        }

        public IEnumerable<Funcionario> GetAllAtivoCupom()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	funcionarios.Id,funcionarios.Referencia,funcionarios.Nome,IF(funcionarios.usacupom = 0,'Não','Sim') as DescUsaCupom ");
            sql.AppendLine("FROM 	funcionarios");
            sql.AppendLine("WHERE Ativo = 1 AND funcionarios.EmpresaId = " + VestilloSession.EmpresaLogada.Id);
            sql.AppendLine("ORDER BY Nome");

            var cn = new DapperConnection<Funcionario>();
            return cn.ExecuteStringSqlToList(new Funcionario(), sql.ToString());
        }

        public List<FuncionarioView> GetByIdView(List<int> id)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.Referencia,F.Id, F.Nome, F.CPF, F.DataNascimento, F.DataAdmissao, F.DataDemissao, F.Ativo, F.CEP,F.Foto,");
            sql.AppendLine("	C.Descricao AS Cargo,F.Endereco,F.Complemento,F.Bairro,F.Municipio,F.EstadoId,F.CalendarioId,F.DDD,F.Telefone,F.RG,F.Obs");
            sql.AppendLine("FROM 	funcionarios AS F");
            sql.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            sql.AppendLine("   WHERE F.id");
            if (id.Count > 1)
            {
                sql.Append(" IN( ");
                sql.Append(string.Join(",", id));
                sql.Append(")");
            }
            else
            {
                sql.Append(" = ");
                sql.Append(id[0]);
            }

            sql.AppendLine(" ORDER BY F.Nome");

            var cn = new DapperConnection<FuncionarioView>();
            return cn.ExecuteStringSqlToList(new FuncionarioView(), sql.ToString()).ToList();
        }

        public List<FuncionariosRelListagemView> GetByIdViewCore(List<int> id, bool ativo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.Referencia,F.Id, F.Nome, F.CPF, F.DataNascimento, F.DataAdmissao, F.DataDemissao, F.Ativo, F.CEP,F.Foto,");
            sql.AppendLine("	C.Descricao AS Cargo,F.Endereco,F.Complemento,F.Bairro,F.Municipio,F.EstadoId,F.CalendarioId,F.DDD,F.Telefone,F.RG,F.Obs");
            sql.AppendLine("FROM 	funcionarios AS F");
            sql.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            
            if (id.Count > 0)
            {
                sql.AppendLine("   WHERE F.id");
                sql.Append(" IN( ");
                sql.Append(string.Join(",", id));
                sql.Append(")");

                if (ativo)
                    sql.AppendLine(" AND F.ativo = 1 ");
            }
            else if (ativo)
            {
                sql.AppendLine(" WHERE F.ativo = 1 ");
            }            

            sql.AppendLine(" ORDER BY F.Nome");

            var cn = new DapperConnection<FuncionariosRelListagemView>();
            return cn.ExecuteStringSqlToList(new FuncionariosRelListagemView(), sql.ToString()).ToList();
        }

        public List<FuncionarioView> GetByAniversariantes(string DoMes,String AteMEs, bool ativo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.Referencia,F.Id, F.Nome,  F.DataNascimento, F.Foto,CAST(IFNULL(month(F.DataNascimento),'') as unsigned integer) as Mes,");
            sql.AppendLine("F.DDD,F.Telefone,F.RG,F.Obs");
            sql.AppendLine("FROM 	funcionarios AS F");
            sql.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            sql.AppendLine("   WHERE IFNULL(month(F.DataNascimento),'') BETWEEN " + DoMes + " AND " + AteMEs);

            if(ativo)
                sql.AppendLine("   AND F.ativo = 1 ");


            sql.AppendLine(" ORDER BY CAST(IFNULL(month(F.DataNascimento),'') as unsigned integer)");

            var cn = new DapperConnection<FuncionarioView>();
            return cn.ExecuteStringSqlToList(new FuncionarioView(), sql.ToString()).ToList();
        }

        public IEnumerable<Funcionario> GetByIds(List<int> ids, string ordenacao)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("FROM funcionarios");
            sql.AppendLine("   WHERE id");
            if (ids.Count > 1)
            {
                sql.Append(" IN( ");
                sql.Append(string.Join(",", ids));
                sql.Append(")");
            }
            else if (ids.Count == 0)
            {
                sql.Append(" > 0 ");
            }
            else
            {
                sql.Append(" = ");
                sql.Append(ids[0]);
            }

            sql.AppendLine(" ORDER BY " + ordenacao);

            var cn = new DapperConnection<Funcionario>();
            return cn.ExecuteStringSqlToList(new Funcionario(), sql.ToString());
        }

        public IEnumerable<Funcionario> GetByIdsEPremios(List<int> premios, List<int> ids, string ordenacao, bool premioPartida)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT f.* ");
            sql.AppendLine("FROM funcionarios f ");

            if(!premioPartida)
                sql.AppendLine("LEFT JOIN pessoaspremio p ON p.PessoaId = f.Id");
            else
                sql.AppendLine("LEFT JOIN premiopartidafuncionarios p ON p.IdFuncionario = f.Id");

            sql.AppendLine("   WHERE f.id ");
            if (ids.Count > 1)
            {
                sql.Append(" IN( ");
                sql.Append(string.Join(",", ids));
                sql.Append(")");
            }
            else if (ids.Count == 0)
            {
                sql.Append(" > 0 ");
            }
            else
            {
                sql.Append(" = ");
                sql.Append(ids[0]);
            }

            if (premioPartida)
            {
                if (premios.Count > 0)
                {
                    sql.AppendLine("  AND p.IdPremio");
                    sql.Append(" IN( ");
                    sql.Append(string.Join(",", premios));
                    sql.Append(")");
                }
            }
            else
            {
                if (premios.Count > 0)
                {
                    sql.AppendLine("  AND p.PremioId");
                    sql.Append(" IN( ");
                    sql.Append(string.Join(",", premios));
                    sql.Append(")");
                }
            }            

            sql.AppendLine(" ORDER BY " + ordenacao);

            var cn = new DapperConnection<Funcionario>();
            return cn.ExecuteStringSqlToList(new Funcionario(), sql.ToString());
        }

        public IEnumerable<Funcionario> GetByPremios(List<int> premios, string ordenacao, bool premioPartida = false)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT f.* ");
            sql.AppendLine("FROM funcionarios f");

            if (!premioPartida)
            {
                sql.AppendLine("INNER JOIN pessoaspremio p ON p.PessoaId = f.Id");
                sql.AppendLine("   WHERE p.PremioId");
                if (premios.Count > 1)
                {
                    sql.Append(" IN( ");
                    sql.Append(string.Join(",", premios));
                    sql.Append(")");
                }
                else if (premios.Count == 0)
                {
                    sql.Append(" > 0 ");
                }
                else
                {
                    sql.Append(" = ");
                    sql.Append(premios[0]);
                }
            }
            else
            {
                sql.AppendLine("INNER JOIN premiopartidafuncionarios p ON p.IdFuncionario = f.Id");
                sql.AppendLine("   WHERE p.IdPremio");
                if (premios.Count > 1)
                {
                    sql.Append(" IN( ");
                    sql.Append(string.Join(",", premios));
                    sql.Append(")");
                }
                else if (premios.Count == 0)
                {
                    sql.Append(" > 0 ");
                }
                else
                {
                    sql.Append(" = ");
                    sql.Append(premios[0]);
                }
            }

            sql.AppendLine(" ORDER BY " + ordenacao);

            var cn = new DapperConnection<Funcionario>();
            return cn.ExecuteStringSqlToList(new Funcionario(), sql.ToString());
        }

        public IEnumerable<FuncionarioView> GetListPorCargo(string cargo)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.Id, F.Nome, F.CPF, F.DataNascimento, F.DataAdmissao, F.DataDemissao, F.Ativo,");
            sql.AppendLine("	C.Descricao AS Cargo");
            sql.AppendLine("FROM 	funcionarios AS F");
            sql.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            sql.AppendLine(" WHERE F.CalculaProducao = 1 AND F.Ativo = 1");
            sql.AppendLine("ORDER BY F.Nome");

            var cn = new DapperConnection<FuncionarioView>();
            return cn.ExecuteStringSqlToList(new FuncionarioView(), sql.ToString());
        }

        public IEnumerable<FuncionarioView> GetListPorNome(string nome)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.Id, F.Nome,F.EmpresaId, F.CPF, F.DataNascimento, F.DataAdmissao, F.DataDemissao, F.Ativo,F.UsaCupom, ");
            sql.AppendLine("	C.Descricao AS Cargo");
            sql.AppendLine("FROM 	funcionarios AS F");
            sql.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            sql.AppendLine(" WHERE F.Nome like '%" + nome + "%' ");
            sql.AppendLine("ORDER BY F.Nome");

            var cn = new DapperConnection<FuncionarioView>();
            return cn.ExecuteStringSqlToList(new FuncionarioView(), sql.ToString());
        }

        public IEnumerable<FuncionarioSimplificado> GetListComCargo()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	F.*,");
            sql.AppendLine("	C.Abreviatura AS CargoDescricao");
            sql.AppendLine("FROM 	funcionarios AS F");
            sql.AppendLine("   LEFT JOIN Cargos AS C ON C.Id = F.CargoId");
            sql.AppendLine("ORDER BY F.Id");

            var cn = new DapperConnection<FuncionarioSimplificado>();
            return cn.ExecuteStringSqlToList(new FuncionarioSimplificado(), sql.ToString());
        }

        public bool CpfRGExiste(string Cpf, string RG)
        {
            bool Encontrado = false;

            var SQL = new StringBuilder();
            var cn = new DapperConnection<Colaborador>();

            SQL.AppendLine("select * from funcionarios WHERE" + FiltroEmpresa());
            if (Cpf != null)
            {
                SQL.AppendLine("AND CPF = " + "'" + Cpf + "'");
            }
            if(RG != null)
            {
                SQL.AppendLine("AND RG = " + "'" + RG + "'");
            }


            Colaborador ret = new Colaborador();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                Encontrado = true;
            }

            return Encontrado;

        }

        public IEnumerable<Funcionario> GetByCalendario(int idCalendario)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT 	*");
            sql.AppendLine("FROM 	funcionarios");
            sql.AppendLine("WHERE CalendarioId = " + idCalendario);

            var cn = new DapperConnection<Funcionario>();
            return cn.ExecuteStringSqlToList(new Funcionario(), sql.ToString());
        }

        public List<FuncionariosRelCracha> GetByViewCoreCracahs(List<int> id)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT funcionarios.id as idFuncionario, funcionarios.RelogioDePonto as codPonto,funcionarios.nome,IFNULL(cargos.Descricao,'') as cargo, ");
            sql.AppendLine(" funcionarios.Foto as Foto,empresas.NomeFantasia as Fantasia,CONCAT(enderecos.ddd, '-',enderecos.telefone) as Telefone");
            sql.AppendLine("FROM 	funcionarios");
            sql.AppendLine("  INNER JOIN  empresas ON empresas.id = funcionarios.EmpresaId");
            sql.AppendLine("  INNER JOIN  enderecos ON enderecos.EmpresaId = empresas.id ");            
            sql.AppendLine("   LEFT JOIN cargos ON cargos.Id = funcionarios.CargoId ");

            if (id.Count > 0)
            {
                sql.AppendLine("   WHERE funcionarios.id");
                sql.Append(" IN( ");
                sql.Append(string.Join(",", id));
                sql.Append(")");
            }

            sql.AppendLine(" ORDER BY  funcionarios.nome");

            var cn = new DapperConnection<FuncionariosRelCracha>();
            return cn.ExecuteStringSqlToList(new FuncionariosRelCracha(), sql.ToString()).ToList();
        }

        public List<FuncionarioPremioGrupo> GrupoPremioPartidaDiario(List<int> premios, DateTime daData, DateTime ateData, string ordenacao)
        {
            StringBuilder SQL =  new StringBuilder();
            List<FuncionarioPremioGrupo> ListValoresPremios = new List<FuncionarioPremioGrupo>();


            SQL.AppendLine(" SELECT funcionarios.id as CodFuncionario, funcionarios.referencia as RefFuncionario, funcionarios.nome as NomeFuncionario, premiopartida.id as PremioId, premiopartida.referencia as PremioReferencia, premiopartida.descricao as PremioDescricao, premiopartida.GruTipo, premiopartida.GruMinimo, premiopartida.GruMaximo, premiopartida.GruValor, premiopartida.GruValPartida, produtividade.data as ProdutividadeData, ");
            SQL.AppendLine(" (ROUND((IFNULL((SELECT SUM(ROUND(IFNULL(tempofuncionario.tempo,0),4)) FROM tempofuncionario WHERE tempofuncionario.funcionarioId = funcionarios.id AND tempofuncionario.data = produtividade.data),0) + IFNULL((SELECT SUM((ROUND(IFNULL(grupooperacoes.tempo,0),4) * IFNULL(pacotes.quantidade,0)) + ROUND(IFNULL(PR.tempopacote,0),4)) FROM operacaooperadora INNER JOIN pacotes ON pacotes.id = operacaooperadora.PacoteId INNER JOIN grupopacote ON grupopacote.id = pacotes.grupopacoteid INNER JOIN grupooperacoes ON grupooperacoes.grupopacoteId = grupopacote.id INNER JOIN produtos PR ON PR.id = pacotes.produtoId WHERE operacaooperadora.FuncionarioId = funcionarios.id AND operacaooperadora.data = produtividade.data AND NOT ISNULL(pacotes.referencia) AND (operacaooperadora.OperacaoId = grupooperacoes.operacaopadraoid OR ISNULL(operacaooperadora.OperacaoId))),0)),4)) AS Producao, ");
            SQL.AppendLine(" (ROUND(IFNULL(produtividade.jornada,0),4)) AS Jornada, ");
            SQL.AppendLine(" (ROUND(IFNULL((SELECT SUM(IF(ocorrencias.tipo = 0,ROUND(IFNULL(ocorrenciafuncionario.tempo,0),4),0)) FROM ocorrenciafuncionario INNER JOIN ocorrencias ON ocorrencias.id = ocorrenciafuncionario.ocorrenciaid WHERE ocorrenciafuncionario.funcionarioid = funcionarios.id AND ocorrenciafuncionario.data = produtividade.data),0),4)) AS Ocorrencia, ");
            SQL.AppendLine(" (ROUND(IFNULL((SELECT SUM(IF(IFNULL(ocorrencias.id,0)=3,ROUND(IFNULL(ocorrenciafuncionario.tempo,0),4),0)) FROM ocorrenciafuncionario INNER JOIN ocorrencias ON ocorrencias.id = ocorrenciafuncionario.ocorrenciaid WHERE ocorrenciafuncionario.funcionarioid = funcionarios.id AND ocorrenciafuncionario.data = produtividade.data),0),4)) AS Falta, ");
            SQL.AppendLine(" (ROUND(IFNULL((SELECT SUM(IF(IFNULL(ocorrencias.id,0)=1,ROUND(IFNULL(ocorrenciafuncionario.tempo,0),4),0)) FROM ocorrenciafuncionario INNER JOIN ocorrencias ON ocorrencias.id = ocorrenciafuncionario.ocorrenciaid WHERE ocorrenciafuncionario.funcionarioid = funcionarios.id AND ocorrenciafuncionario.data = produtividade.data),0),4)) AS BHDebito, ");
            SQL.AppendLine(" (ROUND(IFNULL((SELECT SUM(IF(IFNULL(ocorrencias.id,0)=5,ROUND(IFNULL(ocorrenciafuncionario.tempo,0),4),0)) FROM ocorrenciafuncionario INNER JOIN ocorrencias ON ocorrencias.id = ocorrenciafuncionario.ocorrenciaid WHERE ocorrenciafuncionario.funcionarioid = funcionarios.id AND ocorrenciafuncionario.data = produtividade.data),0),4)) AS Punicao, ");
            SQL.AppendLine(" (ROUND(IFNULL((SELECT SUM(IF(IFNULL(ocorrencias.id,0)=4 OR IFNULL(ocorrencias.id,0)=2,ROUND(IFNULL(ocorrenciafuncionario.tempo,0),4),0)) FROM ocorrenciafuncionario INNER JOIN ocorrencias ON ocorrencias.id = ocorrenciafuncionario.ocorrenciaid WHERE ocorrenciafuncionario.funcionarioid = funcionarios.id AND ocorrenciafuncionario.data = produtividade.data),0),4)) AS Extra ");            
            SQL.AppendLine(" FROM funcionarios ");
            SQL.AppendLine(" INNER JOIN empresas ON empresas.id  = funcionarios.EmpresaId ");
            SQL.AppendLine(" INNER JOIN premiopartidafuncionarios ON premiopartidafuncionarios.IdFuncionario = funcionarios.id ");
            SQL.AppendLine(" INNER JOIN premiopartida ON premiopartida.id = premiopartidafuncionarios.IdPremio AND premiopartida.GruTipo <> 0 ");
            SQL.AppendLine(" INNER JOIN produtividade ON produtividade.FuncionarioId = funcionarios.id AND produtividade.data BETWEEN " + "'" + daData.ToString("yyyy-MM-dd") + "'" + " AND " + "'" + ateData.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine(" WHERE empresas.id = " + VestilloSession.EmpresaLogada.Id);
            SQL.AppendLine(" AND premiopartida.id ");
            
            if (premios.Count > 1)
            {
                SQL.Append(" IN( ");
                SQL.Append(string.Join(",", premios));
                SQL.Append(")");
            }
            else if (premios.Count == 0)
            {
                SQL.Append(" > 0 ");
            }
            else
            {
                SQL.Append(" = ");
                SQL.Append(premios[0]);
            }

            SQL.Append(" GROUP BY funcionarios.id, produtividade.data ");
            SQL.Append(" ORDER BY " + ordenacao);


            var cn = new DapperConnection<FuncionarioPremioGrupo>();
            var Dados =  cn.ExecuteStringSqlToList(new FuncionarioPremioGrupo(), SQL.ToString()).ToList();

            List<FuncionarioPremioGrupo> DadosIniciais = new List<FuncionarioPremioGrupo>();
            int UltimoFuncionario = 0;
            foreach (var itemDadosIniciais in Dados)
            {
                decimal dblJornada = 0;
                decimal dblOcorrencia = 0;
                decimal dblFalta = 0;
                decimal dblExtra = 0;
                decimal dbMinutosProduzidos = 0;

                int CodFuncionario = itemDadosIniciais.CodFuncionario;
                if(UltimoFuncionario != CodFuncionario)
                {
                    var DadosFuncionario = Dados.Where(x => x.CodFuncionario == CodFuncionario);
                    var PessoasPremio = new FuncionarioPremioGrupo();
                    PessoasPremio.RefFuncionario = itemDadosIniciais.RefFuncionario;
                    PessoasPremio.NomeFuncionario = itemDadosIniciais.NomeFuncionario;
                    PessoasPremio.PremioId = itemDadosIniciais.PremioId;
                    PessoasPremio.PremioReferencia = itemDadosIniciais.PremioReferencia;
                    PessoasPremio.PremioDescricao = itemDadosIniciais.PremioDescricao;
                    foreach (var itemFuncionario in DadosFuncionario)
                    {
                        dblJornada += itemFuncionario.Jornada;
                        dblOcorrencia += itemFuncionario.Ocorrencia;
                        dblFalta += itemFuncionario.Falta + itemFuncionario.BHDebito;
                        dblExtra += itemFuncionario.Extra;
                        dbMinutosProduzidos +=  (decimal.Round(itemFuncionario.Producao, 4) - decimal.Round(itemFuncionario.Punicao, 4));
                    }
                    UltimoFuncionario = CodFuncionario;
                    PessoasPremio.Jornada = dblJornada;
                    PessoasPremio.Ocorrencia = dblOcorrencia;
                    PessoasPremio.Falta = dblFalta;
                    PessoasPremio.Extra = dblExtra;
                    PessoasPremio.MinutosProduzidos = dbMinutosProduzidos;

                    decimal dblAux = 0;
                    //Calcula Presença
                    if (decimal.Round(PessoasPremio.Jornada, 2) - decimal.Round(PessoasPremio.Extra, 4) > 0)
                    {
                        dblAux = (decimal.Round(PessoasPremio.Jornada, 2) - decimal.Round(PessoasPremio.Extra, 4)) - decimal.Round(PessoasPremio.Falta, 4);
                        if (dblAux < 0)
                        {
                            dblAux = 0;
                        }
                        PessoasPremio.Presenca = dblAux / (decimal.Round(PessoasPremio.Jornada, 2) - decimal.Round(PessoasPremio.Extra, 4));
                        if (decimal.Round(PessoasPremio.Presenca, 4) > 1)
                        {
                            PessoasPremio.Presenca = 1;
                        }
                    }

                    PessoasPremio.Presenca = decimal.Round(PessoasPremio.Presenca * 100,4);

                    //Calcula o Aproveitamento                 
                    dblAux = decimal.Round(PessoasPremio.Jornada, 2) - decimal.Round(PessoasPremio.Falta, 4);
                    if (dblAux > 0)
                    {
                        if (dblAux - decimal.Round(PessoasPremio.Ocorrencia, 4) < 0)
                        {
                            PessoasPremio.Aproveitamento = 0;
                        }
                        else
                        {
                            PessoasPremio.Aproveitamento = (dblAux - decimal.Round(PessoasPremio.Ocorrencia, 4)) / dblAux;

                        }
                        if (decimal.Round(PessoasPremio.Aproveitamento, 4) > 1)
                        {
                            PessoasPremio.Aproveitamento = 1;
                        }
                    }

                    PessoasPremio.Aproveitamento = decimal.Round(PessoasPremio.Aproveitamento * 100,4);

                    //Cálcula a Eficiência
                    dblAux = decimal.Round(PessoasPremio.Jornada, 2) - decimal.Round(PessoasPremio.Falta, 4) - decimal.Round(PessoasPremio.Ocorrencia, 4);
                    if (dblAux > 0)
                    {
                        PessoasPremio.Eficiencia = decimal.Round(decimal.Round(PessoasPremio.MinutosProduzidos, 4) / dblAux, 4);
                    }
                    PessoasPremio.MinutosUtil = decimal.Round((decimal.Round(PessoasPremio.Jornada, 2)  + decimal.Round(PessoasPremio.Extra, 2)) - decimal.Round(PessoasPremio.Ocorrencia, 4) - decimal.Round(PessoasPremio.Falta, 4), 4);

                    DadosIniciais.Add(PessoasPremio);
                }
                

            }


            /*
            List<FuncionarioPremioGrupo> DadosFinais = new List<FuncionarioPremioGrupo>();
            foreach (var item in premios)
            {
                var ValoresPremios = ListValoresPremios.Where(x => x.PremioId == item).ToList();
                var PessoasPremio = Dados.Where(y => y.PremioId == item).ToList();
                foreach (var itemValPremios in PessoasPremio)
                {
                    var PessoasFinais = new FuncionarioPremioGrupo();
                    PessoasFinais.RefFuncionario = itemValPremios.RefFuncionario;
                    PessoasFinais.NomeFuncionario = itemValPremios.NomeFuncionario;
                    PessoasFinais.PremioDescricao = itemValPremios.PremioDescricao;
                    PessoasFinais.PremioId = item;
                    PessoasFinais.PremioReferencia = itemValPremios.PremioReferencia;
                    PessoasFinais.QtdFunc = FuncionariosPorPremio(item);                    
                    PessoasFinais.GruMinimo = ValoresPremios[0].GruMinimo;
                    PessoasFinais.Jornada = ValoresPremios[0].Jornada;
                    PessoasFinais.MinutosProduzidos = ValoresPremios[0].MinutosProduzidos;
                    PessoasFinais.PremioDia = ValoresPremios[0].PremioDia;
                    PessoasFinais.PremioMes = ValoresPremios[0].PremioMes;
                    PessoasFinais.Ocorrencia = ValoresPremios[0].Ocorrencia;
                    PessoasFinais.Falta = ValoresPremios[0].Falta;
                    PessoasFinais.Extra = ValoresPremios[0].Extra;
                    PessoasFinais.MinutosUtil = ValoresPremios[0].GruMinimo;
                    PessoasFinais.Eficiencia = ValoresPremios[0].GruMinimo;
                    PessoasFinais.Aproveitamento = ValoresPremios[0].GruMinimo;
                    PessoasFinais.Presenca = ValoresPremios[0].GruMinimo;
                    DadosFinais.Add(PessoasFinais);

                }
            } 
            */

            return DadosIniciais;

        }

        public int FuncionariosPorPremio(int IdPremio)
        {
            int QtdFunc = 0;
            string SQL = String.Empty;
            SQL = "SELECT COUNT(*) AS QtdFunc FROM premiopartidafuncionarios P1 WHERE P1.IdPremio = " + IdPremio;
            var cn = new DapperConnection<FuncionarioPremioGrupo>();
            var Dados = cn.ExecuteStringSqlToList(new FuncionarioPremioGrupo(), SQL).ToList();

            foreach (var item in Dados)
            {
                QtdFunc = item.QtdFunc;
            }

            return QtdFunc;


        }

        public void AtualizaLicencaCupom(List<Funcionario> FuncsParaGravar)
        {
            try
            {
                foreach (var item in FuncsParaGravar)
                {
                    string SQL = String.Empty;
                    SQL = "UPDATE funcionarios SET UsaCupom = " + item.UsaCupom + " WHERE Id = " + item.Id;
                    _cn.ExecuteNonQuery(SQL);

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }


            

        }
    }
}
