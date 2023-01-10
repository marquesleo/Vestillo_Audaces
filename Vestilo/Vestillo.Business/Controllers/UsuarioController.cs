using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class UsuarioController: GenericController<Usuario,  UsuarioRepository>
    {
        public Usuario GetByLogin(string login, ref IEnumerable<Empresa> empresas, ref IEnumerable<ModuloSistema> modulos)
        {
          
            using (UsuarioRepository repository = new UsuarioRepository())
            {
                try
                {
                    repository.BeginTransaction();

                    var usu = repository.GetByLogin(login);
                    if (usu != null)
                    {
                        using (UsuarioGrupoRepository grupoRepository = new UsuarioGrupoRepository())
                        {
                            usu.Grupos = grupoRepository.GetByUsuarioId(usu.Id);
                        }

                        using (UsuarioEmpresaRepository usuarioEmpresaRepository = new UsuarioEmpresaRepository())
                        {
                            usu.Empresas = usuarioEmpresaRepository.GetByUsuarioId(usu.Id);
                        }

                        using (EmpresaRepository empresaRepository = new EmpresaRepository())
                        {
                            empresas = empresaRepository.GetByUsuarioId(usu.Id);
                        }

                        using (ModuloSistemaRepository moduloRepository = new ModuloSistemaRepository())
                        {
                            modulos = moduloRepository.GetByUsuario(usu.Id);
                        }
                    }

                    repository.CommitTransaction();
                    return usu;
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw ex;
                }
            }
        }

        public override Usuario GetById(int id)
        {
            using (UsuarioRepository repository = new UsuarioRepository())
            {
                var usu = base.GetById(id);
                if (usu != null)
                {
                    using (UsuarioGrupoRepository grupoRepository = new UsuarioGrupoRepository())
                    {
                        usu.Grupos = grupoRepository.GetByUsuarioId(usu.Id);
                    }

                    using (UsuarioEmpresaRepository usuarioEmpresaRepository = new UsuarioEmpresaRepository())
                    {
                        usu.Empresas = usuarioEmpresaRepository.GetByUsuarioId(usu.Id);
                    }

                    using (UsuarioModulosSistemaRepository modulosSistemaRepository = new UsuarioModulosSistemaRepository())
                    {
                        usu.Modulos = modulosSistemaRepository.GetByUsuario(usu.Id);
                    }
                }
                return usu;
            }
        }

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
            catch(Exception ex)
            {
                grupoRepository.RollbackTransaction();
                throw new Vestillo.Lib.VestilloException(ex);
            }
        }

        public override void Save(ref Usuario usuario)
        {
            using (UsuarioRepository repository = new UsuarioRepository())
            {
                try
                {
                    Validate(usuario);

                    repository.BeginTransaction();

                    if (usuario.Id == 0)
                    {
                        usuario.DataStatus = DateTime.Now;
                        usuario.DataCadastro = DateTime.Now;
                    }
                    else
                    {
                        var usuarioOld = this.GetById(usuario.Id);
                        if (string.IsNullOrEmpty(usuario.Idioma))
                            usuario.Idioma = usuarioOld.Idioma;

                        if (usuario.Ativo != usuarioOld.Ativo)
                        {
                            usuario.DataStatus = DateTime.Now;
                        }
                        else
                        {
                            usuario.DataStatus = usuarioOld.DataStatus;
                        }
                    }
                    repository.Save(ref usuario);

                    using (UsuarioGrupoRepository grupoRepository = new UsuarioGrupoRepository())
                    {
                        var grupos = grupoRepository.GetByUsuarioId(usuario.Id);

                        foreach (var g in grupos)
                        {
                            grupoRepository.Delete(g.Id);
                        }

                        foreach (var gu in usuario.Grupos)
                        {
                            UsuarioGrupo g = gu;
                            g.Id = 0;
                            g.UsuarioId = usuario.Id;
                            grupoRepository.Save(ref g);
                        }
                    }


                    using (UsuarioModulosSistemaRepository usuarioModulosSistema = new UsuarioModulosSistemaRepository())
                    {
                        var usuarioModulos = usuarioModulosSistema.GetByUsuario(usuario.Id);

                        foreach (var m in usuarioModulos)
                        {
                            usuarioModulosSistema.Delete(m.Id);
                        }

                        foreach (var m in usuario.Modulos)
                        {
                            UsuarioModulosSistema mo = m;
                            mo.Id = 0;
                            mo.UsuarioId = usuario.Id;
                            usuarioModulosSistema.Save(ref mo);
                        }
                    }

                    using (UsuarioEmpresaRepository empresaRepository = new UsuarioEmpresaRepository())
                    {
                        empresaRepository.DeleteByUsuarioId(usuario.Id);

                        foreach (var e in usuario.Empresas)
                        {
                            var empresa = e;
                            empresa.Id = 0;
                            empresa.UsuarioId = usuario.Id;
                            empresaRepository.Save(ref empresa);
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

        public IEnumerable<Usuario> GetVendedores()
        {
            return _repository.GetVendedores();
        }
    }
}
