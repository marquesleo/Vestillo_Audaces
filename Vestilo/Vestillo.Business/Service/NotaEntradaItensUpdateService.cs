

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
    public class NotaEntradaItensUpdateService : GenericService<NotaEntradaItensUpdate,NotaEntradaItensUpdateRepository, NotaEntradaItensUpdateController>
    {
        public NotaEntradaItensUpdateService()
        {
            base.RequestUri = "notaentradaitens";
        }
    }
}