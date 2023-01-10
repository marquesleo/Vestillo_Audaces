
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;
using Vestillo.Core.Models;
using Vestillo.Models;

namespace Vestillo.Business.Repositories
{
    public class TransferenciaRepository : GenericRepository<Transferencia>
    {
        public TransferenciaRepository() : base(new DapperConnection<Transferencia>())
        {
        }

        //para preencher o grid da tela de browse
        public IEnumerable<TransferenciaView> GetAllView()
        {
            var cn = new DapperConnection<TransferenciaView>();
            var p = new TransferenciaView();


            var SQL = new Select()
                .Campos("n.Id as Id, n.referencia, n.IdCliente as IdCliente,cli.razaosocial as NomeCliente,n.TipoTransferencia,if(n.TipoTransferencia = 0,'Nota de Venda','Somente Itens') as DescricaoTipo,  " +
                " Origem.Descricao As AlmoxarifadoOrigem,Destino.Descricao as AlmoxarifadoDestino, n.* ")
                .From("Transferencia n")
                .LeftJoin("colaboradores cli", "cli.id = n.IdCliente")
                .LeftJoin("almoxarifados Origem", "Origem.id = n.idAlmoxarifadoOrigem")
                .LeftJoin("almoxarifados Destino", "Destino.id = n.idAlmoxarifadoDestino")
                .Where(FiltroEmpresa("n.Idempresa"));



            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }


        public IEnumerable<TrasnferenciaRelatorioView> ListByTransferencias(DateTime DataInicio, DateTime DataFim,int Tipo, bool ExibeSOmenteItens)
        {
            StringBuilder sql = new StringBuilder();
            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            var cn = new DapperConnection<TrasnferenciaRelatorioView>();
            var p = new TrasnferenciaRelatorioView();


            sql.AppendLine(" select transferencia.id,transferencia.DataInclusao,transferencia.NotasTransferidas,transferencia.referencia,if(transferencia.TipoTransferencia = 0,'Nota de Venda','Somente Itens') as DescricaoTipo, ");
            sql.AppendLine(" Origem.Descricao As AlmoxarifadoOrigem,Destino.Descricao as AlmoxarifadoDestino,transferencia.Usuario,transferencia.totalitens,transferencia.Obs, ");
            sql.AppendLine(" produtos.Referencia as ReferenciaProduto, produtos.descricao as DescricaoProduto,cores.descricao as DescricaoCor, tamanhos.descricao as DescricaoTamanho, ");
            sql.AppendLine(" transferenciaitens.quantidade from transferencia ");
            sql.AppendLine(" INNER JOIN transferenciaitens ON transferenciaitens.Idtransferencia = transferencia.id ");
            sql.AppendLine(" INNER JOIN produtos ON produtos.id = transferenciaitens.iditem ");
            sql.AppendLine(" INNER JOIN cores ON cores.id = transferenciaitens.idcor ");
            sql.AppendLine(" INNER JOIN produtodetalhes ON produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = transferenciaitens.idtamanho AND produtodetalhes.idcor = transferenciaitens.idcor ");
            sql.AppendLine(" INNER JOIN tamanhos ON tamanhos.id = transferenciaitens.idtamanho ");
            sql.AppendLine(" LEFT JOIN almoxarifados Origem ON Origem.id = transferencia.idAlmoxarifadoOrigem ");
            sql.AppendLine(" LEFT JOIN almoxarifados Destino ON Destino.id = transferencia.idAlmoxarifadoDestino  ");
            sql.AppendLine(" WHERE  SUBSTRING(transferencia.DataInclusao, 1, 10) BETWEEN  " + Valor );
            if (Tipo == 2)
            {
                sql.AppendLine("  AND transferencia.TipoTransferencia = 0 ");
            }
            else if(Tipo == 3)
            {
                sql.AppendLine("  AND transferencia.TipoTransferencia = 1 ");
            }

            sql.AppendLine(" AND transferencia.Idempresa = " + VestilloSession.EmpresaLogada.Id);

            if (ExibeSOmenteItens == false)
            {
                sql.AppendLine("  Group by transferencia.id");
            }


            return cn.ExecuteStringSqlToList(p, sql.ToString());
        }

    }
}
