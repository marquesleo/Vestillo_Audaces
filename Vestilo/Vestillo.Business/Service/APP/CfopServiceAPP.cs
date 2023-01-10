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
    public class CfopServiceAPP: GenericServiceAPP<Cfop, CfopRepository, CfopController>, ICfopService
    {
        public CfopServiceAPP()
            : base(new CfopController())
        {
        }

        public IEnumerable<Cfop> GetPorReferencia(string referencia, String TipoCfop)
        {
            CfopController controller = new CfopController();
            return controller.GetPorReferencia(referencia, TipoCfop);
        }

        public IEnumerable<Cfop> GetPorDescricao(string desc, String TipoCfop)
        {
            CfopController controller = new CfopController();
            return controller.GetPorDescricao(desc, TipoCfop);
        }

    }
}
