using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ServicoController : GenericController<Servico, ServicoRepository>
    {
        public IEnumerable<Servico> GetPorReferencia(string referencia)
        {
            using (ServicoRepository repository = new ServicoRepository())
            {
                return repository.GetPorReferencia(referencia);
            }
        }

        public IEnumerable<Servico> GetPorDescricao(string desc)
        {
            using (ServicoRepository repository = new ServicoRepository())
            {
                return repository.GetPorDescricao(desc);
            }
        }

        public IEnumerable<Servico> GetByIdList(int id)
        {
            using (ServicoRepository repository = new ServicoRepository())
            {
                return repository.GetByIdList(id);
            }
        }
    }
}
