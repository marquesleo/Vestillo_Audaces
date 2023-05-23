using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Lib;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class FatNfeRepository : GenericRepository<FatNfe>
    {
        string modificaWhere = "";

        public FatNfeRepository()
            : base(new DapperConnection<FatNfe>())
        {
        }

        //para preencher o grid da tela de browse
        public IEnumerable<FatNfeView> GetCamposEspecificos(string parametrosDaBusca)
        {

            var cn = new DapperConnection<FatNfeView>();
            var p = new FatNfeView();
           
            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = " n.NotaComplementar = 0 AND n.statusnota = 1 AND n.Recebidasefaz = 1 AND n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }

            var SQL = new Select()
                .Campos("n.id,n.IdPedido,n.idDocEntrada,n.NumPedido as NumPedido,n.idNfe,n.idnotaassinada as idnotaassinada, n.referencia,  n.serie as Serie,  n.numero , n.IdColaborador as idColaborador,n.refpedidocliente as refpedidocliente, cli.referencia as RefColaborador, cli.nome as NomeColaborador,cli.razaosocial as RazaoColaborador, IFNULL(Est.abreviatura,'') As UfColaborador, n.idvendedor,  ven.nome as nomevendedor, n.idtransportadora,  tra.nome as nometransportadora," +
                " IF(IFNULL(n.tipo,0) = 1 OR IFNULL(n.tipo,0) = 3 OR IFNULL(n.tipo,0) = 4 OR IFNULL(n.tipo,0) = 6,'Entrada','SAÍDA') AS DescricaoTipo ,IF(IFNULL(n.statusnota = 1,0),'Emitida',IF(IFNULL(n.statusnota = 2,0),'Cancelada','Sem emissão')) AS DescStatusNota, IF(IFNULL(n.Recebidasefaz,0) = 1, 'Recebida','Não Recebida') AS DescRecebidasefaz, IF(n.Denegada = 1, 'Sim','Não') as DescDenegada," +
                "ven.referencia as refvendedor,ven2.referencia  as refvendedor2,ven2.nome as nomevendedor2,tab.referencia as RefTabela, tab.Descricao As DescTabela, tra.referencia as reftransportadora,n.datainclusao, n.dataemissao, n.recebidasefaz, n.frete as frete,n.seguro as seguro, n.despesa as despesa, n.valdesconto as valdesconto,n.totalitens as totalitens, n.totalnota as Totalnota,(n.totalnota - n.frete) TotalSemFrete ,n.Observacao as Observacao,n.EmpresaTrocada,IF(IFNULL(n.EmpresaTrocada,0) > 0, 'Sim','Não') AS DescOutraEmpresa,n.NomeEmpresaTrocada ")
                .From("nfe n")
                .LeftJoin("colaboradores cli", "cli.id = n.IdColaborador")
                .LeftJoin("colaboradores ven", "ven.id = n.idvendedor")
                .LeftJoin("colaboradores ven2", "ven2.id = n.IdVendedor2")
                .LeftJoin("estados  Est", "Est.Id = cli.idestado ")
                .LeftJoin("tabelaspreco tab", "tab.id = n.idtabela")                
                .LeftJoin("colaboradores tra", "tra.id = n.idtransportadora")
                .Where(modificaWhere + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }


        public IEnumerable<FatNfeView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            var cn = new DapperConnection<FatNfeView>();
            var p = new FatNfeView();


            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = " n.NotaComplementar = 0 AND n.statusnota = 1 AND n.Recebidasefaz = 1 AND n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }
            else if (parametrosDaBusca == "EMITIDA E ACEITA PELO SEFAZ")
            {
                modificaWhere = " n.statusnota = 1 AND n.Recebidasefaz = 1 AND ";
            }
            else if (parametrosDaBusca == "TRANSFERENCIA")
            {
                modificaWhere = " n.tipo = 0 AND n.statusnota <> 2 AND n.Transferida = 0 AND ";
            }

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as Referencia,  n.serie as Serie,  n.numero as Numero, cli.referencia as RefColaborador, cli.nome as NomeColaborador, cli.razaosocial as RazaoColaborador ")
                .From("nfe n")
                .LeftJoin("colaboradores cli", "cli.id = n.IdColaborador")
                .LeftJoin("colaboradores ven", "ven.id = n.idvendedor")
                .LeftJoin("colaboradores tra", "tra.id = n.idtransportadora")
                .Where(modificaWhere + "n.referencia like '%" + referencia + "%' And  " + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<FatNfeView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            var cn = new DapperConnection<FatNfeView>();
            var p = new FatNfeView();

           
            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = " n.NotaComplementar = 0 AND n.statusnota = 1 AND n.Recebidasefaz = 1 AND n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }
            else if (parametrosDaBusca == "EMITIDA E ACEITA PELO SEFAZ")
            {
                modificaWhere = " n.statusnota = 1 AND n.Recebidasefaz = 1 AND ";
            }
            else if (parametrosDaBusca == "TRANSFERENCIA")
            {
                modificaWhere = " n.tipo = 0 AND n.statusnota <> 2 AND n.Transferida = 0 AND ";
            }

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as referencia,  n.serie as Serie,  n.numero as numero, cli.referencia as RefColaborador, cli.nome as NomeColaborador, cli.razaosocial as RazaoColaborador ")
                .From("nfe n")
                .InnerJoin("colaboradores cli", "cli.id = n.IdColaborador")
                .Where(modificaWhere + "n.numero like '%" + Numero + "%' And  " + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<FatNfeView> GetListagemNfeRelatorio( FiltroFatNfeListagem filtro)
        {
            var cn = new DapperConnection<FatNfeView>();
            var p = new FatNfeView();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT n.id, IF(IFNULL(n.tipo,0) = 1 OR IFNULL(n.tipo,0) = 3 OR IFNULL(n.tipo,0) = 4 OR IFNULL(n.tipo,0) = 6,'Entrada','SAÍDA') AS DescricaoTipo, n.referencia, (n.totalnota - n.frete) TotalSemFrete, ");
            SQL.AppendLine(" cli.nome as NomeColaborador, cli.razaosocial as RazaoColaborador, IFNULL(Est.abreviatura,'') As UfColaborador, ven.nome as nomevendedor, tab.Descricao As DescTabela, tra.nome as nometransportadora,");
            SQL.AppendLine(" n.serie as Serie, n.Numero, n.DataInclusao, n.DataEmissao, IF(IFNULL(n.statusnota = 1,0),'Emitida',IF(IFNULL(n.statusnota = 2,0),'Cancelada','Sem emissão')) AS DescStatusNota, IF(IFNULL(n.Recebidasefaz,0) = 1, 'Recebida','Não Recebida') AS DescRecebidasefaz, ");

            if (filtro.DescontaDevolvida)
                SQL.AppendLine(" n.totalnota - (sum(ni.Qtddevolvida * ni.preco)) as Totalnota, ");
            else
                SQL.AppendLine("n.totalnota as Totalnota, ");

            SQL.AppendLine(" n.valdesconto as ValDesconto, n.totalitens as totalitens, sum(ni.Qtddevolvida) AS QtddevolvidaTotal, IF(n.Denegada = 1, 'Sim','Não') as DescDenegada ");
            SQL.AppendLine("FROM nfe n");
            SQL.AppendLine(" LEFT JOIN nfeitens ni ON n.id = ni.IdNfe");
            SQL.AppendLine(" LEFT JOIN colaboradores cli ON cli.id = n.IdColaborador");
            SQL.AppendLine(" LEFT JOIN colaboradores ven ON ven.id = n.idvendedor");
            SQL.AppendLine(" LEFT JOIN colaboradores tra ON tra.id = n.idtransportadora ");
            SQL.AppendLine(" LEFT JOIN estados  Est ON Est.Id = cli.idestado");
            SQL.AppendLine(" LEFT JOIN tabelaspreco tab ON tab.id = n.idtabela");
            SQL.AppendLine("WHERE " + FiltroEmpresa("n.idEmpresa") );

            if (filtro.NotaEmitida)
                SQL.AppendLine(" AND n.statusnota = 1 AND n.Recebidasefaz = 1 ");

            if (filtro.Colaborador != null && filtro.Colaborador.Count() > 0)
                SQL.AppendLine(" AND n.IdColaborador in (" + string.Join(",", filtro.Colaborador.ToArray()) + ")");

            if (filtro.Vendedor != null && filtro.Vendedor.Count() > 0)
                SQL.AppendLine(" AND n.idvendedor in (" + string.Join(",", filtro.Vendedor.ToArray()) + ")");

            if (filtro.UF != null && filtro.UF.Count() > 0)
                SQL.AppendLine(" AND cli.idestado in (" + string.Join(",", filtro.UF.ToArray()) + ")");

            if (filtro.TipoMovimentacao != null && filtro.TipoMovimentacao.Count() > 0)
                SQL.AppendLine(" AND ni.IdTipoMov in (" + string.Join(",", filtro.TipoMovimentacao.ToArray()) + ")");

            if(filtro.TipoDocumento != -1)
                SQL.AppendLine(" AND n.Tipo = " + filtro.TipoDocumento );

            if (filtro.DaInclusao != null && filtro.AteInclusao != null)
            {
                SQL.AppendLine("AND n.DataInclusao BETWEEN ' " + filtro.DaInclusao.Date.ToString("yyyy-MM-dd HH:mm:ss") + " ' AND ' " + filtro.AteInclusao.Date.ToString("yyyy-MM-dd HH:mm:ss") + " ' ");
            }

            SQL.AppendLine("GROUP BY n.Id");

            switch (filtro.Ordernar)
            {
                case 0:
                    SQL.AppendLine("ORDER BY n.DataInclusao, n.numero");
                    break;
                case 1:
                    SQL.AppendLine("ORDER BY Totalnota, n.numero");                    
                    break;
                case 2:
                    SQL.AppendLine("ORDER BY n.totalitens, n.numero");
                    break;
                case 3:
                    SQL.AppendLine("ORDER BY cli.nome, n.numero");
                    break;
                case 4:
                    SQL.AppendLine("ORDER BY ven.nome, n.numero");
                    break;
                default:
                    SQL.AppendLine("ORDER BY n.DataInclusao, n.numero");
                    break;
            }

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<FechamentoDoDiaView> GetFechamentoDoDia(DateTime DataInicio, DateTime DataFim,int Tipo)
        {

            var cn = new DapperConnection<FechamentoDoDiaView>();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

           

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT nfe.id as IdNota, nfe.referencia as Referencia,nfe.numero as Numero, nfe.DataInclusao as Inclusao, nfe.DataEmissao as Emissao,IFNULL(nfe.StatusNota,0) as StatusNota, ");
            SQL.AppendLine("nfe.ValorCartaoDebito as ValorCartaoDebito,nfe.ValorCheque as ValorCheque,nfe.ValorDinheiro as ValorDinheiro, 0 as ValorNcc, 0 as ValorTroco, ");
            SQL.AppendLine("(nfe.totalnota )as total,nfe.ValorCartaoCredito as ValorCartaoCredito, 1 as TP, c.referencia as RefCliente,c.nome as NomeCliente, (nfe.valdesconto + nfe.TotalDescontoSuframa) as Desconto from nfe ");
            SQL.AppendLine(" INNER JOIN colaboradores c on c.id = nfe.IdColaborador  ");
            SQL.AppendLine(" WHERE SUBSTRING(nfe.DataInclusao,1,10) BETWEEN " + Valor);
            //0 exibe todas
            if (Tipo == 1) // só emitidas e aceitas pelo sefaz
            {
                SQL.AppendLine( " AND (IFNULL(nfe.StatusNota,0)  = 1 AND IFNULL(nfe.Recebidasefaz,0) = 1)  ");
            }
            else if (Tipo == 2) //só canceladas
            {
                SQL.AppendLine(" AND IFNULL(nfe.StatusNota,0)  = 2  ");
            }
            else if (Tipo == 3) // sem emissão
            {
                SQL.AppendLine(" AND IFNULL(nfe.StatusNota,0)  = 0  ");
            }
            else if (Tipo == 4) //emitidas e canceladas
            {
                SQL.AppendLine(" AND (IFNULL(nfe.StatusNota,0)  = 1 AND IFNULL(nfe.Recebidasefaz,0) = 1 OR IFNULL(nfe.StatusNota,0)  = 2) ");
            }
            else if (Tipo == 5) //emitidas e não aceitas
            {
                SQL.AppendLine(" AND (IFNULL(nfe.StatusNota,0)  = 1 AND IFNULL(nfe.Recebidasefaz,0) = 1 OR IFNULL(nfe.StatusNota,0)  = 0) ");
            }
            else if (Tipo == 6) //canceladas e sem emissão
            {
                SQL.AppendLine("   AND (IFNULL(nfe.StatusNota,0)  = 0 OR IFNULL(nfe.StatusNota,0)  = 2  )");
            }
            else if (Tipo == 7) //Emitidas e não aceitas
            {
                SQL.AppendLine("  AND (IFNULL(nfe.StatusNota,0)  = 1 AND IFNULL(nfe.Recebidasefaz,0) <> 1)   ");
            }

            SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString() );
            SQL.AppendLine("   UNION ");
            SQL.AppendLine(" SELECT nfce.id as IdNota, nfce.referencia as Referencia,nfce.numeronfce as Numero, nfce.dataemissao as Inclusao, nfce.datafinalizacao as Emissao,IFNULL(nfce.StatusNota,0) as StatusNota, ");
            SQL.AppendLine("nfce.ValorCartaoDebito as ValorCartaoDebito,nfce.ValorCheque as ValorCheque,nfce.ValorDinheiro as ValorDinheiro, nfce.valorncc as ValorNcc, nfce.troco as ValorTroco, ");
            SQL.AppendLine("(nfce.ValorCartaoDebito + nfce.ValorCartaoCredito + nfce.ValorCheque + nfce.ValorDinheiro + nfce.valorncc) as total,nfce.ValorCartaoCredito as ValorCartaoCredito, 2 as TP,c.referencia as RefCliente,c.nome as NomeCliente, (nfce.valordesconto + nfce.descontogrid) as Desconto from nfce ");
            SQL.AppendLine(" INNER JOIN colaboradores c on c.id = nfce.idcliente ");
            SQL.AppendLine(" WHERE SUBSTRING(nfce.dataemissao,1,10) BETWEEN " + Valor); 
            if (Tipo == 1)
            {
                SQL.AppendLine( " AND (IFNULL(nfce.StatusNota,0)  = 1 AND IFNULL(nfce.recebidasefaz,0) = 1) ");
            }
            else if (Tipo == 2)
            {
                SQL.AppendLine(" AND IFNULL(nfce.StatusNota,0)  = 2  ");
            }
            else if (Tipo == 3)
            {
                SQL.AppendLine(" AND IFNULL(nfce.StatusNota,0)  = 0  ");
            }
            else if (Tipo == 4)
            {
                SQL.AppendLine(" AND (IFNULL(nfce.StatusNota,0)  = 1 AND IFNULL(nfce.Recebidasefaz,0) = 1 OR IFNULL(nfce.StatusNota,0)  = 2) ");
            }
            else if (Tipo == 5)
            {
                SQL.AppendLine(" AND (IFNULL(nfce.StatusNota,0)  = 1 AND IFNULL(nfce.Recebidasefaz,0) = 1 OR IFNULL(nfce.StatusNota,0)  = 0) ");
            }
            else if (Tipo == 6)
            {
                SQL.AppendLine("   AND (IFNULL(nfce.StatusNota,0)  = 0 OR IFNULL(nfce.StatusNota,0)  = 2)  ");
            }
            else if (Tipo == 7) //Emitidas e não aceitas
            {
                SQL.AppendLine("  AND (IFNULL(nfce.StatusNota,0)  = 1 AND IFNULL(nfce.Recebidasefaz,0) <> 1)   ");
            }
            SQL.AppendLine(" AND nfce.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString() );
            SQL.AppendLine("    ORDER BY TP, Inclusao ");
            return cn.ExecuteStringSqlToList(new FechamentoDoDiaView(), SQL.ToString());

        }

        public IEnumerable<RepXVendaView> GetRepXVenda(string Ano, int Uf)
        {
            var SQL = new StringBuilder();
            var cn = new DapperConnection<RepXVendaView>();


            var DataInicioTotal = Ano + "-01-01";
            var DataFimTotal = int.Parse(Ano) + 1 + "-01-01";

            SQL.AppendLine("SELECT V.id as IdVendedor,V.referencia as RefVendedor,v.Nome as NomeVendedor");

            //loop para cada mês do ano
            for (int i = 1; i <= 12 ; i++ )            
            {
                var DataInicio = Ano + "-" + i.ToString("d2") +  "-01";
                string DataFim = "";
                if (i == 12)
                {
                    DataFim = int.Parse(Ano) + 1 + "-" + "01-01";

                }
                else
                {
                    DataFim = Ano + "-" + (i + 1).ToString("d2") + "-01";
                }


                SQL.AppendLine(",ROUND(SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + "'" + DataInicio + "'" + " AND SUBSTRING(NFE.DataInclusao,1,10) < " + "'" + DataFim + "'" + ",nfeitens.quantidade - nfeitens.Qtddevolvida,0))," + VestilloSession.QtdCasasQuantidade + ")" + " as QtdTotalPecasM" + i);
                SQL.AppendLine(",FORMAT(SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + "'" + DataInicio + "'" + " AND SUBSTRING(NFE.DataInclusao,1,10) < " + "'" + DataFim + "'" + ",(nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ),0)),2,'de_DE') as ValorTotalPecasM" + i);
                SQL.AppendLine(",FORMAT(SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + "'" + DataInicio + "'" + " AND SUBSTRING(NFE.DataInclusao,1,10) < " + "'" + DataFim + "'" + ",(nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ),0)) / SUM(IF(SUBSTRING(NFE.DataInclusao,1,10) >=" + "'" + DataInicio + "'" + " AND SUBSTRING(NFE.DataInclusao,1,10) <" + "'" + DataFim + "'" + ",nfeitens.quantidade - nfeitens.Qtddevolvida,0)),2,'de_DE') as PrecoMedioTotalPecasM" + i);
            }

            SQL.AppendLine(" from nfeitens");
            SQL.AppendLine(" INNER JOIN NFE ON NFE.Id = nfeitens.IdNfe");
            SQL.AppendLine(" LEFT OUTER JOIN colaboradores V on V.id = NFE.idvendedor");
            SQL.AppendLine(" INNER JOIN colaboradores C on C.id = NFE.IdColaborador");
            SQL.AppendLine(" INNER JOIN estados E on E.id = C.idestado");
            SQL.AppendLine(" WHERE NFE.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            SQL.AppendLine(" AND IFNULL(NFE.statusnota = 1,0) AND NFE.Tipo = 0 AND NFE.recebidasefaz = 1 AND NFE.StatusNota = 1 ");
            if (Uf > 0)
            {
                SQL.AppendLine(" AND  E.id = " + Uf);
            }
            SQL.AppendLine(" GROUP BY v.id ");
            return cn.ExecuteStringSqlToList(new RepXVendaView(), SQL.ToString());
        }




        public IEnumerable<ListaFatVendaView> GetListaFatXVenda(DateTime DataInicio, DateTime DataFim, List<int> Vendedor, bool SomenteNFCe, bool SomenteTipoVendaNFCe,bool DataDatNfce)
        {
            //total ficava errado, fiz rotina a parte
            //SQL.AppendLine("(select COUNT(nfe.id) as QtdFaturamento from nfe WHERE  NFE.Tipo = 0  AND NFE.StatusNota <> 2 AND SUBSTRING(NFE.DataInclusao,1,10) BETWEEN " + Valor + " AND NFE.Idempresa = 1 and nfe.idvendedor IN(" + Vendedores + ")) as QtdTotalFaturamentos");

            var SQL = new StringBuilder();
            var cn = new DapperConnection<ListaFatVendaView>();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            string Vendedores = string.Empty;

            foreach (var item in Vendedor)
            {

                Vendedores += item + ",";
            }

            Vendedores = Vendedores.ToString().Substring(0, Vendedores.Length - 1);

            if(SomenteNFCe == false)
            {
                SQL.AppendLine("SELECT  Round(IFNULL(IF(SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))<0,0.00,SUM((nfeitens.preco * (nfeitens.quantidade - nfeitens.Qtddevolvida)) - (nfeitens.DescontoRateado + nfeitens.DescontoBonificado + nfeitens.DescontoCofins + nfeitens.DescontoPis + nfeitens.DescontoSefaz ))),0),2) as ValorTotal, ");
                SQL.AppendLine("ROUND(SUM(nfeitens.quantidade - nfeitens.Qtddevolvida),0) QtdTotalPecas, colaboradores.nome as NomeVendedor,colaboradores.id as IdVendedor,colaboradores.referencia as RefVendedor ");
                SQL.AppendLine("FROM nfeitens");
                SQL.AppendLine("INNER JOIN produtos ON produtos.id = nfeitens.iditem");
                SQL.AppendLine("INNER JOIN nfe on nfe.id = nfeitens.IdNfe");
                SQL.AppendLine("INNER JOIN colaboradores ON colaboradores.id = NFE.idvendedor");
                SQL.AppendLine("WHERE    NFE.Tipo = 0 AND   NFE.StatusNota <> 2 AND ");
                SQL.AppendLine(" SUBSTRING(NFE.DataInclusao,1,10) BETWEEN " + Valor);
                SQL.AppendLine(" AND NFE.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
                SQL.AppendLine(" AND NFE.idvendedor IN (" + Vendedores + ")");
                SQL.AppendLine(" GROUP BY NFE.idvendedor order by colaboradores.nome");
            }
            else
            {
                SQL.AppendLine("SELECT  Round(IFNULL(SUM((nfceitens.preco * (nfceitens.quantidade - nfceitens.Qtddevolvida)) - (nfceitens.DescontoRateado + nfceitens.DescValor)),0),2) as ValorTotal, ");
                SQL.AppendLine("ROUND(SUM(nfceitens.quantidade - nfceitens.Qtddevolvida), 0) QtdTotalPecas, colaboradores.nome as NomeVendedor,colaboradores.id as IdVendedor,colaboradores.referencia as RefVendedor ");
                SQL.AppendLine("FROM nfceitens ");
                SQL.AppendLine("INNER JOIN produtos ON produtos.id = nfceitens.idproduto");
                SQL.AppendLine("INNER JOIN nfce on nfce.id = nfceitens.idnfce");
                SQL.AppendLine("INNER JOIN colaboradores ON colaboradores.id = nfce.idvendedor");
                SQL.AppendLine("WHERE nfce.StatusNota <> 2 AND ");
                if(SomenteTipoVendaNFCe)
                {
                    SQL.AppendLine(" nfce.TipoNFCe = 0 AND ");
                }
                SQL.AppendLine("nfceitens.Devolucao = 0 AND ");
                if(DataDatNfce == false)
                {
                    SQL.AppendLine("SUBSTRING(nfce.dataemissao, 1, 10) BETWEEN " + Valor);
                }
                else
                {
                    SQL.AppendLine("SUBSTRING(nfce.datafaturamento, 1, 10) BETWEEN " + Valor);
                }
               
                SQL.AppendLine("AND nfce.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
                SQL.AppendLine("AND nfce.idvendedor IN (" + Vendedores + ")");
                SQL.AppendLine(" GROUP BY nfce.idvendedor order by colaboradores.nome");
            }
            
            return cn.ExecuteStringSqlToList(new ListaFatVendaView(), SQL.ToString());

            //Retirado a Pedido da Aline 03/06/2019
            //SQL.AppendLine("WHERE  IFNULL(NFE.statusnota = 1,0) AND  NFE.Tipo = 0 AND NFE.recebidasefaz = 1 AND NFE.StatusNota = 1 AND "); 
        }

        public int TotalFaturamentos(DateTime DataInicio, DateTime DataFim, int Vendedor, bool SomenteNFCe, bool DataDatNfce)
        {
            int Total = 0;

            var SQL = new StringBuilder();
            var cn = new DapperConnection<ListaFatVendaView>();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            if(SomenteNFCe == false)
            {
                SQL.AppendLine("select COUNT(*) as QtdTotalFaturamentos from nfe WHERE   NFE.Tipo = 0  AND NFE.StatusNota <> 2 AND SUBSTRING(NFE.DataInclusao,1,10) BETWEEN " + Valor + " AND NFE.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id + " and nfe.idvendedor IN(" + Vendedor + ") ");
            }
            else
            {
                if(DataDatNfce == false)
                {
                    SQL.AppendLine("select COUNT(*) as QtdTotalFaturamentos from nfce WHERE   nfce.StatusNota <> 2 AND SUBSTRING(nfce.dataemissao,1,10) BETWEEN " + Valor + " AND nfce.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id + " and nfce.idvendedor IN(" + Vendedor + ") ");
                }
                else
                {
                    SQL.AppendLine("select COUNT(*) as QtdTotalFaturamentos from nfce WHERE   nfce.StatusNota <> 2 AND SUBSTRING(nfce.datafaturamento,1,10) BETWEEN " + Valor + " AND nfce.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id + " and nfce.idvendedor IN(" + Vendedor + ") ");
                }
                
            }
            


            ListaFatVendaView ret = new ListaFatVendaView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                Total = ret.QtdTotalFaturamentos;
            }

            return Total;

        }


        public decimal TotalNcc(DateTime DataInicio, DateTime DataFim, int IdVendedor)
        {
            decimal Total = 0;
            string SQL2 = String.Empty;
            var SQL = new StringBuilder();
            var cn = new DapperConnection<ListaFatVendaView>();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            
            SQL.AppendLine("select nfce.id as IdNfce from nfce WHERE   nfce.StatusNota <> 2 AND SUBSTRING(nfce.dataemissao,1,10) BETWEEN " + Valor + " AND nfce.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id + " and nfce.idvendedor IN(" + IdVendedor + ") ");

            var crt = new ListaFatVendaView();
            var dados = cn.ExecuteStringSqlToList(crt, SQL.ToString());

            
            if (dados.Count() > 0 && dados != null)
            {
                foreach (var item in dados)
                {
                    SQL2 = "select IFNULL(SUM(contasreceber.ValorParcela),0) as ValorNcc from contasreceber where contasreceber.IdNotaconsumidor = " + item.IdNfce + " AND contasreceber.IdTipoDocumento = 4";
                    
                    ListaFatVendaView ret = new ListaFatVendaView();

                    cn.ExecuteToModel(ref ret, SQL2.ToString());

                    if (ret != null)
                    {
                        Total += ret.ValorNcc;
                    }                       
                }
            }

            return Total;

        }

        public IEnumerable<FechamentoDoDiaPagView> GetFechamentoDoDiaPorPagamento(DateTime DataInicio, DateTime DataFim)
        {

            var cn = new DapperConnection<FechamentoDoDiaPagView>();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";



            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT nfe.id as IdNfe,nfe.referencia as ReferenciaFat,nfe.numero as NumNota, nfe.DataInclusao as Inclusao, ");
            SQL.AppendLine("nfe.DataEmissao as Emissao,nfe.valdesconto as Desconto,nfe.frete as Frete,nfe.despesa as Despesas, ");
            SQL.AppendLine("nfe.totalnota as Total, ctr.id as IdTitulo,ctr.numtitulo as RefTitulo,ctr.parcela as ParcelaTitulo,ctr.dataVencimento as VencTitulo, ");
            SQL.AppendLine("ctr.ValorParcela as ValorParcela,IFNULL(ctr.SisCob,'') as SisCob, c.referencia as RefCliente,c.nome as NomeCliente,v.nome as NomeVendedor,ifnull(tp.Referencia,' - ') as TabPreco from nfe ");
            SQL.AppendLine("INNER JOIN colaboradores c on c.id = nfe.IdColaborador ");
            SQL.AppendLine("INNER JOIN contasreceber ctr on ctr.IdFatNfe = nfe.id  ");
            SQL.AppendLine("LEFT JOIN colaboradores v on v.Id = nfe.idvendedor  ");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.Id = nfe.idtabela  ");            
            SQL.AppendLine(" WHERE SUBSTRING(nfe.DataInclusao,1,10) BETWEEN " + Valor + " AND not isnull(ctr.IdCliente) AND nfe.tipo = 0 ");
            SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            SQL.AppendLine("   ORDER BY  nfe.id,ctr.parcela ");
            return cn.ExecuteStringSqlToList(new FechamentoDoDiaPagView(), SQL.ToString());

        }


        public FatNfeEtiquetaView EtiquetaEnderecamento (int Faturamento, int Tipo)
        {         

            var SQL = new StringBuilder();
            var cn = new DapperConnection<FatNfeEtiquetaView>();


            if (Tipo == 0)
            {
                SQL.AppendLine(" SELECT 'DESTINATARIO' as Linha1,cli.razaosocial as Linha2, CONCAT(cli.endereco,',',cli.numero,',',IFNULL(cli.complemento,'')) as Linha3, ");
                SQL.AppendLine(" CONCAT(cli.bairro, ' - ', mcli.municipio, ' - ',mcli.uf ) as Linha4, CONCAT('CEP:',IFNULL(cli.cep,'')) as Linha5, 'TRANSPORTADORA' as Linha6, ");
                SQL.AppendLine(" CONCAT('', tra.razaosocial) as Linha7, CONCAT('VOLUME:___ DE ',nfe.volumes) as Linha8, CONCAT('NOTA FISCAL: ', nfe.numero) as Linha9,'' as Linha10, '' as Linha11 from nfe  ");
                SQL.AppendLine(" INNER JOIN colaboradores cli ON cli.id = nfe.IdColaborador ");
                SQL.AppendLine(" INNER JOIN municipiosibge as mcli ON mcli.id = cli.idmunicipio ");
                SQL.AppendLine(" INNER JOIN colaboradores tra ON tra.id = nfe.idtransportadora ");
                SQL.AppendLine(" WHERE nfe.Id = " + Faturamento);
                SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            }
            else if(Tipo == 2)
            {
                SQL.AppendLine(" SELECT 'REMETENTE' as Linha1,emp.razaosocial as Linha2, CONCAT(enderecos.Logradouro,',',enderecos.Numero) as Linha3, ");
                SQL.AppendLine(" CONCAT(enderecos.bairro, ' - ', memp.municipio, ' - ',memp.uf )as Linha4, CONCAT('CEP:',IFNULL(enderecos.cep,'')) as Linha5, 'DESTINATÁRIO' as Linha6, ");
                SQL.AppendLine(" cli.razaosocial as Linha7, CONCAT(cli.endereco,',',cli.numero,',',IFNULL(cli.complemento,'')) as Linha8, CONCAT(cli.bairro, ' - ', mcli.municipio, ' - ',mcli.uf ) as Linha9, ");
                SQL.AppendLine(" CONCAT('CEP:',cli.cep) as Linha10, ''  as Linha11 from nfe  ");
                SQL.AppendLine(" INNER JOIN colaboradores cli ON cli.id = nfe.IdColaborador ");
                SQL.AppendLine(" INNER JOIN municipiosibge as mcli ON mcli.id = cli.idmunicipio ");
                SQL.AppendLine(" INNER JOIN empresas emp ON emp.id = nfe.Idempresa ");
                SQL.AppendLine(" INNER JOIN enderecos ON enderecos.EmpresaId = nfe.Idempresa ");
                SQL.AppendLine(" INNER JOIN municipiosibge as memp ON memp.id = enderecos.MunicipioId ");
                SQL.AppendLine(" WHERE nfe.Id = " + Faturamento);
                SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            }
            else
            {

                SQL.AppendLine(" SELECT 'REMETENTE' as Linha1,emp.razaosocial as Linha2, CONCAT(enderecos.Logradouro,',',enderecos.Numero) as Linha3, ");
                SQL.AppendLine(" CONCAT(enderecos.bairro, ' - ', memp.municipio, ' - ',memp.uf )as Linha4, CONCAT('CEP:',IFNULL(enderecos.cep,'')) as Linha5, 'DESTINATÁRIO' as Linha6, ");
                SQL.AppendLine(" cli.razaosocial as Linha7, CONCAT(cli.endereco,',',cli.numero,',',IFNULL(cli.complemento,'')) as Linha8, CONCAT(cli.bairro, ' - ', mcli.municipio, ' - ',mcli.uf ) as Linha9, ");
                SQL.AppendLine(" CONCAT('CEP:',cli.cep) as Linha10, CONCAT('TRANSPORTADO POR: EMP. BRASILEIRA DE CORREIOS E TELEG.: ',tra.nome)  as Linha11 from nfe  ");         
                SQL.AppendLine(" INNER JOIN colaboradores cli ON cli.id = nfe.IdColaborador ");
                SQL.AppendLine(" INNER JOIN municipiosibge as mcli ON mcli.id = cli.idmunicipio ");
                SQL.AppendLine(" INNER JOIN empresas emp ON emp.id = nfe.Idempresa ");
                SQL.AppendLine(" INNER JOIN enderecos ON enderecos.EmpresaId = nfe.Idempresa ");
                SQL.AppendLine(" INNER JOIN municipiosibge as memp ON memp.id = enderecos.MunicipioId ");
                SQL.AppendLine(" INNER JOIN colaboradores tra ON tra.id = nfe.idtransportadora ");
                SQL.AppendLine(" WHERE nfe.Id = " + Faturamento);
                SQL.AppendLine(" AND nfe.Idempresa = " + Business.VestilloSession.EmpresaLogada.Id.ToString());
            }


            FatNfeEtiquetaView ret = new FatNfeEtiquetaView();

            cn.ExecuteToModel(ref ret, SQL.ToString());           

            return ret;

        }

        public void UpdateTransferida(int IdFatNfe)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE nfe SET ");
            SQL.AppendLine("Transferida = 1");            
            SQL.AppendLine(" WHERE Id = ");
            SQL.Append(IdFatNfe);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public FatNfe GetByNfce(int idNFCe)
        {
            var cn = new DapperConnection<FatNfe>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT * ");
            SQL.AppendLine(" FROM nfe");
            SQL.AppendLine(" WHERE idNfce = ");
            SQL.Append(idNFCe);

            FatNfe ret = new FatNfe();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            return ret;
        }

    }
}