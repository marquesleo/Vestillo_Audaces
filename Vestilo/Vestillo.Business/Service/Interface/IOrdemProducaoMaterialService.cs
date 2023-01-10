using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IOrdemProducaoMaterialService : IService<OrdemProducaoMaterial, OrdemProducaoMaterialRepository, OrdemProducaoMaterialController>
    {
        IEnumerable<OrdemProducaoMaterialView> GetByOrdemIdView(int ordemId);
        IEnumerable<OrdemProducaoMaterialView> GetByOrdensIdView(int ordemId);
        IEnumerable<OrdemProducaoMaterialView> GetByOrdenEItem(int itemId, int ordemId);
        IEnumerable<OrdemProducaoMaterialView> GetExcluir(int itemId, int ordemId);
        OrdemProducaoMaterialView GetEmpenhoLivreByOrdem(OrdemProducaoMaterialView ordem);
        IEnumerable<OrdemProducaoMaterialView> GetListByItemComFichaTecnicaMaterial(List<int> idsIOP, int ordemId, bool agruparItem);
        IEnumerable<OrdemProducaoMaterialEstoqueView> GetMaterialLiberacaoView();
        void LiberarEstoque(List<OrdemProducaoMaterialEstoqueView> ordemMateriaisEstoque);
        IEnumerable<CompraMaterialSemana> GetListCompraMaterialSemana(List<int> semanas);
        IEnumerable<CustoConsumo> GetCustoConsumo(FiltroCustoConsumo filtro);
        IEnumerable<OrdemProducaoMaterial> GetByOrdemView(int ordemId, int materialId, int corId, int tamanhoId);
        IEnumerable<ConsultaRelListaMateriaisView> GetListaMateriaisBaseadoOP(FiltroRelListaMateriais filtro);
    }
}
