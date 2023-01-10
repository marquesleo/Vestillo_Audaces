using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class NaturezaFinanceiraRepository:GenericRepository<NaturezaFinanceira>
    {
        public NaturezaFinanceiraRepository()
            : base(new DapperConnection<NaturezaFinanceira>())
        {
        
        
        }

        public override void Save(ref NaturezaFinanceira naturezFinanceira)
        {
            if (naturezFinanceira.PadraoParaVendas)
                _cn.ExecuteUpdate(new NaturezaFinanceira(), "UPDATE naturezasfinanceiras SET PadraoParaVendas = 0 WHERE (IdEmpresa = " + VestilloSession.EmpresaLogada.Id.ToString() + " OR IdEmpresa IS NULL)");

            base.Save(ref naturezFinanceira);
        }

        public IEnumerable<NaturezaFinanceira> GetPorReferencia(string referencia)
        {
            NaturezaFinanceira m = new NaturezaFinanceira();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%'");
        }

        public IEnumerable<NaturezaFinanceira> GetPorDescricao(string desc)
        {
            NaturezaFinanceira m = new NaturezaFinanceira();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%'");
        }

        public IEnumerable<NaturezaFinanceira> GetByIdList(int id)
        {
            NaturezaFinanceira m = new NaturezaFinanceira();
            return _cn.ExecuteToList(m, "id =" + id);
        }
    }
}
