using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class PessoasPremioServiceAPP: GenericServiceAPP<PessoasPremio, PessoasPremioRepository, PessoasPremioController>, IPessoasPremioService
    {
        public PessoasPremioServiceAPP()
            : base(new PessoasPremioController())
        {
        }

        public IEnumerable<PessoasPremio> GetByPessoaId(int pessoaId)
        {
            PessoasPremioController controller = new PessoasPremioController();
            return controller.GetByPessoaId(pessoaId);
        }
    }
}
