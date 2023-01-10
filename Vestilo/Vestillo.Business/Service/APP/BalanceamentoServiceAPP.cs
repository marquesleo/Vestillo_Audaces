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
    public class BalanceamentoServiceAPP: GenericServiceAPP<Balanceamento, BalanceamentoRepository, BalanceamentoController>, IBalanceamentoService
    {
        public BalanceamentoServiceAPP()
            : base(new BalanceamentoController())
        {

        }

        public Balanceamento GetByIdView(int id)
        {
            return controller.GetByIdView(id);
        }
    }
}
