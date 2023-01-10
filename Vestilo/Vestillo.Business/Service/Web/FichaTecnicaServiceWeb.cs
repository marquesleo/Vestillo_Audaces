using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class FichaTecnicaServiceWeb : GenericServiceWeb<FichaTecnica, FichaTecnicaRepository, FichaTecnicaController>, IFichaTecnicaService
    {

        public FichaTecnicaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FichaTecnicaView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<FichaTecnica> fichas, IEnumerable<FichaTecnicaOperacao> operacoesExcluidas = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaTecnicaView> GetByFiltros(int[] produtosIds, int[] operacoesIds, string titulo)
        {
            throw new NotImplementedException();
        }

        public FichaTecnica GetByProduto(int produtoId)
        {
            throw new NotImplementedException();
        }


        public void Update(FichaTecnica ficha)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FichaTecnicaRelatorio> GetAllViewByFiltro(Models.Views.FiltroFichaTecnica filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FichaTecnicaOperacaoView> GetOperacoes(int ficha)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FichaTecnicaOperacaoProdutoView> GetByOperacoesProduto(int operacaoId)
        {
            throw new NotImplementedException();
        }
    }
}


