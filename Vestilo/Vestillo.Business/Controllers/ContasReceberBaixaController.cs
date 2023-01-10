using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;
using Vestillo.Lib;

namespace Vestillo.Business.Controllers
{
    public class ContasReceberBaixaController : GenericController<ContasReceberBaixa, ContasReceberBaixaRepository>
    {
        string NumTitulos = string.Empty;

        public override void Save(ref ContasReceberBaixa entity)
        {
            this.Save(ref entity, true, 0, 0);
        }

        public void Save(ref ContasReceberBaixa entity, decimal valorCredito)
        {
            this.Save(ref entity, true, valorCredito, 0);
        }

        public void Save(ref ContasReceberBaixa entity, bool movimentarSalvo)
        {
            this.Save(ref entity, movimentarSalvo, 0, 0);
        }


        public void BaixaEmLote(IEnumerable<TitulosBaixaLoteView> parcelasBaixa, IEnumerable<Cheque> cheques = null, decimal valorDinheiro = 0, decimal valorCreditoGerar = 0, int clienteId = 0)
        {
            try
            {
                int idCliente = 0;
                var repositoryContasReceber = new ContasReceberRepository();
                var ControllerContasReceber = new ContasReceberController();


                int? contasReceberBaixaLoteId = null;
                ContasReceberBaixa baixaLote = new ContasReceberBaixa();
                baixaLote.Cheques = cheques.ToList();
                baixaLote.ValorDinheiro = valorDinheiro;
                baixaLote.ValorCheque = cheques.Sum(x => x.Valor);
                baixaLote.Obs = string.Join(", ", parcelasBaixa.Select(x => x.NumTitulo).ToArray());
                baixaLote.DataBaixa = DateTime.Now;
                baixaLote.BancoId = parcelasBaixa.First().BancoMovimentacaoId;
                baixaLote.Lote = true;
                this.Save(ref baixaLote, true, valorCreditoGerar, clienteId);
                contasReceberBaixaLoteId = baixaLote.Id;

                               

                foreach (TitulosBaixaLoteView parcelaLote in parcelasBaixa)
                {
                    ContasReceber parcela = repositoryContasReceber.GetById(parcelaLote.Id);
                    ContasReceberBaixa baixa = new ContasReceberBaixa();
                    baixa.ContasReceberId = parcela.Id;
                    baixa.DataBaixa = DateTime.Now;
                    baixa.Obs = "Baixado em lote";
                    baixa.ValorDinheiro = (parcela.ValorParcela + parcela.Juros - parcela.Desconto - parcela.ValorPago);
                    baixa.Cheques = new List<Cheque>();
                    baixa.ContasReceberBaixaLoteId = contasReceberBaixaLoteId;
                    this.Save(ref baixa, false, 0, 0);
                    if (parcela.IdCliente != null)
                    {
                        idCliente = Convert.ToInt32(parcela.IdCliente);
                    }
                    
                }

                if(idCliente > 0)
                {
                    ControllerContasReceber.AtualizaDadosDeCompra(idCliente);

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
                throw ex;
            }
        }

        public void EstornarLote(IEnumerable<TitulosBaixaLoteView> parcelas, int contasReceberBaixaLoteId)
        {
            try
            {
                foreach (TitulosBaixaLoteView parcelaLote in parcelas)
                {
                    this.Delete(parcelaLote.BaixaId, false, false, "");
                }

                string titulos = string.Join(", ", parcelas.Select(x => x.NumTitulo).ToArray());
                this.Delete(contasReceberBaixaLoteId, true, true, titulos);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Save(ref ContasReceberBaixa contasReceberBaixa, bool movimentarSaldo, decimal valorCredito, int clienteId)
        {
            var repository = new ContasReceberBaixaRepository();
            int idCliente = 0;

            try
            {
                repository.BeginTransaction();

                base.Save(ref contasReceberBaixa);
                BancoRepository saldo = new BancoRepository();
                var ctContasReceber = new ContasReceberController();
                var cr = ctContasReceber.GetById(contasReceberBaixa.ContasReceberId.GetValueOrDefault());
                var creditoClienteController = new CreditosClientesController();
                ChequeController chequeController = new ChequeController();

                if (cr != null)
                {
                    if (cr.IdCliente != null)
                    {
                        idCliente = Convert.ToInt32(cr.IdCliente);
                    }
                }
                else
                {
                    if (clienteId > 0)
                    {
                        idCliente = clienteId;
                    }

                }

                contasReceberBaixa.Cheques = contasReceberBaixa.Cheques ?? new List<Cheque>();

                foreach (var c in contasReceberBaixa.Cheques)
                {
                    Cheque cheque = c;

                    if (cheque.BancoMovimentacaoId.GetValueOrDefault() == 0)
                    {
                        if (contasReceberBaixa.Lote)
                            cheque.BancoMovimentacaoId = contasReceberBaixa.BancoId.GetValueOrDefault();
                        else
                            cheque.BancoMovimentacaoId = cr.IdBanco;
                    }

                    if (cheque.TipoEmitenteCheque == 0)
                        cheque.TipoEmitenteCheque = 1;

                    cheque.Status = (int)Cheque.enumStatus.Incluido;
                    cheque.ContasReceberBaixaId = contasReceberBaixa.Id;
                    chequeController.Save(ref cheque);
                }

                contasReceberBaixa.Creditos = contasReceberBaixa.Creditos ?? new List<CreditosClientes>();
                
                foreach (var c in contasReceberBaixa.Creditos)
                {
                    CreditosClientes credito = creditoClienteController.GetById(c.Id);

                    credito.ContasReceberBaixaId = contasReceberBaixa.Id;
                    credito.ObsQuitacao = "Crédito utilizado na baixa do título: " + cr.NumTitulo + ".";
                    credito.Status = 2; //Quitado
                    credito.dataquitacao = contasReceberBaixa.DataBaixa;
                    
                    creditoClienteController.Save(ref credito);
                }


                //CRIA NOVA PARCELA DA BAIXA
                if (contasReceberBaixa.RedefiniuBaixa == 1)
                {
                    using (ContasReceberRepository parcelasRepository = new ContasReceberRepository())
                    {
                        int contadorParcela = 0;
                        foreach (var ctr in contasReceberBaixa.ParcelasCtr)
                        {
                            contadorParcela += 1;
                            ContasReceber c = ctr;
                            c.Id = 0;
                            c.IdTituloPai = contasReceberBaixa.ContasReceberId;
                            c.IdFatNfe = null;
                            c.NumTitulo = cr.NumTitulo.ToString() + "." + contadorParcela.ToString();
                            c.Obs = ctr.Obs;
                            parcelasRepository.Save(ref c);
                            NumTitulos += c.NumTitulo + ",";

                            //realiza a baixa do titulo caso o tipo de documento seja = 1 *Dinheiro
                            if (c.IdTipoDocumento == 1)
                            {
                                ContasReceberBaixa dadosBaixa = new ContasReceberBaixa();

                                dadosBaixa.Id = 0;
                                dadosBaixa.BancoId = c.IdBanco;
                                dadosBaixa.BorderoId = null;
                                dadosBaixa.Cheques = null;
                                dadosBaixa.ContasReceberId = c.Id;
                                dadosBaixa.ContasReceberBaixaLoteId = null;
                                dadosBaixa.DataBaixa = c.DataEmissao;
                                dadosBaixa.Lote = false;
                                dadosBaixa.Obs = "Titulo baixado pelo negociação: " + cr.NumTitulo.ToString() + ".1" + " Parcela: " + c.Parcela;
                                dadosBaixa.ValorCheque = 0;
                                dadosBaixa.ValorCredito = 0;
                                dadosBaixa.ValorDinheiro = c.ValorParcela;
                                new ContasReceberBaixaService().GetServiceFactory().Save(ref dadosBaixa);

                            }


                            if (ctr.ParcelaDetalhes != null)
                            {
                                if (ctr.ParcelaDetalhes.TipoDocumentoId.ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cheque)
                                {


                                    Cheque cheque = ctr.ParcelaDetalhes.Cheque;
                                    cheque.DataEmissao = c.DataEmissao;
                                    cheque.TipoEmitenteCheque = 1;
                                    cheque.NaturezaFinanceiraId = c.IdNaturezaFinanceira;
                                    cheque.ColaboradorId = c.IdCliente.GetValueOrDefault();
                                    cheque.Referencia = c.NumTitulo.ToString();
                                    cheque.NFeId = null;
                                    cheque.Valor = c.ValorParcela;

                                    //cheque.ContasReceberBaixaId = dadosBaixa.Id; //testar

                                    ChequeController chequeControllerBaixa = new ChequeController();
                                    chequeControllerBaixa.Save(ref cheque, "Cheque gerado pelo Faturamento: " + c.NumTitulo.ToString() + " Parcela: " + c.Parcela + " - " + ctr.Obs);

                                }
                                else if (ctr.ParcelaDetalhes.CreditoClienteId > 0)
                                {
                                    var creditoClienteControllerBaixa = new CreditosClientesController();
                                    CreditosClientes credito = creditoClienteControllerBaixa.GetById(ctr.ParcelaDetalhes.CreditoClienteId);

                                    credito.ContasReceberBaixaId = ctr.Id;
                                    credito.ObsQuitacao = "Crédito utilizado na baixa do título: " + cr.NumTitulo.ToString() + ".1";
                                    credito.Status = 2; //Quitado
                                    credito.dataquitacao = DateTime.Now.Date;
                                    creditoClienteController.Save(ref credito);
                                }
                            }
                        }
                    }
                }
                //fim NOVA PARCEL DA BAIXA





                if (contasReceberBaixa.ContasReceberId.GetValueOrDefault() > 0)
                {
                    if (cr.Saldo == 0)
                        cr.Saldo = cr.ValorParcela + cr.Juros - cr.Desconto;

                    if (valorCredito > 0)
                    {
                        cr.Saldo = 0;
                        cr.ValorPago = cr.ValorParcela + cr.Juros - cr.Desconto;
                    }
                    else
                    {
                        cr.Saldo = (cr.Saldo - (contasReceberBaixa.ValorCheque + contasReceberBaixa.ValorDinheiro + contasReceberBaixa.ValorCredito.GetValueOrDefault()));
                        cr.ValorPago = (cr.ValorPago + contasReceberBaixa.ValorCheque + contasReceberBaixa.ValorDinheiro + contasReceberBaixa.ValorCredito.GetValueOrDefault());
                    }
                    cr.DataPagamento = contasReceberBaixa.DataBaixa;
                    
                    if(contasReceberBaixa.BancoId.GetValueOrDefault() > 0)
                        cr.IdBanco = contasReceberBaixa.BancoId.GetValueOrDefault();

                    if (cr.IdNaturezaFinanceira == 0)
                        cr.IdNaturezaFinanceira = null;

                    if (cr.IdTipoDocumento == 0)
                        cr.IdTipoDocumento = null;

                    if (contasReceberBaixa.RedefiniuBaixa == 1)
                    {
                        if (NumTitulos.Length != 0)
                        {
                            NumTitulos.Remove(NumTitulos.ToString().Length - 1, 1);
                        }
                        cr.Prefixo = "NEG";
                        cr.Obs += "@NEG-Alterado o tipo de baixa " + " * Titulos Gerados" + NumTitulos  ;
                        cr.DataPagamento = null;
                        cr.Status = (int)enumStatusContasReceber.Negociado;
                        cr.ValorPago = 0;
                        cr.Saldo = 0;
                    }



                    ctContasReceber.Save(ref cr);
                }

                if (contasReceberBaixa.ValorDinheiro > 0 && contasReceberBaixa.RedefiniuBaixa != 1)
                {
                    //Movimenta saldo do banco
                    if (contasReceberBaixa.Lote)
                    {
                        saldo.UpdateSaldo(contasReceberBaixa.BancoId.GetValueOrDefault(), 1, contasReceberBaixa.ValorDinheiro);

                        var MovBanco = new MovimentacaoBanco();

                        MovBanco.Id = 0;
                        MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                        MovBanco.IdBanco = contasReceberBaixa.BancoId.GetValueOrDefault();
                        MovBanco.IdContasPagar = null;
                        MovBanco.IdContasReceber = null;
                        MovBanco.IdCheque = null;
                        MovBanco.Tipo = 1;
                        MovBanco.DataMovimento = contasReceberBaixa.DataBaixa;
                        MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                        MovBanco.Valor = contasReceberBaixa.ValorDinheiro;
                        MovBanco.Observacao = "Crédito realizado pela baixa em lote dos títulos " + contasReceberBaixa.Obs + ".";
                        new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                    }
                    else if (movimentarSaldo && cr.IdBanco != null && contasReceberBaixa.ValorDinheiro > 0)
                    {

                        // se for Nota de débito do fornecedor não movimenta o banco JAMAICA
                        if (cr.IdTipoDocumento != 4)
                        {
                            saldo.UpdateSaldo((int)cr.IdBanco, 1, contasReceberBaixa.ValorDinheiro);

                            var MovBanco = new MovimentacaoBanco();

                            MovBanco.Id = 0;
                            MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            MovBanco.IdBanco = (int)cr.IdBanco;
                            MovBanco.IdContasPagar = null;
                            MovBanco.IdContasReceber = cr.Id;
                            MovBanco.IdCheque = null;
                            MovBanco.Tipo = 1;
                            MovBanco.DataMovimento = contasReceberBaixa.DataBaixa;
                            MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            MovBanco.Valor = contasReceberBaixa.ValorDinheiro;
                            MovBanco.Observacao = "Crédito realizado pela baixa do título " + cr.NumTitulo + ", parcela: " + cr.Parcela;
                            new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                        }

                    }
                }

                // Geração de crédito pro cliente na baixa.
                if (valorCredito > 0)
                {
                    var creditoCliente = new CreditosClientes();
                    creditoCliente.Ativo = true;
                    creditoCliente.dataemissao = DateTime.Now;
                    if (cr != null)
                    {
                        creditoCliente.idcolaborador = cr.IdCliente.GetValueOrDefault();
                        creditoCliente.ObsInclusao = "Gerado através da baixa do título: " + cr.NumTitulo + ".";
                    }
                    else
                    {
                        creditoCliente.ObsInclusao = "Gerado através da baixa em lote dos títulos " + contasReceberBaixa.Obs + ".";
                        creditoCliente.idcolaborador = clienteId;
                    }
                    creditoCliente.Status = (int)enumStatusCreditoCliente.Aberto;
                    creditoCliente.valor = valorCredito;
                    creditoCliente.ContasReceberBaixaId = contasReceberBaixa.Id;
                    creditoCliente.GeradoPeloContasReceber = 1;
                    creditoCliente.ContasReceberQueGerouCreditoId = contasReceberBaixa.ContasReceberId;
                    creditoClienteController.Save(ref creditoCliente);
                }

                repository.CommitTransaction();

                if (idCliente > 0)
                {
                    ctContasReceber.AtualizaDadosDeCompra(idCliente);

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
            var repository = new ContasReceberBaixaRepository();


            try
            {
                bool gerouCredito = false;

                decimal valorCreditoGerado = 0;

                repository.BeginTransaction();

                ContasReceberBaixa baixa = this.GetById(id);

                CreditosClientesController creditoClienteController = new CreditosClientesController();
                
                IEnumerable<CreditosClientes> creditosGerados = creditoClienteController.GetByContasReceberQueGerou(baixa.ContasReceberId.GetValueOrDefault());

                if (creditosGerados != null && creditosGerados.Count() > 0)
                {
                    if (creditosGerados.Where(x => x.Status != 1).Count() > 0)
                    {
                        throw new Exception("O contas a receber não pode ser estornado pois o crédito gerado na baixa foi utilizado!");
                    }

                    foreach (CreditosClientes credito in creditosGerados)
                    {
                        if (credito.GeradoPeloContasReceber.GetValueOrDefault() == 1 && credito.ContasReceberQueGerouCreditoId.GetValueOrDefault() == baixa.ContasReceberId)
                        {
                            valorCreditoGerado += credito.valor;
                            creditoClienteController.Delete(credito.Id);
                            gerouCredito = true;
                        }
                    }
                }

                if (baixa.Creditos.Count() > 0)
                {
                    foreach (CreditosClientes credito in baixa.Creditos)
                    {

                        if (credito.GeradoPeloContasReceber.GetValueOrDefault() == 1 && credito.ContasReceberQueGerouCreditoId.GetValueOrDefault() == baixa.ContasReceberId)
                        {
                            creditoClienteController.Delete(credito.Id);
                            gerouCredito = true;
                        }
                        else
                        {
                            CreditosClientes creditoUsado = credito;
                            creditoUsado.Status = 1;
                            creditoUsado.dataquitacao = null;
                            creditoUsado.ObsInclusao = null;
                            creditoUsado.ContasReceberBaixaId = null;
                            creditoClienteController.Save(ref creditoUsado);
                        }
                    }
                }

                var repositoryCheque = new ChequeRepository();
                foreach (var c in baixa.Cheques)
                {
                    Cheque cheque = c;

                    if (cheque.Status == 0 || cheque.Status == (int)Cheque.enumStatus.Incluido)
                    {
                        repositoryCheque.Delete(cheque.Id);
                    }
                    else
                    {
                        cheque.ContasReceberBaixaId = null;
                        repositoryCheque.Save(ref cheque);
                    }
                }

                var ctContasReceber = new ContasReceberController();
                ContasReceber cr = null;
                int idCliente = 0;

                if (baixa.ContasReceberId.GetValueOrDefault() > 0)
                {
                    cr = ctContasReceber.GetById(baixa.ContasReceberId.GetValueOrDefault());

                    if(cr != null)
                    {
                        if (cr.IdCliente != null)
                        {
                            idCliente = Convert.ToInt32(cr.IdCliente);
                        }
                        
                    }
                    

                    if (!gerouCredito)
                    {
                        cr.Saldo = (cr.Saldo + (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito.GetValueOrDefault()));
                        cr.ValorPago = (cr.ValorPago - (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito.GetValueOrDefault()));
                    }
                    else
                    {
                        cr.Saldo += (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito.GetValueOrDefault() - valorCreditoGerado);
                        cr.ValorPago -= (baixa.ValorCheque + baixa.ValorDinheiro + baixa.ValorCredito.GetValueOrDefault() - valorCreditoGerado);
                        //cr.Saldo = (cr.ValorParcela + cr.Juros - cr.Desconto);
                        //cr.ValorPago = 0;
                    }

                    if (cr.ValorPago > (cr.ValorParcela + cr.Juros - cr.Desconto))
                        cr.Saldo = (cr.ValorParcela + cr.Juros - cr.Desconto);

                    if (cr.Saldo < 0)
                        cr.Saldo = 0;

                    if (cr.ValorPago == 0)
                        cr.DataPagamento = null;

                    ctContasReceber.Save(ref cr);
                }

                if (movimentarSaldo && baixa.ValorDinheiro > 0)
                {
                    if (estornarLote)
                    {
                        BancoRepository saldo = new BancoRepository();

                        saldo.UpdateSaldo(baixa.BancoId.GetValueOrDefault(), 2, baixa.ValorDinheiro); 

                        var MovBanco = new MovimentacaoBanco();

                        string dia = DateTime.Now.Day.ToString("d2");//duas casas, preenche com zero esquerda
                        string mes = DateTime.Now.Month.ToString("d2");
                        string ano = DateTime.Now.Year.ToString();
                        string DataMovimento = dia + "/" + mes + "/" + ano;

                        MovBanco.Id = 0;
                        MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                        MovBanco.IdBanco = baixa.BancoId.GetValueOrDefault();
                        MovBanco.IdContasPagar = null;
                        MovBanco.IdContasReceber = null;
                        MovBanco.IdCheque = null;
                        MovBanco.Tipo = 2;
                        MovBanco.DataMovimento = Convert.ToDateTime(DataMovimento);
                        MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                        MovBanco.Valor = baixa.ValorDinheiro;
                        MovBanco.Observacao = "Débito realizado pelo estorno em lote dos titulo " + titulosLote;
                        new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                        
                    }
                    //Movimenta saldo do banco
                    else if (cr.IdBanco != null)
                    {
                        BancoRepository saldo = new BancoRepository();
                        // se for Nota de crédito do cliente não movimenta o banco JAMAICA
                        if (cr.IdTipoDocumento != 7 && baixa.ValorDinheiro > 0)
                        {
                            saldo.UpdateSaldo((int)cr.IdBanco, 2, baixa.ValorDinheiro); //JAMAICA

                            var MovBanco = new MovimentacaoBanco();

                            string dia = DateTime.Now.Day.ToString("d2");//duas casas, preenche com zero esquerda
                            string mes = DateTime.Now.Month.ToString("d2");
                            string ano = DateTime.Now.Year.ToString();
                            string DataMovimento = dia + "/" + mes + "/" + ano;

                            MovBanco.Id = 0;
                            MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            MovBanco.IdBanco = (int)cr.IdBanco;
                            MovBanco.IdContasPagar = null;
                            MovBanco.IdContasReceber = cr.Id;
                            MovBanco.IdCheque = null;
                            MovBanco.Tipo = 2;
                            MovBanco.DataMovimento = Convert.ToDateTime(DataMovimento);
                            MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            MovBanco.Valor = baixa.ValorDinheiro;
                            MovBanco.Observacao = "Débito realizado pelo estorno do titulo " + cr.NumTitulo + " - Parcela: " + cr.Parcela;
                            new MovimentacaoBancoService().GetServiceFactory().Save(ref MovBanco);
                        }
                    }
                }

                base.Delete(id);

                repository.CommitTransaction();

                if (idCliente > 0)
                {
                    ctContasReceber.AtualizaDadosDeCompra(idCliente);
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
                repository.RollbackTransaction();
                throw new Vestillo.Lib.VestilloException(ex);
            }
            finally
            {
                repository.Dispose();
                repository = null;
            }
        }

        public List<ContasReceberBaixa> GetByContasReceber(int contasReceberId)
        {
            using (ContasReceberBaixaRepository repository = new ContasReceberBaixaRepository())
            {
                return repository.GetByContasReceber(contasReceberId);
            }
        }

        public List<ContasReceberBaixa> GetByContasReceberEBordero(int contasReceberId, int borderoCobrancaId)
        {
            using (ContasReceberBaixaRepository repository = new ContasReceberBaixaRepository())
            {
                return repository.GetByContasReceberEBordero(contasReceberId, borderoCobrancaId);
            }
        }

        public override ContasReceberBaixa GetById(int id)
        {
            var baixa = base.GetById(id);
            if (baixa != null)
            {
                using (var chequeRepository = new ChequeRepository())
                {
                    baixa.Cheques = chequeRepository.GetByContasReceberBaixa(id).ToList();
                }

                using (var contasReceberRepository = new ContasReceberRepository())
                {
                    baixa.ContasReceber = contasReceberRepository.GetViewById(baixa.ContasReceberId.GetValueOrDefault());
                }

                using (var creditoClienteRepository = new CreditosClientesRepository())
                {
                    baixa.Creditos = creditoClienteRepository.GetByContasReceberBaixa(id);
                }
            }

            return baixa;
        }

    }
}
