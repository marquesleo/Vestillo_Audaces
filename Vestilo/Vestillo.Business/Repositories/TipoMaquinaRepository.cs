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
    public class TipoMaquinaRepository:GenericRepository<TipoMaquina>
    {
        public TipoMaquinaRepository()
            : base(new DapperConnection<TipoMaquina>())
        {
        }

        public IEnumerable<TipoMaquina> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("tipomaquinas ")
                .Where(" Ativo = " + AtivoInativo);

            var tm = new TipoMaquina();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}
