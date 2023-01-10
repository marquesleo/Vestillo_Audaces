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
    public interface ICreditoFornecedorService : IService<CreditoFornecedor, CreditoFornecedorRepository, CreditoFornecedorController>
    {
        IEnumerable<CreditoFornecedor> GetByContasPagarBaixa(int contasPagarBaixaId);
        IEnumerable<CreditoFornecedorView> GetAllView();
        CreditoFornecedorView GetViewById(int id);
        IEnumerable<CreditoFornecedorView> GetFiltro(string fornecedor);
    }
}
