using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class PessoasPremioServiceWeb: GenericServiceWeb<PessoasPremio, PessoasPremioRepository, PessoasPremioController>, IPessoasPremioService
    {
        public PessoasPremioServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<PessoasPremio> GetByPessoaId(int pessoaId)
        {
            throw new NotImplementedException();
        }
    }
}
