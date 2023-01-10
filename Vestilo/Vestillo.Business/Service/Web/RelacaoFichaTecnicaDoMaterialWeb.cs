using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;


namespace Vestillo.Business.Service.Web
{
    public class FichaTecnicaDoMaterialRelacaoWeb : GenericServiceWeb<FichaTecnicaDoMaterialRelacao, 
                                                                      FichaTecnicaDoMaterialRelacaoRepository, 
                                                                      FichaTecnicaDoMaterialRelacaoController>, 
                                                                      IFichaTecnicaDoMaterialRelacaoService
    {

        public FichaTecnicaDoMaterialRelacaoWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {
            throw new NotImplementedException();
        }


        public void ExcluirRelacao(int FichaTecnicaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGrade(ProdutoDetalheView grade)
        {
            throw new NotImplementedException();
        }

        public void UpdateFichaRelacao(FichaTecnicaDoMaterialRelacao ficha, int TipoItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaTecnicaDoMaterialRelacao> GetAllViewByGradeProduto(ProdutoDetalhe grade)
        {
            throw new NotImplementedException();
        }

        public List<MontagemFichaTecnicaDoMaterialCorView> getMontagemFichaTecnicaDoMaterialCor(int produtoId, int materiaPrimaId)
        {
            throw new NotImplementedException();
        }

        public List<MontagemFichaTecnicaDoMaterialTamanhoView> getMontagemFichaTecnicaDoMaterialTamanho(int produtoId, int materiaPrimaId)
        {
            throw new NotImplementedException();
        }
    }
}
