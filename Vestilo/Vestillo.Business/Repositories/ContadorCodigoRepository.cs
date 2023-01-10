using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ContadorCodigoRepository: GenericRepository<ContadorCodigo>
    {
        public ContadorCodigoRepository()
            : base(new DapperConnection<ContadorCodigo>())
        {
        }

        public string GetProximo(string nomeContador)
        {
            var c = new ContadorCodigo();
            _cn.ExecuteToModel("Nome = '" + nomeContador + "'", ref c);

            var contAtual = c.ContadorAtual + 1;
            var contador = new StringBuilder();
            string formato = new string('0', c.CasasDecimais);

            contador.Append(c.Prefixo);
            contador.Append((formato + contAtual.ToString()).Substring(contAtual.ToString().Length));
                        

            c.ContadorAtual = contAtual;
            this.Save(ref c);

            return contador.ToString();
        }

        public ContadorCodigo GetByNome(string nome)
        {
           
            var c = new ContadorCodigo();
            _cn.ExecuteToModel("Nome = '" + nome + "'", ref c);

            return c;
        }
    }
}