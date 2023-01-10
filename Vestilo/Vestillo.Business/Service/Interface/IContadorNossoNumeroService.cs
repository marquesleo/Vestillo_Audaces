
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
    public interface IContadorNossoNumeroService : IService<ContadorNossoNumero, ContadorNossoNumeroRepository, ContadorNossoNumeroController>
    {
        ContadorNossoNumero GetByBanco(int IdBanco);
        string GetProximo(int IdBanco);
    }
}
