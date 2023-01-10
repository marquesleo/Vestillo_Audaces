
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
    public class DepartamentosRepository:GenericRepository<Departamentos>
    {
        public DepartamentosRepository() : base(new DapperConnection<Departamentos>())
        {
        }

        public IEnumerable<Departamentos> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("Departamentos ")
                .Where(" Ativo = " + AtivoInativo);

            var tm = new Departamentos();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}
