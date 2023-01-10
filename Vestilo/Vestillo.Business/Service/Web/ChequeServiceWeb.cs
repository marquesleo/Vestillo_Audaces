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
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.Web
{
    public class ChequeServiceWeb: GenericServiceWeb<Cheque, ChequeRepository, ChequeController>, IChequeService
    {
        public ChequeServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ConsultaChequeRelatorio> GetChequeRelatorio(FiltroChequeRelatorio filtro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChequeView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public void UpdateCamposCheque(Cheque cheque)
        {
            throw new NotImplementedException();
        }

        public void CompensarCheque(ChequeView cheque)
        {
            throw new NotImplementedException();
        }

        public void DevolverCheque(ChequeView cheque, bool estornar)
        {
            throw new NotImplementedException();
        }

        public void ProrrogarCheque(Cheque cheque)
        {
            throw new NotImplementedException();
        }

        public ChequeView GetViewById(int id)
        {
            throw new NotImplementedException();
        }

        public void ResgatarCheque(Cheque cheque)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChequeView> GetByNumeroCheque(string numeroCheque, bool naoCompensado = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChequeView> GetByReferencia(string referencia, bool naoCompensado = false)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Cheque> GetByContasPagarBaixa(int baixaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cheque> GetByContasPagarReceberBaixa(int baixaId)
        {
            throw new NotImplementedException();
        }

        public void LiberarChequeBordero(ChequeView cheque)
        {
            throw new NotImplementedException();
        }
    }
}
