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
    public class BorderoCobrancaController : GenericController<BorderoCobranca, BorderoCobrancaRepository>
    {
        public override BorderoCobranca GetById(int id)
        {
            BorderoCobranca bordero = base.GetById(id);

            if (bordero != null)
            {
                BorderoCobrancaDocumentoRepository borderoCobrancaDocumentoRepository = new BorderoCobrancaDocumentoRepository();
                bordero.Documentos = borderoCobrancaDocumentoRepository.GetByBordero(id).ToList();
            }

            return bordero;
        }

        public override void Save(ref BorderoCobranca bordero)
        {
            BorderoCobrancaDocumentoRepository borderoCobrancaDocumentoRepository = new BorderoCobrancaDocumentoRepository();

            try
            {
                borderoCobrancaDocumentoRepository.BeginTransaction();

                if (bordero.Id == 0)
                {
                    bordero.Status = BorderoCobranca.StatusBordero.Incluido;
                }

                base.Save(ref bordero);

                borderoCobrancaDocumentoRepository.DeleteByBordero(bordero.Id);

                foreach (BorderoCobrancaDocumento documento in bordero.Documentos)
                {
                    var doc = documento;
                    doc.BorderoCobrancaId = bordero.Id;
                    borderoCobrancaDocumentoRepository.Save(ref doc);
                }

                borderoCobrancaDocumentoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                borderoCobrancaDocumentoRepository.RollbackTransaction();
                throw ex;
            }
        }

        public void BaixarEstornarBordero(BorderoCobranca bordero, bool estornar = false)
        {
            var repository = new BorderoCobrancaRepository();
            
            try
            {
                repository.BeginTransaction();

                foreach (BorderoCobrancaDocumento documento in bordero.Documentos)
                {
                    if (documento.ChequeId.GetValueOrDefault() > 0)
                    {
                        ChequeController chequeController = new ChequeController();
                        ChequeView cheque = chequeController.GetViewById(documento.ChequeId.GetValueOrDefault());

                        if (estornar)
                            cheque.ObsCompensacao = "Estornado no bodero: " + bordero.Referencia;
                        else
                            cheque.ObsCompensacao = "Baixado no bodero: " + bordero.Referencia; 
                       
                        chequeController.CompensarCheque(cheque);
                    }
                    else
                    {
                        ContasReceberBaixaController contasReceberBaixaController = new ContasReceberBaixaController();
                        ContasReceberController contasReceberController = new ContasReceberController();

                        ContasReceber contasReceber = contasReceberController.GetById(documento.ContasReceberId.GetValueOrDefault());

                        if (!estornar)
                        {

                            ContasReceberBaixa baixa = new ContasReceberBaixa();
                            baixa.Obs = "Baixado no bodero: " + bordero.Referencia; 
                            baixa.ContasReceberId = documento.ContasReceberId.GetValueOrDefault();
                            baixa.DataBaixa = bordero.DataAcerto.GetValueOrDefault();
                            baixa.Cheques = new List<Cheque>();
                            baixa.BorderoId = bordero.Id;
                            baixa.ValorCheque = 0;
                            baixa.ValorDinheiro = contasReceber.ValorParcela;
                            contasReceberBaixaController.Save(ref baixa, false);

                            //contasReceber.ValorPago += baixa.ValorDinheiro;
                            //contasReceberController.Save(ref contasReceber);
                        }
                        else
                        {
                            IEnumerable<ContasReceberBaixa> contasReceberBaixaBordero = contasReceberBaixaController.GetByContasReceberEBordero(contasReceber.Id, bordero.Id);

                            foreach (ContasReceberBaixa baixa in contasReceberBaixaBordero)
                            {
                                //contasReceber.ValorPago -= baixa.ValorDinheiro;
                                //contasReceberController.Save(ref contasReceber);
                                contasReceberBaixaController.Delete(baixa.Id);
                            }
                        }
                    }
                }

                //Movimenta saldo do banco
                if (bordero.BancoId > 0)
                {
                    BancoRepository saldo = new BancoRepository();

                    int tipo = estornar ? 2 : 1;

                    var MovBanco = new MovimentacaoBanco();

                    MovBanco.Id = 0;
                    MovBanco.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                    MovBanco.IdBanco = bordero.BancoId;
                    MovBanco.IdContasPagar = null;
                    MovBanco.IdContasReceber = null;
                    MovBanco.IdCheque = null;
                    MovBanco.Tipo = tipo;
                    MovBanco.BorderoId = bordero.Id;
                    MovBanco.DataMovimento = bordero.DataAcerto.GetValueOrDefault();
                    MovBanco.IdUsuario = VestilloSession.UsuarioLogado.Id;
                    MovBanco.Valor = bordero.ValorReceber; //TODO;

                    if (tipo == 1)
                        MovBanco.Observacao = "Crédito realizado pela baixa do borderô " + bordero.Referencia;
                    else
                        MovBanco.Observacao = "Débito realizado pela baixa do borderô " + bordero.Referencia;
                    
                    new MovimentacaoBancoController().Save(ref MovBanco);
                }

                repository.BaixarEstornarBordero(bordero, estornar);

                repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                throw ex;
            }
        }

        public IEnumerable<BorderoCobranca> GetByDocumento(int documentoId, bool isCheque)
        {
            return new BorderoCobrancaRepository().GetByDocumento(documentoId, isCheque);
        }

        public override void Delete(int id)
        {
             var borderoDocumentoRepository = new BorderoCobrancaDocumentoRepository();
            
            try
            {
                borderoDocumentoRepository.BeginTransaction();
                borderoDocumentoRepository.DeleteByBordero(id);
                base.Delete(id);
                borderoDocumentoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                borderoDocumentoRepository.RollbackTransaction();
                throw ex;
            }
        }
    }
}
