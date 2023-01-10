using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class OrdemProducaoServiceAPP : GenericServiceAPP<OrdemProducao, OrdemProducaoRepository, OrdemProducaoController>, IOrdemProducaoService
    {
        public OrdemProducaoServiceAPP()
            : base(new OrdemProducaoController())
        {

        }

        public OrdemProducaoView GetByIdView(int id)
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            return controller.GetByIdView(id);
        }

        public IEnumerable<OrdemProducaoView> GetAllView(int IdOp = 0)
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            return controller.GetAllView(IdOp);
        }


        public List<int> GetSemanas()
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            return controller.GetSemanas();
        }


        public IEnumerable<OrdemProducaoView> GetByRefView(string referencia, int IdColecao = 0)
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            return controller.GetByRefView(referencia,IdColecao);
        }


        public IEnumerable<OrdemProducao> GetAllByProduto(int produtoId, bool ExcluirRegisto)
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            return controller.GetAllByProduto(produtoId, ExcluirRegisto);
        }

        public void TrataOrdemAberto()
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            controller.TrataOrdemAberto();
        }

        public void EnviarParaCorte(bool cancelaEnvio, int ordemId)
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            controller.EnviarParaCorte(cancelaEnvio, ordemId);
        }

        public void UpdateObsMateriais(int ordemId, string observacao)
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            controller.UpdateObsMateriais(ordemId, observacao);
        }

        public IEnumerable<OrdemProducaoView> GetByItem(string referencia)
        {
            OrdemProducaoController controller = new OrdemProducaoController();
            return controller.GetByItem(referencia);
        }
    }
}
