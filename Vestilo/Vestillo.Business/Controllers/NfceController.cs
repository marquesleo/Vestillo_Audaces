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
    public class NfceController : GenericController<Nfce, NfceRepository>
    {
        public IEnumerable<NfceView> GetCamposDeterminados(string parametrosDaBusca)
        {
            using (NfceRepository repository = new NfceRepository())
            {
                return repository.GetCamposDeterminados(parametrosDaBusca);
            }
        }

        public IEnumerable<NfceView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            using (NfceRepository repository = new NfceRepository())
            {
                return repository.GetListPorReferencia(referencia, parametrosDaBusca);
            }
        }

        public IEnumerable<NfceView> GetListPorObservacao(string observacao, string parametrosDaBusca)
        {
            using (NfceRepository repository = new NfceRepository())
            {
                return repository.GetListPorObservacao(observacao, parametrosDaBusca);
            }
        }

        public IEnumerable<NfceView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            using (NfceRepository repository = new NfceRepository())
            {
                return repository.GetListPorNumero(Numero, parametrosDaBusca);
            }
        }

        public override Nfce GetById(int id)
        {
            Nfce result = base.GetById(id);

            if (result != null)
            {
                ContasReceberController contasReceberController = new ContasReceberController();
                result.ParcelasCtr = contasReceberController.GetByNotaConsumidor(id);

                result.CreditosClientes = new CreditosClientesController().GetByNotaConsumidorQuitado(id);
            }
            
            return result;
        }

        public override void Save(ref Nfce entity)
        {
            Save(ref entity, false);
        }

        public void Save(ref Nfce nfce, bool precoProduto)
        {
            bool openTransaction = _repository.BeginTransaction();

            using (NfceRepository repository = _repository)
            {
                try
                {
                    bool inclusao = nfce.Id == 0;
                    bool gravarParcelas = nfce.Id == 0; //grava parcelas na inclusão
                    bool excluirParcela = false;

                    //testando a alteração da nota
                    if (!inclusao)
                    {
                        var nfceAntigo = GetById(nfce.Id);

                        var parcelaExcluir = nfce.ParcelasCtr.Where(p => p.Excluir).FirstOrDefault();
                        if (parcelaExcluir != null) excluirParcela = true;

                        if (nfceAntigo.Total != nfce.Total || excluirParcela)
                        {
                            AlterarStatusCredito(nfce.Id);
                            ExcluirContasReceber(nfce.Id);                            
                            gravarParcelas = true; //grava parcelas caso houve alteração na nota
                        }
                        else if (nfceAntigo.ParcelasCtr.Count() < nfce.ParcelasCtr.Count()) // não tinha parcelas, mas foi incluído
                            gravarParcelas = true;
                    }

                    //PEGA A REFERENCIA
                    base.Save(ref nfce);

                    //Gravação das parcelas e Crédito usado
                    if (gravarParcelas)
                    {
                        using (ContasReceberRepository parcelasRepository = new ContasReceberRepository())
                        {
                            var parcelas = nfce.ParcelasCtr.Where(p => !p.Excluir);
                            if (parcelas != null && parcelas.Count() > 0)
                                GravarParcelas(nfce.Referencia, nfce.Id, parcelas);
                        }

                        if (nfce.CreditosClientes != null && nfce.CreditosClientes.Count() > 0)
                        {
                            foreach (var credito in nfce.CreditosClientes)
                            {
                                CreditosClientes c = credito;
                                c.ObsQuitacao = "Crédito realizado pela Nota de Consumidor: " + nfce.Referencia ;
                                c.Status = 2; //Quitado
                                c.IdNfceQuitado = nfce.Id;
                                c.dataquitacao = DateTime.Now.Date;
                                new CreditosClientesController().Save(ref c);
                                
                            }                            
                        }                       

                    }   

                    //grava os itens da Nfce
                    using (NfceItensRepository gradeRepository = new NfceItensRepository())
                    {
                        var grades = gradeRepository.GetListByNfce(nfce.Id);

                        foreach (var g in grades)
                        {
                            gradeRepository.Delete(g.Id);
                        }

                        var itensMovimentacaoEstoque = new List<MovimentacaoEstoque>();

                        foreach (var gr in nfce.GradeItens)
                        {
                            NfceItens g = gr;
                            g.Id = 0;
                            g.IdNfce = nfce.Id;
                            gradeRepository.Save(ref g);
                            
                        }
                        
                    }

                    //Movimenta itens no estoque
                    var estoqueController = new EstoqueController();
                    if (nfce.ItensMovimentacaoEstoque != null)
                    {
                        foreach (var item in nfce.ItensMovimentacaoEstoque)
                        {
                            if (item.Observacao == "" || item.Observacao == null)
                            {
                                if (item.Saida > 0)
                                {
                                    item.Observacao = "SAÍDA PELO NFCE: " + nfce.Referencia.ToString();
                                }
                                else
                                {
                                    item.Observacao = "ENTRADA PELO NFCE: " + nfce.Referencia.ToString();
                                }
                            }

                        }

                        estoqueController.MovimentarEstoque(nfce.ItensMovimentacaoEstoque, true);
                    }

                    //Gravação da movimentação do caixa
                    if (nfce.ParcelasCtr != null && nfce.ParcelasCtr.Count() > 0)
                        GravarMovimentacaoCaixa(nfce.Referencia, nfce.Id, nfce.ParcelasCtr.Where(p => !p.Excluir));
                    else
                        GravarMovimentacaoCaixa(nfce.Referencia, nfce.Id);

                    if (VerificaReferenciaDuplicada(nfce))
                    {
                        var contador = new ContadorCodigoController().GetProximo("Nfce");
                        nfce.GetType().GetProperty("Referencia").SetValue(nfce, contador, null);
                    }

                    base.Save(ref nfce);

                    if (openTransaction)
                        repository.CommitTransaction();
                }
                catch (Exception ex)
                {
                    if (openTransaction)
                        repository.RollbackTransaction();
        
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        private bool VerificaReferenciaDuplicada(Nfce nfce)
        {
            List<NfceView> notas = new NfceRepository().GetListPorReferencia(nfce.Referencia, string.Empty).ToList();
            if (notas != null && notas.Count > 0)
            {
                if (notas.Exists(n => nfce.Id != n.Id))
                    return true;
            }
            return false;
        }

        private void GravarParcelas(string referenciaNota, int idNota, IEnumerable<ContasReceber> parcelasNFce)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                BancoRepository bancoRepository = new BancoRepository();
                ContasReceberBaixaRepository contasReceberBaixaRepository = new ContasReceberBaixaRepository();
                CreditosClientesController creditoClienteController = new CreditosClientesController();

                using (ContasReceberRepository parcelasRepository = new ContasReceberRepository())
                {
                    foreach (var ctr in parcelasNFce)
                    {
                        ContasReceber c = ctr;
                        c.Id = 0;
                        c.IdNotaConsumidor = idNota;
                        c.NumTitulo = referenciaNota;
                        c.Ativo = 1;
                        c.Obs = "Título Gerado pela Nota de Consumidor: " + referenciaNota + " - " + ctr.Obs;
                        parcelasRepository.Save(ref c);

                        if (c.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Dinheiro)
                        {
                            bancoRepository.UpdateSaldo(c.IdBanco.GetValueOrDefault(), 1, c.ValorParcela);

                            var movimentacaoBanco = new MovimentacaoBanco();

                            movimentacaoBanco.Id = 0;
                            movimentacaoBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            movimentacaoBanco.IdBanco = c.IdBanco.GetValueOrDefault();
                            movimentacaoBanco.IdContasPagar = null;
                            movimentacaoBanco.IdContasReceber = c.Id;
                            movimentacaoBanco.IdCheque = null;
                            movimentacaoBanco.Tipo = 1;
                            movimentacaoBanco.DataMovimento = c.DataPagamento.GetValueOrDefault(DateTime.Now.Date);
                            movimentacaoBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                            movimentacaoBanco.Valor = c.ValorParcela;
                            movimentacaoBanco.Observacao = "Crédito realizado pela Nota de Consumidor: " + referenciaNota + " - " + ctr.Obs;
                            new MovimentacaoBancoService().GetServiceFactory().Save(ref movimentacaoBanco);

                            ContasReceberBaixa contasReceberBaixa = new ContasReceberBaixa();
                            contasReceberBaixa.BancoId = c.IdBanco.GetValueOrDefault();
                            contasReceberBaixa.ContasReceberId = c.Id;
                            contasReceberBaixa.DataBaixa = c.DataPagamento.GetValueOrDefault(DateTime.Now.Date);
                            contasReceberBaixaRepository.Save(ref contasReceberBaixa);
                        }
                        

                        if (ctr.ParcelaDetalhes != null)
                        {
                            if (ctr.ParcelaDetalhes.TipoDocumentoId.ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cheque)
                            {
                                Cheque cheque = ctr.ParcelaDetalhes.Cheque;
                                if (!string.IsNullOrEmpty(cheque.Banco) && !string.IsNullOrEmpty(cheque.Agencia) && !string.IsNullOrEmpty(cheque.NumeroCheque))
                                {                                    
                                    cheque.DataEmissao = ctr.DataEmissao;
                                    cheque.TipoEmitenteCheque = 1;
                                    cheque.NaturezaFinanceiraId = ctr.IdNaturezaFinanceira;
                                    cheque.ColaboradorId = c.IdCliente.GetValueOrDefault();
                                    cheque.NFCeId = idNota;
                                    cheque.Valor = ctr.ValorParcela;

                                    ChequeController chequeController = new ChequeController();
                                    chequeController.Save(ref cheque, c.Obs);
                                    
                                }
                            }
                        }
                    }
                }

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

        public void ExcluirContasReceber(int idNfce)
        {
            string NOperadoraCred = String.Empty;
            string NOperadoraDeb = String.Empty;

            var nfce = GetById(idNfce);
            //excluir cheques
            var cheques = new ChequeRepository().GetListaPorCampoEValor("NFCeId", idNfce.ToString()).ToList();
            cheques.ForEach(c => new ChequeRepository().Delete(c.Id));

            //excluir contas a receber
            var parcelasRepository = new ContasReceberRepository();
            var ctreceber = parcelasRepository.GetListaPorCampoEValor("IdNotaConsumidor", Convert.ToString(idNfce));

            foreach (var g in ctreceber)
            {                

                var comissao = new ComissoesvendedorService().GetServiceFactory().GetByParcelaCtrDeletar(g.Id);
                if (comissao != null && comissao.Count() > 0)
                {
                    foreach (var item in comissao)
                    {
                        new ComissoesvendedorService().GetServiceFactory().Delete(item.Id);
                    }

                }                

                using (MovimentacaoBancoRepository delete = new MovimentacaoBancoRepository())
                {
                    delete.DeleteByContasReceber(g.Id);
                }

                if (g.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Dinheiro)
                {
                    new BancoRepository().UpdateSaldo(g.IdBanco.GetValueOrDefault(), 2, g.ValorParcela);
                }

                var IdBaixa = new ContasReceberBaixaService().GetServiceFactory().GetByContasReceber(g.Id);
                var creditoRepository = new CreditosClientesRepository();

                foreach (var item in IdBaixa)
                {
                    var creditos = creditoRepository.GetByContasReceberBaixa(item.Id);
                    foreach (var credito in creditos)
                    {
                        if (credito != null)
                        {
                            var c = credito;
                            credito.ContasReceberBaixaId = null;
                            credito.ObsQuitacao = "";
                            credito.Status = 1; //Aberto
                            credito.dataquitacao = null;
                            creditoRepository.Save(ref c);
                        }
                    }

                    new ContasReceberBaixaService().GetServiceFactory().Delete(item.Id);
                }

                new ContasReceberService().GetServiceFactory().Delete(g.Id);

            }
        }

        public void AlterarStatusCredito(int idNfce)
        {
            using (NfceRepository repository = new NfceRepository())
            {
                repository.AlterarStatusCredito(idNfce);
            }
        }

        private void GravarMovimentacaoCaixa(string referenciaNota, int idNota, IEnumerable<ContasReceber> parcelasNFce = null)
        {
            bool openTransaction = false;
            decimal VDinheiro = 0;
            decimal VCCredito = 0;
            decimal VCDebito = 0;
            decimal VCheque = 0;
            decimal VOutros = 0;
            decimal VPixVDep = 0;
            
            string NOperadoraCred = String.Empty;
            string NOperadoraDeb = String.Empty;
            int? IdCliente = 0;

            var dadosNota = new NfceService().GetServiceFactory().GetById(idNota);
            var dadosCliente = new ColaboradorService().GetServiceFactory().GetById(dadosNota.IdCliente);

            if (dadosCliente.TipoCliente == Colaborador.enumTipoCliente.Funcionario)
            {
                IdCliente = dadosNota.IdCliente;
            }

            try
            {
                openTransaction = _repository.BeginTransaction();

                ExcluirTotaisCaixas(idNota);

                if (parcelasNFce != null)
                {
                    foreach (var ctr in parcelasNFce)
                    {
                        if (ctr.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Dinheiro)
                        {
                            VDinheiro += ctr.ValorParcela;
                        }
                        else if (ctr.ParcelaDetalhes.TipoDocumentoId.ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cheque)
                        {
                            VCheque += ctr.ValorParcela;
                        }
                        else if (ctr.ParcelaDetalhes.TipoDocumentoId.ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cartão_de_Credito)
                        {
                            if (ctr.IdCliente == null)
                            {
                                VCCredito += ctr.ValorParcela;
                                NOperadoraCred += "-" + ctr.NomeCliente;

                            }
                        }
                        else if (ctr.ParcelaDetalhes.TipoDocumentoId.ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cartão_de_Debito)
                        {
                            if (ctr.IdCliente == null)
                            {
                                VCDebito += ctr.ValorParcela;
                                NOperadoraDeb += "-" + ctr.NomeCliente;
                            }
                        }
                        else if (ctr.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Deposito)
                        {
                            VPixVDep += ctr.ValorParcela;
                        }
                        else if (ctr.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Pix)
                        {
                            VPixVDep += ctr.ValorParcela;
                        }
                        else
                        {
                            VOutros += ctr.ValorParcela;
                        }
                    }
                }


                if(VestilloSession.DefineCaixaPadrao > 0)
                {
                    var CaixaDaEmpresa = new CaixasService().GetServiceFactory().GetById(VestilloSession.DefineCaixaPadrao);
                    if (CaixaDaEmpresa != null)
                    {
                        if(CaixaDaEmpresa.IdEmpresa == VestilloSession.EmpresaLogada.Id)
                        {
                            //VDinheiro = VDinheiro - dadosNota.Troco;

                            var TCx = new TotaisCaixas();
                            TCx.Id = 0;
                            TCx.idcaixa = VestilloSession.DefineCaixaPadrao;
                            TCx.Idcolaborador = IdCliente == 0 ? null : IdCliente;
                            TCx.datamovimento = DateTime.Now;
                            TCx.tipo = (int)enumTipoMovimentoCaixa.Credito;
                            TCx.dinheiro = VDinheiro;
                            TCx.cartaocredito = VCCredito;
                            TCx.cartaodebito = VCDebito;
                            TCx.cheque = VCheque;
                            TCx.outros = VOutros;
                            TCx.PixDep = VPixVDep;
                            TCx.operadoracredito = NOperadoraCred;
                            TCx.operadoradebito = NOperadoraDeb;
                            TCx.idNfce = dadosNota.Id;

                            TCx.observacao = "Entrada realizada pela NFCe." + referenciaNota;
                            new TotaisCaixasService().GetServiceFactory().Save(ref TCx);

                            using (TotaisCaixasRepository update = new TotaisCaixasRepository())
                            {
                                update.UpdateSaldo(VestilloSession.DefineCaixaPadrao, (int)enumTipoMovimentoCaixa.Credito, VDinheiro);
                            }

                            var pendencia = new Pendencias();
                            pendencia.Id = 0;
                            pendencia.Evento = "INSERT";
                            pendencia.idItem = TCx.Id;
                            pendencia.Tabela = "TOTAISCAIXAS";
                            pendencia.Status = 0;
                            pendencia.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                            var pendenciaDoc = new PendenciasRepository();
                            pendenciaDoc.Save(ref pendencia);
                        }
                    }

                    
                }
                


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

        public void ExcluirTotaisCaixas(int idNota)
        {
            bool openTransaction = false;
            decimal VDinheiro = 0;

            try
            {
                openTransaction = _repository.BeginTransaction();

                var caixas = new TotaisCaixasRepository().GetByNfce(idNota).ToList();
                foreach (var c in caixas)
                {
                    VDinheiro = c.dinheiro;
                    new TotaisCaixasRepository().Delete(c.Id);

                    if (VDinheiro <= 0)
                        continue;

                    using (TotaisCaixasRepository update = new TotaisCaixasRepository())
                    {
                        update.UpdateSaldo(VestilloSession.DefineCaixaPadrao, (int)enumTipoMovimentoCaixa.Debito, VDinheiro);
                    }
                }
                 
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
    }
}
