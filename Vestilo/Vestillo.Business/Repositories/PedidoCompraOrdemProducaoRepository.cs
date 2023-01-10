using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PedidoCompraOrdemProducaoRepository: GenericRepository<PedidoCompraOrdemProducao>
    {
        public PedidoCompraOrdemProducaoRepository()
            : base(new DapperConnection<PedidoCompraOrdemProducao>())
        {
        }

        public IEnumerable<PedidoCompraOrdemProducao> GetByPedido(int pedidoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *");
            SQL.AppendLine("FROM 	pedidocompraordemproducao I");
            SQL.AppendLine("WHERE	PedidoCompraId = ");
            SQL.Append(pedidoId);

            var cn = new DapperConnection<PedidoCompraOrdemProducao>();
            return cn.ExecuteStringSqlToList(new PedidoCompraOrdemProducao(), SQL.ToString());
        }
    }
}
