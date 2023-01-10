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
    public class FornecedorRepository: GenericRepository<Fornecedor>
    {
        public FornecedorRepository()
            : base(new DapperConnection<Fornecedor>())
        {
        }

        public IEnumerable<Fornecedor> GetListPorDescricao(string razao)
        {
            var SQL = new Select()
                .Campos("Id,RazaoSocial ")
                .From("colaboradores ")
               .Where(" RazaoSocial = '" + razao + "'");

            var tm = new Fornecedor();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}
