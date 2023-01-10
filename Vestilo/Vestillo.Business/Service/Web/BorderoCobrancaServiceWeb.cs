using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class BorderoCobrancaServiceWeb : GenericServiceWeb<BorderoCobranca, BorderoCobrancaRepository, BorderoCobrancaController>, IBorderoCobrancaService
    {
        public BorderoCobrancaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void BaixarEstornarBordero(BorderoCobranca bordero, bool estornar = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BorderoCobranca> GetByDocumento(int documentoId, bool isCheque)
        {
            throw new NotImplementedException();
        }
    }
}
