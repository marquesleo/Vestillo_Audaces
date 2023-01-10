using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class FichaTecnicaServiceAPP : GenericServiceAPP<FichaTecnica, FichaTecnicaRepository, FichaTecnicaController>, IFichaTecnicaService
    {
        public FichaTecnicaServiceAPP() : base(new FichaTecnicaController())
        {

        }

        public IEnumerable<FichaTecnicaView> GetAllView()
        {
            return controller.GetAllView();
        }

        public void Save(IEnumerable<FichaTecnica> fichas, IEnumerable<FichaTecnicaOperacao> operacoesExcluidas = null)
        {
            controller.Save(fichas, operacoesExcluidas);
        }

        public IEnumerable<FichaTecnicaView> GetByFiltros(int[] produtosIds, int[] operacoesIds, string titulo)
        {
            return controller.GetByFiltros(produtosIds, operacoesIds, titulo);
        }

        public FichaTecnica GetByProduto(int produtoId)
        {
            return controller.GetByProduto(produtoId);
        }


        public void Update(FichaTecnica ficha)
        {
            controller.Update(ficha);
        }


        public IEnumerable<FichaTecnicaRelatorio> GetAllViewByFiltro(Models.Views.FiltroFichaTecnica filtro)
        {
            return controller.GetAllViewByFiltro(filtro);
        }


        public IEnumerable<FichaTecnicaOperacaoView> GetOperacoes(int ficha)
        {
            return controller.GetOperacoes(ficha);
        }


        public IEnumerable<FichaTecnicaOperacaoProdutoView> GetByOperacoesProduto(int operacaoId)
        {
            return controller.GetByOperacoesProduto(operacaoId);
        }
    }
}



