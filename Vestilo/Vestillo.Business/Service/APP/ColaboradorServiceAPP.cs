using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class ColaboradorServiceAPP: GenericServiceAPP<Colaborador, ColaboradorRepository, ColaboradorController>, IColaboradorService
    {
        public ColaboradorServiceAPP()
            : base(new ColaboradorController())
        {
        }
        
        public IEnumerable<Colaborador> GetPorReferencia(string referencia, string tipoColaborador)
        {
            ColaboradorController controller = new ColaboradorController();
            return controller.GetPorReferencia(referencia, tipoColaborador);
        }

        public IEnumerable<Colaborador> GetPorNome(string nome,string tipoColaborador)
        {
            ColaboradorController controller = new ColaboradorController();
            return controller.GetPorNome(nome, tipoColaborador);
        }

        public Colaborador GetByColaborador(int Id)
        {
            return controller.GetByColaborador(Id);
        }

        public IEnumerable<Colaborador> GetAlgunsCampos()
        {
            return controller.GetAlgunsCampos();
        }

        public IEnumerable<Colaborador> GetByIdList(int id, String TipoColaborador)
        {
            return controller.GetByIdList(id, TipoColaborador);
        }


        public IEnumerable<Colaborador> GetAlgunsCamposPorTipo(string Tipo)
        {
            return controller.GetAlgunsCamposPorTipo(Tipo);
        }


        public bool CnpfCpfExiste(string CnpjCpf, int id)
        {
            return controller.CnpfCpfExiste(CnpjCpf,id);

        }

    }
}
