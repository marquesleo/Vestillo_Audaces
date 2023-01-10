using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class NfceServiceWeb : GenericServiceWeb<Nfce, NfceRepository, NfceController>, INfceService
    {

        private string modificaWhere = "";

        public NfceServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<NfceView> GetCamposDeterminados(string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NfceView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {

            throw new NotImplementedException();
        }

        public IEnumerable<NfceView> GetListPorObservacao(string observacao, string parametrosDaBusca)
        {

            throw new NotImplementedException();

        }

        public IEnumerable<NfceView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }


        public void Save(ref Nfce nfce, bool precoEstoque)
        {
            throw new NotImplementedException();
        }

        public void ExcluirContasReceber(int idNfce)
        {
            throw new NotImplementedException();
        }

        public void ExcluirTotaisCaixas(int idNfce)
        {
            throw new NotImplementedException();
        }

        public void AlterarStatusCredito(int idNfce)
        {
            throw new NotImplementedException();
        }
    }
}
