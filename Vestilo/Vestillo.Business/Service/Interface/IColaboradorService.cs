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
    public interface IColaboradorService : IService<Colaborador, ColaboradorRepository, ColaboradorController>
    {
        IEnumerable<Colaborador> GetPorReferencia(String referencia, String TipoColaborador);

        IEnumerable<Colaborador> GetPorNome(String nome, String TipoColaborador);

        Colaborador GetByColaborador(int Id);

        IEnumerable<Colaborador> GetAlgunsCampos();

        IEnumerable<Colaborador> GetByIdList(int id, String TipoColaborador);

        IEnumerable<Colaborador> GetAlgunsCamposPorTipo(string Tipo);

        bool CnpfCpfExiste(string CnpjCpf, int id);

        
    }
}
