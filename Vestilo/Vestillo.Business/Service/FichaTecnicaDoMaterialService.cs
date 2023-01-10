using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.Interface;


namespace Vestillo.Business.Service
{
    public class FichaTecnicaDoMaterialService : GenericService<FichaTecnicaDoMaterial, FichaTecnicaDoMaterialRepository, FichaTecnicaDoMaterialController>
    {
        public FichaTecnicaDoMaterialService()
        {
            base.RequestUri = "FichaTecnicaDoMaterial";
        }


        public new IFichaTecnicaDoMaterialService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new FichaTecnicaDoMaterialServiceWeb(this.RequestUri);
            }
            else
            {
                return new FichaTecnicaDoMaterialServiceAPP();
            }
        }   
    }
}
