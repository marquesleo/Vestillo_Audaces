using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface ITabelaPrecoPCPService: IService<TabelaPrecoPCP, TabelaPrecoPCPRepository, TabelaPrecoPCPController>
    {
        TabelaPrecoPCP GetByReferencia(string referencia);
        IEnumerable<TabelaPrecoPCP> GetListPorDescricao(string desc);
        IEnumerable<TabelaPrecoPCP> GetListPorReferencia(string referencia);
        void CalcularCustos(ref TabelaPrecoPCP tabela);
        void CalcularCustosItemComValorManual(ref TabelaPrecoPCP tabela, Empresa empresa, Produto produto, ItemTabelaPrecoPCP item);
        void CalcularCustosComValoresManuais(ref TabelaPrecoPCP tabela);
        void CalcularCustosItem(ref TabelaPrecoPCP tabela, Empresa empresa, decimal CustoMinutoMaoObra, decimal CustoMinutoPrevisto, decimal CustoMinutoRealizado, Produto produto, ItemTabelaPrecoPCP item);
    }
}
