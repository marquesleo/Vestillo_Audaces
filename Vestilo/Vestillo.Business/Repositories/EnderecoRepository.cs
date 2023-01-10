using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class EnderecoRepository: GenericRepository<Endereco>
    {
        public EnderecoRepository() : base(new DapperConnection<Endereco>())
        {
        }

        public IEnumerable<Endereco> GetByEmpresaId(int empresaId)
        {
            return _cn.ExecuteToList(new Endereco(), "EmpresaId = " + empresaId.ToString());
        }
    }
}
