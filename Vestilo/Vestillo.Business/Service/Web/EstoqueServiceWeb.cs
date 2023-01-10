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
    public class EstoqueServiceWeb : GenericServiceWeb<Estoque, EstoqueRepository, EstoqueController>, IEstoqueService
    {

        public EstoqueServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ConsultaEstoqueView> GetEstoque()
        {
            var c = new ConnectionWebAPI<ConsultaEstoqueView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri);
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int estoqueId)
        {
            var c = new ConnectionWebAPI<ConsultaMovimentacaoEstoqueView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri);
        }

        public ConsultaEstoqueView GetSaldoAtualProduto(int almoxarifadoId, int produtoId, int corId, int tamanhoId)
        {
            var c = new ConnectionWebAPI<ConsultaEstoqueView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "almoxarifadoId=" + almoxarifadoId.ToString() + "&produtoId=" + produtoId.ToString() + "&corId=" + corId.ToString() + "&tamanhoId=" + tamanhoId.ToString());
        }

        public void MovimentarEstoque(List<MovimentacaoEstoque> lstMovimentacao, bool conjunto)
        {
        
        }

        public void TransferirEstoque(List<MovimentacaoEstoque> lstMovimentacao, int almoxarifadoDestinoId)
        {
        
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int almoxarifadoId, string codigoBarras, int produtoId)
        {
            return null;
        }


        public IEnumerable<ConsultaEstoqueProdutoroduzidoView> GetEstoque(Models.Views.FichaEstoqueProdutoProduzido filtro)
        {
            throw new NotImplementedException();
        }

        public ConsultaEstoqueView GetEmpenhoAtualProduto(int almoxerifadoId, int produtoId, int corId, int tamanhoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Models.Views.ConsultaEstoqueMateriaPrima> GetEstoqueMateriaPrima(Models.Views.FichaEstoqueMateriaPrima filtro)
        {
            throw new NotImplementedException();
        }

        public void MovimentarEstoque(List<MovimentacaoEstoque> lstMovimentacao)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ConsultaEstoqueView> GetEstoqueRelatorio(Models.Views.FiltroEstoqueRelatorio filtro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConsultaEstoqueRelatorioView> GetConsultaEstoqueRelatorio(List<int> idEmpresas, int? tipoProduto, bool faturado, DateTime? daData, DateTime? ateData)
        {
            throw new NotImplementedException();
        }
    }
}
