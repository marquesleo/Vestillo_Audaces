
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class AtividadeFaccaoController : GenericController<AtividadeFaccao, AtividadeFaccaoRepository>
    {


        public IEnumerable<AtividadeFaccao> GetByAtivos(int AtivoInativo)
        {
            using (AtividadeFaccaoRepository repository = new AtividadeFaccaoRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<AtividadeFaccao> GetListPorReferencia(string referencia)
        {
            using (AtividadeFaccaoRepository repository = new AtividadeFaccaoRepository())
            {
                return repository.GetListPorReferencia(referencia);
            }
        }

        public IEnumerable<AtividadeFaccao> GetListPorDescricao(string desc)
        {
            using (AtividadeFaccaoRepository repository = new AtividadeFaccaoRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<AtividadeFaccao> GetListById(int id)
        {
            using (AtividadeFaccaoRepository repository = new AtividadeFaccaoRepository())
            {
                return repository.GetListById(id);
            }
        }
        
    }
}
