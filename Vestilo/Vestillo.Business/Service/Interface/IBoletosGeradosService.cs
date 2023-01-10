
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;

namespace Vestillo.Business.Service
{
    public interface IBoletosGeradosService : IService<BoletosGerados, BoletosGeradosRepository, BoletosGeradosController>
    {
        void Save(List<BoletosGerados> parcelas);
        BoletosGerados GetViewByIdTitulo(int idTitulo);
        void DeleteBoletoPeloTitulo(int idTitulo);
        BoletosGerados GetViewByNossoNumero(string NossoNumero);
        BoletosGerados GetViewByNumDocumento(string NumDocumento);
    }
}
