
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ControlaEmailCobrancaController : GenericController<ControlaEmailCobranca, ControlaEmailCobrancaRepository>
    {
       

        public IEnumerable<ControlaEmailCobrancaView> GetAllView()
        {
            return _repository.GetAllView();
        }

        public IEnumerable<ControlaEmailCobrancaView> GetAllViewAtivos()
        {
            return _repository.GetAllViewAtivos();
        }

        
    }
}
