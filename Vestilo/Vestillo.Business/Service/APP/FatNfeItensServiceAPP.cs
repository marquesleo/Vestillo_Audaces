using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class FatNfeItensServiceAPP: GenericServiceAPP<FatNfeItens, FatNfeItensRepository, FatNfeItensController>, IFatNfeItensService
    {
        public FatNfeItensServiceAPP() : base(new FatNfeItensController())
        {
        }

        public IEnumerable<FatNfeItens> GetListByNfe(int IdNfe)
        {
            return controller.GetListByNfe(IdNfe);
        }

        public IEnumerable<FatNfeItens> GetListByNfeItensComplementar(int IdNfe)
        {
            return controller.GetListByNfeItensComplementar(IdNfe);
        }
                

        public IEnumerable<FatNfeItensView> GetListByNfeItensView(int IdNfe)
        {
            return controller.GetListByNfeViewItem(IdNfe);
        }


        public IEnumerable<FatNfeItensView> GetListByNfeItensViewAgrupados(int IdNfe)
        {
            return controller.GetListByNfeItensViewAgrupados(IdNfe);
        }
        
    }
}
