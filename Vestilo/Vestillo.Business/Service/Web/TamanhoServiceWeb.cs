
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
    public class TamanhoServiceWeb : GenericServiceWeb<Tamanho, TamanhoRepository, TamanhoController>, ITamanhoService
    {

        public TamanhoServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Tamanho> GetByAtivos(int AtivoInativo)
        {
            var t = new ConnectionWebAPI<Tamanho>(VestilloSession.UrlWebAPI);
            return t.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }


        public IEnumerable<Tamanho> GetListByIds(List<int> ids)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Tamanho> GetListPorDescricao(string Descricao)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Tamanho> GetTamanhosProduto(int produto)
        {
            throw new NotImplementedException();
        }
    }
}

