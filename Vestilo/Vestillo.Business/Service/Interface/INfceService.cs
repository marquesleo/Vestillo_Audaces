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
    public interface INfceService : IService<Nfce, NfceRepository, NfceController>
    {
        IEnumerable<NfceView> GetCamposDeterminados(string parametrosDaBusca);
        IEnumerable<NfceView> GetListPorReferencia(string referencia, string parametrosDaBusca);
        IEnumerable<NfceView> GetListPorObservacao(string observacao, string parametrosDaBusca);
        IEnumerable<NfceView> GetListPorNumero(string Numero, string parametrosDaBusca);
        void Save(ref Nfce nfce, bool precoEstoque);
        void ExcluirContasReceber(int idNfce);
        void ExcluirTotaisCaixas(int idNfce);
        void AlterarStatusCredito(int idNfce);
    }
}
