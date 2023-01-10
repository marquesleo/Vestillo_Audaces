using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;
using System.Data;

namespace Vestillo.Business.Repositories
{
    public class ControlaEmailCobrancaRepository : GenericRepository<ControlaEmailCobranca>
    {
        public ControlaEmailCobrancaRepository() : base(new DapperConnection<ControlaEmailCobranca>())
        {
        }

       
        public IEnumerable<ControlaEmailCobrancaView> GetAllView()
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	* FROM controlaemailcobranca ");
           

            var cn = new DapperConnection<ControlaEmailCobrancaView>();
            return cn.ExecuteStringSqlToList(new ControlaEmailCobrancaView(), sql.ToString());
        }

        public IEnumerable<ControlaEmailCobrancaView> GetAllViewAtivos()
        {

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("SELECT 	* FROM controlaemailcobranca");           
            sql.AppendLine("WHERE Ativo = 1");            

            var cn = new DapperConnection<ControlaEmailCobrancaView>();
            return cn.ExecuteStringSqlToList(new ControlaEmailCobrancaView(), sql.ToString());
        }

        public bool EhUsuarioDoControleEmail(int IdUsuarioLogado, ref string DataExecucao, ref string Executou)
        {
            bool EhUsuario = false;
            String DataAgora = DateTime.Now.ToShortDateString(); //data = 01/12/2014
            string SQL = String.Empty;
            
            SQL = " SELECT * FROM UsuarioEmail";

            var dados = _cn.ExecuteToDataTable(SQL);
            if(dados != null)
            {
                int IdUsuario = int.Parse(dados.Rows[0]["IdUsuario"].ToString());
                DataExecucao = dados.Rows[0]["Dia"].ToString();
                Executou = dados.Rows[0]["Executou"].ToString();

                if (IdUsuario == IdUsuarioLogado)
                {
                    EhUsuario = true;
                }
                else
                {
                    EhUsuario = false;
                }

            }
            return EhUsuario;
        }

        public DataTable GetTitulosDoPeriodo(int Dia)
        {
            bool EhUsuario = false;
            String DataAgora = DateTime.Now.ToShortDateString(); //data = 01/12/2014
            string SQL = String.Empty;
            
            SQL = " SELECT contasreceber.id as IdTitulo,colaboradores.id,colaboradores.razaosocial,contasreceber.IdEmpresa as EmpresaId,IFNULL(colaboradores.EmailCobranca,'') as EmailCobranca,IFNULL(colaboradores.ContatoCobranca,'') as ContatoCobranca, " +
                  " IFNULL(UltimoEmail,'') as UltimoEmail,CONCAT(MID(dataVencimento,9,2),'/',MID(dataVencimento,6,2),'/',MID(dataVencimento,1,4)) dataVencimento, " +
                  " DATEDIFF(DataVencimento, now()) AS quantidade_dias, ROUND(Saldo, 2) as Saldo from contasreceber " +
                  " INNER JOIN colaboradores ON colaboradores.id = contasreceber.IdCliente " +
                  " WHERE ISNULL(DataPagamento) AND DATEDIFF(DataVencimento, now()) = " + Dia + " AND saldo > 0  order by DATEDIFF (DataVencimento,now()) ";
            return _cn.ExecuteToDataTable(SQL);
        }

        public void UpdateUsuarioEmail(string SimNao)
        {
            DateTime data = DateTime.Now;
            string SQL = String.Empty;
            
            SQL = "UPDATE usuarioemail SET Dia = " + "'" + data.ToString("dd/MM/yyyy") + "'" + ",Executou = " + "'" + SimNao + "'";
            _cn.ExecuteNonQuery(SQL);

        }

    }
}

