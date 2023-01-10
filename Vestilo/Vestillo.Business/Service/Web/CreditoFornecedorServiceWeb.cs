
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class CreditoFornecedorServiceWeb : GenericServiceWeb<CreditoFornecedor, CreditoFornecedorRepository, CreditoFornecedorController>, ICreditoFornecedorService
    {
        public CreditoFornecedorServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<CreditoFornecedor> GetByContasPagarBaixa(int contasPagarBaixaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditoFornecedorView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public CreditoFornecedorView GetViewById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditoFornecedorView> GetFiltro(string fornecedor)
        {
            throw new NotImplementedException();
        }
    }
}
