
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
    public class AdmCartaoServiceAPP : GenericServiceAPP<AdmCartao, AdmCartaoRepository, AdmCartaoController>, IAdmCartaoService
    {

        public AdmCartaoServiceAPP() : base(new AdmCartaoController())
        {
        }

        public IEnumerable<AdmCartao> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }


        public IEnumerable<AdmCartao> GetPorReferencia(string referencia)
        {
            AdmCartaoController controller = new AdmCartaoController();
            return controller.GetPorReferencia(referencia);
        }

        public IEnumerable<AdmCartao> GetPornome(string desc)
        {
            AdmCartaoController controller = new AdmCartaoController();
            return controller.GetPornome(desc);
        }

        public IEnumerable<AdmCartao> GetByIdList(int id)
        {
            AdmCartaoController controller = new AdmCartaoController();
            return controller.GetByIdList(id);
        }
    }
}



