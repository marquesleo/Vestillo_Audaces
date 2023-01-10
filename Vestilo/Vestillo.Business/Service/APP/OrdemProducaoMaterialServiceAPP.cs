using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.APP
{
    public class OrdemProducaoMaterialServiceAPP : GenericServiceAPP<OrdemProducaoMaterial, OrdemProducaoMaterialRepository, OrdemProducaoMaterialController>, IOrdemProducaoMaterialService
    {
        public OrdemProducaoMaterialServiceAPP()
            : base(new OrdemProducaoMaterialController())
        {

        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdemIdView(int ordemId)
        {
            return controller.GetByOrdemIdView(ordemId);
        }


        public IEnumerable<OrdemProducaoMaterialEstoqueView> GetMaterialLiberacaoView()
        {
            return controller.GetMaterialLiberacaoView();
        }


        public void LiberarEstoque(List<OrdemProducaoMaterialEstoqueView> ordemMateriaisEstoque)
        {
            controller.LiberarEstoque(ordemMateriaisEstoque);
        }


        public IEnumerable<CompraMaterialSemana> GetListCompraMaterialSemana(List<int> semanas)
        {
            return controller.GetListCompraMaterialSemana(semanas);
        }


        public IEnumerable<CustoConsumo> GetCustoConsumo(FiltroCustoConsumo filtro)
        {
            return controller.GetCustoConsumo(filtro);
        }


        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensIdView(int ordemId)
        {
            return controller.GetByOrdensIdView(ordemId);
        }


        public IEnumerable<OrdemProducaoMaterialView> GetByOrdenEItem(int itemId, int ordemId)
        {
            return controller.GetByOrdenEItem(itemId, ordemId);
        }


        public OrdemProducaoMaterialView GetEmpenhoLivreByOrdem(OrdemProducaoMaterialView ordem)
        {
            return controller.GetEmpenhoLivreByOrdem( ordem );
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListByItemComFichaTecnicaMaterial(List<int> idsIOP, int ordemId, bool agruparItem)
        {
            return controller.GetListByItemComFichaTecnicaMaterial(idsIOP, ordemId, agruparItem);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetExcluir(int itemId, int ordemId)
        {
            return controller.GetExcluir(itemId, ordemId);
        }

        public IEnumerable<OrdemProducaoMaterial> GetByOrdemView(int ordemId, int materialId, int corId, int tamanhoId)
        {
            return controller.GetByOrdemView(ordemId, materialId, corId, tamanhoId);
        }

        public IEnumerable<ConsultaRelListaMateriaisView> GetListaMateriaisBaseadoOP(FiltroRelListaMateriais filtro)
        {
            return controller.GetListaMateriaisBaseadoOP(filtro);
        }
    }
}
