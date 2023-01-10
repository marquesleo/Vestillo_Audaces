
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
    public interface IComissoesvendedorService : IService<Comissoesvendedor, ComissoesvendedorRepository, ComissoesvendedorController>
    {
        IEnumerable<ComissoesvendedorView> GetCamposBrowse();
        IEnumerable<Comissoesvendedor> GetByParcelaCtr(int idParcela);
        IEnumerable<ComissoesvendedorView> GetLiberarComissoes(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim);
        IEnumerable<Comissoesvendedor> GetCancelarLiberacao(int idContasPagar);
        IEnumerable<ComissoesvendedorView> GetListagemPorPeriodo(string Vendedores, string Guias, DateTime DataInicio, DateTime DataFim);
        IEnumerable<Comissoesvendedor> GetByParcelaCtrDeletar(int idParcela);
        void DeletePorNotaConsumidor(int idnotaconsumidor);

    }

}