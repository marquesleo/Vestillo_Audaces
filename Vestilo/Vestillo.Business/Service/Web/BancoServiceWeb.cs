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
    public class BancoServiceWeb: GenericServiceWeb<Banco, BancoRepository, BancoController>, IBancoService
    {
        public BancoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Banco> GetPorNumBanco(string numBanco)
        {
            var c = new ConnectionWebAPI<IEnumerable<Banco>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "numbanco like '%" + numBanco + "%' And ativo = 1");
        }

        public IEnumerable<Banco> GetPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<Banco>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Banco> GetByIdList(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<Banco>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id);
        }

        public IEnumerable<Banco> GetAllAtivos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Banco> GetAllParaBoleto()
        {
            throw new NotImplementedException();

        }

        public Banco GetPadraoVenda()
        {
            throw new NotImplementedException();

        }
    }
}
