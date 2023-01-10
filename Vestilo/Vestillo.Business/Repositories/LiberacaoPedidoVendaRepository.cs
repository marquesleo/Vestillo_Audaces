using Vestillo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class LiberacaoPedidoVendaRepository : GenericRepository<LiberacaoPedidoVenda>
    {
        public LiberacaoPedidoVendaRepository()
            : base(new DapperConnection<LiberacaoPedidoVenda>())
        {
        }

        public IEnumerable<LiberacaoPedidoVendaView> GetLiberacaoPedidoVenda(int pedidoVendaId)
        {
            var cn = new DapperConnection<LiberacaoPedidoVendaView>();

            var SQL = new Select()
                 .Campos("itensliberacaopedidovenda.LiberacaoId, itensliberacaopedidovenda.Data as DataLiberacao, " +
                 " itensliberacaopedidovenda.Status, SUM(itensliberacaopedidovenda.Qtd) as Qtd, " +
                 " SUM(itensliberacaopedidovenda.Qtd) - SUM(itensliberacaopedidovenda.QtdFaturada) as QtdLiberada, " +
                 " SUM(itensliberacaopedidovenda.QtdFaturada) as QtdFaturada, SUM(itensliberacaopedidovenda.QtdConferencia) as QtdConferencia," +
                  "SUM(itensliberacaopedidovenda.QtdConferida) as QtdConferida")
                 .From("itenspedidovenda")
                 .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                 //.InnerJoin("liberacaopedidovenda", "itensliberacaopedidovenda.LiberacaoId = liberacaopedidovenda.LiberacaoId")
                 .Where(" itenspedidovenda.PedidoVendaId = " + pedidoVendaId)
                 .GroupBy(" LiberacaoId");

            return cn.ExecuteStringSqlToList(new LiberacaoPedidoVendaView(), SQL.ToString());
        }
    }
}
