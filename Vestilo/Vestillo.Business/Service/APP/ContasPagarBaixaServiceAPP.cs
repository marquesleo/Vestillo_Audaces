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
    public class ContasPagarBaixaServiceAPP : GenericServiceAPP<ContasPagarBaixa, ContasPagarBaixaRepository, ContasPagarBaixaController>, IContasPagarBaixaService
    {

        public ContasPagarBaixaServiceAPP() : base(new ContasPagarBaixaController())
        {
        }

        public List<ContasPagarBaixa> GetByContasPagar(int ContasPagarId)
        {
            return controller.GetByContasPagar(ContasPagarId);
        }


        public void Save(ref ContasPagarBaixa baixa, decimal creditoFornecedor)
        {
            controller.Save(ref baixa, creditoFornecedor);
        }

        public void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int fornecedorId = 0)
        {
            controller.BaixaEmLote(parcelasBaixa, cheques, valorDinheiro, valorCreditoGerar, fornecedorId);
        }

        public void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasPagarBaixaLoteId)
        {
            controller.EstornarLote(parcelas, contasPagarBaixaLoteId);
        }
    }
}



