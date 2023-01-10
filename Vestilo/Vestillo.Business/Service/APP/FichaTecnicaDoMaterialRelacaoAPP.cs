using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class FichaTecnicaDoMaterialRelacaoAPP : GenericServiceAPP<FichaTecnicaDoMaterialRelacao, 
                                                                      FichaTecnicaDoMaterialRelacaoRepository, 
                                                                      FichaTecnicaDoMaterialRelacaoController>, IFichaTecnicaDoMaterialRelacaoService
    {

        public FichaTecnicaDoMaterialRelacaoAPP()
            : base(new FichaTecnicaDoMaterialRelacaoController())
        {
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {
            return controller.GetAllViewByFichaTecnica(FichaTecnicaId);
        }


        public void ExcluirRelacao(int FichaTecnicaId)
        {
            controller.ExcluirRelacao(FichaTecnicaId);
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGrade(ProdutoDetalheView grade)
        {
            return controller.GetAllViewByGrade(grade);
        }

        public void UpdateFichaRelacao(FichaTecnicaDoMaterialRelacao ficha, int TipoItem)
        {
            controller.UpdateFichaRelacao(ficha, TipoItem);
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGradeProduto(ProdutoDetalhe grade)
        {
            return controller.GetAllViewByGradeProduto(grade);
        }

        public List<MontagemFichaTecnicaDoMaterialCorView> getMontagemFichaTecnicaDoMaterialCor(int produtoId, int materiaPrimaId)
        {
            return controller.getMontagemFichaTecnicaDoMaterialCor(produtoId, materiaPrimaId);
        }

        public List<MontagemFichaTecnicaDoMaterialTamanhoView> getMontagemFichaTecnicaDoMaterialTamanho(int produtoId, int materiaPrimaId)
        {
            return controller.getMontagemFichaTecnicaDoMaterialTamanho(produtoId, materiaPrimaId);
        }
    }
}
