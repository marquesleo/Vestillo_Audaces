using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class OrdemProducaoMaterialService: GenericService<OrdemProducaoMaterial, OrdemProducaoMaterialRepository, OrdemProducaoMaterialController>
    {
        public OrdemProducaoMaterialService()
        {
            base.RequestUri = "OrdemProducaoMaterial";
        }

        public new IOrdemProducaoMaterialService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new OrdemProducaoMaterialServiceWeb(this.RequestUri);
            }
            else
            {
                return new OrdemProducaoMaterialServiceAPP();
            }
        }   
    }
}
