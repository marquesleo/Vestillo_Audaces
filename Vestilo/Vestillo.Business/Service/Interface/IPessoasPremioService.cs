using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IPessoasPremioService : IService<PessoasPremio, PessoasPremioRepository, PessoasPremioController>
    {
        IEnumerable<PessoasPremio> GetByPessoaId(int pessoaId);
    }
}
