using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Controllers
{
    public class ChequeController : GenericController<Cheque, ChequeRepository>
    {
        public List<Cheque> GetByContasReceberBaixa(int contasReceberBaixaId)
        {
            using (var repository = new ChequeRepository())
            {
                return repository.GetByContasReceberBaixa(contasReceberBaixaId).ToList();
            }
        }

        public IEnumerable<ChequeView> GetAllView()
        {
            using (var repository = new ChequeRepository())
            {
                return repository.GetAllView();
            }
        }

        public IEnumerable<ConsultaChequeRelatorio> GetChequeRelatorio(FiltroChequeRelatorio filtro)
        {
            using (var repository = new ChequeRepository())
            {
                return repository.GetChequeRelatorio(filtro);
            }   
        }

        public IEnumerable<ChequeView> GetByNumeroCheque(string numeroCheque, bool naoCompensado = false)
        {
            using (var repository = new ChequeRepository())
            {
                return repository.GetByNumeroCheque(numeroCheque, naoCompensado);
            }
        }

        public IEnumerable<ChequeView> GetByReferencia(string referencia, bool naoCompensado = false)
        {
            using (var repository = new ChequeRepository())
            {
                return repository.GetByReferencia(referencia, naoCompensado);
            }
        }

        public void UpdateCamposCheque(Cheque cheque)
        {
            using (var repository = new ChequeRepository())
            {
                repository.UpdateCamposCheque(cheque);
            }
        }

        public IEnumerable<Cheque> GetByContasPagarBaixa(int baixaId)
        {
            using (var repository = new ChequeRepository())
            {
                return repository.GetByContasPagarBaixa(baixaId);
            }
        }

        public override void Save(ref Cheque cheque)
        {
            Save(ref cheque, "");
        }

        public void Save(ref Cheque cheque, string observacaoHistorico)
        {
             var histoticoChequeRepository = new HistoricoChequeRepository();

            try
            {
                histoticoChequeRepository.BeginTransaction();

                if (cheque.Status == 0)
                    cheque.Status = (int)Cheque.enumStatus.Incluido;

                base.Save(ref cheque);

                var historico = new HistoricoCheque();
                historico.ChequeId = cheque.Id;
                historico.Data = DateTime.Now;

                if (string.IsNullOrWhiteSpace(observacaoHistorico))
                    historico.Observacao = observacaoHistorico;
                else
                    historico.Observacao = "Cheque Incluído.";

                historico.UsuarioId = VestilloSession.UsuarioLogado.Id;
                historico.Status = cheque.Status;

                histoticoChequeRepository.Save(ref historico);

                histoticoChequeRepository.CommitTransaction();

            }
            catch (Exception ex)
            {
                histoticoChequeRepository.RollbackTransaction();
                throw ex;
            }
        }

        public ChequeView GetViewById(int id)
        {
            using (var repository = new ChequeRepository())
            {
                ChequeView cheque = repository.GetViewById(id);
                if (cheque != null)
                {
                    using (var histoticoChequeRepository = new HistoricoChequeRepository())
                    {
                        cheque.Historico = histoticoChequeRepository.GetByCheque(id);
                    }
                }
                return cheque;
            }
        }

        public void CompensarCheque(ChequeView c, DateTime? dataBaixa = null)
        {
            var historicoChequeRepository = new HistoricoChequeRepository();

            try
            {
                historicoChequeRepository.BeginTransaction();

                Cheque cheque = (c as Cheque);

                bool estornar = cheque.Status == (int)Cheque.enumStatus.Compensado;

                using (var repository = new ChequeRepository())
                {
                    if (estornar)
                    {
                        cheque.Status = cheque.ProrrogarPara != null ? (int)Cheque.enumStatus.Prorrogado : (int)Cheque.enumStatus.Incluido;
                        cheque.Compensacao = null;

                        if (cheque.BancoMovimentacaoId.GetValueOrDefault() > 0 && !cheque.DeTerceiro)
                        {
                            int tipoMovimentacao = cheque.TipoEmitenteCheque == 1 ? 2 : 1; //Cliente = Débito - Empresa = Crédito
                            BancoRepository saldo = new BancoRepository();
                            MovimentacaoBancoController movBancoController = new MovimentacaoBancoController();

                            saldo.UpdateSaldo(cheque.BancoMovimentacaoId.GetValueOrDefault(), tipoMovimentacao, cheque.ValorCompensado.GetValueOrDefault());

                            var movBanco = new MovimentacaoBanco();

                            movBanco.Id = 0;
                            movBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            movBanco.IdBanco = cheque.BancoMovimentacaoId.GetValueOrDefault();
                            movBanco.IdContasPagar = null;
                            movBanco.IdContasReceber = null;
                            movBanco.IdCheque = cheque.Id;
                            movBanco.IdCheque = null;
                            movBanco.Tipo = tipoMovimentacao;
                            movBanco.DataMovimento = DateTime.Now;
                            movBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            movBanco.Valor = cheque.ValorCompensado.GetValueOrDefault();
                            movBanco.Observacao = "Estorno do Cheque: " + cheque.Referencia ?? "";
                            movBancoController.Save(ref movBanco);
                        }
                    }
                    else
                    {
                        cheque.Status = (int)Cheque.enumStatus.Compensado;

                        if (dataBaixa != null)
                            cheque.Compensacao = dataBaixa.GetValueOrDefault();
                        else
                            cheque.Compensacao = DateTime.Now;

                        if (cheque.BancoMovimentacaoId.GetValueOrDefault() > 0 && !cheque.DeTerceiro)
                        {
                            int tipoMovimentacao = cheque.TipoEmitenteCheque == 1 ? 1 : 2; //Cliente = Credito - Empresa = Debito
                            BancoRepository saldo = new BancoRepository();
                            MovimentacaoBancoController movBancoController = new MovimentacaoBancoController();

                            saldo.UpdateSaldo(cheque.BancoMovimentacaoId.GetValueOrDefault(), tipoMovimentacao, cheque.ValorCompensado.GetValueOrDefault());

                            var movBanco = new MovimentacaoBanco();

                            movBanco.Id = 0;
                            movBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            movBanco.IdBanco = cheque.BancoMovimentacaoId.GetValueOrDefault();
                            movBanco.IdContasPagar = null;
                            movBanco.IdContasReceber = null;
                            movBanco.IdCheque = cheque.Id;
                            movBanco.IdCheque = null;
                            movBanco.Tipo = tipoMovimentacao;
                            movBanco.DataMovimento = DateTime.Now;
                            movBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            movBanco.Valor = cheque.ValorCompensado.GetValueOrDefault();
                            movBanco.Observacao = "Baixa do Cheque: " + cheque.Referencia ?? "";
                            movBancoController.Save(ref movBanco);
                        }
                    }
                    
                    repository.CompensarCheque(cheque);
                }

                var historico = new HistoricoCheque();
                historico.ChequeId = cheque.Id;
                historico.Data = DateTime.Now;

                if (estornar)
                    historico.Observacao =  ("Compensação estornada. " + c.ObsCompensacao).Trim();
                else
                    historico.Observacao = c.ObsCompensacao;

                historico.UsuarioId = VestilloSession.UsuarioLogado.Id;
                historico.Status = cheque.Status;

                historicoChequeRepository.Save(ref historico);

                historicoChequeRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                historicoChequeRepository.RollbackTransaction();
                throw ex;
            }

        }

        public void DevolverCheque(ChequeView c, bool estornar)
        {
            var historicoChequeRepository = new HistoricoChequeRepository();

            try
            {
                historicoChequeRepository.BeginTransaction();

                Cheque cheque = (c as Cheque);

                using (var repository = new ChequeRepository())
                {
                    if (estornar)
                    {
                        cheque.Status = cheque.ProrrogarPara != null ? (int)Cheque.enumStatus.Prorrogado : (int)Cheque.enumStatus.Incluido;
                        cheque.Alinea1Id = null;
                        cheque.Alinea2Id = null;
                        cheque.DataAlinea1 = null;
                        cheque.DataAlinea2 = null;
                        cheque.DataApresentacaoAlinea1 = null;
                        cheque.DataApresentacaoAlinea2 = null;
                    }
                    else
                    {
                        cheque.Status = (int)Cheque.enumStatus.Devolvido;
                    }
                    repository.DevolverCheque(c);
                }

                var historico = new HistoricoCheque();
                historico.ChequeId = cheque.Id;
                historico.Data = DateTime.Now;

                if (estornar)
                    historico.Observacao = ("Devolução estornada. " + c.ObsDevolucao).Trim();
                else
                    historico.Observacao = c.ObsDevolucao;
                
                historico.UsuarioId = VestilloSession.UsuarioLogado.Id;
                historico.Status = cheque.Status;

                historicoChequeRepository.Save(ref historico);

                historicoChequeRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                historicoChequeRepository.RollbackTransaction();
                throw ex;
            }

        }

        public void LiberarChequeBordero(ChequeView c)
        {
            var historicoChequeRepository = new HistoricoChequeRepository();

            try
            {
                historicoChequeRepository.BeginTransaction();

                Cheque cheque = (c as Cheque);

                using (var repository = new ChequeRepository())
                {
                    if (cheque.BancoMovimentacaoId.GetValueOrDefault() > 0 && !cheque.DeTerceiro)
                    {
                        int tipoMovimentacao = cheque.TipoEmitenteCheque == 1 ? 1 : 2; //Cliente = Credito - Empresa = Débito
                        BancoRepository saldo = new BancoRepository();
                        MovimentacaoBancoController movBancoController = new MovimentacaoBancoController();

                        saldo.UpdateSaldo(cheque.BancoMovimentacaoId.GetValueOrDefault(), tipoMovimentacao, cheque.ValorCompensado.GetValueOrDefault());

                        var movBanco = new MovimentacaoBanco();

                        movBanco.Id = 0;
                        movBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                        movBanco.IdBanco = cheque.BancoMovimentacaoId.GetValueOrDefault();
                        movBanco.IdContasPagar = null;
                        movBanco.IdContasReceber = null;
                        movBanco.IdCheque = cheque.Id;
                        movBanco.Tipo = tipoMovimentacao;
                        movBanco.DataMovimento = DateTime.Now;
                        movBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                        movBanco.Valor = cheque.ValorCompensado.GetValueOrDefault();
                        movBanco.Observacao = "Liberação do Borderô do Cheque: " + cheque.Referencia ?? "";
                        movBancoController.Save(ref movBanco);
                    }

                    cheque.Compensacao = null;
                    cheque.Status = (int)Cheque.enumStatus.Devolvido;                    
                    repository.DevolverCheque(c);
                }

                var historico = new HistoricoCheque();
                historico.ChequeId = cheque.Id;
                historico.Data = DateTime.Now;
                historico.Observacao = c.ObsDevolucao;
                historico.UsuarioId = VestilloSession.UsuarioLogado.Id;
                historico.Status = cheque.Status;
                historicoChequeRepository.Save(ref historico);

                var borderoRepository = new BorderoCobrancaRepository();
                var borderoDocRepository = new BorderoCobrancaDocumentoRepository();
                var borderoController = new BorderoCobrancaController();
                var bordero = borderoRepository.GetByDocumento(c.Id, true).ToList();
                bordero.ForEach(b => {
                    b.ValorReceber -= c.Valor;
                    borderoRepository.Save(ref b);

                    var borderoDoc = new BorderoCobrancaDocumentoRepository().GetByBordero(b.Id).ToList();
                    var borderoCheque = borderoDoc.FindAll(d => d.ChequeId == c.Id);
                    borderoCheque.ForEach(doc => borderoDocRepository.Delete(doc.Id));
                    
                });

                historicoChequeRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                historicoChequeRepository.RollbackTransaction();
                throw ex;
            }

        }

        public void ProrrogarCheque(Cheque cheque)
        {
            var histoticoChequeRepository = new HistoricoChequeRepository();

            try
            {
                histoticoChequeRepository.BeginTransaction();

                using (var repository = new ChequeRepository())
                {
                    cheque.Status = (int)Cheque.enumStatus.Prorrogado;
                    repository.ProrrogarCheque(cheque);
                }

                var historico = new HistoricoCheque();
                historico.ChequeId = cheque.Id;
                historico.Data = DateTime.Now;
                historico.Observacao = "Prorrogado para: " + ((DateTime)cheque.ProrrogarPara).ToString("dd/MM/yyyy");

                historico.UsuarioId = VestilloSession.UsuarioLogado.Id;
                historico.Status = cheque.Status;

                histoticoChequeRepository.Save(ref historico);

                histoticoChequeRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                histoticoChequeRepository.RollbackTransaction();
                throw ex;
            }

        }

        public override void Delete(int id)
        {
            bool openTransaction = this._repository.BeginTransaction();

            try
            {
                MovimentacaoBancoRepository movimentacaoBancoRepository = new MovimentacaoBancoRepository();
                movimentacaoBancoRepository.DeleteByCheque(id);

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

        public void ResgatarCheque(Cheque cheque)
        {
            var histoticoChequeRepository = new HistoricoChequeRepository();
            var contasReceberRepository = new ContasReceberBaixaRepository();

            try
            {
                histoticoChequeRepository.BeginTransaction();

                bool estornar = cheque.Status == (int)Cheque.enumStatus.Resgatado;

                using (var repository = new ChequeRepository())
                {
                    if (estornar)
                    {
                        cheque.Status = cheque.ProrrogarPara != null ? (int)Cheque.enumStatus.Prorrogado : (int)Cheque.enumStatus.Incluido;
                        cheque.Resgate = null;
                    }
                    else
                    {
                        cheque.Status = (int)Cheque.enumStatus.Resgatado;
                        cheque.Resgate = DateTime.Now;
                    }

                    repository.ResgatarCheque(cheque);
                }               

                string referenciaParcela = "";

                if (cheque.TipoEmitenteCheque == 1) //cheque do cliente
                {
                    var contasReceberController = new ContasReceberController();

                    if (estornar)
                    {
                        ContasReceber parcela = contasReceberController.GetByCheque(cheque.Id);

                        if (parcela != null && parcela.ValorPago > 0)
                        {
                            throw new Exception("Não é possivel estornar o resgate do cheque pois o contas a receber já foi baixado.");
                        }
                        else
                        {
                            if (parcela != null)
                                contasReceberController.Delete(parcela.Id);
                        }
                    }
                    else
                    {

                        ContasReceber parcela = new ContasReceber()
                        {
                            Ativo = 1,
                            DataEmissao = DateTime.Now,
                            DataVencimento = DateTime.Now,
                            IdBanco = cheque.BancoMovimentacaoId,
                            IdCliente = cheque.ColaboradorId,
                            IdEmpresa = cheque.EmpresaId,
                            IdNaturezaFinanceira = cheque.NaturezaFinanceiraId ?? 1, //Receita
                            IdTipoDocumento = 2,  // Cheque
                            ValorParcela = cheque.Valor,
                            Saldo = cheque.Valor,
                            ValorPago = 0,
                            Parcela = "1",
                            IdCheque = cheque.Id,
                            Prefixo = "CH"
                        };


                        contasReceberController.Save(ref parcela);

                        referenciaParcela = parcela.NumTitulo;
                    }
                }
                else //cheque da empresa
                {
                    var contasPagarController = new ContasPagarController();

                    if (estornar)
                    {
                        ContasPagar parcela = contasPagarController.GetByCheque(cheque.Id);

                        if (parcela != null && parcela.ValorPago > 0)
                        {
                            throw new Exception("Não é possivel estornar o resgate do cheque pois o contas a pagar já foi baixado.");
                        }
                        else
                        {
                            if (parcela != null)
                                contasPagarController.Delete(parcela.Id);
                        }
                    }
                    else
                    {

                        ContasPagar parcela = new ContasPagar()
                        {
                            Ativo = 1,
                            DataEmissao = DateTime.Now,
                            DataVencimento = DateTime.Now,
                            IdBanco = cheque.BancoMovimentacaoId,
                            IdEmpresa = cheque.EmpresaId,
                            IdNaturezaFinanceira = cheque.NaturezaFinanceiraId ?? 2, //Despesa
                            IdTipoDocumento = 2,  // Cheque
                            ValorParcela = cheque.Valor,
                            Saldo = cheque.Valor,
                            ValorPago = 0,
                            Parcela = "1",
                            Prefixo = "CH",
                            IdCheque = cheque.Id
                        };


                        contasPagarController.Save(ref parcela);

                        referenciaParcela = parcela.NumTitulo;
                    }
                }

                var historico = new HistoricoCheque();
                historico.ChequeId = cheque.Id;
                historico.Data = DateTime.Now;
               
                historico.UsuarioId = VestilloSession.UsuarioLogado.Id;
                historico.Status = cheque.Status;

                if (estornar)
                    historico.Observacao = "Resgate estornado.";
                else
                    historico.Observacao = "Título gerado: " + referenciaParcela;

                histoticoChequeRepository.Save(ref historico);

                histoticoChequeRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                histoticoChequeRepository.RollbackTransaction();
                throw ex;
            }

        }
    }
}
