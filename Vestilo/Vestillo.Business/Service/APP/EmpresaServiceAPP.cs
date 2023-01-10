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
    public class EmpresaServiceAPP : GenericServiceAPP<Empresa, EmpresaRepository, EmpresaController>, IEmpresaService
    {

        public EmpresaServiceAPP()
            : base(new EmpresaController())
        {
        }

        public IEnumerable<Empresa> GetByUsuarioId(int usuarioId)
        {
            return controller.GetByUsuarioId(usuarioId);
        }


        public List<ProducaoEmpresa> GetByDataProducao(DateTime daData, DateTime ateData)
        {
            return controller.GetByDataProducao(daData, ateData);
        }


        public PercentuaisEmpresaAuto GetByProducaoEmpresa()
        {
            return controller.GetByProducaoEmpresa();
        }

        public IEnumerable<Empresa> GetBySelecao()
        {  
             return controller.GetBySelecao();           
        }

        public Endereco GetEndereco(int IdEmpresa)
        {
            return controller.GetEndereco(IdEmpresa);
        }
    }
}
