using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;
using Vestillo.Connection;
using System.Data;

namespace Vestillo.Core.Repositories
{
    public class VendedorRepository : GenericRepository<Vendedor>
    {
        public Vendedor FindByUsuario(int usuarioId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  Id, Nome");
            sql.AppendLine("FROM    Colaboradores");
            sql.AppendLine("WHERE   Vendedor = 1 AND UsuarioId = " + usuarioId.ToString());
            sql.AppendLine("ORDER BY Id DESC");

            return VestilloConnection.ExecSQLToModel<Vendedor>(sql.ToString());
        }

        public FunilDeVendasView ListDadosFunilDeVendas(int[] id, DateTime[] mesAno)
        {
            string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();
            
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT  ");
            sql.AppendLine("    SUM(QtdAtividadeRegistrada) AS QtdAtividadeRegistrada,");
            sql.AppendLine("    SUM(QtdClientesAtivos) AS QtdClientesAtivos,");
            sql.AppendLine("    SUM(QtdAtividadeRegistradaMes) AS QtdAtividadeRegistradaMes,");
            sql.AppendLine("    SUM(QtdPedidosMes) AS QtdPedidosMes,");
            sql.AppendLine("    SUM(QtdFaturados) AS QtdFaturados");
            sql.AppendLine("FROM");
            sql.AppendLine("(");
            sql.AppendLine("     SELECT");
            sql.AppendLine("		    (SELECT COUNT(*) FROM Colaboradores CLI WHERE CLI.Cliente = 1 AND CLI.IdVendedor = VEN.id AND CLI.Ativo = 1) AS QtdClientesAtivos,");
            sql.AppendLine("		    (SELECT COUNT(*) FROM Colaboradores CLI WHERE CLI.Cliente = 1 AND CLI.IdVendedor = VEN.id AND CLI.Ativo = 1");
            sql.AppendLine("		    AND (SELECT COUNT(*) FROM Atividades ATI WHERE ATI.ClienteId = CLI.Id)) AS QtdAtividadeRegistrada,");
            sql.AppendLine("");
            sql.AppendLine("		    (SELECT COUNT(*) FROM Colaboradores CLI WHERE CLI.Cliente = 1 AND CLI.IdVendedor = VEN.id AND CLI.Ativo = 1");
            sql.AppendLine("		    AND (SELECT COUNT(*) FROM Atividades ATI WHERE ATI.ClienteId = CLI.Id AND DATE_FORMAT(ATI.DataAtividade, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")) > 0) AS QtdAtividadeRegistradaMes,");
            sql.AppendLine("");
            sql.AppendLine("		    (SELECT COUNT(*) FROM Colaboradores CLI WHERE CLI.Cliente = 1 AND CLI.IdVendedor = VEN.id AND CLI.Ativo = 1");
            sql.AppendLine("		    AND (SELECT COUNT(*) FROM PedidoVenda PED WHERE PED.ClienteId = CLI.Id AND DATE_FORMAT(PED.DataEmissao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")) > 0) AS QtdPedidosMes,");
            sql.AppendLine("");
            sql.AppendLine("		    (SELECT COUNT(*) FROM Colaboradores CLI WHERE CLI.Cliente = 1 AND CLI.IdVendedor = VEN.id AND CLI.Ativo = 1");
            sql.AppendLine("		    AND (SELECT COUNT(*) FROM NFE WHERE NFE.Tipo = 0  AND NFE.StatusNota <> 2 AND NFE.IdColaborador = CLI.Id AND DATE_FORMAT(NFE.DataInclusao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")) > 0) AS QtdFaturados");
            sql.AppendLine("");
            sql.AppendLine("    FROM 	Colaboradores AS VEN");
            sql.AppendLine("    WHERE 	VEN.Id IN (" + string.Join(", ", id) + ")");
            sql.AppendLine(") T");


            return VestilloConnection.ExecSQLToModel<FunilDeVendasView>(sql.ToString());
        }

        public DashVendedorLead ListDadosClientesLead(int[] id, DateTime mesAno)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("	(SELECT COUNT(*) ");
            sql.AppendLine("	 FROM 	colaboradores C");
            sql.AppendLine("	 WHERE 	C.cliente = 1 AND C.Ativo = 1 ");
            sql.AppendLine("	        AND " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("			AND C.TipoCliente <> 0 AND C.idvendedor IN (" + string.Join(", ", id) + ")) AS TotalLead");
            sql.AppendLine("");
            sql.AppendLine("   ,(SELECT COUNT(*)");
            sql.AppendLine("	 FROM 	colaboradores C");
            sql.AppendLine("	 WHERE 	C.cliente = 1  ");
            sql.AppendLine("			AND C.TipoCliente = 1");
            sql.AppendLine("	        AND " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("			AND DATE_FORMAT(C.DataCadastro, '%m-%Y') = '" + mesAno.ToString("MM-yyyy") + "' AND C.idvendedor IN (" + string.Join(", ", id) + ")) AS TotalLeadMes");
            sql.AppendLine("");
            sql.AppendLine("   ,(SELECT COUNT(*)");
            sql.AppendLine("	 FROM 	colaboradores C");
            sql.AppendLine("	 WHERE 	C.cliente = 1  ");
            sql.AppendLine("	        AND " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("			AND DATE_FORMAT(C.DataCadastro, '%m-%Y') = '" + mesAno.ToString("MM-yyyy") + "'");
            sql.AppendLine("			AND C.DataLead IS NOT NULL AND C.DataPrimeiraCompra IS NOT NULL AND C.idvendedor IN (" + string.Join(", ", id) + ")) AS TotalLeadFaturado");
            sql.AppendLine("");
            sql.AppendLine("FROM DUAL");


            return VestilloConnection.ExecSQLToModel<DashVendedorLead>(sql.ToString());
        }

        public DashVendedorCarteiraClientes ListDadosCarteira(int[] id)
        {
            StringBuilder sql = new StringBuilder();

            

            sql.AppendLine("SELECT");
            sql.AppendLine("	(SELECT COUNT(*)");
            sql.AppendLine("	 FROM 	colaboradores C");
            sql.AppendLine("	 WHERE 	C.cliente = 1");
            sql.AppendLine("	        AND " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("	        AND C.idvendedor IN (" + string.Join(", ", id) + ")");
            sql.AppendLine("            AND C.Ativo = 1) AS TotalCarteiraClientes");
            sql.AppendLine("");
            sql.AppendLine("   ,(SELECT COUNT(*)");
            sql.AppendLine("	 FROM 	colaboradores C");
            sql.AppendLine("	 WHERE 	C.cliente = 1");
            sql.AppendLine("	        AND " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("	        AND C.idvendedor IN (" + string.Join(", ", id) + ")");
            sql.AppendLine("			AND (SELECT COUNT(*) FROM NFe WHERE NFe.IdColaborador = C.id AND TIMESTAMPDIFF(MONTH, Nfe.DataInclusao, SYSDATE()) <= 12) > 0 ");
            sql.AppendLine("	 ) AS FaturamentoUltimos12Meses");
            sql.AppendLine("");
            sql.AppendLine("   ,(SELECT COUNT(*)");
            sql.AppendLine("	 FROM 	colaboradores C");
            sql.AppendLine("	 WHERE 	C.cliente = 1");
            sql.AppendLine("	        AND " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("	        AND C.idvendedor IN (" + string.Join(", ", id) + ")");
            sql.AppendLine("			AND (SELECT COUNT(*) FROM NFe WHERE NFe.IdColaborador = C.id AND DATE_FORMAT(Nfe.DataInclusao, '%m-%Y') = DATE_FORMAT(SYSDATE(), '%m-%Y')) > 0 ");
            sql.AppendLine("	 ) AS TotalFaturadoMes");
            sql.AppendLine("");
            sql.AppendLine(" ,(SELECT COUNT(*)");
            sql.AppendLine("	 FROM 	colaboradores C");
            sql.AppendLine("	 WHERE 	C.cliente = 1");
            sql.AppendLine("	        AND " + FiltroEmpresa("C.IdEmpresa"));
            sql.AppendLine("	        AND C.idvendedor IN (" + string.Join(", ", id) + ")");
            sql.AppendLine("			AND DATE_FORMAT(C.DataPrimeiraCompra, '%m-%Y') = DATE_FORMAT(SYSDATE(), '%m-%Y')");
            sql.AppendLine("	 ) AS NovosClientes");
            sql.AppendLine("FROM DUAL;");

            return VestilloConnection.ExecSQLToModel<DashVendedorCarteiraClientes>(sql.ToString());
        }


        public IEnumerable<CurvaAbcView> GetCurvaAbc(int[] id, DateTime[] mesAno,int tipo = 0)
        {
            string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();

            StringBuilder sqlCorte = new StringBuilder();
            StringBuilder sqlCurvaAbc = new StringBuilder();

            decimal ValorCorte = 0;            
            var cnCorte = new DapperConnection<CurvaAbcView>();

            List<CurvaAbcView> MontaCurva = null;
            MontaCurva = new List<CurvaAbcView>();
            MontaCurva.Clear();


            if (tipo == 0)
            {
                sqlCorte.AppendLine("SELECT  1 as grupo,  ");
                sqlCorte.AppendLine("   SUM(n.TotalProdutos) as TotalProdutos, ");
                sqlCorte.AppendLine("    SUM(n.ValDesconto) as ValDesconto, ");
                sqlCorte.AppendLine("    ROUND(((SUM(n.TotalProdutos) - SUM(n.ValDesconto))) /3,2) as ValorParaCorte ");
                sqlCorte.AppendLine("FROM nfe n");
                sqlCorte.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador ");
                sqlCorte.AppendLine("    INNER JOIN  colaboradores ven ON ven.id = n.idvendedor "); 
                sqlCorte.AppendLine("    Left JOIN  colaboradores ven2 ON ven2.id = n.idvendedor2 ");
                sqlCorte.AppendLine("    WHERE  n.Tipo = 0  AND n.StatusNota <> 2 AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
                sqlCorte.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                sqlCorte.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");
            }
            else if (tipo == 1)
            {
                sqlCorte.AppendLine("SELECT  1 as grupo,  ");
                sqlCorte.AppendLine("   SUM(n.totaloriginal) as TotalProdutos, ");
                sqlCorte.AppendLine("   (SUM(n.valordesconto) + SUM(n.Descontogrid)) as ValDesconto, ");
                sqlCorte.AppendLine("   ROUND(SUM(n.total) /3,2) as ValorParaCorte ");
                sqlCorte.AppendLine("FROM nfce n");
                sqlCorte.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdCliente ");
                sqlCorte.AppendLine("    INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
                sqlCorte.AppendLine("    WHERE   n.StatusNota <> 2 AND DATE_FORMAT(n.dataemissao, '%m-%Y') ");
                sqlCorte.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                sqlCorte.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");

            }
            else
            {
                sqlCorte.AppendLine("SELECT SUM(ValorParaCorte) as ValorParaCorte   FROM (");
                sqlCorte.AppendLine("SELECT");
                sqlCorte.AppendLine("SUM(n.TotalProdutos) as TotalProdutos,");
                sqlCorte.AppendLine("SUM(n.ValDesconto) as ValDesconto,");
                sqlCorte.AppendLine("ROUND(((SUM(n.TotalProdutos) - SUM(n.ValDesconto))) /3,2) as ValorParaCorte");
                sqlCorte.AppendLine("FROM nfe n");
                sqlCorte.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador ");
                sqlCorte.AppendLine("    INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
                sqlCorte.AppendLine("    Left JOIN  colaboradores ven2 ON ven2.id = n.idvendedor2 ");
                sqlCorte.AppendLine("    WHERE  n.Tipo = 0  AND n.StatusNota <> 2 AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
                sqlCorte.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                sqlCorte.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");
                sqlCorte.AppendLine("UNION ALL");
                sqlCorte.AppendLine("SELECT");
                sqlCorte.AppendLine("SUM(n.totaloriginal) as TotalProdutos,");
                sqlCorte.AppendLine("(SUM(n.valordesconto) + SUM(n.Descontogrid)) as ValDesconto,");
                sqlCorte.AppendLine("ROUND(SUM(n.total) /3,2) as ValorParaCorte");
                sqlCorte.AppendLine("FROM nfce n");
                sqlCorte.AppendLine("INNER JOIN colaboradores cli ON cli.id = n.IdCliente");
                sqlCorte.AppendLine("INNER JOIN  colaboradores ven ON ven.id = n.idvendedor");
                sqlCorte.AppendLine("    WHERE   n.StatusNota <> 2 AND DATE_FORMAT(n.dataemissao, '%m-%Y') ");
                sqlCorte.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                sqlCorte.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");
                sqlCorte.AppendLine(")  as t1");

            }

            var crt = new CurvaAbcView();
            var dados = cnCorte.ExecuteStringSqlToList(crt, sqlCorte.ToString());


            if (dados != null && dados.Count() > 0)
            {
                List<CurvaAbcView> crCorte = new List<CurvaAbcView>();
                crCorte = dados.ToList();
                ValorCorte = crCorte[0].ValorParaCorte;

                if (tipo == 0)
                {
                    sqlCurvaAbc.AppendLine("SELECT  cli.id as IdCliente,cli.referencia as RefCliente, cli.razaosocial as NomeCliente,ven.razaosocial as NomeVendedor1,ven2.razaosocial as NomeVendedor2,SUM(n.TotalItens) as TotalItens,SUM(n.TotalProdutos) as TotalProdutos,SUM(n.ValDesconto) as ValDesconto,  ");
                    sqlCurvaAbc.AppendLine("  ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as ValorParaCorte,ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as TotalComDesconto,estados.abreviatura as Uf,rotavisitas.descricao as rota  ");
                    sqlCurvaAbc.AppendLine("FROM nfe n");
                    sqlCurvaAbc.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador  ");
                    sqlCurvaAbc.AppendLine("    INNER JOIN  colaboradores ven ON ven.id = n.idvendedor "); 
                    sqlCurvaAbc.AppendLine("    Left JOIN  colaboradores ven2 ON ven2.id = n.idvendedor2 ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  estados ON estados.id = cli.idestado ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  rotavisitas ON rotavisitas.id = cli.idrota ");

                    sqlCurvaAbc.AppendLine("    WHERE n.Tipo = 0  AND n.StatusNota <> 2  AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
                    sqlCurvaAbc.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                    sqlCurvaAbc.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");
                    sqlCurvaAbc.AppendLine("    GROUP BY cli.id order by ValorParaCorte desc ");
                }
                else if (tipo == 1)
                {
                    sqlCurvaAbc.AppendLine("SELECT  cli.id as IdCliente,cli.referencia as RefCliente, cli.razaosocial as NomeCliente,ven.razaosocial as NomeVendedor1, null as NomeVendedor2, SUM(n.totaloriginal) as TotalProdutos,SUM(n.valordesconto + n.descontogrid) as ValDesconto,  ");
                    sqlCurvaAbc.AppendLine("  ROUND(SUM(n.total),2) as ValorParaCorte,ROUND(SUM(n.total),2) as TotalComDesconto,estados.abreviatura as Uf,rotavisitas.descricao as rota  ");
                    sqlCurvaAbc.AppendLine("FROM nfce n");
                    sqlCurvaAbc.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.idcliente  ");
                    sqlCurvaAbc.AppendLine("    INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  estados ON estados.id = cli.idestado ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  rotavisitas ON rotavisitas.id = cli.idrota ");
                    //sqlCurvaAbc.AppendLine("    INNER JOIN  nfceitens nItn ON nItn.idnfce = n.id "); //SUM(nItn.quantidade) as TotalItens
                    sqlCurvaAbc.AppendLine("    WHERE n.StatusNota <> 2  AND DATE_FORMAT(n.dataemissao, '%m-%Y') ");
                    sqlCurvaAbc.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                    sqlCurvaAbc.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");
                    sqlCurvaAbc.AppendLine("    GROUP BY cli.id  desc "); 

                }
                else
                {
                    sqlCurvaAbc.AppendLine(" SELECT IdCliente,RefCliente,NomeCliente,NomeVendedor1,NomeVendedor2,SUM(TotalItens) as TotalItens,SUM(ValDesconto) as ValDesconto, SUM(ValorParaCorte) as ValorParaCorte, SUM(TotalComDesconto) as TotalComDesconto  FROM ( ");
                    sqlCurvaAbc.AppendLine(" SELECT  cli.id as IdCliente,cli.referencia as RefCliente, cli.razaosocial as NomeCliente,ven.razaosocial as NomeVendedor1,ven2.razaosocial as NomeVendedor2,SUM(n.TotalItens) as TotalItens,SUM(n.TotalProdutos) as TotalProdutos,SUM(n.ValDesconto) as ValDesconto,  ");
                    sqlCurvaAbc.AppendLine("  ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as ValorParaCorte,ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as TotalComDesconto,estados.abreviatura as Uf,rotavisitas.descricao as rota  ");
                    sqlCurvaAbc.AppendLine("FROM nfe n");
                    sqlCurvaAbc.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.IdColaborador  ");
                    sqlCurvaAbc.AppendLine("    INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
                    sqlCurvaAbc.AppendLine("    Left JOIN  colaboradores ven2 ON ven2.id = n.idvendedor2 ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  estados ON estados.id = cli.idestado ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  rotavisitas ON rotavisitas.id = cli.idrota ");
                    sqlCurvaAbc.AppendLine("    WHERE n.Tipo = 0  AND n.StatusNota <> 2  AND DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
                    sqlCurvaAbc.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                    sqlCurvaAbc.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");
                    sqlCurvaAbc.AppendLine("    GROUP BY cli.id  ");
                    sqlCurvaAbc.AppendLine(" UNION ALL");
                    sqlCurvaAbc.AppendLine("SELECT  cli.id as IdCliente,cli.referencia as RefCliente, cli.razaosocial as NomeCliente,ven.razaosocial as NomeVendedor1, null as NomeVendedor2,0 as TotalItens,SUM(n.totaloriginal) as TotalProdutos,SUM(n.valordesconto + n.descontogrid) as ValDesconto,  ");
                    sqlCurvaAbc.AppendLine("  ROUND(SUM(n.total),2) as ValorParaCorte,ROUND(SUM(n.total),2) as TotalComDesconto,estados.abreviatura as Uf,rotavisitas.descricao as rota  ");
                    sqlCurvaAbc.AppendLine("FROM nfce n");
                    sqlCurvaAbc.AppendLine("    INNER JOIN colaboradores cli ON cli.id = n.idcliente  ");
                    sqlCurvaAbc.AppendLine("    INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  estados ON estados.id = cli.idestado ");
                    sqlCurvaAbc.AppendLine(" LEFT JOIN  rotavisitas ON rotavisitas.id = cli.idrota ");
                    //sqlCurvaAbc.AppendLine("    INNER JOIN  nfceitens nItn ON nItn.idnfce = n.id "); //SUM(nItn.quantidade) as TotalItens
                    sqlCurvaAbc.AppendLine("    WHERE n.StatusNota <> 2  AND DATE_FORMAT(n.dataemissao, '%m-%Y') ");
                    sqlCurvaAbc.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                    sqlCurvaAbc.AppendLine("	        AND n.idvendedor IN (" + string.Join(", ", id) + ")");
                    sqlCurvaAbc.AppendLine("    GROUP BY cli.id ");
                    sqlCurvaAbc.AppendLine(" )as t1 Group by IdCliente order by ValorParaCorte desc ");

                }

                var crtCurva = new CurvaAbcView();
                var dadosCurva = cnCorte.ExecuteStringSqlToList(crtCurva, sqlCurvaAbc.ToString());
                if (dadosCurva != null && dadosCurva.Count() > 0)
                {
                    decimal CorteDaCurva = 0;
                    string Curva = "A";
                    
                    CurvaAbcView it = new CurvaAbcView();

                    foreach (var item in dadosCurva)
                    {
                        decimal SomaItens = 0;
                        if (tipo != 0)
                        {

                            StringBuilder sql = new StringBuilder();

                            sql.AppendLine(" SELECT IFNULL(SUM(nIt.quantidade),0) as TotalItens ");
                            sql.AppendLine("  FROM 	nfceitens nIt ");
                            sql.AppendLine("    INNER JOIN nfce ON nfce.id = nIt.idnfce ");
                            sql.AppendLine("	WHERE nfce.idcliente = " + item.IdCliente );
                            sql.AppendLine("    AND nfce.StatusNota <> 2  AND DATE_FORMAT(nfce.dataemissao, '%m-%Y') ");
                            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                            sql.AppendLine("	        AND nIt.Devolucao = 0 ");
                            sql.AppendLine("	        AND nfce.idvendedor IN (" + string.Join(", ", id) + ")");

                            DataTable dt = VestilloConnection.ExecToDataTable(sql.ToString());

                            SomaItens =  Convert.ToDecimal(dt.Rows[0]["TotalItens"]);
                        }

                        it = new CurvaAbcView();
                        it.IdCliente = item.IdCliente;
                        it.RefCliente = item.RefCliente;
                        it.NomeCliente = item.NomeCliente;
                        it.Uf = item.Uf;
                        it.rota = item.rota;
                        it.NomeVendedor1 = item.NomeVendedor1;
                        it.NomeVendedor2 = item.NomeVendedor2;
                        if (CorteDaCurva >= ValorCorte && Curva != "C")
                        {
                            CorteDaCurva = 0;
                            switch (Curva)
                            {
                                case "A": //Dinheiro
                                    Curva = "B";
                                    break;
                                case "B":
                                    Curva = "C";
                                    break;
                            }
                            
                        }
                        it.Curva = Curva;
                        it.TotalItens = item.TotalItens + SomaItens;
                        it.TotalComDesconto = item.TotalComDesconto;
                        CorteDaCurva += item.ValorParaCorte;
                        MontaCurva.Add(it);
                        
                    }
                }

            }
            return MontaCurva;
        }

        public decimal GetValorMetaRealizadaNoMes(int vendedorId, DateTime[] mesAno)
        {
            StringBuilder sql = new StringBuilder();
            string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();

            string[] mesSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM"), "'")).ToArray();

            sql.AppendLine("SELECT");
            sql.AppendLine("(((");
            sql.AppendLine("    SELECT  ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as Total");
            sql.AppendLine("	FROM 	nfe n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.IdColaborador  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.Tipo = 0  AND n.StatusNota <> 2");
            sql.AppendLine("	       AND ven.Id = " + vendedorId.ToString());
            sql.AppendLine("  AND        DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
            sql.AppendLine(") /  ");
            sql.AppendLine("(");
            sql.AppendLine("	SELECT SUM(IFNULL(M.Meta, 0))");
            sql.AppendLine("    FROM 	metavendedores M");
            sql.AppendLine("    WHERE	M.Mes IN(" + string.Join(", ", mesSQL) + ") ");
            sql.AppendLine("            AND M.VendedorId = " + vendedorId.ToString());
            sql.AppendLine(")) * 100) AS Valor");
            sql.AppendLine("FROM DUAL");

            DataTable dt = VestilloConnection.ExecToDataTable(sql.ToString());

            //primeiro esse
            sql = new StringBuilder();

            //NFCE
            sql.AppendLine("SELECT");
            sql.AppendLine("(((");
            sql.AppendLine("    SELECT  ROUND(SUM(n.Total) ,2) as Total");
            sql.AppendLine("	FROM 	NFCE n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.idcliente  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.StatusNota <> 2");
            sql.AppendLine("	       AND ven.Id = " + vendedorId.ToString());
            sql.AppendLine("            AND n.Total > 0 ");
            sql.AppendLine("  AND        DATE_FORMAT(n.dataemissao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
            sql.AppendLine(") /  ");
            sql.AppendLine("(");
            sql.AppendLine("	SELECT SUM(IFNULL(M.Meta, 0))");
            sql.AppendLine("    FROM 	metavendedores M");
            sql.AppendLine("    WHERE	M.Mes IN(" + string.Join(", ", mesSQL) + ") ");
            sql.AppendLine("            AND M.VendedorId = " + vendedorId.ToString());
            sql.AppendLine(")) * 100) AS Valor");
            sql.AppendLine("FROM DUAL");

            DataTable dtNFCE = VestilloConnection.ExecToDataTable(sql.ToString());

            decimal NFE = decimal.Parse("0" + dt.Rows[0][0].ToString()); 
            decimal NFCE = decimal.Parse("0" + dtNFCE.Rows[0][0].ToString());
            decimal Total = NFE + NFCE;
            return Total;

            //return decimal.Parse("0" + dt.Rows[0][0].ToString());

        }


        //Retorna Somenta a Meta Pedido Aline 23/07
        public decimal GetValorMetaNoMes(int vendedorId, DateTime[] mesAno)
        {
            StringBuilder sql = new StringBuilder();
            string[] mesSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM"), "'")).ToArray();

            sql.AppendLine("	SELECT SUM(IFNULL(M.Meta, 0))");
            sql.AppendLine("    FROM 	metavendedores M");
            sql.AppendLine("    WHERE	M.Mes IN(" + string.Join(", ", mesSQL) + ") ");
            sql.AppendLine("            AND M.VendedorId = " + vendedorId.ToString());           

            DataTable dt = VestilloConnection.ExecToDataTable(sql.ToString());

            return decimal.Parse("0" + dt.Rows[0][0].ToString());

        }

        //Retorna Somenta a Realizado Pedido Aline 23/07
        public decimal GetValorRealizadaNoMes(int vendedorId, DateTime[] mesAno)
        {
            StringBuilder sql = new StringBuilder();
            string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();

            sql.AppendLine("    SELECT  ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as Total");
            sql.AppendLine("	FROM 	nfe n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.IdColaborador  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.Tipo = 0  AND n.StatusNota <> 2");
            sql.AppendLine("	       AND ven.Id = " + vendedorId.ToString());
            sql.AppendLine("  AND        DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");

            DataTable dt = VestilloConnection.ExecToDataTable(sql.ToString());

            //NFCE
            sql = new StringBuilder();
            sql.AppendLine("    SELECT  ROUND(SUM(n.Total),2) as Total");
            sql.AppendLine("	FROM 	NFCE n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.idcliente  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.StatusNota <> 2");
            sql.AppendLine("	AND  n.Total > 0");
            sql.AppendLine("	       AND ven.Id = " + vendedorId.ToString());
            sql.AppendLine("  AND        DATE_FORMAT(n.dataemissao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");

            DataTable dtNFCE = VestilloConnection.ExecToDataTable(sql.ToString());

            decimal NFE = decimal.Parse("0" + dt.Rows[0][0].ToString());
            decimal NFCE = decimal.Parse("0" + dtNFCE.Rows[0][0].ToString());
            decimal Total = NFE + NFCE;
            return Total;
                                  

            //return decimal.Parse("0" + dt.Rows[0][0].ToString());

        }



        public decimal GetValorMetaRealizadaNoMesGeral(int[] id, DateTime[] mesAno)
        {
            StringBuilder sql = new StringBuilder();

            string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();

            string[] mesSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM"), "'")).ToArray();

            sql.AppendLine("SELECT");
            sql.AppendLine("(((");
            sql.AppendLine("    SELECT  ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as Total");
            sql.AppendLine("	FROM 	nfe n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.IdColaborador  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.Tipo = 0  AND n.StatusNota <> 2");
            sql.AppendLine("	       AND ven.Id IN (" + string.Join(", ", id) + ")");
            //sql.AppendLine("           AND DATE_FORMAT(n.DataInclusao, '%m-%Y') = '" + mesAno.ToString("MM-yyyy") + "'");
            sql.AppendLine("  AND        DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
            sql.AppendLine(") /  ");
            sql.AppendLine("(");
            sql.AppendLine("	SELECT SUM(IFNULL(M.Meta, 0))");
            sql.AppendLine("    FROM 	metavendedores M");
            sql.AppendLine("    WHERE	M.Mes  IN(" + string.Join(", ", mesSQL) + ") ");
            sql.AppendLine("            AND M.VendedorId IN (" + string.Join(", ", id) + ")");
            sql.AppendLine(")) * 100) AS Valor");
            sql.AppendLine("FROM DUAL");

            DataTable dt = VestilloConnection.ExecToDataTable(sql.ToString());


            //NFCE
            sql = new StringBuilder();
            sql.AppendLine("SELECT");
            sql.AppendLine("(((");
            sql.AppendLine("    SELECT  ROUND(SUM(n.Total),2) as Total");
            sql.AppendLine("	FROM 	NFCE n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.idcliente  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.StatusNota <> 2");
            sql.AppendLine("	       AND ven.Id IN (" + string.Join(", ", id) + ")");
            //sql.AppendLine("           AND DATE_FORMAT(n.DataInclusao, '%m-%Y') = '" + mesAno.ToString("MM-yyyy") + "'");
            sql.AppendLine("  AND        DATE_FORMAT(n.dataemissao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
            sql.AppendLine(") /  ");
            sql.AppendLine("(");
            sql.AppendLine("	SELECT SUM(IFNULL(M.Meta, 0))");
            sql.AppendLine("    FROM 	metavendedores M");
            sql.AppendLine("    WHERE	M.Mes  IN(" + string.Join(", ", mesSQL) + ") ");
            sql.AppendLine("            AND M.VendedorId IN (" + string.Join(", ", id) + ")");
            sql.AppendLine(")) * 100) AS Valor");
            sql.AppendLine("FROM DUAL");

            DataTable dtNFCE = VestilloConnection.ExecToDataTable(sql.ToString());

            decimal NFE = decimal.Parse("0" + dt.Rows[0][0].ToString());
            decimal NFCE = decimal.Parse("0" + dtNFCE.Rows[0][0].ToString());
            decimal Total = NFE + NFCE;
            return Total;

            //return decimal.Parse("0" + dt.Rows[0][0].ToString());

        }


        public decimal GetValorMetaNoMesGeral(int[] id, DateTime[] mesAno)
        {
            StringBuilder sql = new StringBuilder();

            string[] mesSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM"), "'")).ToArray();

            sql.AppendLine("	SELECT SUM(IFNULL(M.Meta, 0))");
            sql.AppendLine("    FROM 	metavendedores M");
            sql.AppendLine("    WHERE	M.Mes  IN(" + string.Join(", ", mesSQL) + ") ");
            sql.AppendLine("            AND M.VendedorId IN (" + string.Join(", ", id) + ")");
           

            DataTable dt = VestilloConnection.ExecToDataTable(sql.ToString());

            return decimal.Parse("0" + dt.Rows[0][0].ToString());

        }


        public decimal GetValorRealizadaNoMesGeral(int[] id, DateTime[] mesAno)
        {
            StringBuilder sql = new StringBuilder();
            string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();

            sql.AppendLine("    SELECT  ROUND(SUM(n.TotalProdutos) - SUM(n.ValDesconto),2) as Total");
            sql.AppendLine("	FROM 	nfe n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.IdColaborador  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.Tipo = 0  AND n.StatusNota <> 2");
            sql.AppendLine("	       AND ven.Id IN (" + string.Join(", ", id) + ")");
            //sql.AppendLine("           AND DATE_FORMAT(n.DataInclusao, '%m-%Y') = '" + mesAno.ToString("MM-yyyy") + "'");
            sql.AppendLine("  AND        DATE_FORMAT(n.DataInclusao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");

            DataTable dt = VestilloConnection.ExecToDataTable(sql.ToString());

            //NFCE
            sql = new StringBuilder();
            sql.AppendLine("    SELECT  ROUND(SUM(n.Total),2) as Total");
            sql.AppendLine("	FROM 	NFCE n");
            sql.AppendLine("		INNER JOIN colaboradores cli ON cli.id = n.idcliente  ");
            sql.AppendLine("		INNER JOIN  colaboradores ven ON ven.id = n.idvendedor ");
            sql.AppendLine("	WHERE  n.StatusNota <> 2");
            sql.AppendLine("	       AND ven.Id IN (" + string.Join(", ", id) + ")");
            //sql.AppendLine("           AND DATE_FORMAT(n.DataInclusao, '%m-%Y') = '" + mesAno.ToString("MM-yyyy") + "'");
            sql.AppendLine("  AND        DATE_FORMAT(n.dataemissao, '%m-%Y') ");
            sql.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");

            DataTable dtNFCE = VestilloConnection.ExecToDataTable(sql.ToString());


            decimal NFE = decimal.Parse("0" + dt.Rows[0][0].ToString());
            decimal NFCE = decimal.Parse("0" + dtNFCE.Rows[0][0].ToString());
            decimal Total = NFE + NFCE;
            return Total;

            //return decimal.Parse("0" + dt.Rows[0][0].ToString());

        }



        //retorna Itens comprados pelo cliente
        public IEnumerable<CurvaAbcItensView> GetCurvaAbcItens(int id, DateTime[] mesAno, int tipo = 0)
        {

            string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();

           
            StringBuilder sqlCurvaAbcItens = new StringBuilder();

            var cnItens = new DapperConnection<CurvaAbcItensView>();


            if (tipo == 0)
            {
                sqlCurvaAbcItens.AppendLine("select IFNULL(IF(SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado +  ");
                sqlCurvaAbcItens.AppendLine("   nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))<0,0.00,SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - ");
                sqlCurvaAbcItens.AppendLine("    (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))),0)  as TotalItens, produtos.Id as ProdutoId, produtos.Referencia as RefProduto,produtos.Descricao as DescrProduto,(SUM(nfeitens.quantidade) - SUM( nfeitens.Qtddevolvida)) as Qtd ");
                sqlCurvaAbcItens.AppendLine("FROM nfeitens ");
                sqlCurvaAbcItens.AppendLine("    INNER JOIN produtos ON produtos.id = nfeitens.iditem ");
                sqlCurvaAbcItens.AppendLine("    INNER JOIN nfe on nfe.id = nfeitens.IdNfe ");
                sqlCurvaAbcItens.AppendLine("  WHERE  NFE.Tipo = 0 AND NFE.StatusNota <> 2 AND DATE_FORMAT(NFE.DataInclusao, '%m-%Y') ");
                sqlCurvaAbcItens.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                sqlCurvaAbcItens.AppendLine("	        AND NFE.IdColaborador = " + id);
                sqlCurvaAbcItens.AppendLine(" GROUP BY produtos.Id order by TotalItens desc ");
            }
            else if (tipo == 1)
            {
                sqlCurvaAbcItens.AppendLine("select IFNULL(IF(SUM((nfceitens.preco * (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.DescontoRateado))<0,0.00,SUM((nfceitens.preco *   ");
                sqlCurvaAbcItens.AppendLine("   (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.DescontoRateado ))),0) as TotalItens,produtos.Id as ProdutoId,produtos.Referencia as RefProduto,produtos.Descricao as DescrProduto,(SUM(nfceitens.quantidade) - SUM(nfceitens.Qtddevolvida)) as Qtd ");
                sqlCurvaAbcItens.AppendLine("FROM nfceitens ");
                sqlCurvaAbcItens.AppendLine("    INNER JOIN produtos ON produtos.id = nfceitens.idproduto ");
                sqlCurvaAbcItens.AppendLine("    INNER JOIN nfce on nfce.id = nfceitens.idnfce ");
                sqlCurvaAbcItens.AppendLine("  WHERE  NFCE.StatusNota <> 2 AND DATE_FORMAT(NFCE.dataemissao, '%m-%Y') ");
                sqlCurvaAbcItens.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                sqlCurvaAbcItens.AppendLine("	        AND nfceitens.Devolucao = 0 " );
                sqlCurvaAbcItens.AppendLine("	        AND NFCE.idcliente = " + id);
                sqlCurvaAbcItens.AppendLine(" GROUP BY produtos.Id  order by TotalItens desc ");

            }
            else
            {
                sqlCurvaAbcItens.AppendLine("SELECT ProdutoId,RefProduto,DescrProduto , SUM(TotalItens) as TotalItens,SUM(Qtd) as Qtd   FROM (");
                 sqlCurvaAbcItens.AppendLine(" select IFNULL(IF(SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))<0,0.00,SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - " );
                 sqlCurvaAbcItens.AppendLine(" (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))),0)  as TotalItens, produtos.Id as ProdutoId, produtos.Referencia as RefProduto,produtos.Descricao as DescrProduto,(SUM(nfeitens.quantidade) - SUM( nfeitens.Qtddevolvida)) as Qtd  FROM nfeitens ");
                 sqlCurvaAbcItens.AppendLine(" INNER JOIN produtos ON produtos.id = nfeitens.iditem " );
                 sqlCurvaAbcItens.AppendLine(" INNER JOIN nfe on nfe.id = nfeitens.IdNfe ");
                 sqlCurvaAbcItens.AppendLine(" WHERE    NFE.Tipo = 0 AND NFE.StatusNota <> 2 AND DATE_FORMAT(NFE.DataInclusao, '%m-%Y')");
                 sqlCurvaAbcItens.AppendLine("      IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                 sqlCurvaAbcItens.AppendLine("	        AND NFE.IdColaborador = " + id);
                 sqlCurvaAbcItens.AppendLine(" GROUP BY produtos.Id ");
                 sqlCurvaAbcItens.AppendLine(" UNION ALL " );
                 sqlCurvaAbcItens.AppendLine(" select IFNULL(IF(SUM((nfceitens.preco * (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.DescontoRateado))<0,0.00,SUM((nfceitens.preco * " );
                 sqlCurvaAbcItens.AppendLine(" (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.DescontoRateado ))),0) as TotalItens,produtos.Id as ProdutoId,produtos.Referencia as RefProduto,produtos.Descricao as DescrProduto,(SUM(nfceitens.quantidade) - SUM(nfceitens.Qtddevolvida)) as Qtd   FROM nfceitens ");
                 sqlCurvaAbcItens.AppendLine(" INNER JOIN produtos ON produtos.id = nfceitens.idproduto ");
                 sqlCurvaAbcItens.AppendLine(" INNER JOIN nfce on nfce.id = nfceitens.idnfce ");
                 sqlCurvaAbcItens.AppendLine(" WHERE   NFCE.StatusNota <> 2 AND DATE_FORMAT(NFCE.dataemissao, '%m-%Y') ");
                 sqlCurvaAbcItens.AppendLine("		    IN(" + string.Join(", ", mesAnoSQL) + ")  ");
                 sqlCurvaAbcItens.AppendLine("	        AND nfceitens.Devolucao = 0 ");
                 sqlCurvaAbcItens.AppendLine("	        AND NFCE.idcliente = " + id);                             
                 sqlCurvaAbcItens.AppendLine(" GROUP BY produtos.Id)  as t1 Group by ProdutoId order by TotalItens desc ");

            }

            var crt = new CurvaAbcItensView();
            var dados = cnItens.ExecuteStringSqlToList(crt, sqlCurvaAbcItens.ToString());

            return dados;

        }

        public IEnumerable<ItenPorClienteView> ItensPorClientesNFe(List<int> ListaIdsProduto, DateTime DataInicio, DateTime DataFim,bool UnirEmpresas,int EmpresaLogada)
        {
            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";
            StringBuilder Itens = new StringBuilder();

            for (int i = 0; i < ListaIdsProduto.Count; i++)
            {
                Itens.Append( ListaIdsProduto[i].ToString() + ",");
            }

            if(Itens.Length > 0)
            {
                Itens.Remove(Itens.ToString().Length - 1, 1);
            }
            

            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("select nfeitens.IdItemNoPedido,colaboradores.RazaoSocial as RazaoSocial,Municipiosibge.UF as 'Uf',ven.razaosocial as Vendedor,");
            SQL.AppendLine("produtos.Referencia as RefItem,cores.Abreviatura as Cor,tamanhos.Abreviatura as Tamanho, SUM(nfeitens.quantidade) - SUM(nfeitens.Qtddevolvida) as  Quantidade,");
            SQL.AppendLine("ROUND(nfeitens.preco,2) as Preco,ROUND(SUM(nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz),2) as Descontos,");
            SQL.AppendLine("ROUND(IFNULL(IF(SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))<0,0.00,");
            SQL.AppendLine("SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + ");
            SQL.AppendLine("nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))),0),2) as Total, ");
            SQL.AppendLine(" tp.Descricao as TabelaPreco,  cat.descricao as Catalogo, gp.descricao as Grupo ");
            SQL.AppendLine("FROM nfeitens ");
            SQL.AppendLine("INNER JOIN nfe on nfe.id = nfeitens.IdNfe ");
            SQL.AppendLine("INNER JOIN produtos ON produtos.id =  nfeitens.iditem");
            SQL.AppendLine("INNER JOIN cores ON cores.id =  nfeitens.idcor");
            SQL.AppendLine("INNER JOIN tamanhos ON tamanhos.id =  nfeitens.idtamanho");
            SQL.AppendLine("INNER JOIN colaboradores ON colaboradores.id = NFE.IdColaborador ");
            SQL.AppendLine("LEFT JOIN municipiosibge on municipiosibge.id = colaboradores.idmunicipio ");
            SQL.AppendLine("LEFT JOIN rotavisitas on rotavisitas.id = colaboradores.idrota");
            SQL.AppendLine("LEFT JOIN colaboradores ven ON ven.id = nfe.idvendedor ");
            SQL.AppendLine("LEFT JOIN grupoprodutos gp ON gp.id = produtos.IdGrupo ");
            SQL.AppendLine("LEFT JOIN catalogo cat ON cat.id = produtos.IdCatalogo ");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp ON tp.id = nfe.idtabela ");
            SQL.Append("WHERE  ");
            if (Itens.Length > 0)
            {
                SQL.Append(" nfeitens.iditem IN(" + Itens + ") AND ");
            }
            
            SQL.AppendLine("   IFNULL(NFE.StatusNota,0) <> 2 AND nfe.Tipo = 0 AND"); //desconsidera canceladas
            SQL.Append(" SUBSTRING(NFE.datainclusao,1,10) BETWEEN " + Valor);
            if(UnirEmpresas == false)
            {

                SQL.Append(" AND NFE.idempresa = " + EmpresaLogada);
            }
            SQL.AppendLine(" GROUP BY NFE.IdColaborador,nfeitens.IdItemNoPedido,nfeitens.iditem,nfeitens.idcor,nfeitens.idtamanho ");
            SQL.AppendLine(" order by NFE.IdColaborador,nfeitens.iditem,nfeitens.idcor,nfeitens.idtamanho ");

            var cnItens = new DapperConnection<ItenPorClienteView>();

            var crt = new ItenPorClienteView();
            var dados = cnItens.ExecuteStringSqlToList(crt, SQL.ToString());



            if(dados != null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    if(item.RefItem != null)
                    {
                        if(item.IdItemNoPedido > 0)
                        {
                            string sql = "select itempedidovendaid from itensliberacaopedidovenda where Id = " + item.IdItemNoPedido;
                            DataTable libItens = VestilloConnection.ExecToDataTable(sql);
                            int IdLib = int.Parse(libItens.Rows[0][0].ToString());
                            string sql2 = "select pedidovenda.Referencia as RefPedido from itenspedidovenda INNER JOIN pedidovenda on pedidovenda.Id = itenspedidovenda.PedidoVendaId where itenspedidovenda.id =" + IdLib;
                            DataTable pedItens = VestilloConnection.ExecToDataTable(sql2);
                            item.Pedido = pedItens.Rows[0][0].ToString();
                        }
                        
                    }

                }

            }
            return dados;

            
        }

        public IEnumerable<ItenPorClienteView> ItensPorClientesNFCe(List<int> ListaIdsProduto, DateTime DataInicio, DateTime DataFim, bool UnirEmpresas, int EmpresaLogada)
        {
            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";
            StringBuilder Itens = new StringBuilder();

            for (int i = 0; i < ListaIdsProduto.Count; i++)
            {
                Itens.Append(ListaIdsProduto[i].ToString() + ",");
            }

            if (Itens.Length > 0)
            {
                Itens.Remove(Itens.ToString().Length - 1, 1);
            }

            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("select (SELECT id FROM itensliberacaopedidovenda WHERE itempedidovendaid = nfceitens.IdItemPedidoVenda LIMIT 1) as IdItemNoPedido,colaboradores.RazaoSocial as RazaoSocial,Municipiosibge.UF as 'Uf',ven.razaosocial as Vendedor,");
            SQL.AppendLine("produtos.Referencia as RefItem,cores.Abreviatura as Cor,tamanhos.Abreviatura as Tamanho, SUM(nfceitens.quantidade) - SUM(nfceitens.Qtddevolvida) as  Quantidade,");
            SQL.AppendLine("ROUND(nfceitens.preco,2) as Preco,(ROUND(SUM(nfceitens.descontorateado),2) + ROUND(SUM(nfceitens.descvalor),2))as Descontos,");
            SQL.AppendLine("ROUND(IFNULL(IF(SUM((nfceitens.preco * (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.descontorateado + nfceitens.DescValor))<0,0.00,");
            SQL.AppendLine("SUM((nfceitens.preco * (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.descontorateado + nfceitens.DescValor ))),0),2) as Total, ");
            SQL.AppendLine(" tp.Descricao as TabelaPreco,  cat.descricao as Catalogo, gp.descricao as Grupo ");
            SQL.AppendLine("FROM nfceitens ");
            SQL.AppendLine("INNER JOIN nfce on nfce.id = nfceitens.idnfce ");
            SQL.AppendLine("INNER JOIN produtos ON produtos.id =  nfceitens.idproduto");
            SQL.AppendLine("INNER JOIN cores ON cores.id =  nfceitens.idcor");
            SQL.AppendLine("INNER JOIN tamanhos ON tamanhos.id =  nfceitens.idtamanho");
            SQL.AppendLine("INNER JOIN colaboradores ON colaboradores.id = nfce.idcliente ");
            SQL.AppendLine("LEFT JOIN municipiosibge on municipiosibge.id = colaboradores.idmunicipio ");
            SQL.AppendLine("LEFT JOIN rotavisitas on rotavisitas.id = colaboradores.idrota");
            SQL.AppendLine("LEFT JOIN colaboradores ven ON ven.id = nfce.idvendedor ");
            SQL.AppendLine("LEFT JOIN grupoprodutos gp ON gp.id = produtos.IdGrupo ");
            SQL.AppendLine("LEFT JOIN catalogo cat ON cat.id = produtos.IdCatalogo ");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp ON tp.id = nfce.idtabelapreco ");
            SQL.Append("WHERE  ");
            if (Itens.Length > 0)
            {
                SQL.Append(" nfceitens.idproduto IN(" + Itens + ") AND ");
            }
            SQL.AppendLine("   IFNULL(nfce.statusnota,0) <> 2 AND "); // desconsidera notas canceladas
            //SQL.AppendLine("   IFNULL(nfce.statusnota,0) <> 2 AND nfe.Tipo = 0 AND");
            SQL.AppendLine("   nfceitens.Devolucao = 0 AND "); // desconsidera devolvidos
            SQL.Append(" SUBSTRING(nfce.DataEmissao,1,10) BETWEEN " + Valor);
            if (UnirEmpresas == false)
            {
                SQL.Append(" AND nfce.idempresa = " + EmpresaLogada);
            }
            SQL.AppendLine(" GROUP BY nfceitens.IdItemPedidoVenda, nfceitens.idproduto,nfceitens.idcor,nfceitens.idtamanho ");
            SQL.AppendLine(" order by nfce.idcliente,nfceitens.idproduto,nfceitens.idcor,nfceitens.idtamanho ");

            var cnItens = new DapperConnection<ItenPorClienteView>();

            var crt = new ItenPorClienteView();
            var dados = cnItens.ExecuteStringSqlToList(crt, SQL.ToString());



            if (dados != null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    if (item.RefItem != null)
                    {
                        if (item.IdItemNoPedido > 0)
                        {
                            string sql = "select itempedidovendaid from itensliberacaopedidovenda where Id = " + item.IdItemNoPedido;
                            DataTable libItens = VestilloConnection.ExecToDataTable(sql);
                            int IdLib = int.Parse(libItens.Rows[0][0].ToString());
                            string sql2 = "select pedidovenda.Referencia as RefPedido from itenspedidovenda INNER JOIN pedidovenda on pedidovenda.Id = itenspedidovenda.PedidoVendaId where itenspedidovenda.id =" + IdLib;
                            DataTable pedItens = VestilloConnection.ExecToDataTable(sql2);
                            item.Pedido = pedItens.Rows[0][0].ToString();
                        }

                    }

                }

            }
            return dados;


        }

        public IEnumerable<ItenPorClienteView> ItensPorClientes(List<int> ListaIdsProduto, DateTime DataInicio, DateTime DataFim, bool UnirEmpresas, int EmpresaLogada)
        {
            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";
            StringBuilder Itens = new StringBuilder();

            for (int i = 0; i < ListaIdsProduto.Count; i++)
            {
                Itens.Append(ListaIdsProduto[i].ToString() + ",");
            }

            if (Itens.Length > 0)
            {
                Itens.Remove(Itens.ToString().Length - 1, 1);
            }


            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine(" SELECT IdItemNoPedido, Vendedor, RazaoSocial, Uf, RefItem, Cor, Tamanho, sum(Quantidade) as Quantidade, sum(Preco) as Preco, sum(Descontos) as Descontos,  ");
            SQL.AppendLine(" sum(Total) as Total, TabelaPreco,  Catalogo, Grupo ");
            SQL.AppendLine("  FROM  ( (  ");

            SQL.AppendLine("select nfeitens.IdItemNoPedido,colaboradores.RazaoSocial as RazaoSocial,Municipiosibge.UF as 'Uf',ven.razaosocial as Vendedor,");
            SQL.AppendLine("produtos.Referencia as RefItem,cores.Abreviatura as Cor,tamanhos.Abreviatura as Tamanho, SUM(nfeitens.quantidade) - SUM(nfeitens.Qtddevolvida) as  Quantidade,");
            SQL.AppendLine("ROUND(nfeitens.preco,2) as Preco,ROUND(SUM(nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz),2) as Descontos,");
            SQL.AppendLine("ROUND(IFNULL(IF(SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))<0,0.00,");
            SQL.AppendLine("SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + ");
            SQL.AppendLine("nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))),0),2) as Total, ");
            SQL.AppendLine(" tp.Descricao as TabelaPreco,  cat.descricao as Catalogo, gp.descricao as Grupo, ");
            SQL.AppendLine(" nfeitens.iditem AS idproduto, nfeitens.idcor, nfeitens.idtamanho, NFE.IdColaborador AS idcliente ");
            SQL.AppendLine("FROM nfeitens ");
            SQL.AppendLine("INNER JOIN nfe on nfe.id = nfeitens.IdNfe ");
            SQL.AppendLine("INNER JOIN produtos ON produtos.id =  nfeitens.iditem");
            SQL.AppendLine("INNER JOIN cores ON cores.id =  nfeitens.idcor");
            SQL.AppendLine("INNER JOIN tamanhos ON tamanhos.id =  nfeitens.idtamanho");
            SQL.AppendLine("INNER JOIN colaboradores ON colaboradores.id = NFE.IdColaborador ");
            SQL.AppendLine("LEFT JOIN municipiosibge on municipiosibge.id = colaboradores.idmunicipio ");
            SQL.AppendLine("LEFT JOIN rotavisitas on rotavisitas.id = colaboradores.idrota");
            SQL.AppendLine("LEFT JOIN colaboradores ven ON ven.id = nfe.idvendedor ");
            SQL.AppendLine("LEFT JOIN grupoprodutos gp ON gp.id = produtos.IdGrupo ");
            SQL.AppendLine("LEFT JOIN catalogo cat ON cat.id = produtos.IdCatalogo ");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp ON tp.id = nfe.idtabela ");
            SQL.Append("WHERE  ");
            if (Itens.Length > 0)
            {
                SQL.Append(" nfeitens.iditem IN(" + Itens + ") AND ");
            }
            SQL.AppendLine("   IFNULL(NFE.StatusNota,0) <> 2 AND nfe.Tipo = 0 AND"); //desconsidera canceladas
            SQL.Append(" SUBSTRING(NFE.datainclusao,1,10) BETWEEN " + Valor);
            if (UnirEmpresas == false)
            {
                SQL.Append(" AND NFE.idempresa = " + EmpresaLogada);
            }
            SQL.AppendLine(" GROUP BY nfeitens.IdItemNoPedido,nfeitens.iditem,nfeitens.idcor,nfeitens.idtamanho ");
            SQL.AppendLine(" order by NFE.IdColaborador,nfeitens.iditem,nfeitens.idcor,nfeitens.idtamanho ");

            SQL.AppendLine("  )  UNION ALL  (  ");

            SQL.AppendLine("select (SELECT id FROM itensliberacaopedidovenda WHERE itempedidovendaid = nfceitens.IdItemPedidoVenda LIMIT 1) as IdItemNoPedido, colaboradores.RazaoSocial as RazaoSocial,Municipiosibge.UF as 'Uf',ven.razaosocial as Vendedor,");
            SQL.AppendLine("produtos.Referencia as RefItem,cores.Abreviatura as Cor,tamanhos.Abreviatura as Tamanho, SUM(nfceitens.quantidade) - SUM(nfceitens.Qtddevolvida) as  Quantidade,");
            SQL.AppendLine("ROUND(nfceitens.preco,2) as Preco,(ROUND(SUM(nfceitens.descontorateado),2)  + ROUND(SUM(nfceitens.DescValor),2)) as Descontos, ");
            SQL.AppendLine("ROUND(IFNULL(IF(SUM((nfceitens.preco * (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.descontorateado + nfceitens.DescValor ))<0,0.00,");
            SQL.AppendLine("SUM((nfceitens.preco * (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.descontorateado + nfceitens.DescValor ))),0),2) as Total, ");
            SQL.AppendLine(" tp.Descricao as TabelaPreco,  cat.descricao as Catalogo, gp.descricao as Grupo, ");
            SQL.AppendLine(" nfceitens.idproduto, nfceitens.idcor, nfceitens.idtamanho, nfce.idcliente ");
            SQL.AppendLine("FROM nfceitens ");
            SQL.AppendLine("INNER JOIN nfce on nfce.id = nfceitens.idnfce ");
            SQL.AppendLine("INNER JOIN produtos ON produtos.id =  nfceitens.idproduto");
            SQL.AppendLine("INNER JOIN cores ON cores.id =  nfceitens.idcor");
            SQL.AppendLine("INNER JOIN tamanhos ON tamanhos.id =  nfceitens.idtamanho");
            SQL.AppendLine("INNER JOIN colaboradores ON colaboradores.id = nfce.idcliente ");
            SQL.AppendLine("LEFT JOIN municipiosibge on municipiosibge.id = colaboradores.idmunicipio ");
            SQL.AppendLine("LEFT JOIN rotavisitas on rotavisitas.id = colaboradores.idrota");
            SQL.AppendLine("LEFT JOIN colaboradores ven ON ven.id = nfce.idvendedor ");
            SQL.AppendLine("LEFT JOIN grupoprodutos gp ON gp.id = produtos.IdGrupo ");
            SQL.AppendLine("LEFT JOIN catalogo cat ON cat.id = produtos.IdCatalogo ");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp ON tp.id = nfce.idtabelapreco ");
            SQL.Append("WHERE  ");
            if (Itens.Length > 0)
            {
                SQL.Append(" nfceitens.idproduto IN(" + Itens + ") AND ");
            }
            SQL.AppendLine("   IFNULL(nfce.statusnota,0) <> 2 AND ");
            SQL.AppendLine("   nfceitens.Devolucao = 0 AND "); // desconsidera devolvidos
            SQL.Append(" SUBSTRING(nfce.DataEmissao,1,10) BETWEEN " + Valor);
            if (UnirEmpresas == false)
            {
                SQL.Append(" AND nfce.idempresa = " + EmpresaLogada);
            }
            SQL.AppendLine(" GROUP BY nfceitens.IdItemPedidoVenda, nfceitens.idproduto,nfceitens.idcor,nfceitens.idtamanho ");
            SQL.AppendLine(" order by nfce.idcliente,nfceitens.idproduto,nfceitens.idcor,nfceitens.idtamanho ");

            SQL.AppendLine(" ) ) as t GROUP BY IdItemNoPedido, idproduto, idcor, idtamanho ");
            SQL.AppendLine(" order by idcliente, idproduto, idcor, idtamanho ");

            var cnItens = new DapperConnection<ItenPorClienteView>();

            var crt = new ItenPorClienteView();
            var dados = cnItens.ExecuteStringSqlToList(crt, SQL.ToString());



            if (dados != null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    if (item.RefItem != null)
                    {
                        if (item.IdItemNoPedido > 0)
                        {
                            string sql = "select itempedidovendaid from itensliberacaopedidovenda where Id = " + item.IdItemNoPedido;
                            DataTable libItens = VestilloConnection.ExecToDataTable(sql);
                            int IdLib = int.Parse(libItens.Rows[0][0].ToString());
                            string sql2 = "select pedidovenda.Referencia as RefPedido from itenspedidovenda INNER JOIN pedidovenda on pedidovenda.Id = itenspedidovenda.PedidoVendaId where itenspedidovenda.id =" + IdLib;
                            DataTable pedItens = VestilloConnection.ExecToDataTable(sql2);
                            item.Pedido = pedItens.Rows[0][0].ToString();
                        }

                    }

                }

            }
            return dados;


        }


    }
}
