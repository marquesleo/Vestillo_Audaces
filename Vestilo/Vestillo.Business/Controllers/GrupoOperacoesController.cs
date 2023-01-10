using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class GrupoOperacoesController : GenericController<GrupoOperacoes, GrupoOperacoesRepository>
    {
        public List<GrupoOperacoesView> GetListByGrupoPacote(int grupoPacoteId)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                var resp = repository.GetListByGrupoPacoteView(grupoPacoteId);
                List<GrupoOperacoesView> grupos = new List<GrupoOperacoesView>();
                resp.ForEach(r =>
                {
                    if (r.Quebra)
                    {
                        var grupo = grupos.Find(g => g.SetorId == r.SetorId && g.PacoteId == r.PacoteId);
                        if (grupo == null)
                        {
                            grupos.Add(r);
                        }
                        else
                        {
                            grupo.TempoTotal += r.TempoTotal;
                            grupo.Tempo += r.Tempo;
                            grupo.TempoUnitario += r.TempoUnitario;
                            grupo.QtdOperacao += r.QtdOperacao;
                        }
                        //grupos.Add(r);
                    }
                    else if (!string.IsNullOrEmpty(r.QuebraManual))
                    {
                        var listQuebra = r.QuebraManual.Split(';').Select(item => int.Parse(item)).ToList();
                        int index = 0;
                        foreach (var q in listQuebra)
                        {
                            index++;
                            if (q > Convert.ToInt32(r.Sequencia))
                            {
                                r.SequenciaQuebra = index;
                                break;
                            }
                        }
                        if (index == (listQuebra.Count) && Convert.ToInt32(r.Sequencia) >= listQuebra[index - 1])
                            r.SequenciaQuebra = index + 1;

                        var grupo = grupos.Find(g => g.SequenciaQuebra == r.SequenciaQuebra && g.PacoteId == r.PacoteId);
                        if (grupo == null)
                        {
                            grupos.Add(r);
                        }
                        else
                        {
                            grupo.TempoTotal += r.TempoTotal;
                            grupo.Tempo += r.Tempo;
                            grupo.TempoUnitario += r.TempoUnitario;
                            grupo.QtdOperacao += r.QtdOperacao;
                        }
                    }
                    else
                    {
                        var grupo = grupos.Find(g => g.PacoteId == r.PacoteId);
                        if (grupo == null)
                        {
                            grupos.Add(r);
                        }
                        else
                        {
                            grupo.TempoTotal += r.TempoTotal;
                            grupo.Tempo += r.Tempo;
                            grupo.TempoUnitario += r.TempoUnitario;
                            grupo.QtdOperacao += r.QtdOperacao;
                        }
                    }
                });
                return grupos;
                //return repository.GetListByGrupoPacoteView(grupoPacoteId);
            }
        }

        public List<GrupoOperacoesView> GetListByGrupoPacoteVisualizar(int grupoPacoteId, int pacoteId)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                return repository.GetListByGrupoPacoteVisualizar(grupoPacoteId, pacoteId);
            }
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                return repository.GetListByPacoteView(pacoteRef);
            }
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, int setorId)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                return repository.GetListByPacoteView(pacoteRef, setorId);
            }
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, string sequencia)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                return repository.GetListByPacoteView(pacoteRef, sequencia);
            }
        }

        public List<GrupoOperacoesView> GetListByPacotesView(List<int> PacotesId)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                var resp = repository.GetListByPacotesView(PacotesId);
                List<GrupoOperacoesView> grupos = new List<GrupoOperacoesView>();
                resp.ForEach(r =>
                {
                    if (r.Quebra)
                    {
                        var grupo = grupos.Find(g => g.SetorId == r.SetorId && g.PacoteId == r.PacoteId);
                        if (grupo == null)
                        {
                            grupos.Add(r);
                        }
                        else
                        {
                            grupo.TempoTotal += r.TempoTotal;
                            grupo.Tempo += r.Tempo;
                            grupo.TempoUnitario += r.TempoUnitario;
                            grupo.QtdOperacao += r.QtdOperacao;
                        }
                        //grupos.Add(r);
                    }
                    else if (!string.IsNullOrEmpty(r.QuebraManual))
                    {
                        var listQuebra = r.QuebraManual.Split(';').Select(item => int.Parse(item)).ToList();
                        int index = 0;
                        foreach (var q in listQuebra)
                        {
                            index++;
                            if (q > Convert.ToInt32(r.Sequencia))
                            {                                
                                r.SequenciaQuebra = index;
                                break;
                            }
                        }
                        if (index == (listQuebra.Count) && Convert.ToInt32(r.Sequencia) >= listQuebra[index - 1])
                            r.SequenciaQuebra = index + 1;

                        var grupo = grupos.Find(g => g.SequenciaQuebra == r.SequenciaQuebra && g.PacoteId == r.PacoteId);
                        if (grupo == null)
                        {
                            grupos.Add(r);
                        }
                        else
                        {
                            grupo.TempoTotal += r.TempoTotal;
                            grupo.Tempo += r.Tempo;
                            grupo.TempoUnitario += r.TempoUnitario;
                            grupo.QtdOperacao += r.QtdOperacao;
                        }
                    }
                    else
                    {
                        var grupo = grupos.Find(g => g.PacoteId == r.PacoteId);
                        if (grupo == null)
                        {
                            grupos.Add(r);
                        }
                        else
                        {
                            grupo.TempoTotal += r.TempoTotal;
                            grupo.Tempo += r.Tempo;
                            grupo.TempoUnitario += r.TempoUnitario;
                            grupo.QtdOperacao += r.QtdOperacao;
                        }
                    }
                });
                return grupos;
            }
        }

        public List<GrupoOperacoesView> GetListByPacotesViewSemFicha(List<int> PacotesId)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                return repository.GetListByPacotesViewSemFicha(PacotesId);
            }
        }

        public GrupoOperacoesView GetListByPacoteESequneciaView(string pacoteRef, string sequencia)
        {
            using (var repository = new GrupoOperacoesRepository())
            {
                return repository.GetListByPacoteESequneciaView(pacoteRef, sequencia);
                //var cupom = repository.GetListByPacoteESequneciaView(pacoteRef, sequencia);
                //var operacaooperadora = new OperacaoOperadoraRepository().GetByCupom(cupom.PacoteId, cupom.OperacaoPadraoId);
                //if (operacaooperadora != null)
                //{
                //    return null;
                //}
                //else
                //{
                //    return cupom;
                //}
            }
        }
    }
}
