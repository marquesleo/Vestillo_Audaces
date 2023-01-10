using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class OrdemProducaoServiceWeb : GenericServiceWeb<OrdemProducao, OrdemProducaoRepository, OrdemProducaoController>, IOrdemProducaoService
    {
        public OrdemProducaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public OrdemProducaoView GetByIdView(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdemProducaoView> GetAllView(int IdOp = 0)
        {
            throw new NotImplementedException();
        }


        public List<int> GetSemanas()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoView> GetByRefView(string referencia, int IdColecao = 0)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducao> GetAllByProduto(int produtoId, bool ExcluirRegisto)
        {
            throw new NotImplementedException();
        }

        public void TrataOrdemAberto()
        {
            throw new NotImplementedException();
        }

        public void EnviarParaCorte(bool cancelaEnvio, int ordemId)
        {
            throw new NotImplementedException();
        }

        public void UpdateObsMateriais(int ordemId, string observacao)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdemProducaoView> GetByItem(string referencia)
        {
            throw new NotImplementedException();
        }
    }
}
