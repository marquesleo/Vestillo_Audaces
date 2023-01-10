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
    public class FichaTecnicaOperacaoController : GenericController<FichaTecnicaOperacao, FichaTecnicaOperacaoRepository>
    {
        public IEnumerable<FichaTecnicaOperacao> GetByProduto(int produtoId)
        {
            return _repository.GetByProduto(produtoId);
        }

        public IEnumerable<FichaTecnicaOperacao> GetByFichaTecnica(int fichaTecnicaId)
        {
            return _repository.GetByFichaTecnica(fichaTecnicaId);
        }

        public IEnumerable<FichaTecnicaOperacao> GetByMovimentosDaOperacao(int movimentoId)
        {
            var fichas = _repository.GetByMovimentosDaOperacao(movimentoId).ToList();           

            return fichas.AsEnumerable();
        }
    }
}
