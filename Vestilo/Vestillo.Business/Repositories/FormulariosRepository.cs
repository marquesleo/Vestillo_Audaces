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
    public class FormulariosRepository : GenericRepository<Formularios>
    {
        public FormulariosRepository(): base(new DapperConnection<Formularios>())
        {
        }

        public IEnumerable<Formularios> GetBuscarPeloTipo(int Tipo)
        {
            //Tipo = 1 GEstão, Igual a 2 Produção
            var SQL = String.Empty;
            if (Tipo == 1 )
            {
                SQL = " select * from formularios where TipoSistema = 1 or TipoSistema = 3 order by NomeForm  ";
            }
            else
            {
                SQL = " select * from formularios where TipoSistema = 2 or TipoSistema = 3 order by NomeForm  ";
            }
            
            var fr = new Formularios();
            return _cn.ExecuteStringSqlToList(fr, SQL);
        }
    }
}
