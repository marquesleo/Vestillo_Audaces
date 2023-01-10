
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
    public class ComissoesvendedorServiceAPP : GenericServiceAPP<Comissoesvendedor, ComissoesvendedorRepository, ComissoesvendedorController>, IComissoesvendedorService
    {
        public ComissoesvendedorServiceAPP()  : base(new ComissoesvendedorController())
        {
        }

        public IEnumerable<ComissoesvendedorView> GetCamposBrowse()
        {
            return controller.GetCamposBrowse();
        }

        public IEnumerable<Comissoesvendedor> GetByParcelaCtr(int idParcela)
        {
            return controller.GetByParcelaCtr(idParcela);
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

        public IEnumerable<Comissoesvendedor> GetByParcelaCtrDeletar(int idParcela)
        {
            return controller.GetByParcelaCtrDeletar(idParcela);
        }

        public void DeletePorNotaConsumidor(int idnotaconsumidor)
        {
            controller.DeletePorNotaConsumidor(idnotaconsumidor);
        }
    }
}