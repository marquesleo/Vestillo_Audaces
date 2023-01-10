
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class OperacaoPadraoController: GenericController<OperacaoPadrao, OperacaoPadraoRepository>
    {

        public IEnumerable<OperacaoPadrao> GetByAllSetorBal()
        {
            using (OperacaoPadraoRepository repository = new OperacaoPadraoRepository())
            {
                return repository.GetByAllSetorBal();
            }
        }

        public IEnumerable<OperacaoPadrao> GetByAtivos(int AtivoInativo)
        {
            using (OperacaoPadraoRepository repository = new OperacaoPadraoRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<OperacaoPadrao> GetListPorReferencia(string referencia)
        {
            using (OperacaoPadraoRepository repository = new OperacaoPadraoRepository())
            {
                return repository.GetListPorReferencia(referencia);
            }
        }

        public IEnumerable<OperacaoPadrao> GetListPorDescricao(string desc)
        {
            using (OperacaoPadraoRepository repository = new OperacaoPadraoRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<OperacaoPadrao> GetListById(int id)
        {
            using (OperacaoPadraoRepository repository = new OperacaoPadraoRepository())
            {
                return repository.GetListById(id);
            }
        }

        public override void Save(ref OperacaoPadrao operacao)
        {
            using (OperacaoPadraoRepository repository = new OperacaoPadraoRepository())
            {
                try
                {
                    repository.BeginTransaction();
                    base.Save(ref operacao);

                    if (Vestillo.Business.VestilloSession.AtualizaTempoDasFichas == 1)
                    {
                        var fichaTecnicaRepository = new FichaTecnicaOperacaoRepository();
                        var fichas = fichaTecnicaRepository.GetByOperacao(operacao.Id);
                        foreach (var f in fichas)
                        {
                            var ficha = f;
                            ficha.TempoCosturaComAviamento = Convert.ToDecimal(VestilloSession.FormatarTempo(operacao.TempoCosturaComAviamento.ToString()));
                            ficha.TempoCosturaSemAviamento = Convert.ToDecimal(VestilloSession.FormatarTempo(operacao.TempoCosturaSemAviamento.ToString()));
                            if (VestilloSession.AtualizaProtheus)
                            {
                                ficha.SetorId = operacao.IdSetor;
                            }
                            decimal tempoOperacao = (ficha.UtilizaAviamento ? ficha.TempoCosturaComAviamento : ficha.TempoCosturaSemAviamento);

                            var fichaMovimentosTecnicaRepository = new FichaTecnicaOperacaoMovimentoRepository();
                            var fichaMovimentos = fichaMovimentosTecnicaRepository.GetByFichaTecnica(ficha.FichaTecnicaId);
                            decimal totalMovimentos = 0;

                            var MovOpera = fichaMovimentos.Where(x => x.FichaTecnicaOperacaoId == ficha.Id);

                            foreach (var movimento in MovOpera)
                            {
                                var movimentoOperacao = new MovimentosDaOperacaoRepository().GetById(movimento.MovimentosDaOperacaoId);
                                totalMovimentos += (movimentoOperacao.Tempo * movimento.TempoMovimento);
                            }

                            if (Vestillo.Business.VestilloSession.AlteraTituloOperacaoFicha == 1)
                            {
                                ficha.OperacaoPadraoDescricao = operacao.Descricao;
                            }

                            ficha.TempoCalculado = (ficha.Pontadas * tempoOperacao) + (totalMovimentos);
                            ficha.TempoEmSegundos = ficha.TempoCalculado * 60;
                            if (ficha.TempoCalculado > 0 && ficha.TempoCronometrado > 0)
                                ficha.Diferenca = ficha.TempoCronometrado.GetValueOrDefault() - ficha.TempoCalculado;
                            else
                                ficha.Diferenca = 0;

                            fichaTecnicaRepository.Save(ref ficha);
                            
                        }

                        //atualiza os totais das fichas
                        var fichasIds = fichas.Select(f => f.FichaTecnicaId).Distinct();
                        var fichaController = new FichaTecnicaController();

                        foreach (int id in fichasIds)
                        {
                            var fichaTecnica = fichaController.GetById(id);
                            fichaTecnica.TempoTotal = fichaController.CalculaTempoTotalFicha(fichaTecnica);

                            if(VestilloSession.AtualizaProtheus)
                            {
                                decimal tempoInterno = fichaController.CalculaTempoTotalFichaInterna(fichaTecnica);
                                decimal TotalMenosInterno = fichaTecnica.TempoTotal - tempoInterno;
                                IncluirDadosProtheus(fichaTecnica.ProdutoId, fichaTecnica.TempoTotal, TotalMenosInterno);
                            }

                            new FichaTecnicaRepository().Save(ref fichaTecnica);
                        }
                        
                        //ATUALIZA TEMPO NO PROTHEUS
                        


                        //FIM ATUALIZA TEMPO PROTHEUS
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


        public void IncluirDadosProtheus(int IdProduto, decimal Tempototal, decimal TempoInterno)
        {
            try
            {
                using (ProdutoRepository prd = new ProdutoRepository())
                {
                    prd.IncluirAlterarProdutoProtheus(IdProduto, Tempototal, TempoInterno);
                }

            }
            catch(Exception ex)
            {
                throw new Vestillo.Lib.VestilloException(ex);
            }

        }

    }
}
