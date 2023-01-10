using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class EnderecoController: GenericController<Endereco, EnderecoRepository>
    {
        public IEnumerable<Endereco> GetByEmpresaId(int empresaId)
        {
            using (EnderecoRepository repository = new EnderecoRepository())
            {
                return repository.GetByEmpresaId(empresaId);
            }
        }
    }
}
