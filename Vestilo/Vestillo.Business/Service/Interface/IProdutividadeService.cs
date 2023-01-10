using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IProdutividadeService : IService<Produtividade, ProdutividadeRepository, ProdutividadeController>
    {
        Produtividade GetByFuncionarioIdEData(int funcId, DateTime data);
    }
}
