using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PedidoVendaConferenciaItemRepository : GenericRepository<PedidoVendaConferenciaItem>
    {
        public PedidoVendaConferenciaItemRepository()
            : base(new DapperConnection<PedidoVendaConferenciaItem>())
        {
        }

        public List<PedidoVendaConferenciaItem> GetListByPedidoVendaConferencia(int pedidoVendaConferenciaId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  *");
            sql.AppendLine("FROM    pedidovendaconferenciaitens");
            sql.AppendLine("WHERE   PedidoVendaConferenciaId = " + pedidoVendaConferenciaId.ToString());

            return _cn.ExecuteStringSqlToList(new PedidoVendaConferenciaItem(), sql.ToString()).ToList();
        }

        public IEnumerable<PedidoVendaConferenciaItemView> GetListByPedidoVendaConferenciaView(int pedidoVendaConferenciaId)
        {
            StringBuilder sql = new StringBuilder();
            
            sql.AppendLine(" SELECT  produtos.Referencia as ProdutoReferencia,produtos.Descricao as ProdutoDescricao, ");
            sql.AppendLine(" tamanhos.Descricao as TamanhoDescricao,cores.Descricao as CorDescricao, ");
            sql.AppendLine(" tamanhos.Abreviatura as TamanhoReferencia,cores.Abreviatura as CorReferencia, ");
            sql.AppendLine(" unidademedidas.id as UnidadeMedidaId, pedidovendaconferenciaitens.* ");
            sql.AppendLine(" FROM    pedidovendaconferenciaitens ");
            sql.AppendLine(" INNER JOIN produtos on produtos.Id = pedidovendaconferenciaitens.ProdutoId ");
            sql.AppendLine(" INNER JOIN cores on cores.Id = pedidovendaconferenciaitens.CorId ");
            sql.AppendLine(" INNER JOIN tamanhos on tamanhos.Id = pedidovendaconferenciaitens.TamanhoId ");
            sql.AppendLine(" INNER JOIN unidademedidas on unidademedidas.id = produtos.IdUniMedida ");
            sql.AppendLine("WHERE   PedidoVendaConferenciaId = " + pedidoVendaConferenciaId.ToString());


            DapperConnection<PedidoVendaConferenciaItemView> cnView = new DapperConnection<PedidoVendaConferenciaItemView>();
            return cnView.ExecuteStringSqlToList(new PedidoVendaConferenciaItemView(), sql.ToString());
        }

        public void DeleteByConferencia(int conferenciaId)
        {
            string SQL = String.Empty;

             SQL = "DELETE FROM pedidovendaconferenciaitens WHERE PedidoVendaConferenciaId = " + conferenciaId.ToString();
            _cn.ExecuteNonQuery(SQL);

             SQL = "DELETE FROM pedidovendaconferenciaitensdci WHERE PedidoVendaConferenciaId = " + conferenciaId.ToString();
            _cn.ExecuteNonQuery(SQL);
        }


        public void DeleteByConferenciaDci(int pedidoId)
        {
            string SQL = String.Empty;
                       
            SQL = "DELETE FROM pedidovendaconferenciaitensdci WHERE PedidoVendaId = " + pedidoId;

            var cn = new DapperConnection<PedidoVendaConferenciaitensDci>();
            cn.ExecuteNonQuery(SQL);
        }


        public void GravarItensConferenciaDci(PedidoVendaConferenciaitensDci dados)
        {
            string SQl = String.Empty;
            var cn = new DapperConnection<PedidoVendaConferenciaitensDci>();

            SQl = " insert into vestillo.pedidovendaconferenciaitensdci " +
                  " (PedidoVendaConferenciaId, PedidoVendaId, ProdutoId, " +
                  " CorId, TamanhoId, QtdConferida, Observacao ) " +
                  " VALUES ( " + dados.PedidoVendaConferenciaId + "," +
                    dados.PedidoVendaId + "," + dados.ProdutoId + "," +
                    dados.CorId + "," + dados.TamanhoId + "," + dados.QtdConferida.ToString().Replace(",", ".") + "," +
                    "'" + dados.Observacao + "'" + ")";

            cn.ExecuteNonQuery(SQl);
        }

        public IEnumerable<PedidoVendaConferenciaitensDciView> GetListByPedidoVendaConferenciaItensDciView(int pedidoVenda)
        {
            string SQL = String.Empty;

            SQL = " SELECT pedidovendaconferenciaitensDci.id,produtos.Referencia as ProdutoReferencia,produtos.Descricao as ProdutoDescricao, " +
            " tamanhos.Descricao as TamanhoDescricao,cores.Descricao as CorDescricao,  " +
            " tamanhos.Abreviatura as TamanhoReferencia,cores.Abreviatura as CorReferencia, " +
            " unidademedidas.id as UnidadeMedidaId, pedidovendaconferenciaitensDci.* " +
            " FROM    pedidovendaconferenciaitensDci " +
            " INNER JOIN produtos on produtos.Id = pedidovendaconferenciaitensDci.ProdutoId " +
            " INNER JOIN cores on cores.Id = pedidovendaconferenciaitensDci.CorId " +
            " INNER JOIN tamanhos on tamanhos.Id = pedidovendaconferenciaitensDci.TamanhoId " +
            " INNER JOIN unidademedidas on unidademedidas.id = produtos.IdUniMedida " +
            " WHERE PedidoVendaId = " + pedidoVenda + " AND VirouPedidoDci = 0 ";


            DapperConnection<PedidoVendaConferenciaitensDciView> cnView = new DapperConnection<PedidoVendaConferenciaitensDciView>();
            return cnView.ExecuteStringSqlToList(new PedidoVendaConferenciaitensDciView(), SQL);
        }
    }
}
