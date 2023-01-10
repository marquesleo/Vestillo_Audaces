using Vestillo.Lib;
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
    public class DevolucaoController : GenericController<Devolucao, DevolucaoRepository>
    {


        public IEnumerable<DevolucaoView> GetAllView()
        {
            using (DevolucaoRepository repository = new DevolucaoRepository())
            {
                return repository.GetAllView();
            }
        }


        public override void Save(ref Devolucao Dev)
        {
            bool openTransaction = false;

            using (DevolucaoRepository repository = new DevolucaoRepository())
            {
                try
                {
                    openTransaction = repository.BeginTransaction();

                    var estoqueController = new EstoqueController();
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();

                    //PEGA A REFERENCIA
                    base.Save(ref Dev);

                    //grava os itens da nota
                    using (DevolucaoItensRepository itensRepository = new DevolucaoItensRepository())
                    {
                        itensRepository.DeleteByNfeItens(Dev.Id);
                        foreach (var gr in Dev.ItensDevolucao)
                        {
                            DevolucaoItens g = gr;
                            g.Id = 0;
                            g.IdDevolucao = Dev.Id;
                            itensRepository.Save(ref g);
                        }

                        //Movimenta itens no estoque
                        if (Dev.ItensMovimentacaoEstoque != null)
                        {                           

                            foreach (var est in Dev.ItensMovimentacaoEstoque)
                            {
                                var mov = new MovimentacaoEstoque() { Saida = 0, Entrada = 0 };

                                mov.ProdutoId = est.ProdutoId;
                                mov.AlmoxarifadoId = est.AlmoxarifadoId;
                                mov.DataMovimento = est.DataMovimento;
                                mov.UsuarioId = est.UsuarioId;
                                mov.TamanhoId = est.TamanhoId;
                                mov.CorId = est.CorId;
                                mov.Entrada = est.Entrada;
                                mov.Observacao = "ENTRADA PELA DEVOLUÇÃO DE ITENS: " + Dev.Referencia;
                                listMovimentacaoEstoque.Add(mov);
                            }
                            estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false);
                        }
                    }

                    using (CreditosClientesRepository creditoRepository = new CreditosClientesRepository())
                    {
                        var cCredito = creditoRepository.GetByCredito(Dev.Id);

                        foreach (var cred in Dev.CreditoCliente)
                        {
                            CreditosClientes c = cred;
                            c.Id = 0;
                            c.Ativo = true;
                            c.dataemissao = cred.dataemissao;
                            c.dataquitacao = null;
                            c.idcolaborador = cred.idcolaborador;
                            c.idnotaconsumidor = null;
                            c.idNotaFat = null;
                            c.idDevolucaoItens = Dev.Id;
                            c.Status = 1;
                            c.valor = cred.valor;
                            c.ObsInclusao = "Crédito gerado pela devolução de Itens: " + Dev.Referencia;
                            c.ObsQuitacao = "";
                            new CreditosClientesService().GetServiceFactory().Save(ref c);
                        }
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

        //delete

        public override void Delete(int id)
        {
            bool openTransaction = false;

            var itemDevolucaoItensRepository = new DevolucaoItensRepository();
            var creditoRepository = new CreditosClientesRepository();
            var DevolucaoRepository = new DevolucaoRepository();

            var estoqueControllerSaida = new EstoqueController();
            var listMovimentacaoEstoqueSaida = new List<MovimentacaoEstoque>();

            try
            {
                openTransaction = _repository.BeginTransaction();

                var Dev = DevolucaoRepository.GetById(id);
                var listItens =  itemDevolucaoItensRepository.GetListByNfeItens(id);

                //RETIRA ITENS DO ESTOQUE
                foreach (var est in listItens)
                {
                    var mov = new MovimentacaoEstoque() { Saida = 0, Entrada = 0 };

                    mov.ProdutoId = est.iditem;
                    mov.AlmoxarifadoId = Dev.idAlmoxarifado;
                    mov.DataMovimento = DateTime.Now; 
                    mov.UsuarioId = Vestillo.Business.VestilloSession.UsuarioLogado.Id;
                    mov.TamanhoId = est.idtamanho;
                    mov.CorId = est.idcor;
                    mov.Saida = est.Quantidade;
                    mov.Observacao = "SAÍDA PELA DEVOLUÇÃO DE ITENS : " + Dev.Referencia + "FOI EXCLUÍDA";
                    listMovimentacaoEstoqueSaida.Add(mov);
                }
                estoqueControllerSaida.MovimentarEstoque(listMovimentacaoEstoqueSaida, false);

                //Excluir Crédito gerado
                var credito = new Vestillo.Business.Service.CreditosClientesService().GetServiceFactory();
                var dados = credito.GetByCredito(Dev.Id);
                if (dados != null)
                {
                    new CreditosClientesService().GetServiceFactory().Delete(dados.Id);
                }


                //----------------------Exclui os itens---------------------------

                    foreach (var l in listItens)
                    {
                        itemDevolucaoItensRepository.Delete(l.Id);//Exclui os itens
                    }

                    base.Delete(Dev.Id);

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
