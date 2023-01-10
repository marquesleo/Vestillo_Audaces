
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class TransferenciaItensController : GenericController<TransferenciaItens, TransferenciaItensRepository>
    {
        public IEnumerable<TransferenciaItens> GetListByItens(int IdTransferencia)
        {
            using (TransferenciaItensRepository repository = new TransferenciaItensRepository())
            {
                return repository.GetListByItens(IdTransferencia);
            }
        }


        public IEnumerable<TransferenciaItensView> GetListByItensView(int IdTransferencia)
        {
            using (TransferenciaItensRepository repository = new TransferenciaItensRepository())
            {
                return repository.GetListByItensView(IdTransferencia);
            }
        }


    }
}
