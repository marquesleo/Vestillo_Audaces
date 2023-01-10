
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
    public interface ISerieNfeService : IService<SerieNfe , SerieNfeRepository, SerieNfeController>
    {
        SerieNfe GetByNumeracao(int SerieNota);
    }
}


