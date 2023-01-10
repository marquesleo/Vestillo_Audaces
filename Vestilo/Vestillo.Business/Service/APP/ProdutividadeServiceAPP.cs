using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class ProdutividadeServiceAPP: GenericServiceAPP<Produtividade, ProdutividadeRepository, ProdutividadeController>, IProdutividadeService
    {
        public ProdutividadeServiceAPP()
            : base(new ProdutividadeController())
        {

        }

        public Produtividade GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            ProdutividadeController controller = new ProdutividadeController();
            return controller.GetByFuncionarioIdEData(funcId, data);
        }
    }
}
