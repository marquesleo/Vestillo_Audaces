

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
    public class TransferenciaItensServiceAPP : GenericServiceAPP<TransferenciaItens, TransferenciaItensRepository, TransferenciaItensController>, ITransferenciaItensService
    {
        public TransferenciaItensServiceAPP() : base(new TransferenciaItensController())
        {
        }

        public IEnumerable<TransferenciaItens> GetListByItens(int IdNfe)
        {
            return controller.GetListByItens(IdNfe);
        }       


        public IEnumerable<TransferenciaItensView> GetListByItensView(int IdNfe)
        {
            return controller.GetListByItensView(IdNfe);
        }

    }
}
