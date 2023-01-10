
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


namespace Vestillo.Business.Service
{
    public class SerieNfceService : GenericService<SerieNfce , SerieNfceRepository, SerieNfceController>
    {
        public SerieNfceService()
        {
            base.RequestUri = "SerieNfce";
        }

        public new ISerieNfceService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new SerieNfceServiceWeb(this.RequestUri);
            }
            else
            {
                return new SerieNfceServiceAPP();
            }
        }   
    }
}
