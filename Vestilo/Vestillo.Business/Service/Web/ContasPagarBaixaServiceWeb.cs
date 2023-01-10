using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ContasPagarBaixaServiceWeb : GenericServiceWeb<ContasPagarBaixa, ContasPagarBaixaRepository, ContasPagarBaixaController>, IContasPagarBaixaService
    {

        public ContasPagarBaixaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public List<ContasPagarBaixa> GetByContasPagar(int ContasPagarBaixa)
        {
            var c = new ConnectionWebAPI<ContasPagarBaixa>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?ContasPagarBaixa=" + ContasPagarBaixa.ToString()).ToList();
        }

        public void Save(ref ContasPagarBaixa baixa, decimal creditoCliente)
        {
            throw new NotImplementedException();
        }

        public void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int fornecedorId = 0)
        {
            throw new NotImplementedException();
        }

        public void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasPagarBaixaLoteId)
        {
            throw new NotImplementedException();
        }
    }
}


