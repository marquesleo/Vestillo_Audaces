using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;

namespace Vestillo.Business.Service
{
    public interface IFuncionarioService : IService<Funcionario, FuncionarioRepository, FuncionarioController>
    {
        IEnumerable<FuncionarioView> GetAllView();
        List<FuncionarioView> GetByIdView(List<int> id);
        IEnumerable<FuncionarioView> GetListPorCargo(string cargo);
        IEnumerable<FuncionarioView> GetListPorNome(string nome);
        FuncionarioView GetFuncPorCPF(string CPF);
        IEnumerable<Funcionario> GetByFuncionarioProducao(List<int> funcionariosIds);
        IEnumerable<Funcionario> GetByFuncionarioDataProducao(List<int> premiosIds, List<int> funcionariosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida);
        IEnumerable<Funcionario> GetByPremioDataProducao(List<int> premiosIds, DateTime daData, DateTime ateData, string ordenacao, bool premioPartida);
        IEnumerable<Funcionario> GetAllAtivo();
        IEnumerable<FuncionarioSimplificado> GetListComCargo();
        bool CpfRGExiste(string Cpf, string RG);
    }
}
