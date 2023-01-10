using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IFichaTecnicaService : IService<FichaTecnica, FichaTecnicaRepository, FichaTecnicaController>
    {
        IEnumerable<FichaTecnicaView> GetAllView();
        void Save(IEnumerable<FichaTecnica> fichas, IEnumerable<FichaTecnicaOperacao> operacoesExcluidas = null);
        IEnumerable<FichaTecnicaView> GetByFiltros(int[] produtosIds, int[] operacoesIds, string titulo);
        FichaTecnica GetByProduto(int produtoId);
        IEnumerable<FichaTecnicaRelatorio> GetAllViewByFiltro(FiltroFichaTecnica filtro);
        IEnumerable<FichaTecnicaOperacaoView> GetOperacoes(int ficha);
        IEnumerable<FichaTecnicaOperacaoProdutoView> GetByOperacoesProduto(int operacaoId);
        void Update(FichaTecnica ficha);
    }
}
