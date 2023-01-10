
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
    public interface ISetoresService : IService<Setores, SetoresRepository, SetoresController>
    {
        IEnumerable<Setores> GetByAtivos(int AtivoInativo);
        IEnumerable<Setores> GetBalanceamentos();
        IEnumerable<Setores> GetListPorDescricao(string desc);
        IEnumerable<Setores> GetListPorReferencia(string Abreviatura);
        IEnumerable<Setores> GetListById(int id);
        //balanceamento
        IEnumerable<Setores> GetByAtivosBalanceamento(int AtivoInativo);
        IEnumerable<Setores> GetListPorReferenciaBalanceamento(string Abreviatura);
        IEnumerable<Setores> GetListPorDescricaoBalanceamento(string desc);
        IEnumerable<Setores> GetListByIdBalanceamento(int id);
        




    }
}



