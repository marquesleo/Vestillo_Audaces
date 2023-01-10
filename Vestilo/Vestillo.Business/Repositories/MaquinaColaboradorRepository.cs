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
    public class MaquinaColaboradorRepository: GenericRepository<MaquinaColaborador>
    {
        public MaquinaColaboradorRepository()
            : base(new DapperConnection<MaquinaColaborador>())
        {
        }

        public MaquinaColaboradorView GetByColaborador(int IdColaborador)
        {
            var ColaboradorGrade = new MaquinaColaboradorView();
            var cn = new DapperConnection<MaquinaColaboradorView>();
            cn.ExecuteToModel("IdColaborador = " + IdColaborador.ToString(), ref ColaboradorGrade);
            return ColaboradorGrade;
        }

        public IEnumerable<MaquinaColaboradorView> GetListByColaborador(int IdColaborador)
        {
            var SQL = new Select()
                .Campos("tipomaquinas.descricao, maquinacolaboradores.* ")
                .From("maquinacolaboradores")
                .InnerJoin("tipomaquinas", "tipomaquinas.id =  maquinacolaboradores.idtipomaquina")
                .Where("idcolaborador = " + IdColaborador );

            var cn = new DapperConnection<MaquinaColaboradorView>();
            var tm = new MaquinaColaboradorView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }
    }
}
