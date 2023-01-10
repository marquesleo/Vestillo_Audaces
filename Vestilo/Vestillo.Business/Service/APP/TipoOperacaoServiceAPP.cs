
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
    public class TipoOperacaoServiceAPP : GenericServiceAPP<TipoOperacao, TipoOperacaoRepository, TipoOperacaoController>, ITipoOperacaoService
    {

        public TipoOperacaoServiceAPP() : base(new TipoOperacaoController())
        {
        }

        public IEnumerable<TipoOperacao> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }
    }
}



