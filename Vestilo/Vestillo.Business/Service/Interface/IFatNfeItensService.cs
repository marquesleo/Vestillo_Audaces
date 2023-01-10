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
    public interface IFatNfeItensService : IService<FatNfeItens, FatNfeItensRepository, FatNfeItensController>
    {
        IEnumerable<FatNfeItens> GetListByNfe(int IdNfe);
        IEnumerable<FatNfeItens> GetListByNfeItensComplementar(int IdNfe);        
        IEnumerable<FatNfeItensView> GetListByNfeItensView(int IdNfe);
        IEnumerable<FatNfeItensView> GetListByNfeItensViewAgrupados(int IdNfe);
        
    }
}
