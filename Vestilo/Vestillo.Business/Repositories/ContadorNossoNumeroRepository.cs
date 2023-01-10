


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ContadorNossoNumeroRepository : GenericRepository<ContadorNossoNumero>
    {
        public ContadorNossoNumeroRepository() : base(new DapperConnection<ContadorNossoNumero>())
        {
        }

        public string GetProximo(int IdBanco)
        {
            var c = new ContadorNossoNumero();
            _cn.ExecuteToModel("IdBanco = " + IdBanco , ref c);

            var contAtual = c.NumeracaoAtual + 1;
            var contador = new StringBuilder();
            string formato = new string('0', 5);


            contador.Append((formato + contAtual.ToString()).Substring(contAtual.ToString().Length));

            //contador.Append((contAtual.ToString()).Substring(contAtual.ToString().Length));

            c.NumeracaoAtual = contAtual;
            this.Save(ref c);

            return contador.ToString();
        }

        public ContadorNossoNumero GetByBanco(int IdBanco)
        {

            var c = new ContadorNossoNumero();
            _cn.ExecuteToModel("IdBanco = " + IdBanco , ref c);

            return c;
        }
    }
}