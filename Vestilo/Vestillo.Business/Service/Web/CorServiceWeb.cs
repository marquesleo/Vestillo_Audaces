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
    public class CorServiceWeb : GenericServiceWeb<Cor, CorRepository, CorController>, ICorService
    {

        public CorServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Cor> GetByAtivos(int AtivoInativo)
        {
            var c = new ConnectionWebAPI<Cor>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }


        public IEnumerable<Cor> GetListPorDescricao(string Descricao)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Cor> GetCoresProduto(int produto)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Cor> GetCoresByProdutoTamanho(int produto, int tamanho)
        {
            throw new NotImplementedException();
        }
    }
}


