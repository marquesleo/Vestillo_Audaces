using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class ProdutividadeController : GenericController<Produtividade, ProdutividadeRepository>
    {
        public Produtividade GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            using (var repository = new ProdutividadeRepository())
            {
                return repository.GetByFuncionarioIdEData(funcId, data);
            }
        }
    }
}
