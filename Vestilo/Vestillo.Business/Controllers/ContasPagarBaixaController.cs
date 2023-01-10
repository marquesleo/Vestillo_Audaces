using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace Vestillo.Business.Controllers
{
    public class ContasPagarBaixaController : GenericController<ContasPagarBaixa, ContasPagarBaixaRepository>
    {

        public void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int fornecedorId = 0)
        {
            try
            {
                var repositoryContasPagar = new ContasPagarRepository();

                int? contasPagarBaixaLoteId = null;
                ContasPagarBaixa baixaLote = new ContasPagarBaixa();
                baixaLote.Cheques = cheques.ToList();
                baixaLote.ValorDinheiro = valorDinheiro;
                baixaLote.ValorCheque = cheques.Sum(x => x.Valor);
                baixaLote.Obs = string.Join(", ", parcelasBaixa.Select(x => x.NumTitulo).ToArray());
                baixaLote.DataBaixa = DateTime.Now;
                if (baixaLote.BancoId.GetValueOrDefault() == 0)
                {
                    baixaLote.BancoId = parcelasBaixa.First().BancoMovimentacaoId;
                }
                baixaLote.Lote = true;
                this.Save(ref baixaLote, true, valorCreditoGerar, fornecedorId);
                contasPagarBaixaLoteId = baixaLote.Id;
                
                foreach (TitulosBaixaLoteView parcelaLote in parcelasBaixa)
                {
                    ContasPagar parcela = repositoryContasPagar.GetById(parcelaLote.Id);
                    ContasPagarBaixa baixa = new ContasPagarBaixa();
                    baixa.ContasPagarId = parcela.Id;
                    baixa.DataBaixa = DateTime.Now;
                    baixa.Obs = "Baixado em lote";
                    baixa.ValorDinheiro = (parcela.ValorParcela + parcela.Juros - parcela.Desconto - parcela.ValorPago);
                    baixa.Cheques = new List<Cheque>();
                    baixa.ContasPagarBaixaLoteId = contasPagarBaixaLoteId;
                    this.Save(ref baixa, false,0 ,0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasPagarBaixaLoteId)
        {
            try
            {
                foreach (TitulosBaixaLoteView parcelaLote in parcelas)
                {
                    this.Delete(parcelaLote.BaixaId, false, false, "");
                }

                string titulos = string.Join(", ", parcelas.Select(x => x.NumTitulo).ToArray());
                this.Delete(contasPagarBaixaLoteId, true, true, titulos);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(ref ContasPagarBaixa entity, decimal valorCredito)
        {
            this.Save(ref entity, true, valorCredito, 0);
        }

        public override void Save(ref ContasPagarBaixa ContasPagarBaixa)
        {
            Save(ref ContasPagarBaixa, true, 0, 0);
        }

        private void Save(ref ContasPagarBaixa contasPagarBaixa, bool movimentaSaldo, decimal valorCredito, int fornecedorId)
        {
            var repository = new ContasPagarBaixaRepository();

            try
            {
                repository.BeginTransaction();

                base.Save(ref contasPagarBaixa);

                var chequeController = new ChequeController();
                var histoticoChequeRepository = new HistoricoChequeRepository();
                var contasPagarController = new ContasPagarController();
                ContasPagar cp = null;

                if (contasPagarBaixa.ContasPagarId.GetValueOrDefault() > 0)
                {
                    cp = contasPagarController.GetById(contasPagarBaixa.ContasPagarId.GetValueOrDefault());

                    //atualiza despesas fixas variáveis
                    new DespesaFixaVariavelController().AtualizaDespesasByNaturezasFinanceiras(contasPagarBaixa.DataBaixa.Year, contasPagarBaixa.DataBaixa.Month, cp.IdNaturezaFinanceira);
                }
                
                var movBancoService = new MovimentacaoBancoService().GetServiceFactory();
                BancoRepository saldo = new BancoRepository();

                contasPagarBaixa.Cheques = contasPagarBaixa.Cheques ?? new List<Cheque>();

                foreach (var c in contasPagarBaixa.Cheques)
                {
                    var cheque = c;

                    if (cheque.Id > 0)
                        cheque = chequeController.GetById(cheque.Id);
                    else
                        cheque.Status = (int)Cheque.enumStatus.Incluido;

                    if(c.TipoEmitenteCheque == 1)
                    {
                        cheque.Status = (int)Cheque.enumStatus.Compensado;
                        cheque.Compensacao = DateTime.Now;
                        cheque.ValorCompensado = cheque.Valor; 
                    }
                    
                    cheque.ContasPagarBaixaId = contasPagarBaixa.Id;
                    chequeController.Save(ref cheque);

                    var historico = new HistoricoCheque();
                    historico.ChequeId = cheque.Id;
                    historico.Data = DateTime.Now;
                    
                    if (cp != null)
                        historico.Observacao = "Baixa Contas a Pagar: " + cp.NumTitulo;
                    else
                        historico.Observacao = "Baixa Contas a Pagar: " + contasPagarBaixa.Obs ?? "";
                    
                    historico.UsuarioId = VestilloSession.UsuarioLogado.Id;
                    historico.Status = cheque.Status;
                    histoticoChequeRepository.Save(ref historico);

                   /* if (movimentaSaldo && cheque.TipoEmitenteCheque == 2 && !cheque.DeTerceiro) //Cheque empresa e não é de terceiros
                    {
                        if (contasPagarBaixa.Lote)
                        {
                            int bancoCheque = cheque.BancoMovimentacaoId ?? contasPagarBaixa.BancoId.GetValueOrDefault();

                            saldo.UpdateSaldo(bancoCheque, 2, cheque.ValorCompensado.GetValueOrDefault());

                            var movBanco = new MovimentacaoBanco();

                            movBanco.Id = 0;
                            movBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            movBanco.IdBanco = cheque.BancoMovimentacaoId.GetValueOrDefault();
                            movBanco.IdContasPagar = null;
                            movBanco.IdContasReceber = null;
                            movBanco.IdCheque = cheque.Id;
                            movBanco.Tipo = 2;
                            movBanco.DataMovimento = contasPagarBaixa.DataBaixa;
                            movBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            movBanco.Valor = cheque.Valor;
                            movBanco.Observacao = "Débito realizado pela baixa em lote dos titulos " + contasPagarBaixa.Obs + " com o cheque " + cheque.Referencia;
                            movBancoService.Save(ref movBanco);
                        }
                        else if (cp.IdTipoDocumento != 7)
                        {
                            saldo.UpdateSaldo(cheque.BancoMovimentacaoId.GetValueOrDefault(), 2, cheque.ValorCompensado.GetValueOrDefault());

                            var MovBanco = new MovimentacaoBanco();

                            MovBanco.Id = 0;
                            MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            MovBanco.IdBanco = cheque.BancoMovimentacaoId.GetValueOrDefault();
                            MovBanco.IdContasPagar = cp.Id;
                            MovBanco.IdContasReceber = null;
                            MovBanco.IdCheque = cheque.Id;
                            MovBanco.Tipo = 2;
                            MovBanco.DataMovimento = contasPagarBaixa.DataBaixa;
                            MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            MovBanco.Valor = cheque.Valor;
                            MovBanco.Observacao = "Débito realizado pela baixa do titulo " + cp.NumTitulo + " com o cheque " + cheque.Referencia;
                            movBancoService.Save(ref MovBanco);
                        }
                    }*/
                }

                contasPagarBaixa.Creditos = contasPagarBaixa.Creditos ?? new List<CreditoFornecedor>();

                CreditoFornecedorController creditoFornecedorController = new CreditoFornecedorController();

                foreach (var c in contasPagarBaixa.Creditos)
                {
                    CreditoFornecedor credito = creditoFornecedorController.GetById(c.Id);

                    credito.IdContasPagarBaixa = contasPagarBaixa.Id;
                    credito.ObsQuitacao = "Crédito utilizado na baixa do título: " + cp.NumTitulo + ".";
                    credito.Status = CreditoFornecedor.StatusCredito.Quitado; //Quitado
                    credito.DataQuitacao = contasPagarBaixa.DataBaixa;
                    creditoFornecedorController.Save(ref credito);
                }

                if (contasPagarBaixa.ContasPagarId.GetValueOrDefault() > 0)
                {
                    if (cp.Saldo == 0)
                        cp.Saldo = cp.ValorParcela + cp.Juros - cp.Desconto;

                    if (valorCredito > 0)
                    {
                        cp.Saldo = 0;
                        cp.ValorPago = cp.ValorParcela + cp.Juros - cp.Desconto;
                    }
                    else
                    {
                        cp.Saldo = (cp.Saldo - (contasPagarBaixa.ValorCheque + contasPagarBaixa.ValorDinheiro + contasPagarBaixa.ValorCredito));
                        cp.ValorPago = (cp.ValorPago + contasPagarBaixa.ValorCheque + contasPagarBaixa.ValorDinheiro + contasPagarBaixa.ValorCredito);
                    }
                    cp.DataPagamento = contasPagarBaixa.DataBaixa;
                    
                    if(contasPagarBaixa.BancoId.GetValueOrDefault() > 0)
                        cp.IdBanco = contasPagarBaixa.BancoId;

                    contasPagarController.Save(ref cp);
                }

                if (movimentaSaldo && contasPagarBaixa.ValorDinheiro > 0)
                {
                    if (contasPagarBaixa.Lote)
                    {

                        saldo.UpdateSaldo(contasPagarBaixa.BancoId.GetValueOrDefault(), 2, contasPagarBaixa.ValorDinheiro);

                        var movBanco = new MovimentacaoBanco();

                        movBanco.Id = 0;
                        movBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                        movBanco.IdBanco = contasPagarBaixa.BancoId.GetValueOrDefault();
                        movBanco.IdContasPagar = null;
                        movBanco.IdContasReceber = null;
                        movBanco.IdCheque = null;
                        movBanco.Tipo = 2; 
                        movBanco.DataMovimento = contasPagarBaixa.DataBaixa;
                        movBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                        movBanco.Valor = contasPagarBaixa.ValorDinheiro;
                        movBanco.Observacao = "Débito realizado pela baixa em lote dos titulos: " + contasPagarBaixa.Obs;
                        movBancoService.Save(ref movBanco);
                    }
                    else if (cp != null && cp.IdBanco != null && contasPagarBaixa.ValorDinheiro > 0) //Movimenta saldo do banco
                    {
                        // se for Nota de débito do fornecedor não movimenta o banco
                        if (cp.IdTipoDocumento != 7 && contasPagarBaixa.ValorDinheiro > 0)
                        {
                            saldo.UpdateSaldo(cp.IdBanco.GetValueOrDefault(), 2, contasPagarBaixa.ValorDinheiro);

                            var MovBanco = new MovimentacaoBanco();

                            MovBanco.Id = 0;
                            MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            MovBanco.IdBanco = cp.IdBanco.GetValueOrDefault();
                            MovBanco.IdContasPagar = cp.Id;
                            MovBanco.IdContasReceber = null;
                            MovBanco.IdCheque = null;
                            MovBanco.Tipo = 2;
                            MovBanco.DataMovimento = contasPagarBaixa.DataBaixa;
                            MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            MovBanco.Valor = contasPagarBaixa.ValorDinheiro;
                            MovBanco.Observacao = "Débito realizado pela baixa do titulo " + cp.NumTitulo;
                            movBancoService.Save(ref MovBanco);
                        }
                    }
                }

                // Geração de crédito pro cliente na baixa.
                if (valorCredito > 0)
                {
                    var creditoFornecedor = new CreditoFornecedor();
                    creditoFornecedor.Ativo = true;
                    creditoFornecedor.DataEmissao = DateTime.Now;
                    if (cp != null)
                    {
                        creditoFornecedor.IdFornecedor = cp.IdFornecedor;
                        creditoFornecedor.Obs = "Gerado através da baixa do título: " + cp.NumTitulo + ".";
                    }
                    else
                    {
                        creditoFornecedor.Obs = "Gerado através da baixa em lote dos títulos " + contasPagarBaixa.Obs + ".";
                        creditoFornecedor.IdFornecedor = fornecedorId;
                    }
                    creditoFornecedor.Status = CreditoFornecedor.StatusCredito.Aberto;
                    creditoFornecedor.Valor = valorCredito;
                    creditoFornecedor.IdContasPagarBaixa = null;
                    creditoFornecedor.IdContasPagarBaixaQueGerou = contasPagarBaixa.Id;
                    creditoFornecedorController.Save(ref creditoFornecedor);
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

        public override void Delete(int id)
        {
            Delete(id, true, false, "");
        }

        public void Delete(int id, bool movimentarSaldo, bool estornarLote, string titulosLote)
        {
            var repository = new ContasPagarBaixaRepository();

            try
            {
                bool gerouCredito = false;
                decimal valorCreditoGerado = 0;

                repository.BeginTransaction();
                var baixa = this.GetById(id);

                CreditoFornecedorController creditoFornecedorController = new CreditoFornecedorController();

                IEnumerable<CreditoFornecedor> creditosGerados = creditoFornecedorController.GetByContasPagarQueGerou(baixa.Id);

                if (creditosGerados != null && creditosGerados.Count() > 0)
                {
                    if (creditosGerados.Where(x => x.Status != CreditoFornecedor.StatusCredito.Aberto).Count() > 0)
                    {
                        throw new Exception("O contas a pagar não pode ser estornado pois o crédito gerado na baixa foi utilizado!");
                    }

                    foreach (CreditoFornecedor credito in creditosGerados)
                    {
                        if (credito.IdContasPagarBaixaQueGerou.GetValueOrDefault() > 0 && credito.IdContasPagarBaixaQueGerou.GetValueOrDefault() == baixa.Id)
                        {
                            valorCreditoGerado += credito.Valor;
                            creditoFornecedorController.Delete(credito.Id);
                            gerouCredito = true;
                        }
                    }
                }

                if (baixa.Creditos.Count() > 0)
                {
                    foreach (CreditoFornecedor credito in baixa.Creditos)
                    {

                        if (credito.IdContasPagarBaixaQueGerou.GetValueOrDefault() == baixa.ContasPagarId)
                        {
                            creditoFornecedorController.Delete(credito.Id);
                            gerouCredito = true;
                        }
                        else
                        {
                            CreditoFornecedor creditoUsado = credito;
                            creditoUsado.Status =  CreditoFornecedor.StatusCredito.Aberto;
                            creditoUsado.DataQuitacao = null;
                            creditoUsado.ObsQuitacao = null;
                            creditoUsado.IdContasPagarBaixa = null;
                            creditoFornecedorController.Save(ref creditoUsado);
                        }
                    }
                }

                BancoRepository saldo = new BancoRepository();
                
                var chequeController = new ChequeController();
                var ctContasPagar = new ContasPagarController();
                var cp = ctContasPagar.GetById(baixa.ContasPagarId.GetValueOrDefault());

                foreach (var c in baixa.Cheques)
                {
                    if (c.TipoEmitenteCheque == 2) //Empresa
                    {

                        if (estornarLote)
                        {
                            saldo.UpdateSaldo(c.BancoMovimentacaoId.GetValueOrDefault(), 1, c.ValorCompensado.GetValueOrDefault());

                            var MovBanco = new MovimentacaoBanco();

                            MovBanco.Id = 0;
                            MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            MovBanco.IdBanco = c.BancoMovimentacaoId.GetValueOrDefault();
                            MovBanco.IdContasPagar = null;
                            MovBanco.IdContasReceber = null;
                            MovBanco.IdCheque = c.Id;
                            MovBanco.Tipo = 1;
                            MovBanco.DataMovimento = baixa.DataBaixa;
                            MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            MovBanco.Valor = c.Valor;
                            MovBanco.Observacao = "Crédito realizado pelo estorno em lote dos titulos " + titulosLote + " com o cheque " + c.Referencia;
                            new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                        }

                        else if (cp.IdTipoDocumento != 7 && c.BancoMovimentacaoId.GetValueOrDefault() > 0 && !c.DeTerceiro)
                        {
                            saldo.UpdateSaldo(c.BancoMovimentacaoId.GetValueOrDefault(), 1, c.ValorCompensado.GetValueOrDefault());

                            var MovBanco = new MovimentacaoBanco();

                            MovBanco.Id = 0;
                            MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            MovBanco.IdBanco = c.BancoMovimentacaoId.GetValueOrDefault();
                            MovBanco.IdContasPagar = cp.Id;
                            MovBanco.IdContasReceber = null;
                            MovBanco.IdCheque = c.Id;
                            MovBanco.Tipo = 1;
                            MovBanco.DataMovimento = baixa.DataBaixa;
                            MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            MovBanco.Valor = c.Valor;
                            MovBanco.Observacao = "Crédito realizado pelo estorno do titulo " + cp.NumTitulo + " com o cheque " + c.Referencia;
                            new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                        }
                        
                        chequeController.Delete(c.Id);
                    }
                    else
                    {
                        var ch = c;
                        c.ContasPagarBaixaId = null;
                        chequeController.Save(ref ch);

                        ChequeView cheque = chequeController.GetViewById(c.Id);
                        cheque.ContasPagarBaixaId = null;
                        chequeController.CompensarCheque(cheque);
                    }
                }

                if (baixa.ContasPagarId.GetValueOrDefault() > 0)
                {
                    if (!gerouCredito)
                    {
                        cp.Saldo = (cp.Saldo + (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito));
                        cp.ValorPago = (cp.ValorPago - (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito));
                    }
                    else
                    {
                        cp.Saldo += (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito - valorCreditoGerado);
                        cp.ValorPago -= (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito - valorCreditoGerado);
                        //cr.Saldo = (cr.ValorParcela + cr.Juros - cr.Desconto);
                        //cr.ValorPago = 0;
                    }

                    if (cp.ValorPago > (cp.ValorParcela + cp.Juros - cp.Desconto))
                        cp.Saldo = (cp.ValorParcela + cp.Juros - cp.Desconto);

                    if (cp.Saldo < 0)
                        cp.Saldo = 0;

                    if (cp.ValorPago == 0)
                        cp.DataPagamento = null;

                    ctContasPagar.Save(ref cp);
                }


                if (estornarLote)
                {
                    // se for Nota de débito do fornecedor não movimenta o banco 
                    if (baixa.ValorDinheiro > 0)
                    {
                        saldo.UpdateSaldo(baixa.BancoId.GetValueOrDefault(), 1, baixa.ValorDinheiro);

                        var MovBanco = new MovimentacaoBanco();

                        string dia = DateTime.Now.Day.ToString("d2");//duas casas, preenche com zero esquerda
                        string mes = DateTime.Now.Month.ToString("d2");
                        string ano = DateTime.Now.Year.ToString();
                        string DataMovimento = dia + "/" + mes + "/" + ano;

                        MovBanco.Id = 0;
                        MovBanco.IdBanco = baixa.BancoId.GetValueOrDefault();
                        MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                        MovBanco.IdContasPagar = null;
                        MovBanco.IdContasReceber = null;
                        MovBanco.IdCheque = null;
                        MovBanco.Tipo = 1;
                        MovBanco.DataMovimento = Convert.ToDateTime(DataMovimento);
                        MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                        MovBanco.Valor = baixa.ValorDinheiro;
                        MovBanco.Observacao = "Crédito realizado pelo estorno do lote dos titulos  " + titulosLote;
                        new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                    }
                }
                //Movimenta saldo do banco
                else if (movimentarSaldo && cp.IdBanco != null)
                {
                    // se for Nota de débito do fornecedor não movimenta o banco 
                    if (cp.IdTipoDocumento != 7 && baixa.ValorDinheiro > 0)
                    {
                        saldo.UpdateSaldo(cp.IdBanco.GetValueOrDefault(), 1, baixa.ValorDinheiro);

                        var MovBanco = new MovimentacaoBanco();

                        string dia = DateTime.Now.Day.ToString("d2");//duas casas, preenche com zero esquerda
                        string mes = DateTime.Now.Month.ToString("d2");
                        string ano = DateTime.Now.Year.ToString();
                        string DataMovimento = dia + "/" + mes + "/" + ano;

                        MovBanco.Id = 0;
                        MovBanco.IdBanco = cp.IdBanco.GetValueOrDefault();
                        MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                        MovBanco.IdContasPagar = cp.Id;
                        MovBanco.IdContasReceber = null;
                        MovBanco.IdCheque = null;
                        MovBanco.Tipo = 1;
                        MovBanco.DataMovimento = Convert.ToDateTime(DataMovimento);
                        MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                        MovBanco.Valor = baixa.ValorDinheiro;
                        MovBanco.Observacao = "Crédito realizado pelo estorno do titulo " + cp.NumTitulo;
                        new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                    }
                }


                base.Delete(id);

                //atualiza despesas fixas variáveis
                if(baixa.ContasPagarId.GetValueOrDefault() > 0)
                    new DespesaFixaVariavelController().AtualizaDespesasByNaturezasFinanceiras(baixa.DataBaixa.Year, baixa.DataBaixa.Month, cp.IdNaturezaFinanceira);

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

        public List<ContasPagarBaixa> GetByContasPagar(int ContasPagarId)
        {
            using (ContasPagarBaixaRepository repository = new ContasPagarBaixaRepository())
            {
                return repository.GetByContasPagar(ContasPagarId);
            }
        }

        public override ContasPagarBaixa GetById(int id)
        {
            var baixa = base.GetById(id);
            
            if (baixa != null)
            {
                using (var chequeRepository = new ChequeRepository())
                {
                    baixa.Cheques = chequeRepository.GetByContasPagarBaixa(id).ToList();
                }

                using (var ContasPagarRepository = new ContasPagarRepository())
                {
                    baixa.ContasPagar = ContasPagarRepository.GetViewById(baixa.ContasPagarId.GetValueOrDefault());
                }

                using (var creditoFornecedorRepository = new CreditoFornecedorRepository())
                {
                    baixa.Creditos = creditoFornecedorRepository.GetByContasPagarBaixa(id);
                }
            }

            return baixa;
        }

    }
}
