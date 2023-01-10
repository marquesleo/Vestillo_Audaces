
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
    public class AtividadeFaccaoRepository : GenericRepository<AtividadeFaccao>
    {
        public AtividadeFaccaoRepository() : base(new DapperConnection<AtividadeFaccao>())
        {
        }

        public IEnumerable<AtividadeFaccao> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("*")
                .From("AtividadeFaccao ");
            if (AtivoInativo > 0)
            {
                SQL.Where(" Ativo = " + AtivoInativo);
            }
            SQL.OrderBy("Descricao");
            var atf = new AtividadeFaccao();
            return _cn.ExecuteStringSqlToList(atf, SQL.ToString());
        }

        public IEnumerable<AtividadeFaccao> GetListPorReferencia(string referencia)
        {
            AtividadeFaccao m = new AtividadeFaccao();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1" );
        }

        public IEnumerable<AtividadeFaccao> GetListPorDescricao(string desc)
        {
            AtividadeFaccao m = new AtividadeFaccao();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1" );
        }

        public IEnumerable<AtividadeFaccao> GetListById(int id)
        {
            AtividadeFaccao m = new AtividadeFaccao();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1");
        }
    }
}
