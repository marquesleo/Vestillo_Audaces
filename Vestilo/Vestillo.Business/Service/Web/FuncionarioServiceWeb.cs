using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class FuncionarioServiceWeb : GenericServiceWeb<Funcionario, FuncionarioRepository, FuncionarioController>, IFuncionarioService
    {

        public FuncionarioServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public bool CpfRGExiste(string Cpf, string RG)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FuncionarioView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public List<FuncionarioView> GetByIdView(List<int> id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FuncionarioView> GetListPorCargo(string cargo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FuncionarioView> GetListPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public FuncionarioView GetFuncPorCPF(string CPF)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Funcionario> GetByFuncionarioProducao(List<int> funcionariosIds)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Funcionario> GetByFuncionarioDataProducao(List<int> premiosIds, List<int> funcionariosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Funcionario> GetByPremioDataProducao(List<int> premiosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Funcionario> GetAllAtivo()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FuncionarioSimplificado> GetListComCargo()
        {
            throw new NotImplementedException();
        }
    }
}


