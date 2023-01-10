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
    public class AdmCartaoRepository : GenericRepository<AdmCartao>
    {
        public AdmCartaoRepository(): base(new DapperConnection<AdmCartao>())
        {
        }

        public IEnumerable<AdmCartao> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("* ")
                .From("admcartao ")
                .Where(" Ativo = " + AtivoInativo);

            var adm = new AdmCartao();
            return _cn.ExecuteStringSqlToList(adm, SQL.ToString());
        }



        public IEnumerable<AdmCartao> GetPorReferencia(string referencia)
        {
            AdmCartao r = new AdmCartao();

            return _cn.ExecuteToList(r, "referencia like '%" + referencia + "%' And ativo = 1 ");
        }

        public IEnumerable<AdmCartao> GetPornome(string desc)
        {
            AdmCartao d = new AdmCartao();
            return _cn.ExecuteToList(d, "nome like '%" + desc + "%' And ativo = 1 ");
        }

        public IEnumerable<AdmCartao> GetByIdList(int id)
        {
            AdmCartao i = new AdmCartao();
            return _cn.ExecuteToList(i, "id =" + id);
        }

    }
}