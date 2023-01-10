using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace Vestillo.Business.Controllers
{
    public class FichaTecnicaDoMaterialItemController : GenericController<FichaTecnicaDoMaterialItem, FichaTecnicaDoMaterialItemRepository>
    {

        private FichaTecnicaDoMaterialItemService _fichaTecnicaDoMaterialItemService;
        private FichaTecnicaDoMaterialItemService fichaTecnicaDoMaterialItemService
        {
            get
            {
                if (_fichaTecnicaDoMaterialItemService == null)
                    _fichaTecnicaDoMaterialItemService = new FichaTecnicaDoMaterialItemService();
                return _fichaTecnicaDoMaterialItemService;
            }
        }

        private FichaTecnicaDoMaterialRelacaoService _fichaTecnicaDoMaterialRelacaoService;
        private FichaTecnicaDoMaterialRelacaoService fichaTecnicaDoMaterialRelacaoService
        {
            get
            {
                if (_fichaTecnicaDoMaterialRelacaoService == null)
                    _fichaTecnicaDoMaterialRelacaoService = new FichaTecnicaDoMaterialRelacaoService();
                return _fichaTecnicaDoMaterialRelacaoService;
            }
        }


        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {
            using (FichaTecnicaDoMaterialItemRepository repository = new FichaTecnicaDoMaterialItemRepository())
            {
                return repository.GetAllViewByFichaTecnica(FichaTecnicaId);
            }
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByProdutoId(int produtoId)
        {
            using (FichaTecnicaDoMaterialItemRepository repository = new FichaTecnicaDoMaterialItemRepository())
            {
                return repository.GetAllViewByProdutoId(produtoId);
            }
        }

        public void ExcluirRelacao(int FichaTecnicaId)
        {
            using (FichaTecnicaDoMaterialItemRepository repository = new FichaTecnicaDoMaterialItemRepository())
            {
                repository.ExcluirRelacao(FichaTecnicaId);
            }
        }


        public List<MontagemDaFichaTecnicaDoMaterialView> getMontagemDaFichaTecnicaDoMaterial(FichaTecnicaDoMaterial fichaTecnica)
        {
            IEnumerable<FichaTecnicaDoMaterialRelacao> lstFichaTecnicaDoMaterialRelacao = fichaTecnicaDoMaterialRelacaoService.GetServiceFactory().GetAllViewByFichaTecnica(fichaTecnica.Id);
            List<MontagemDaFichaTecnicaDoMaterialView> lstMontagemDaFichaTecnicaDoMaterial = new List<MontagemDaFichaTecnicaDoMaterialView>();

            if (fichaTecnica != null && fichaTecnica.Id > 0)
            {
                IEnumerable<FichaTecnicaDoMaterialItem> lstFichaTecnicaItem = new List<FichaTecnicaDoMaterialItem>();
                lstFichaTecnicaItem = fichaTecnicaDoMaterialItemService.GetServiceFactory().GetAllViewByFichaTecnica(fichaTecnica.Id);
                foreach (var item in lstFichaTecnicaItem)
                {

                    var montagem = new MontagemDaFichaTecnicaDoMaterialView(fichaTecnica.getProduto, item.getMateriaPrima, item);

                    montagem.fichaTecnicaMaterial = fichaTecnica;
                    var lstRelacaoCor = (from obj in lstFichaTecnicaDoMaterialRelacao where obj.ProdutoId == fichaTecnica.ProdutoId && obj.MateriaPrimaId == item.MateriaPrimaId && obj.FichaTecnicaItemId == item.Id select obj).ToList();
                    if (lstRelacaoCor != null)
                    {
                        foreach (var relacao in lstRelacaoCor)
                        {
                            var relacaoDeCor = new MontagemFichaTecnicaDoMaterialCorView(relacao);
                            var relacaoDeTamanho = new MontagemFichaTecnicaDoMaterialTamanhoView(relacao);                            
                            
                            relacaoDeCor.CorDaMateriaPrima = relacao.cor_materiaprima_Id;
                            relacaoDeCor.CorDoProduto = relacao.Cor_Produto_Id;

                            relacaoDeTamanho.TamanhoDaMateriaPrima = relacao.Tamanho_Materiaprima_Id;
                            relacaoDeTamanho.TamanhoDoProduto = relacao.Tamanho_Produto_Id;

                            //var materiaPrima = new ProdutoDetalheService().GetServiceFactory().GetByGrade(relacao.MateriaPrimaId, relacao.cor_materiaprima_Id, relacao.Tamanho_Materiaprima_Id);

                            /*if (materiaPrima != null)
                            {
                                if (materiaPrima.CorUnica)
                                    relacaoDeCor.CorUnicaDaMateriaPrima = materiaPrima.CorUnica;
                                else
                                    relacaoDeCor.CorUnicaDaMateriaPrima = new ProdutoDetalheService().GetServiceFactory().VerificarCorUnica(relacao.MateriaPrimaId, relacao.cor_materiaprima_Id);
                                
                                if (materiaPrima.TamanhoUnico)
                                    relacaoDeTamanho.TamanhoUnicoDaMateriaPrima = materiaPrima.TamanhoUnico;
                                else
                                    relacaoDeTamanho.TamanhoUnicoDaMateriaPrima = new ProdutoDetalheService().GetServiceFactory().VerificarTamanhoUnico(relacao.MateriaPrimaId, relacao.Tamanho_Materiaprima_Id);
                            }
                            else
                            {*/
                            relacaoDeCor.CorUnicaDaMateriaPrima = new ProdutoDetalheService().GetServiceFactory().VerificarCorUnica(relacao.MateriaPrimaId, relacao.cor_materiaprima_Id);
                            relacaoDeTamanho.TamanhoUnicoDaMateriaPrima = new ProdutoDetalheService().GetServiceFactory().VerificarTamanhoUnico(relacao.MateriaPrimaId, relacao.Tamanho_Materiaprima_Id);
                            //}

                            montagem.lstMontagemFichaTecnicaDoMaterialCorView.Add(relacaoDeCor);
                            montagem.lstMontagemFichaTecnicaDoMaterialTamanhoView.Add(relacaoDeTamanho);
                        }
                    }
                    montagem.MaterialQuebra = false;

                    lstMontagemDaFichaTecnicaDoMaterial.Add(montagem);
                }

            }
            return lstMontagemDaFichaTecnicaDoMaterial;
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByProdutos(int[] produtosIds)
        {
            return _repository.GetListByProdutos(produtosIds);
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByProduto(int produto)
        {
            return _repository.GetListByProduto(produto);
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByMateriaPrima(int materiaId)
        {
            return _repository.GetListByMateriaPrima(materiaId);
        }

        public void Update(ref FichaTecnicaDoMaterialItem item)
        {
            var repository = new FichaTecnicaDoMaterialRepository();
            try
            {
                repository.BeginTransaction();

                _repository.Save(ref item);

                var fichaTecnicaDoMaterial = repository.GetById(item.FichaTecnicaId);
                var lstItens = _repository.GetAllViewByFichaTecnica(fichaTecnicaDoMaterial.Id);

                fichaTecnicaDoMaterial.Total = (from obj in lstItens select obj.CustoCalculado).Sum();

                repository.Save(ref fichaTecnicaDoMaterial);

                //Atualizar Vestillo.Tabela de Preço
                ItemTabelaPrecoPCPRepository itemTabelaPrecoRepository = new ItemTabelaPrecoPCPRepository();                
                var itensTabPreco = itemTabelaPrecoRepository.GetListByProduto(fichaTecnicaDoMaterial.ProdutoId);
                foreach (var itp in itensTabPreco)
                {
                    var itemTabelaPreco = itp;
                    itemTabelaPreco.CustoMaterial = fichaTecnicaDoMaterial.Total;
                    itemTabelaPrecoRepository.Save(ref itemTabelaPreco);
                    TabelaPrecoPCPController tabelaPrecoController = new TabelaPrecoPCPController();
                    var tabela = tabelaPrecoController.GetById(itemTabelaPreco.TabelaPrecoPCPId);

                    CalcularItemTabela(itemTabelaPreco, tabela);

                    tabelaPrecoController.Save(ref tabela);
                }

                repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                throw new Vestillo.Lib.VestilloException(ex);
            }
        }

        private static void CalcularItemTabela(ItemTabelaPrecoPCP itemTabelaPreco, TabelaPrecoPCP tabela)
        {
            var item = tabela.Itens.Find(i => i.ProdutoId == itemTabelaPreco.ProdutoId);

            item.CustoPrevisto = item.CustoDespesaPrevisto + item.CustoMaterial + item.CustoMaoDeObra;
            item.CustoRealizado = item.CustoDespesaRealizado + item.CustoMaterial + item.CustoMaoDeObra;

            var produto = new ProdutoRepository().GetById(itemTabelaPreco.ProdutoId);

            decimal totalImpostos = produto.Ipi + produto.Icms;
            decimal encargosEImpostos = (tabela.Outros + tabela.Frete + tabela.Encargos + tabela.Comissao + totalImpostos) / 100;
            decimal percentualLucro = (1 - (tabela.Lucro / 100) - encargosEImpostos).ToRound(4);

            //CONFIRMAR
            item.PrecoSugeridoPrevisto = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaPrevisto) / (percentualLucro)).ToRound(4);
            item.PrecoSugeridoRealizado = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaRealizado) / (percentualLucro)).ToRound(4);

            if (item.PrecoSugeridoPrevisto != 0)
                item.PercentualLucroBaseCustoPrevisto = (item.PrecoVenda / item.PrecoSugeridoPrevisto).ToRound(4);
            else
                item.PercentualLucroBaseCustoPrevisto = 0;

            if (item.PrecoSugeridoRealizado != 0)
                item.PercentualLucroBaseCustoRealizado = (item.PrecoVenda / item.PrecoSugeridoRealizado).ToRound(4);
            else
                item.PercentualLucroBaseCustoRealizado = 0;

            item.LucroBaseCustoPrevisto = (item.PrecoVenda * item.PercentualLucroBaseCustoPrevisto).ToRound(4);
            item.LucroBaseCustoRealizado = (item.PrecoVenda * item.PercentualLucroBaseCustoRealizado).ToRound(4);

            item.LucroPrevisto = item.PrecoSugeridoPrevisto - item.CustoMaoDeObra - item.CustoMaterial - encargosEImpostos;// DUVIDA;
            item.LucroRealizado = item.PrecoSugeridoRealizado - item.CustoMaoDeObra - item.CustoMaterial - encargosEImpostos;// DUVIDA;
        }

    }
}
