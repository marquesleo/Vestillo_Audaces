using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Service;

namespace TemplateAudacesApi.Services
{
    public class UsuarioService
    {
     
        public Usuario RetornarUsuario(string usuario, string senha)
        {
            try
            {
                
                var service = new Vestillo.Business.Service.UsuarioService().GetServiceFactory();
                IEnumerable<Empresa> empresasUsuario = null;
                IEnumerable<ModuloSistema> modulosUsuario = null;

                var user = service.GetByLogin(usuario, ref empresasUsuario, ref modulosUsuario);
                
                           
                if (user != null && user.Senha==senha)
                    return user;
                 

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }


        private void Logar()
        {

            
            var service = new Vestillo.Business.Service.UsuarioService().GetServiceFactory();
            IEnumerable<Empresa> empresasUsuario = null;
            IEnumerable<ModuloSistema> modulosUsuario = null;

            var usuario = service.GetByLogin("User", ref empresasUsuario, ref modulosUsuario);

            Vestillo.Business.VestilloSession.UsuarioLogado = usuario;
            Vestillo.Business.VestilloSession.EmpresasAcesso = empresasUsuario;
            Vestillo.Business.VestilloSession.ModulosSistema = modulosUsuario;

        }
        private void ValidarLogon(int codigo)
        {

            UsuarioLogado ul = new UsuarioLogado();
            ul.Ip = Vestillo.Business.VestilloSession.Ip();
            ul.DataLogin = DateTime.Now;
            ul.Maquina = Vestillo.Business.VestilloSession.NomeComputador();
            ul.UsuarioId = Vestillo.Business.VestilloSession.UsuarioLogado.Id;

            int moduloLogado = 1;
            Vestillo.Business.VestilloSession.ModuloLogado = Vestillo.Business.VestilloSession.ModulosSistema.Where(x => x.Id == moduloLogado).FirstOrDefault();

            var us = new UsuarioModulosSistemaService().GetServiceFactory();
            us.UpdateModuloPadraoUsuario(ul.UsuarioId, moduloLogado);
            Vestillo.Business.VestilloSession.EmpresaAcessoDados = new EmpresaAcessoService().GetServiceFactory().GetAll();

            var serviceEmpresa = new EmpresaService().GetServiceFactory();
            Empresa empresaLogada = serviceEmpresa.GetById(codigo);
            Vestillo.Business.VestilloSession.EmpresaLogada = empresaLogada;

        }


    }
}
