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
    public class OperacaoOperadoraServiceWeb : GenericServiceWeb<OperacaoOperadora, OperacaoOperadoraRepository, OperacaoOperadoraController>, IOperacaoOperadoraService
    {
        public OperacaoOperadoraServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<OperacaoOperadora> GetByIdView(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OperacaoOperadoraView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            throw new NotImplementedException();
        }


        public OperacaoOperadoraView GetByCupom(int pacoteId, int operacaoId, string sequencia)
        {
            throw new NotImplementedException();
        }


    }
}
