

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class DevolucaoItensController : GenericController<DevolucaoItens, DevolucaoItensRepository>
    {
        public IEnumerable<DevolucaoItens> GetListByNfe(int IdNFe)
        {
            using (DevolucaoItensRepository repository = new DevolucaoItensRepository())
            {
                return repository.GetListByNfeItens(IdNFe);
            }
        }

        public IEnumerable<DevolucaoItens> GetListByNfeItensComplementar(int IdNFe)
        {
            using (DevolucaoItensRepository repository = new DevolucaoItensRepository())
            {
                return repository.GetListByNfeItensComplementar(IdNFe);
            }
        }




        public IEnumerable<DevolucaoItensView> GetListByNfeViewItem(int IdNFe)
        {
            using (DevolucaoItensRepository repository = new DevolucaoItensRepository())
            {
                return repository.GetListByNfeItensView(IdNFe);
            }
        }

        public IEnumerable<DevolucaoItensView> GetListByNfeItensViewAgrupados(int IdNFe)
        {
            using (DevolucaoItensRepository repository = new DevolucaoItensRepository())
            {
                return repository.GetListByNfeItensViewAgrupados(IdNFe);
            }
        }

        
    }
}
