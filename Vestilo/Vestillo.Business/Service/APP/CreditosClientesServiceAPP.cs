
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class CreditosClientesServiceAPP : GenericServiceAPP<CreditosClientes, CreditosClientesRepository, CreditosClientesController>, ICreditosClientesService
    {
        public CreditosClientesServiceAPP(): base(new CreditosClientesController())
        {
        }

        public CreditosClientesView GetViewById(int id)
        {
            return controller.GetViewById(id);
        }

        public IEnumerable<CreditosClientesView> GetFiltro(string cliente)
        {
            return controller.GetFiltro(cliente);
        }

        public IEnumerable<CreditosClientesView> GetCamposBrowse()
        {
            return controller.GetCamposBrowse();
        }

        public IEnumerable<CreditosClientesView> GetByCreditoAbertos(int idCliente,int Ativo)
        {
            return controller.GetByCreditoAbertos(idCliente, Ativo);
        }

        public IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim)
        {
            return controller.GetLiberarComissoes(Vendedores, Guias, DataInicio, DataFim);
        }

        public IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar)
        {
            return controller.GetCancelarLiberacao(idContasPagar);
        }

        public IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, string Guias, DateTime DataInicio,DateTime DataFim)
        {
            return controller.GetListagemPorPeriodo(Vendedores, Guias, DataInicio, DataFim);
        }

        public CreditosClientes GetByCredito(int idDevolucaoItens)
        {

            return controller.GetByCredito(idDevolucaoItens);           
        }

        public CreditosClientes GetByNotaConsumidor(int idnotaconsumidor)
        {
            return controller.GetByNotaConsumidor(idnotaconsumidor);
        }

        public IEnumerable<CreditosClientes> GetByNotaConsumidorQuitado(int idnotaconsumidor)
        {
            return controller.GetByNotaConsumidorQuitado(idnotaconsumidor);
        }
    }
}