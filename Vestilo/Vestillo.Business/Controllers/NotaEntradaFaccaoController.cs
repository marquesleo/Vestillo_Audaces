
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NotaEntradaFaccaoController : GenericController<NotaEntradaFaccao, NotaEntradaFaccaoRepository>
    {

        public IEnumerable<NotaEntradaFaccaoView> GetCamposEspecificos(string parametrosDaBusca)
        {
            using (NotaEntradaFaccaoRepository repository = new NotaEntradaFaccaoRepository())
            {
                return repository.GetCamposEspecificos(parametrosDaBusca);
            }
        }



        public IEnumerable<NotaEntradaFaccaoView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            using (NotaEntradaFaccaoRepository repository = new NotaEntradaFaccaoRepository())
            {
                return repository.GetListPorReferencia(referencia, parametrosDaBusca);
            }
        }

        public IEnumerable<NotaEntradaFaccaoView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            using (NotaEntradaFaccaoRepository repository = new NotaEntradaFaccaoRepository())
            {
                return repository.GetListPorNumero(Numero, parametrosDaBusca);
            }
        }


        public IEnumerable<NotaEntradaFaccaoView> GetListPorOrdem(int OrdemId)
        {
            using (NotaEntradaFaccaoRepository repository = new NotaEntradaFaccaoRepository())
            {
                return repository.GetListPorOrdem(OrdemId);
            }
        }

        public override void Save(ref NotaEntradaFaccao Nota)
        {
            using (NotaEntradaFaccaoRepository repository = new NotaEntradaFaccaoRepository())
            {
                try
                {
                    repository.BeginTransaction();

                    //PEGA A REFERENCIA
                    base.Save(ref Nota);

                    //grava os itens da nota
                    using (NotaEntradaFaccaoItensRepository itensRepository = new NotaEntradaFaccaoItensRepository())
                    {
                        var itens = itensRepository.GetListByNotaEntradaItens(Nota.Id);

                        foreach (var i in itens)
                        {
                            itensRepository.Delete(i.Id);
                        }

                      

                        foreach (var gr in Nota.ItensNota)
                        {
                            NotaEntradaFaccaoItens g = gr;
                            g.Id = 0;
                            g.IdNota = Nota.Id;
                            itensRepository.Save(ref g);
                        }
                    }

                    using (ContasPagarController parcelasRepository = new ContasPagarController())
                    {
                        var ctpagar = parcelasRepository.GetListaPorCampoEValor("IdNotaEntradaFaccao", Convert.ToString(Nota.Id));

                        foreach (var g in ctpagar)
                        {
                            parcelasRepository.Delete(g.Id);
                        }

                        foreach (var ctp in Nota.ParcelasCtp)
                        {
                            ContasPagar c = ctp;
                            c.Id = 0;
                            c.IdNotaEntradaFaccao = Nota.Id;
                            c.NumTitulo = Nota.Numero.ToString();
                            c.Obs = "Título Gerado pela Nota de Facção: " + Nota.Referencia.ToString() + " - " + ctp.Obs;

                            parcelasRepository.Save(ref c);
                        }
                    }

                    repository.CommitTransaction();

                    AlimentarDadosDaOrdemVinculada(Nota.ItensNota.ToList());
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }


        public void AlimentarDadosDaOrdemVinculada(List<NotaEntradaFaccaoItens> itensNota)
        {
            ItemOrdemProducaoRepository AttOrdem = new ItemOrdemProducaoRepository();

            AttOrdem.AlimentarDadosDaOrdemVinculada(itensNota);

        }

    }
}
