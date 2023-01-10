using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class CreditoFornecedorController : GenericController<CreditoFornecedor, CreditoFornecedorRepository>
    {
        public IEnumerable<CreditoFornecedor> GetByContasPagarBaixa(int contasPagarBaixaId)
        {
            return _repository.GetByContasPagarBaixa(contasPagarBaixaId);
        }


        public IEnumerable<CreditoFornecedor> GetByContasPagarQueGerou(int contasPagarId)
        {
            using (CreditoFornecedorRepository repository = new CreditoFornecedorRepository())
            {
                return repository.GetByContasPagarBaixaQueGerou(contasPagarId);
            }
        }

        public IEnumerable<CreditoFornecedorView> GetAllView()
        {
            return _repository.GetAllView();
        }

        public CreditoFornecedorView GetViewById(int id)
        {
            return _repository.GetViewById(id);
        }

        public IEnumerable<CreditoFornecedorView> GetFiltro(string fornecedor)
        {
            return _repository.GetFiltro(fornecedor);
        }
    }
}
