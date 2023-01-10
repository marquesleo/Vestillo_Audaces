using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Controllers
{
    public class TabelaPrecoPCPController: GenericController<TabelaPrecoPCP, TabelaPrecoPCPRepository>
    {
        public override void Save(ref TabelaPrecoPCP tabelaPreco)
        {
            var repository = new ItemTabelaPrecoPCPRepository();
            bool openTransaction = false;
            
            try
            {
                openTransaction = repository.BeginTransaction();
                
                base.Save(ref tabelaPreco);

                var ctItem = new ItemTabelaPrecoPCPController();
                ctItem.DeleteByTabelaPreco(tabelaPreco.Id);

                foreach (var item in tabelaPreco.Itens)
                {
                    var itp = item;
                    itp.Id = 0;
                    itp.TabelaPrecoPCPId = tabelaPreco.Id;
                    ctItem.Save(ref itp);
                }

                if (openTransaction)
                    repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    repository.RollbackTransaction();

                Funcoes.ExibirErro(ex);                
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

        public override TabelaPrecoPCP GetById(int id)
        {
            TabelaPrecoPCP result = base.GetById(id);

            if (result != null)
            {
                using (var itemRepository = new ItemTabelaPrecoPCPRepository())
                {
                    result.Itens = itemRepository.GetItensTabelaPreco(id).ToList();
                }
            }

            return result;
        }

        public override void Delete(int id)
        {
            var repository = new ItemTabelaPrecoPCPRepository();
            bool openTransaction = false;

            try
            {
                openTransaction = repository.BeginTransaction();

                repository.DeleteByTabelaPreco(id);
                base.Delete(id);

                if (openTransaction)
                    repository.CommitTransaction();
            }
            catch (Exception ex)
            {
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

        public TabelaPrecoPCP GetByReferencia(string referencia)
        {
            using (var repository = new TabelaPrecoPCPRepository())
            {
                return repository.GetByReferencia(referencia);
            }
        }

        public IEnumerable<TabelaPrecoPCP> GetListPorReferencia(string referencia)
        {
            using (var repository = new TabelaPrecoPCPRepository())
            {
                return repository.GetListPorReferencia(referencia);
            }
        }

        public IEnumerable<TabelaPrecoPCP> GetListPorDescricao(string desc)
        {
            using (var repository = new TabelaPrecoPCPRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public void CalcularCustos(ref TabelaPrecoPCP tabela)
        {
            try
            {
                //===========================================================================================================
                // Busca informações de custo da empresa
                //===========================================================================================================
                int QtdFuncProducao= 0;  
                int QtdFuncExtras = 0;   
                int MinutosTotalFuncProducao = 0;
                int MinutosTotalFuncExtras = 0;  
                decimal CustoTotalFuncProducao = 0;   
                decimal CustoTotalFuncExtras = 0;
                decimal Aproveitamento = 0;   
                decimal Presenca = 0;   
                decimal Eficiencia = 0;   
                decimal CustoMinutoMaoObra = 0;
                decimal CustoMinutoPrevisto = 0;
                decimal MediaMensalPrevista = 0; 
                decimal CustoMinutoRealizado = 0;
                decimal MediaMensalRealizada = 0;

                EmpresaController empresaController = new EmpresaController();
                empresaController.RetornaInformacaoMaoDeObra(ref QtdFuncProducao, ref QtdFuncExtras, ref  MinutosTotalFuncProducao,
                                                             ref MinutosTotalFuncExtras, ref CustoTotalFuncProducao, ref  CustoTotalFuncExtras,
                                                             ref Aproveitamento, ref  Presenca, ref  Eficiencia, ref  CustoMinutoMaoObra);


                empresaController.RetornaInformacaoDespesas(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString(), ref CustoMinutoPrevisto,ref MediaMensalPrevista, 
                                                            ref CustoMinutoRealizado,ref MediaMensalRealizada);
                
              
                //===========================================================================================================
                // Custo da Ficha Tecnica
                //===========================================================================================================
                IEnumerable<ItemTabelaPrecoPCP> itensTabela = tabela.Itens;
                
                int[] produtosIds = itensTabela.Select(x => x.ProdutoId).Distinct().ToList().ToArray();
                
                FichaTecnicaController fichaTecnicaController = new FichaTecnicaController();
                FichaTecnicaDoMaterialController fichaTecnicaMaterialController = new FichaTecnicaDoMaterialController();
                IEnumerable<FichaTecnicaView> fichasTecnicas =  fichaTecnicaController.GetByFiltros(produtosIds, null, "");
                var empresa = VestilloSession.EmpresaLogada;
                //===========================================================================================================

                IEnumerable<Produto> produtos = new ProdutoController().GetById(produtosIds);

                foreach (ItemTabelaPrecoPCP it in itensTabela)
                {
                    ItemTabelaPrecoPCP item = it;

                    Produto produto = produtos.Where(x => x.Id == item.ProdutoId).First();

                    FichaTecnicaView fichaItem = fichasTecnicas.Where(x => x.ProdutoId == item.ProdutoId).FirstOrDefault();

                    item.CustoMaterial = 0;

                    decimal somaFichaTecnicaOperacoesSemFaccao = 0;
                    decimal somaFichaTecnicaOperacoesFaccao = 0;

                    if (fichaItem != null)
                    {
                        FichaTecnicaDoMaterial fichaMaterial = fichaTecnicaMaterialController.GetByProduto(fichaItem.ProdutoId);
                        foreach (FichaTecnicaOperacaoView operacao in fichaItem.Operacoes)
                        {
                            if (operacao.SetorDescricao.ToUpper() != "*FACÇÃO")
                            {
                                somaFichaTecnicaOperacoesSemFaccao += operacao.TempoCronometrado == 0 ? operacao.TempoCalculado : (decimal)operacao.TempoCronometrado;
                            }
                            else
                            {
                                somaFichaTecnicaOperacoesFaccao += operacao.TempoCronometrado == 0 ? operacao.TempoCalculado : (decimal)operacao.TempoCronometrado;
                            }
                            //somaFichaTecnicaOperacoesSemFaccao = fichaItem.Operacoes.Where(x => x);

                        }
                        //somaFichaTecnicaOperacoesFaccao = fichaItem.Operacoes.Where(x => x.SetorDescricao.ToUpper() == "*FACÇÃO").Sum(x => x.TempoCalculado);
                        var operacoesSemFaccao = fichaItem.Operacoes.Where(x => x.SetorDescricao.ToUpper() != "*FACÇÃO").Count();
                        somaFichaTecnicaOperacoesSemFaccao += (produto.TempoPacote / produto.QtdPacote) * operacoesSemFaccao;
                        
                        item.CustoMaterial = fichaMaterial == null ? 0 : fichaMaterial.Total;

                        item.CustoMaoDeObra = ((somaFichaTecnicaOperacoesSemFaccao * CustoMinutoMaoObra) +
                                              (1 * fichaItem.CustoFaccao)).ToRound(4);
                    
                        item.CustoDespesaPrevisto = ((somaFichaTecnicaOperacoesSemFaccao * CustoMinutoPrevisto) +
                                                    (somaFichaTecnicaOperacoesFaccao * fichaItem.CustoFaccao)).ToRound(4);

                        item.CustoDespesaRealizado = ((somaFichaTecnicaOperacoesSemFaccao * CustoMinutoRealizado) +
                                                    (somaFichaTecnicaOperacoesFaccao * fichaItem.CustoFaccao)).ToRound(4);
                    }
                    
                    item.CustoPrevisto = item.CustoDespesaPrevisto + item.CustoMaterial + item.CustoMaoDeObra;
                    item.CustoRealizado = item.CustoDespesaRealizado + item.CustoMaterial + item.CustoMaoDeObra;
                    
                    decimal totalImpostos = produto.Ipi + produto.Icms;
                    decimal encargosEImpostos = (tabela.Outros + tabela.Frete + tabela.Encargos + tabela.Comissao + totalImpostos
                        + empresa.icms + empresa.ipi + empresa.irpj + empresa.pis + empresa.csll + empresa.cofins + empresa.iss + empresa.simples) / 100;
                    decimal percentualLucro = (1 - (tabela.Lucro/100) - encargosEImpostos).ToRound(4);

                    //CONFIRMAR
                    item.PrecoSugeridoPrevisto = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaPrevisto) / (percentualLucro)).ToRound(4);
                    item.PrecoSugeridoRealizado = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaRealizado) / (percentualLucro)).ToRound(4);
                    if (item.Id == 0 && item.PrecoVenda == 0)
                    {
                        item.PrecoVenda = item.PrecoSugeridoPrevisto;
                    }

                    //if (item.PrecoSugeridoPrevisto != 0)
                    //    item.PercentualLucroBaseCustoPrevisto = (item.PrecoVenda / item.PrecoSugeridoPrevisto).ToRound(4);
                    //else
                    //    item.PercentualLucroBaseCustoPrevisto = 0;

                    //if (item.PrecoSugeridoRealizado != 0)
                    //    item.PercentualLucroBaseCustoRealizado = (item.PrecoVenda / item.PrecoSugeridoRealizado).ToRound(4);
                    //else
                    //    item.PercentualLucroBaseCustoRealizado = 0;

                    //item.LucroBaseCustoPrevisto = (item.PrecoVenda * item.PercentualLucroBaseCustoPrevisto).ToRound(4);
                    //item.LucroBaseCustoRealizado = (item.PrecoVenda * item.PercentualLucroBaseCustoRealizado).ToRound(4);
                    var encargosValorPrevisto = item.PrecoSugeridoPrevisto * encargosEImpostos;
                    var encargosValorRealizado = item.PrecoSugeridoRealizado * encargosEImpostos;
                    item.LucroPrevisto = item.PrecoSugeridoPrevisto - item.CustoPrevisto - encargosValorPrevisto;// CustoPrevisto leva em consideração Despesa, Material e Mão de Obra
                    item.LucroRealizado = item.PrecoSugeridoRealizado - item.CustoRealizado - encargosValorRealizado;// CustoRealizado leva em consideração Despesa, Material e Mão de Obra

                    var encargosPrecoVenda = item.PrecoVenda * encargosEImpostos;
                    item.LucroBaseCustoPrevisto = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaPrevisto;
                    item.LucroBaseCustoRealizado = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaRealizado;

                    if (item.PrecoVenda > 0)
                    {
                        item.PercentualLucroBaseCustoPrevisto = ((item.LucroBaseCustoPrevisto / item.PrecoVenda) * 100).ToRound(4);
                        item.PercentualLucroBaseCustoRealizado = ((item.LucroBaseCustoRealizado / item.PrecoVenda) * 100).ToRound(4);
                    }
                    else
                    {
                        item.PercentualLucroBaseCustoPrevisto = 0;
                        item.PercentualLucroBaseCustoRealizado = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CalcularCustosComValoresManuais(ref TabelaPrecoPCP tabela)
        {
            try
            {
                
                //===========================================================================================================
                // Custo da Ficha Tecnica
                //===========================================================================================================
                IEnumerable<ItemTabelaPrecoPCP> itensTabela = tabela.Itens;

                int[] produtosIds = itensTabela.Select(x => x.ProdutoId).Distinct().ToList().ToArray();
                
                FichaTecnicaDoMaterialController fichaTecnicaMaterialController = new FichaTecnicaDoMaterialController();

                var empresa = VestilloSession.EmpresaLogada;                

                IEnumerable<Produto> produtos = new ProdutoController().GetById(produtosIds);

                //===========================================================================================================

                foreach (ItemTabelaPrecoPCP it in itensTabela)
                {
                    ItemTabelaPrecoPCP item = it;

                    Produto produto = produtos.Where(x => x.Id == item.ProdutoId).First();
                    
                    item.CustoMaterial = 0;
                    
                    FichaTecnicaDoMaterial fichasDoMaterial = fichaTecnicaMaterialController.GetByProduto(item.ProdutoId);
                    if(fichasDoMaterial != null)
                        item.CustoMaterial = fichasDoMaterial.Total;

                    item.CustoPrevisto = item.CustoDespesaPrevisto + item.CustoMaterial + item.CustoMaoDeObra;
                    item.CustoRealizado = item.CustoDespesaRealizado + item.CustoMaterial + item.CustoMaoDeObra;

                    decimal totalImpostos = produto.Ipi + produto.Icms;
                    decimal encargosEImpostos = (tabela.Outros + tabela.Frete + tabela.Encargos + tabela.Comissao + totalImpostos
                        + empresa.icms + empresa.ipi + empresa.irpj + empresa.pis + empresa.csll + empresa.cofins + empresa.iss + empresa.simples) / 100;
                    decimal percentualLucro = (1 - (tabela.Lucro / 100) - encargosEImpostos).ToRound(4);

                    //CONFIRMAR
                    item.PrecoSugeridoPrevisto = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaPrevisto) / (percentualLucro)).ToRound(4);
                    item.PrecoSugeridoRealizado = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaRealizado) / (percentualLucro)).ToRound(4);
                    if (item.Id == 0 && item.PrecoVenda == 0)
                    {
                        item.PrecoVenda = item.PrecoSugeridoPrevisto;
                    }


                    var encargosValorPrevisto = item.PrecoSugeridoPrevisto * encargosEImpostos;
                    var encargosValorRealizado = item.PrecoSugeridoRealizado * encargosEImpostos;
                    item.LucroPrevisto = item.PrecoSugeridoPrevisto - item.CustoPrevisto - encargosValorPrevisto;// CustoPrevisto leva em consideração Despesa, Material e Mão de Obra
                    item.LucroRealizado = item.PrecoSugeridoRealizado - item.CustoRealizado - encargosValorRealizado;// CustoRealizado leva em consideração Despesa, Material e Mão de Obra

                    var encargosPrecoVenda = item.PrecoVenda * encargosEImpostos;
                    item.LucroBaseCustoPrevisto = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaPrevisto;
                    item.LucroBaseCustoRealizado = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaRealizado;

                    if (item.PrecoVenda > 0)
                    {
                        item.PercentualLucroBaseCustoPrevisto = ((item.LucroBaseCustoPrevisto / item.PrecoVenda) * 100).ToRound(4);
                        item.PercentualLucroBaseCustoRealizado = ((item.LucroBaseCustoRealizado / item.PrecoVenda) * 100).ToRound(4);
                    }
                    else
                    {
                        item.PercentualLucroBaseCustoPrevisto = 0;
                        item.PercentualLucroBaseCustoRealizado = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CalcularCustosItemComValorManual(ref TabelaPrecoPCP tabela, Empresa empresa, Produto produto, ItemTabelaPrecoPCP item)
        {
            try
            {
                
                //===========================================================================================================
                // Custo da Ficha Tecnica
                //===========================================================================================================

                FichaTecnicaDoMaterialController fichaTecnicaMaterialController = new FichaTecnicaDoMaterialController();
                item.CustoMaterial = 0;                

                FichaTecnicaDoMaterial fichasDoMaterial = fichaTecnicaMaterialController.GetByProduto(item.ProdutoId);
                item.CustoMaterial = fichasDoMaterial == null? 0 : fichasDoMaterial.Total;

                //===========================================================================================================

                item.CustoPrevisto = item.CustoDespesaPrevisto + item.CustoMaterial + item.CustoMaoDeObra;
                item.CustoRealizado = item.CustoDespesaRealizado + item.CustoMaterial + item.CustoMaoDeObra;

                decimal totalImpostos = produto.Ipi + produto.Icms;
                decimal encargosEImpostos = (tabela.Outros + tabela.Frete + tabela.Encargos + tabela.Comissao + totalImpostos
                    + empresa.icms + empresa.ipi + empresa.irpj + empresa.pis + empresa.csll + empresa.cofins + empresa.iss + empresa.simples) / 100;
                decimal percentualLucro = (1 - (tabela.Lucro / 100) - encargosEImpostos).ToRound(4);

                //CONFIRMAR
                item.PrecoSugeridoPrevisto = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaPrevisto) / (percentualLucro)).ToRound(4);
                item.PrecoSugeridoRealizado = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaRealizado) / (percentualLucro)).ToRound(4);
                if (item.Id == 0 && item.PrecoVenda == 0)
                {
                    item.PrecoVenda = item.PrecoSugeridoPrevisto;
                }                
            
                var encargosValorPrevisto = item.PrecoSugeridoPrevisto * encargosEImpostos;
                var encargosValorRealizado = item.PrecoSugeridoRealizado * encargosEImpostos;

                item.LucroPrevisto = item.PrecoSugeridoPrevisto - item.CustoPrevisto - encargosValorPrevisto;//CustoPrevisto já inclui despesa, mão de obra e material
                item.LucroRealizado = item.PrecoSugeridoRealizado - item.CustoRealizado - encargosValorRealizado;// CustoRealizado já inclui despesa, mão de obra e material

                var encargosPrecoVenda = item.PrecoVenda * encargosEImpostos;
                item.LucroBaseCustoPrevisto = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaPrevisto;
                item.LucroBaseCustoRealizado = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaRealizado;

                if (item.PrecoVenda > 0)
                {
                    item.PercentualLucroBaseCustoPrevisto = ((item.LucroBaseCustoPrevisto / item.PrecoVenda) * 100).ToRound(4);
                    item.PercentualLucroBaseCustoRealizado = ((item.LucroBaseCustoRealizado / item.PrecoVenda) * 100).ToRound(4);
                }
                else
                {
                    item.PercentualLucroBaseCustoPrevisto = 0;
                    item.PercentualLucroBaseCustoRealizado = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CalcularCustosItem(ref TabelaPrecoPCP tabela, Empresa empresa, decimal CustoMinutoMaoObra, decimal CustoMinutoPrevisto, decimal CustoMinutoRealizado, Produto produto, ItemTabelaPrecoPCP item)
        {
            try
            {
                //===========================================================================================================
                // Busca informações de custo da empresa
                //===========================================================================================================
                //int QtdFuncProducao = 0;
                //int QtdFuncExtras = 0;
                //int MinutosTotalFuncProducao = 0;
                //int MinutosTotalFuncExtras = 0;
                //decimal CustoTotalFuncProducao = 0;
                //decimal CustoTotalFuncExtras = 0;
                //decimal Aproveitamento = 0;
                //decimal Presenca = 0;
                //decimal Eficiencia = 0;
                //decimal CustoMinutoMaoObra = 0;
                //decimal CustoMinutoPrevisto = 0;
                //decimal MediaMensalPrevista = 0;
                //decimal CustoMinutoRealizado = 0;
                //decimal MediaMensalRealizada = 0;

                //EmpresaController empresaController = new EmpresaController();
                //empresaController.RetornaInformacaoMaoDeObra(ref QtdFuncProducao, ref QtdFuncExtras, ref  MinutosTotalFuncProducao,
                //                                             ref MinutosTotalFuncExtras, ref CustoTotalFuncProducao, ref  CustoTotalFuncExtras,
                //                                             ref Aproveitamento, ref  Presenca, ref  Eficiencia, ref  CustoMinutoMaoObra);


                //empresaController.RetornaInformacaoDespesas(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString(), ref CustoMinutoPrevisto, ref MediaMensalPrevista,
                //                                            ref CustoMinutoRealizado, ref MediaMensalRealizada);


                //===========================================================================================================
                // Custo da Ficha Tecnica
                //===========================================================================================================

                FichaTecnicaController fichaTecnicaController = new FichaTecnicaController();
                FichaTecnicaDoMaterialController fichaTecnicaMaterialController = new FichaTecnicaDoMaterialController();
                IEnumerable<FichaTecnicaView> fichasTecnicas = fichaTecnicaController.GetByFiltrosProdutos(produto.Id);

                //===========================================================================================================

                    FichaTecnicaView fichaItem = fichasTecnicas.Where(x => x.ProdutoId == item.ProdutoId).FirstOrDefault();

                    item.CustoMaterial = 0;

                    decimal somaFichaTecnicaOperacoesSemFaccao = 0;
                    decimal somaFichaTecnicaOperacoesFaccao = 0;

                    if (fichaItem != null)
                    {
                        FichaTecnicaDoMaterial fichasDoMaterial = fichaTecnicaMaterialController.GetByProduto(fichaItem.ProdutoId);
                        somaFichaTecnicaOperacoesSemFaccao = fichaItem.Operacoes.Where(x => x.SetorDescricao.ToUpper() != "*FACÇÃO").Sum(x => x.TempoCalculado);
                        somaFichaTecnicaOperacoesFaccao = fichaItem.Operacoes.Where(x => x.SetorDescricao.ToUpper() == "*FACÇÃO").Sum(x => x.TempoCalculado);

                        item.CustoMaterial = fichasDoMaterial == null? 0 : fichasDoMaterial.Total;

                        item.CustoMaoDeObra = ((somaFichaTecnicaOperacoesSemFaccao * CustoMinutoMaoObra) +
                                              (somaFichaTecnicaOperacoesFaccao * fichaItem.CustoFaccao)).ToRound(4);

                        item.CustoDespesaPrevisto = ((somaFichaTecnicaOperacoesSemFaccao * CustoMinutoPrevisto) +
                                                    (somaFichaTecnicaOperacoesFaccao * fichaItem.CustoFaccao)).ToRound(4);

                        item.CustoDespesaRealizado = ((somaFichaTecnicaOperacoesSemFaccao * CustoMinutoRealizado) +
                                                    (somaFichaTecnicaOperacoesFaccao * fichaItem.CustoFaccao)).ToRound(4);
                    }

                    item.CustoPrevisto = item.CustoDespesaPrevisto + item.CustoMaterial + item.CustoMaoDeObra;
                    item.CustoRealizado = item.CustoDespesaRealizado + item.CustoMaterial + item.CustoMaoDeObra;

                    decimal totalImpostos = produto.Ipi + produto.Icms;
                    decimal encargosEImpostos = (tabela.Outros + tabela.Frete + tabela.Encargos + tabela.Comissao + totalImpostos 
                        + empresa.icms + empresa.ipi + empresa.irpj + empresa.pis + empresa.csll + empresa.cofins + empresa.iss + empresa.simples) / 100;
                    decimal percentualLucro = (1 - (tabela.Lucro / 100) - encargosEImpostos).ToRound(4);

                    //CONFIRMAR
                    item.PrecoSugeridoPrevisto = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaPrevisto) / (percentualLucro)).ToRound(4);
                    item.PrecoSugeridoRealizado = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaRealizado) / (percentualLucro)).ToRound(4);
                    if (item.Id == 0 && item.PrecoVenda == 0)
                    {
                        item.PrecoVenda = item.PrecoSugeridoPrevisto;
                    }

                    //if (item.PrecoSugeridoPrevisto != 0)
                    //    item.PercentualLucroBaseCustoPrevisto = (item.PrecoVenda / item.PrecoSugeridoPrevisto).ToRound(4);
                    //else
                    //    item.PercentualLucroBaseCustoPrevisto = 0;

                    //if (item.PrecoSugeridoRealizado != 0)
                    //    item.PercentualLucroBaseCustoRealizado = (item.PrecoVenda / item.PrecoSugeridoRealizado).ToRound(4);
                    //else
                    //    item.PercentualLucroBaseCustoRealizado = 0;

                    //item.LucroBaseCustoPrevisto = (item.PrecoVenda * item.PercentualLucroBaseCustoPrevisto).ToRound(4);
                    //item.LucroBaseCustoRealizado = (item.PrecoVenda * item.PercentualLucroBaseCustoRealizado).ToRound(4);
                    var encargosValorPrevisto = item.PrecoSugeridoPrevisto * encargosEImpostos;
                    var encargosValorRealizado = item.PrecoSugeridoRealizado * encargosEImpostos;

                    item.LucroPrevisto = item.PrecoSugeridoPrevisto - item.CustoPrevisto - encargosValorPrevisto;//CustoPrevisto já inclui despesa, mão de obra e material
                    item.LucroRealizado = item.PrecoSugeridoRealizado - item.CustoRealizado - encargosValorRealizado;// CustoRealizado já inclui despesa, mão de obra e material

                    var encargosPrecoVenda = item.PrecoVenda * encargosEImpostos;
                    item.LucroBaseCustoPrevisto = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaPrevisto;
                    item.LucroBaseCustoRealizado = item.PrecoVenda - item.CustoMaoDeObra - item.CustoMaterial - encargosPrecoVenda - item.CustoDespesaRealizado;

                    if (item.PrecoVenda > 0)
                    {
                        item.PercentualLucroBaseCustoPrevisto = ((item.LucroBaseCustoPrevisto / item.PrecoVenda) * 100).ToRound(4);
                        item.PercentualLucroBaseCustoRealizado = ((item.LucroBaseCustoRealizado / item.PrecoVenda) * 100).ToRound(4);
                    }
                    else
                    {
                        item.PercentualLucroBaseCustoPrevisto = 0;
                        item.PercentualLucroBaseCustoRealizado = 0;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
