
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class BalanceamentoSemanalServiceAPP : GenericServiceAPP<BalanceamentoSemanal, BalanceamentoSemanalRepository, BalanceamentoSemanalController>, IBalanceamentoSemanalService
    {
        public BalanceamentoSemanalServiceAPP() : base(new BalanceamentoSemanalController())
        {

        }
    }
}



