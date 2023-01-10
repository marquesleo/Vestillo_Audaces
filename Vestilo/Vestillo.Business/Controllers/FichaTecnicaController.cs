using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class FichaTecnicaController : GenericController<FichaTecnica, FichaTecnicaRepository>
    {
        public IEnumerable<FichaTecnicaView> GetAllView()
        {
            return _repository.GetAllView();
        }

        public override FichaTecnica GetById(int id)
        {
            FichaTecnica result = base.GetById(id);

            if (result != null)
            {
                FichaTecnicaOperacaoRepository fichaTecnicaOperacaoRepository = new FichaTecnicaOperacaoRepository();
              
                result.Operacoes = fichaTecnicaOperacaoRepository.GetByFichaTecnica(id).ToList();

                if (result.Operacoes.Count > 0)
                {
                    FichaTecnicaOperacaoMovimentoRepository fichaTecnicaOperacaoMovimentoRepository = new FichaTecnicaOperacaoMovimentoRepository();
                    IEnumerable<FichaTecnicaOperacaoMovimento> movimentos = fichaTecnicaOperacaoMovimentoRepository.GetByFichaTecnica(id);
                    result.Operacoes.ForEach(x => x.Movimentos = movimentos.Where(m => m.FichaTecnicaOperacaoId == x.Id).ToList());
                }

            }

            return result;
        }

        public FichaTecnica GetByProduto(int produtoId)
        {
            return _repository.GetByProduto(produtoId);
        }

        public IEnumerable<FichaTecnicaOperacaoProdutoView> GetByOperacoesProduto(int operacaoId)
        {
            return _repository.GetByOperacoesProduto(operacaoId);
        }

        public override void Delete(int id)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                //se não tiver ficha de material, apaga também a observação do produto
                var ficha = new FichaTecnicaRepository().GetById(id);
                var fichaMaterial = new FichaTecnicaDoMaterialRepository().GetByProduto(ficha.ProdutoId);

                if (fichaMaterial == null)
                {
                    ObservacaoProdutoRepository observacaorepository = new ObservacaoProdutoRepository();
                    var obs = observacaorepository.GetByProduto(ficha.ProdutoId);
                    if(obs != null)
                        observacaorepository.Delete(obs.Id);
                }  

                FichaTecnicaOperacaoRepository fichaTecnicaOperacaoRepository = new FichaTecnicaOperacaoRepository();
                FichaTecnicaOperacaoMovimentoRepository fichaTecnicaOperacaoMovimentoRepository = new FichaTecnicaOperacaoMovimentoRepository();
                FichaFaccaoRepository fichaFaccaoRepository = new FichaFaccaoRepository();
                
                fichaTecnicaOperacaoMovimentoRepository.DeleteByFichaTecnica(id);
                fichaTecnicaOperacaoRepository.DeleteByFichaTecnica(id);
                fichaFaccaoRepository.DeleteByFichaTecnica(id);
                base.Delete(id);


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

        public void Save(IEnumerable<FichaTecnica> fichas, IEnumerable<FichaTecnicaOperacao> operacoesExcluidas = null)
        {
            bool openTransaction = false;
           

            try
            {
                openTransaction = _repository.BeginTransaction();

                FichaTecnicaOperacaoRepository fichaTecnicaOperacaoRepository = new FichaTecnicaOperacaoRepository();
                FichaTecnicaOperacaoMovimentoRepository fichaTecnicaOperacaoMovimentoRepository = new FichaTecnicaOperacaoMovimentoRepository();
                GrupoOperacoesRepository grupoOperacoesRepository = new GrupoOperacoesRepository();

                List<int> fichasIds = new List<int>();

                if (operacoesExcluidas != null && operacoesExcluidas.Count() > 0)
                {
                    foreach (FichaTecnicaOperacao operacao in operacoesExcluidas)
                    {
                        fichaTecnicaOperacaoMovimentoRepository.DeleteByOperacao(operacao.Id);
                        fichaTecnicaOperacaoRepository.Delete(operacao.Id);
                        fichasIds.Add(operacao.FichaTecnicaId);
                    }

                    fichasIds = fichasIds.Distinct().ToList();
                }

                //=====================================================================================================
                // Gravação das Fichas
                //=====================================================================================================
                foreach (FichaTecnica f in fichas)
                {
                    int IdSetor = 0;                    
                    Setores Setor = new SetoresRepository().SetorParaFicha("*FAC");
                    Produto produto = new ProdutoRepository().GetById(f.ProdutoId);
                    FichaTecnica fichaTecnica = f;

                    decimal tempoTotalInterno = 0;
                    decimal tempoTotal = 0;
                    fichaTecnica.Operacoes.ForEach(x =>
                        {
                            if (x.TempoCronometrado != null && x.TempoCronometrado > 0)
                            {
                                tempoTotal += (decimal)x.TempoCronometrado;
                            }
                            else
                            {
                                tempoTotal += x.TempoCalculado;
                            }
                        });
                    var QtdPacote = produto.QtdPacote == 0 ? 1 : produto.QtdPacote;
                    f.TempoTotal = tempoTotal + ((produto.TempoPacote / QtdPacote) * fichaTecnica.Operacoes.Where(o => o.SetorId != IdSetor).Count());

                    _repository.Save(ref fichaTecnica);



                    //================================================================================================
                    // Gravação das Operações
                    //================================================================================================
                    foreach (FichaTecnicaOperacao op in fichaTecnica.Operacoes)
                    {

                        FichaTecnicaOperacao operacao = op;
                        operacao.FichaTecnicaId = fichaTecnica.Id;
                        fichaTecnicaOperacaoRepository.Save(ref operacao);
                        if (VestilloSession.AtuaizaTempoPacote)
                        {
                            var grupoOperacoes = grupoOperacoesRepository.GetListByOperacaoeProduto(operacao.OperacaoPadraoId, produto.Id);                            
                            grupoOperacoes.ForEach(gop =>
                            {
                                var grupoOp = gop;
                                if(gop.OperacaoPadraoId == operacao.OperacaoPadraoId && gop.Sequencia == operacao.Numero.ToString())
                                {
                                    if (operacao.TempoCronometrado != 0)
                                    {
                                        grupoOp.Tempo = (decimal)operacao.TempoCronometrado;
                                    }
                                    else
                                    {
                                        grupoOp.Tempo = operacao.TempoCalculado;
                                    }
                                    //var QtdPacoteOp = produto.QtdPacote == 0 ? 1 : produto.QtdPacote;
                                    //grupoOp.Tempo += (produto.TempoPacote / QtdPacoteOp);
                                    grupoOperacoesRepository.Save(ref grupoOp);
                                }
                                
                            });

                        }

                        //============================================================================================
                        // Gravação das Operações
                        //============================================================================================
                        fichaTecnicaOperacaoMovimentoRepository.DeleteByOperacao(operacao.Id);
                        
                        foreach (FichaTecnicaOperacaoMovimento m in operacao.Movimentos)
                        {
                            FichaTecnicaOperacaoMovimento movimento = m;
                            movimento.FichaTecnicaOperacaoId = operacao.Id;
                            movimento.Id = 0;
                           
                            fichaTecnicaOperacaoMovimentoRepository.Save(ref movimento);
                        }
                        //======================== Fim Movimentos ====================================================
                    }
                    //============================ Fim Operações =====================================================

                    if (VestilloSession.AtualizaProtheus)
                    {
                        tempoTotalInterno = CalculaTempoTotalFichaInterna(fichaTecnica);
                        decimal TotalMenosInterno = fichaTecnica.TempoTotal - tempoTotalInterno;

                        using (ProdutoRepository prd = new ProdutoRepository())
                        {
                            prd.IncluirAlterarProdutoProtheus(fichaTecnica.ProdutoId, fichaTecnica.TempoTotal, TotalMenosInterno);
                        }
                    }

                    if (!VestilloSession.TabelaPrecoManual)
                        CalcularTabelaPreco(fichaTecnica);
                }

                foreach (int id in fichasIds)
                {
                    if (fichaTecnicaOperacaoRepository.GetByFichaTecnica(id).Count() == 0)
                    {
                        base.Delete(id);
                    }
                }
                //================================ Fim Fichas ========================================================

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


        public override void Save(ref FichaTecnica fichaTecnica)
        {
            FichaTecnicaOperacaoRepository fichaTecnicaOperacaoRepository = new FichaTecnicaOperacaoRepository();
            FichaTecnicaOperacaoMovimentoRepository fichaTecnicaOperacaoMovimentoRepository = new FichaTecnicaOperacaoMovimentoRepository();
            GrupoOperacoesRepository grupoOperacoesRepository = new GrupoOperacoesRepository();

            Produto produto = new ProdutoRepository().GetById(fichaTecnica.ProdutoId);

            decimal tempoTotal = 0;
            decimal tempoTotalInterno = 0;
            var QtdPacote = produto.QtdPacote == 0 ? 1 : produto.QtdPacote;
            if (fichaTecnica.Operacoes != null && fichaTecnica.Operacoes.Count > 0)
            {
                fichaTecnica.Operacoes.ForEach(x =>
                {
                    if (x.TempoCronometrado != null && x.TempoCronometrado > 0)
                    {
                        tempoTotal += (decimal)x.TempoCronometrado;
                    }
                    else
                    {
                        tempoTotal += x.TempoCalculado;
                    }
                });
                fichaTecnica.TempoTotal = tempoTotal + ((produto.TempoPacote / QtdPacote) * fichaTecnica.Operacoes.Count());    

            }
            base.Save(ref fichaTecnica);

            if (VestilloSession.AtualizaProtheus)
            {
                tempoTotalInterno = CalculaTempoTotalFichaInterna(fichaTecnica);

                using (ProdutoRepository prd = new ProdutoRepository())
                {
                    prd.IncluirAlterarProdutoProtheus(fichaTecnica.ProdutoId, fichaTecnica.TempoTotal, tempoTotalInterno);
                }
            }

            //================================================================================================
            // Gravação das Operações
            //================================================================================================
            foreach (FichaTecnicaOperacao op in fichaTecnica.Operacoes)
            {

                FichaTecnicaOperacao operacao = op;
                operacao.FichaTecnicaId = fichaTecnica.Id;
                fichaTecnicaOperacaoRepository.Save(ref operacao);
                if (VestilloSession.AtuaizaTempoPacote)
                {
                    var grupoOperacoes = grupoOperacoesRepository.GetListByOperacaoeProduto(operacao.OperacaoPadraoId, produto.Id);
                    grupoOperacoes.ForEach(gop =>
                    {
                        var grupoOp = gop;
                        if (gop.OperacaoPadraoId == operacao.OperacaoPadraoId && gop.Sequencia == operacao.Numero.ToString())
                        {
                            if (operacao.TempoCronometrado != 0)
                            {
                                grupoOp.Tempo = (decimal)operacao.TempoCronometrado;
                            }
                            else
                            {
                                grupoOp.Tempo = operacao.TempoCalculado;
                            }                            
                            //grupoOp.Tempo += (produto.TempoPacote / produto.QtdPacote);
                            grupoOperacoesRepository.Save(ref grupoOp);
                        }
                    });

                }

                //============================================================================================
                // Gravação das Operações
                //============================================================================================
                if (operacao.Movimentos != null && operacao.Movimentos.Count > 0)
                {
                    fichaTecnicaOperacaoMovimentoRepository.DeleteByOperacao(operacao.Id);

                    foreach (FichaTecnicaOperacaoMovimento m in operacao.Movimentos)
                    {
                        FichaTecnicaOperacaoMovimento movimento = m;
                        movimento.FichaTecnicaOperacaoId = operacao.Id;
                        movimento.Id = 0;

                        fichaTecnicaOperacaoMovimentoRepository.Save(ref movimento);
                    }
                }
                //======================== Fim Movimentos ====================================================
            }
            if (!VestilloSession.TabelaPrecoManual)
                CalcularTabelaPreco(fichaTecnica);
        }

        private static void CalcularTabelaPreco(FichaTecnica fichaTecnica)
        {
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

            FichaTecnicaRepository fichaTecnicaRepository = new FichaTecnicaRepository();

            ItemTabelaPrecoPCPRepository itemTabelaPrecoRepository = new ItemTabelaPrecoPCPRepository();
            var fichaItem = fichaTecnicaRepository.GetByIdView(fichaTecnica.Id);

            var itensTabPreco = itemTabelaPrecoRepository.GetListByProduto(fichaItem.ProdutoId);

            foreach (var it in itensTabPreco)
            {
                TabelaPrecoPCPController tabelaPrecoController = new TabelaPrecoPCPController();
                var tabela = tabelaPrecoController.GetById(it.TabelaPrecoPCPId);


                var item = tabela.Itens.Find(i => i.ProdutoId == fichaItem.ProdutoId);

                EmpresaController empresaController = new EmpresaController();
                empresaController.RetornaInformacaoMaoDeObra(ref QtdFuncProducao, ref QtdFuncExtras, ref  MinutosTotalFuncProducao,
                                                             ref MinutosTotalFuncExtras, ref CustoTotalFuncProducao, ref  CustoTotalFuncExtras,
                                                             ref Aproveitamento, ref  Presenca, ref  Eficiencia, ref  CustoMinutoMaoObra);

                empresaController.RetornaInformacaoDespesas(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString(), ref CustoMinutoPrevisto, ref MediaMensalPrevista,
                                                            ref CustoMinutoRealizado, ref MediaMensalRealizada);


                var produto = new ProdutoRepository().GetById(item.ProdutoId);

                item.CustoMaoDeObra = 0;

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

                tabelaPrecoController.Save(ref tabela);
            }
        }

        public IEnumerable<FichaTecnicaView> GetByFiltros(int[] produtosIds, int[] operacoesIds, string titulo)
        {
            return _repository.GetByFiltros(produtosIds, operacoesIds, titulo);
        }

        public IEnumerable<FichaTecnicaView> GetByFiltrosProdutos(int produto)
        {
            return _repository.GetByFiltrosProdutos(produto);
        }

        public IEnumerable<FichaTecnicaRelatorio> GetAllViewByFiltro(FiltroFichaTecnica filtro)
        {
            return _repository.GetAllViewByFiltro(filtro);
        }

        public IEnumerable<FichaTecnicaOperacaoView> GetOperacoes(int ficha)
        {
            using (FichaTecnicaOperacaoRepository repository = new FichaTecnicaOperacaoRepository())
            {
                return repository.GetByFichaTecnicaView(ficha);
            }
        }

        public void Update(FichaTecnica fichaTecnica)
        {
            _repository.Update(fichaTecnica);
        }

        public decimal CalculaTempoTotalFicha(FichaTecnica f)
        {
            int IdSetor = 0;
            Produto produto = new ProdutoRepository().GetById(f.ProdutoId);
            Setores Setor = new SetoresRepository().SetorParaFicha("*FAC");
            if(Setor != null)
            {
                IdSetor = Setor.Id;
            }
            

            FichaTecnica fichaTecnica = f;

            decimal tempoTotal = 0;
            fichaTecnica.Operacoes.ForEach(x =>
            {
                if (x.TempoCronometrado != null && x.TempoCronometrado > 0)
                {
                    tempoTotal += (decimal)x.TempoCronometrado;
                }
                else
                {
                    tempoTotal += x.TempoCalculado;
                }
            });
            var QtdPacote = produto.QtdPacote == 0 ? 1 : produto.QtdPacote;
            var tempo = tempoTotal + ((produto.TempoPacote / QtdPacote) * fichaTecnica.Operacoes.Where(o => o.SetorId != IdSetor).Count());

            return tempo;
        }

        public decimal CalculaTempoTotalFichaInterna(FichaTecnica f)
        {           
            Produto produto = new ProdutoRepository().GetById(f.ProdutoId);    

            FichaTecnica fichaTecnica = f;

            decimal tempoTotal = 0;
            fichaTecnica.Operacoes.ForEach(x =>
            {
                if(x.SetorId == 2000)
                {
                    if (x.TempoCronometrado != null && x.TempoCronometrado > 0)
                    {
                        tempoTotal += (decimal)x.TempoCronometrado;
                    }
                    else
                    {
                        tempoTotal += x.TempoCalculado;
                    }
                }
                
            });
            decimal tempo = 0;
            var QtdPacote = produto.QtdPacote == 0 ? 1 : produto.QtdPacote;
            if(tempoTotal > 0 )
            {
                tempo = tempoTotal + ((produto.TempoPacote / QtdPacote) * fichaTecnica.Operacoes.Where(o => o.SetorId == 2000).Count());
            }
            

            return tempo;
        }
    }
}
