using Vestillo.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;
using Vestillo.Core.Services;

namespace Vestillo.Business.Controllers
{
    public class FatNfeController : GenericController<FatNfe, FatNfeRepository >
    {
        public IEnumerable<FatNfeView> GetCamposEspecificos(string parametrosDaBusca)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetCamposEspecificos(parametrosDaBusca);
            }
        }



        public IEnumerable<FatNfeView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetListPorReferencia(referencia, parametrosDaBusca);
            }
        }

        public IEnumerable<FatNfeView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetListPorNumero(Numero, parametrosDaBusca);
            }
        }

        public IEnumerable<FatNfeView> GetListagemNfeRelatorio(FiltroFatNfeListagem filtro)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetListagemNfeRelatorio(filtro);
            }
        }

        public IEnumerable<FechamentoDoDiaView> GetFechamentoDoDia(DateTime DataInicio, DateTime DataFim, int Tipo)
        {

            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetFechamentoDoDia(DataInicio, DataFim, Tipo);
            }

        }


        public IEnumerable<RepXVendaView> GetRepXVenda(string Ano, int Uf)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetRepXVenda(Ano,Uf);
            }
        }


        public IEnumerable<ListaFatVendaView> GetListaFatXVenda(DateTime DataInicio, DateTime DataFim, List<int> Vendedor, bool SomenteNFCe, bool SomenteTipoVendaNFCe, bool DataDatNfce)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetListaFatXVenda(DataInicio, DataFim, Vendedor,SomenteNFCe, SomenteTipoVendaNFCe, DataDatNfce);
            }
        }

        public int TotalFaturamentos(DateTime DataInicio, DateTime DataFim, int Vendedor, bool SomenteNFCe, bool DataDatNfce)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.TotalFaturamentos(DataInicio, DataFim, Vendedor,SomenteNFCe, DataDatNfce);
            }

        }

        public IEnumerable<FechamentoDoDiaPagView> GetFechamentoDoDiaPorPagamento(DateTime DataInicio, DateTime DataFim)
        {

            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.GetFechamentoDoDiaPorPagamento(DataInicio, DataFim);
            }
        }


        public FatNfeEtiquetaView EtiquetaEnderecamento(int Faturamento, int Tipo)
        {

            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.EtiquetaEnderecamento(Faturamento, Tipo);
            }
        }

        public decimal TotalNcc(DateTime DataInicio, DateTime DataFim, int IdVendedor)
        {
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                return repository.TotalNcc(DataInicio, DataFim, IdVendedor);
            }

        }




        public override void Save(ref FatNfe fatnfe)
        {
            bool openTransaction = false;
            
            using (FatNfeRepository repository = new FatNfeRepository())
            {
                try
                {
                    openTransaction = repository.BeginTransaction();

                    var estoqueController = new EstoqueController();

                    bool inclusao = fatnfe.Id == 0;

                    //PEGA A REFERENCIA
                    base.Save(ref fatnfe);

                    //grava os itens da nota
                    using (FatNfeItensRepository itensRepository = new FatNfeItensRepository())
                    {
                        itensRepository.DeleteByNfeItens(fatnfe.Id);
                        foreach (var gr in fatnfe.ItensNota)
                        {
                            FatNfeItens  g = gr;
                            g.Id = 0;
                            g.IdNfe = fatnfe.Id;
                            itensRepository.Save(ref g);
                        }
                        
                    }

                    using (ContasReceberRepository parcelasRepository = new ContasReceberRepository())
                    {                       
                        var ctreceber = parcelasRepository.GetListaPorCampoEValor("IdFatNfe", Convert.ToString(fatnfe.Id));

                        foreach (var g in ctreceber)
                        {
                            ExcluirContasReceber(g.Id);
                            //parcelasRepository.Delete(g.Id); 26-02 verificar Alex
                        }

                        foreach (var ctr in fatnfe.ParcelasCtr)
                        {
                            ContasReceber c = ctr;
                            c.Id = 0;
                            c.IdFatNfe = fatnfe.Id;
                            c.NumTitulo = fatnfe.Referencia.ToString();
                            c.Obs = "Título Gerado pelo Faturamento: " + fatnfe.Referencia.ToString() + " - " + ctr.Obs;
                            if (c.IdTipoDocumento == 1 )
                            {
                                c.DataPagamento = c.DataEmissao;
                                c.Saldo = 0;
                                c.Status = (int)enumStatusContasReceber.Baixa_Total;
                            }

                            parcelasRepository.Save(ref c);

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
                                dadosBaixa.Obs = "Titulo baixado pelo faturamento: " + fatnfe.Referencia.ToString() + " Parcela: " + c.Parcela;
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
                                    cheque.DataEmissao = ctr.DataEmissao;
                                    cheque.TipoEmitenteCheque = 1;
                                    cheque.NaturezaFinanceiraId = ctr.IdNaturezaFinanceira;
                                    cheque.ColaboradorId = c.IdCliente.GetValueOrDefault();
                                    cheque.NFeId = fatnfe.Id;
                                    cheque.Valor = ctr.ValorParcela;

                                    ChequeController chequeController = new ChequeController();
                                    chequeController.Save(ref cheque, "Cheque gerado pelo Faturamento: " + fatnfe.Referencia.ToString() + " - " + ctr.Obs);
                                   
                                 
                                }
                                else if (ctr.ParcelaDetalhes.CreditoClienteId > 0)
                                {
                                    var creditoClienteController = new CreditosClientesController();
                                    CreditosClientes credito = creditoClienteController.GetById(ctr.ParcelaDetalhes.CreditoClienteId);

                                    credito.ContasReceberBaixaId = ctr.Id;
                                    credito.ObsQuitacao = "Crédito utilizado na baixa do título: " + ctr.NumTitulo + ".";
                                    credito.Status = 2; //Quitado
                                    credito.dataquitacao = DateTime.Now.Date;
                                    creditoClienteController.Save(ref credito);
                                }
                            }
                        }
                    }

                    if (inclusao)
                    {
                        ColaboradorRepository colaboradorRepository = new ColaboradorRepository();
                        Colaborador colaborador = colaboradorRepository.GetById(fatnfe.IdColaborador);

                        if (colaborador != null && colaborador.DataPrimeiraCompra == null)
                        {
                            colaborador.TipoCliente = Colaborador.enumTipoCliente.Cliente;
                            colaborador.DataPrimeiraCompra = DateTime.Now;
                            colaboradorRepository.Save(ref colaborador);
                        }

                        if (openTransaction)
                            repository.CommitTransaction();

                        Colaborador vendedor = colaboradorRepository.GetById(fatnfe.idvendedor.GetValueOrDefault());

                        if (vendedor != null && vendedor.UsuarioId.GetValueOrDefault() > 0)
                        {
                            var atividade = new Vestillo.Core.Models.Atividade();
                            atividade.AlertarUsuario = true;
                            atividade.ClienteId = fatnfe.IdColaborador;
                            atividade.DataAlerta = DateTime.Now.AddDays(15);
                            atividade.DataAlerta = new DateTime(atividade.DataAlerta.GetValueOrDefault().Year, atividade.DataAlerta.GetValueOrDefault().Month, atividade.DataAlerta.GetValueOrDefault().Day, 8, 30, 0);
                            atividade.DataCriacao = DateTime.Now;
                            atividade.Observacao = "Atividade gerada pelo Faturamento: " + fatnfe.Referencia + ".";
                            atividade.StatusAtividade = Core.Models.Atividade.EnumStatusAtividade.Incluido;
                            atividade.TipoAtividadeCliente = Core.Models.Atividade.EnumTipoAtividadeCliente.Relacionamento;
                            atividade.TipoAtividadeId = 1; //Pós-Venda;
                            atividade.UsuarioAlertaAtividadeId =  vendedor.UsuarioId.GetValueOrDefault();
                            atividade.UsuarioCriacaoAtividadeId = VestilloSession.UsuarioLogado.Id;
                            atividade.VendedorId = vendedor.Id;
                            atividade.DataAtividade = atividade.DataAlerta.GetValueOrDefault();

                            using (Core.Services.AtividadeService atividadeService = new Core.Services.AtividadeService())
                            {
                                atividadeService.Save(atividade);
                            }
                        }
                    }

                    //Movimenta itens no estoque
                    if (fatnfe.ItensMovimentacaoEstoque != null)
                    {
                        foreach (var item in fatnfe.ItensMovimentacaoEstoque)
	                    {
                            if(item.Observacao == "")
                            {
                                if (item.Saida > 0)
                                {
                                    item.Observacao = "SAÍDA PELO FATURAMENTO: " + fatnfe.Referencia.ToString();
                                }
                                else
                                {
                                    item.Observacao = "ENTRADA PELO FATURAMENTO: " + fatnfe.Referencia.ToString();
                                }
                            }
                            
	                    }

                        estoqueController.MovimentarEstoque(fatnfe.ItensMovimentacaoEstoque, true);
                    }

                    if (fatnfe.IdColaborador > 0)
                    {
                        AtualizaDadosDeCompra(fatnfe.IdColaborador);
                    }

                    if (openTransaction)
                        repository.CommitTransaction();

                


                }
                catch (Exception ex)
                {
                    if (openTransaction)
                        repository.RollbackTransaction();
                    
                    throw ex;
                }
            }           
        }



        private void ExcluirContasReceber(int IdContasReceber)
        {
            bool Baixados = false;
            //verifica o contas a receber e marca como inutilizado
            var serviceCTR = new ContasReceberService().GetServiceFactory();
            var ctr = serviceCTR.GetById(IdContasReceber);
            var listParcelas = new List<ContasReceber>();

            

            //var comissao = new ComissoesvendedorService().GetServiceFactory().GetByParcelaCtr(c.Id);
            var comissao = new ComissoesvendedorService().GetServiceFactory().GetByParcelaCtrDeletar(ctr.Id);
            if (comissao != null && comissao.Count() > 0)
            {

                foreach (var item in comissao)
                {
                    new ComissoesvendedorService().GetServiceFactory().Delete(item.Id);
                }


            }

            if (ctr.Status != (int)enumStatusContasReceber.Aberto)
            {
                Baixados = true;
            }

            using (MovimentacaoBancoRepository delete = new MovimentacaoBancoRepository())
            {
                delete.DeleteByContasReceber(ctr.Id);
            }

            var IdBaixa = new ContasReceberBaixaService().GetServiceFactory().GetByContasReceber(ctr.Id);

            foreach (var item in IdBaixa)
            {
                new ContasReceberBaixaService().GetServiceFactory().Delete(item.Id);
            }           

            new ContasReceberService().GetServiceFactory().Delete(ctr.Id);


            if (ctr.IdCliente != null)
            {
                AtualizaDadosDeCompra(Convert.ToInt32(ctr.IdCliente));
            }


        }

        public void GravarMovimentacaoCaixa(string referenciaNota, int idNota, IEnumerable<ContasReceber> parcelas, bool debito = false)
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

            var dadosNota = GetById(idNota);
            var dadosCliente = new ColaboradorController().GetById(dadosNota.IdColaborador);

            if (dadosCliente.TipoCliente == Colaborador.enumTipoCliente.Funcionario)
            {
                IdCliente = dadosNota.IdColaborador;
            }

            try
            {
                openTransaction = _repository.BeginTransaction();

                var caixas = new TotaisCaixasRepository().GetByNfe(idNota).ToList();
                foreach (var c in caixas)
                {
                    decimal dinheiro = c.dinheiro;
                    new TotaisCaixasRepository().Delete(c.Id);

                    if (dinheiro <= 0)
                        continue;

                    using (TotaisCaixasRepository update = new TotaisCaixasRepository())
                    {
                        update.UpdateSaldo(VestilloSession.DefineCaixaPadrao, (int)enumTipoMovimentoCaixa.Debito, dinheiro);
                    }
                }

                if (parcelas != null)
                {

                    foreach (var ctr in parcelas)
                    {
                        if (ctr.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Dinheiro)
                        {
                            VDinheiro += ctr.ValorParcela;
                        }
                        else if (ctr.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cheque)
                        {
                            VCheque += ctr.ValorParcela;
                        }
                        else if (ctr.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cartão_de_Credito)
                        {
                            if (ctr.IdCliente == null)
                            {
                                VCCredito += ctr.ValorParcela;
                                NOperadoraCred += "-" + ctr.NomeCliente;

                            }
                        }
                        else if (ctr.IdTipoDocumento.GetValueOrDefault().ToEnum<enumTipoDocumento>() == enumTipoDocumento.Cartão_de_Debito)
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

                VDinheiro += dadosNota.ValorDinheiro;
                VCheque += dadosNota.ValorCheque;
                VCCredito += dadosNota.ValorCartaoCredito;
                VCDebito += dadosNota.ValorCartaoDebito;

                if (VestilloSession.DefineCaixaPadrao > 0)
                {
                    var CaixaDaEmpresa = new CaixasService().GetServiceFactory().GetById(VestilloSession.DefineCaixaPadrao);
                    if (CaixaDaEmpresa != null)
                    {
                        if (CaixaDaEmpresa.IdEmpresa == VestilloSession.EmpresaLogada.Id)
                        {
                            
                            var TCx = new TotaisCaixas();
                            TCx.Id = 0;
                            TCx.idcaixa = VestilloSession.DefineCaixaPadrao;
                            TCx.Idcolaborador = IdCliente == 0 ? null : IdCliente;
                            TCx.datamovimento = DateTime.Now;
                            TCx.tipo = debito ? (int)enumTipoMovimentoCaixa.Debito : (int)enumTipoMovimentoCaixa.Credito;
                            TCx.dinheiro = VDinheiro;
                            TCx.cartaocredito = VCCredito;
                            TCx.cartaodebito = VCDebito;
                            TCx.cheque = VCheque;
                            TCx.outros = VOutros;
                            TCx.PixDep = VPixVDep;
                            TCx.operadoracredito = NOperadoraCred;
                            TCx.operadoradebito = NOperadoraDeb;
                            TCx.idNfe = dadosNota.Id;

                            if(debito)
                                TCx.observacao = "Débito realizado pela exclusão da NFe. " + referenciaNota;
                            else
                                TCx.observacao = "Entrada realizada pela NFe." + referenciaNota;
                            
                            new TotaisCaixasService().GetServiceFactory().Save(ref TCx);

                            using (TotaisCaixasRepository update = new TotaisCaixasRepository())
                            {
                                if(debito)
                                    update.UpdateSaldo(VestilloSession.DefineCaixaPadrao, (int)enumTipoMovimentoCaixa.Debito, VDinheiro);
                                else
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

        public void AtualizaDadosDeCompra(int IdCliente)
        {
            using (var Cliente = new ColaboradorRepository())
            {
                Cliente.DefineLimiteDeCompra(Convert.ToInt32(IdCliente));
            }
        }

        public void AtualizaDadosDaOrdem(bool Saida,int IdDaDordem, List<FatNfeItensView> MaterialDaOrdem, int idAlmoxarifado)
        {
            try
            {
                foreach (var item in MaterialDaOrdem)
                {
                    using (var material = new OrdemProducaoMaterialRepository())
                    {
                        material.AtualizaDadosOrdemFaturada(Saida, IdDaDordem, item.iditem, Convert.ToInt32(item.idcor), Convert.ToInt32(item.idtamanho), item.Quantidade,item.QtdAcimaDaOp, idAlmoxarifado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FatNfe GetByNfce(int idNFCe)
        {
            return _repository.GetByNfce(idNFCe);
        }



    }
}
