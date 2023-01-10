
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
    public interface ICaixasService : IService<Caixas, CaixasRepository, CaixasController>
    {
        IEnumerable<Caixas> GetByAtivos(int AtivoInativo);
        IEnumerable<Caixas> GetListPorDescricao(string desc);
        IEnumerable<Caixas> GetListPorReferencia(string Abreviatura);
        IEnumerable<Caixas> GetListById(int id);
        IEnumerable<Caixas> GetAllTrataHoras();
        Caixas GetByIdTrataHoras(int id);
        
    }
}



