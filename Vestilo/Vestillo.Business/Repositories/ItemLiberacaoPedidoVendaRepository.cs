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
    public class ItemLiberacaoPedidoVendaRepository: GenericRepository<ItemLiberacaoPedidoVenda>
    {
        public ItemLiberacaoPedidoVendaRepository()
            : base(new DapperConnection<ItemLiberacaoPedidoVenda>())
        {
        }

        public List<ItemLiberacaoPedidoVenda> GetByPedidoVenda(int pedidoVendaId)
        {
            return _cn.ExecuteToList(new ItemLiberacaoPedidoVenda(), "ItemPedidoVendaId  IN (SELECT ID FROM itenspedidovenda where pedidovendaid = " + pedidoVendaId.ToString() + ")").ToList();
        }


        public IEnumerable<ItemLiberacaoPedidoVenda> GetByItemPedidoVenda(int itemPedidoVendaId)
        {
            var SQL = new Select()
                .Campos("*")
                .From("itensliberacaopedidovenda")
                .Where("itensliberacaopedidovenda.ItemPedidoVendaId = " + itemPedidoVendaId);

            var cn = new DapperConnection<ItemLiberacaoPedidoVenda>();
            var tm = new ItemLiberacaoPedidoVenda();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoView(int IdPedido)
        {
            var SQL = new Select()
                .Campos("itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Descricao as DescTamanho,cores.Id as IdCor,cores.Descricao as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, itensliberacaopedidovenda.Qtd as Qtd, " +
                " itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.Id as Id, itensliberacaopedidovenda.QtdEmpenhada as QtdEmpenhada, itensliberacaopedidovenda.QtdFaturada as QtdFaturada," +
                " itenspedidovenda.PedidoVendaId as PedidoVendaId, itensliberacaopedidovenda.QtdConferencia as QtdConferencia, itensliberacaopedidovenda.QtdConferida, itensliberacaopedidovenda.SemEmpenho as SemEmpenho")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("(itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada) > 0 AND itenspedidovenda.PedidoVendaId = " + IdPedido);

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }


        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByConferencia(int conferenciaId)
        {
            var SQL = new Select()
                .Campos("CONVERT(PedidoVendaConferenciaItens.Sequencia, CHAR(3)) AS Sequencia, itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Descricao as DescTamanho,cores.Id as IdCor,cores.Descricao as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, itensliberacaopedidovenda.Qtd as Qtd, " +
                " itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.Id as Id, itensliberacaopedidovenda.QtdEmpenhada as QtdEmpenhada, itensliberacaopedidovenda.QtdFaturada as QtdFaturada, itenspedidovenda.PedidoVendaId as PedidoVendaId, itensliberacaopedidovenda.QtdConferencia as QtdConferencia, " +
                " PedidoVendaConferenciaItens.QtdConferida AS Conferido, produtodetalhes.codbarras as CodigoBarras")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .InnerJoin("PedidoVendaConferencia", "PedidoVendaConferencia.PedidoVendaId =  itenspedidovenda.PedidoVendaId")
                .InnerJoin("PedidoVendaConferenciaItens", "PedidoVendaConferenciaItens.PedidoVendaConferenciaId = PedidoVendaConferencia.Id AND PedidoVendaConferenciaItens.CorId = itenspedidovenda.CorId AND PedidoVendaConferenciaItens.TamanhoId = itenspedidovenda.TamanhoId AND PedidoVendaConferenciaItens.ProdutoId = produtos.id")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("PedidoVendaConferencia.Id = " + conferenciaId)
                .OrderBy("1");

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoParaConferenciaView(List<int> IdPedido)
        {
            var SQL = new Select()
                .Campos("itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Abreviatura as DescTamanho,cores.Id as IdCor,cores.Abreviatura as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, sum(itensliberacaopedidovenda.Qtd) as Qtd, " +
                " SUM(itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada) as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.Id as Id," +
                " SUM(itensliberacaopedidovenda.QtdEmpenhada) as QtdEmpenhada, SUM(itensliberacaopedidovenda.QtdFaturada) as QtdFaturada, itenspedidovenda.PedidoVendaId as PedidoVendaId," +
                " SUM(itensliberacaopedidovenda.QtdConferencia) as QtdConferencia, SUM(itensliberacaopedidovenda.QtdConferida) as QtdConferida,  produtodetalhes.codbarras as CodigoBarras ")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                //.Where("itensliberacaopedidovenda.QtdConferencia < itensliberacaopedidovenda.Qtd AND itenspedidovenda.PedidoVendaId in ( " + string.Join(",", IdPedido) + " )")
                .Where("(itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada) > 0 AND itensliberacaopedidovenda.QtdConferencia > 0 AND  itensliberacaopedidovenda.SemEmpenho = 0 AND itenspedidovenda.PedidoVendaId in ( " + string.Join(",", IdPedido) + " )")
                .GroupBy(" produtos.id, cores.Id, tamanhos.id")
                .OrderBy(" produtos.Referencia, cores.Descricao, tamanhos.Id");

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParaConferenciaSemEmpenho(List<int> IdPedido, bool visualizar)
        {
            string campos = "";
            if (visualizar)
                campos = "itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Abreviatura as DescTamanho,cores.Id as IdCor,cores.Abreviatura as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, sum(itensliberacaopedidovenda.QtdConferencia) as Qtd, " +
                " SUM(itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada) as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.Id as Id," +
                " SUM(itensliberacaopedidovenda.QtdEmpenhada) as QtdEmpenhada, SUM(itensliberacaopedidovenda.QtdFaturada) as QtdFaturada, itenspedidovenda.PedidoVendaId as PedidoVendaId," +
                " SUM(itensliberacaopedidovenda.QtdConferencia - itensliberacaopedidovenda.QtdConferida ) as Diferenca,SUM(itensliberacaopedidovenda.QtdConferencia - itensliberacaopedidovenda.QtdConferida ) as DiferencaOld, SUM(itensliberacaopedidovenda.QtdConferencia) as QtdConferencia, SUM(itensliberacaopedidovenda.QtdConferida) as QtdConferida,  produtodetalhes.codbarras as CodigoBarras, SUM(itensliberacaopedidovenda.QtdConferida) as Conferido ";
            else
                campos = "itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Abreviatura as DescTamanho,cores.Id as IdCor,cores.Abreviatura as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, SUM(itensliberacaopedidovenda.QtdConferencia - itensliberacaopedidovenda.QtdConferida ) as Qtd, " +
                " SUM(itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada) as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.Id as Id," +
                " SUM(itensliberacaopedidovenda.QtdEmpenhada) as QtdEmpenhada, SUM(itensliberacaopedidovenda.QtdFaturada) as QtdFaturada, itenspedidovenda.PedidoVendaId as PedidoVendaId," +
                " SUM(itensliberacaopedidovenda.QtdConferencia - itensliberacaopedidovenda.QtdConferida ) as Diferenca,SUM(itensliberacaopedidovenda.QtdConferencia - itensliberacaopedidovenda.QtdConferida ) as DiferencaOld, SUM(itensliberacaopedidovenda.QtdConferencia) as QtdConferencia, SUM(itensliberacaopedidovenda.QtdConferida) as QtdConferida,  produtodetalhes.codbarras as CodigoBarras ";

            var SQL = new Select()
                .Campos(campos)
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("(itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada) > 0 AND itensliberacaopedidovenda.QtdConferencia > 0 AND itensliberacaopedidovenda.QtdConferencia <> itensliberacaopedidovenda.QtdConferida AND  itensliberacaopedidovenda.SemEmpenho = 1 AND itenspedidovenda.PedidoVendaId in ( " + string.Join(",", IdPedido) + " )")
                .GroupBy(" produtos.id, cores.Id, tamanhos.id")
                .OrderBy(" produtos.Referencia, cores.Descricao, tamanhos.Id");

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoViewByDataEPedido(DateTime data, int idPedido)
        {
            var SQL = new Select()
                .Campos("itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Descricao as DescTamanho,cores.Id as IdCor,cores.Descricao as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, itensliberacaopedidovenda.Qtd as Qtd, " +
                " itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.QtdEmpenhada as QtdEmpenhada, itensliberacaopedidovenda.QtdFaturada as QtdFaturada ")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("itensliberacaopedidovenda.Data = " + data + " AND itenspedidovenda.PedidoVendaId = " + idPedido);

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoViewByLiberacaoIdEPedido(int idLiberacao, int idPedido)
        {
            var SQL = new Select()
                .Campos("itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Descricao as DescTamanho,cores.Id as IdCor,cores.Descricao as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, itensliberacaopedidovenda.Qtd as Qtd, " +
                " itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.QtdEmpenhada as QtdEmpenhada, itensliberacaopedidovenda.QtdFaturada as QtdFaturada, estoque.Saldo as QtdEstoque, " +
                "itensliberacaopedidovenda.Status as Status, itensliberacaopedidovenda.QtdConferencia as QtdConferencia, IFNULL(itensliberacaopedidovenda.QtdConferida,0) as QtdConferida")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("estoque", "estoque.ProdutoId = produtos.id AND IFNULL(estoque.TamanhoId, 0) = IFNULL(itenspedidovenda.TamanhoId,0) AND IFNULL(estoque.CorId, 0) = IFNULL(itenspedidovenda.CorId, 0) AND estoque.AlmoxarifadoId = itensliberacaopedidovenda.AlmoxarifadoId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("itensliberacaopedidovenda.LiberacaoId = " + idLiberacao + " AND itenspedidovenda.PedidoVendaId = " + idPedido);

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public ItemLiberacaoPedidoVendaView GetItensLiberacaoViewByProduto(int AlmoxarifadoId, int idProduto, int idCor, int idTamanho, int pedidoVendaId, int itemPedidoVendaId)
        {
            var SQL = new Select()
                .Campos("itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Descricao as DescTamanho,cores.Id as IdCor,cores.Descricao as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, itensliberacaopedidovenda.Qtd as Qtd, itensliberacaopedidovenda.SemEmpenho as SemEmpenho, " +
                " itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada as QtdLiberada,  itenspedidovenda.Preco as Preco, itensliberacaopedidovenda.QtdEmpenhada as QtdEmpenhada, itensliberacaopedidovenda.QtdFaturada as QtdFaturada, itensliberacaopedidovenda.QtdConferencia as QtdConferencia, itensliberacaopedidovenda.QtdConferida as QtdConferida ")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("itensliberacaopedidovenda.AlmoxarifadoId = " + AlmoxarifadoId + " AND itenspedidovenda.ProdutoId = " + idProduto + " AND itenspedidovenda.PedidoVendaId = " + pedidoVendaId + " AND itensliberacaopedidovenda.id = " + itemPedidoVendaId);

            if (idCor > 0)
            {
                SQL.Where(" itenspedidovenda.CorId =  " + idCor.ToString());
            }
            else
            {
                SQL.Where(" itenspedidovenda.CorId IS NULL");
            }

            if (idTamanho > 0)
            {
                SQL.Where(" itenspedidovenda.TamanhoId = " + idTamanho.ToString());
            }
            else
            {
                SQL.Where(" itenspedidovenda.TamanhoId IS NULL");
            }

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            cn.ExecuteToModel(ref tm, SQL.ToString());
            return tm;
        }

        public IEnumerable<LiberacaoPedidoVenda> GetLiberacaoPedidoVenda(int pedidoVendaId)
        {
            var cn = new DapperConnection<LiberacaoPedidoVenda>();

           var SQL = new Select()
                .Campos("itensliberacaopedidovenda.LiberacaoId, itensliberacaopedidovenda.Data as DataLiberacao, " +
                " itensliberacaopedidovenda.Status, SUM(itensliberacaopedidovenda.Qtd) as Qtd, " +
                " SUM(itensliberacaopedidovenda.Qtd) - SUM(itensliberacaopedidovenda.QtdFaturada) as QtdLiberada, " +
                " SUM(itensliberacaopedidovenda.QtdFaturada) as QtdFaturada")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .Where("(itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada) > 0 AND itenspedidovenda.PedidoVendaId = " + pedidoVendaId)
                .GroupBy(" LiberacaoId");

           return cn.ExecuteStringSqlToList(new LiberacaoPedidoVenda(), SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVenda> GetLiberacaoPedidoVendaPorPedidoProdutoEGrade(int IdPedido, int idProduto, int idCor, int idTamanho)
        {
            var SQL = new Select()
              .Campos("itensliberacaopedidovenda.*")
              .From("itenspedidovenda")
              .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
              .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
              .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
              .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
              .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
              .Where("itenspedidovenda.ProdutoId = " + idProduto + " AND itenspedidovenda.CorId " + (idCor == 0 ? " IS NULL" : " = " + idCor.ToString()) + " AND itenspedidovenda.TamanhoId " + (idTamanho == 0 ? " IS NULL" : " = " + idTamanho.ToString()) + " AND itenspedidovenda.PedidoVendaId = " + IdPedido);

            return _cn.ExecuteStringSqlToList(new ItemLiberacaoPedidoVenda(), SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque()

        {
            var SQL = new Select()
                .Campos("itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Descricao as DescTamanho,cores.Id as IdCor,cores.Descricao as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, itensliberacaopedidovenda.QtdNaoAtendida as Qtd, " +
                " itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada as QtdLiberada,  itenspedidovenda.Preco as Preco, itenspedidovenda.pedidovendaid, " +
                " pedidovenda.Referencia as ReferenciaPedidoVenda, itensliberacaopedidovenda.LiberacaoId as SeqLibPedVenda")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("pedidovenda", "pedidovenda.id = itenspedidovenda.PedidoVendaId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("(itensliberacaopedidovenda.Status = " + (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial + " OR itensliberacaopedidovenda.Status = " + (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque
                + ") AND itensliberacaopedidovenda.QtdNaoAtendida > 0;");

            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVenda> GetItensLiberacao()
        {
            var SQL = new Select()
                .Campos("*")
                .From("itensliberacaopedidovenda")
                .Where("(itensliberacaopedidovenda.Status = " + (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial + " OR itensliberacaopedidovenda.Status = " + (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque
                + ") AND itensliberacaopedidovenda.QtdNaoAtendida > 0;");

            var cn = new DapperConnection<ItemLiberacaoPedidoVenda>();
            var tm = new ItemLiberacaoPedidoVenda();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public int GetUltimaLiberacao()
        {
            var cn = new DapperConnection<ItemLiberacaoPedidoVenda>();

            var SQL = new Select()
                 .Campos("*")
                 .From("itensliberacaopedidovenda")
                 .OrderBy("LiberacaoId DESC LIMIT 1");
            var consulta = new ItemLiberacaoPedidoVenda();
            cn.ExecuteToModel(ref consulta, SQL.ToString());
            return consulta.LiberacaoId;
        }


        public decimal GetValorDaLiberacao(int pedidoVendaId)
        {
            var cn = new DapperConnection<ItemPedidoVendaValorLiberacaoView>();

            var SQL = new Select()
                 .Campos("pedidovenda.Id, pedidovenda.ClienteId,ROUND(SUM(itenspedidovenda.Preco * ItensLiberacaoPedidoVenda.QtdEmpenhada),2) as Total, SUM(ItensLiberacaoPedidoVenda.QtdEmpenhada) as QtdEmpenhada ")
                 .From("itensliberacaopedidovenda")
                 .InnerJoin("itenspedidovenda", "itenspedidovenda.Id = ItensLiberacaoPedidoVenda.ItemPedidoVendaId ")
                 .InnerJoin("pedidovenda", "pedidovenda.Id = itenspedidovenda.PedidoVendaId ")
                 .Where(" ItemPedidoVendaId  IN (SELECT ID FROM itenspedidovenda where pedidovendaid =  " + pedidoVendaId + " ) AND ItensLiberacaoPedidoVenda.QtdEmpenhada > 0 ")
                 .GroupBy("pedidovenda.Id ");
            var consulta = new ItemPedidoVendaValorLiberacaoView();
            cn.ExecuteToModel(ref consulta, SQL.ToString());

            if (consulta != null)
            {
                return consulta.Total;
            }
            else
            {
                return 0;               
            }
        }

        public IEnumerable<ItemLiberacaoPedidoVenda> GetItensLiberacao(int produtoId, int corId, int tamanhoId, int almoxarifadoId)
        {
            var SQL = new Select()
                .Campos("ipl.*")
                .From("itensliberacaopedidovenda ipl")
                .InnerJoin("itenspedidovenda ip", "ip.id = ipl.ItemPedidoVendaId")
                .InnerJoin("pedidovenda p", "p.id = ip.PedidoVendaId")
                .Where("ip.Produtoid = " + produtoId + " and ip.CorId = " + corId + " and ip.TamanhoId = " + tamanhoId + " and p.AlmoxarifadoId = " + almoxarifadoId + " and ipl.QtdNaoAtendida > 0;");

            var cn = new DapperConnection<ItemLiberacaoPedidoVenda>();
            var tm = new ItemLiberacaoPedidoVenda();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque(bool fichaTecnicaCompleta)
        {
            var SQL = new Select()
                .Campos("itensliberacaopedidovenda.Data as DataLiberacao,produtos.id as iditem,produtos.Referencia as RefProduto,produtos.Descricao as DescProduto,itenspedidovenda.ReferenciaPedidoCliente as ReferenciaPedidoCliente,itenspedidovenda.SeqPedCliente as SeqPedCliente, " +
                "  tamanhos.id as IdTamanho,tamanhos.Descricao as DescTamanho,cores.Id as IdCor,cores.Descricao as DescCor, itenspedidovenda.TipoMovimentacaoId as IdTipoMov," +
                " itensliberacaopedidovenda.Id as ItemPedidoVendaId, itensliberacaopedidovenda.AlmoxarifadoId as AlmoxarifadoId, IF(itensliberacaopedidovenda.SemEmpenho = 1, (itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada), itensliberacaopedidovenda.QtdNaoAtendida) as Qtd, " +
                " itensliberacaopedidovenda.Qtd - itensliberacaopedidovenda.QtdFaturada as QtdLiberada,  itenspedidovenda.Preco as Preco, itenspedidovenda.pedidovendaid, " +
                " pedidovenda.Referencia as ReferenciaPedidoVenda, itensliberacaopedidovenda.LiberacaoId as SeqLibPedVenda")
                .From("itenspedidovenda")
                .InnerJoin("itensliberacaopedidovenda", "itensliberacaopedidovenda.ItemPedidoVendaID = itenspedidovenda.Id")
                .InnerJoin("produtos", "produtos.id =  itenspedidovenda.ProdutoId")
                .LeftJoin("pedidovenda", "pedidovenda.id = itenspedidovenda.PedidoVendaId")
                .LeftJoin("cores", "cores.id = itenspedidovenda.CorId")
                .LeftJoin("produtodetalhes", "produtodetalhes.IdProduto = produtos.id AND produtodetalhes.idtamanho = itenspedidovenda.TamanhoId  AND produtodetalhes.idcor = itenspedidovenda.CorId")
                .LeftJoin("tamanhos", "tamanhos.id =  itenspedidovenda.TamanhoId")
                .Where("(itensliberacaopedidovenda.Status = " + (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial + " OR itensliberacaopedidovenda.Status = " + (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque
                + " OR ( itensliberacaopedidovenda.Status = " + (int)enumStatusLiberacaoPedidoVenda.Atendido + " AND itensliberacaopedidovenda.SemEmpenho = 1 ) ) " +
                " AND (itensliberacaopedidovenda.QtdNaoAtendida > 0 OR (itensliberacaopedidovenda.SemEmpenho = 1 AND itensliberacaopedidovenda.QtdEmpenhada = 0 ) ) " +
                " AND pedidovenda.Status <> 4 AND pedidovenda.QtdPedida = pedidovenda.QtdLiberada");

            SQL.Where(" (SELECT COUNT(*) FROM fichatecnica F WHERE F.ProdutoId = produtos.Id) > 0 ");

            if (fichaTecnicaCompleta)
            {
                SQL.Where(" (SELECT COUNT(*) FROM fichatecnicadomaterial F WHERE F.ProdutoId = produtos.Id) > 0");
            }


            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            var tm = new ItemLiberacaoPedidoVendaView();
            return cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

        public decimal GetQtdNaoAtendida(int itemId, int corId, int tamanhoId, List<int> almoxarifadoIds)
        {
            var cn = new DapperConnection<ItemPedidoVendaValorLiberacaoView>();

            var SQL = new Select()
                 .Campos("SUM(itensliberacaopedidovenda.QtdNaoAtendida) as Total ")
                 .From("itensliberacaopedidovenda")
                 .InnerJoin("itenspedidovenda", "itenspedidovenda.Id = ItensLiberacaoPedidoVenda.ItemPedidoVendaId ")
                 .Where(" QtdNaoAtendida > 0 AND itenspedidovenda.produtoid = " + itemId + " AND itenspedidovenda.tamanhoid = " + tamanhoId 
                 + " AND itenspedidovenda.corid = " + corId + " AND itensliberacaopedidovenda.almoxarifadoid in ( " + string.Join(", ", almoxarifadoIds) + ") ")
                 .GroupBy("produtoid, corid, tamanhoid ");

            var consulta = new ItemPedidoVendaValorLiberacaoView();
            cn.ExecuteToModel(ref consulta, SQL.ToString());

            if (consulta != null)
            {
                return consulta.Total;
            }
            else
            {
                return 0;
            }
        }
    }
}
