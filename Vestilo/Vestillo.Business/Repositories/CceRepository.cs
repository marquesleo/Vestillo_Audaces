
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
    public class CceRepository: GenericRepository<Cce>
    {
        public CceRepository(): base(new DapperConnection<Cce>())
        {
        }

        public int MaiorSeqNota(int Nota)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT IFNULL(MAX(seq),0) as seq FROM cartacorrecao WHERE idNotaFiscal = ");
            SQL.Append(Nota);

            var c = new Cce();
            var dados = _cn.ExecuteStringSqlToList(c, SQL.ToString());

            int seqAtual = 0;

            var query = (from p in dados select p).ToList();

            if (c != null)
            {
                seqAtual = query[0].seq;
            }
            else
            {
                seqAtual = 0;
            }

            return seqAtual;
            
        }

        public IEnumerable<Cce> DadosDaUltimaCarta(int Nota, int seq)
        {
                       
            string Where = "idNotaFiscal = " + Nota + " AND seq = " + seq;

            var c = new Cce();
            return _cn.ExecuteToList(c, Where);

        }

    }
}
