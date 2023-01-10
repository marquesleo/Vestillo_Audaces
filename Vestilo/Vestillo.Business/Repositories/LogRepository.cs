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
    public class LogRepository : GenericRepository<Log>
    {
        public LogRepository()
            : base(new DapperConnection<Log>())
        {
        }

        public IEnumerable<LogView> GetCarregarAcoes(string Modulos,int operacao, DateTime DataInicio, DateTime DataFim)
        {

            var cn = new DapperConnection<LogView>();
            var p = new LogView();

            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *,usuarios.Nome as NomeUsuario, CONCAT(mid(log.Data,9,2), '/',mid(log.Data,6,2),'/',left(log.Data,4)) as DataAcao,CONCAT(mid(log.Data,12,2),mid(log.Data,14,3)) as Hora from log ");
            SQL.AppendLine("INNER JOIN usuarios on usuarios.id = log.UsuarioId ");
            SQL.AppendLine("WHERE SUBSTRING(log.Data,1,10)  BETWEEN " + Valor);
            if (operacao != 0)
            {
                SQL.AppendLine(" AND log.operacao = " + operacao);
            }
            SQL.AppendLine(" AND log.Modulo = " + "'" + Modulos + "'");
            SQL.AppendLine(" ORDER BY log.Data desc ");
           

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public void EliminaDiasLog(int QtdMaximaLogVestillo)
        {
           
            string sqlDiasLimpeza = String.Empty;
            string sqlParametro = String.Empty;

            var cn = new DapperConnection<Log>();
            var cn2 = new DapperConnection<Parametro>();

            try
            {

                sqlParametro = "SELECT Valor FROM parametros WHERE chave = 'DIA_LIMPEZA_LOG'";
                var par = new Parametro();
                cn2.ExecuteToModel(ref par, sqlParametro.ToString());

                String DataAgora = DateTime.Now.ToShortDateString(); 

                if (par != null)
                {
                    String DataBanco = par.Valor;

                    //verifico se a data da empresa é igual a data de hoje,se for não precisa executar novamente
                    if (DataAgora == DataBanco)
                    {
                        return;
                    }
                }



                DateTime hoje = DateTime.Now;

                DateTime dataBase = hoje.AddDays(- QtdMaximaLogVestillo);

                var dataFiltro = dataBase.ToString("yyyy-MM-dd");
           

                     
          
                sqlDiasLimpeza = "DELETE FROM  log  WHERE date(Data) <= " +  "'" + dataFiltro + "'";
                cn.ExecuteNonQuery(sqlDiasLimpeza);

           
                string DiaParametro = hoje.ToShortDateString();           

           
                sqlParametro = "UPDATE parametros SET Valor = " + "'" + DiaParametro + "'" + " WHERE chave = 'DIA_LIMPEZA_LOG' ";
                cn2.ExecuteNonQuery(sqlParametro);
            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }

        }

        public void InsereLogPlanilhaEstoque(string RefItem, string RefCor, string RefTamanho, string Almoxarifado,decimal SaldoInserido)
        {
            int IdEmpresa = VestilloSession.EmpresaLogada.Id;
            int IdUsuario = VestilloSession.UsuarioLogado.Id;
            DateTime Agora = DateTime.Now;

            var dataAgora = Agora.ToString("yyyy-MM-dd");

            string SQL = "insert into log(EmpresaId,UsuarioId,Data,Operacao,DescricaoOperacao,ObjetoId,Objeto,Modulo) values (" +
                 +IdEmpresa + "," + IdUsuario + "," + "'" + dataAgora + "'"  + ",1,'PLANILHA EXCEL',000," + "'" + RefItem + "-" + RefCor + "-" + RefTamanho + "-" +  Almoxarifado + SaldoInserido.ToString().Replace(",",".") + "'" + ",'ACERTA ESTOQUE POR PLANILHA'" + ")";
            _cn.ExecuteNonQuery(SQL);

        }

    }
}
