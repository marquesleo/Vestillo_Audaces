
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class MapaRepository : GenericRepository<MapaView>
    {
        public IEnumerable<MapaView> ListRelatorioMapa(DateTime DaInspecao, DateTime AteInspecao, DateTime DoAgendamento, DateTime AteAgendamento, DateTime DaPrevisao, DateTime AtePrevisao, string NumeroOrdem, bool Agrupado = false, bool Agendamento = false, bool Inspecao = false, bool Previsao = false, bool SomenteAbertos = false, bool ExibirTempos = false)
        {

              var Valor1 = "'" + DaInspecao.ToString("yyyy-MM-dd") + "' AND '" + AteInspecao.ToString("yyyy-MM-dd") + "'";
              var Valor2 = "'" + DoAgendamento.ToString("yyyy-MM-dd") + "' AND '" + AteAgendamento.ToString("yyyy-MM-dd") + "'";
              var Valor3 = "'" + DaPrevisao.ToString("yyyy-MM-dd") + "' AND '" + AtePrevisao.ToString("yyyy-MM-dd") + "'";

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT pedidovenda.OrdemProducao as Op,pedidovenda.Familia as Colecao,produtos.Referencia as Referencia,cores.Abreviatura as Cor,tamanhos.Abreviatura as Tamanho, ");
            sql.AppendLine(" colaboradores.nome as Cliente,DataInspecao as Inspecao,DataAgendamento as Entrega,PrevisaoEntrega as Previsao,ROUND(itenspedidovenda.Qtd) as Quantidade,pedidovenda.Referencia as Pedido, ");
            sql.AppendLine("	pedidovenda.ObsAux as Obs,ROUND(itenspedidovenda.Qtd * itenspedidovenda.Preco, 2) as Valor ");
            if (ExibirTempos)
            {
                sql.AppendLine(" , fichatecnica.TempoTotal,ROUND(itenspedidovenda.Qtd * fichatecnica.TempoTotal,4) as TempoQuantidade   " );                
            }

            sql.AppendLine("FROM pedidovenda  ");
            sql.AppendLine("	INNER JOIN itenspedidovenda ON itenspedidovenda.PedidoVendaId = pedidovenda.Id ");
            sql.AppendLine("	INNER JOIN produtos ON produtos.Id =  itenspedidovenda.ProdutoId ");
            sql.AppendLine("	INNER JOIN cores ON cores.Id =  itenspedidovenda.CorId ");
            sql.AppendLine("	INNER JOIN tamanhos ON tamanhos.Id =  itenspedidovenda.TamanhoId ");
            sql.AppendLine("	INNER JOIN colaboradores ON colaboradores.id = pedidovenda.ClienteId ");
           
            if (ExibirTempos)
            {
                sql.AppendLine(" INNER JOIN fichatecnica ON fichatecnica.ProdutoId = itenspedidovenda.ProdutoId    ");
            }


            sql.AppendLine("WHERE " );
            sql.AppendLine("  (pedidovenda.EmpresaId IS NULL OR pedidovenda.EmpresaId = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ") AND ");
            if (Agendamento == false && Inspecao == false && Previsao == false)
            {
                sql.AppendLine(" SUBSTRING(IFNULL(pedidovenda.DataInspecao,''),1,10) BETWEEN " + Valor1);
                sql.AppendLine(" AND SUBSTRING(IFNULL(pedidovenda.DataAgendamento,''),1,10) BETWEEN " + Valor2);
                sql.AppendLine(" AND SUBSTRING(IFNULL(pedidovenda.PrevisaoEntrega,''),1,10) BETWEEN " + Valor3);
            }
            else if (Agendamento == true)
            {
                sql.AppendLine(" SUBSTRING(IFNULL(pedidovenda.DataAgendamento,''),1,10) BETWEEN " + Valor2);               
            }
            else if (Inspecao == true)
            {
                sql.AppendLine(" SUBSTRING(IFNULL(pedidovenda.DataInspecao,''),1,10) BETWEEN " + Valor1);               
            }
            else if (Previsao == true)
            {
                sql.AppendLine(" SUBSTRING(IFNULL(pedidovenda.PrevisaoEntrega,''),1,10) BETWEEN " + Valor3);
            }

            if (SomenteAbertos == true)
            {
                sql.AppendLine("   AND ( pedidovenda.status <> 3 AND  pedidovenda.status <>  4 ) ");                
            }

            
            if (Agrupado == false)
            {
                sql.AppendLine("  AND OrdemProducao = " + "'" + NumeroOrdem + "'");
                sql.AppendLine(" ORDER by OrdemProducao,pedidovenda.ClienteId,itenspedidovenda.ProdutoId,cores.Abreviatura,tamanhos.Id ");
            }
            else
            {                 
                 sql.AppendLine(" AND pedidovenda.OrdemProducao <>'' ");                 
                 sql.AppendLine(" ORDER by OrdemProducao,pedidovenda.id,pedidovenda.Familia,itenspedidovenda.ProdutoId,cores.Abreviatura ");
            }           
 
            return VestilloConnection.ExecSQLToListWithNewConnection<MapaView>(sql.ToString());
        }
    }
}
