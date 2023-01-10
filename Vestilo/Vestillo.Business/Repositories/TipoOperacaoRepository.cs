
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
    public class TipoOperacaoRepository:GenericRepository<TipoOperacao>
    {
        public TipoOperacaoRepository() : base(new DapperConnection<TipoOperacao>())
        {
        }

        public IEnumerable<TipoOperacao> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("TipoOperacao ")
                .Where(" Ativo = " + AtivoInativo);

            var to = new TipoOperacao();
            return _cn.ExecuteStringSqlToList(to, SQL.ToString());
        }
    }
}
