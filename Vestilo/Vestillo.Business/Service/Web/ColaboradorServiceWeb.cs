using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ColaboradorServiceWeb: GenericServiceWeb<Colaborador, ColaboradorRepository, ColaboradorController>, IColaboradorService
    {

        public ColaboradorServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Colaborador> GetPorReferencia(string referencia, String TipoColaborador)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> GetPorNome(string nome, String TipoColaborador)
        {
            throw new NotImplementedException();
        }

        public Colaborador GetByColaborador(int Id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Colaborador> GetAlgunsCampos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> GetAlgunsCamposPorTipo(string Tipo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> GetByIdList(int id, String TipoColaborador)
        {
            throw new NotImplementedException();
        }

        public bool CnpfCpfExiste(string CnpjCpf, int id)
        {
            throw new NotImplementedException();
        }
    }
}
