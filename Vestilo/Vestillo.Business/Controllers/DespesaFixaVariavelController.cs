using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class DespesaFixaVariavelController : GenericController<DespesaFixaVariavel, DespesaFixaVariavelRepository>
    {
        public int GetUltimoAno()
        {
            return _repository.GetUltimoAno();
        }

        public DespesaFixaVariavel GetByAno(int ano)
        {
            DespesaFixaVariavel despesa = _repository.GetByAno(ano);

            if (despesa != null)
            {
                using (DespesaFixaVariavelMesRepository despesaFixaVariavelMesRepository = new DespesaFixaVariavelMesRepository())
                {
                    despesa.Meses = despesaFixaVariavelMesRepository.GetByDespesaFixaVariavel(despesa.Id).ToList();
                }
            }
            return despesa;
        }

        public override DespesaFixaVariavel GetById(int id)
        {
            DespesaFixaVariavel despesa = base.GetById(id);

            if (despesa != null)
            {
                using ( DespesaFixaVariavelMesRepository despesaFixaVariavelMesRepository = new DespesaFixaVariavelMesRepository())
                {
                    despesa.Meses = despesaFixaVariavelMesRepository.GetByDespesaFixaVariavel(id).ToList();
                }
            }
            return despesa;
        }

        public override void Delete(int id)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                DespesaFixaVariavelMesRepository despesaFixaVariavelMesRepository = new DespesaFixaVariavelMesRepository();
                despesaFixaVariavelMesRepository.DeleteByDespesaFixaVariavel(id);
                despesaFixaVariavelMesRepository.Dispose();

                base.Delete(id);
                if (!VestilloSession.TabelaPrecoManual)
                    CalcularTabelaPreco();
                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }

        public override void Save(ref DespesaFixaVariavel despesaFixaVariavel)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();
                base.Save(ref despesaFixaVariavel);


                DespesaFixaVariavelMesRepository despesaFixaVariavelMesRepository = new DespesaFixaVariavelMesRepository();
                despesaFixaVariavelMesRepository.DeleteByDespesaFixaVariavel(despesaFixaVariavel.Id);

                foreach (DespesaFixaVariavelMes dp in despesaFixaVariavel.Meses)
                {
                    DespesaFixaVariavelMes despesaMes = dp;

                    despesaMes.Id = 0;
                    despesaMes.DespesaFixaVariavelId = despesaFixaVariavel.Id;
                    despesaFixaVariavelMesRepository.Save(ref despesaMes);
                }

                despesaFixaVariavelMesRepository.Dispose();
                despesaFixaVariavelMesRepository = null;
                if(!VestilloSession.TabelaPrecoManual)
                    CalcularTabelaPreco();
                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();
                
                throw ex;
            }
        }

        private static void CalcularTabelaPreco()
        {
            try
            {
                TabelaPrecoPCPController tabelaPrecoController = new TabelaPrecoPCPController();
                var tabelas = tabelaPrecoController.GetAll();
                foreach (TabelaPrecoPCP t in tabelas)
                {
                    //===========================================================================================================
                    // Busca informações de custo da empresa
                    //===========================================================================================================
                    var tabela = t;
                    int QtdFuncProducao = 0;
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


                    empresaController.RetornaInformacaoDespesas(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString(), ref CustoMinutoPrevisto, ref MediaMensalPrevista,
                                                                ref CustoMinutoRealizado, ref MediaMensalRealizada);


                    //===========================================================================================================
                    // Custo da Ficha Tecnica
                    //===========================================================================================================
                    var itemRepository = new ItemTabelaPrecoPCPRepository();
                    tabela.Itens = itemRepository.GetItensTabelaPreco(tabela.Id).ToList();
                    IEnumerable<ItemTabelaPrecoPCP> itensTabela = tabela.Itens;

                    if (itensTabela.Count() <= 0)
                    {
                        continue;
                    }

                    int[] produtosIds = itensTabela.Select(x => x.ProdutoId).Distinct().ToList().ToArray();
                    

                    FichaTecnicaController fichaTecnicaController = new FichaTecnicaController();
                    FichaTecnicaDoMaterialItemController fichaTecnicaMaterialItemController = new FichaTecnicaDoMaterialItemController();
                    IEnumerable<FichaTecnicaView> fichasTecnicas = fichaTecnicaController.GetByFiltros(produtosIds, null, "");
                    IEnumerable<FichaTecnicaDoMaterialItem> fichasDoMaterialItens = fichaTecnicaMaterialItemController.GetListByProdutos(produtosIds);
                    //===========================================================================================================

                    IEnumerable<Produto> produtos = new ProdutoController().GetById(produtosIds);

                    foreach (ItemTabelaPrecoPCP it in itensTabela)
                    {
                        ItemTabelaPrecoPCP item = it;

                        Produto produto = produtos.Where(x => x.Id == item.ProdutoId).First();

                        FichaTecnicaView fichaItem = fichasTecnicas.Where(x => x.ProdutoId == item.ProdutoId).FirstOrDefault();


                        decimal somaFichaTecnicaOperacoesSemFaccao = 0;
                        decimal somaFichaTecnicaOperacoesFaccao = 0;

                        if (fichaItem != null)
                        {
                            somaFichaTecnicaOperacoesSemFaccao = fichaItem.Operacoes.Where(x => x.SetorDescricao.ToUpper() != "*FACÇÃO").Sum(x => x.TempoCalculado);
                            somaFichaTecnicaOperacoesFaccao = fichaItem.Operacoes.Where(x => x.SetorDescricao.ToUpper() == "*FACÇÃO").Sum(x => x.TempoCalculado);

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
                        decimal encargosEImpostos = (tabela.Outros + tabela.Frete + tabela.Encargos + tabela.Comissao + totalImpostos) / 100;
                        decimal percentualLucro = (1 - tabela.Lucro - encargosEImpostos).ToRound(4);

                        //CONFIRMAR
                        item.PrecoSugeridoPrevisto = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaPrevisto) / (1 - encargosEImpostos)).ToRound(4);
                        item.PrecoSugeridoRealizado = ((item.CustoMaterial + item.CustoMaoDeObra + item.CustoDespesaRealizado) / (1 - encargosEImpostos)).ToRound(4);

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
                    tabelaPrecoController.Save(ref tabela);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DespesaFixaVariavelMes> GetDespesasNaturezasByAno(int ano) //get para a tela de registro
        {
            List<DespesaFixaVariavelMes> despesas = new List<DespesaFixaVariavelMes>();
            using (DespesaFixaVariavelMesRepository despesaFixaVariavelMesRepository = new DespesaFixaVariavelMesRepository())
            {
                for (int i = 1; i < 13; i ++)
                {
                    var despesa = despesaFixaVariavelMesRepository.GetDespesasNaturezas(ano, i.ToString(), null);
                    despesas.AddRange(despesa);
                }

            }
            return despesas.OrderBy(d => d.Despesa).ThenBy(d => d.Mes);
        }

        public void AtualizaDespesasByNaturezasFinanceiras(int ano, int mes, int idNatFinanceira)
        {
            using (DespesaFixaVariavelMesRepository despesaFixaVariavelMesRepository = new DespesaFixaVariavelMesRepository())
            {
                if(idNatFinanceira > 0)
                {
                    var natureza = new NaturezaFinanceiraRepository().GetById(idNatFinanceira);
                    if (natureza.Automatico)
                    {
                        var valoresDespesa = despesaFixaVariavelMesRepository.GetDespesasNaturezas(ano, mes.ToString(), idNatFinanceira).FirstOrDefault();

                        if (valoresDespesa != null)
                        {
                            despesaFixaVariavelMesRepository.UpdateDespesasNaturezas(ano, valoresDespesa);
                            var despesa = GetByAno(ano);
                            if (despesa != null) UpdateTotaisDespesa(despesa.Id);
                        }
                    }
                }                             

            }
        }

        public void UpdateDespesaByNatureza(int idNatFinanceira, bool automatico )
        {
            if (automatico)
            {
                var despesasAutomatizar = new DespesaFixaVariavelRepository().GetDespesasByAutomatizarContasPagar().ToList();
                despesasAutomatizar.ForEach(d => {

                    d.Meses = new DespesaFixaVariavelMesRepository().GetByDespesaFixaVariavel(d.Id).ToList();
                    for (int i = 1; i <= 12; i++)
                    {
                        var despesaMes = new DespesaFixaVariavelMesRepository().GetDespesasNaturezas(d.Ano, i.ToString(), idNatFinanceira).FirstOrDefault();
                        if (despesaMes != null)
                        {
                            despesaMes.DespesaFixaVariavelId = d.Id;
                            d.Meses.Add(despesaMes);
                        }

                    }

                    Save(ref d);
                    UpdateTotaisDespesa(d.Id);

                });
            }
            else
            {
                new DespesaFixaVariavelMesRepository().DeleteByNaturezaFinanceira(idNatFinanceira);
                var despesasAutomatizar = new DespesaFixaVariavelRepository().GetDespesasByAutomatizarContasPagar().ToList();
                despesasAutomatizar.ForEach(d => UpdateTotaisDespesa(d.Id));
            }
                       
        }

        public void UpdateTotaisDespesa( int despesaId)
        {
            if (despesaId > 0)
            {
                var despesa = GetById(despesaId);

                var despesaMeses = despesa.Meses.Select(d => d.Despesa).Distinct().ToList();

                decimal mediaMensalPrevista = 0;
                decimal totalAnualPrevisto = 0;
                decimal mediaMensalRealizada = 0;
                decimal totalAnualRealizado = 0;

                decimal totalDespesaMediaMensalPrevista = 0;
                decimal totalDespesaAnualPrevista = 0;
                decimal totalDespesaMediaMensalRealizada = 0;
                decimal totalDespesaAnualRealizado = 0;

                foreach (var nomeDespesa in despesaMeses)
                {
                    //======================================================================
                    totalDespesaAnualPrevista = 0;

                    totalDespesaAnualPrevista += despesa.Meses.Where(d => d.Despesa == nomeDespesa).Sum(d => d.ValorPrevisto);
                    totalDespesaMediaMensalPrevista = totalDespesaAnualPrevista / 12;

                    //======================================================================

                    totalDespesaAnualRealizado = 0;
                    if (despesa.Ano <= DateTime.Now.Year)
                    {
                        int mesMedia = (DateTime.Now.Month - 1);
                        if (mesMedia == 0 || despesa.Ano < DateTime.Now.Year) mesMedia = 12;

                        totalDespesaAnualRealizado += despesa.Meses.Where(d => d.Despesa == nomeDespesa && d.Mes <= mesMedia).Sum(d => d.ValorRealizado);
                        totalDespesaMediaMensalRealizada = totalDespesaAnualRealizado / mesMedia;
                    }

                    //======================================================================
                    // TOTAIS
                    //======================================================================
                    totalAnualPrevisto += totalDespesaAnualPrevista;
                    mediaMensalPrevista += totalDespesaMediaMensalPrevista;
                    totalAnualRealizado += totalDespesaAnualRealizado;
                    mediaMensalRealizada += totalDespesaMediaMensalRealizada;
                }

                despesa.MediaAnualPrevista = mediaMensalPrevista;
                despesa.MediaAnualRealizada = mediaMensalRealizada;

                despesa.TotalAnualPrevisto = totalAnualPrevisto;
                despesa.TotalAnualRealizado = totalAnualRealizado;

                base.Save(ref despesa);
            }

        }
    }
}
