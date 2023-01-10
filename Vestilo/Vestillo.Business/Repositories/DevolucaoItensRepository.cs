using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{

    public class DevolucaoItensRepository : GenericRepository<DevolucaoItens>
    {
        public DevolucaoItensRepository() : base(new DapperConnection<DevolucaoItens>())
        {
        }

        public IEnumerable<DevolucaoItens> GetListByNfeItens(int IdNFE)
        {
            var SQL = new Select()
                .Campos("devolucaoitens.Id  as Id,devolucaoitens.IdDevolucao as IdDevolucao,devolucaoitens.total as total, devolucaoitens.* ")
                .From("devolucaoitens")
                .Where("IdDevolucao = " + IdNFE);

            var cn = new DapperConnection<DevolucaoItens>();
            var tm = new DevolucaoItens();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public void DeleteByNfeItens(int IdNFE)
        {
            string sql = "DELETE FROM devolucaoitens WHERE IdDevolucao = " + IdNFE;
            _cn.ExecuteNonQuery(sql);
        }

        public IEnumerable<DevolucaoItens> GetListByNfeItensComplementar(int IdNFE)
        {
            var SQL = new Select()
                .Campos("nfeitens.IdTipoMov,nfeitens.CalculaIcms,nfeitens.total as Total, nfeitens.IdItem,nfeitens.NumItem as NumLinha, nfeitens.* ")
                .From("nfeitens")
                .Where("idnfe = " + IdNFE);

            var cn = new DapperConnection<DevolucaoItens>();
            var tm = new DevolucaoItens();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }


        public IEnumerable<DevolucaoItensView> GetListByNfeItensView(int IdNFE)
        {
            var SQL = new Select()
                .Campos(" produtos.Referencia as ProdutoReferencia, produtos.descricao as ProdutoDescricao,cores.descricao as DescCor, " +
                        "cores.Abreviatura as CorAbreviatura,tamanhos.Abreviatura as TamanhoAbreviatura,devolucaoitens.quantidade as Qtd,devolucaoitens.preco as Preco, " + 
                        "devolucaoitens.total as total  ")
                 .From(" devolucaoitens ")
                 .InnerJoin("produtos", "produtos.id =  devolucaoitens.iditem")                
                .LeftJoin("cores", "cores.id =  devolucaoitens.idcor")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = devolucaoitens.idtamanho AND produtodetalhes.idcor = devolucaoitens.idcor")
                .LeftJoin("tamanhos", "tamanhos.id =  devolucaoitens.idtamanho")
                .Where("IdDevolucao = " + IdNFE);

            var cn = new DapperConnection<DevolucaoItensView>();
            var tm = new DevolucaoItensView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<DevolucaoItensView> GetListByNfeItensViewAgrupados(int IdNFE)
        {
            var SQL = new Select()
                .Campos("nfeitens.IdTipoMov,nfeitens.CalculaIcms,SUM(nfeitens.total) as Total,produtos.descricao as DescProduto,produtos.referenciacli as referenciacli, " +
                        "produtos.Referencia as referencia, produtodetalhes.referenciagradecli as referenciagradecli,produtodetalhes.codbarras as codbarrasgrade, " +
                        "produtos.CodBarrasUnico as  codbarrasunico,produtos.ncm as ncm,produtos.Cest as cest, nfeitens.IdItem,nfeitens.NumItem as NumLinha, " +
                        "'' as DescCor,'' as RefCor, produtos.Fci as fci,produtos.ValorImportacao as parcelaimp,produtos.ci as ci, produtos.draw as draw, produtos.origem as origem, " +
                        "tipomovimentacoes.cst as cst,tipomovimentacoes.cstipi as cstipi,tipomovimentacoes.enquadraipi as enquadraipi,Percenticms, " +
                        "tipomovimentacoes.percentualreducaoicms as percentualreducaoicms, tipomovimentacoes.csosn as csosn,tipomovimentacoes.creditoicms as creditoicms, " +
                        "tipomovimentacoes.enquadracofins as enquadracofins,tipomovimentacoes.enquadrapis as enquadrapis,nfeitens.IdItemNoPedido as IdItemNoPedido, " +
                        "'' as DescTamanho, '' as RefTamanho,cfops.descricao as DescCfop,cfops.referencia as RefCfop,unidademedidas.abreviatura as unidade, " +
                        "sum(nfeitens.quantidade) as quantidade,nfeitens.preco as preco,SUM(nfeitens.total) as total,SUM(nfeitens.DescontoBonificado) as DescontoBonificado, " +
                        "SUM(nfeitens.DescontoPis) as DescontoPis ,SUM(nfeitens.DescontoSefaz) as DescontoSefaz,SUM(nfeitens.DescontoRateado) as DescontoRateado, " +
                        "SUM(nfeitens.BaseIcmsRateado) as BaseIcmsRateado,SUM(nfeitens.ValorIcmsRateado) as ValorIcmsRateado,SUM(nfeitens.DespesaRateio) as DespesaRateio, " +
                        "SUM(nfeitens.FreteRateio) as FreteRateio, SUM(nfeitens.SeguroRateio) as SeguroRateio ,nfeitens.icmsintestadual as icmsintestadual, " +
                        "nfeitens.icmsestadodestino as icmsestadodestino ,nfeitens.icmsdiferenca as icmsdiferenca ,nfeitens.partemissor as partemissor, " +
                        "nfeitens.partdest as partdest ,SUM(nfeitens.valorbcicmsdest) as valorbcicmsdest,SUM(nfeitens.valoricmsintestadual) as valoricmsintestadual," +
                        "SUM(nfeitens.valoricmsdiferenca) as valoricmsdiferenca,SUM(nfeitens.valorpartemissor) as valorpartemissor,SUM(nfeitens.valorpartdest) as valorpartdest, " +
                        " SUM(nfeitens.basefcpestado) as basefcpestado , SUM(nfeitens.Valorfcpestado) as Valorfcpestado, nfeitens.alqfcp as alqfcp, " +
                        "SUM(nfeitens.valorfcp) as valorfcp,nfeitens.RefPedidoCliente as RefPedidoCliente, nfeitens.SeqPedCliente as SeqPedCliente  ")
                .From("nfeitens")
                .InnerJoin("produtos", "produtos.id =  nfeitens.iditem")
                .InnerJoin("tipomovimentacoes", "tipomovimentacoes.id = nfeitens.IdTipoMov")
                .InnerJoin("cfops", "cfops.id = tipomovimentacoes.idcfop")
                .InnerJoin("unidademedidas", "unidademedidas.id = produtos.IdUniMedida")
                .LeftJoin("cores", "cores.id =  nfeitens.idcor")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = nfeitens.idtamanho AND produtodetalhes.idcor = nfeitens.idcor")
                .LeftJoin("tamanhos", "tamanhos.id =  nfeitens.idtamanho")
                .Where("idnfe = " + IdNFE)
                .GroupBy("nfeitens.iditem");

            var cn = new DapperConnection<DevolucaoItensView>();
            var tm = new DevolucaoItensView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

    }





}
