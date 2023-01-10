
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;
using Vestillo.Core.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NotaEntradaController : GenericController<NotaEntrada, NotaEntradaRepository>
    {

        public IEnumerable<NotaEntradaView> GetCamposEspecificos(string parametrosDaBusca)
        {
            using (NotaEntradaRepository repository = new NotaEntradaRepository())
            {
                return repository.GetCamposEspecificos(parametrosDaBusca);
            }
        }



        public IEnumerable<NotaEntradaView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            using (NotaEntradaRepository repository = new NotaEntradaRepository())
            {
                return repository.GetListPorReferencia(referencia, parametrosDaBusca);
            }
        }

        public IEnumerable<NotaEntradaView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            using (NotaEntradaRepository repository = new NotaEntradaRepository())
            {
                return repository.GetListPorNumero(Numero, parametrosDaBusca);
            }
        }


        public IEnumerable<NotaEntradaView> GetListPorPedido(int pedidoId)
        {
            using (NotaEntradaRepository repository = new NotaEntradaRepository())
            {
                return repository.GetListPorPedido(pedidoId);
            }
        }

        public override void Save(ref NotaEntrada Nota)
        {
            using (NotaEntradaRepository repository = new NotaEntradaRepository())
            {
                try
                {
                    repository.BeginTransaction();

                    //PEGA A REFERENCIA
                    base.Save(ref Nota);

                    //grava os itens da nota
                    using (NotaEntradaItensRepository itensRepository = new NotaEntradaItensRepository())
                    {
                        var itens = itensRepository.GetListByNotaEntradaItens(Nota.Id);

                        foreach (var i in itens)
                        {
                            itensRepository.Delete(i.Id);
                        }

                        ProdutoFornecedorPrecoRepository produtoFornecedorRepository = new ProdutoFornecedorPrecoRepository();
                        var PossuiFornecedor = false;

                        foreach (var gr in Nota.ItensNota)
                        {
                            NotaEntradaItens  g = gr;
                            g.Id = 0;
                            g.IdNota  = Nota.Id;
                            itensRepository.Save(ref g);

                            if (Nota.AtualizaPreco)
                            {
                                var fornecedorId = Nota.IdColaborador;
                                var listfornecedores = produtoFornecedorRepository.GetListByProdutoFornecedor(g.iditem).ToList();
                                var produto = new ProdutoRepository().GetById(g.iditem);
                                var GradeProduto = new ProdutoDetalheRepository().GetListByProduto(g.iditem,0);
                                bool ManterMaiorPreco = false;
                                ProdutoFornecedorPreco fornecedor = new ProdutoFornecedorPreco();
                                ProdutoFornecedorPreco Nofornecedor = new ProdutoFornecedorPreco();

                                if (listfornecedores != null && listfornecedores.Count() > 0)
                                {
                                    Nofornecedor = listfornecedores.Find(f => f.IdFornecedor == fornecedorId);
                                    if(Nofornecedor == null)
                                    {
                                        PossuiFornecedor = false;
                                    }
                                    else
                                    {
                                        PossuiFornecedor = true;
                                    }
                                    

                                    fornecedor = listfornecedores.Find(f => f.IdFornecedor == fornecedorId && f.IdCor == g.idcor && f.IdTamanho == g.idtamanho);
                                    if (fornecedor == null)
                                    {
                                        fornecedor = new ProdutoFornecedorPreco();
                                        fornecedor.IdCor = g.idcor;
                                        fornecedor.IdTamanho = g.idtamanho;
                                        fornecedor.IdProduto = g.iditem;
                                        fornecedor.IdFornecedor = fornecedorId;
                                        listfornecedores.Add(fornecedor);
                                    }
                                    else
                                    {
                                        ManterMaiorPreco = VestilloSession.ManterMaiorPreco;
                                    }
                                }
                                else
                                {
                                    listfornecedores = new List<ProdutoFornecedorPreco>();
                                    fornecedor = new ProdutoFornecedorPreco();
                                    fornecedor.IdCor = g.idcor;
                                    fornecedor.IdTamanho = g.idtamanho;
                                    fornecedor.IdProduto = g.iditem;
                                    fornecedor.IdFornecedor = fornecedorId;
                                    listfornecedores.Add(fornecedor);
                                }

                                switch (produto.TipoCustoFornecedor)
                                {
                                    case 1:
                                        if (!(ManterMaiorPreco && fornecedor.PrecoFornecedor > g.Preco))
                                            fornecedor.PrecoFornecedor = g.Preco;
                                        break;
                                    case 2:
                                        if (!(ManterMaiorPreco && fornecedor.PrecoCor > g.Preco))
                                            fornecedor.PrecoCor = g.Preco;
                                        break;
                                    case 3:
                                        if (!(ManterMaiorPreco && fornecedor.PrecoTamanho > g.Preco))
                                            fornecedor.PrecoTamanho = g.Preco;
                                        break;
                                    default:
                                        break;
                                }

                                produtoFornecedorRepository.Save(ref fornecedor);

                                if(PossuiFornecedor == false)
                                {                                    
                                    foreach (var item in GradeProduto)
                                    {
                                        if(item.Idcor == g.idcor && item.IdTamanho == g.idtamanho)
                                        {

                                        }
                                        else
                                        {
                                            fornecedor = new ProdutoFornecedorPreco();
                                            fornecedor.IdCor = item.Idcor;
                                            fornecedor.IdTamanho = item.IdTamanho;
                                            fornecedor.IdProduto = item.IdProduto;
                                            fornecedor.IdFornecedor = fornecedorId;                                            
                                            listfornecedores.Add(fornecedor);

                                            produtoFornecedorRepository.Save(ref fornecedor);
                                        }
                                    }
                                }


                                //Atualizar Vestillo.Custo Material
                                if (produto.TipoItem == 1 || produto.TipoItem == 2)
                                {
                                    bool atualizar = false;

                                    /* comentado por ALEX 04/05/2021, sempre tem que atualizar
                                    var dif = listfornecedores.Find(p => p.IdFornecedor == fornecedor.IdFornecedor);
                                    if (dif != null)
                                    {
                                        if (dif.PrecoFornecedor != fornecedor.PrecoFornecedor || dif.PrecoCor != fornecedor.PrecoCor || dif.PrecoTamanho != fornecedor.PrecoTamanho)
                                        {
                                            atualizar = true;

                                        }
                                    }
                                    else
                                    {
                                        atualizar = true;

                                    }
                                    */
                                    atualizar = true;
                                    
                                    if (atualizar)
                                    {
                                        using (FichaTecnicaDoMaterialItemController fichaTecnicaMaterialItemController = new FichaTecnicaDoMaterialItemController())
                                        {
                                            var fichasItens = fichaTecnicaMaterialItemController.GetListByMateriaPrima(produto.Id);
                                            foreach (var fi in fichasItens)
                                            {
                                                var item = fi;
                                                item.preco = listfornecedores.FirstOrDefault().PrecoFornecedor;
                                                var ret = listfornecedores;
                                                if (produto.TipoCalculoPreco == 2) //Pega a media
                                                {
                                                    if (produto.TipoCustoFornecedor == 2)// Cor
                                                    {
                                                        int count = ret.Where(x => x.PrecoCor > 0).ToList().Count();
                                                        if (count == 0) count = 1;
                                                        item.preco = (ret.Sum(x => x.PrecoCor) / count);
                                                    }
                                                    else if (produto.TipoCustoFornecedor == 3)// Tamanho
                                                    {
                                                        int count = ret.Where(x => x.PrecoTamanho > 0).ToList().Count();
                                                        if (count == 0) count = 1;
                                                        item.preco = (ret.Sum(x => x.PrecoTamanho) / count);
                                                    }
                                                    else // Fornecedor
                                                    {
                                                        int count = ret.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                                                        if (count == 0) count = 1;
                                                        item.preco = (ret.Sum(x => x.PrecoFornecedor) / count);
                                                    }
                                                }
                                                else // Pega  o maior valor
                                                {
                                                    if (produto.TipoCustoFornecedor == 2)// Cor
                                                        item.preco = ret.Max(x => x.PrecoCor);
                                                    else if (produto.TipoCustoFornecedor == 3)// Tamanho
                                                        item.preco = ret.Max(x => x.PrecoTamanho);
                                                    else
                                                        item.preco = ret.Max(x => x.PrecoFornecedor);
                                                }



                                                item.valor = item.CustoCalculado;
                                                //CalcularCustosProduto(ref itemTabelaPreco, produto, produto.Fornecedor.ToList());
                                                fichaTecnicaMaterialItemController.Update(ref item);
                                            }
                                        }
                                    }
                                }
                               
                                //Atualizar Vestillo.Custo Material
                                //if (produto.TipoItem == 1 || produto.TipoItem == 2)
                                //{
                                //    using (FichaTecnicaDoMaterialItemController fichaTecnicaMaterialItemController = new FichaTecnicaDoMaterialItemController())
                                //    {
                                //        var fichasItens = fichaTecnicaMaterialItemController.GetListByMateriaPrima(produto.Id);
                                //        foreach (var fi in fichasItens)
                                //        {
                                //            var item = fi;

                                //            switch (produto.TipoCustoFornecedor)
                                //            {
                                //                case 1:
                                //                    if (produto.TipoCalculoPreco == 2)
                                //                    {
                                //                        item.preco = (listfornecedores.Sum(f => f.PrecoFornecedor) / listfornecedores.Count());
                                //                    }
                                //                    else
                                //                    {
                                //                        item.preco = (listfornecedores.Max(f => f.PrecoFornecedor));
                                //                    }
                                //                    break;
                                //                case 2:
                                //                    if (produto.TipoCalculoPreco == 2)
                                //                    {
                                //                        item.preco = (listfornecedores.Sum(f => f.PrecoCor) / listfornecedores.Count());
                                //                    }
                                //                    else
                                //                    {
                                //                        item.preco = (listfornecedores.Max(f => f.PrecoCor));
                                //                    }
                                //                    break;
                                //                case 3:
                                //                    if (produto.TipoCalculoPreco == 2)
                                //                    {
                                //                        item.preco = (listfornecedores.Sum(f => f.PrecoTamanho) / listfornecedores.Count());
                                //                    }
                                //                    else
                                //                    {
                                //                        item.preco = (listfornecedores.Max(f => f.PrecoTamanho));
                                //                    }
                                //                    break;
                                //                default:
                                //                    break;
                                //            }
                                //            item.valor = item.CustoCalculado;
                                //            //CalcularCustosProduto(ref itemTabelaPreco, produto, produto.Fornecedor.ToList());
                                //            fichaTecnicaMaterialItemController.Update(ref item);
                                //        }
                                //    }
                                //}
                            }
                        }
                    }

                    using (ContasPagarController parcelasRepository = new ContasPagarController())
                    {
                        var ctpagar = parcelasRepository.GetListaPorCampoEValor("IdNotaEntrada", Convert.ToString(Nota.Id));

                        foreach (var g in ctpagar)
                        {
                            parcelasRepository.Delete(g.Id);
                        }

                        foreach (var ctp in Nota.ParcelasCtp)
                        {
                            ContasPagar  c = ctp;
                            c.Id = 0;
                            c.IdNotaEntrada  = Nota.Id;
                            c.NumTitulo = Nota.Numero.ToString();
                            c.Obs = "Título Gerado pela Nota: " + Nota.Referencia.ToString() + " - " + ctp.Obs;

                            parcelasRepository.Save(ref c);
                        }
                    } 

                    repository.CommitTransaction();

                    foreach (var It in Nota.ItensNota)
                    {
                        var produto2 = new ProdutoRepository().GetById(It.iditem);

                        var precoProduto = new ProdutoService().GetServiceFactory().GetPrecoDeCustoDoProduto(produto2);
                        if (produto2.TipoItem == 1 && VestilloSession.UsaFichaProduto)
                        {
                            using (ProdutoFichaRepository pdtFicha = new ProdutoFichaRepository())
                            {
                                pdtFicha.AtualizaCustoItens(produto2.Id, precoProduto);
                            }
                        }
                    }
         

                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

    }
}
