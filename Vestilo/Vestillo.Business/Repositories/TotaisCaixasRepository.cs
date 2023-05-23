
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
    public class TotaisCaixasRepository: GenericRepository<TotaisCaixas>
    {
        public TotaisCaixasRepository() : base(new DapperConnection<TotaisCaixas>())
        {
        }

        public IEnumerable<TotaisCaixas> GetPorNumCaixa(string numCaixa)
        {
            var SQL = new Select()
                .Campos(" id,referencia,descricao,dataultabertura ,dataultfechamento,Ativo,saldo  ")
                .From("totaiscaixa ")
                .InnerJoin("caixas", "caixas.id =  totaiscaixa.idcaixa")
                .Where("caixas.referencia =  " + "'" + numCaixa  + "'" + " AND " + FiltroEmpresa("totaiscaixa.idempresa"));
                
                

            var cx = new TotaisCaixas();
            return _cn.ExecuteStringSqlToList(cx, SQL.ToString());
        }

        public IEnumerable<TotaisCaixas> GetByIdList(int idCaixa)
        {
            TotaisCaixas cx = new TotaisCaixas();
            return _cn.ExecuteToList(cx, "idcaixa =" + idCaixa);
        }


        public IEnumerable<TotaisCaixasView> GetPorData(string numCaixa, DateTime dataInicial, DateTime dataFinal)
        {
            var SQL = new Select()
                .Campos(" caixas.referencia as ReferenciaCaixa,caixas.descricao as DescricaoCaixa,totaiscaixas.datamovimento as datamovimento, " +
                        " IFNULL(colaboradores.razaosocial,'-') as NomeColaborador,tipo, IF(tipo = 2,totaiscaixas.dinheiro * -1,totaiscaixas.dinheiro) as dinheiro, " +
                        " totaiscaixas.cheque,totaiscaixas.cartaocredito,totaiscaixas.cartaodebito,totaiscaixas.outros, totaiscaixas.PixDep, totaiscaixas.operadoracredito,totaiscaixas.operadoradebito, " +
                        " totaiscaixas.observacao  ")
                .From("totaiscaixas ")
                .InnerJoin("caixas", "caixas.id =  totaiscaixas.idcaixa")
                .LeftJoin("colaboradores", "colaboradores.id =  totaiscaixas.Idcolaborador")
                .Where("caixas.referencia =  " + "'" + numCaixa + "'" + " AND DATE_FORMAT(totaiscaixas.datamovimento, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("totaiscaixas.idempresa"))
                .OrderBy("totaiscaixas.datamovimento");

            var cn = new DapperConnection<TotaisCaixasView>();
            var cx = new TotaisCaixasView();
           
           return  cn.ExecuteStringSqlToList(cx, SQL.ToString());


        }

        public IEnumerable<TotaisCaixasView> GetPorDataFechamento(DateTime dataInicial, DateTime dataFinal,bool SomenteVendas)
        {
            string SQL = String.Empty;
            SQL = " SELECT caixas.referencia as ReferenciaCaixa,caixas.descricao as DescricaoCaixa,totaiscaixas.datamovimento as datamovimento, " +
                " IFNULL(colaboradores.razaosocial,'-') as NomeColaborador,tipo, IF(tipo = 2,totaiscaixas.dinheiro * -1,totaiscaixas.dinheiro) as dinheiro, " +
                " totaiscaixas.cheque,totaiscaixas.cartaocredito,totaiscaixas.cartaodebito,totaiscaixas.outros, totaiscaixas.PixDep, totaiscaixas.operadoracredito,totaiscaixas.operadoradebito, " +
                " totaiscaixas.observacao  " +
                " FROM totaiscaixas " +
                " INNER JOIN caixas ON caixas.id =  totaiscaixas.idcaixa" +
                " LEFT JOIN colaboradores ON colaboradores.id =  totaiscaixas.Idcolaborador" +
                " WHERE  DATE_FORMAT(totaiscaixas.datamovimento, '%Y-%m-%d') BETWEEN '" + dataInicial.ToString("yyyy-MM-dd") + "' AND '" + dataFinal.ToString("yyyy-MM-dd") + "' AND" + FiltroEmpresa("totaiscaixas.idempresa");

                if(SomenteVendas)
                {
                SQL += " AND (NOT ISNULL(idnfce) OR NOT ISNULL(idNfe)) ";

                }

                SQL += " Order by totaiscaixas.datamovimento ";
                

            var cn = new DapperConnection<TotaisCaixasView>();
            var cx = new TotaisCaixasView();

            return cn.ExecuteStringSqlToList(cx, SQL.ToString());


        }

        public IEnumerable<TotaisCaixas> GetByNfce(int idNfce)
        {
            TotaisCaixas cx = new TotaisCaixas();
            return _cn.ExecuteToList(cx, "idNfce =" + idNfce);
        }

        public IEnumerable<TotaisCaixas> GetByNfe(int idNfe)
        {
            TotaisCaixas cx = new TotaisCaixas();
            return _cn.ExecuteToList(cx, "idNfe =" + idNfe);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdCaixa"></param>
        /// <param name="Tipo">Tipo 1 crédito 2 Débito</param>
        /// <param name="Valor"></param>
        public void UpdateSaldo(int IdCaixa, int Tipo,decimal Valor) // Tipo 1 crédito 2 Débito JAMAICA
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE caixas SET ");
            SQL.AppendLine("saldo = ");
            if (Tipo == (int)enumTipoMovimentoCaixa.Credito)
            {
                SQL.Append(" saldo + " + Valor.ToString().Replace(",","."));
            }
            else
            {
                SQL.Append(" saldo - " + Valor.ToString().Replace(",", "."));
            }           
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(IdCaixa);
            _cn.ExecuteNonQuery(SQL.ToString());
        }
    }
}

