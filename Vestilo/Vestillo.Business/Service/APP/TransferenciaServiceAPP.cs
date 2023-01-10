
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class TransferenciaServiceAPP : GenericServiceAPP<Transferencia, TransferenciaRepository, TransferenciaController>, ITransferenciaService
    {
        public TransferenciaServiceAPP() : base(new TransferenciaController())
        {

        }

        public IEnumerable<TransferenciaView> GetAllView()
        {
            return controller.GetAllView();
        }
    }
}



