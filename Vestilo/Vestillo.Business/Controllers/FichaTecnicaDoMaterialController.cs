using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Controllers
{
    public class FichaTecnicaDoMaterialController : GenericController<FichaTecnicaDoMaterial, FichaTecnicaDoMaterialRepository>
    {
        public int UltimoCodigo = 0;

        public IEnumerable<FichaTecnicaDoMaterialView> GetAllView()
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                return repository.GetAllView();
            }
        }

        public IEnumerable<FichaTecnicaDoMaterial> GetAllView(List<int> lstProdutos)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                return repository.GetAllView(lstProdutos);
            }
        }

        
        public FichaTecnicaDoMaterial GetByProduto(int produtoId)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                return repository.GetByProduto(produtoId);
            }
        }

        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(FiltroCustoProdutoAnalitico filtro)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                return repository.GetAllViewByFiltro(filtro);
            }
        }

        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(FiltroCustoProdutoSintetico filtro)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                return repository.GetAllViewByFiltro(filtro);
            }
        }

        private Vestillo.Business.Repositories.FichaTecnicaDoMaterialItemRepository _fichaTecnicaDoMaterialRepositoryItem;
        private Vestillo.Business.Repositories.FichaTecnicaDoMaterialItemRepository fichaTecnicaDoMaterialRepositoryItem
        {
            get
            {
                if (_fichaTecnicaDoMaterialRepositoryItem == null)
                    _fichaTecnicaDoMaterialRepositoryItem = new Repositories.FichaTecnicaDoMaterialItemRepository();
                return _fichaTecnicaDoMaterialRepositoryItem;
            }
        }


        private Vestillo.Business.Repositories.FichaTecnicaDoMaterialRelacaoRepository _FichaTecnicaDoMaterialRelacaoRepository;
        private Vestillo.Business.Repositories.FichaTecnicaDoMaterialRelacaoRepository FichaTecnicaDoMaterialRelacaoRepository
        {
            get
            {
                if (_FichaTecnicaDoMaterialRelacaoRepository == null)
                    _FichaTecnicaDoMaterialRelacaoRepository = new Repositories.FichaTecnicaDoMaterialRelacaoRepository();
                return _FichaTecnicaDoMaterialRelacaoRepository;
            }
        }


        public void AlterarManutencao(List<MontagemDaFichaTecnicaDoMaterialView> lstMontagem)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                try
                {
                    repository.BeginTransaction();

                    foreach (var montagem in lstMontagem)
                    {

                        var fichaTecnicaItem = new FichaTecnicaDoMaterialItem();
                        fichaTecnicaItem.Id = montagem.getFichaDoMaterialItem.Id;
                        fichaTecnicaItem.FichaTecnicaId = montagem.fichaTecnicaMaterial.Id;
                        fichaTecnicaItem.MateriaPrimaId = montagem.getFichaDoMaterialItem.MateriaPrimaId;
                        fichaTecnicaItem.percentual_custo = montagem.percentual_custo;
                        fichaTecnicaItem.preco = montagem.Custo;
                        fichaTecnicaItem.valor = montagem.Valor;
                        fichaTecnicaItem.quantidade = montagem.Quantidade;
                        fichaTecnicaItem.sequencia = montagem.Sequencia;
                        fichaTecnicaItem.DestinoId = montagem.getFichaDoMaterialItem.DestinoId;
                        fichaTecnicaItem.excecao = montagem.getFichaDoMaterialItem.excecao;
                        fichaTecnicaDoMaterialRepositoryItem.Save(ref fichaTecnicaItem);

                    }

                    var lstCodigoDaFicha = (from obj in lstMontagem select obj.fichaTecnicaMaterial.Id).Distinct();

                    foreach (var codigoDaFicha in lstCodigoDaFicha)
                    {
                        FichaTecnicaDoMaterial fichaTecnicaDoMaterial = (from obj in lstMontagem
                                                                         where codigoDaFicha == obj.fichaTecnicaMaterial.Id
                                                                         select obj.fichaTecnicaMaterial).FirstOrDefault();



                        var lstItens = fichaTecnicaDoMaterialRepositoryItem.GetAllViewByFichaTecnica(codigoDaFicha);

                        fichaTecnicaDoMaterial.Total = (from obj in lstItens select obj.valor).Sum();
                        fichaTecnicaDoMaterial.DataAlteracao = DateTime.Now;
                        fichaTecnicaDoMaterial.UserId = VestilloSession.UsuarioLogado.Id;

                        repository.Save(ref fichaTecnicaDoMaterial);
                    }




                    repository.CommitTransaction();
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        public void IncluirFicha(List<MontagemDaFichaTecnicaDoMaterialView> lstMontagem)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                try
                {
                    repository.BeginTransaction();

                    var query = from m in lstMontagem
                                group m by m.getProduto.Id into g
                                select new { Id = g.Key, KeyCols = g.ToList() };

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            var Produto = new Produto();
                            Produto = lstMontagem.Find(p => p.getProduto.Id.Equals(item.Id)).getProduto;
                            
                            var ficha = new FichaTecnicaDoMaterial();
                            ficha.Ativo = true;
                            ficha.ProdutoId = item.Id;
                            ficha.Total = item.KeyCols.Sum(p => p.Valor);
                            //ficha.possuiquebra = false;
                            ficha.QuebraManual = item.KeyCols.Select(p => p.QuebraManual).FirstOrDefault();
                            ficha.possuiquebra = item.KeyCols.Select(p => p.QuebraDestino).FirstOrDefault();

                            base.Save(ref ficha);

                            
                            
                            UltimoCodigo = ficha.Id;                            

                            List<MontagemDaFichaTecnicaDoMaterialView> lst = item.KeyCols.ToList();
                            incluirItem(ficha, lst);
                        }

                    }
                    repository.CommitTransaction();

                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        public void AlterarFicha(List<MontagemDaFichaTecnicaDoMaterialView> lstMontagem)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                try
                {
                    repository.BeginTransaction();
                    var query = from m in lstMontagem
                                group m by m.getProduto.Id into g
                                select new { Name = g.Key, KeyCols = g.ToList() };




                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            var Produto = new Produto();
                            Produto = lstMontagem.Find(p => p.getProduto.Id.Equals(item.Name)).getProduto;
                            var ficha = new FichaTecnicaDoMaterial();
                            if (item.KeyCols[0].fichaTecnicaMaterial.Id > 0)
                                ficha.Ativo = item.KeyCols[0].fichaTecnicaMaterial.Ativo;
                            else
                                ficha.Ativo = true;
                            ficha.Id = item.KeyCols[0].fichaTecnicaMaterial.Id;
                            ficha.ProdutoId = item.Name;
                            ficha.Total = item.KeyCols.Sum(p => p.Valor);
                            if(ficha.Id > 0)
                            {
                                ficha.DataAlteracao = DateTime.Now;
                                ficha.UserId = VestilloSession.UsuarioLogado.Id;
                            }
                            //ficha.possuiquebra = item.KeyCols[0].fichaTecnicaMaterial.possuiquebra;
                            ficha.QuebraManual = item.KeyCols.Select(p => p.QuebraManual).FirstOrDefault();
                            ficha.possuiquebra = item.KeyCols.Select(p => p.QuebraDestino).FirstOrDefault();
                            Save(ref ficha);

                            fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(ficha.Id);

                            UltimoCodigo = ficha.Id;

                            List<MontagemDaFichaTecnicaDoMaterialView> lst = item.KeyCols.ToList();
                            incluirItem(ficha, lst);

                        }
                    }

                    repository.CommitTransaction();
                }
                catch (Exception ex)
                {

                    repository.RollbackTransaction();
                    throw ex;
                }
            }

        }

        private void incluirItem(FichaTecnicaDoMaterial ficha, List<MontagemDaFichaTecnicaDoMaterialView> lst)
        {

            IEnumerable<ProdutoDetalhe> gradeProduto = new ProdutoDetalheController().GetListByProduto(ficha.ProdutoId, 1);
            List<int> fichas = new List<int>();

            foreach (MontagemDaFichaTecnicaDoMaterialView itemDaFicha in lst)
            {
                try
                {


                    var fichaTecnicaItem = new FichaTecnicaDoMaterialItem();
                    fichaTecnicaItem.Id = 0;
                    fichaTecnicaItem.FichaTecnicaId = ficha.Id;
                    fichaTecnicaItem.MateriaPrimaId = itemDaFicha.MateriaPrimaId;
                    fichaTecnicaItem.percentual_custo = itemDaFicha.percentual_custo;
                    fichaTecnicaItem.preco = itemDaFicha.Custo;
                    fichaTecnicaItem.quantidade = itemDaFicha.Quantidade;
                    fichaTecnicaItem.sequencia = itemDaFicha.Sequencia;
                    fichaTecnicaItem.DestinoId = itemDaFicha.DestinoId;
                    fichaTecnicaItem.idFornecedor = itemDaFicha.idFornecedor;

                    fichaTecnicaItem.excecao = "N";

                    fichaTecnicaDoMaterialRepositoryItem.Save(ref fichaTecnicaItem);

                    ItemTabelaPrecoPCPRepository itemTabelaPrecoRepository = new ItemTabelaPrecoPCPRepository();
                    var itensTabPreco = itemTabelaPrecoRepository.GetListByProduto(ficha.ProdutoId);
                    foreach (var itp in itensTabPreco)
                    {
                        var itemTabelaPreco = itp;
                        itemTabelaPreco.CustoMaterial = ficha.Total;
                        itemTabelaPrecoRepository.Save(ref itemTabelaPreco);
                        TabelaPrecoPCPController tabelaPrecoController = new TabelaPrecoPCPController();
                        var tabela = tabelaPrecoController.GetById(itemTabelaPreco.TabelaPrecoPCPId);


                        CalcularItemTabela(itemTabelaPreco, tabela);

                        tabelaPrecoController.Save(ref tabela);
                    }

                    var lstRelacao = new List<FichaTecnicaDoMaterialRelacao>();
                    int i = 0;

                    if (fichas.Where(x => x == ficha.Id).Count() == 0)
                    {
                        FichaTecnicaDoMaterialRelacaoRepository.ExcluirRelacao(ficha.Id);
                        fichas.Add(ficha.Id);
                    }

                    IEnumerable<ProdutoDetalhe> gradeMateriaPrima = new ProdutoDetalheController().GetListByProduto(itemDaFicha.MateriaPrimaId, 1);

                    foreach (ProdutoDetalhe grade in gradeProduto)
                    {
                        var fichaRelacao = new FichaTecnicaDoMaterialRelacao();
                        fichaRelacao.FichaTecnicaId = ficha.Id;
                        fichaRelacao.MateriaPrimaId = itemDaFicha.MateriaPrimaId;
                        fichaRelacao.ProdutoId = itemDaFicha.getProduto.Id;
                        fichaRelacao.FichaTecnicaItemId = fichaTecnicaItem.Id;

                        int corProduto = grade.Idcor;
                        int corMateriaPrima;

                        MontagemFichaTecnicaDoMaterialCorView excecaoCor = itemDaFicha.lstMontagemFichaTecnicaDoMaterialCorView.Where(x => x.CorDoProduto == corProduto).FirstOrDefault();

                        if (excecaoCor != null)
                            corMateriaPrima = excecaoCor.CorDaMateriaPrima;
                        else
                            corMateriaPrima = corProduto;

                        int tamanhoProduto = grade.IdTamanho;
                        int tamanhoMateriaPrima;

                        MontagemFichaTecnicaDoMaterialTamanhoView excecaoTamanho = itemDaFicha.lstMontagemFichaTecnicaDoMaterialTamanhoView.Where(x => x.TamanhoDoProduto == tamanhoProduto).FirstOrDefault();

                        if (excecaoTamanho != null)
                            tamanhoMateriaPrima = excecaoTamanho.TamanhoDaMateriaPrima;
                        else
                            tamanhoMateriaPrima = tamanhoProduto;

                        fichaRelacao.Cor_Produto_Id = corProduto;
                        fichaRelacao.cor_materiaprima_Id = corMateriaPrima;
                        fichaRelacao.Tamanho_Produto_Id = tamanhoProduto;
                        fichaRelacao.Tamanho_Materiaprima_Id = tamanhoMateriaPrima;

                        FichaTecnicaDoMaterialRelacao relacao = fichaRelacao;

                        //if ((tamanhoMateriaPrima != tamanhoProduto) || (corMateriaPrima != corProduto))
                            FichaTecnicaDoMaterialRelacaoRepository.Save(ref relacao);
                    }


                    //foreach (var itemRelacao in itemDaFicha.lstMontagemFichaTecnicaDoMaterialCorView)
                    //{
                    //        var fichaRelacao = new FichaTecnicaDoMaterialRelacao();
                    //        bool achou = false;
                    //        while (!achou)
                    //        {

                    //            if (itemDaFicha.lstMontagemFichaTecnicaDoMaterialTamanhoView.Count() > 0)
                    //            {
                    //                var relacaoDeTamanho = itemDaFicha.lstMontagemFichaTecnicaDoMaterialTamanhoView.ToList()[i];
                    //                fichaRelacao.Tamanho_Materiaprima_Id = relacaoDeTamanho.TamanhoDaMateriaPrima;
                    //                fichaRelacao.Tamanho_Produto_Id = relacaoDeTamanho.TamanhoDoProduto;
                    //            }
                    //            achou = true;

                    //            if (i == itemDaFicha.lstMontagemFichaTecnicaDoMaterialTamanhoView.ToList().Count - 1)
                    //                i = 0;
                    //            else
                    //                i += 1;

                    //        }

                    //         fichaRelacao.FichaTecnicaId = ficha.Id;
                    //         fichaRelacao.MateriaPrimaId = itemDaFicha.MateriaPrimaId;
                    //         fichaRelacao.ProdutoId = itemDaFicha.getProduto.Id;
                    //         fichaRelacao.Cor_Produto_Id = itemRelacao.CorDoProduto;
                    //         fichaRelacao.cor_materiaprima_Id = itemRelacao.CorDaMateriaPrima;
                    //         FichaTecnicaDoMaterialRelacao relacao = fichaRelacao;

                    //         FichaTecnicaDoMaterialRelacaoRepository.Save(ref relacao);

                    // }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
            decimal percentualLucro = (1 - (tabela.Lucro/100) - encargosEImpostos).ToRound(4);

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

        public override void Delete(int FichaTecnicaID)
        {
            using (FichaTecnicaDoMaterialRepository repository = new FichaTecnicaDoMaterialRepository())
            {
                try
                {
                    //se não tiver ficha de operações, apaga também a observação do produto                    
                    var fichaMaterial = new FichaTecnicaDoMaterialRepository().GetById(FichaTecnicaID);
                    var ficha = new FichaTecnicaRepository().GetByProduto(fichaMaterial.ProdutoId);

                    if (ficha == null)
                    {
                        ObservacaoProdutoRepository observacaorepository = new ObservacaoProdutoRepository();
                        var obs = observacaorepository.GetByProduto(fichaMaterial.ProdutoId);
                        if (obs != null)
                            observacaorepository.Delete(obs.Id);
                    }

                    repository.BeginTransaction();
                    FichaTecnicaDoMaterialRelacaoRepository.ExcluirRelacao(FichaTecnicaID);
                    fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(FichaTecnicaID);
                    repository.Delete(FichaTecnicaID);
                    repository.CommitTransaction();
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        public IEnumerable<FichaTecnicaDoMaterialView> GetAllViewByFiltro(FiltroFichaTecnica filtro)
        {
            return _repository.GetAllViewByFiltro(filtro);
        }

    }
}
