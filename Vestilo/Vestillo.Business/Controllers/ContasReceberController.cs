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
    public class ContasReceberController : GenericController<ContasReceber, ContasReceberRepository>
    {
        public void Save(List<ContasReceber> parcelas)
        {
            var repository = new ContasReceberRepository();
            List<int> ListaIDsClientes = null;
            string Evento = String.Empty;

            try
            {
                repository.BeginTransaction();

                foreach (var p in parcelas)
                {

                    if (ListaIDsClientes == null)
                    {
                        ListaIDsClientes = new List<int>();
                        ListaIDsClientes.Clear();                        
                    }

                    var parcela = p;

                    if (parcela.Id > 0)
                    {
                        var cr = this.GetById(parcela.Id);
                        parcela.Status = cr.Status;
                        Evento = "UPDATE";
                    }
                    else
                    {
                        parcela.Status = (int)enumStatusContasReceber.Aberto;
                        Evento = "INSERT";
                    }

                    base.Save(ref parcela);
                    if (parcela.IdCliente != null)
                    {
                        ListaIDsClientes.Add(Convert.ToInt32(parcela.IdCliente));   
                    }

                    var pendencia = new Pendencias();
                    pendencia.Id = 0;
                    pendencia.Evento = Evento;
                    pendencia.idItem = parcela.Id;
                    pendencia.Tabela = "CONTASRECEBER";
                    pendencia.Status = 0;
                    pendencia.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                    var pendenciaDoc = new PendenciasRepository();
                    pendenciaDoc.Save(ref pendencia);
                }

                repository.CommitTransaction();

                if (ListaIDsClientes != null && ListaIDsClientes.Count > 0)
                {
                    List<int> SemRepeticao = ListaIDsClientes.Distinct().ToList();

                    foreach (var item in SemRepeticao)
                    {
                        AtualizaDadosDeCompra(item);
                    }
                }

                



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

        public IEnumerable<ContasReceber> GetByNotaConsumidor(int idNotaConsumidor)
        {
            var repository = new ContasReceberRepository();
            return repository.GetByNotaConsumidor(idNotaConsumidor);
        }


        public IEnumerable<ContasReceber> GetAllTitulosFilhos(int TituloPai)
        {
            var repository = new ContasReceberRepository();
            return repository.GetAllTitulosFilhos(TituloPai);
        }

        public IEnumerable<ContasReceber> GetAllTitulosBaixados(int Titulo)
        {
            var repository = new ContasReceberRepository();
            return repository.GetAllTitulosBaixados(Titulo);
        }

        public IEnumerable<ReceitasFuturasView> GetReceitaFutura(int[] Ano, bool UnirEmpresas)
        {
            var repository = new ContasReceberRepository();
            return repository.GetReceitaFutura(Ano, UnirEmpresas);
        }


        public override void Save(ref ContasReceber entity)
        {
            string Evento = String.Empty;
            if (entity.Id > 0)
            {
                Evento = "UPDATE";
                if (entity.ValorPago > 0)
                {
                    if (entity.ValorPago >= (entity.ValorParcela + entity.Juros - entity.Desconto))
                    {
                        entity.Status = (int)enumStatusContasReceber.Baixa_Total;
                    }
                    else
                    {
                        entity.Status = (int)enumStatusContasReceber.Baixa_Parcial;
                    }
                }
                else
                {
                    if(entity.Status != (int)enumStatusContasReceber.Negociado)
                        entity.Status = (int)enumStatusContasReceber.Aberto;                   
                }
            }
            else
            {
                Evento = "INSERT";
                if (entity.Status != (int)enumStatusContasReceber.Negociado)
                    entity.Status = (int)enumStatusContasReceber.Aberto;
            }

            base.Save(ref entity);

            if (entity.IdCliente != null)
            {
                AtualizaDadosDeCompra(Convert.ToInt32(entity.IdCliente));
            }

            var pendencia = new Pendencias();
            pendencia.Id = 0;
            pendencia.Evento = Evento;
            pendencia.idItem = entity.Id;
            pendencia.Tabela = "CONTASRECEBER";
            pendencia.Status = 0;
            pendencia.IdEmpresa = VestilloSession.EmpresaLogada.Id;
            var pendenciaDoc = new PendenciasRepository();
            pendenciaDoc.Save(ref pendencia);

        }

        public IEnumerable<ContasReceberView> GetAllView()
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetAllView();
            }
        }

        public IEnumerable<ContasReceberView> GetViewByReferencia(string referencia, int[] status = null, bool SomenteParaBoleto = false, int BancoPortador = 0)
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetViewByReferencia(referencia, status, SomenteParaBoleto,BancoPortador);
            }
        }

        public ContasReceberView GetViewById(int id)
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetViewById(id);
            }
        }

        public ContasReceber GetByCheque(int chequeId)
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetByCheque(chequeId);
            }
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int clienteId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetParcelasParaBaixaEmLote(clienteId, dataVencimentoInicial, dataVencimentoFinal);
            }
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasReceberBaixaId)
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetParcelasParaEstornarBaixaEmLote(contasReceberBaixaId);
            }
        }

        public IEnumerable<ContasReceberView> GetListaPorCampoEValor(string campoBusca, string valor, bool CarregarSomenteAval = false)
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetListaPorCampoEValor(campoBusca, valor, CarregarSomenteAval);
            }
        }

        public void UpdatePossuiAtividade(int IdTitulo, int SimNao)
        {
            using (var repository = new ContasReceberRepository())
            {
                repository.UpdatePossuiAtividade(IdTitulo, SimNao);
            }
        }

        public void UpdateNotaFiscal(int IdFatNfe, string NumNota)
        {
            using (var repository = new ContasReceberRepository())
            {
                repository.UpdateNotaFiscal(IdFatNfe, NumNota);
            }
        }

        public void UpdateConcluirAtividade(int IdTitulo, decimal Saldo)
        {
            using (var repository = new ContasReceberRepository())
            {
                repository.UpdateConcluirAtividade(IdTitulo, Saldo);
            }
        }



        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            using (var repository = new ContasReceberRepository())
            {
                return repository.GetParcelasRelatorio(filtro);
            }
        }
        

        public override void Delete(int id)
        {
            bool openTransaction = false;            
            try
            {
                int idCliente = 0;
                openTransaction = _repository.BeginTransaction();

                var contasReceberRepository = new ContasReceberRepository();
                var dadosTitulo = contasReceberRepository.GetById(id);
                if (dadosTitulo != null)
                {
                    if (dadosTitulo.IdCliente != null)
                    {
                        idCliente = Convert.ToInt32(dadosTitulo.IdCliente);
                    }
                }

                MovimentacaoBancoRepository movimentacaoBancoRepository = new MovimentacaoBancoRepository();
                movimentacaoBancoRepository.DeleteByContasReceber(id);

                base.Delete(id);

                if (openTransaction)
                    _repository.CommitTransaction();

                if (idCliente > 0)
                {
                    AtualizaDadosDeCompra(idCliente);

                    if ((Vestillo.Business.VestilloSession.ControlaInadimplenciaCliente))
                    {
                        using (var Cliente = new Vestillo.Business.Repositories.ColaboradorRepository())
                        {
                            Cliente.ModificaRiscoCliente(idCliente);
                        }
                    }

                }


                


            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();
                throw ex;
            }
        }


        public void AtualizaDadosDeCompra(int IdCliente)
        {
            using (var Cliente = new ColaboradorRepository())
            {
                Cliente.DefineLimiteDeCompra(Convert.ToInt32(IdCliente));
            }
        }


    }
}
