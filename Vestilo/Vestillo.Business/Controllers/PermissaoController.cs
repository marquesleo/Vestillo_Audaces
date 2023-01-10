using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class PermissaoController: GenericController<Permissao, PermissaoRepository>
    {
        public override Permissao GetById(int id)
        {
            using (PermissaoRepository repository = new PermissaoRepository())
            {
                var permisssao = base.GetById(id);
                using (PermissaoGrupoRepository grupoRepository = new PermissaoGrupoRepository())
                {
                    permisssao.Grupos = grupoRepository.GetByPermissaoId(permisssao.Id);
                }   
                return permisssao;
            }
        }

        public override void Save(ref Permissao permissao)
        {
            using (PermissaoRepository repository = new PermissaoRepository())
            {
                try
                {
                    Validate(permissao);

                    repository.BeginTransaction();

                    repository.Save(ref permissao);

                    using (PermissaoGrupoRepository grupoRepository = new PermissaoGrupoRepository())
                    {
                        var grupos = grupoRepository.GetByPermissaoId(permissao.Id);

                        foreach (var g in grupos)
                        {
                            grupoRepository.Delete(g.Id);
                        }

                        foreach (var gu in permissao.Grupos)
                        {
                            var g = gu;

                            g.Id = 0;
                            g.PermissaoId = permissao.Id;
                            grupoRepository.Save(ref g);
                        }
                    }


                    repository.CommitTransaction();
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        public IEnumerable<Permissao> GetByGrupos(string grupos)
        {
            using (PermissaoRepository repository = new PermissaoRepository())
            {
                return repository.GetByGrupos(grupos);
            }
        
        }

    }
}
