using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class OrdemProducaoMaterialController : GenericController<OrdemProducaoMaterial, OrdemProducaoMaterialRepository>
    {
        public IEnumerable<OrdemProducaoMaterialView> GetByOrdemIdView(int ordemId)
        {
            return _repository.GetByOrdemIdView(ordemId);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensIdView(int ordemId)
        {
            return _repository.GetByOrdensIdView(ordemId);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdenEItem(int itemId, int ordemId)
        {
            return _repository.GetByOrdenEItem(itemId, ordemId);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetExcluir(int itemId, int ordemId)
        {
            return _repository.GetExcluir(itemId, ordemId);
        }

        public IEnumerable<OrdemProducaoMaterialEstoqueView> GetMaterialLiberacaoView()
        {
            return _repository.GetMaterialLiberacaoView();
        }

        public OrdemProducaoMaterialView GetEmpenhoLivreByOrdem(OrdemProducaoMaterialView ordem)
        {
            return _repository.GetEmpenhoLivreByOrdem( ordem );
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListByItemComFichaTecnicaMaterial(List<int> idsIOP, int ordemId, bool agruparItem)
        {
            return _repository.GetListByItemComFichaTecnicaMaterial(idsIOP, ordemId, agruparItem);
        }

        public IEnumerable<CompraMaterialSemana> GetListCompraMaterialSemana(List<int> semanas)
        {
            return _repository.GetListCompraMaterialSemana(semanas);
        }

         public IEnumerable<CustoConsumo> GetCustoConsumo(FiltroCustoConsumo filtro)
        {
            return _repository.GetCustoConsumo(filtro);
        }

        public void LiberarEstoque(List<OrdemProducaoMaterialEstoqueView> ordemMateriaisEstoque)
        {
            if (ordemMateriaisEstoque != null && ordemMateriaisEstoque.Count() > 0)
            {
                OrdemProducaoMaterialRepository repository = new OrdemProducaoMaterialRepository();
                var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                EstoqueController estoqueController = new EstoqueController();

                foreach (OrdemProducaoMaterialEstoqueView ordemProducaoMaterial in ordemMateriaisEstoque)
                {
                    OrdemProducaoMaterial ordemProducaoMaterialSave;
                    ordemProducaoMaterialSave = repository.GetById(ordemProducaoMaterial.Id);
                    if (ordemProducaoMaterial.EmpenhoLivre >= ordemProducaoMaterial.Lancamento)
                    {
                        var movEstoque = new MovimentacaoEstoque();
                        movEstoque.Empenho = true;
                        movEstoque.AlmoxarifadoId = ordemProducaoMaterial.ArmazemId;
                        movEstoque.Entrada = ordemProducaoMaterial.Lancamento;
                        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        movEstoque.ProdutoId = ordemProducaoMaterial.MaterialId;
                        movEstoque.CorId = ordemProducaoMaterial.CorId;
                        movEstoque.TamanhoId = ordemProducaoMaterial.TamanhoId;
                        movEstoque.Observacao = "Liberação de material na Ordem de produção";
                        movEstoque.DataMovimento = DateTime.Now;
                        listMovimentacaoEstoque.Add(movEstoque);

                        ordemProducaoMaterialSave.EmpenhoProducao -= ordemProducaoMaterial.Lancamento;
                        repository.Save(ref ordemProducaoMaterialSave);
                    }
                    
                    
                }

                if (listMovimentacaoEstoque.Count > 0)
                {
                    estoqueController.MovimentarEstoque(listMovimentacaoEstoque);
                }
                
            }
        }

        public IEnumerable<OrdemProducaoMaterial> GetByOrdemView(int ordemId, int materialId, int corId, int tamanhoId)
        {
            return _repository.GetByOrdemView(ordemId, materialId, corId, tamanhoId);
        }

        public IEnumerable<ConsultaRelListaMateriaisView> GetListaMateriaisBaseadoOP(FiltroRelListaMateriais filtro)
        {
            List<ConsultaRelListaMateriaisView> _ListMateriais = new List<ConsultaRelListaMateriaisView>();
            using (OrdemProducaoMaterialRepository repository = new OrdemProducaoMaterialRepository())
            {
                var ordensMateriais = repository.GetListaMateriaisBaseadoOP(filtro).ToList();
                if (filtro.TodosMateriais)
                {
                    _ListMateriais = repository.GetTodosMateriaisBaseadoOP(filtro).ToList();
                    if (ordensMateriais != null && ordensMateriais.Count() > 0 && _ListMateriais.Count() > 0)
                    {
                        _ListMateriais.ForEach(m => {
                            var ordem = ordensMateriais.Find(o => o.materiaPrimaId == m.materiaPrimaId && o.corId == m.corId && o.tamanhoId == m.tamanhoId);
                            if (ordem != null) m.quantidade = ordem.quantidade;                                
                        });
                    }                        
                }
                else
                    _ListMateriais = ordensMateriais;

                var valoresOP = repository.GetValoresOP(filtro).ToList();
                var semanas = GetSemanasCompra(filtro.DataPedidoCompra);

                _ListMateriais.ForEach(material => {

                    var estoque = new EstoqueRepository().GetSaldoProduto(material.materiaPrimaId, material.corId, material.tamanhoId, filtro.idAlmoxarifado);
                    material.saldo = estoque != null ? estoque.Saldo : 0;

                    material.precoCusto = new ProdutoFornecedorPrecoController().GetCusto(material.materiaPrimaId, material.corId, material.tamanhoId);
                    
                    material.semana0 = new PedidoCompraRepository().GetQuantidadeComprada(null, semanas.ElementAt(0).DataFim, material.materiaPrimaId, material.corId, material.tamanhoId);
                    material.semana1 = new PedidoCompraRepository().GetQuantidadeComprada(semanas.ElementAt(1).DataInicio, semanas.ElementAt(1).DataFim, material.materiaPrimaId, material.corId, material.tamanhoId);
                    material.semana2 = new PedidoCompraRepository().GetQuantidadeComprada(semanas.ElementAt(2).DataInicio, semanas.ElementAt(2).DataFim, material.materiaPrimaId, material.corId, material.tamanhoId);
                    material.semana3 = new PedidoCompraRepository().GetQuantidadeComprada(semanas.ElementAt(3).DataInicio, semanas.ElementAt(3).DataFim, material.materiaPrimaId, material.corId, material.tamanhoId);
                    material.semana4 = new PedidoCompraRepository().GetQuantidadeComprada(semanas.ElementAt(4).DataInicio, semanas.ElementAt(4).DataFim, material.materiaPrimaId, material.corId, material.tamanhoId);
                    material.semana5 = new PedidoCompraRepository().GetQuantidadeComprada(semanas.ElementAt(5).DataInicio, semanas.ElementAt(5).DataFim, material.materiaPrimaId, material.corId, material.tamanhoId);
                    material.semana6 = new PedidoCompraRepository().GetQuantidadeComprada(semanas.ElementAt(6).DataInicio, null, material.materiaPrimaId, material.corId, material.tamanhoId);

                    material.descSemana1 = semanas.ElementAt(1).DataInicio.Day.ToString();
                    material.descSemana2 = semanas.ElementAt(2).DataInicio.Day.ToString();
                    material.descSemana3 = semanas.ElementAt(3).DataInicio.Day.ToString();
                    material.descSemana4 = semanas.ElementAt(4).DataInicio.Day.ToString();
                    material.descSemana5 = semanas.ElementAt(5).DataInicio.Day.ToString();

                    if(valoresOP != null && valoresOP.Count > 0)
                    {
                        var ordem = valoresOP.Find(v => v.materiaPrimaId == material.materiaPrimaId && v.corId == material.corId && v.tamanhoId == material.tamanhoId);
                        if (ordem != null)
                        {
                            material.qtdopliberada = ordem.qtdopliberada;
                            material.qtdopnaoliberada = ordem.qtdopnaoliberada;
                            material.estoquebaixado = ordem.estoquebaixado;
                            material.estoqueempenhado = ordem.estoqueempenhado;
                        }
                    }

                });

                return _ListMateriais;
            }
        }

        public List<SemanasCompra> GetSemanasCompra(DateTime dataPedidoCompra)
        {
            List<SemanasCompra> _semanas = new List<SemanasCompra>();

            for (int semana = -1; semana < 6; semana++)
            {
                var semanaCompra = new SemanasCompra();

                var dataBase = dataPedidoCompra.AddDays(semana * 7);

                int dias = 0;
                dias = ((int)DayOfWeek.Sunday - 7 - (int)dataBase.DayOfWeek) % 7;//domingo anterior a data base
                if(dias == 0 ) dias = 7 ;

                semanaCompra.DataInicio = dataBase.AddDays(dias);

                dias = ((int)DayOfWeek.Saturday + 7 - (int)dataBase.DayOfWeek) % 7; //sábado posterior a data base
                if (dias == 0) dias = 7;

                semanaCompra.DataFim = dataBase.AddDays(dias);

                _semanas.Add(semanaCompra);
            }

            return _semanas;
        }
    }
}
