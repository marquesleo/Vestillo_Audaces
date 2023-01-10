using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;

namespace Vestillo.Business.Service
{
    public interface IGruposProducaoService : IService<GruposProducao, GruposProducaoRepository, GruposProducaoController>
    {

    }
}
