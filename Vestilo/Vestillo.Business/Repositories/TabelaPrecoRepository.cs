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
    public class TabelaPrecoRepository: GenericRepository<TabelaPreco>
    {
        public TabelaPrecoRepository()
            : base(new DapperConnection<TabelaPreco>())
        {
        }

        public TabelaPreco GetByReferencia(string referencia)
        {
            var ret = new TabelaPreco();
            _cn.ExecuteToModel("referencia like '%" + referencia + "%'" , ref ret);            
            return ret;
        }

        public IEnumerable<TabelaPreco> GetListPorReferencia(string referencia)
        {
            TabelaPreco m = new TabelaPreco();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<TabelaPreco> GetListPorDescricao(string desc)
        {
            TabelaPreco m = new TabelaPreco();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<TabelaPreco> GetListPorTabelaBase(int tabelaPrecoBaseId)
        {
            TabelaPreco m = new TabelaPreco();
            return _cn.ExecuteToList(m, "tabelaprecobaseid = " + tabelaPrecoBaseId);
        }

        public IEnumerable<TabelaPrecoView> GetAllView()
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT TP.*,");
            SQL.AppendLine("T.Descricao AS TabelaPrecoBaseDescricao");
            SQL.AppendLine("FROM 	tabelaspreco TP");
            SQL.AppendLine("LEFT JOIN tabelaspreco T ON T.Id = TP.tabelaprecobaseid	");
            SQL.AppendLine("        WHERE " + FiltroEmpresa("", "TP"));


            var cn = new DapperConnection<TabelaPrecoView>();
            return cn.ExecuteStringSqlToList(new TabelaPrecoView(), SQL.ToString());
        }

    }
}
