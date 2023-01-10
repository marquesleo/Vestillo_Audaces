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
    public interface ICfopService : IService<Cfop, CfopRepository, CfopController>
    {
        IEnumerable<Cfop> GetPorReferencia(String referencia, String TipoCfop);

        IEnumerable<Cfop> GetPorDescricao(String desc, String TipoCfop);

    }
}
