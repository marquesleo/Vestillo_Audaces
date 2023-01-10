using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class PlanoAnualController : GenericController<PlanoAnual, PlanoAnualRepository>
    {
        public override void Save(ref PlanoAnual plano)
        {
            _repository.BeginTransaction();
            try
            {
                PlanoAnualDetalhesRepository planoDetalheRepository = new PlanoAnualDetalhesRepository();
                base.Save(ref plano);
                var planoId = plano.Id;
                if (plano.planoDetalhes != null && plano.planoDetalhes.Count > 0)
                {
                    foreach(PlanoAnualDetalhes planoDetalhe in plano.planoDetalhes)
                        {
                            PlanoAnualDetalhes planoDetalheSave;
                            planoDetalheSave = planoDetalhe;
                            planoDetalheSave.PlanoId = planoId;
                            if (planoDetalheSave.GrupoId == 0)
                            {
                                planoDetalheRepository.Delete(planoDetalheSave.Id);
                            }
                            else
                            {
                                planoDetalheRepository.Save(ref planoDetalheSave);
                            }
                        };
                }
                _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                _repository.RollbackTransaction();
                throw ex;
            }
        }

        public override void Delete(int id)
        {
            bool openTransaction = false;

            var planoDetalhesRepository = new PlanoAnualDetalhesRepository();
          
            try
            {
                openTransaction = _repository.BeginTransaction();

                IEnumerable<PlanoAnualDetalhesView> itens = _repository.GetPlanoAnualDetalhes(id);
                foreach (PlanoAnualDetalhesView item in itens)
                {
                    planoDetalhesRepository.Delete(item.Id);
                }
                
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

        public List<PlanoAnualDetalhesView> GetPlanoAnualDetalhesTotal(int codigo)
        {
            return _repository.GetPlanoAnualDetalhesTotal(codigo);
        }

        public List<PlanoAnualDetalhesView> GetPlanoAnualDetalhes(int codigo)
        {
            return _repository.GetPlanoAnualDetalhes(codigo);
        }

        public List<GrupProduto> GetGrupos(int codigo)
        {
            return _repository.GetGrupos(codigo);
        }
    }
}
