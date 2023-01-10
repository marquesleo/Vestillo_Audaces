
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
    public interface IDestinosService : IService<Destinos, DestinosRepository, DestinosController>
    {
        IEnumerable<Destinos> GetByAtivos(int AtivoInativo);
        IEnumerable<Destinos> GetListPorDescricao(string desc);
        IEnumerable<Destinos> GetListPorReferencia(string Abreviatura);
        IEnumerable<Destinos> GetListById(int id);
    }
}



