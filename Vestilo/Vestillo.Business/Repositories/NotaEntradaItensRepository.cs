
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
    public class NotaEntradaItensRepository : GenericRepository<NotaEntradaItens>
    {
         public NotaEntradaItensRepository(): base(new DapperConnection<NotaEntradaItens>())
        {
        }

         public IEnumerable<NotaEntradaItens> GetListByNotaEntradaItens(int IdNota)
         {
             var SQL = new Select()
                 .Campos("notaentradaitens.IdTipoMov,notaentradaitens.CalculaIcms,notaentradaitens.total as Total,produtos.descricao as DescProduto, notaentradaitens.IdItem,notaentradaitens.NumItem as NumLinha, cores.descricao as DescCor, tamanhos.descricao as DescTamanho, notaentradaitens.* ")
                 .From("notaentradaitens")
                 .InnerJoin("produtos", "produtos.id =  notaentradaitens.iditem")
                 .LeftJoin("cores", "cores.id =  notaentradaitens.idcor")
                 .LeftJoin("tamanhos", "tamanhos.id =  notaentradaitens.idtamanho")
                 .Where("IdNota = " + IdNota);

             var cn = new DapperConnection<NotaEntradaItens>();
             var tm = new NotaEntradaItens();
             return cn.ExecuteStringSqlToList(tm, SQL.ToString());
         }



         public IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensView(int IdNota)
         {
             var SQL = new Select()
                 .Campos("notaentradaitens.IdTipoMov,notaentradaitens.CalculaIcms,notaentradaitens.total as Total,produtos.descricao as DescProduto,produtos.referenciacli as referenciacli, " +
                 "produtos.Referencia as referencia, produtodetalhes.referenciagradecli as referenciagradecli,produtodetalhes.codbarras as codbarrasgrade, " +
                 "produtos.CodBarrasUnico as  codbarrasunico,produtos.ncm as ncm,produtos.Cest as cest, notaentradaitens.IdItem,notaentradaitens.NumItem as NumLinha, cores.descricao as DescCor, " +
                 "produtos.Fci as fci,produtos.ValorImportacao as parcelaimp,produtos.ci as ci, produtos.draw as draw, produtos.origem as origem, tipomovimentacoes.cst as cst," +
                 "tipomovimentacoes.cstipi as cstipi,tipomovimentacoes.enquadraipi as enquadraipi, pedidocompra.referencia as RefPedido," +
                 "tipomovimentacoes.percentualreducaoicms as percentualreducaoicms, tipomovimentacoes.csosn as csosn,tipomovimentacoes.creditoicms as creditoicms, " +
                 "tipomovimentacoes.enquadracofins as enquadracofins,tipomovimentacoes.enquadrapis as enquadrapis,notaentradaitens.IdItemNoPedido as IdItemNoPedido," +
                 "tamanhos.descricao as DescTamanho,tamanhos.Abreviatura as AbreviaturaTamanho,cores.Abreviatura as AbreviaturaCores, cfops.descricao as DescCfop,cfops.referencia as RefCfop,unidademedidas.abreviatura as unidade, notaentradaitens.* ")
                 .From("notaentradaitens")
                 .InnerJoin("produtos", "produtos.id =  notaentradaitens.iditem")
                 .InnerJoin("tipomovimentacoes", "tipomovimentacoes.id = notaentradaitens.IdTipoMov")
                 .InnerJoin("cfops", "cfops.id = tipomovimentacoes.idcfop")
                 .InnerJoin("unidademedidas", "unidademedidas.id = produtos.IdUniMedida")
                 .LeftJoin("itenspedidocompra", "itenspedidocompra.Id =  notaentradaitens.IdItemNoPedido")
                 .LeftJoin("pedidocompra", "pedidocompra.Id =  itenspedidocompra.PedidoCompraId")
                 .LeftJoin("cores", "cores.id =  notaentradaitens.idcor")
                 .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = notaentradaitens.idtamanho AND produtodetalhes.idcor = notaentradaitens.idcor")
                 .LeftJoin("tamanhos", "tamanhos.id =  notaentradaitens.idtamanho")
                 .Where("IdNota = " + IdNota);

             var cn = new DapperConnection<NotaEntradaItensView>();
             var tm = new NotaEntradaItensView();
             return cn.ExecuteStringSqlToList(tm, SQL.ToString());
         }

         public IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensViewAgrupados(int IdNota)
         {
             var SQL = new Select()
                 .Campos("notaentradaitens.IdTipoMov,notaentradaitens.CalculaIcms,SUM(notaentradaitens.total) as Total,produtos.descricao as DescProduto,produtos.referenciacli as referenciacli, " +
                         "produtos.Referencia as referencia, produtodetalhes.referenciagradecli as referenciagradecli,produtodetalhes.codbarras as codbarrasgrade, " +
                         "produtos.CodBarrasUnico as  codbarrasunico,produtos.ncm as ncm,produtos.Cest as cest, notaentradaitens.IdItem,notaentradaitens.NumItem as NumLinha, " +
                         "'' as DescCor, produtos.Fci as fci,produtos.ValorImportacao as parcelaimp,produtos.ci as ci, produtos.draw as draw, produtos.origem as origem, " +
                         "tipomovimentacoes.cst as cst,tipomovimentacoes.cstipi as cstipi,tipomovimentacoes.enquadraipi as enquadraipi,Percenticms, " +
                         "tipomovimentacoes.percentualreducaoicms as percentualreducaoicms, tipomovimentacoes.csosn as csosn,tipomovimentacoes.creditoicms as creditoicms, " +
                         "tipomovimentacoes.enquadracofins as enquadracofins,tipomovimentacoes.enquadrapis as enquadrapis,notaentradaitens.IdItemNoPedido as IdItemNoPedido, " +
                         "'' as DescTamanho, cfops.descricao as DescCfop,cfops.referencia as RefCfop,unidademedidas.abreviatura as unidade, " +
                         "sum(notaentradaitens.quantidade) as quantidade,notaentradaitens.preco as preco,SUM(notaentradaitens.total) as total,SUM(notaentradaitens.DescontoBonificado) as DescontoBonificado, " +
                         "SUM(notaentradaitens.DescontoPis) as DescontoPis ,SUM(notaentradaitens.DescontoSefaz) as DescontoSefaz,SUM(notaentradaitens.DescontoRateado) as DescontoRateado, " +
                         "SUM(notaentradaitens.BaseIcmsRateado) as BaseIcmsRateado,SUM(notaentradaitens.ValorIcmsRateado) as ValorIcmsRateado,SUM(notaentradaitens.DespesaRateio) as DespesaRateio, " +
                         "SUM(notaentradaitens.FreteRateio) as FreteRateio, SUM(notaentradaitens.SeguroRateio) as SeguroRateio ,notaentradaitens.icmsintestadual as icmsintestadual, " +
                         "notaentradaitens.icmsestadodestino as icmsestadodestino ,notaentradaitens.icmsdiferenca as icmsdiferenca ,notaentradaitens.partemissor as partemissor, " +
                         "notaentradaitens.partdest as partdest ,SUM(notaentradaitens.valorbcicmsdest) as valorbcicmsdest,SUM(notaentradaitens.valoricmsintestadual) as valoricmsintestadual," +
                         "SUM(notaentradaitens.valoricmsdiferenca) as valoricmsdiferenca,SUM(notaentradaitens.valorpartemissor) as valorpartemissor,SUM(notaentradaitens.valorpartdest) as valorpartdest, " +
                         "SUM(notaentradaitens.valorfcp) as valorfcp ")
                 .From("notaentradaitens")
                 .InnerJoin("produtos", "produtos.id =  notaentradaitens.iditem")
                 .InnerJoin("tipomovimentacoes", "tipomovimentacoes.id = notaentradaitens.IdTipoMov")
                 .InnerJoin("cfops", "cfops.id = tipomovimentacoes.idcfop")
                 .InnerJoin("unidademedidas", "unidademedidas.id = produtos.IdUniMedida")
                 .LeftJoin("cores", "cores.id =  notaentradaitens.idcor")
                 .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = notaentradaitens.idtamanho AND produtodetalhes.idcor = notaentradaitens.idcor")
                 .LeftJoin("tamanhos", "tamanhos.id =  notaentradaitens.idtamanho")
                 .Where("IdNota = " + IdNota)
                 .GroupBy("notaentradaitens.iditem");

             var cn = new DapperConnection<NotaEntradaItensView>();
             var tm = new NotaEntradaItensView();
             return cn.ExecuteStringSqlToList(tm, SQL.ToString());
         }

    }
}

