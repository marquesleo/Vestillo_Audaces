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
    public class EstoqueController: GenericController<Estoque, EstoqueRepository>
    {
        public IEnumerable<ConsultaEstoqueView> GetEstoque()
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetEstoque();
            }
        }

        public IEnumerable<ConsultaEstoqueView> GetEstoqueRelatorio(FiltroEstoqueRelatorio filtro)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetEstoqueRelatorio(filtro);
            }
        }

        public Estoque GetEstoque(int almoxerifadoId, int produtoId, int corId, int tamanhoId, bool ProcuraPorEmpresa)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetEstoque(almoxerifadoId, produtoId, corId, tamanhoId, ProcuraPorEmpresa);
            }
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int estoqueId)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetMovimentacaoEstoque(estoqueId);
            }
        }

        public void MovimentarEstoque(IEnumerable<MovimentacaoEstoque> lstMovimentacao, bool movimentaConjunto = false, bool ProcuraPorEmpresa = true, bool LiberandoOP = false)
        {
            var repository = new MovimentacaoEstoqueRepository(); 
            var repositoryProduto = new ProdutoRepository();

            try
            {
                lstMovimentacao = lstMovimentacao.Reverse();

                foreach (var mov in lstMovimentacao)
                {
                    repository.BeginTransaction();
                    if (mov.AlmoxarifadoId == 0)
                        throw new Exception("Almoxarifado não informado na movimentação de estoque.!");

                    int tamanho = 0;
                    int cor = 0;

                    if (mov.CorId != null)
                        cor = int.Parse(mov.CorId.ToString());

                    if (mov.TamanhoId != null)
                        tamanho = int.Parse(mov.TamanhoId.ToString());

                    var estoque = this.GetEstoque(mov.AlmoxarifadoId, mov.ProdutoId, cor, tamanho, ProcuraPorEmpresa);

                    if (estoque == null)
                    {
                        estoque = new Estoque();
                        estoque.Id = 0;
                        estoque.AlmoxarifadoId = mov.AlmoxarifadoId;
                        estoque.CorId = mov.CorId;
                        estoque.TamanhoId = mov.TamanhoId;
                        estoque.ProdutoId = mov.ProdutoId;
                    }

                    estoque.DataAlteracao = DateTime.Now;

                    if (mov.Empenho)
                    {
                        if (mov.Entrada > 0)
                            estoque.Empenhado -= mov.Entrada;

                        if (mov.Saida > 0)
                            estoque.Empenhado += mov.Saida;
                    }

                    if (!mov.Baixa)
                    {
                        if (!mov.SoEmpenho)
                        {
                            if (mov.Entrada > 0)
                                estoque.Saldo += mov.Entrada;

                            if (mov.Saida > 0)
                                estoque.Saldo -= mov.Saida;
                        }
                    }
                    else
                    {
                        if (mov.Entrada > 0)
                            estoque.Empenhado += mov.Entrada;

                        if (mov.Saida > 0)
                            estoque.Empenhado -= mov.Saida;
                    }

                    //if (mov.Entrada > 0)
                    //    estoque.Saldo += mov.Entrada;

                    //if (mov.Saida > 0)
                    //    estoque.Saldo -= mov.Saida;

                    if ((VestilloSession.ControleDeEstoqueAtivo == VestilloSession.ControleEstoque.SIM && LiberandoOP == false && estoque.Saldo < 0) && mov.Saida > 0) // ALEX 17-03-2021
                    {
                        throw new Exception("Não existe saldo suficiente para a operação");
                    }
                    else
                    {

                        this.Save(ref estoque);
                        mov.EstoqueId = estoque.Id;
                        
                        var pendencia = new Pendencias();
                        pendencia.Id = 0;
                        pendencia.Evento = "INSERT";
                        pendencia.idItem = estoque.Id;
                        pendencia.Tabela = "ESTOQUE";
                        pendencia.Status = 0;
                        pendencia.IdEmpresa = VestilloSession.EmpresaLogada.Id;
                        var pendenciaDoc = new PendenciasRepository();
                        pendenciaDoc.Save(ref pendencia);

                        if (movimentaConjunto)
                        {
                            var produto = repositoryProduto.GetById(mov.ProdutoId);
                            var produtoVinculadoId = produto.IdItemVinculado != null ? (int)produto.IdItemVinculado : 0;

                            if (produtoVinculadoId > 0)
                            {
                                var estoqueVinculado = this.GetEstoque(mov.AlmoxarifadoId, produtoVinculadoId, cor, tamanho, ProcuraPorEmpresa);
                                if (estoqueVinculado == null)
                                {
                                    estoqueVinculado = new Estoque();
                                    estoqueVinculado.Id = 0;
                                    estoqueVinculado.AlmoxarifadoId = mov.AlmoxarifadoId;
                                    estoqueVinculado.CorId = mov.CorId;
                                    estoqueVinculado.TamanhoId = mov.TamanhoId;
                                    estoqueVinculado.ProdutoId = produtoVinculadoId;
                                    estoqueVinculado.Empenhado = 0;
                                    estoqueVinculado.Saldo = 0;
                                }
                                else
                                {
                                    if (!mov.Baixa)
                                    {
                                        if (mov.Entrada > 0)
                                            estoqueVinculado.Saldo += mov.Entrada;

                                        if (mov.Saida > 0)
                                        {
                                            if(estoqueVinculado.Saldo > 0)
                                            {
                                                estoqueVinculado.Saldo -= mov.Saida;
                                                if (estoqueVinculado.Saldo < 0)
                                                {
                                                    estoqueVinculado.Saldo = 0;
                                                }
                                            }
                                            
                                        }
                                           
                                    }
                                }

                                estoqueVinculado.DataAlteracao = DateTime.Now;
                                //else
                                //{
                                //    if (mov.Entrada > 0)
                                //        estoque.Empenhado += mov.Entrada;

                                //    if (mov.Saida > 0)
                                //        estoque.Empenhado -= mov.Saida;
                                //}

                                //if (mov.Entrada > 0)
                                //    estoqueVinculado.Saldo += mov.Entrada;

                                //if (mov.Saida > 0)
                                //    estoqueVinculado.Saldo -= mov.Saida;
                                this.Save(ref estoqueVinculado);
                            }
                        }

                        var entity = mov;

                        if (entity.DataMovimento == DateTime.MinValue)
                            entity.DataMovimento = DateTime.Now;

                        repository.Save(ref entity);
                        
                    }
                }
                repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                if (ex.Message == "Não existe saldo suficiente para a operação")
                {
                    throw new Vestillo.Lib.VestilloException(Vestillo.Lib.Enum_Tipo_VestilloNet_Exception.Registro_Duplicado, "Não existe saldo suficiente para a operação");
                }
                else
                {
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
         
        }

        public IEnumerable<ConsultaEstoqueProdutoroduzidoView> GetEstoque(FichaEstoqueProdutoProduzido filtro)
        {
            using (var repository = new EstoqueRepository())
            {
                var resp = repository.GetEstoque(filtro).ToList();
                resp.ForEach(r =>
                {
                    r.Itens = new ItemOrdemProducaoRepository().GetByProduto(r.ProdutoId, r.CorId, r.TamanhoId, r.AlmoxarifadoId,true).ToList();
                    r.ItensLiberacao = new ItemLiberacaoPedidoVendaRepository().GetItensLiberacao(r.ProdutoId, r.CorId, r.TamanhoId, r.AlmoxarifadoId).ToList();
                    r.ItensPedido = new ItemPedidoVendaRepository().GetItensLiberadosSemEmpenho(r.ProdutoId, r.CorId, r.TamanhoId, r.AlmoxarifadoId).ToList();

                    if (filtro.SemLiberacao) //considera pedidos incluídos
                    {
                        //var itensPedido = new ItemPedidoVendaRepository().GetItens(r.ProdutoId, r.CorId, r.TamanhoId, r.AlmoxarifadoId).ToList();
                        //r.ItensPedidoIncluido.AddRange(itensPedido);
                        r.ItensPedidoIncluido = new ItemPedidoVendaRepository().GetItens(r.ProdutoId, r.CorId, r.TamanhoId, r.AlmoxarifadoId).ToList();
                    }
                       
                });
                return resp;
            }
        }

        public void TransferirEstoque(List<MovimentacaoEstoque> lstMovimentacao, int almoxarifadoDestinoId)
        {
            var repository = new MovimentacaoEstoqueRepository();
            List<MovimentacaoEstoque> lstMovimentacaoEntrada = new List<MovimentacaoEstoque>();

            try
            {
               
                MovimentarEstoque(lstMovimentacao, false);

                foreach (MovimentacaoEstoque mov in lstMovimentacao)
                {
                    MovimentacaoEstoque MovEntrada = new MovimentacaoEstoque();

                    MovEntrada.AlmoxarifadoId = almoxarifadoDestinoId;
                    MovEntrada.CorId = mov.CorId;
                    MovEntrada.DataMovimento = mov.DataMovimento;
                    MovEntrada.Empenho = mov.Empenho;
                    MovEntrada.Entrada = mov.Saida;
                    MovEntrada.Observacao = "ENTRADA MANUAL:  Recebido por Estoque transferido";
                    MovEntrada.ProdutoId = mov.ProdutoId;
                    MovEntrada.SoEmpenho = mov.SoEmpenho;
                    MovEntrada.TamanhoId = mov.TamanhoId;
                    MovEntrada.UsuarioId = mov.UsuarioId;
                    MovEntrada.Saida = 0;
                    lstMovimentacaoEntrada.Add(MovEntrada);
                }
                MovimentarEstoque(lstMovimentacaoEntrada, false);
            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                if (ex.Message == "Não existe saldo suficiente para a operação")
                {
                    throw new Vestillo.Lib.VestilloException(Vestillo.Lib.Enum_Tipo_VestilloNet_Exception.Registro_Duplicado, "Não existe saldo suficiente para a operação");
                }
                else
                {
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
         
        }

        public IEnumerable<ConsultaEstoqueMateriaPrima> GetEstoqueMateriaPrima(FichaEstoqueMateriaPrima filtro)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetEstoqueMateriaPrima(filtro).ToList();
            }
        }

        public ConsultaEstoqueView GetSaldoAtualProduto(int almoxarifadoId, int produtoId, int corId, int tamanhoId)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetSaldoAtualProduto(almoxarifadoId, produtoId, corId, tamanhoId);
            }
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int almoxarifadoId, string codigoBarras, int produtoId)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetMovimentacaoEstoque(almoxarifadoId, codigoBarras, produtoId);
            }
        }

        public ConsultaEstoqueView GetEmpenhoAtualProduto(int almoxerifadoId, int produtoId, int corId, int tamanhoId)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetEmpenhoAtualProduto(almoxerifadoId, produtoId, corId, tamanhoId);
            }
        }

        public IEnumerable<ConsultaEstoqueRelatorioView> GetConsultaEstoqueRelatorio(List<int> idEmpresas, int? tipoProduto, bool faturado, DateTime? daData, DateTime? ateData)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetConsultaEstoqueRelatorio(idEmpresas, tipoProduto, faturado, daData, ateData);
            }
        }

        public MovimentacaoEstoque GetUltimaMovimentacaoByPacote(int idPacote)
        {
            using (var repository = new EstoqueRepository())
            {
                return repository.GetUltimaMovimentacaoByPacote(idPacote);
            }
        }
    }
}
