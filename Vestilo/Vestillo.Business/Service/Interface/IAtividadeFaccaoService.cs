
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
    public interface IAtividadeFaccaoService : IService<AtividadeFaccao, AtividadeFaccaoRepository, AtividadeFaccaoController>
    {
        IEnumerable<AtividadeFaccao> GetByAtivos(int AtivoInativo);
        IEnumerable<AtividadeFaccao> GetListPorDescricao(string desc);
        IEnumerable<AtividadeFaccao> GetListPorReferencia(string referencia);
        IEnumerable<AtividadeFaccao> GetListById(int id);
    }
}



