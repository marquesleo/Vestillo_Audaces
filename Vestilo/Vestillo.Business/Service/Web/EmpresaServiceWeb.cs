using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class EmpresaServiceWeb : GenericServiceWeb<Empresa, EmpresaRepository, EmpresaController>, IEmpresaService
    {

        public EmpresaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Empresa> GetByUsuarioId(int usuarioId)
        {
            var c = new ConnectionWebAPI<Empresa>(VestilloSession.UrlWebAPI);
            return c.Get( this.RequestUri + "?usuarioId=" + usuarioId.ToString());
        }
        public List<ProducaoEmpresa> GetByDataProducao(DateTime daData, DateTime ateData)
        {
            throw new NotImplementedException();
        }


        public PercentuaisEmpresaAuto GetByProducaoEmpresa()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empresa> GetBySelecao()
        {
            throw new NotImplementedException();
        }

        public Endereco GetEndereco(int IdEmpresa)
        {
            throw new NotImplementedException(); 
        }


    }
}
