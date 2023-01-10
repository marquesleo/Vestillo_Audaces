
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
    public class CreditosClientesRepository: GenericRepository<CreditosClientes>
    {
        public CreditosClientesRepository() : base(new DapperConnection<CreditosClientes>())
        {
        }

        public  IEnumerable<CreditosClientes> GetByContasReceberBaixa(int contasReceberBaixaId)
        {
            return _cn.ExecuteToList(new CreditosClientes(), "ContasReceberBaixaId = " + contasReceberBaixaId.ToString());
        }

        public IEnumerable<CreditosClientes> GetByContasReceberQueGerou(int contasReceberId)
        {
            return _cn.ExecuteToList(new CreditosClientes(), "ContasReceberQueGerouCreditoId = " + contasReceberId.ToString());
        }

        //para preencher o grid da tela de browse
        public IEnumerable<CreditosClientesView> GetCamposBrowse()
        {
            var cn = new DapperConnection<CreditosClientesView>();
            var p = new CreditosClientesView();

            var SQL = new Select()
                .Campos("creditoscliente.id as id,creditoscliente.idempresa,idNotaFat,creditoscliente.idcolaborador," +
                        "colaboradores.referencia as RefCliente,colaboradores.razaosocial as NomeCliente, idnotaconsumidor,status,creditoscliente.Ativo, " +
                        "valor,dataemissao,dataquitacao ")
                .From("creditoscliente")
                .InnerJoin(" colaboradores", "colaboradores.id =  creditoscliente.idcolaborador")
                .Where(FiltroEmpresa("creditoscliente.idempresa"));

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public CreditosClientesView GetViewById(int id)
        {
            var cn = new DapperConnection<CreditosClientesView>();
            var p = new CreditosClientesView();

            var SQL = new Select()
                .Campos("creditoscliente.id as id,creditoscliente.idempresa,idNotaFat,creditoscliente.idcolaborador," +
                        "colaboradores.referencia as RefCliente,colaboradores.razaosocial as NomeCliente, idnotaconsumidor,status,creditoscliente.Ativo, " +
                        "valor,dataemissao,dataquitacao ")
                .From("creditoscliente")
                .InnerJoin(" colaboradores", "colaboradores.id =  creditoscliente.idcolaborador")
                .Where("creditoscliente.Id = " + id.ToString());

            cn.ExecuteToModel(ref p, SQL.ToString());
            return p;
        }

        //para preencher o grid da tela de browse
        public IEnumerable<CreditosClientesView> GetFiltro(string cliente)
        {
            var cn = new DapperConnection<CreditosClientesView>();
            var p = new CreditosClientesView();

            var SQL = new Select()
                .Campos("creditoscliente.id as id,creditoscliente.idempresa,idNotaFat,creditoscliente.idcolaborador," +
                        "colaboradores.referencia as RefCliente,colaboradores.razaosocial as NomeCliente, idnotaconsumidor,status,creditoscliente.Ativo, " +
                        "valor,dataemissao,dataquitacao ")
                .From("creditoscliente")
                .InnerJoin(" colaboradores", "colaboradores.id =  creditoscliente.idcolaborador")
                .Where(FiltroEmpresa("creditoscliente.idEmpresa") + " AND (colaboradores.razaosocial LIKE '%" + cliente.Trim() + "%' OR colaboradores.Referencia LIKE '%" + cliente.Trim() + "%')");
            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<CreditosClientesView> GetByCreditoAbertos(int idCliente, int Ativo)
        {
            var cn = new DapperConnection<CreditosClientesView>();
            var p = new CreditosClientesView();

            var SQL = new Select()
                .Campos("creditoscliente.id, creditoscliente.dataemissao, creditoscliente.valor ")
                .From("creditoscliente")
                .InnerJoin(" colaboradores", "colaboradores.id =  creditoscliente.idcolaborador");
                
                if (Ativo == 0)
                {
                    SQL.Where("idcolaborador = " + idCliente + " AND ISNULL(.creditosclientedataquitacao) AND creditoscliente.ativo = 0");
                }
                else if (Ativo == 1)
                {
                    SQL.Where("idcolaborador = " + idCliente + " AND ISNULL(creditoscliente.dataquitacao) AND creditoscliente.ativo = 1");
                }
                else
                {
                    SQL.Where("idcolaborador = " + idCliente + " AND ISNULL(creditoscliente.dataquitacao)");
                }
                


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public void UpdateBaixaCredito(int idCredito, DateTime  quitacao,string ObsQuitacao)
        {
            string Valor = "";
            Valor = "'" + quitacao.ToString("yyyy-MM-dd") + "'";
        

            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE creditoscliente SET ");
            SQL.AppendLine(" dataquitacao = ");
            SQL.Append(Valor);
            SQL.AppendLine(", ObsQuitacao = " +  "'" + ObsQuitacao + "'");
            SQL.AppendLine(", status = 2");
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(idCredito);

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


        public IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores,DateTime DataInicio,DateTime DataFim)
        {

            var cn = new DapperConnection<ComissoesvendedorView>();
            var p = new ComissoesvendedorView();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";


            var SQL = new Select()
                .Campos("comissoesvendedor.id as id,comissoesvendedor.idempresa,idcontasreceber,comissoesvendedor.parcela as parcela,idNotaFat,idcontaspagar,comissoesvendedor.idvendedor as idvendedor," +
                        "colaboradores.referencia as RefVendedor,colaboradores.razaosocial as NomeVendedor, idnotaconsumidor,status,comissoesvendedor.Ativo, " +
                        "comissoesvendedor.Referencia as RefComissao,percentual,basecalculo,valor,dataliberacao,dataemissao,comissoesvendedor.Obs,ExibirTitulo ")
                .From("comissoesvendedor")
                .InnerJoin(" colaboradores", "colaboradores.id =  comissoesvendedor.idvendedor")
                .Where("comissoesvendedor.Status = 1 AND comissoesvendedor.idvendedor IN (" + Vendedores + ") AND " + " dataemissao BETWEEN " + Valor + "AND ExibirTitulo = 1 AND " + FiltroEmpresa("comissoesvendedor.idEmpresa"));

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar)
        {

            var cn = new DapperConnection<Comissoesvendedor>();
            var p = new Comissoesvendedor();

            
            var SQL = new Select()
                .Campos("id as id,idempresa,idcontasreceber,parcela as parcela,idNotaFat,idcontaspagar,idvendedor idnotaconsumidor,status,comissoesvendedor.Ativo, " +
                        "comissoesvendedor.Referencia as RefComissao,percentual,basecalculo,valor,dataliberacao,dataemissao,comissoesvendedor.Obs,ExibirTitulo ")
                .From("comissoesvendedor")
                .Where("idcontaspagar = " + idContasPagar + " AND " + FiltroEmpresa("comissoesvendedor.idEmpresa"));

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, DateTime DataInicio,DateTime DataFim)
        {

            var cn = new DapperConnection<ComissoesvendedorView>();
            var p = new ComissoesvendedorView();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";


            var SQL = new Select()
                .Campos("comissoesvendedor.id as id,comissoesvendedor.idempresa,idcontasreceber,comissoesvendedor.parcela as parcela,idNotaFat,idcontaspagar,comissoesvendedor.idvendedor as idvendedor," +
                        "colaboradores.referencia as RefVendedor,colaboradores.razaosocial as NomeVendedor, idnotaconsumidor,status,comissoesvendedor.Ativo, " +
                        "comissoesvendedor.Referencia as RefComissao,percentual,basecalculo,valor,dataliberacao,dataemissao,comissoesvendedor.Obs,ExibirTitulo ")
                .From("comissoesvendedor")
                .InnerJoin(" colaboradores", "colaboradores.id =  comissoesvendedor.idvendedor")
                .Where("comissoesvendedor.idvendedor IN (" + Vendedores + ") AND " + " dataemissao BETWEEN " + Valor + "AND ExibirTitulo = 1 AND " + FiltroEmpresa("comissoesvendedor.idEmpresa"))
                  .OrderBy("colaboradores.razaosocial,comissoesvendedor.dataemissao");
            return cn.ExecuteStringSqlToList(p, SQL.ToString());

        }

        public CreditosClientes GetByCredito(int idDevolucaoItens)
        {
            var cn = new DapperConnection<CreditosClientes>();
            CreditosClientes cr = new CreditosClientes();
            cn.ExecuteToModel("idDevolucaoItens = " + idDevolucaoItens.ToString(), ref cr);

            return cr;
        }

        public CreditosClientes GetByNotaConsumidor(int idnotaconsumidor)
        {
            var cn = new DapperConnection<CreditosClientes>();
            CreditosClientes cr = new CreditosClientes();
            cn.ExecuteToModel("idnotaconsumidor = " + idnotaconsumidor.ToString(), ref cr);

            return cr;
        }

        public IEnumerable<CreditosClientes> GetByNotaConsumidorQuitado(int idnotaconsumidor)
        {
            var cn = new DapperConnection<CreditosClientes>();
            var cr = cn.ExecuteToList(new CreditosClientes(), "IdNfceQuitado = " + idnotaconsumidor.ToString());

            return cr;
        }

        public decimal GetByAteData(DateTime data)
        {
            var cn = new DapperConnection<CreditosClientes>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) AS Valor");
            SQL.AppendLine("FROM	creditoscliente AS C");
            SQL.AppendLine("WHERE DATE(C.DataEmissao) <= '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));

            var cr = new CreditosClientes();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.valor;
        }

        public decimal GetByData(DateTime data)
        {
            var cn = new DapperConnection<CreditosClientes>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT SUM(C.Valor) AS Valor");
            SQL.AppendLine("FROM	creditoscliente AS C");
            SQL.AppendLine("WHERE DATE(C.DataEmissao) = '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.Ativo = 1 ");
            SQL.AppendLine("        AND " + FiltroEmpresa("", "C"));

            var cr = new CreditosClientes();

            cn.ExecuteToModel(ref cr, SQL.ToString());

            return cr.valor;
        }

        public List<CreditosClientesView> GetByDataConsulta(DateTime data)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	C.*, CO.referencia as RefCliente,CO.razaosocial as NomeCliente");
            sql.AppendLine("FROM 	creditoscliente C ");
            sql.AppendLine("    INNER JOIN colaboradores CO ON CO.id = C.IdColaborador");
            sql.AppendLine("WHERE DATE(C.DataEmissao) = '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.Ativo = 1 ");
            sql.AppendLine("        AND " + FiltroEmpresa("", "C"));
            sql.AppendLine("ORDER BY C.DataEmissao");

            var cn = new DapperConnection<CreditosClientesView>();
            return cn.ExecuteStringSqlToList(new CreditosClientesView(), sql.ToString()).ToList();
        }

        public List<CreditosClientesView> GetByAteDataConsulta(DateTime data)
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	C.*, CO.referencia as RefCliente,CO.razaosocial as NomeCliente");
            sql.AppendLine("FROM 	creditoscliente C ");
            sql.AppendLine("    INNER JOIN colaboradores CO ON CO.id = C.IdColaborador");
            sql.AppendLine("WHERE DATE(C.DataEmissao) <= '" + data.ToString("yyyy-MM-dd") + "' AND C.Status = 1 AND C.Ativo = 1 ");
            sql.AppendLine("        AND " + FiltroEmpresa("", "C"));
            sql.AppendLine("ORDER BY C.DataEmissao");

            var cn = new DapperConnection<CreditosClientesView>();
            return cn.ExecuteStringSqlToList(new CreditosClientesView(), sql.ToString()).ToList();
        }
    }
}

