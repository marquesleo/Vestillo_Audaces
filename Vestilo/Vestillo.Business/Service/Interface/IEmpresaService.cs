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
    public interface IEmpresaService : IService<Empresa, EmpresaRepository, EmpresaController>
    {
        IEnumerable<Empresa> GetByUsuarioId(int usuarioId);
        List<ProducaoEmpresa> GetByDataProducao(DateTime daData, DateTime ateData);
        PercentuaisEmpresaAuto GetByProducaoEmpresa();
        IEnumerable<Empresa> GetBySelecao();
        Endereco GetEndereco(int IdEmpresa);
        

    }
}
