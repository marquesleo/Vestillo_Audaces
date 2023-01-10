using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class FuncionarioServiceAPP : GenericServiceAPP<Funcionario, FuncionarioRepository, FuncionarioController>, IFuncionarioService
    {
        public FuncionarioServiceAPP()
            : base(new FuncionarioController())
        {

        }

        public bool CpfRGExiste(string Cpf, string RG)
        {
            return controller.CpfRGExiste(Cpf, RG);
        }

        public IEnumerable<FuncionarioView> GetAllView()
        {
            return controller.GetAllView();
        }

        public List<FuncionarioView> GetByIdView(List<int> id)
        {
            return controller.GetByIdView(id);
        }

        public IEnumerable<FuncionarioView> GetListPorCargo(string cargo)
        {
            return controller.GetListPorCargo(cargo);
        }

        public IEnumerable<FuncionarioView> GetListPorNome(string nome)
        {
            return controller.GetListPorNome(nome);
        }

        public FuncionarioView GetFuncPorCPF(string CPF)
        {
            return controller.GetFuncPorCPF(CPF);
        }

        public IEnumerable<Funcionario> GetByFuncionarioProducao(List<int> funcionariosIds)
        {
            return controller.GetByFuncionarioProducao(funcionariosIds);
        }


        public IEnumerable<Funcionario> GetByFuncionarioDataProducao(List<int> premiosIds, List<int> funcionariosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida)
        {
            return controller.GetByFuncionarioDataProducao(premiosIds, funcionariosIds, daData, ateData, ordenacao, premioPartida);
        }


        public IEnumerable<Funcionario> GetByPremioDataProducao(List<int> premiosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida)
        {
            return controller.GetByPremioDataProducao(premiosIds, daData, ateData, ordenacao, premioPartida);
        }


        public IEnumerable<Funcionario> GetAllAtivo()
        {
            return controller.GetAllAtivo();
        }


        public IEnumerable<FuncionarioSimplificado> GetListComCargo()
        {
            return controller.GetListComCargo();
        }
    }
}



