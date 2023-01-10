
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class AdmCartaoController : GenericController<AdmCartao, AdmCartaoRepository>
    {
        public IEnumerable<AdmCartao> GetByAtivos(int AtivoInativo)
        {
            using (AdmCartaoRepository repository = new AdmCartaoRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<AdmCartao> GetPorReferencia(string referencia)
        {
            using (AdmCartaoRepository repository = new AdmCartaoRepository())
            {
                return repository.GetPorReferencia(referencia);
            }
        }

        public IEnumerable<AdmCartao> GetPornome(string desc)
        {
            using (AdmCartaoRepository repository = new AdmCartaoRepository())
            {
                return repository.GetPornome(desc);
            }
        }

        public IEnumerable<AdmCartao> GetByIdList(int id)
        {
            using (AdmCartaoRepository repository = new AdmCartaoRepository())
            {
                return repository.GetByIdList(id);
            }
        }

    }
}
