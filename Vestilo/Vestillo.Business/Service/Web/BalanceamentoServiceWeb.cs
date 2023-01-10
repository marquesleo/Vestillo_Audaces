using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.Web
{
    public  class BalanceamentoServiceWeb : GenericServiceWeb<Balanceamento, BalanceamentoRepository, BalanceamentoController>, IBalanceamentoService
    {

        public BalanceamentoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }


        public Balanceamento GetByIdView(int id)
        {
            throw new NotImplementedException();
        }
    }
}
