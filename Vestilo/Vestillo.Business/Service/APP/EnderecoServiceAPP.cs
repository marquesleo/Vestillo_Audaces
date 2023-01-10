using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class EnderecoServiceAPP : GenericServiceAPP<Endereco, EnderecoRepository, EnderecoController>, IEnderecoService
    {

        public EnderecoServiceAPP()
            : base(new EnderecoController())
        {
        }

        public IEnumerable<Endereco> GetByEmpresaId(int empresaId)
        {
            return controller.GetByEmpresaId(empresaId);
        }
    }
}
