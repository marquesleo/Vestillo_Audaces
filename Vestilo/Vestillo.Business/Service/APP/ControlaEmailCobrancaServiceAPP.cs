
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
    public class ControlaEmailCobrancaServiceAPP : GenericServiceAPP<ControlaEmailCobranca, ControlaEmailCobrancaRepository, ControlaEmailCobrancaController>, IControlaEmailCobrancaService
    {
        public ControlaEmailCobrancaServiceAPP() : base(new ControlaEmailCobrancaController())
        {

        }
                

        public IEnumerable<ControlaEmailCobrancaView> GetAllView()
        {
            return controller.GetAllView();
        }

        public IEnumerable<ControlaEmailCobrancaView> GetAllViewAtivos()
        {
            return controller.GetAllViewAtivos();
        }



    }
}
