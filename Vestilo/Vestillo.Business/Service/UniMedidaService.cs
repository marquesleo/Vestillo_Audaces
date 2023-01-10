using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;

namespace Vestillo.Business.Service
{
    public class UniMedidaService : GenericService<UniMedida, UniMedidaRepository, UniMedidaController>
    {
        public UniMedidaService()
        {
            base.RequestUri = "UniMedida";
        }
    }
}