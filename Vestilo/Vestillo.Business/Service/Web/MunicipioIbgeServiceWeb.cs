using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    class MunicipioIbgeServiceWeb: GenericServiceWeb<MunicipioIbge, MunicipioIbgeRepository, MunicipioIbgeController>, IMunicipioService
    {

        public MunicipioIbgeServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<MunicipioIbge> GetAllWhere(string where)
        {
            var c = new ConnectionWebAPI<IEnumerable<MunicipioIbge>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "uf = '" + where + "'");
        }
    }
}
