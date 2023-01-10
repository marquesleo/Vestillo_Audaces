
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class MovimentosDaOperacaoController : GenericController<MovimentosDaOperacao, MovimentosDaOperacaoRepository>
    {

        public override void Save(ref MovimentosDaOperacao Mov)
        {
            using (MovimentosDaOperacaoRepository repository = new MovimentosDaOperacaoRepository())
            {
                try
                {
                    repository.BeginTransaction();
                    base.Save(ref Mov);
                    
                    if (Vestillo.Business.VestilloSession.AtualizaTempoDasFichas == 1)
                    {
                        var fichaTecnicaRepository = new FichaTecnicaOperacaoRepository();
                        var fichasOperacao = fichaTecnicaRepository.GetByMovimentosDaOperacao(Mov.Id);
                        foreach (var f in fichasOperacao)
                        {
                            var ficha = f;

                            var fichaMovimentosTecnicaRepository = new FichaTecnicaOperacaoMovimentoRepository();
                            var fichaMovimentos = fichaMovimentosTecnicaRepository.GetByFichaTecnica(ficha.FichaTecnicaId);

                            decimal tempoAviamento = Convert.ToDecimal(VestilloSession.FormatarTempo(ficha.TempoCosturaComAviamento.ToString()));
                            decimal tempoSemAviamento = Convert.ToDecimal(VestilloSession.FormatarTempo(ficha.TempoCosturaSemAviamento.ToString()));
                            decimal tempoOperacao = (ficha.UtilizaAviamento ? tempoAviamento : tempoSemAviamento);

                            var MovOpera = fichaMovimentos.Where(x => x.FichaTecnicaOperacaoId == ficha.Id);
                            decimal totalMovimentos = 0;
                            foreach (var movimento in MovOpera)
                            {
                                var movimentoOperacao = new MovimentosDaOperacaoRepository().GetById(movimento.MovimentosDaOperacaoId);
                                totalMovimentos += (movimentoOperacao.Tempo * movimento.TempoMovimento);
                            }                            

                            ficha.TempoCalculado = (ficha.Pontadas * tempoOperacao) + (totalMovimentos);
                            ficha.TempoEmSegundos = ficha.TempoCalculado * 60;
                            if (ficha.TempoCalculado > 0 && ficha.TempoCronometrado > 0)
                                ficha.Diferenca = ficha.TempoCronometrado.GetValueOrDefault() - ficha.TempoCalculado;
                            else
                                ficha.Diferenca = 0;

                            fichaTecnicaRepository.Save(ref ficha);
                            

                        }

                        var fichasIds = fichasOperacao.Select(f => f.FichaTecnicaId).Distinct();

                        //atualiza a ficha técnica
                        foreach (int id in fichasIds)
                        {                            
                            var fichaController = new FichaTecnicaController();

                            var fichaTecnica = fichaController.GetById(id);
                            fichaTecnica.TempoTotal = fichaController.CalculaTempoTotalFicha(fichaTecnica);

                            new FichaTecnicaRepository().Save(ref fichaTecnica);
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

        public IEnumerable<MovimentosDaOperacao> GetByAtivos(int AtivoInativo)
        {
            using (MovimentosDaOperacaoRepository repository = new MovimentosDaOperacaoRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<MovimentosDaOperacao> GetListPorReferencia(string referencia)
        {
            using (MovimentosDaOperacaoRepository repository = new MovimentosDaOperacaoRepository())
            {
                return repository.GetListPorReferencia(referencia);
            }
        }

        public IEnumerable<MovimentosDaOperacao> GetListPorDescricao(string desc)
        {
            using (MovimentosDaOperacaoRepository repository = new MovimentosDaOperacaoRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<MovimentosDaOperacao> GetListById(int id)
        {
            using (MovimentosDaOperacaoRepository repository = new MovimentosDaOperacaoRepository())
            {
                return repository.GetListById(id);
            }
        }

    }
}

