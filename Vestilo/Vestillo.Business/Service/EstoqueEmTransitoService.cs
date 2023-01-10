
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
    public class EstoqueEmTransitoService : GenericService<EstoqueEmTransito, EstoqueEmTransitoRepository, EstoqueEmTransitoController>
    {
        public EstoqueEmTransitoService()
        {
            base.RequestUri = "EstoqueEmTransito";
        }
    }
}