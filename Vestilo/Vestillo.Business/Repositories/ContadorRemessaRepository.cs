
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ContadorRemessaRepository : GenericRepository<ContadorRemessa>
    {
        public ContadorRemessaRepository() : base(new DapperConnection<ContadorRemessa>())
        {
        }

        public int GetProximo(int IdBanco)
        {
            var c = new ContadorRemessa();
            _cn.ExecuteToModel("IdBanco = " + IdBanco, ref c);

            string UltimoArquivoGerado = c.UltimoArquivoGerado;
            var contAtual = c.NumeracaoAtual + 1;
            var contador = new StringBuilder();
            //string formato = new string('0', 8);

            //contador.Append((contAtual.ToString()).Substring(contAtual.ToString().Length));
            //contador.Append((formato + contAtual.ToString()).Substring(contAtual.ToString().Length));            

            c.NumeracaoAtual = contAtual;
            c.UltimoArquivoGerado = UltimoArquivoGerado;
            this.Save(ref c);

            return contAtual;
        }

        public ContadorRemessa GetByBanco(int IdBanco)
        {

            var c = new ContadorRemessa();
            _cn.ExecuteToModel("IdBanco = " + IdBanco, ref c);

            return c;
        }
        

        public string GetUltimoArquivoGerado(int IdBanco)
        {
            var c = new ContadorRemessa();
            _cn.ExecuteToModel("IdBanco = " + IdBanco, ref c);

            string UltimoArquivo = String.Empty;

            UltimoArquivo = c.UltimoArquivoGerado;            

            return UltimoArquivo;
        }

        public string GetPrefixo(int IdBanco)
        {
            var c = new ContadorRemessa();
            _cn.ExecuteToModel("IdBanco = " + IdBanco, ref c);

            string UltimoArquivo = String.Empty;
                        
            UltimoArquivo = c.Prefixo;

            return UltimoArquivo;
        }

        public void UpdateUltimoArquivoGerado(int IdBanco, string UltimoArquivoGerado)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE contadorremessa SET ");           
            SQL.AppendLine("  UltimoArquivoGerado = ");
            SQL.Append("'" + UltimoArquivoGerado + "'");
            SQL.AppendLine(" WHERE IdBanco = ");
            SQL.Append(IdBanco);

            _cn.ExecuteNonQuery(SQL.ToString());
        }



    }
}