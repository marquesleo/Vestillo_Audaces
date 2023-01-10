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
    public interface IBancoService : IService<Banco, BancoRepository, BancoController>
    {
        IEnumerable<Banco> GetPorNumBanco(String numBanco);

        IEnumerable<Banco> GetPorDescricao(String desc);

        IEnumerable<Banco> GetByIdList(int id);

        IEnumerable<Banco> GetAllAtivos();

        IEnumerable<Banco> GetAllParaBoleto();

        Banco GetPadraoVenda();


    }
}
