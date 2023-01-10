
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
    public class DevolucaoItensServiceAPP: GenericServiceAPP<DevolucaoItens, DevolucaoItensRepository, DevolucaoItensController>, IDevolucaoItensService
    {
        public DevolucaoItensServiceAPP() : base(new DevolucaoItensController())
        {
        }

        public IEnumerable<DevolucaoItens> GetListByNfe(int IdNfe)
        {
            return controller.GetListByNfe(IdNfe);
        }

        public IEnumerable<DevolucaoItens> GetListByNfeItensComplementar(int IdNfe)
        {
            return controller.GetListByNfeItensComplementar(IdNfe);
        }


        public IEnumerable<DevolucaoItensView> GetListByNfeItensView(int IdNfe)
        {
            return controller.GetListByNfeViewItem(IdNfe);
        }


        public IEnumerable<DevolucaoItensView> GetListByNfeItensViewAgrupados(int IdNfe)
        {
            return controller.GetListByNfeItensViewAgrupados(IdNfe);
        }
        
    }
}
