
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;


namespace Vestillo.Business.Controllers
{
    public class CreditosClientesController : GenericController<CreditosClientes, CreditosClientesRepository>
    {
        public CreditosClientesView GetViewById(int id)
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetViewById(id);
            }
        }

        public IEnumerable<CreditosClientes> GetByContasReceberQueGerou(int contasReceberId)
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetByContasReceberQueGerou(contasReceberId);
            }
        }

        public IEnumerable<CreditosClientesView> GetCamposBrowse()
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetCamposBrowse();
            }
        }

        public IEnumerable<CreditosClientesView> GetByCreditoAbertos(int idCliente, int Ativo)
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetByCreditoAbertos(idCliente,Ativo);
            }
        }

        public IEnumerable<CreditosClientesView> GetFiltro(string cliente)
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetFiltro(cliente);
            }
        }

        public IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim)
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                return repository.GetLiberarComissoes(Vendedores, Guias, DataInicio, DataFim);
            }
        }

         public IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar)
        {
            using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
            {
                return repository.GetCancelarLiberacao(idContasPagar);
            }
        }

        public IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, string Guias, DateTime DataInicio,DateTime DataFim)
         {
             using (ComissoesvendedorRepository repository = new ComissoesvendedorRepository())
             {
                 return repository.GetListagemPorPeriodo(Vendedores, Guias, DataInicio, DataFim);
             }
         }

        public CreditosClientes GetByCredito(int idDevolucaoItens)
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetByCredito(idDevolucaoItens);
            }

        }

        public IEnumerable<CreditosClientes> GetByNotaConsumidorQuitado(int idnotaconsumidor)
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetByNotaConsumidorQuitado(idnotaconsumidor);
            }
        }

        public CreditosClientes GetByNotaConsumidor(int idnotaconsumidor)
        {
            using (CreditosClientesRepository repository = new CreditosClientesRepository())
            {
                return repository.GetByNotaConsumidor(idnotaconsumidor);
            }
        }
    }
}
