using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class NcmServiceAPP: GenericServiceAPP<Ncm, NcmRepository, NcmController>, INcmService
    {
        public NcmServiceAPP()
            : base(new NcmController())
        {
        }

        public Ncm GetByReferencia(string referencia)
        {
            NcmController controller = new NcmController();
            return controller.GetByReferencia(referencia);
        }
    }
}
