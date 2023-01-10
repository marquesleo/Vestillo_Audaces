
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
    public class NotaEntradaFaccaoServiceApp : GenericServiceAPP<NotaEntradaFaccao, NotaEntradaFaccaoRepository, NotaEntradaFaccaoController>, INotaEntradaFaccaoService
    {
        public NotaEntradaFaccaoServiceApp() : base(new NotaEntradaFaccaoController())
        {
        }

        public IEnumerable<NotaEntradaFaccaoView> GetCamposEspecificos(string parametrosDaBusca)
        {
            return controller.GetCamposEspecificos(parametrosDaBusca);
        }


        public IEnumerable<NotaEntradaFaccaoView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {

            return controller.GetListPorReferencia(referencia, parametrosDaBusca);

        }

        public IEnumerable<NotaEntradaFaccaoView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            return controller.GetListPorNumero(Numero, parametrosDaBusca);

        }

        public IEnumerable<NotaEntradaFaccaoView> GetListPorOrdem(int OrdemId)
        {
            return controller.GetListPorOrdem(OrdemId);
        }
    }
}
