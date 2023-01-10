using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class PessoasPremioController : GenericController<PessoasPremio, PessoasPremioRepository>
    {
        public IEnumerable<PessoasPremio> GetByPessoaId(int pessoaId)
        {
            using (var repository = new PessoasPremioRepository())
            {
                return repository.GetByPessoaId(pessoaId);
            }
        }
    }
}
