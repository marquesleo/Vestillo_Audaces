
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface ICreditosClientesService : IService<CreditosClientes, CreditosClientesRepository, CreditosClientesController>
    {
        IEnumerable<CreditosClientesView> GetCamposBrowse();
        IEnumerable<CreditosClientesView> GetByCreditoAbertos(int idCliente,int Ativo);
        IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim);
        IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar);
        IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim);
        IEnumerable<CreditosClientesView> GetFiltro(string cliente);
        CreditosClientesView GetViewById(int id);
        CreditosClientes GetByCredito(int idDevolucaoItens);
        CreditosClientes GetByNotaConsumidor(int idnotaconsumidor);
        IEnumerable<CreditosClientes> GetByNotaConsumidorQuitado(int idnotaconsumidor);
    }

}