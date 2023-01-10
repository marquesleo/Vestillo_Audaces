using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IServicoService : IService<Servico, ServicoRepository, ServicoController>
    {
        IEnumerable<Servico> GetPorReferencia(String referencia);

        IEnumerable<Servico> GetPorDescricao(String desc);

        IEnumerable<Servico> GetByIdList(int id);
    }
}
