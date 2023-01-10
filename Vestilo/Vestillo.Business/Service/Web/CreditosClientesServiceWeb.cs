
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
    public class CreditosClientesServiceWeb : GenericServiceWeb<CreditosClientes, CreditosClientesRepository, CreditosClientesController>, ICreditosClientesService
    {
        

        public CreditosClientesServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<CreditosClientesView> GetFiltro(string cliente)
        {
            throw new NotImplementedException();
        }

        public CreditosClientesView GetViewById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditosClientesView> GetCamposBrowse()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditosClientesView> GetByCreditoAbertos(int idCliente, int Ativo)
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

        public CreditosClientes GetByCredito(int idDevolucaoItens)
        {
            throw new NotImplementedException();
        }
        public CreditosClientes GetByNotaConsumidor(int idnotaconsumidor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditosClientes> GetByNotaConsumidorQuitado(int idnotaconsumidor)
        {
            throw new NotImplementedException();
        }
    }
}