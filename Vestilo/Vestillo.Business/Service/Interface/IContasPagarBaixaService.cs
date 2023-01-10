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
    public interface IContasPagarBaixaService : IService<ContasPagarBaixa, ContasPagarBaixaRepository, ContasPagarBaixaController>
    {
        List<ContasPagarBaixa> GetByContasPagar(int ContasPagarId);
        void Save(ref ContasPagarBaixa baixa, decimal creditoFornecedor);
        void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int fornecedorId = 0);
        void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasPagarBaixaLoteId);
    }
}



