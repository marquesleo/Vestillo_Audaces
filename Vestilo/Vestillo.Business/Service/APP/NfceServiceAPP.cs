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
    public class NfceServiceAPP: GenericServiceAPP<Nfce, NfceRepository, NfceController>, INfceService
    {
        public NfceServiceAPP()
            : base(new NfceController())
        {
        }

        public IEnumerable<NfceView> GetCamposDeterminados(string parametrosDaBusca)
        {
            return controller.GetCamposDeterminados(parametrosDaBusca);
        }

        public IEnumerable<NfceView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {

            return controller.GetListPorReferencia(referencia, parametrosDaBusca);

        }

        public IEnumerable<NfceView> GetListPorObservacao(string observacao, string parametrosDaBusca)
        {

            return controller.GetListPorObservacao(observacao, parametrosDaBusca);

        }

        public IEnumerable<NfceView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            return controller.GetListPorNumero(Numero, parametrosDaBusca);

        }

        public void Save(ref Nfce nfce, bool precoEstoque)
        {
            controller.Save(ref nfce, precoEstoque);
        }

        public void ExcluirContasReceber(int idNfce)
        {
            controller.ExcluirContasReceber(idNfce);
        }

        public void ExcluirTotaisCaixas(int idNfce)
        {
            controller.ExcluirTotaisCaixas(idNfce);
        }

        public void AlterarStatusCredito(int idNfce)
        {
            controller.AlterarStatusCredito(idNfce);
        }
    }
}
