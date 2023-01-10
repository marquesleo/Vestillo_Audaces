using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class ItemLiberacaoOrdemProducaoController : GenericController<ItemLiberacaoOrdemProducao, ItemLiberacaoOrdemProducaoRepository>
    {
        public IEnumerable<ItemLiberacaoOrdemProducaoView> GetByOrdemIdView(int ordemId)
        {
            using (var repository = new ItemLiberacaoOrdemProducaoRepository())
            {
                return repository.GetByOrdemIdView(ordemId);
            }
        }

        public void Save(List<ItemLiberacaoOrdemProducaoView> itensLiberacao, List<OrdemProducaoMaterialView> ordemProducaoMateriais, OrdemProducao ordemProducao) 
        {
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var itemLiberacaoOrdemProducaoRepository = new ItemLiberacaoOrdemProducaoRepository();
            var ordemProducaoMaterialRepository = new OrdemProducaoMaterialRepository();
            var ordemProducaoRepository = new OrdemProducaoRepository();

            try
            {
                itemLiberacaoOrdemProducaoRepository.BeginTransaction();
                if (itensLiberacao != null && itensLiberacao.Count() > 0)
                {
                    //var itens = ordem.Itens.Where(x => x.Quantidade > 0).ToList();

                    foreach (ItemLiberacaoOrdemProducaoView item in itensLiberacao)
                    {
                        ItemLiberacaoOrdemProducao itemSave;
                        itemSave = item;

                        if (item.Quantidade != 0)
                        {
                            itemLiberacaoOrdemProducaoRepository.Save(ref itemSave);
                        }
                        else
                            itemLiberacaoOrdemProducaoRepository.Delete(item.Id);

                    }
                }

                if (ordemProducaoMateriais != null && ordemProducaoMateriais.Count() > 0)
                {
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                    EstoqueController estoqueController = new EstoqueController();

                    foreach (OrdemProducaoMaterialView ordemProducaoMaterial in ordemProducaoMateriais)
                    {
                        OrdemProducaoMaterial ordemProducaoMaterialSave;
                        
                        ordemProducaoMaterialSave = ordemProducaoMaterial;
                        if(ordemProducaoMaterialSave.OrdemProducaoId >0 && ordemProducaoMaterialSave.ItemOrdemProducaoId>0)
                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);

                        if (ordemProducaoMaterial.Excluir)
                        {
                            if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                            {
                                var movEstoque = new MovimentacaoEstoque();
                                if(ordemProducaoMaterial.EmpenhoProducao > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.EmpenhoProducao;

                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;

                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                                if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                {

                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeEmpenhada;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;

                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }

                                if (ordemProducaoMaterial.QuantidadeBaixada > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeBaixada;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }
                            ordemProducaoMaterialRepository.Delete(ordemProducaoMaterial.Id);
                        }
                        else
                        {
                             if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                             {
                                 var movEstoque = new MovimentacaoEstoque();

                                 switch (ordemProducaoMaterial.Movimento)
                                 {
                                     case OrdemProducaoMaterialView.TipoMovimento.Empenho:
                                         movEstoque.Empenho = true;
                                         movEstoque.Saida = ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.EmpenhoLivreUsado;
                                         break;
                                     case OrdemProducaoMaterialView.TipoMovimento.Baixa:
                                         decimal empenho = 0;
                                         if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                         {
                                            movEstoque = new MovimentacaoEstoque();
                                            movEstoque.Baixa = true;
                                            movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                                            if (ordemProducaoMaterial.QuantidadeEmpenhada <= ordemProducaoMaterial.Lancamento)
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.QuantidadeEmpenhada;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.QuantidadeEmpenhada;

                                                if (ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.QuantidadeEmpenhada > 0 && ordemProducaoMaterial.EmpenhoLivreUsado > 0)
                                                {
                                                    movEstoque.Saida += ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    ordemProducaoMaterialSave.EmpenhoProducao -= ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    if (ordemProducaoMaterialSave.EmpenhoProducao < 0) ordemProducaoMaterialSave.EmpenhoProducao = 0;

                                                }
                                            }
                                            else
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.Lancamento;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.Lancamento;
                                            }
                                            empenho = movEstoque.Saida;
                                            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                            movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                            movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                            movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                            movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                            movEstoque.DataMovimento = DateTime.Now;
                                            listMovimentacaoEstoque.Add(movEstoque);
                                             
                                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);
                                         }
                                         movEstoque = new MovimentacaoEstoque();
                                         movEstoque.Saida = ordemProducaoMaterial.Lancamento - empenho;
                                         if (movEstoque.Saida < 0)
                                         {
                                             movEstoque.Saida = 0;
                                         }
                                         break;
                                     case OrdemProducaoMaterialView.TipoMovimento.Estorno_Empenho:
                                         movEstoque.Empenho = true;
                                         movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                         break;
                                     case OrdemProducaoMaterialView.TipoMovimento.Estorno_Baixa:
                                         movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                         break;

                                 }
                                 movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                                 
                                 movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                 movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                 movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                 movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                 movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                 movEstoque.DataMovimento = DateTime.Now;
                                 listMovimentacaoEstoque.Add(movEstoque);
                             }
                        }
                        //else if (ordemProducaoMaterial.QuantidadeNecessaria > 0)
                        //{
                        //    //var material = ordemProducaoMaterialRepository.GetByOrdemView(ordemProducaoMaterial.OrdemProducaoId, ordemProducaoMaterial.MateriaPrimaId, ordemProducaoMaterial.CorId, ordemProducaoMaterial.TamanhoId);
                        //    //if (material != null)
                        //    //{
                        //    //    material.QuantidadeEmpenhada += ordemProducaoMaterialSave.QuantidadeEmpenhada;
                        //    //    ordemProducaoMaterialRepository.Save(ref material);
                        //    //}
                        //    //else
                        //    //{

                        //    //}
                        //    if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                        //    {                                
                        //            var movEstoque = new MovimentacaoEstoque();
                        //            movEstoque.Empenho = true;
                        //            movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                        //            movEstoque.Saida = ordemProducaoMaterial.Lancamento;
                        //            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        //            movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                        //            movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                        //            movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                        //            movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducaoMaterialSave.OrdemProducaoId;
                        //            movEstoque.DataMovimento = DateTime.Now;
                        //            listMovimentacaoEstoque.Add(movEstoque);                                
                        //    }
                        //}
                        //else
                        //{
                        //    if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                        //    {
                        //        var movEstoque = new MovimentacaoEstoque();
                        //        movEstoque.Empenho = true;
                        //        movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                        //        movEstoque.Entrada = ordemProducaoMaterial.QuantidadeNecessaria;
                        //        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        //        movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                        //        movEstoque.CorId = ordemProducaoMaterial.CorId;
                        //        movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                        //        movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducaoMaterial.OrdemProducaoId;
                        //        movEstoque.DataMovimento = DateTime.Now;
                        //        listMovimentacaoEstoque.Add(movEstoque);
                        //    }
                        //    ordemProducaoMaterialRepository.Delete(ordemProducaoMaterial.Id);
                        //}

                    }

                    if (listMovimentacaoEstoque.Count > 0)
                    {                        
                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque,false,true,true); //ALEX 20/05/2021 para não verificar saldo de estoque duas vezes na liberação da OP
                    }
                }

                if (ordemProducao.Itens != null && ordemProducao.Itens.Count() > 0)
                {
                    foreach (ItemOrdemProducaoView itemOrdemProducao in ordemProducao.Itens)
                    {
                        ItemOrdemProducao itemOrdemProducaoSave;
                        itemOrdemProducaoSave = itemOrdemProducao;

                        itemOrdemProducaoRepository.Save(ref itemOrdemProducaoSave);
                     

                    }
                }

                if (ordemProducao != null)
                {
                    if (itensLiberacao != null)
                    {
                        if (itensLiberacao.Exists(i => i.Quantidade > 0))
                        {
                            ordemProducao.Liberacao = DateTime.Now;
                        }
                        else
                        {
                            ordemProducao.Liberacao = null;
                        }

                        ordemProducaoRepository.Save(ref ordemProducao);
                    }
                }

                itemLiberacaoOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemLiberacaoOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }

        public void Finalizar(List<ItemLiberacaoOrdemProducaoView> itensLiberacao, OrdemProducao ordemProducao)
        {
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var itemLiberacaoOrdemProducaoRepository = new ItemLiberacaoOrdemProducaoRepository();
            var ordemProducaoMaterialRepository = new OrdemProducaoMaterialRepository();
            var ordemProducaoRepository = new OrdemProducaoRepository();

            try
            {
                itemLiberacaoOrdemProducaoRepository.BeginTransaction();
                if (itensLiberacao != null && itensLiberacao.Count() > 0)
                {
                    //var itens = ordem.Itens.Where(x => x.Quantidade > 0).ToList();

                    foreach (ItemLiberacaoOrdemProducaoView item in itensLiberacao)
                    {
                        ItemLiberacaoOrdemProducao itemSave;
                        itemSave = item;

                        if (item.Quantidade != 0)
                        {
                            itemLiberacaoOrdemProducaoRepository.Save(ref itemSave);
                        }
                        else
                            itemLiberacaoOrdemProducaoRepository.Delete(item.Id);

                    }
                }

                if (ordemProducao.ListaOrdemProducaoMaterial != null && ordemProducao.ListaOrdemProducaoMaterial.Count() > 0)
                {
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                    EstoqueController estoqueController = new EstoqueController();

                    foreach (OrdemProducaoMaterialView ordemProducaoMaterial in ordemProducao.ListaOrdemProducaoMaterial)
                    {
                        OrdemProducaoMaterial ordemProducaoMaterialSave;

                        ordemProducaoMaterialSave = ordemProducaoMaterial;
                        if (ordemProducaoMaterialSave.OrdemProducaoId > 0 && ordemProducaoMaterialSave.ItemOrdemProducaoId > 0)
                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);

                        if (ordemProducaoMaterial.Excluir)
                        {
                            if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                            {
                                var movEstoque = new MovimentacaoEstoque();
                                if (ordemProducaoMaterial.EmpenhoProducao > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.EmpenhoProducao;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                                if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                {

                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeEmpenhada;

                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;

                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }

                                if (ordemProducaoMaterial.QuantidadeBaixada > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeBaixada;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }
                            ordemProducaoMaterialRepository.Delete(ordemProducaoMaterial.Id);
                        }
                        else
                        {
                            if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                            {
                                var movEstoque = new MovimentacaoEstoque();

                                switch (ordemProducaoMaterial.Movimento)
                                {
                                    case OrdemProducaoMaterialView.TipoMovimento.Empenho:
                                        movEstoque.Empenho = true;
                                        movEstoque.Saida = ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.EmpenhoLivreUsado;
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Baixa:
                                        decimal empenho = 0;
                                        if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                        {
                                            movEstoque = new MovimentacaoEstoque();
                                            movEstoque.Baixa = true;
                                            movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                                            if (ordemProducaoMaterial.QuantidadeEmpenhada <= ordemProducaoMaterial.Lancamento)
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.QuantidadeEmpenhada;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.QuantidadeEmpenhada;

                                                if (ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.QuantidadeEmpenhada > 0 && ordemProducaoMaterial.EmpenhoLivreUsado > 0)
                                                {
                                                    movEstoque.Saida += ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    ordemProducaoMaterialSave.EmpenhoProducao -= ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    if (ordemProducaoMaterialSave.EmpenhoProducao < 0) ordemProducaoMaterialSave.EmpenhoProducao = 0;

                                                }
                                            }
                                            else
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.Lancamento;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.Lancamento;
                                            }
                                            empenho = movEstoque.Saida;
                                            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                            movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                            movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                            movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                            movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                            movEstoque.DataMovimento = DateTime.Now;
                                            listMovimentacaoEstoque.Add(movEstoque);

                                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);
                                        }
                                        movEstoque = new MovimentacaoEstoque();
                                        movEstoque.Saida = ordemProducaoMaterial.Lancamento - empenho;
                                        if (movEstoque.Saida < 0)
                                        {
                                            movEstoque.Saida = 0;
                                        }
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Estorno_Empenho:
                                        movEstoque.Empenho = true;
                                        movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Estorno_Baixa:
                                        movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                        break;

                                }
                                movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;

                                movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                movEstoque.DataMovimento = DateTime.Now;
                                listMovimentacaoEstoque.Add(movEstoque);
                            }
                        }
                        //else if (ordemProducaoMaterial.QuantidadeNecessaria > 0)
                        //{
                        //    //var material = ordemProducaoMaterialRepository.GetByOrdemView(ordemProducaoMaterial.OrdemProducaoId, ordemProducaoMaterial.MateriaPrimaId, ordemProducaoMaterial.CorId, ordemProducaoMaterial.TamanhoId);
                        //    //if (material != null)
                        //    //{
                        //    //    material.QuantidadeEmpenhada += ordemProducaoMaterialSave.QuantidadeEmpenhada;
                        //    //    ordemProducaoMaterialRepository.Save(ref material);
                        //    //}
                        //    //else
                        //    //{

                        //    //}
                        //    if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                        //    {                                
                        //            var movEstoque = new MovimentacaoEstoque();
                        //            movEstoque.Empenho = true;
                        //            movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                        //            movEstoque.Saida = ordemProducaoMaterial.Lancamento;
                        //            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        //            movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                        //            movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                        //            movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                        //            movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducaoMaterialSave.OrdemProducaoId;
                        //            movEstoque.DataMovimento = DateTime.Now;
                        //            listMovimentacaoEstoque.Add(movEstoque);                                
                        //    }
                        //}
                        //else
                        //{
                        //    if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                        //    {
                        //        var movEstoque = new MovimentacaoEstoque();
                        //        movEstoque.Empenho = true;
                        //        movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                        //        movEstoque.Entrada = ordemProducaoMaterial.QuantidadeNecessaria;
                        //        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        //        movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                        //        movEstoque.CorId = ordemProducaoMaterial.CorId;
                        //        movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                        //        movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducaoMaterial.OrdemProducaoId;
                        //        movEstoque.DataMovimento = DateTime.Now;
                        //        listMovimentacaoEstoque.Add(movEstoque);
                        //    }
                        //    ordemProducaoMaterialRepository.Delete(ordemProducaoMaterial.Id);
                        //}

                    }

                    if (listMovimentacaoEstoque.Count > 0)
                    {

                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque);
                    }
                }
                

                if (ordemProducao.Itens != null && ordemProducao.Itens.Count() > 0)
                {
                    foreach (ItemOrdemProducaoView itemOrdemProducao in ordemProducao.Itens)
                    {
                        ItemOrdemProducao itemOrdemProducaoSave;
                        itemOrdemProducaoSave = itemOrdemProducao;

                        itemOrdemProducaoRepository.Save(ref itemOrdemProducaoSave);


                    }
                }

                if (ordemProducao != null)
                {
                    ordemProducaoRepository.Save(ref ordemProducao);
                }

                itemLiberacaoOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemLiberacaoOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }

        public void SalvarLiberacaoTotal(List<ItemLiberacaoOrdemProducaoView> itensLiberacao, List<OrdemProducaoMaterialView> ordemProducaoMateriais, OrdemProducao ordemProducao)
        {
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var itemLiberacaoOrdemProducaoRepository = new ItemLiberacaoOrdemProducaoRepository();
            var ordemProducaoMaterialRepository = new OrdemProducaoMaterialRepository();
            var ordemProducaoRepository = new OrdemProducaoRepository();

            try
            {
                itemLiberacaoOrdemProducaoRepository.BeginTransaction();

                if (!ordemProducaoMateriais[0].Excluir)
                {
                    if (itensLiberacao != null && itensLiberacao.Count() > 0)
                    {
                        //var itens = ordem.Itens.Where(x => x.Quantidade > 0).ToList();

                        foreach (ItemLiberacaoOrdemProducaoView item in itensLiberacao)
                        {
                            ItemLiberacaoOrdemProducao itemSave;
                            itemSave = item;

                            if (item.Quantidade != 0)
                            {
                                itemLiberacaoOrdemProducaoRepository.Save(ref itemSave);
                            }
                            else
                                itemLiberacaoOrdemProducaoRepository.Delete(item.Id);

                        }
                    }
                }


                if (ordemProducaoMateriais != null && ordemProducaoMateriais.Count() > 0)
                {
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                    EstoqueController estoqueController = new EstoqueController();

                    foreach (OrdemProducaoMaterialView ordemProducaoMaterial in ordemProducaoMateriais)
                    {
                        OrdemProducaoMaterial ordemProducaoMaterialSave;

                        ordemProducaoMaterialSave = ordemProducaoMaterial;
                        if (ordemProducaoMaterialSave.OrdemProducaoId > 0 && ordemProducaoMaterialSave.ItemOrdemProducaoId > 0)
                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);

                        if (ordemProducaoMaterial.Excluir)
                        {
                            if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                            {
                                var movEstoque = new MovimentacaoEstoque();
                                if (ordemProducaoMaterial.EmpenhoProducao > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.EmpenhoProducao;

                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;

                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                                if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                {

                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeEmpenhada;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;

                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }

                                if (ordemProducaoMaterial.QuantidadeBaixada > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeBaixada;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }
                            // to apagando no Browser ordemProducaoMaterialRepository.Delete(ordemProducaoMaterial.Id);
                        }
                        else
                        {
                            if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                            {
                                var movEstoque = new MovimentacaoEstoque();

                                switch (ordemProducaoMaterial.Movimento)
                                {
                                    case OrdemProducaoMaterialView.TipoMovimento.Empenho:
                                        movEstoque.Empenho = true;
                                        movEstoque.Saida = ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.EmpenhoLivreUsado;
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Baixa:
                                        decimal empenho = 0;
                                        if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                        {
                                            movEstoque = new MovimentacaoEstoque();
                                            movEstoque.Baixa = true;
                                            movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                                            if (ordemProducaoMaterial.QuantidadeEmpenhada <= ordemProducaoMaterial.Lancamento)
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.QuantidadeEmpenhada;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.QuantidadeEmpenhada;

                                                if (ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.QuantidadeEmpenhada > 0 && ordemProducaoMaterial.EmpenhoLivreUsado > 0)
                                                {
                                                    movEstoque.Saida += ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    ordemProducaoMaterialSave.EmpenhoProducao -= ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    if (ordemProducaoMaterialSave.EmpenhoProducao < 0) ordemProducaoMaterialSave.EmpenhoProducao = 0;

                                                }
                                            }
                                            else
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.Lancamento;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.Lancamento;
                                            }
                                            empenho = movEstoque.Saida;
                                            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                            movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                            movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                            movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                            movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                            movEstoque.DataMovimento = DateTime.Now;
                                            listMovimentacaoEstoque.Add(movEstoque);

                                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);
                                        }
                                        movEstoque = new MovimentacaoEstoque();
                                        movEstoque.Saida = ordemProducaoMaterial.Lancamento - empenho;
                                        if (movEstoque.Saida < 0)
                                        {
                                            movEstoque.Saida = 0;
                                        }
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Estorno_Empenho:
                                        movEstoque.Empenho = true;
                                        movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Estorno_Baixa:
                                        movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                        break;

                                }
                                movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;

                                movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                movEstoque.DataMovimento = DateTime.Now;
                                listMovimentacaoEstoque.Add(movEstoque);
                            }
                        }

                    }

                    if (listMovimentacaoEstoque.Count > 0)
                    {
                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false, true, true); //ALEX 20/05/2021 para não verificar saldo de estoque duas vezes na liberação da OP
                    }
                }

                if (ordemProducao.Itens != null && ordemProducao.Itens.Count() > 0)
                {
                    foreach (ItemOrdemProducaoView itemOrdemProducao in ordemProducao.Itens)
                    {
                        if(!ordemProducaoMateriais[0].Excluir)
                        {
                            ItemOrdemProducao itemOrdemProducaoSave = new ItemOrdemProducao(); ;

                            itemOrdemProducaoSave = itemOrdemProducao;
                            itemOrdemProducaoSave.Status = 1;

                            itemOrdemProducaoRepository.Save(ref itemOrdemProducaoSave);
                        }
                        else
                        {
                            ItemOrdemProducao itemOrdemProducaoSave = new ItemOrdemProducao(); ;

                            itemOrdemProducaoSave = itemOrdemProducao;
                            itemOrdemProducaoSave.Status = 0;

                            itemOrdemProducaoRepository.Save(ref itemOrdemProducaoSave);
                        }
                        


                    }
                }

                if (ordemProducao != null)
                {
                    if (!ordemProducaoMateriais[0].Excluir)
                    {
                        ordemProducao.Liberacao = DateTime.Now;
                        ordemProducao.Status = 8;
                        
                    }
                    else
                    {
                        ordemProducao.Liberacao = null;
                        ordemProducao.Status = 1;
                    }
                    ordemProducaoRepository.Save(ref ordemProducao);
                }

                itemLiberacaoOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemLiberacaoOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }

       public void SomenteMovimentaEstoquetotal(List<OrdemProducaoMaterialView> ordemProducaoMateriais, OrdemProducao ordemProducao)
       {

            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var itemLiberacaoOrdemProducaoRepository = new ItemLiberacaoOrdemProducaoRepository();
            var ordemProducaoMaterialRepository = new OrdemProducaoMaterialRepository();
            var ordemProducaoRepository = new OrdemProducaoRepository();

            try
            {
                itemLiberacaoOrdemProducaoRepository.BeginTransaction();

                if (ordemProducaoMateriais != null && ordemProducaoMateriais.Count() > 0)
                {
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                    EstoqueController estoqueController = new EstoqueController();

                    foreach (OrdemProducaoMaterialView ordemProducaoMaterial in ordemProducaoMateriais)
                    {
                        OrdemProducaoMaterial ordemProducaoMaterialSave;

                        ordemProducaoMaterialSave = ordemProducaoMaterial;
                        if (ordemProducaoMaterialSave.OrdemProducaoId > 0 && ordemProducaoMaterialSave.ItemOrdemProducaoId > 0)                            
                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);

                        var movEstoque = new MovimentacaoEstoque();

                        switch (ordemProducaoMaterial.Movimento)
                        {
                            case OrdemProducaoMaterialView.TipoMovimento.Empenho:
                                movEstoque.Empenho = true;
                                movEstoque.Saida = ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.EmpenhoLivreUsado;
                                break;
                            case OrdemProducaoMaterialView.TipoMovimento.Baixa:
                                decimal empenho = 0;
                                if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Baixa = true;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                                    if (ordemProducaoMaterial.QuantidadeEmpenhada <= ordemProducaoMaterial.Lancamento)
                                    {
                                        movEstoque.Saida = ordemProducaoMaterial.QuantidadeEmpenhada;
                                        ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.QuantidadeEmpenhada;

                                        if (ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.QuantidadeEmpenhada > 0 && ordemProducaoMaterial.EmpenhoLivreUsado > 0)
                                        {
                                            movEstoque.Saida += ordemProducaoMaterial.EmpenhoLivreUsado;
                                            ordemProducaoMaterialSave.EmpenhoProducao -= ordemProducaoMaterial.EmpenhoLivreUsado;
                                            if (ordemProducaoMaterialSave.EmpenhoProducao < 0) ordemProducaoMaterialSave.EmpenhoProducao = 0;

                                        }
                                    }
                                    else
                                    {
                                        movEstoque.Saida = ordemProducaoMaterial.Lancamento;
                                        ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.Lancamento;
                                    }
                                    empenho = movEstoque.Saida;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);

                                    ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);
                                }
                                movEstoque = new MovimentacaoEstoque();
                                movEstoque.Saida = ordemProducaoMaterial.Lancamento - empenho;
                                if (movEstoque.Saida < 0)
                                {
                                    movEstoque.Saida = 0;
                                }
                                break;
                            case OrdemProducaoMaterialView.TipoMovimento.Estorno_Empenho:
                                movEstoque.Empenho = true;
                                movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                break;
                            case OrdemProducaoMaterialView.TipoMovimento.Estorno_Baixa:
                                movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                break;

                        }
                        movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;

                        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                        movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                        movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                        movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                        movEstoque.DataMovimento = DateTime.Now;
                        listMovimentacaoEstoque.Add(movEstoque);

                    }

                    if (listMovimentacaoEstoque.Count > 0)
                    {
                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false, true, true); //ALEX 20/05/2021 para não verificar saldo de estoque duas vezes na liberação da OP
                    }
                }

                itemLiberacaoOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemLiberacaoOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }

        }

        public void FinalizarLiberaTotal(List<OrdemProducaoMaterialView> ordemProducaoMateriais, OrdemProducao ordemProducao)
        {
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var itemLiberacaoOrdemProducaoRepository = new ItemLiberacaoOrdemProducaoRepository();
            var ordemProducaoMaterialRepository = new OrdemProducaoMaterialRepository();
            var ordemProducaoRepository = new OrdemProducaoRepository();

            try
            {
                itemLiberacaoOrdemProducaoRepository.BeginTransaction();
               

                if (ordemProducaoMateriais != null && ordemProducaoMateriais.Count() > 0)
                {
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                    EstoqueController estoqueController = new EstoqueController();

                    foreach (OrdemProducaoMaterialView ordemProducaoMaterial in ordemProducaoMateriais)
                    {
                        OrdemProducaoMaterial ordemProducaoMaterialSave;

                        ordemProducaoMaterialSave = ordemProducaoMaterial;
                        if (ordemProducaoMaterialSave.OrdemProducaoId > 0 && ordemProducaoMaterialSave.ItemOrdemProducaoId > 0)
                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);

                        if (ordemProducaoMaterial.Excluir)
                        {
                            if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                            {
                                var movEstoque = new MovimentacaoEstoque();
                                if (ordemProducaoMaterial.EmpenhoProducao > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.EmpenhoProducao;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                                if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                {

                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Empenho = true;
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeEmpenhada;

                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;

                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }

                                if (ordemProducaoMaterial.QuantidadeBaixada > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Entrada = ordemProducaoMaterial.QuantidadeBaixada;
                                    movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = ordemProducaoMaterial.MateriaPrimaId;
                                    movEstoque.CorId = ordemProducaoMaterial.CorId;
                                    movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                                    movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }
                            ordemProducaoMaterialRepository.Delete(ordemProducaoMaterial.Id);
                        }
                        else
                        {
                            if (!ordemProducaoMaterial.NaoMovimentaEstoque)
                            {
                                var movEstoque = new MovimentacaoEstoque();

                                switch (ordemProducaoMaterial.Movimento)
                                {
                                    case OrdemProducaoMaterialView.TipoMovimento.Empenho:
                                        movEstoque.Empenho = true;
                                        movEstoque.Saida = ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.EmpenhoLivreUsado;
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Baixa:
                                        decimal empenho = 0;
                                        if (ordemProducaoMaterial.QuantidadeEmpenhada > 0)
                                        {
                                            movEstoque = new MovimentacaoEstoque();
                                            movEstoque.Baixa = true;
                                            movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;
                                            if (ordemProducaoMaterial.QuantidadeEmpenhada <= ordemProducaoMaterial.Lancamento)
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.QuantidadeEmpenhada;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.QuantidadeEmpenhada;

                                                if (ordemProducaoMaterial.Lancamento - ordemProducaoMaterial.QuantidadeEmpenhada > 0 && ordemProducaoMaterial.EmpenhoLivreUsado > 0)
                                                {
                                                    movEstoque.Saida += ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    ordemProducaoMaterialSave.EmpenhoProducao -= ordemProducaoMaterial.EmpenhoLivreUsado;
                                                    if (ordemProducaoMaterialSave.EmpenhoProducao < 0) ordemProducaoMaterialSave.EmpenhoProducao = 0;

                                                }
                                            }
                                            else
                                            {
                                                movEstoque.Saida = ordemProducaoMaterial.Lancamento;
                                                ordemProducaoMaterialSave.QuantidadeEmpenhada -= ordemProducaoMaterial.Lancamento;
                                            }
                                            empenho = movEstoque.Saida;
                                            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                            movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                            movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                            movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                            movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                            movEstoque.DataMovimento = DateTime.Now;
                                            listMovimentacaoEstoque.Add(movEstoque);

                                            ordemProducaoMaterialRepository.Save(ref ordemProducaoMaterialSave);
                                        }
                                        movEstoque = new MovimentacaoEstoque();
                                        movEstoque.Saida = ordemProducaoMaterial.Lancamento - empenho;
                                        if (movEstoque.Saida < 0)
                                        {
                                            movEstoque.Saida = 0;
                                        }
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Estorno_Empenho:
                                        movEstoque.Empenho = true;
                                        movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                        break;
                                    case OrdemProducaoMaterialView.TipoMovimento.Estorno_Baixa:
                                        movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                                        break;

                                }
                                movEstoque.AlmoxarifadoId = ordemProducaoMaterialSave.ArmazemId;

                                movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                movEstoque.ProdutoId = ordemProducaoMaterialSave.MateriaPrimaId;
                                movEstoque.CorId = ordemProducaoMaterialSave.CorId;
                                movEstoque.TamanhoId = ordemProducaoMaterialSave.TamanhoId;
                                movEstoque.Observacao = "ORDEM PRODUÇÂO: " + ordemProducao.Referencia;
                                movEstoque.DataMovimento = DateTime.Now;
                                listMovimentacaoEstoque.Add(movEstoque);
                            }
                        }
                    }

                    if (listMovimentacaoEstoque.Count > 0)
                    {

                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque);
                    }
                }


                if (ordemProducao.Itens != null && ordemProducao.Itens.Count() > 0)
                {
                    foreach (ItemOrdemProducaoView itemOrdemProducao in ordemProducao.Itens)
                    {
                        ItemOrdemProducao itemOrdemProducaoSave;
                        itemOrdemProducaoSave = itemOrdemProducao;

                        itemOrdemProducaoRepository.Save(ref itemOrdemProducaoSave);


                    }
                }

                if (ordemProducao != null)
                {
                    ordemProducaoRepository.Save(ref ordemProducao);
                }

                itemLiberacaoOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemLiberacaoOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }
    }
}
