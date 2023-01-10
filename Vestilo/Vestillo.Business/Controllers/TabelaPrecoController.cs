using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class TabelaPrecoController: GenericController<TabelaPreco, TabelaPrecoRepository>
    {
        public override void Save(ref TabelaPreco tabelaPreco)
        {
            var repository = new ItemTabelaPrecoRepository();
            bool openTransaction = false;
            
            try
            {
                openTransaction = repository.BeginTransaction();
                VestilloSession.UsandoBanco = true;
                base.Save(ref tabelaPreco);


                var ctItem = new ItemTabelaPrecoController();
                ctItem.DeleteByTabelaPreco(tabelaPreco.Id);

                foreach (var item in tabelaPreco.Itens)
                {
                    var itp = item;
                    itp.Id = 0;
                    itp.TabelaPrecoId = tabelaPreco.Id;
                    ctItem.Save(ref itp);
                }

                if (VestilloSession.UsaTabelaPrecoBase)
                {
                    VerificarFilhos(tabelaPreco, ctItem);
                }

                VestilloSession.UsandoBanco = false;
                if (openTransaction)
                    repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                VestilloSession.UsandoBanco = false;
                if (openTransaction)
                    repository.RollbackTransaction();
                
                throw ex;
            }
            finally
            {
                try
                {
                    repository.Dispose();
                }
                finally { }
            }
        }

        private static void VerificarFilhos(TabelaPreco tabelaPreco, ItemTabelaPrecoController ctItem)
        {
            var tabelasFilho = new TabelaPrecoRepository().GetListPorTabelaBase(tabelaPreco.Id).ToList();
            if (tabelasFilho != null && tabelasFilho.Count > 0)
            {
                foreach (var tabelaFilho in tabelasFilho)
                {
                    tabelaFilho.Itens = new List<ItemTabelaPreco>();

                    int casasDecimais = 0;
                    switch (tabelaFilho.MetodoArredondamento)
                    {
                        case 0:
                            casasDecimais = 15;
                            break;
                        case 1:
                            casasDecimais = 0;
                            break;
                        case 2:
                            casasDecimais = 1;
                            break;
                    }

                    var arredondamento = tabelaFilho.MetodoArredondamento;
                    foreach (var item in tabelaPreco.Itens)
                    {
                        var itp = new ItemTabelaPreco();
                        itp.ProdutoId = item.ProdutoId ;
                        itp.PrecoSugerido = item.PrecoSugerido;
                        itp.CustoMedio = item.CustoMedio;
                        itp.Lucro = item.Lucro;
                        itp.Id = 0;
                        itp.PrecoVenda = decimal.Round((item.PrecoVenda * tabelaFilho.Fator), casasDecimais, MidpointRounding.AwayFromZero);
                        itp.TabelaPrecoId = tabelaFilho.Id;
                        ctItem.DeleteByTabelaPrecoEProduto(tabelaFilho.Id, item.ProdutoId);
                        ctItem.Save(ref itp);
                        tabelaFilho.Itens.Add(itp);
                    }

                    VerificarFilhos(tabelaFilho, ctItem);
                }
            }
        }

        public TabelaPreco GetByReferencia(string referencia)
        {
            using (var repository = new TabelaPrecoRepository())
            {
                return repository.GetByReferencia(referencia);
            }
        }

        public IEnumerable<TabelaPreco> GetListPorReferencia(string referencia)
        {
            using (var repository = new TabelaPrecoRepository())
            {
                return repository.GetListPorReferencia(referencia);
            }
        }

        public IEnumerable<TabelaPreco> GetListPorDescricao(string desc)
        {
            using (var repository = new TabelaPrecoRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<TabelaPreco> GetAllView()
        {
            using (var repository = new TabelaPrecoRepository())
            {
                return repository.GetAllView();
            }
        }

        public void CalcularCustos(ref ItemTabelaPrecoView item, decimal encargosEImpostos, decimal margemLucro)
        {
            try
            {
                Produto produto = new ProdutoController().GetById(item.ProdutoId);


                decimal totalImpostos = produto.Ipi + produto.Icms;

                encargosEImpostos = (encargosEImpostos + totalImpostos) / 100;
                margemLucro = margemLucro / 100;

                //=========================================================================================
                //Custo Médido
                //=========================================================================================
                ProdutoFornecedorPrecoController produtoFornecedorPrecoController = new ProdutoFornecedorPrecoController();
                IEnumerable<ProdutoFornecedorPreco> fornecedoresProduto = produtoFornecedorPrecoController.GetListByProdutoFornecedor(item.ProdutoId);
                decimal precoFornecedor = 0;
                decimal custoMedioProduto = 0;

                if (fornecedoresProduto != null && fornecedoresProduto.Count() > 0)
                {
                    int tipoCaluloCusto = VestilloSession.TipoCalculoCustoFornecedor;

                    if (produto.TipoCalculoPreco != null && produto.TipoCalculoPreco != 0)
                        tipoCaluloCusto = int.Parse(produto.TipoCalculoPreco.ToString());

                    //#ALEX
                    if (tipoCaluloCusto == 2) //Pega a media
                    {
                        if (produto.TipoCustoFornecedor == 2)// Cor
                        {
                            int count = fornecedoresProduto.Where(x => x.PrecoCor > 0).ToList().Count();
                            if (count == 0) count = 1;
                            precoFornecedor = (fornecedoresProduto.Sum(x => x.PrecoCor) / count);
                        }
                        else if (produto.TipoCustoFornecedor == 3)// Tamanho
                        {
                            int count = fornecedoresProduto.Where(x => x.PrecoTamanho > 0).ToList().Count();
                            if (count == 0) count = 1;
                            precoFornecedor = (fornecedoresProduto.Sum(x => x.PrecoTamanho) / count);
                        }
                        else // Fornecedor
                        {
                            int count = fornecedoresProduto.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                            if (count == 0) count = 1;
                            precoFornecedor = (fornecedoresProduto.Sum(x => x.PrecoFornecedor) / count);
                        }
                    }
                    else // Pega  o maior valor
                    {
                        if (produto.TipoCustoFornecedor == 2)// Cor
                            precoFornecedor = fornecedoresProduto.Max(x => x.PrecoCor);
                        else if (produto.TipoCustoFornecedor == 3)// Tamanho
                            precoFornecedor = fornecedoresProduto.Max(x => x.PrecoTamanho);
                        else
                            precoFornecedor = fornecedoresProduto.Max(x => x.PrecoFornecedor);
                    }

                    custoMedioProduto = precoFornecedor;
                }
                else
                {
                    //Como Calcular Custo médio aqui
                    custoMedioProduto = produto.Custo;
                }
                //=========================================================================================

                decimal percentualLucro = (1 - margemLucro - encargosEImpostos);
                decimal custoMedio = custoMedioProduto + (custoMedioProduto * encargosEImpostos); 

                //=========================================================================================
                //Formação de Preço
                //=========================================================================================
                item.CustoMedio =  custoMedio;
                item.PrecoSugerido = custoMedio / percentualLucro;


                if (item.PrecoVenda == 0)
                {
                    item.PrecoVenda = item.PrecoSugerido;
                }

                if (item.PrecoVenda == 0)
                {
                    item.Lucro = -1;                
                }
                else
                {
                    decimal percAux = custoMedio / item.PrecoVenda;
                    decimal lucroMedio = item.PrecoVenda * (1 - percAux - encargosEImpostos);

                    if (lucroMedio > 0)
                    {
                        item.Lucro = lucroMedio / item.PrecoVenda;
                    }
                    else
                    {
                        item.Lucro = lucroMedio / custoMedio;
                    }
                }

                //Formatação de casas decimais
                item.CustoMedio = decimal.Round(item.CustoMedio, VestilloSession.QtdCasasPreco);
                item.Lucro = decimal.Round(item.Lucro * 100, VestilloSession.QtdCasasPreco);
                item.PrecoSugerido = decimal.Round(item.PrecoSugerido, VestilloSession.QtdCasasPreco);
                item.PrecoVenda = decimal.Round(item.PrecoVenda, VestilloSession.QtdCasasPreco); 
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}
