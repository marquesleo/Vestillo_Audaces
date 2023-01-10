
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IAdmCartaoService : IService<AdmCartao, AdmCartaoRepository, AdmCartaoController>
    {
        IEnumerable<AdmCartao> GetByAtivos(int AtivoInativo);

        IEnumerable<AdmCartao> GetPorReferencia(String referencia);

        IEnumerable<AdmCartao> GetPornome(String desc);

        IEnumerable<AdmCartao> GetByIdList(int id);
    }
}



