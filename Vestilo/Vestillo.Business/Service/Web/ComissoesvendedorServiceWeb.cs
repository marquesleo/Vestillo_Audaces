
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
    public class ComissoesvendedorServiceWeb : GenericServiceWeb<Comissoesvendedor, ComissoesvendedorRepository, ComissoesvendedorController>, IComissoesvendedorService
    {
        

        public ComissoesvendedorServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ComissoesvendedorView> GetCamposBrowse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comissoesvendedor> GetByParcelaCtr(int idParcela)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comissoesvendedor> GetByParcelaCtrDeletar(int idParcela)
        {
            throw new NotImplementedException();
        }

        public void DeletePorNotaConsumidor(int idnotaconsumidor)
        {
            throw new NotImplementedException();
        }
    }
}