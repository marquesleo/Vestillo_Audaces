using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;
using Vestillo.Core.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ProdutoController : GenericController<Produto,ProdutoRepository> 
    {
     
        public override void Save(ref Produto  produto)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                try
                {
                    repository.BeginTransaction();
                    base.Save(ref produto);
                    
                    //Grave a grade de cores e tamanho do produto
                    using (ProdutoDetalheRepository gradeRepository = new ProdutoDetalheRepository())
                    {
                        var grades = gradeRepository.GetListByProduto(produto.Id, 0);

                        foreach (var g in grades)
                        {
                            gradeRepository.Delete(g.Id);
                        }

                        foreach (var gr in produto.Grade)
                        {
                            ProdutoDetalhe  g = gr;
                            g.Id = 0;
                            g.IdProduto = produto.Id;
                            gradeRepository.Save(ref g);
                            var fichasRelacao = new FichaTecnicaDoMaterialRelacaoRepository().GetAllViewByGradeProduto(g);
                            if (fichasRelacao == null || fichasRelacao.Count() == 0)
                            {
                                var fichas = new FichaTecnicaDoMaterialItemRepository().GetListByProduto(g.IdProduto);
                                foreach (var ficha in fichas)
                                {
                                    FichaTecnicaDoMaterialRelacao fichaRelacao = new FichaTecnicaDoMaterialRelacao();
                                    fichaRelacao.FichaTecnicaId = ficha.FichaTecnicaId;
                                    fichaRelacao.MateriaPrimaId = ficha.MateriaPrimaId;
                                    var materia = gradeRepository.GetListByProduto(ficha.MateriaPrimaId, 1).ToList();
                                    var corId = materia.Find(m => m.Idcor == g.Idcor);

                                    if (corId != null)
                                    {
                                        fichaRelacao.cor_materiaprima_Id = corId.Idcor;
                                    }
                                    else
                                    {
                                        fichaRelacao.cor_materiaprima_Id = materia.FirstOrDefault().Idcor;
                                    }

                                    var tamanhoId = materia.Find(m => m.IdTamanho == g.IdTamanho);

                                    if (tamanhoId != null)
                                    {
                                        fichaRelacao.Tamanho_Materiaprima_Id = tamanhoId.IdTamanho;
                                    }
                                    else
                                    {
                                        fichaRelacao.Tamanho_Materiaprima_Id = materia.FirstOrDefault().IdTamanho;
                                    }

                                    fichaRelacao.FichaTecnicaItemId = ficha.Id;
                                    fichaRelacao.Cor_Produto_Id = g.Idcor;
                                    fichaRelacao.ProdutoId = g.IdProduto;
                                    fichaRelacao.Tamanho_Produto_Id = g.IdTamanho;
                                    new FichaTecnicaDoMaterialRelacaoRepository().Save(ref fichaRelacao);
                                }
                            }
                        }

                        //gradeRepository.ExclusaoDeGradeEstoque(produto.Id, produto.IdAlmoxarifado);

                    }
                                       

                    //Grava os fornecedores do produto e seus custos
                    using (ProdutoFornecedorPrecoRepository FornecedorRepository = new ProdutoFornecedorPrecoRepository())
                    {                        
                        var fornecedores = FornecedorRepository.GetListByProdutoFornecedor(produto.Id);

                        foreach (var g in fornecedores)
                        {
                            FornecedorRepository.Delete(g.Id);                            
                        }

                        foreach (var gr in produto.Fornecedor)
                        {
                            ProdutoFornecedorPreco  g = gr;
                            g.Id = 0;
                            g.IdProduto = produto.Id;
                            FornecedorRepository.Save(ref g);

                        }                        

                    }


                    //Atualizar Tabela de Preço

                    /* 
                     * RETIRADO A PEDIDO DA SHIRLEY PARA JUFRAN 15-06-2022*********
                     * 
                    using (ItemTabelaPrecoRepository itemTabelaPrecoRepository = new ItemTabelaPrecoRepository())
                    {
                        var itensTabPreco = itemTabelaPrecoRepository.GetListByProduto(produto.Id);
                        var ret = new ProdutoFornecedorPrecoRepository().GetValoresSemInativos(produto.Id);

                        foreach (var itp in itensTabPreco)
                        {
                            var itemTabelaPreco = itp;
                            CalcularCustosProduto(ref itemTabelaPreco, produto, ret.ToList());
                            itemTabelaPrecoRepository.Save(ref itemTabelaPreco);
                        }
                    }
                    */


                    //Grava as imagens
                    using (ImagemRepository ImgRepository = new ImagemRepository())
                    {
                        var Img = ImgRepository.GetImagem("IdProduto",produto.Id);

                        foreach (var g in Img)
                        {
                            ImgRepository.Delete(g.Id);
                        }

                        foreach (var gr in produto.Imagem)
                        {
                            Imagem  g = gr;
                            g.Id = 0;
                            g.IdProduto = produto.Id;
                            g.IdColaborador = null;
                            g.IdFuncionario = null;
                            g.IdMateriaPrima = null;
                            ImgRepository.Save(ref g);
                        }
                    }                    

                    if (produto.IdItemVinculado != null)
                    {
                        using (ProdutoRepository pdrepository = new ProdutoRepository())
                        {
                            pdrepository.UpdateItemVinculado(Convert.ToInt32(produto.IdItemVinculado));
                        }

                    }

                    using (FichaTecnicaController fichaTecnicaRepository = new FichaTecnicaController())
                    {
                        var fichaTecnica = fichaTecnicaRepository.GetByProduto(produto.Id);
                        if (fichaTecnica != null)
                        {
                            fichaTecnica.Operacoes = new FichaTecnicaOperacaoRepository().GetByFichaTecnica(fichaTecnica.Id).ToList();
                            fichaTecnicaRepository.Save(ref fichaTecnica);
                        }
                        else if(produto.TipoItem == 0 && VestilloSession.IncluiFichaAutomatica && Convert.ToInt32(VestilloSession.OperacaoFichaAutomatica) > 0)
                        {
                            var operacao = new OperacaoPadraoRepository().GetById(Convert.ToInt32(VestilloSession.OperacaoFichaAutomatica));

                            if(operacao != null)
                            {
                                fichaTecnica = new FichaTecnica();
                                fichaTecnica.EmpresaId = produto.IdEmpresa;
                                fichaTecnica.ProdutoId = produto.Id;
                                fichaTecnica.Ativo = true;

                                var fichaOperacao = new FichaTecnicaOperacao();
                                fichaOperacao.OperacaoPadraoId = operacao.Id;
                                fichaOperacao.OperacaoPadraoDescricao = operacao.Descricao;
                                fichaOperacao.Numero = 1;
                                fichaOperacao.SetorId = operacao.IdSetor;
                                fichaOperacao.BalanceamentoId = operacao.IdBalanceamento;
                                fichaOperacao.TempoCosturaComAviamento = operacao.TempoCosturaComAviamento;
                                fichaOperacao.TempoCosturaSemAviamento = operacao.TempoCosturaSemAviamento;
                                fichaOperacao.Pontadas = 1;
                                fichaOperacao.TempoCalculado = fichaOperacao.Pontadas * operacao.TempoCosturaSemAviamento;
                                fichaOperacao.TempoEmSegundos = fichaOperacao.TempoCalculado * 60;
                                fichaOperacao.Ativo = true;

                                fichaTecnica.Operacoes.Add(fichaOperacao);

                                fichaTecnicaRepository.Save(ref fichaTecnica);
                            }                            
                        }
                    }

                    //incluir os itens na tabela de estoque
                    if(produto.TipoItem != 1)
                    {
                        using (ProdutoDetalheRepository pdt = new ProdutoDetalheRepository())
                        {
                            pdt.InclusaoDeGradeEstoque(produto.Grade, produto.IdAlmoxarifado);
                        }
                    }                   
                     

                    repository.CommitTransaction();

                    //chamar depois de gravar
                    var precoProduto = new ProdutoService().GetServiceFactory().GetPrecoDeCustoDoProduto(produto);
                    if(produto.TipoItem == 1 && VestilloSession.UsaFichaProduto)
                    {
                        using (ProdutoFichaRepository pdtFicha = new ProdutoFichaRepository())
                        {
                            pdtFicha.AtualizaCustoItens(produto.Id,precoProduto);
                        }
                    }
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        public void RecalculaCustoMaterialFichas(Produto Material, ref FichaTecnicaDoMaterialItem fi)
        {
            
            try
            {                
                using (FichaTecnicaDoMaterialItemController fichaTecnicaMaterialItemController = new FichaTecnicaDoMaterialItemController())
                {                    
                    List<ProdutoFornecedorPreco> ret = new List<ProdutoFornecedorPreco>();                     
                    var item = fi;                        

                    if (VestilloSession.CustoFichaPorExcecao && Material.TipoCustoFornecedor != 1)
                    {
                        ret = new ProdutoFornecedorPrecoRepository().GetValoresByExcecaoFicha(Material.Id, item.Id).ToList();

                    }
                    else
                        ret = new ProdutoFornecedorPrecoRepository().GetValoresSemInativos(Material.Id).ToList();
                    
                    if(ret.Count > 0)
                    {   
                        item.preco = ret.FirstOrDefault().PrecoFornecedor;
                        int IdFornecedorFicha = fi.idFornecedor;

                        if (IdFornecedorFicha > 0 && VestilloSession.AtualizaPrecoFichaPorFornecedor)
                        {
                            if (Material.TipoCalculoPreco == 2) //Pega a media
                            {
                                if (Material.TipoCustoFornecedor == 2)// Cor
                                {
                                    int ContCoresMedia = 0;
                                    decimal PrecoCorMedia = 0;
                                    foreach (var PrecoCorMediaFc in ret)
                                    {
                                        if (PrecoCorMediaFc.IdFornecedor == IdFornecedorFicha && PrecoCorMediaFc.PrecoCor > 0)
                                        {
                                            PrecoCorMedia += PrecoCorMediaFc.PrecoCor;
                                            ContCoresMedia += 1;
                                        }
                                    }
                                    if (ContCoresMedia == 0) ContCoresMedia = 1;
                                    item.preco = PrecoCorMedia / ContCoresMedia ;
                                }
                                else if (Material.TipoCustoFornecedor == 3)// Tamanho
                                {
                                    int ContTamanMedia = 0;
                                    decimal PrecoTamanMedia = 0;
                                    foreach (var PrecoTamanMediaFc in ret)
                                    {
                                        if (PrecoTamanMediaFc.IdFornecedor == IdFornecedorFicha && PrecoTamanMediaFc.PrecoTamanho > 0)
                                        {
                                            PrecoTamanMedia += PrecoTamanMediaFc.PrecoTamanho;
                                            ContTamanMedia += 1;
                                        }
                                    }
                                    if (ContTamanMedia == 0) ContTamanMedia = 1;
                                    item.preco = PrecoTamanMedia / ContTamanMedia ;
                                }
                                else // Fornecedor
                                {
                                    int ContFornecMedia = 0;
                                    decimal PrecoFornecMedia = 0;
                                    foreach (var PrecoFornecMediaFc in ret)
                                    {
                                        if(PrecoFornecMediaFc.IdFornecedor == IdFornecedorFicha && PrecoFornecMediaFc.PrecoFornecedor > 0)
                                        {
                                            PrecoFornecMedia += PrecoFornecMediaFc.PrecoFornecedor;
                                            ContFornecMedia += 1;
                                        }
                                    }
                                    if (ContFornecMedia == 0) ContFornecMedia = 1;
                                    item.preco = PrecoFornecMedia / ContFornecMedia ;
                                }
                            }
                            else // Pega  o maior valor
                            {
                                if (Material.TipoCustoFornecedor == 2)// Cor        
                                {
                                    decimal PrecoCorMaior = 0;
                                    foreach (var PrecoCorMaiorFc in ret)
                                    {
                                        if (PrecoCorMaiorFc.IdFornecedor == IdFornecedorFicha)
                                        {
                                            if(PrecoCorMaiorFc.PrecoCor > PrecoCorMaior)
                                            {
                                                PrecoCorMaior = PrecoCorMaiorFc.PrecoCor;
                                            }                                           
                                        }
                                    }
                                    item.preco = PrecoCorMaior;
                                }
                                    
                                else if (Material.TipoCustoFornecedor == 3)// Tamanho
                                {
                                    decimal PrecoTamanMaior = 0;
                                    foreach (var PrecoTamanMaiorFc in ret)
                                    {
                                        if (PrecoTamanMaiorFc.IdFornecedor == IdFornecedorFicha)
                                        {
                                            if (PrecoTamanMaiorFc.PrecoTamanho > PrecoTamanMaior)
                                            {
                                                PrecoTamanMaior = PrecoTamanMaiorFc.PrecoTamanho;
                                            }
                                        }
                                    }
                                    item.preco = PrecoTamanMaior;
                                }                                    
                                else
                                {
                                    decimal PrecoFornecMaior = 0;
                                    foreach (var PrecoFornecMaiorFc in ret)
                                    {
                                        if (PrecoFornecMaiorFc.IdFornecedor == IdFornecedorFicha)
                                        {
                                            if (PrecoFornecMaiorFc.PrecoFornecedor > PrecoFornecMaior)
                                            {
                                                PrecoFornecMaior = PrecoFornecMaiorFc.PrecoFornecedor;
                                            }
                                        }
                                    }
                                    item.preco = PrecoFornecMaior;
                                }
                                    
                            }
                        }
                        else
                        {
                            if (Material.TipoCalculoPreco == 2) //Pega a media
                            {
                                if (Material.TipoCustoFornecedor == 2)// Cor
                                {
                                    int count = ret.Where(x => x.PrecoCor > 0).ToList().Count();
                                    if (count == 0) count = 1;
                                    item.preco = (ret.Sum(x => x.PrecoCor) / count);
                                }
                                else if (Material.TipoCustoFornecedor == 3)// Tamanho
                                {
                                    int count = ret.Where(x => x.PrecoTamanho > 0).ToList().Count();
                                    if (count == 0) count = 1;
                                    item.preco = (ret.Sum(x => x.PrecoTamanho) / count);
                                }
                                else // Fornecedor
                                {
                                    int count = ret.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                                    if (count == 0) count = 1;
                                    item.preco = (ret.Sum(x => x.PrecoFornecedor) / count);
                                }
                            }
                            else // Pega  o maior valor
                            {
                                if (Material.TipoCustoFornecedor == 2)// Cor
                                    item.preco = ret.Max(x => x.PrecoCor);
                                else if (Material.TipoCustoFornecedor == 3)// Tamanho
                                    item.preco = ret.Max(x => x.PrecoTamanho);
                                else
                                    item.preco = ret.Max(x => x.PrecoFornecedor);
                            }
                        }
                        

                        item.valor = item.CustoCalculado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Vestillo.Lib.VestilloException(ex);
            }
        }

        public decimal CalculaCusto(decimal precoCompra, decimal ipi, decimal icms, decimal lucro)
        {
            decimal totalIPI = 0;
            decimal totalIcms = 0;
            decimal custo = 0;

            if (ipi > 0 || icms > 0 || lucro > 0)
            {
                totalIPI = ((precoCompra * ipi) / 100);
                totalIcms = ((precoCompra * icms) / 100);
                custo = (precoCompra + totalIcms + totalIPI);
            }
            else
            {
                custo = precoCompra;
            }

            return custo;
        }

        public decimal CalculaPrecoVenda(decimal precoCompra, decimal ipi, decimal icms, decimal lucro)
        {
            decimal custo = this.CalculaCusto(precoCompra, ipi, icms, lucro);
            decimal precoVenda = custo + ((custo * lucro) / 100);

            return precoVenda;
        }

        public void CalcularCustosProduto(ref ItemTabelaPreco item, Produto produto, List<ProdutoFornecedorPreco> fornecedores, TabelaPreco tabelaPreco = null)
        {
            var pc = new Business.Controllers.ProdutoController();
            var Tpreco = pc.CalculaPrecoVenda(produto.PrecoCompra, produto.Ipi, produto.Icms, produto.Lucro);
            if (item.PrecoVenda > 0 && Tpreco <= 0) return;

            item.PrecoVenda = pc.CalculaPrecoVenda(produto.PrecoCompra, produto.Ipi, produto.Icms, produto.Lucro);
            item.Lucro = pc.CalculaCusto(produto.PrecoCompra, produto.Ipi, produto.Icms, produto.Lucro);

            var tabelaPrecoController = new Business.Controllers.TabelaPrecoController();
            
            if (tabelaPreco == null)
                tabelaPreco = tabelaPrecoController.GetById(item.TabelaPrecoId);

            decimal frete = tabelaPreco.Frete; 
            decimal outrosEncargos = tabelaPreco.OutrosEncargos;
            decimal comissao = tabelaPreco.Comissao;
            decimal margemLucro = tabelaPreco.MargemLucro;
            decimal precoFornecedor = 0;

            if (fornecedores != null && fornecedores.Count > 0)
            {
                int tipoCaluloCusto = VestilloSession.TipoCalculoCustoFornecedor;

                if (produto.TipoCalculoPreco != null && produto.TipoCalculoPreco != 0)
                    tipoCaluloCusto = int.Parse(produto.TipoCalculoPreco.ToString());

                //#ALEX
                if (tipoCaluloCusto == 2) //Pega a media
                {
                    if (produto.TipoCustoFornecedor == 2)// Cor
                    {
                        int count = fornecedores.Where(x => x.PrecoCor > 0).ToList().Count();
                        if (count == 0) count = 1;
                        precoFornecedor = (fornecedores.Sum(x => x.PrecoCor) / count);
                    }
                    else if (produto.TipoCustoFornecedor == 3)// Tamanho
                    {
                        int count = fornecedores.Where(x => x.PrecoTamanho > 0).ToList().Count();
                        if (count == 0) count = 1;
                        precoFornecedor = (fornecedores.Sum(x => x.PrecoTamanho) / count);
                    }
                    else // Fornecedor
                    {
                        int count = fornecedores.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                        if (count == 0) count = 1;
                        precoFornecedor = (fornecedores.Sum(x => x.PrecoFornecedor) / count);
                    }
                }
                else // Pega  o maior valor
                {
                    if (produto.TipoCustoFornecedor == 2)// Cor
                        precoFornecedor = fornecedores.Max(x => x.PrecoCor);
                    else if (produto.TipoCustoFornecedor == 3)// Tamanho
                        precoFornecedor = fornecedores.Max(x => x.PrecoTamanho);
                    else
                        precoFornecedor = fornecedores.Max(x => x.PrecoFornecedor);
                }
            }

            item.CustoMedio = decimal.Round(precoFornecedor + (precoFornecedor * ((frete + outrosEncargos + comissao + margemLucro) / 100)), VestilloSession.QtdCasasPreco);
            item.PrecoSugerido = decimal.Round(item.CustoMedio + (item.CustoMedio * ((frete + outrosEncargos + comissao + margemLucro) / 100)), VestilloSession.QtdCasasPreco);
        }

        public Produto GetByReferencia(string referencia)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetByReferencia(referencia);
            }
        }

        public Produto GetByReferenciaFornecedor(string referencia)
        {

            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetByReferenciaFornecedor(referencia);
            }

        }

        public IEnumerable<FocoVendas> GetFocoVendas(FiltroRelatorioFocoVendas filtro,bool AgruparCor)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetFocoVendas(filtro, AgruparCor);                
                //return foco;
            }
        }
             
        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiqueta(FiltroRelatorioEtiquetaProduto filtro)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetProdutosParaEtiqueta(filtro);
            }
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaOrdem(FiltroRelatorioEtiquetaProdutoOrdem filtro)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetProdutosParaEtiquetaOrdem(filtro);
            }
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPedido(FiltroRelatorioEtiquetaProdutoPedidoVenda filtro)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetProdutosParaEtiquetaPedido(filtro);
            }
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPacote(FiltroRelatorioEtiquetaProdutoPacote filtro)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetProdutosParaEtiquetaPacote(filtro);
            }
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaComposicao(FiltroRelatorioEtiquetaComposicao filtro)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetProdutosParaEtiquetaComposicao(filtro);
            }
        }

        public IEnumerable<Produto> GetListPorReferencia(string referencia)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorReferencia(referencia);
            }
        }

        public IEnumerable<Produto> GetListPorDescricao(string desc)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<Produto> GetListPorFornecedor(string fornecedor)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorFornecedor(fornecedor);
            }
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnica(string referencia)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorReferenciaSemFichaTecnica(referencia);
            }
        }

        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnica(string desc)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorDescricaoSemFichaTecnica(desc);
            }
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnica(string fornecedor)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorFornecedorSemFichaTecnica(fornecedor);
            }
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnicaMaterial(string referencia)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorReferenciaSemFichaTecnicaMaterial(referencia);
            }
        }

        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnicaMaterial(string desc)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorDescricaoSemFichaTecnicaMaterial(desc);
            }
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnicaMaterial(string fornecedor)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorFornecedorSemFichaTecnicaMaterial(fornecedor);
            }
        }

        public Produto GetByUnicoCodBarras(string CodBarras)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetByUnicoCodBarras(CodBarras);
            }
        }

        public IEnumerable<MovimentarEstoqueView> GetProdutoPraMovimentarEstoque(string busca, bool buscarPorId, int almoxarifadoId)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetProdutoPraMovimentarEstoque(busca, buscarPorId, almoxarifadoId);
            }
        }


        public IEnumerable<DevolucaoItensView> GetProdutoDevolucaoItens(string busca, bool buscarPorId, int almoxarifadoId)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetProdutoDevolucaoItens(busca, buscarPorId, almoxarifadoId);
            }
        }




        public enum PossuiSaldoEstoque
        {
            SIM = 1,
            NAO = 2
        }

        public PossuiSaldoEstoque possuiSaldoEstoque(int AlmoxarifadoId, int iditem, int idcor, int idtamanho, decimal qtdParaMovimentar)
        {
            PossuiSaldoEstoque possuiSaldo = 0;
            var serviceEstoque = new EstoqueService().GetServiceFactory().GetSaldoAtualProduto(AlmoxarifadoId, iditem, idcor, idtamanho);
            var saldo = serviceEstoque.Saldo + serviceEstoque.Empenhado;
            if (qtdParaMovimentar <= saldo)
            {
                possuiSaldo = PossuiSaldoEstoque.SIM;
            }
            else
            {
                possuiSaldo = PossuiSaldoEstoque.NAO;

            }

            return possuiSaldo;
        }

        public PossuiSaldoEstoque possuiSaldoEstoque(int AlmoxarifadoId, int iditem, int itemPedidoVendaid,int idcor, int idtamanho, decimal qtdParaMovimentar,bool LiberaPedido, int PedidoVendaId, ref decimal qtdParcial)
        {
            PossuiSaldoEstoque possuiSaldo = 0;
            var serviceEstoque = new EstoqueService().GetServiceFactory().GetSaldoAtualProduto(AlmoxarifadoId, iditem, idcor, idtamanho);
            var repositoryLiberacaoPedido = new ItemLiberacaoPedidoVendaRepository().GetItensLiberacaoViewByProduto(AlmoxarifadoId, iditem, idcor, idtamanho, PedidoVendaId, itemPedidoVendaid);
            var servicePedido = new PedidoVendaService().GetServiceFactory().GetById(PedidoVendaId);

            var saldo = serviceEstoque.Saldo; 
                
            saldo+= repositoryLiberacaoPedido.QtdEmpenhada;

            if (VestilloSession.UsaConferencia)//&& servicePedido.Conferencia 
            {
                saldo = repositoryLiberacaoPedido.QtdConferida;
            }
            
            if (qtdParaMovimentar <= saldo)
            {
                possuiSaldo = PossuiSaldoEstoque.SIM;
            }
            else
            {
                if (saldo > 0)
                {
                    qtdParcial = saldo;
                    possuiSaldo = PossuiSaldoEstoque.NAO;
                }
                else
                {
                    qtdParcial = 0;
                    possuiSaldo = PossuiSaldoEstoque.NAO;
                }

            }

            return possuiSaldo;
        }


        public Produto GetByReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetByReferenciaComFichaTecnica(referencia, fichaTecnicaCompleta);
            }
        }

        public IEnumerable<Produto> GetListPorTipoAtivo(Produto.enuTipoItem tipo,bool Ambos = false)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorTipoAtivo(tipo);
            }
        }

        public decimal GetPrecoDeCustoDoProduto(Produto produto)
        {
            List<decimal> lstPreco = new List<decimal>();
            List<ProdutoFornecedorPreco> ListFornecedor;                
            var FornecedorProduto = new Vestillo.Business.Service.ProdutoFornecedorPrecoService().GetServiceFactory();
            IEnumerable<ProdutoFornecedorPreco> enumerableFornecedor;
            enumerableFornecedor = new ProdutoFornecedorPrecoRepository().GetValoresSemInativos(produto.Id);
            ListFornecedor = enumerableFornecedor.ToList();
            if (ListFornecedor != null && ListFornecedor.Any())
            {
                foreach (var item in ListFornecedor)
                {
                    if (produto.TipoCustoFornecedor == 1)
                    {
                        lstPreco.Add(item.PrecoFornecedor);
                    }
                    else if (produto.TipoCustoFornecedor == 2)
                    {
                        lstPreco.Add(item.PrecoCor);
                    }
                    else if (produto.TipoCustoFornecedor == 3)
                    {
                        lstPreco.Add(item.PrecoTamanho);
                    }
                }

                if (lstPreco != null && lstPreco.Count > 0)
                {
                    if (produto.TipoCalculoPreco == 2)//media
                        return lstPreco.Average();
                    else
                        return lstPreco.Max();
                }
                else
                    return 0;

            }
            else
                return 0;        
            

        }

        public IEnumerable<Produto> GetListPorReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorReferenciaComFichaTecnica(referencia, fichaTecnicaCompleta);
            }
        }
        
        public IEnumerable<Produto> GetListPorDescricaoComFichaTecnica(string desc, bool fichaTecnicaCompleta)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListPorDescricaoComFichaTecnica(desc, fichaTecnicaCompleta);
            }
        }

        public IEnumerable<Produto> GetListById(int id)
        {
            using (ProdutoRepository repository = new ProdutoRepository())
            {
                return repository.GetListById(id);
            }
        }
        
        public IEnumerable<Produto> GetAllAtivos()
        {
            return _repository.GetAllAtivos();
        }

        public IEnumerable<Produto> GetListProdutosQuePossuemGradeENaoPossuemFichaTecnica()
        {
            return _repository.GetListProdutosQuePossuemGradeENaoPossuemFichaTecnica();
        }

        public IEnumerable<Produto> GetListMateriasPrimasQuePossuemGrade()
        {
            return _repository.GetListMateriasPrimasQuePossuemGrade();
        }

        public IEnumerable<Produto> GetProdutosParaManutencaoFichaTecnica(bool comFicha, bool semFicha)
        {
            return _repository.GetProdutosParaManutencaoFichaTecnica(comFicha, semFicha);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(List<int> produtosId, int ordemId)
        {
            return _repository.GetListPorIdComFichaTecnicaMaterial(produtosId, ordemId);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterialSemOP(List<int> produtosId, int ordemId)
        {
            return _repository.GetListPorIdComFichaTecnicaMaterialSemOP(produtosId, ordemId);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(ItemOrdemProducaoView item, int ordemId)
        {
            return _repository.GetListPorIdComFichaTecnicaMaterial(item, ordemId);
        }

        public IEnumerable<CompraMaterial> GetListPorIdComFichaTecnicaMaterial(FiltroRelatorioCompraMaterial filtro)
        {
            return _repository.GetListPorIdComFichaTecnicaMaterial(filtro);
        }

        public IEnumerable<Produto> GetProdutosLiberados(string referencia)
        {
            return _repository.GetProdutosLiberados(referencia);
        }
    }         

}
