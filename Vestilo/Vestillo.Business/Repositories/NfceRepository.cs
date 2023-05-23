using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class NfceRepository: GenericRepository<Nfce>
    {
        string modificaWhere = "";

        public NfceRepository()
            : base(new DapperConnection<Nfce>())
        {
        }

        //para preencher o grid da tela de browse
        public IEnumerable<NfceView> GetCamposDeterminados(string parametrosDaBusca)
        {
            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = "  n.statusnota = 1 AND n.Recebidasefaz = 1 AND n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }
            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " n.TipoNFCe = 1 AND total > 0 AND ";
            }

            var SQL = new Select()
                .Campos("n.id, n.referencia, n.serie, n.numeronfce, n.idcliente, cli.referencia as refcliente, n.emitidacontingencia, IF(n.emitidacontingencia = 1, 'Contingência', IF(n.emitidacontingencia = 2, 'Normal', '')) as emcontingencia, n.Observacao, " +
                        " IF(n.nomecliente IS NULL, cli.nome, CONCAT(cli.nome, ' (', n.nomecliente, ' - ', n.cpfcnpj, ')')) AS nomecliente,  ven.nome as nomevendedor, tab.Descricao as TabPreco, n.IdEmpresa, n.totaldevolvido,n.DescontoGrid, " +
                        " ven.referencia as refvendedor, n.dataemissao, n.recebidasefaz, IF(n.recebidasefaz = 1, 'Sim','Não') as recebsefaz,valordesconto as Desconto,(SELECT (SUM(quantidade) -  SUM(Qtddevolvida)) from nfceitens where nfceitens.idnfce = n.id and nfceitens.Devolucao = 0 ) as TotalItens, " +
                        " n.totaloriginal, n.total, IF(IFNULL(n.statusnota,0) = 1, 'Gerada', (IF(IFNULL(n.statusnota,0) = 2 ,'Cancelada', '')))  AS Status, IF(n.TipoNFCe = 0, 'Venda', 'Pré-Venda') as DescTipoNFCe, (SELECT SUM(quantidade) from nfceitens where nfceitens.idnfce = n.id and nfceitens.Devolucao = 1 ) as TotalItensDevolvidos ")
                .From("nfce n")
                .LeftJoin("tabelaspreco tab", "tab.id = n.idtabelapreco")
                .LeftJoin("colaboradores cli", "cli.id = n.idcliente")
                .LeftJoin("colaboradores ven", "ven.id = n.idvendedor")
                .Where(modificaWhere + FiltroEmpresa("n.idEmpresa"));

            var cn = new DapperConnection<NfceView>();
            var nfce = new NfceView();
            return cn.ExecuteStringSqlToList(nfce, SQL.ToString());
        }


        public IEnumerable<NfceView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            var cn = new DapperConnection<NfceView>();
            var p = new NfceView();


            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = " n.statusnota = 1 AND n.Recebidasefaz = 1 AND n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }

            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " n.TipoNFCe = 1 AND total > 0 AND "; 
            }

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as Referencia,  n.serie as Serie,  n.numeronfce as NumeroNfce, cli.referencia as RefColaborador, cli.nome as NomeCliente, n.Observacao ")
                .From("nfce n")
                .LeftJoin("colaboradores cli", "cli.id = n.idcliente")
                .LeftJoin("colaboradores ven", "ven.id = n.idvendedor")                
                .Where(modificaWhere + "n.referencia like '%" + referencia + "%' And  " + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<NfceView> GetListPorObservacao(string observacao, string parametrosDaBusca)
        {
            var cn = new DapperConnection<NfceView>();
            var p = new NfceView();


            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = " n.statusnota = 1 AND n.Recebidasefaz = 1 AND n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }

            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " n.TipoNFCe = 1 AND total > 0 AND ";
            }

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as Referencia,  n.serie as Serie,  n.numeronfce as NumeroNfce, cli.referencia as RefColaborador, cli.nome as NomeCliente, n.Observacao ")
                .From("nfce n")
                .LeftJoin("colaboradores cli", "cli.id = n.idcliente")
                .LeftJoin("colaboradores ven", "ven.id = n.idvendedor")
                .Where(modificaWhere + "n.Observacao like '%" + observacao + "%' And  " + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<NfceView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            var cn = new DapperConnection<NfceView>();
            var p = new NfceView();


            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = " n.statusnota = 1 AND n.Recebidasefaz = 1 AND n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }

            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " n.TipoNFCe = 1 AND totaloriginal > 0 AND "; 
            }

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as referencia,  n.serie as Serie,  n.numeronfce as NumeroNfce, cli.referencia as RefColaborador, cli.nome as NomeCliente, n.Observacao  ")
                .From("nfce n")
                .InnerJoin("colaboradores cli", "cli.id = n.idcliente")
                .Where(modificaWhere + "n.numeronfce like '%" + Numero + "%' And  " + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public NfceView GetViewById(int id)
        {           

            var SQL = new Select()
                .Campos("n.id, n.referencia,  n.DataEmissao, n.idcliente, n.RecebidaSefaz, n.idvendedor, n.Observacao, n.IdEmpresa,n.DescontoGrid, " +
                        "cli.nome as NomeCliente, ven.nome as NomeVendedor,  IF(n.IdGuia IS NULL, '', guia.nome) as NomeGuia, " +
                        "n.totaloriginal,  (SELECT (SUM(quantidade) -  SUM(Qtddevolvida)) from nfceitens where nfceitens.idnfce = n.id and nfceitens.Devolucao = 0 ) as TotalItens,  " +
                        "(SELECT SUM(quantidade) from nfceitens where nfceitens.idnfce = n.id and nfceitens.Devolucao = 1 ) as TotalItensDevolvidos, n.Total  ")
                .From("nfce n")
                .LeftJoin("colaboradores cli", "cli.id = n.idcliente")
                .LeftJoin("colaboradores guia", "guia.id = n.IdGuia")
                .LeftJoin("colaboradores ven", "ven.id = n.idvendedor")
                .Where("n.id = " + id + " AND " + FiltroEmpresa("n.idEmpresa"));

            var cn = new DapperConnection<NfceView>();
            var nfce = new NfceView();
            cn.ExecuteToModel( ref nfce, SQL.ToString());

            return nfce;
        }

        public void AlterarStatusCredito(int idNfce)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE creditoscliente SET ");
            SQL.AppendLine("Status = 1,");
            SQL.AppendLine(" IdNfceQuitado = null,  ");
            SQL.AppendLine(" ObsQuitacao = '',  ");
            SQL.AppendLine(" dataquitacao = null  ");
            SQL.AppendLine(" WHERE IdNfceQuitado = ");
            SQL.Append(idNfce);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void AlterarDataFaturamento(int IdNfce)
        {
            DateTime DiaFaturamento = new DateTime();
            DiaFaturamento = DateTime.Now;
            string SQl = String.Empty;
            var Valor = "'" + DiaFaturamento.ToString("yyyy-MM-dd") + "'";

            SQl = "UPDATE nfce set nfce.datafaturamento = " +  Valor  + " WHERE nfce.id = " + IdNfce;
            _cn.ExecuteNonQuery(SQl.ToString());
        }

    }
}
