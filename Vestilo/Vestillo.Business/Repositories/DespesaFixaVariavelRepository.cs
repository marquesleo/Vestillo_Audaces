using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Connection;
using System.Data;
using Vestillo.Business.Models;

namespace Vestillo.Business.Repositories
{
    public class DespesaFixaVariavelRepository : GenericRepository<DespesaFixaVariavel>
    {
        public DespesaFixaVariavelRepository()
            : base(new DapperConnection<DespesaFixaVariavel>())
        {

        }

        public override IEnumerable<DespesaFixaVariavel> GetAll()
        {
            return base.GetAll();
        }

        public int GetUltimoAno()
        {
            string sql = "SELECT IFNULL(MAX(ano), 0) AS Ano FROM DespesaFixaVariavel WHERE " + FiltroEmpresa();
            DataTable dt = _cn.ExecuteToDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["Ano"]);

            return DateTime.Now.Year;
        }

        public DespesaFixaVariavel GetByAno(int ano)
        {
            var result = new DespesaFixaVariavel();
            _cn.ExecuteToModel("Ano = " + ano.ToString() + " AND " + FiltroEmpresa(), ref result);
            return result;
        }

        public IEnumerable<DespesaFixaVariavel> GetDespesasByAutomatizarContasPagar()
        {
            string sql = "SELECT * FROM DespesaFixaVariavel WHERE " + FiltroEmpresa("despesafixavariavel.EmpresaId") + " AND AutomatizarContasPagar = 1";
            return _cn.ExecuteStringSqlToList(new DespesaFixaVariavel(), sql);
        }
    }
}
