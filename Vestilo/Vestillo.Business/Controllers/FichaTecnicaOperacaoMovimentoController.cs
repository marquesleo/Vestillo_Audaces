using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class FichaTecnicaOperacaoMovimentoController : GenericController<FichaTecnicaOperacaoMovimento, FichaTecnicaOperacaoMovimentoRepository>
    {
        public IEnumerable<FichaTecnicaOperacaoMovimento> GetByFichaTecnica(int fichaTecnicaId)
        {
            return _repository.GetByFichaTecnica(fichaTecnicaId);
        }
    }
}
