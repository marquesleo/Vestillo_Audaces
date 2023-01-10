
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class UsuarioPortalController : GenericController<UsuarioPortal, UsuarioPortalRepository>
    {
                       

        public override void Delete(int id)
        {
            var grupoRepository = new UsuarioGrupoRepository();

            try
            {
                grupoRepository.BeginTransaction();


                var grupos = grupoRepository.GetByUsuarioId(id);

                foreach (var g in grupos)
                {
                    grupoRepository.Delete(g.Id);
                }

                var modulosRepository = new UsuarioModulosSistemaRepository();
                var modulos = grupoRepository.GetByUsuarioId(id);

                foreach (var m in modulos)
                {
                    modulosRepository.Delete(m.Id);
                }


                using (UsuarioEmpresaRepository empresaRepository = new UsuarioEmpresaRepository())
                {
                    empresaRepository.DeleteByUsuarioId(id);
                }

                base.Delete(id);

                grupoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                grupoRepository.RollbackTransaction();
                throw new Vestillo.Lib.VestilloException(ex);
            }
        }

       

        public IEnumerable<UsuarioPortal> GetVendedores()
        {
            return _repository.GetVendedores();
        }
    }
}
