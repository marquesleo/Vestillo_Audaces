
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
    public class NotaEntradaFaccaoRepository : GenericRepository<NotaEntradaFaccao>
    {
        string modificaWhere = "";

        public NotaEntradaFaccaoRepository() : base(new DapperConnection<NotaEntradaFaccao>())
        {
        }

        //para preencher o grid da tela de browse
        public IEnumerable<NotaEntradaFaccaoView> GetCamposEspecificos(string parametrosDaBusca)
        {

            var cn = new DapperConnection<NotaEntradaFaccaoView>();
            var p = new NotaEntradaFaccaoView();

            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = "n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }

            var SQL = new Select()
                .Campos("n.id,n.IdOrdemProducao, n.referencia,  n.serie as Serie,  n.numero , n.IdColaborador as idColaborador,forn.referencia as RefColaborador, forn.nome as NomeColaborador, n.idtransportadora,  tra.nome as nometransportadora," +
                " tra.referencia as reftransportadora,n.datainclusao, n.dataemissao, n.totalnota ")
                .From("NotaEntradaFaccao n")
                .LeftJoin("colaboradores forn", "forn.id = n.IdColaborador")
                .LeftJoin("colaboradores tra", "tra.id = n.idtransportadora")
                .Where(modificaWhere + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }


        public IEnumerable<NotaEntradaFaccaoView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            var cn = new DapperConnection<NotaEntradaFaccaoView>();
            var p = new NotaEntradaFaccaoView();


            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = "n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as Referencia,  n.serie as Serie,  n.numero as Numero, forn.referencia as RefColaborador, forn.nome as NomeColaborador ")
                .From("NotaEntradaFaccao n")
                .LeftJoin("colaboradores forn", "forn.id = n.IdColaborador")
                .LeftJoin("colaboradores tra", "tra.id = n.idtransportadora")
                .Where(modificaWhere + "n.referencia like '%" + referencia + "%' And  " + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<NotaEntradaFaccaoView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            var cn = new DapperConnection<NotaEntradaFaccaoView>();
            var p = new NotaEntradaFaccaoView();


            if (parametrosDaBusca == "SOMENTE PARA DEVOLVER")
            {
                modificaWhere = " n.TotalmenteDevolvida = 0 AND "; // 0 não foi totalmente devolvida, 1 foi totalmente devolvida
            }

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as referencia,  n.serie as Serie,  n.numero as numero, forn.referencia as RefColaborador, forn.nome as NomeColaborador ")
                .From("NotaEntradaFaccao n")
                .InnerJoin("colaboradores forn", "forn.id = n.IdColaborador")
                .Where(modificaWhere + "n.numero like '%" + Numero + "%' And  " + FiltroEmpresa("n.idEmpresa"));


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<NotaEntradaFaccaoView> GetListPorOrdem(int pedidoId)
        {
            var cn = new DapperConnection<NotaEntradaFaccaoView>();
            var p = new NotaEntradaFaccaoView();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT  n.id as id,n.referencia as referencia,  n.serie as Serie,  n.numero as numero, forn.referencia as RefColaborador, forn.nome as NomeColaborador ");
            SQL.AppendLine("FROM    NotaEntradaFaccao n");
            SQL.AppendLine("INNER JOIN colaboradores forn ON forn.id = n.IdColaborador");
            SQL.AppendLine("INNER JOIN notaentradafaccaoitens ni ON n.id = ni.IdNota");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.id = ni.IdItemNaOrdem");
            SQL.AppendLine("INNER JOIN ordemproducao op ON op.id = iop.OrdemProducaoId");
            SQL.AppendLine("WHERE  pc.Id = " + pedidoId);
            SQL.AppendLine("GROUP BY n.id");

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }


    }
}
