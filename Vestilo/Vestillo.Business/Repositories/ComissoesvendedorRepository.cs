
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
    public class ComissoesvendedorRepository: GenericRepository<Comissoesvendedor>
    {
        public ComissoesvendedorRepository() : base(new DapperConnection<Comissoesvendedor>())
        {
        }

        //para preencher o grid da tela de browse
        public IEnumerable<ComissoesvendedorView> GetCamposBrowse()
        {

            var cn = new DapperConnection<ComissoesvendedorView>();
            var p = new ComissoesvendedorView();
                       

            var SQL = new Select()
                .Campos("comissoesvendedor.id as id,comissoesvendedor.idempresa,idcontasreceber,comissoesvendedor.parcela as parcela,idNotaFat,idcontaspagar,comissoesvendedor.idvendedor," +
                        "colaboradores.referencia as RefVendedor,colaboradores.razaosocial as NomeVendedor, idnotaconsumidor,status,comissoesvendedor.Ativo, " +
                        "comissoesvendedor.Referencia as RefComissao,percentual,basecalculo,valor,dataliberacao, comissoesvendedor.dataemissao,comissoesvendedor.Obs,ExibirTitulo, " +
                        "cli.Referencia As RefCliente, cli.razaosocial as NomeCliente, guia.Referencia as RefGuia, guia.razaosocial as NomeGuia, IFNULL(comissoesvendedor.idGuia, 0) as idGuia ")
                .From("comissoesvendedor")
                .LeftJoin("colaboradores", "colaboradores.id =  comissoesvendedor.idvendedor")
                .LeftJoin("Nfe ", "Nfe.id = comissoesvendedor.idNotaFat")
                .LeftJoin("nfce", "nfce.id = comissoesvendedor.idNotaConsumidor")
                .LeftJoin("colaboradores guia", "guia.id = comissoesvendedor.idGuia")
                .LeftJoin("colaboradores cli", "cli.id = (CASE WHEN Nfe.Id IS NOT NULL THEN Nfe.IdColaborador ELSE nfce.IdCliente END)")
                .Where("ExibirTitulo = 1 AND " + FiltroEmpresa("comissoesvendedor.idEmpresa"));

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }


        public IEnumerable<Comissoesvendedor> GetByParcelaCtr(int  idParcela)
        {
            var cms = new Comissoesvendedor();
            var SQL = new Select()
                .Campos("* ")
                .From("comissoesvendedor")
                .Where("idcontasreceber = " + idParcela);

            var Comissao = new Comissoesvendedor();
            return _cn.ExecuteStringSqlToList(cms, SQL.ToString());
            
        }

        public IEnumerable<Comissoesvendedor> GetByParcelaCtrDeletar(int idParcela)
        {
            var cn = new DapperConnection<Comissoesvendedor>();
            var p = new Comissoesvendedor();

            var SQL = new Select()
                .Campos("* ")
                .From("comissoesvendedor")
                .Where("idcontasreceber = " + idParcela);

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public void UpdateExibirComissao(int idComissao, int exibir)
        {
            DateTime Hoje = DateTime.Now;
            string HojeTratado = "";
            HojeTratado = "'" + Hoje.ToString("yyyy-MM-dd") + "'";

            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE comissoesvendedor SET ");
            SQL.AppendLine("ExibirTitulo = ");
            SQL.Append(exibir);
            SQL.AppendLine(" ,dataemissao =  " );
            SQL.Append(HojeTratado);
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(idComissao);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateAtivoInativo(int idComissao, int AtivoInativo,string obs)
        {
            
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE comissoesvendedor SET ");
            SQL.AppendLine("Ativo = ");
            SQL.Append(AtivoInativo);
            SQL.AppendLine(", Obs = ");
            SQL.AppendLine("'" + obs + "'");
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(idComissao);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public void UpdateContasPagarId(int idComissao, int? idContasPagar, DateTime Liberacao)
        {
            string Valor = "";
            if (idContasPagar != null)
            {
                Valor = "'" + Liberacao.ToString("yyyy-MM-dd") + "'";
            }
           

            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE comissoesvendedor SET ");
            if (idContasPagar != null)
            {
                SQL.AppendLine("idcontaspagar = ");
                SQL.Append(idContasPagar);
            }
            else
            {
                SQL.AppendLine("idcontaspagar = null");
            }
            if (idContasPagar != null)
            {
                SQL.AppendLine(", Status = 2");
            }
            else
            {
                SQL.AppendLine(", Status = 1");
            }
            if (idContasPagar != null)
            {
                SQL.AppendLine(", dataliberacao = ");
                SQL.Append(Valor);
            }
            else
            {
                SQL.AppendLine(", dataliberacao = null");
            }

            SQL.AppendLine(" WHERE id = ");
            SQL.Append(idComissao);
            _cn.ExecuteNonQuery(SQL.ToString());
        }


        public IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores, string Guias, DateTime DataInicio,DateTime DataFim)
        {

            var cn = new DapperConnection<ComissoesvendedorView>();
            var p = new ComissoesvendedorView();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            var FiltroColaborador = string.Empty;
            if (!string.IsNullOrEmpty(Vendedores))
            {
                FiltroColaborador = " ( comissoesvendedor.idvendedor IN (" + Vendedores + ") ";
                if (!string.IsNullOrEmpty(Guias))
                    FiltroColaborador = FiltroColaborador + " OR comissoesvendedor.idGuia IN(" + Guias + ") ";

                FiltroColaborador = FiltroColaborador + ") AND ";
            }
            else if (!string.IsNullOrEmpty(Guias))
                FiltroColaborador = " comissoesvendedor.idGuia IN(" + Guias + ") AND ";

            var SQL = new Select()
                .Campos("comissoesvendedor.id as id,comissoesvendedor.idempresa,idcontasreceber,comissoesvendedor.parcela as parcela,idNotaFat,idcontaspagar,comissoesvendedor.idvendedor as idvendedor," +
                        "colaboradores.referencia as RefVendedor,IF(comissoesvendedor.idvendedor > 0, colaboradores.razaosocial, guia.razaosocial) as NomeVendedor, idnotaconsumidor,status,comissoesvendedor.Ativo, " +
                        "comissoesvendedor.Referencia as RefComissao,percentual,basecalculo,valor,dataliberacao,dataemissao,comissoesvendedor.Obs,ExibirTitulo, IFNULL(comissoesvendedor.idGuia, 0) as idGuia")
                .From("comissoesvendedor")
                .LeftJoin(" colaboradores", "colaboradores.id =  comissoesvendedor.idvendedor")
                .LeftJoin(" colaboradores guia", "guia.id =  comissoesvendedor.idGuia")
                .Where("comissoesvendedor.Ativo = 1 AND comissoesvendedor.Status = 1 AND " + FiltroColaborador + " SUBSTRING(comissoesvendedor.dataemissao,1,10)  BETWEEN " + Valor + "AND ExibirTitulo = 1 AND  " + FiltroEmpresa("comissoesvendedor.idEmpresa"));

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar)
        {

            var cn = new DapperConnection<Comissoesvendedor>();
            var p = new Comissoesvendedor();

            
            var SQL = new Select()
                .Campos("id as id,idempresa,idcontasreceber,parcela as parcela,idNotaFat,idcontaspagar,idvendedor, idGuia, idnotaconsumidor,status,comissoesvendedor.Ativo, " +
                        "comissoesvendedor.Referencia as RefComissao,percentual,basecalculo,valor,dataliberacao,dataemissao,comissoesvendedor.Obs,ExibirTitulo ")
                .From("comissoesvendedor")
                .Where("idcontaspagar = " + idContasPagar + " AND " + FiltroEmpresa());

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, string Guias, DateTime DataInicio,DateTime DataFim)
        {

            var cn = new DapperConnection<ComissoesvendedorView>();
            var p = new ComissoesvendedorView();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            var FiltroColaborador = string.Empty;
            if(!string.IsNullOrEmpty(Vendedores))
            {
                FiltroColaborador = " ( comissoesvendedor.idvendedor IN (" + Vendedores + ") ";
                if (!string.IsNullOrEmpty(Guias))
                    FiltroColaborador = FiltroColaborador + " OR comissoesvendedor.idGuia IN(" + Guias + ") ";

                FiltroColaborador = FiltroColaborador + ") AND ";
            }
            else if ( !string.IsNullOrEmpty(Guias))
                FiltroColaborador = " comissoesvendedor.idGuia IN(" + Guias + ") AND ";


           var SQL = new Select()
                .Campos("comissoesvendedor.id as id,comissoesvendedor.idempresa,idcontasreceber,comissoesvendedor.parcela as parcela,idNotaFat,idcontaspagar,comissoesvendedor.idvendedor as idvendedor," +
                        "colaboradores.referencia as RefVendedor,colaboradores.razaosocial as NomeVendedor, idnotaconsumidor,status,comissoesvendedor.Ativo, " +
                        "comissoesvendedor.Referencia as RefComissao,percentual,basecalculo,valor,dataliberacao,comissoesvendedor.dataemissao,comissoesvendedor.Obs,ExibirTitulo, " +
                        "cli.Referencia As RefCliente, cli.razaosocial as NomeCliente, IFNULL(comissoesvendedor.idGuia, 0) AS idGuia, guia.Referencia as RefGuia, guia.razaosocial as NomeGuia, nfce.Observacao as ObsNFCe")
                .From("comissoesvendedor")
                .LeftJoin(" colaboradores", "colaboradores.id =  comissoesvendedor.idvendedor")
                .LeftJoin("Nfe ", "Nfe.id = comissoesvendedor.idNotaFat")
                .LeftJoin("nfce", "nfce.id = comissoesvendedor.idNotaConsumidor")
                .LeftJoin("colaboradores cli", "cli.id = (CASE WHEN Nfe.Id IS NOT NULL THEN Nfe.IdColaborador ELSE nfce.IdCliente END)")
                .LeftJoin("colaboradores guia", "guia.id = comissoesvendedor.idGuia")
                .Where("comissoesvendedor.Ativo = 1 AND  " + FiltroColaborador + " DATE(comissoesvendedor.dataemissao) BETWEEN " + Valor + "AND ExibirTitulo = 1 AND " + FiltroEmpresa("comissoesvendedor.idEmpresa"))
                  .OrderBy("colaboradores.razaosocial,comissoesvendedor.dataemissao");
            return cn.ExecuteStringSqlToList(p, SQL.ToString());

        }

        public void DeletePorNotaConsumidor(int idnotaconsumidor)
        {            
            string SQL = "DELETE FROM comissoesvendedor WHERE idNotaConsumidor = " + idnotaconsumidor.ToString();
            _cn.ExecuteNonQuery(SQL);            
        }

    }
}

