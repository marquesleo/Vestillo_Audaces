using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class TabelaPrecoPCPServiceAPP : GenericServiceAPP<TabelaPrecoPCP, TabelaPrecoPCPRepository, TabelaPrecoPCPController>, ITabelaPrecoPCPService
    {

        public TabelaPrecoPCPServiceAPP()
            : base(new TabelaPrecoPCPController())
        {
        }

        public TabelaPrecoPCP GetByReferencia(string referencia)
        {
            return controller.GetByReferencia(referencia);
        }

        public IEnumerable<TabelaPrecoPCP> GetListPorReferencia(string referencia)
        {

            TabelaPrecoPCPController controller = new TabelaPrecoPCPController();
            return controller.GetListPorDescricao(referencia);

        }

        public IEnumerable<TabelaPrecoPCP> GetListPorDescricao(string desc)
        {
            TabelaPrecoPCPController controller = new TabelaPrecoPCPController();
            return controller.GetListPorDescricao(desc);
        }

        public void CalcularCustos(ref TabelaPrecoPCP tabela)
        {
            controller.CalcularCustos(ref tabela);
        }

        public void CalcularCustosComValoresManuais(ref TabelaPrecoPCP tabela)
        {
            controller.CalcularCustosComValoresManuais(ref tabela);
        }

        public void CalcularCustosItemComValorManual(ref TabelaPrecoPCP tabela, Empresa empresa, Produto produto, ItemTabelaPrecoPCP item)
        {
            controller.CalcularCustosItemComValorManual(ref tabela, empresa, produto, item);
        }

        public void CalcularCustosItem(ref TabelaPrecoPCP tabela, Empresa empresa, decimal CustoMinutoMaoObra, decimal CustoMinutoPrevisto, decimal CustoMinutoRealizado, Produto produto, ItemTabelaPrecoPCP item)
        {
            controller.CalcularCustosItem(ref tabela, empresa, CustoMinutoMaoObra, CustoMinutoPrevisto, CustoMinutoRealizado, produto, item);
        }
    }
}
