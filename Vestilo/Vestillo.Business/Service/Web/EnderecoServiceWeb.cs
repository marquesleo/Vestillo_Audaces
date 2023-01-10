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
    public class EnderecoServiceWeb : GenericServiceWeb<Endereco, EnderecoRepository, EnderecoController>, IEnderecoService
    {

        public EnderecoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Endereco> GetByEmpresaId(int empresaId)
        {
            var c = new ConnectionWebAPI<Endereco>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?empresaId=" + empresaId.ToString());
        }
    }
}
