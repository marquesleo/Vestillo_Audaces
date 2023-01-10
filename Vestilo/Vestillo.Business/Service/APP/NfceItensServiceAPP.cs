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
    public class NfceItensServiceAPP: GenericServiceAPP<NfceItens, NfceItensRepository, NfceItensController>, INfceItensService
    {
        public NfceItensServiceAPP()
            : base(new NfceItensController())
        {
        }

        public IEnumerable<NfceItens> GetListByNfce(int IdNfce)
        {
            return controller.GetListByNfce(IdNfce);
        }

        public IEnumerable<NfceItensView> GetListViewItensNfce(int IdNfce, bool emissao = false)
        {
            return controller.GetListViewItensNfce(IdNfce, emissao);
        }


        public IEnumerable<NfceItensView> GetListViewItensNfceAgrupado(int IdNfce, bool emissao = false)
        {
            return controller.GetListViewItensNfceAgrupado(IdNfce, emissao);
        }
        

       
    }
}
