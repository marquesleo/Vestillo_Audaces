using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Controllers
{
    public class GrupoPacoteController : GenericController<GrupoPacote, GrupoPacoteRepository>
    {

        //public OrdemProducaoView GetByIdView(int id)
        //{
        //    using (var repository = new OrdemProducaoRepository())
        //    {
        //        return repository.GetByIdView(id);
        //    }
        //}
        public override void Save(ref GrupoPacote grupo)
        {
            var pacoteProducaoController = new PacoteProducaoController();
            var itemLiberacaoRepository = new ItemLiberacaoPedidoVendaRepository();
            var itemOrdemProducaoRepository = new  ItemOrdemProducaoRepository();
            var grupoOperacoesRepository = new GrupoOperacoesRepository();
            var ordemProducaoRepository = new OrdemProducaoRepository();

            try
            {
                grupoOperacoesRepository.BeginTransaction();

                if (grupo.Pacotes != null && grupo.Pacotes.Count() > 0)
                {
                    base.Save(ref grupo);
                    //var itens = ordem.Itens.Where(x => x.Quantidade > 0).ToList();
                    var grupoOperacoes = grupoOperacoesRepository.GetListOperacaoPorProduto(grupo.ProdutoId);
                    var produto = new ProdutoRepository().GetById(grupo.ProdutoId);
                    if (grupoOperacoes != null && grupoOperacoes.Count > 0)
                    {
                        foreach (GrupoOperacoes operacao in grupoOperacoes)
                        {
                            GrupoOperacoes oepracaoSave = new GrupoOperacoes();
                            oepracaoSave = operacao;
                            if (oepracaoSave.TempoCronometrado != 0)
                            {
                                oepracaoSave.Tempo = oepracaoSave.TempoCronometrado;
                            }
                            else
                            {
                                oepracaoSave.Tempo = oepracaoSave.TempoCalculado;
                            }
                            //oepracaoSave.Tempo += (produto.TempoPacote / grupo.Pacotes.FirstOrDefault().Quantidade);
                            oepracaoSave.Id = 0;
                            oepracaoSave.GrupoPacoteId = grupo.Id;
                            grupoOperacoesRepository.Save(ref oepracaoSave);
                        }
                    }

                    var pacotes = grupo.Pacotes;

                    foreach (PacoteProducao item in grupo.Pacotes)
                    {
                        PacoteProducao itemSave = new PacoteProducao();
                        itemSave = item;
                        itemSave.Id = 0;
                        itemSave.Referencia = null;
                        itemSave.GrupoPacoteId = grupo.Id;
                        pacoteProducaoController.Save(ref itemSave);

                        if (item.ItemOrdemProducaoId > 0)
                        {
                            var itemOrdemProducao = itemOrdemProducaoRepository.GetById(item.ItemOrdemProducaoId);
                            itemOrdemProducao.QuantidadeAtendida += item.Quantidade;
                            if (itemOrdemProducao.QuantidadeAtendida == itemOrdemProducao.Quantidade)
                            {
                                itemOrdemProducao.Status = (int)enumStatusItemOrdemProducao.Pacote;
                            }
                            itemOrdemProducaoRepository.Save(ref itemOrdemProducao);
                            var ordem = ordemProducaoRepository.GetById(itemOrdemProducao.OrdemProducaoId);
                            ordem.Corte = DateTime.Now;
                            var itens = itemOrdemProducaoRepository.GetByOrdem(ordem.Id).ToList();

                            if (itens.Exists(i => i.QuantidadeProduzida > 0))
                            {
                                ordem.Status = (int)enumStatusOrdemProducao.Atendido_Parcial;
                            }
                            else if (itens.Exists(i => i.Quantidade != i.QuantidadeAtendida && i.QuantidadeProduzida <= 0))
                            {
                                ordem.Status = (int)enumStatusOrdemProducao.Producao_Parcial;
                            }
                            else if (!itens.Exists(i => i.Quantidade != i.QuantidadeAtendida))
                            {
                                ordem.Status = (int)enumStatusOrdemProducao.Em_producao;
                            }

                            ordemProducaoRepository.Save(ref ordem);
                        }
                    }
                }
                grupoOperacoesRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                grupoOperacoesRepository.RollbackTransaction();
                throw ex;
            }
        }

        public override void Delete(int id)
        {
            var pacoteProducaoRepository = new PacoteProducaoRepository();
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var grupoOperacoesRepository = new GrupoOperacoesRepository();

            try
            {
                itemOrdemProducaoRepository.BeginTransaction();
                List<GrupoOperacoes> itens = grupoOperacoesRepository.GetListByGrupoPacote(id);

                if (itens != null && itens.Count() > 0)
                {
                    foreach (var i in itens)
                    {
                        grupoOperacoesRepository.Delete(i.Id);
                    }
                }

                List<PacoteProducao> pacotes = pacoteProducaoRepository.GetByGrupoPacote(id);

                if (pacotes != null && pacotes.Count() > 0)
                {
                    foreach (var p in pacotes)
                    {
                        ItemOrdemProducao itemOrdem = itemOrdemProducaoRepository.GetById(p.ItemOrdemProducaoId);
                        itemOrdem.QuantidadeAtendida -= p.Quantidade;
                        if (itemOrdem.QuantidadeAtendida == 0)
                        {
                            itemOrdem.Status = (int)enumStatusItemOrdemProducao.Liberado;
                        }
                        itemOrdemProducaoRepository.Save(ref itemOrdem);
                        pacoteProducaoRepository.Delete(p.Id);
                    }
                }

                base.Delete(id);

                itemOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }
    }
}
