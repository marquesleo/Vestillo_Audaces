using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class NfceItensService: GenericService<NfceItens, NfceItensRepository, NfceItensController>
    {
        public NfceItensService()
        {
            base.RequestUri = "NfceItens";
        }

        public new INfceItensService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new NfceItensServiceWeb(this.RequestUri);
            }
            else
            {
                return new NfceItensServiceAPP();
            }
        }  
    }
}
