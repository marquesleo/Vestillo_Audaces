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
    public interface IContasReceberBaixaService : IService<ContasReceberBaixa, ContasReceberBaixaRepository, ContasReceberBaixaController>
    {
        List<ContasReceberBaixa> GetByContasReceber(int contasReceberId);
        void Save(ref ContasReceberBaixa baixa, decimal creditoCliente);
        void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int clienteId = 0);
        void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasReceberBaixaLoteId);
    }
}



