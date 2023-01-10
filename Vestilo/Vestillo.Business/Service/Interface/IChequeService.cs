using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IChequeService : IService<Cheque, ChequeRepository, ChequeController>
    {
        IEnumerable<ChequeView> GetAllView();
        void UpdateCamposCheque(Cheque cheque);
        void CompensarCheque(ChequeView cheque);
        void DevolverCheque(ChequeView cheque, bool estornar);
        void ProrrogarCheque(Cheque cheque);
        void ResgatarCheque(Cheque cheque);
        ChequeView GetViewById(int id);
        IEnumerable<ChequeView> GetByNumeroCheque(string numeroCheque, bool naoCompensado = false);
        IEnumerable<ChequeView> GetByReferencia(string referencia, bool naoCompensado = false);
        IEnumerable<Cheque> GetByContasPagarBaixa(int baixaId);
        IEnumerable<Cheque> GetByContasPagarReceberBaixa(int baixaId);
        IEnumerable<ConsultaChequeRelatorio> GetChequeRelatorio(FiltroChequeRelatorio filtro);
        void LiberarChequeBordero(ChequeView cheque);
    }
}
