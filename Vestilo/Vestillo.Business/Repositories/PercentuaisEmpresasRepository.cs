
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
    public class PercentuaisEmpresasRepository : GenericRepository<PercentuaisEmpresas>
    {
        public PercentuaisEmpresasRepository() : base(new DapperConnection<PercentuaisEmpresas>())
        {
        }

        public PercentuaisEmpresasView GetEmpresaLogada(int Id)
        {
            var cn = new DapperConnection<PercentuaisEmpresasView>();
            
                       

            var SQL = new Select()
                .Campos("emp.id, emp.RazaoSocial as NomeEmpresa, pc.* ")
                .From(" percentuaisempresas pc ")
                .InnerJoin("empresas emp", "emp.id = pc.EmpresaId")
                .Where("pc.EmpresaId = " + Id);

            var emp = new PercentuaisEmpresasView();
            cn.ExecuteToModel(ref emp, SQL.ToString());
            return emp;
            
        }

        public PercentuaisEmpresas GetByEmpresaLogada(int Id)
        {
            var cn = new DapperConnection<PercentuaisEmpresas>();



            var SQL = new Select()
                .Campos("pc.* ")
                .From(" percentuaisempresas pc ")
                .Where("pc.EmpresaId = " + Id);

            var emp = new PercentuaisEmpresas();
            cn.ExecuteToModel(ref emp, SQL.ToString());
            return emp;

        }
    }
}

