
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
    public class SerieNfeService : GenericService<SerieNfe , SerieNfeRepository, SerieNfeController>
    {
        public SerieNfeService()
        {
            base.RequestUri = "SerieNfe";
        }

        public new ISerieNfeService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new SerieNfeServiceWeb(this.RequestUri);
            }
            else
            {
                return new SerieNfeServiceAPP();
            }
        }   
    }
}
