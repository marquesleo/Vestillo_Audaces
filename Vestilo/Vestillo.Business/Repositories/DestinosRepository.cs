
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
    public class DestinosRepository : GenericRepository<Destinos>
    {
        public DestinosRepository()
            : base(new DapperConnection<Destinos>())
        {
        }

        public IEnumerable<Destinos> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("Destinos ")
                .Where(" Ativo = " + AtivoInativo);

            var tm = new Destinos();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<Destinos> GetListPorReferencia(string Abreviatura)
        {
            Destinos m = new Destinos();

            return _cn.ExecuteToList(m, "Abreviatura like '%" + Abreviatura + "%' And ativo = 1");
        }

        public IEnumerable<Destinos> GetListPorDescricao(string desc)
        {
            Destinos m = new Destinos();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Destinos> GetListById(int id)
        {
            Destinos m = new Destinos();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1");
        }
    }
}
