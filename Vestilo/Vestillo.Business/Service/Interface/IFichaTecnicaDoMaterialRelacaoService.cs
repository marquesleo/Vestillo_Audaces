using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.Interface
{
    public interface IFichaTecnicaDoMaterialRelacaoService : IService<FichaTecnicaDoMaterialRelacao, 
                                                               FichaTecnicaDoMaterialRelacaoRepository,
                                                               FichaTecnicaDoMaterialRelacaoController>
    {

         IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByFichaTecnica(int FichaTecnicaId);

         void ExcluirRelacao(int FichaTecnicaId);

        IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGrade(ProdutoDetalheView grade);

        void UpdateFichaRelacao(FichaTecnicaDoMaterialRelacao ficha, int TipoItem);

        IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGradeProduto(ProdutoDetalhe grade);

        List<MontagemFichaTecnicaDoMaterialCorView> getMontagemFichaTecnicaDoMaterialCor(int produtoId, int materiaPrimaId);

        List<MontagemFichaTecnicaDoMaterialTamanhoView> getMontagemFichaTecnicaDoMaterialTamanho(int produtoId, int materiaPrimaId);
    }
}
