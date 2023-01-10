using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class PremioController : GenericController<Premio, PremioRepository>
    {
        public Premio GetByIdView(int id)
        {
            using (var repository = new PremioRepository())
            {
                return repository.GetByIdView(id);
            }
        }

        public IEnumerable<Premio> GetByDescricao(string descricao)
        {
            using (var repository = new PremioRepository())
            {
                return repository.GetByDescricao(descricao);
            }
        }

        public IEnumerable<Premio> GetByReferencia(string referencia)
        {
            using (var repository = new PremioRepository())
            {
                return repository.GetByReferencia(referencia);
            }
        }

        public override void Save(ref Premio premio)
        {
            var pessoasRepository = new PessoasPremioRepository();
            var controleFaltaRepository = new ControleFaltaRepository();
            var faixaPremioRepository = new FaixaPremioRepository();
            var MesDiasRepository = new MesDiasRepository();

            try
            {
                pessoasRepository.BeginTransaction();
                base.Save(ref premio);

                if (premio.Pessoas != null && premio.Pessoas.Count() > 0)
                {
                    //var pessoas = pedido.Itens.Where(x => x.Qtd > 0).ToList();
                    foreach (PessoasPremioView pessoa in premio.Pessoas)
                    {
                        PessoasPremio pessoaSave;
                        pessoaSave = pessoa;

                        pessoaSave.PremioId = premio.Id;
                        if (pessoa.PessoaNome == "Excluir")
                            pessoasRepository.Delete(pessoaSave.Id);
                        else
                            pessoasRepository.Save(ref pessoaSave);

                        pessoa.Id = pessoaSave.Id;
                    }
                }

                if (premio.Faixas != null && premio.Faixas.Count() > 0)
                {
                    
                    foreach (FaixaPremio faixa in premio.Faixas)
                    {
                        var faixaSave = faixa;

                        faixaSave.PremioId = premio.Id;
                        if (faixaSave.Maximo == 0 && faixaSave.Minimo == 0)
                            faixaPremioRepository.Delete(faixaSave.Id);
                        else
                            faixaPremioRepository.Save(ref faixaSave);
                    }
                }

                if (premio.Faltas != null && premio.Faltas.Count() > 0)
                {
                    foreach (ControleFalta falta in premio.Faltas)
                    {
                        var faltaSave = falta;
                        faltaSave.PremioId = premio.Id;
                        if (faltaSave.Dias == 0 && faltaSave.PorCento == 0)
                            controleFaltaRepository.Delete(faltaSave.Id);
                        else
                            controleFaltaRepository.Save(ref faltaSave);
                    }
                }

                if (premio.MesDias != null && premio.MesDias.Count() > 0)
                {
                    foreach (MesDias mes in premio.MesDias)
                    {
                        var mesSave = mes;
                        mesSave.PremioId = premio.Id;
                        if (mesSave.Mes == 0)
                            MesDiasRepository.Delete(mesSave.Id);
                        else
                            MesDiasRepository.Save(ref mesSave);
                    }
                }

                //Exlui os itens
                //if (pedido.Itens != null && pedido.Itens.Count() > 0)
                //{
                //    var itens = pedido.Itens.Where(x => x.Qtd == 0).ToList();

                //    foreach (ItemPedidoVenda item in itens)
                //    {
                //        itemPedidoVendaRepository.Delete(item.Id);
                //    }
                //}

                pessoasRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                pessoasRepository.RollbackTransaction();
                throw ex;
            }
        }

        public override void Delete(int id)
        {
            var pessoasRepository = new PessoasPremioRepository();
            var controleFaltaRepository = new ControleFaltaRepository();
            var faixaPremioRepository = new FaixaPremioRepository();
            var mesDiasRepository = new MesDiasRepository();

            try
            {
                pessoasRepository.BeginTransaction();
                
                IEnumerable<PessoasPremio> pessoas = pessoasRepository.GetByPremio(id);
                if (pessoas != null && pessoas.Count() > 0)
                {
                    foreach (var p in pessoas)
                    {
                        pessoasRepository.Delete(p.Id);
                    }
                }

                //IEnumerable<ControleFalta> controleFalta = controleFaltaRepository.GetByPremio(id);

                //if (controleFalta != null && controleFalta.Count() > 0)
                //{
                //    foreach (var cfalta in controleFalta)
                //    {
                //        controleFaltaRepository.Delete(cfalta.Id);
                //    }
                //}

                IEnumerable<FaixaPremio> faixaPremio = faixaPremioRepository.GetByPremio(id);

                if (faixaPremio != null && faixaPremio.Count() > 0)
                {
                    foreach (var faixa in faixaPremio)
                    {
                        faixaPremioRepository.Delete(faixa.Id);
                    }
                }

                IEnumerable<MesDias> mesdias = mesDiasRepository.GetByPremio(id);

                if (mesdias != null && mesdias.Count() > 0)
                {
                    foreach (var mes in mesdias)
                    {
                        mesDiasRepository.Delete(mes.Id);
                    }
                }

                base.Delete(id);

                pessoasRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                pessoasRepository.RollbackTransaction();
                throw ex;
            }
        }
    }
}
