using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PedidoVendaConferenciaRepository : GenericRepository<PedidoVendaConferencia>
    {
        public PedidoVendaConferenciaRepository()
            : base(new DapperConnection<PedidoVendaConferencia>())
        {

        }

        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferencia()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM");
            sql.AppendLine("(");
            sql.AppendLine("SELECT  0 AS Id");
            sql.AppendLine("		,P.id AS PedidoVendaId");
            sql.AppendLine("		,P.Referencia AS PedidoReferencia");
            sql.AppendLine("		,C.referencia AS ClienteReferencia");
            sql.AppendLine("		,C.razaosocial AS ClienteNome");
            sql.AppendLine("		,NULL AS DataConferencia");
            sql.AppendLine("		,DATE(IPL.Data) AS DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");
            sql.AppendLine("		,1 AS Conferir");
            sql.AppendLine("FROM Pedidovenda P");
            sql.AppendLine("	INNER JOIN Colaboradores 				C 	ON C.id = P.ClienteId");
            sql.AppendLine("	INNER JOIN ItensPedidoVenda 			IP 	ON IP.PedidoVendaId = P.Id");
            sql.AppendLine("	INNER JOIN ItensLiberacaoPedidoVenda	IPL	ON IPL.ItemPedidoVendaID = IP.Id");
            sql.AppendLine("	LEFT  JOIN PedidoVendaConferencia       PC  ON PC.PedidoVendaID = P.Id");
            sql.AppendLine("WHERE  	P.Status IN (1, 2, 6, 7, 8)");
            sql.AppendLine("		AND" + FiltroEmpresa("EmpresaId", "P"));
            sql.AppendLine("		AND P.Conferencia = 1  ");
            sql.AppendLine("		AND PC.Id IS NULL");
            sql.AppendLine("		AND IPL.SemEmpenho = 0");
            sql.AppendLine("GROUP BY");
            sql.AppendLine("		P.id");
            sql.AppendLine("		,P.Referencia");
            sql.AppendLine("		,C.referencia");
            sql.AppendLine("		,C.razaosocial");
            sql.AppendLine("		,DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");

            sql.AppendLine("UNION ALL");

            sql.AppendLine("SELECT  PC.Id");
            sql.AppendLine("		,P.id AS PedidoVendaId");
            sql.AppendLine("		,P.Referencia AS PedidoReferencia");
            sql.AppendLine("		,C.referencia AS ClienteReferencia");
            sql.AppendLine("		,C.razaosocial AS ClienteNome");
            sql.AppendLine("		,PC.DataConferencia");
            sql.AppendLine("		,DATE(IPL.Data) AS DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");
            sql.AppendLine("		,IFNULL(P.Conferencia, 0) AS Conferir");
            sql.AppendLine("FROM Pedidovenda P");
            sql.AppendLine("	INNER JOIN Colaboradores 				C 	ON C.id = P.ClienteId");
            sql.AppendLine("	INNER JOIN PedidoVendaConferencia       PC  ON PC.PedidoVendaID = P.Id");
            sql.AppendLine("	INNER JOIN ItensPedidoVenda 			IP 	ON IP.PedidoVendaId = P.Id");
            sql.AppendLine("	INNER JOIN ItensLiberacaoPedidoVenda	IPL	ON IPL.ItemPedidoVendaID = IP.Id");
            sql.AppendLine("WHERE" + FiltroEmpresa("EmpresaId", "P"));
            sql.AppendLine(" AND IPL.QtdConferida <= 0");
            sql.AppendLine(" AND IPL.QtdConferencia > 0");
            sql.AppendLine(" AND P.Conferencia = 1  ");
            sql.AppendLine(" AND IPL.SemEmpenho = 0 ");
            sql.AppendLine("GROUP BY");
            sql.AppendLine("		P.id");
            sql.AppendLine("		,P.Referencia");
            sql.AppendLine("		,C.referencia");
            sql.AppendLine("		,C.razaosocial");
            sql.AppendLine("		,DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");

            sql.AppendLine(") AS Result");
            sql.AppendLine("ORDER BY PedidoReferencia, DataLiberacao, DataConferencia");

            DapperConnection<PedidoVendaConferenciaView> cnView = new DapperConnection<PedidoVendaConferenciaView>();
            return cnView.ExecuteStringSqlToList(new PedidoVendaConferenciaView(), sql.ToString());
        }

        public IEnumerable<PedidoVendaConferenciaitensDciView> GetListParaConferenciaDci()
        {

            string SQL = String.Empty;
           

            SQL = " SELECT pedidovenda.Id as id, PedidoVendaConferenciaId, pedidovenda.Referencia as PedidoReferencia,colaboradores.referencia as ClienteReferencia, " +
                  " colaboradores.razaosocial as ClienteNome, " +
                  " pedidovenda.Obs as ObsPedido " +
                  "  from pedidovendaconferenciaitensdci " +
                  "  INNER JOIN pedidovenda on pedidovenda.Id = pedidovendaconferenciaitensdci.PedidoVendaId " +
                  "  INNER JOIN colaboradores on colaboradores.id = pedidovenda.ClienteId " +
                  "  WHERE VirouPedidoDci = 0 " +
                  "  group by pedidovenda.Id,PedidoVendaConferenciaId ";

            DapperConnection<PedidoVendaConferenciaitensDciView> cnView = new DapperConnection<PedidoVendaConferenciaitensDciView>();
            return cnView.ExecuteStringSqlToList(new PedidoVendaConferenciaitensDciView(), SQL);


        }

        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferenciaSemEmpenho()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM");
            sql.AppendLine("(");
            sql.AppendLine("SELECT  0 AS Id");
            sql.AppendLine("		,P.id AS PedidoVendaId");
            sql.AppendLine("		,P.Referencia AS PedidoReferencia");
            sql.AppendLine("		,C.referencia AS ClienteReferencia");
            sql.AppendLine("		,C.razaosocial AS ClienteNome");
            sql.AppendLine("		,NULL AS DataConferencia");
            sql.AppendLine("		,DATE(IPL.Data) AS DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");
            sql.AppendLine("		,1 AS Conferir");
            sql.AppendLine("FROM Pedidovenda P");
            sql.AppendLine("	INNER JOIN Colaboradores 				C 	ON C.id = P.ClienteId");
            sql.AppendLine("	INNER JOIN ItensPedidoVenda 			IP 	ON IP.PedidoVendaId = P.Id");
            sql.AppendLine("	INNER JOIN ItensLiberacaoPedidoVenda	IPL	ON IPL.ItemPedidoVendaID = IP.Id");
            sql.AppendLine("	LEFT  JOIN PedidoVendaConferencia       PC  ON PC.PedidoVendaID = P.Id");
            sql.AppendLine("WHERE  	P.Status IN (1, 2, 6, 7, 8)");
            sql.AppendLine("		AND" + FiltroEmpresa("EmpresaId", "P"));
            sql.AppendLine("		AND P.Conferencia = 1  ");
            sql.AppendLine("		AND PC.Id IS NULL");
            sql.AppendLine("		AND IPL.SemEmpenho = 1");
            sql.AppendLine("GROUP BY");
            sql.AppendLine("		P.id");
            sql.AppendLine("		,P.Referencia");
            sql.AppendLine("		,C.referencia");
            sql.AppendLine("		,C.razaosocial");
            sql.AppendLine("		,DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");

            sql.AppendLine("UNION ALL");

            sql.AppendLine("SELECT  PC.Id");
            sql.AppendLine("		,P.id AS PedidoVendaId");
            sql.AppendLine("		,P.Referencia AS PedidoReferencia");
            sql.AppendLine("		,C.referencia AS ClienteReferencia");
            sql.AppendLine("		,C.razaosocial AS ClienteNome");
            sql.AppendLine("		,PC.DataConferencia");
            sql.AppendLine("		,DATE(IPL.Data) AS DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");
            sql.AppendLine("		,IFNULL(P.Conferencia, 0) AS Conferir");
            sql.AppendLine("FROM Pedidovenda P");
            sql.AppendLine("	INNER JOIN Colaboradores 				C 	ON C.id = P.ClienteId");
            sql.AppendLine("	INNER JOIN PedidoVendaConferencia       PC  ON PC.PedidoVendaID = P.Id");
            sql.AppendLine("	INNER JOIN ItensPedidoVenda 			IP 	ON IP.PedidoVendaId = P.Id");
            sql.AppendLine("	INNER JOIN ItensLiberacaoPedidoVenda	IPL	ON IPL.ItemPedidoVendaID = IP.Id");
            sql.AppendLine("WHERE" + FiltroEmpresa("EmpresaId", "P"));
            sql.AppendLine(" AND IPL.SemEmpenho = 1 ");
            sql.AppendLine(" AND IPL.QtdConferencia <> IPL.QtdConferida  ");
            sql.AppendLine(" AND IPL.QtdConferencia > 0");
            sql.AppendLine(" AND P.Conferencia = 1  ");
            sql.AppendLine("GROUP BY");
            sql.AppendLine("		P.id");
            sql.AppendLine("		,P.Referencia");
            sql.AppendLine("		,C.referencia");
            sql.AppendLine("		,C.razaosocial");
            sql.AppendLine("		,DataLiberacao");
            sql.AppendLine("		,P.DataEmissao");

            sql.AppendLine(") AS Result");
            sql.AppendLine("ORDER BY PedidoReferencia, DataLiberacao, DataConferencia");

            DapperConnection<PedidoVendaConferenciaView> cnView = new DapperConnection<PedidoVendaConferenciaView>();
            return cnView.ExecuteStringSqlToList(new PedidoVendaConferenciaView(), sql.ToString());
        }

        public void ReferenciaUsada(List<int> listaIdsConferencias)
        {
            string SQL = String.Empty;

            foreach (var id in listaIdsConferencias)
            {
                SQL = "UPDATE pedidovendaconferenciaitensDci set VirouPedidoDci = 1 WHERE Id = " + id;

                DapperConnection<PedidoVendaConferencia> cn = new DapperConnection<PedidoVendaConferencia>();
                cn.ExecuteNonQuery(SQL);
            }            

        }

        public IEnumerable<PedidoVendaConferenciaView> GetByPedido(int pedidoId)
        {
            string SQL = String.Empty;

            SQL = "SELECT * FROM pedidovendaconferencia WHERE PedidoVendaID = " + pedidoId;

            DapperConnection<PedidoVendaConferenciaView> cnView = new DapperConnection<PedidoVendaConferenciaView>();
            return cnView.ExecuteStringSqlToList(new PedidoVendaConferenciaView(), SQL.ToString());
        }
    }
}
