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
    public class ContasReceberBaixaServiceAPP : GenericServiceAPP<ContasReceberBaixa, ContasReceberBaixaRepository, ContasReceberBaixaController>, IContasReceberBaixaService
    {

        public ContasReceberBaixaServiceAPP() : base(new ContasReceberBaixaController())
        {
        }

        public List<ContasReceberBaixa> GetByContasReceber(int contasReceberId)
        {
            return controller.GetByContasReceber(contasReceberId);
        }

        public void Save(ref ContasReceberBaixa baixa, decimal creditoCliente)
        {
            controller.Save(ref baixa, creditoCliente);
        }

        public void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int clienteId = 0)
        {
            controller.BaixaEmLote(parcelasBaixa, cheques, valorDinheiro, valorCreditoGerar, clienteId);
        }

        public void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasReceberBaixaLoteId)
        {
            controller.EstornarLote(parcelas, contasReceberBaixaLoteId);
        }
    }
}



