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
    public class FichaTecnicaOperacaoServiceAPP : GenericServiceAPP<FichaTecnicaOperacao, FichaTecnicaOperacaoRepository, FichaTecnicaOperacaoController>, IFichaTecnicaOperacaoService
    {
        public FichaTecnicaOperacaoServiceAPP()
            : base(new FichaTecnicaOperacaoController())
        {

        }

        public IEnumerable<FichaTecnicaOperacao> GetByProduto(int produtoId)
        {
            return controller.GetByProduto(produtoId);
        }

        public IEnumerable<FichaTecnicaOperacao> GetByFichaTecnica(int fichaTecnicaId)
        {
            return controller.GetByFichaTecnica(fichaTecnicaId);
        }
    }
}



