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
    public class OperacaoFaccaoServiceWeb : GenericServiceWeb<OperacaoFaccao, OperacaoFaccaoRepository, OperacaoFaccaoController>, IOperacaoFaccaoService
    {
        public OperacaoFaccaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<OperacaoFaccao> GetByIdView(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OperacaoFaccaoView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            throw new NotImplementedException();
        }


        public OperacaoFaccaoView GetByCupom(int pacoteId, int operacaoId, string sequencia)
        {
            throw new NotImplementedException();
        }
    }
}
