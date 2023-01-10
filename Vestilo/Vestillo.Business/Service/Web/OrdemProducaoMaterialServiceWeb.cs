using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class OrdemProducaoMaterialServiceWeb: GenericServiceWeb<OrdemProducaoMaterial, OrdemProducaoMaterialRepository, OrdemProducaoMaterialController>, IOrdemProducaoMaterialService
    {
        public OrdemProducaoMaterialServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdemIdView(int ordemId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoMaterialEstoqueView> GetMaterialLiberacaoView()
        {
            throw new NotImplementedException();
        }


        public void LiberarEstoque(List<OrdemProducaoMaterialEstoqueView> ordemMateriaisEstoque)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Models.Views.CompraMaterialSemana> GetListCompraMaterialSemana(List<int> semanas)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Models.Views.CustoConsumo> GetCustoConsumo(Models.Views.FiltroCustoConsumo filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensIdView(int ordemId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoMaterialView> GetByOrdenEItem(int itemId, int ordemId)
        {
            throw new NotImplementedException();
        }


        public OrdemProducaoMaterialView GetEmpenhoLivreByOrdem(OrdemProducaoMaterialView ordem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListByItemComFichaTecnicaMaterial(List<int> idsIOP, int ordemId, bool agruparItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdemProducaoMaterialView> GetExcluir(int itemId, int ordemId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdemProducaoMaterial> GetByOrdemView(int ordemId, int materialId, int corId, int tamanhoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConsultaRelListaMateriaisView> GetListaMateriaisBaseadoOP(FiltroRelListaMateriais filtro)
        {
            throw new NotImplementedException();
        }
    }
}
