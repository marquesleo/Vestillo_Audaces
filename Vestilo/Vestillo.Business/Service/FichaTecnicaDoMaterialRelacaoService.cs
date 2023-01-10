using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
namespace Vestillo.Business.Service
{
    public class FichaTecnicaDoMaterialRelacaoService : GenericService<FichaTecnicaDoMaterialRelacao, 
                                                                        FichaTecnicaDoMaterialRelacaoRepository, 
                                                                        FichaTecnicaDoMaterialRelacaoController>
    {

        public FichaTecnicaDoMaterialRelacaoService()
        {
            base.RequestUri = "FichaTecnicaDoMaterialRelacao";
        }

        public new IFichaTecnicaDoMaterialRelacaoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new FichaTecnicaDoMaterialRelacaoWeb(this.RequestUri);
            }
            else
            {
                return new FichaTecnicaDoMaterialRelacaoAPP();
            }
        }   
    }
}
