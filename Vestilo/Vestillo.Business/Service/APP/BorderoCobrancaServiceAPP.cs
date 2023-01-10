using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class BorderoCobrancaServiceAPP : GenericServiceAPP<BorderoCobranca, BorderoCobrancaRepository, BorderoCobrancaController>, IBorderoCobrancaService
    {
        public BorderoCobrancaServiceAPP() : base(new BorderoCobrancaController())
        {
        }

        public void BaixarEstornarBordero(BorderoCobranca bordero, bool estornar = false)
        {
            base.controller.BaixarEstornarBordero(bordero, estornar);
        }
        
        public IEnumerable<BorderoCobranca> GetByDocumento(int documentoId, bool isCheque)
        {
            return base.controller.GetByDocumento(documentoId, isCheque);
        }
    }
}