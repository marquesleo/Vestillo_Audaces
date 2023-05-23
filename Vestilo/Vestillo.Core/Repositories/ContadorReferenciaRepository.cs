using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public class ContadorReferenciaRepository : GenericRepository<ContadorReferencia>
    {

        public string GetNext(string nomeContadorReferencia)
        {

            string sql = "SELECT * FROM contadorescodigo WHERE Nome = '" + nomeContadorReferencia + "'";
            
            ContadorReferencia contador = VestilloConnection.ExecSQLToModel<ContadorReferencia>(sql);

            int contAtual = contador.ContadorAtual + 1;
            var referencia = new StringBuilder();
            string formato = new string('0', contador.CasasDecimais);

            referencia.Append(contador.Prefixo);
            referencia.Append((formato + contAtual.ToString()).Substring(contAtual.ToString().Length));

            contador.ContadorAtual = contAtual;
            this.Save(contador);

            return referencia.ToString();
        }
    }
}
