
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class BoletosGeradosRepository : GenericRepository<BoletosGerados>
    {
        public BoletosGeradosRepository() : base(new DapperConnection<BoletosGerados>())
        {
        }

        public void DeleteBoletoPeloTitulo(int idTitulo)
        {
            var cnInstrucoes = new DapperConnection<InstrucoesDosBoletos>();
            var cnContasReceber = new DapperConnection<ContasReceber>();

            string  SQL = String.Empty;
            SQL = "SELECT *  FROM  boletosgerados WHERE boletosgerados.idTitulo = " + idTitulo;            

            var cn = new DapperConnection<BoletosGerados>();
            var rcf = new BoletosGerados();
            var dados = cn.ExecuteStringSqlToList(rcf, SQL);

            SQL = String.Empty;
            if (dados != null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    SQL = "DELETE FROM instrucoesdosboletos  WHERE instrucoesdosboletos.IdBoleto = " + item.Id;

                    cn.ExecuteNonQuery(SQL);
                }

                SQL = String.Empty;

                SQL = "DELETE FROM  boletosgerados WHERE boletosgerados.idTitulo = " + idTitulo;
                cn.ExecuteNonQuery(SQL.ToString());

                SQL = String.Empty;

                SQL = "UPDATE contasreceber SET  RemessaGerada = 0, PossuiBoleto = 0, BancoPortador = null WHERE Id = " + idTitulo;
                cn.ExecuteNonQuery(SQL.ToString());

            }
        }


        public BoletosGerados GetViewByIdTitulo(int idTitulo)
        {
            var cn = new DapperConnection<BoletosGerados>();

            string SQL = String.Empty;
            SQL = "SELECT *  FROM  boletosgerados WHERE boletosgerados.idTitulo = " + idTitulo;

            var bl = new BoletosGerados();

            cn.ExecuteToModel(ref bl, SQL);

            return bl;
        }

        public void DeleteInstrucaoRemessaGerada(int idTitulo)
        {
            var cnInstrucoes = new DapperConnection<InstrucoesDosBoletos>();
            var cnContasReceber = new DapperConnection<ContasReceber>();

            string SQL = String.Empty;
            SQL = "SELECT *  FROM  boletosgerados WHERE boletosgerados.idTitulo = " + idTitulo;

            var cn = new DapperConnection<BoletosGerados>();
            var rcf = new BoletosGerados();
            var dados = cn.ExecuteStringSqlToList(rcf, SQL);

            SQL = String.Empty;
            if (dados != null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    SQL = "DELETE FROM instrucoesdosboletos  WHERE instrucoesdosboletos.IdBoleto = " + item.Id;

                    cn.ExecuteNonQuery(SQL);
                }

               
                SQL = String.Empty;

                SQL = "UPDATE contasreceber SET  RemessaGerada = 0,PossuiBoleto = 0, BancoPortador = null WHERE Id = " + idTitulo;
                cn.ExecuteNonQuery(SQL.ToString());

            }
        }

        public void UpdateDvBoleto(int IdBoleto, string DvNossoNumero)
        {
            var cn = new DapperConnection<BoletosGerados>();
            string SQL = String.Empty;


            SQL = "UPDATE boletosgerados SET  DvNossoNumero = " + DvNossoNumero + "  WHERE Id = "  + IdBoleto;
            cn.ExecuteNonQuery(SQL.ToString());
        }

        public BoletosGerados GetViewByNossoNumero(string NossoNumero)
        {
            var cn = new DapperConnection<BoletosGerados>();

            string SQL = String.Empty;
            SQL = "SELECT *  FROM  boletosgerados WHERE boletosgerados.NossoNumero = " + "'" + NossoNumero + "'";

            var bl = new BoletosGerados();

            cn.ExecuteToModel(ref bl, SQL);

            return bl;
        }

        public void LiberaRemessa(DateTime Data, int IdBanco,int IdEmpresa)
        {
            var cn = new DapperConnection<InstrucoesDosBoletos>();
            string SQL = String.Empty;

            var Valor = "'" + Data.ToString("yyyy-MM-dd") + "'";

            SQL = "UPDATE instrucoesdosboletos set instrucoesdosboletos.RemessaGerada = 0 " +
                  "WHERE instrucoesdosboletos.IdEmpresa =" + IdEmpresa + " AND instrucoesdosboletos.IdBanco = " + IdBanco +   " AND SUBSTRING(instrucoesdosboletos.DataEmissao,1,10) = " + Valor;
            cn.ExecuteNonQuery(SQL.ToString());
        }

        public BoletosGerados GetViewByNumDocumento(string NumDocumento)
        {
            var cn = new DapperConnection<BoletosGerados>();

            string SQL = String.Empty;
            SQL = "SELECT *  FROM  boletosgerados WHERE boletosgerados.NumDocumento = " + "'" + NumDocumento + "'";

            var bl = new BoletosGerados();

            cn.ExecuteToModel(ref bl, SQL);

            return bl;
        }

    }
}
