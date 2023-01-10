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
    public class ContasReceberBaixaServiceWeb : GenericServiceWeb<ContasReceberBaixa, ContasReceberBaixaRepository, ContasReceberBaixaController>, IContasReceberBaixaService
    {

        public ContasReceberBaixaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public List<ContasReceberBaixa> GetByContasReceber(int ContasReceberBaixa)
        {
            var c = new ConnectionWebAPI<ContasReceberBaixa>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?ContasReceberBaixa=" + ContasReceberBaixa.ToString()).ToList();
        }

        public void Save(ref ContasReceberBaixa baixa, decimal creditoCliente)
        {
            throw new NotImplementedException();
        }

        public void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int clienteId = 0)
        {
            throw new NotImplementedException();
        }

        public void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasReceberBaixaLoteId)
        {
            throw new NotImplementedException();
        }
    }
}


