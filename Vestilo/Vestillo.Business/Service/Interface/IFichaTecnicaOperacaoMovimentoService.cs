using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IFichaTecnicaOperacaoMovimentoService : IService<FichaTecnicaOperacaoMovimento, FichaTecnicaOperacaoMovimentoRepository, FichaTecnicaOperacaoMovimentoController>
    {
        IEnumerable<FichaTecnicaOperacaoMovimento> GetByFichaTecnica(int fichaTecnicaId);
    }
}
