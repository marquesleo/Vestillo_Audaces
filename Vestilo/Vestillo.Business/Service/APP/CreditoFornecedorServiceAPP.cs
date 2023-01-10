
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
    public class CreditoFornecedorServiceAPP : GenericServiceAPP<CreditoFornecedor, CreditoFornecedorRepository, CreditoFornecedorController>, ICreditoFornecedorService
    {
        public CreditoFornecedorServiceAPP() : base(new CreditoFornecedorController())
        {
        
        }

        public IEnumerable<CreditoFornecedor> GetByContasPagarBaixa(int contasPagarBaixaId)
        {
            return controller.GetByContasPagarBaixa(contasPagarBaixaId);
        }

        public IEnumerable<CreditoFornecedorView> GetAllView()
        {
            return controller.GetAllView();
        }

        public CreditoFornecedorView GetViewById(int id)
        {
            return controller.GetViewById(id);
        }

        public IEnumerable<CreditoFornecedorView> GetFiltro(string fornecedor)
        {
            return controller.GetFiltro(fornecedor);
        }

    }
}
