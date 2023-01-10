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
    public interface IFichaTecnicaOperacaoService : IService<FichaTecnicaOperacao, FichaTecnicaOperacaoRepository, FichaTecnicaOperacaoController>
    {
        IEnumerable<FichaTecnicaOperacao> GetByProduto(int produtoId);
        IEnumerable<FichaTecnicaOperacao> GetByFichaTecnica(int fichaTecnicaId);
    }
}
