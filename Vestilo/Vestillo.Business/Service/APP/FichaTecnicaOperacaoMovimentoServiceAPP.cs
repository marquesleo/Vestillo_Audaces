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
    public class FichaTecnicaOperacaoMovimentoServiceAPP : GenericServiceAPP<FichaTecnicaOperacaoMovimento, FichaTecnicaOperacaoMovimentoRepository, FichaTecnicaOperacaoMovimentoController>, IFichaTecnicaOperacaoMovimentoService
    {
        public FichaTecnicaOperacaoMovimentoServiceAPP()
            : base(new FichaTecnicaOperacaoMovimentoController())
        {

        }

        public IEnumerable<FichaTecnicaOperacaoMovimento> GetByFichaTecnica(int fichaTecnicaId)
        {
            return controller.GetByFichaTecnica(fichaTecnicaId);
        }
    }
}
