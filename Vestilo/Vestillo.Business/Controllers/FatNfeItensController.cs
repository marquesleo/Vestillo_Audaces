using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class FatNfeItensController : GenericController<FatNfeItens, FatNfeItensRepository>
    {
        public IEnumerable<FatNfeItens> GetListByNfe(int IdNFe)
        {
            using (FatNfeItensRepository repository = new FatNfeItensRepository())
            {
                return repository.GetListByNfeItens(IdNFe);
            }
        }

        public IEnumerable<FatNfeItens> GetListByNfeItensComplementar(int IdNFe)
        {
            using (FatNfeItensRepository repository = new FatNfeItensRepository())
            {
                return repository.GetListByNfeItensComplementar(IdNFe);
            }
        }


        

        public IEnumerable<FatNfeItensView > GetListByNfeViewItem(int IdNFe)
        {
            using (FatNfeItensRepository repository = new FatNfeItensRepository())
            {
                return repository.GetListByNfeItensView(IdNFe);
            }
        }

        public IEnumerable<FatNfeItensView> GetListByNfeItensViewAgrupados(int IdNFe)
        {
            using (FatNfeItensRepository repository = new FatNfeItensRepository())
            {
                return repository.GetListByNfeItensViewAgrupados(IdNFe);
            }
        }

        
    }
}
