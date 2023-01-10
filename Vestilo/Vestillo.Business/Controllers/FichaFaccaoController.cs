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
    public class FichaFaccaoController : GenericController<FichaFaccao, FichaFaccaoRepository>
    {
        public override void Delete(int id)
        {
            bool openTransaction = false;
            try
            {
                openTransaction = _repository.BeginTransaction();
                base.Delete(id);

                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }

        public override void Save(ref FichaFaccao fichaFaccao)
        {
            base.Save(ref fichaFaccao);
        }

        public IEnumerable<FichaFaccao> VerificaFaccao(List<int> idPacote, List<int> idProduto, List<int> idFaccao)
        {
            using (var repository = new FichaFaccaoRepository())
            {
                return repository.VerificaFaccao(idPacote, idProduto, idFaccao);
            }
        }
        public IEnumerable<FichaFaccao> GetByIdFicha(int idFicha)
        {
            using (var repository = new FichaFaccaoRepository())
            {
                return repository.GetByIdFicha(idFicha);
            }
        }
        public IEnumerable<FichaFaccao> GetByIdFaccao(int idFaccao)
        {
            using (var repository = new FichaFaccaoRepository())
            {
                return repository.GetByIdFaccao(idFaccao);
            }
        }

        public IEnumerable<FichaFaccaoView> GetByIdFichaView(int idFicha)
        {
            using (var repository = new FichaFaccaoRepository())
            {
                return repository.GetByIdFichaView(idFicha);
            }
        }

    }
}