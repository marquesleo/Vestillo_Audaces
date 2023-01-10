using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IOrdemProducaoService : IService<OrdemProducao, OrdemProducaoRepository, OrdemProducaoController>
    {
        OrdemProducaoView GetByIdView(int id);
        IEnumerable<OrdemProducaoView> GetAllView(int IdOp = 0);
        IEnumerable<OrdemProducaoView> GetByRefView(string referencia, int IdColecao = 0);
        IEnumerable<OrdemProducao> GetAllByProduto(int produtoId, bool ExcluirRegisto);
        List<int> GetSemanas();
        void TrataOrdemAberto();
        void EnviarParaCorte(bool cancelaEnvio, int ordemId);
        void UpdateObsMateriais(int ordemId, string observacao);
        IEnumerable<OrdemProducaoView> GetByItem(string referencia);
    }
}
