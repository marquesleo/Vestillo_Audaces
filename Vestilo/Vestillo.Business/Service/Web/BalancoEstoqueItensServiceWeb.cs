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
    public class BalancoEstoqueItensServiceWeb : GenericServiceWeb<BalancoEstoqueItens, BalancoEstoqueItensRepository, BalancoEstoqueItensController>, IBalancoEstoqueItensService
    {
        public BalancoEstoqueItensServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<BalancoEstoqueItensView> GetViewByBalanco(int idBalanco)
        {
            throw new NotImplementedException();
        }
    }
}
