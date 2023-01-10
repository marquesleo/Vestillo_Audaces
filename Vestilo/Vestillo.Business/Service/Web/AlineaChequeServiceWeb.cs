using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class AlineaChequeServiceWeb: GenericServiceWeb<AlineaCheque, AlineaChequeRepository, AlineaChequeController>, IAlineaChequeService
    {
        public AlineaChequeServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public AlineaCheque GetByAbreviatura(string abreviatura)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlineaCheque> GetListByAbreviatura(string abreviatura)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlineaCheque> GetListByDescricao(string descricao)
        {
            throw new NotImplementedException();
        }
    }
}
