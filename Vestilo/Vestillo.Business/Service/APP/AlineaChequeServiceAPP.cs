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
    public class AlineaChequeServiceAPP: GenericServiceAPP<AlineaCheque, AlineaChequeRepository, AlineaChequeController>, IAlineaChequeService
    {
        public AlineaChequeServiceAPP()
            : base(new AlineaChequeController())
        {
        }

        public AlineaCheque GetByAbreviatura(string abreviatura)
        {
            AlineaChequeController controller = new AlineaChequeController();
            return controller.GetByAbreviatura(abreviatura);
        }

        public IEnumerable<AlineaCheque> GetListByAbreviatura(string abreviatura)
        {
            AlineaChequeController controller = new AlineaChequeController();
            return controller.GetListByAbreviatura(abreviatura);
        }

        public IEnumerable<AlineaCheque> GetListByDescricao(string descricao)
        {
            AlineaChequeController controller = new AlineaChequeController();
            return controller.GetListByDescricao(descricao);
        }
    }
}
