using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.APP
{
    public class ChequeServiceAPP: GenericServiceAPP<Cheque, ChequeRepository, ChequeController>, IChequeService
    {
        public ChequeServiceAPP()
            : base(new ChequeController())
        {
        }
        
        public IEnumerable<ChequeView> GetAllView()
        {
            ChequeController controller = new ChequeController();
            return controller.GetAllView();
        }

        public IEnumerable<ConsultaChequeRelatorio> GetChequeRelatorio(FiltroChequeRelatorio filtro)
        {
            ChequeController controller = new ChequeController();
            return controller.GetChequeRelatorio(filtro);        
        }

        public void UpdateCamposCheque(Cheque cheque)
        {
            ChequeController controller = new ChequeController();
            controller.UpdateCamposCheque(cheque);
        }

        public void CompensarCheque(ChequeView cheque)
        {
            ChequeController controller = new ChequeController();
            controller.CompensarCheque(cheque);
        }

        public void DevolverCheque(ChequeView cheque, bool estornar)
        {
            ChequeController controller = new ChequeController();
            controller.DevolverCheque(cheque, estornar);
        }

        public void ProrrogarCheque(Cheque cheque)
        {
            ChequeController controller = new ChequeController();
            controller.ProrrogarCheque(cheque);
        }


        public ChequeView GetViewById(int id)
        {
            ChequeController controller = new ChequeController();
            return controller.GetViewById(id);
        }

        public void ResgatarCheque(Cheque cheque)
        {
            ChequeController controller = new ChequeController();
            controller.ResgatarCheque(cheque);
        }

        public IEnumerable<ChequeView> GetByNumeroCheque(string numeroCheque, bool naoCompensado = false)
        {
            ChequeController controller = new ChequeController();
            return controller.GetByNumeroCheque(numeroCheque, naoCompensado);
        }

        public IEnumerable<ChequeView> GetByReferencia(string referencia, bool naoCompensado = false)
        {
            ChequeController controller = new ChequeController();
            return controller.GetByReferencia(referencia, naoCompensado);
        }

        public IEnumerable<Cheque> GetByContasPagarBaixa(int baixaId)
        {
            ChequeController controller = new ChequeController();
            return controller.GetByContasPagarBaixa(baixaId);
        }

        public IEnumerable<Cheque> GetByContasPagarReceberBaixa(int baixaId)
        {
            ChequeController controller = new ChequeController();
            return controller.GetByContasReceberBaixa(baixaId);
        }

        public void LiberarChequeBordero(ChequeView cheque)
        {
            ChequeController controller = new ChequeController();
            controller.LiberarChequeBordero(cheque);
        }
    }
}
