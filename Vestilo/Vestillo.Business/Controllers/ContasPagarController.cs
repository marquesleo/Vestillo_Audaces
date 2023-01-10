using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ContasPagarController : GenericController<ContasPagar, ContasPagarRepository>
    {
        public void Save(List<ContasPagar> parcelas)
        {
            var repository = new ContasPagarRepository();
            string Evento = String.Empty;
            try
            {
                repository.BeginTransaction();

                foreach (var p in parcelas)
                {
                    var parcela = p;

                    if (parcela.Id > 0)
                    {
                        var cr = this.GetById(parcela.Id);
                        parcela.Status = cr.Status;
                        Evento = "UPDATE";
                    }
                    else
                    {
                        parcela.Status = (int)enumStatusContasPagar.Aberto;
                        Evento = "INSERT";
                    }

                    base.Save(ref parcela);

                    var pendencia = new Pendencias();
                    pendencia.Id = 0;
                    pendencia.Evento = Evento;
                    pendencia.idItem = parcela.Id;
                    pendencia.Tabela = "CONTASPAGAR";
                    pendencia.Status = 0;
                    pendencia.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                    var pendenciaDoc = new PendenciasRepository();
                    pendenciaDoc.Save(ref pendencia);

                    //atualiza despesas fixas variáveis
                    new DespesaFixaVariavelController().AtualizaDespesasByNaturezasFinanceiras(p.DataVencimento.Year, p.DataVencimento.Month, p.IdNaturezaFinanceira);
                }

                repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                throw new Vestillo.Lib.VestilloException(ex);
            }
            finally
            {
                repository.Dispose();
                repository = null;
            }
        }

        public override void Save(ref ContasPagar entity)
        {
            string Evento = String.Empty;
            if (entity.Id > 0)
            {
                Evento = "UPDATE";
                if (entity.ValorPago > 0)
                {
                    if (entity.ValorPago >= (entity.ValorParcela + entity.Juros - entity.Desconto))
                    {
                        entity.Status = (int)enumStatusContasPagar.Baixa_Total;
                    }
                    else
                    {
                        entity.Status = (int)enumStatusContasPagar.Baixa_Parcial;
                    }
                }
                else
                {
                    entity.Status = (int)enumStatusContasPagar.Aberto;
                }
            }
            else
            {
                Evento = "INSERT";
                entity.Status = (int)enumStatusContasPagar.Aberto;
            }

            base.Save(ref entity);

            var pendencia = new Pendencias();
            pendencia.Id = 0;
            pendencia.Evento = Evento;
            pendencia.idItem = entity.Id;
            pendencia.Tabela = "CONTASPAGAR";
            pendencia.Status = 0;
            pendencia.IdEmpresa = VestilloSession.EmpresaLogada.Id;
            var pendenciaDoc = new PendenciasRepository();
            pendenciaDoc.Save(ref pendencia);


            //atualiza despesas fixas variáveis
            new DespesaFixaVariavelController().AtualizaDespesasByNaturezasFinanceiras(entity.DataVencimento.Year, entity.DataVencimento.Month, entity.IdNaturezaFinanceira);
        }

        public override void Delete(int id)
        {
            bool openTransaction = false;
            try
            {
                openTransaction = _repository.BeginTransaction();

                MovimentacaoBancoRepository movimentacaoBancoRepository = new MovimentacaoBancoRepository();
                movimentacaoBancoRepository.DeleteByContasPagar(id);

                //atualiza despesas fixas variaveis
                var contasPagar = GetById(id);
                int ano = contasPagar.DataVencimento.Year;
                int mes = contasPagar.DataVencimento.Month;
                int natId = contasPagar.IdNaturezaFinanceira;

                base.Delete(id);

                new DespesaFixaVariavelController().AtualizaDespesasByNaturezasFinanceiras(ano, mes, natId);

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

        public IEnumerable<ContasPagarView> GetViewByReferencia(string referencia, int[] status = null)
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetViewByReferencia(referencia, status);
            }
        }
        
        public IEnumerable<ContasPagarView> GetAllView()
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetAllView();
            }
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int fornecedorId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetParcelasParaBaixaEmLote(fornecedorId, dataVencimentoInicial, dataVencimentoFinal);
            }

        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasPagarBaixaId)
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetParcelasParaEstornarBaixaEmLote(contasPagarBaixaId);
            }
        }

        public ContasPagarView GetViewById(int id)
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetViewById(id);
            }
        }

        public IEnumerable<ContasPagarView> GetListaPorCampoEValor(string campoBusca, string valor)
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetListaPorCampoEValor(campoBusca, valor);
            }
        }

        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetParcelasRelatorio(filtro);
            }
        }

        public ContasPagar GetByCheque(int chequeId)
        {
            using (var repository = new ContasPagarRepository())
            {
                return repository.GetByCheque(chequeId);
            }
        }
    }
}
