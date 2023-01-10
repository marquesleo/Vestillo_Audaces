
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IDevolucaoItensService : IService<DevolucaoItens, DevolucaoItensRepository, DevolucaoItensController>
    {
        IEnumerable<DevolucaoItens> GetListByNfe(int IdNfe);
        IEnumerable<DevolucaoItens> GetListByNfeItensComplementar(int IdNfe);
        IEnumerable<DevolucaoItensView> GetListByNfeItensView(int IdNfe);
        IEnumerable<DevolucaoItensView> GetListByNfeItensViewAgrupados(int IdNfe);
        
    }
}
