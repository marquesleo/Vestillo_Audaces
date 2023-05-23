using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;
using Vestillo.Lib;
using Vestillo.Business.Models.Views;
using Vestillo;

namespace Vestillo.Business.Repositories
{
    public class  PedidoVendaRepository: GenericRepository<PedidoVenda>
    {
        
        string modificaWhere = "";

        public PedidoVendaRepository()
            : base(new DapperConnection<PedidoVenda>())
        {
        }

        public PedidoVendaView GetByIdView(int id)
        {
            var cn = new DapperConnection<PedidoVendaView>();

            int OpcaoTabela = 0;

            if (VestilloSession.UserEscolheTabela)
            {
               
                var sql1 = "SELECT pedidovenda.OpcaoTabelaPreco from pedidovenda WHERE pedidovenda.id = " + id;

                PedidoVendaView retOpc = new PedidoVendaView();

                cn.ExecuteToModel(ref retOpc, sql1);

                if (retOpc != null)
                {
                    OpcaoTabela = retOpc.OpcaoTabelaPreco;
                }
            }
            

           




            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("cliente.razaosocial as RazaoColaborador,");            
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco");
            SQL.AppendLine("FROM 	pedidovenda p");


            if (VestilloSession.UserEscolheTabela)
            {
                if(OpcaoTabela == 1)
                {
                    SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id = p.TabelaPrecoId");
                }
                else
                {
                    SQL.AppendLine("LEFT JOIN tabelaprecopcp tp on tp.id = p.TabelaPrecoId");
                }

            }
            else
            {
                if (VestilloSession.UsaTabelaPCP)
                    SQL.AppendLine("LEFT JOIN tabelaprecopcp tp on tp.id = p.TabelaPrecoId");
                else
                    SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id = p.TabelaPrecoId");
            }


            

            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE p.Id = ");
            SQL.Append(id);
            
            PedidoVendaView ret = new PedidoVendaView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var itemPedidoVendaRepository = new ItemPedidoVendaRepository();
                ret.Itens = itemPedidoVendaRepository.GetByPedido(ret.Id).ToList();
            }

            return ret;
        }

        public PedidoVendaView GetByIdViewLiberacao(int id)
        {
            var cn = new DapperConnection<PedidoVendaView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE p.Id = ");
            SQL.Append(id);

            PedidoVendaView ret = new PedidoVendaView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var itemPedidoVendaRepository = new ItemPedidoVendaRepository();
                ret.Itens = itemPedidoVendaRepository.GetByPedidoLiberacao(ret.Id).ToList();
            }

            return ret;
        }

        public PedidoVendaView GetByIdViewAgrupado(int id)
        {
            var cn = new DapperConnection<PedidoVendaView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE p.Id = ");
            SQL.Append(id);

            PedidoVendaView ret = new PedidoVendaView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var itemPedidoVendaRepository = new ItemPedidoVendaRepository();
                ret.Itens = itemPedidoVendaRepository.GetByPedidoAgrupado(ret.Id).ToList();
            }

            return ret;
        }

        public PedidoVendaView GetByItemIdView(int id)
        {
            var cn = new DapperConnection<PedidoVendaView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("INNER JOIN itenspedidovenda pi ON p.id = pi.PedidoVendaId");
            SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ipl ON pi.id = ipl.ItemPedidoVendaId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE ipl.Id = ");
            SQL.Append(id);

            PedidoVendaView ret = new PedidoVendaView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var itemPedidoVendaRepository = new ItemPedidoVendaRepository();
                ret.Itens = itemPedidoVendaRepository.GetByPedido(ret.Id).ToList();
            }

            return ret;
        }

        public IEnumerable<PedidoVendaView> GetAllView()
        {
            var cn = new DapperConnection<PedidoVendaView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("cliente.razaosocial as RazaoColaborador,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "p"));
            SQL.AppendLine("ORDER BY p.Id DESC");

            return cn.ExecuteStringSqlToList(new PedidoVendaView(), SQL.ToString());
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacao(int StatusPedido = 0, int StatusPedido2 = 0)
        {
            var cn = new DapperConnection<PedidoVendaLiberacaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*, ");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("cliente.razaosocial as RazaoSocialCliente,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("e.descricao as DescEntrega,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco,");
            SQL.AppendLine("p.DataFaturamento as DataFaturamento,");
            //SQL.AppendLine("sum(ip.Qtd) as QtdPedida,");
            //SQL.AppendLine("sum(ilp.Qtd) - sum(ilp.QtdFaturada) as QtdLiberada,");
            //SQL.AppendLine("sum(ilp.QtdEmpenhada) as QtdEmpenhada,");
            //SQL.AppendLine("(sum(ilp.QtdEmpenhada)*100)/(sum(ip.Qtd)) as AtendidoTotal,");
            //SQL.AppendLine("(sum(ilp.QtdNaoAtendida)*100)/(sum(ip.Qtd)) as NaoAtendidoTotal,");
            //SQL.AppendLine("(SELECT sum(ilpv.QtdEmpenhada*ipv.Preco)FROM itenspedidovenda ipv ");
            //SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            //SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorEmpenhadoTotal, ");
            //SQL.AppendLine("(SELECT sum((ilpv.Qtd - ilpv.QtdFaturada)*ipv.Preco) FROM itenspedidovenda ipv ");
            //SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            //SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorLiberadoTotal ");

            //ALEX SQL.AppendLine("((p.QtdEmpenhada*100)/p.QtdPedida) as AtendidoTotal,");
            // ALEX SQL.AppendLine("((p.QtdLiberada - p.QtdEmpenhada)*100/p.QtdPedida) as NaoAtendidoTotal");

            SQL.AppendLine("ROUND(CAST(IFNULL(((p.QtdEmpenhada * 100) / p.QtdPedida), 0) as DECIMAL(10, 2)),2) as AtendidoTotal,");
            SQL.AppendLine("ROUND(CAST(IFNULL(((p.QtdLiberada - p.QtdEmpenhada) * 100 / p.QtdPedida), 0) as DECIMAL(10, 2)),2) as NaoAtendidoTotal ");

            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            //SQL.AppendLine("LEFT JOIN itenspedidovenda ip ON p.id = ip.PedidoVendaId");
            SQL.AppendLine("LEFT JOIN entrega e ON e.id = p.EntregaId");
            //SQL.AppendLine("LEFT JOIN itensliberacaopedidovenda ilp ON ip.id = ilp.ItemPedidoVendaId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE ");
            if (StatusPedido > 0 && StatusPedido2 > 0)
            {
                SQL.Append("  p.Status = " + StatusPedido + " OR  p.Status = " + StatusPedido2 + " AND  ");
            }
            else if  (StatusPedido > 0)
            {
                SQL.Append("  p.Status = " + StatusPedido + " AND  ");
            }

           
            
            SQL.Append(FiltroEmpresa("", "p"));            
            SQL.AppendLine(" GROUP BY p.Id ");
            SQL.AppendLine(" ORDER BY p.Id DESC ");

            return cn.ExecuteStringSqlToList(new PedidoVendaLiberacaoView(), SQL.ToString()).ToList();
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia()
        {
            var cn = new DapperConnection<PedidoVendaLiberacaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("cliente.razaosocial as RazaoSocialCliente,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("e.descricao as DescEntrega,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco,");
            //SQL.AppendLine("sum(ip.Qtd) as QtdPedida,");
            //SQL.AppendLine("sum(ilp.Qtd) - sum(ilp.QtdFaturada) as QtdLiberada,");
            //SQL.AppendLine("sum(ilp.QtdEmpenhada) as QtdEmpenhada,");
            //SQL.AppendLine("(sum(ilp.QtdEmpenhada)*100)/(sum(ip.Qtd)) as AtendidoTotal,");
            //SQL.AppendLine("(SELECT sum(ilpv.QtdEmpenhada*ipv.Preco)FROM itenspedidovenda ipv ");
            //SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            //SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorEmpenhadoTotal, ");
            //SQL.AppendLine("(SELECT sum((ilpv.Qtd - ilpv.QtdFaturada)*ipv.Preco) FROM itenspedidovenda ipv ");
            //SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            //SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorLiberadoTotal ");
            SQL.AppendLine("((p.QtdEmpenhada*100)/(p.Qtd)) as AtendidoTotal,");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("LEFT JOIN itenspedidovenda ip ON p.id = ip.PedidoVendaId");
            SQL.AppendLine("LEFT JOIN entrega e ON e.id = p.EntregaId");
            SQL.AppendLine("LEFT JOIN itensliberacaopedidovenda ilp ON ip.id = ilp.ItemPedidoVendaId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" (p.Status = 6 OR p.Status = 8 OR p.Status = 7 OR p.Status = 2) AND ");
            SQL.Append(FiltroEmpresa("", "p"));
            SQL.AppendLine("GROUP BY p.Id");
            SQL.AppendLine("ORDER BY p.Id DESC");

            return cn.ExecuteStringSqlToList(new PedidoVendaLiberacaoView(), SQL.ToString()).ToList();
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia(int codigo)
        {
            var cn = new DapperConnection<PedidoVendaLiberacaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("cliente.razaosocial as RazaoSocialCliente,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("e.descricao as DescEntrega,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco,");
            SQL.AppendLine("sum(ip.Qtd) as QtdPedida,");
            SQL.AppendLine("sum(ilp.Qtd) - sum(ilp.QtdFaturada) as QtdLiberada,");
            SQL.AppendLine("sum(ilp.QtdEmpenhada) as QtdEmpenhada,");
            SQL.AppendLine("(sum(ilp.QtdEmpenhada)*100)/(sum(ip.Qtd)) as AtendidoTotal,");
            SQL.AppendLine("(SELECT sum(ilpv.QtdEmpenhada*ipv.Preco)FROM itenspedidovenda ipv ");
            SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorEmpenhadoTotal, ");
            SQL.AppendLine("(SELECT sum((ilpv.Qtd - ilpv.QtdFaturada)*ipv.Preco) FROM itenspedidovenda ipv ");
            SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorLiberadoTotal ");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("LEFT JOIN itenspedidovenda ip ON p.id = ip.PedidoVendaId");
            SQL.AppendLine("LEFT JOIN entrega e ON e.id = p.EntregaId");
            SQL.AppendLine("LEFT JOIN itensliberacaopedidovenda ilp ON ip.id = ilp.ItemPedidoVendaId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE ");
            SQL.AppendLine(" (p.Status = 6 OR p.Status = 8 OR p.Status = 7 OR p.Status = 2 OR p.Status = 9  OR p.Status = 11) AND ");
            SQL.Append(FiltroEmpresa("", "p"));
            SQL.AppendLine("AND p.id = " + codigo);
            SQL.AppendLine("GROUP BY p.Id");

            return cn.ExecuteStringSqlToList(new PedidoVendaLiberacaoView(), SQL.ToString()).ToList();
        }

        public PedidoVenda GetByIdAtualizacao(int codigo)
        {
            var cn = new DapperConnection<PedidoVenda>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*,");
            SQL.AppendLine("sum(ip.Qtd) as QtdPedida,");
            SQL.AppendLine("sum(ilp.Qtd) - sum(ilp.QtdFaturada) as QtdLiberada,");
            SQL.AppendLine("sum(ilp.QtdEmpenhada) as QtdEmpenhada,");
            SQL.AppendLine("(sum(ilp.QtdEmpenhada)*100)/(sum(ip.Qtd)) as AtendidoTotal,");
            SQL.AppendLine("(((sum(ilp.Qtd) - sum(ilp.QtdFaturada)) - sum(ilp.QtdEmpenhada))*100)/(sum(ip.Qtd)) as NaoAtendidoTotal,");
            SQL.AppendLine("(SELECT sum(ilpv.QtdEmpenhada*ipv.Preco)FROM itenspedidovenda ipv ");
            SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorEmpenhadoTotal, ");
            SQL.AppendLine("(SELECT sum((ilpv.Qtd - ilpv.QtdFaturada)*ipv.Preco) FROM itenspedidovenda ipv ");
            SQL.AppendLine("INNER JOIN itensliberacaopedidovenda ilpv ON ipv.id = ilpv.ItemPedidoVendaId  ");
            SQL.AppendLine("WHERE p.id = ipv.PedidoVendaId) as ValorLiberadoTotal ");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("LEFT JOIN itenspedidovenda ip ON p.id = ip.PedidoVendaId");
            SQL.AppendLine("LEFT JOIN itensliberacaopedidovenda ilp ON ip.id = ilp.ItemPedidoVendaId");
            SQL.AppendLine("WHERE p.id = " + codigo);
            SQL.AppendLine("GROUP BY p.Id");

            PedidoVenda pedido = new PedidoVenda();
            cn.ExecuteToModel(ref pedido, SQL.ToString());
            return pedido;
        }

        public IEnumerable<PedidoVendaView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " (n.Status = 2 OR n.Status = 6 OR n.Status = 7 OR n.Status = 8)  AND "; // Pedido Aberto ou faturado parcial
            }

            if (parametrosDaBusca == "LIBERADO")
            {              

                if (VestilloSession.ControleDeEstoqueAtivo == VestilloSession.ControleEstoque.SIM)
                {
                    modificaWhere = " (n.Status = 6 || n.Status = 7) AND ";

                    modificaWhere += "(SELECT sum(Qtd) FROM itenspedidovenda i where i.PedidoVendaId  = n.ID) = " +
                        "(SELECT sum(il.QtdEmpenhada) FROM itensliberacaopedidovenda il inner join itenspedidovenda i on i.id = il.itempedidovendaid where i.PedidoVendaId  = n.ID)  AND "; 
                }
                else
                {
                    modificaWhere = " (n.Status = 6) AND ";

                    modificaWhere += "(SELECT sum(Qtd) FROM itenspedidovenda i where i.PedidoVendaId  = n.ID) = " + 
                        "(SELECT sum(il.Qtd) FROM itensliberacaopedidovenda il inner join itenspedidovenda i on i.id = il.itempedidovendaid where i.PedidoVendaId  = n.ID)  AND "; 
                }
            }

            var cn = new DapperConnection<PedidoVendaView>();
            var p = new PedidoVendaView();
   

            var SQL = new Select()
                .Campos("n.id as id,n.referencia as Referencia, cli.nome as NomeCliente,cli.razaosocial as RazaoColaborador, n.status, n.Obs ")
                .From("pedidovenda n")
                .LeftJoin("colaboradores cli", "cli.id = n.ClienteId")
                .Where(modificaWhere + "n.referencia like '%" + referencia + "%' And  " + FiltroEmpresa("n.EmpresaId"));

            if (parametrosDaBusca == "SOMENTE PARA FATURAR" && VestilloSession.UsaConferencia)
            {
                SQL.Where("(n.Conferencia = 1 OR (n.Conferencia = 0 AND n.Status = 2) )"); //Atendido Parcialmente não entra em Conferência
            }

            var ret = cn.ExecuteStringSqlToList(p, SQL.ToString());

            if (parametrosDaBusca == "SOMENTE PARA FATURAR" && VestilloSession.UsaConferencia)
            {
                List<PedidoVendaView> pedidos = new List<PedidoVendaView>();
                foreach (PedidoVendaView pv in ret)
                {
                    var itemLiberacaoPedidoVendaRepository = new ItemLiberacaoPedidoVendaRepository();
                    pv.LiberacaoPedidoVenda = itemLiberacaoPedidoVendaRepository.GetLiberacaoPedidoVenda(pv.Id).ToList();
                    foreach (LiberacaoPedidoVenda lpv in pv.LiberacaoPedidoVenda)
                    {
                        lpv.ItensLiberacao = itemLiberacaoPedidoVendaRepository.GetItensLiberacaoViewByLiberacaoIdEPedido(lpv.LiberacaoId, pv.Id).ToList();
                        if (lpv.ItensLiberacao.Exists(i => i.QtdConferida > 0 ))
                        {
                            if (!pedidos.Exists(pe => pe.Id == pv.Id))
                                pedidos.Add(pv);
                        }                        
                    }
                    
                }


                return pedidos;
            }

            return ret;
        }

        public IEnumerable<PedidoVenda> GetByRef(string referencia)
        {
            var cn = new DapperConnection<PedidoVendaView>();
            var p = new PedidoVendaView();


            var SQL = new Select()
                .Campos(" n.* ")
                .From("pedidovenda n")
                .Where("n.referencia = '" + referencia.Trim() + "' ");            

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public IEnumerable<PedidoVendaView> GetListPorCliente(string Cliente, string parametrosDaBusca)
        {
            if (parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                modificaWhere = " (n.Status = 2 OR n.Status = 6 OR n.Status = 7 OR n.Status = 8)  AND "; // Pedido Aberto ou faturado parcial
            }

            if (parametrosDaBusca == "IMPRIMIR CONFERENCIA")
            {
                modificaWhere = " (n.Status = 7 OR n.Status = 8)  AND ";
            }

            if (parametrosDaBusca == "LIBERADO")
            {
                if (VestilloSession.ControleDeEstoqueAtivo == VestilloSession.ControleEstoque.SIM)
                {
                    modificaWhere = " (n.Status = 6 || n.Status = 7) AND ";

                    modificaWhere += "(SELECT sum(Qtd) FROM itenspedidovenda i where i.PedidoVendaId  = n.ID) = " +
                        "(SELECT sum(il.QtdEmpenhada) FROM itensliberacaopedidovenda il inner join itenspedidovenda i on i.id = il.itempedidovendaid where i.PedidoVendaId  = n.ID)  AND ";
                }
                else
                {
                    modificaWhere = " (n.Status = 6) AND ";

                    modificaWhere += "(SELECT sum(Qtd) FROM itenspedidovenda i where i.PedidoVendaId  = n.ID) = " +
                        "(SELECT sum(il.Qtd) FROM itensliberacaopedidovenda il inner join itenspedidovenda i on i.id = il.itempedidovendaid where i.PedidoVendaId  = n.ID)  AND ";
                }

                //modificaWhere = " (n.Status = 6) AND (SELECT sum(QTD) FROM itenspedidovenda i where i.PedidoVendaId  = n.ID) = (SELECT sum(il.QTD) FROM itensliberacaopedidovenda il inner join itenspedidovenda i on i.id = il.itempedidovendaid where i.PedidoVendaId  = n.ID)  AND "; // Pedido Aberto ou faturado parcial
            }

            var cn = new DapperConnection<PedidoVendaView>();
            var p = new PedidoVendaView();


           
            var SQL = new Select()
                .Campos("n.id as id,n.referencia as Referencia, cli.nome as NomeCliente,cli.razaosocial as RazaoColaborador, n.status, n.Obs ")
                .From("pedidovenda n")
                .InnerJoin("colaboradores cli", "cli.id = n.ClienteId")
                .Where(modificaWhere + "cli.nome like '%" + Cliente + "%' And  " + FiltroEmpresa("n.EmpresaId"));
            
            if (VestilloSession.UsaConferencia && parametrosDaBusca != "LIBERADO")
            {
               SQL.Where("(n.Conferencia = 1 OR (n.Conferencia = 0 AND n.Status = 2) )"); //Atendido Parcialmente não entra em Conferência
            }

            var ret = cn.ExecuteStringSqlToList(p, SQL.ToString());

            if (VestilloSession.UsaConferencia && parametrosDaBusca == "SOMENTE PARA FATURAR")
            {
                List<PedidoVendaView> pedidos = new List<PedidoVendaView>();
                foreach (PedidoVendaView pv in ret)
                {
                    var itemLiberacaoPedidoVendaRepository = new ItemLiberacaoPedidoVendaRepository();
                    pv.LiberacaoPedidoVenda = itemLiberacaoPedidoVendaRepository.GetLiberacaoPedidoVenda(pv.Id).ToList();
                    foreach (LiberacaoPedidoVenda lpv in pv.LiberacaoPedidoVenda)
                    {
                        lpv.ItensLiberacao = itemLiberacaoPedidoVendaRepository.GetItensLiberacaoViewByLiberacaoIdEPedido(lpv.LiberacaoId, pv.Id).ToList();
                        if (lpv.ItensLiberacao.Exists(i => i.QtdConferida > 0))
                        {
                            if (!pedidos.Exists(pe => pe.Id == pv.Id))
                                pedidos.Add(pv);
                        }
                    }
                    
                }


                return pedidos;
            }

            return ret;
        }

        public void UpdateStatus(int pedidoVendaId, enumStatusPedidoVenda status)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE pedidovenda SET ");
            SQL.AppendLine("Status = ");
            SQL.Append((int)status);
            SQL.AppendLine(", Conferencia = ");
            SQL.Append(false);
            SQL.AppendLine(", Impresso = ");
            SQL.Append("'N'");
            if (status == enumStatusPedidoVenda.Faturado_Parcial || status == enumStatusPedidoVenda.Faturado_Total)
                SQL.AppendLine(", DataFaturamento ='" + DateTime.Today.ToString("yyyy-MM-dd") + "'");
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(pedidoVendaId);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public List<PedidoVendaLiberacaoView> GetPedidoByItem(string referencia)
        {
            var cn = new DapperConnection<PedidoVendaLiberacaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	p.*, ");
            SQL.AppendLine("cliente.referencia as RefCliente,");
            SQL.AppendLine("cliente.nome as NomeCliente,");
            SQL.AppendLine("cliente.razaosocial as RazaoSocialCliente,");
            SQL.AppendLine("vendedor.referencia as RefVendedor,");
            SQL.AppendLine("vendedor.nome as NomeVendedor,");
            SQL.AppendLine("vendedor2.referencia as RefVendedor2,");
            SQL.AppendLine("vendedor2.nome as NomeVendedor2,");
            SQL.AppendLine("transportadora.referencia as RefTransportadora,");
            SQL.AppendLine("transportadora.nome as NomeTransportadora,");
            SQL.AppendLine("e.descricao as DescEntrega,");
            SQL.AppendLine("r.referencia as RefRotaVisita,");
            SQL.AppendLine("r.descricao as DescricaoRotaVisita,");
            SQL.AppendLine("tp.referencia as RefTabelaPreco,");
            SQL.AppendLine("tp.descricao as DescricaoTabelaPreco,");
            SQL.AppendLine("p.DataFaturamento as DataFaturamento,");
            SQL.AppendLine("((p.QtdEmpenhada*100)/p.QtdPedida) as AtendidoTotal,");
            SQL.AppendLine("((p.QtdLiberada - p.QtdEmpenhada)*100/p.QtdPedida) as NaoAtendidoTotal");
            SQL.AppendLine("FROM 	pedidovenda p");
            SQL.AppendLine("INNER JOIN itenspedidovenda ip ON p.id = ip.PedidoVendaId");
            SQL.AppendLine("INNER JOIN produtos pd ON pd.id = ip.ProdutoId");
            SQL.AppendLine("LEFT JOIN tabelaspreco tp on tp.id	= p.TabelaPrecoId");
            SQL.AppendLine("INNER JOIN colaboradores cliente ON cliente.id = p.ClienteId");
            SQL.AppendLine("LEFT JOIN entrega e ON e.id = p.EntregaId");            
            SQL.AppendLine("LEFT JOIN colaboradores vendedor ON vendedor.id = p.VendedorId");
            SQL.AppendLine("LEFT JOIN colaboradores vendedor2 ON vendedor2.id = p.VendedorId2");
            SQL.AppendLine("LEFT JOIN colaboradores transportadora ON transportadora.id = p.TransportadoraId");
            SQL.AppendLine("LEFT JOIN rotavisitas r ON r.id = p.RotaId");
            SQL.AppendLine("WHERE ");
            if(!string.IsNullOrEmpty(referencia))
                SQL.Append("  pd.Referencia like '%" + referencia + "%' AND  ");           
            SQL.Append(FiltroEmpresa("", "p"));
            SQL.AppendLine(" GROUP BY p.Id ");
            SQL.AppendLine(" ORDER BY p.Id DESC ");

            return cn.ExecuteStringSqlToList(new PedidoVendaLiberacaoView(), SQL.ToString()).ToList();
        }

        public void AlteraEmpresaPedido(int IdPedido,int IdEmpresa,string Usuario,string Data,string Observacao)
        {
            string Sql = String.Empty;
            string ObservacaoPed = "Usuario: " +   Usuario  + " - Data do Evento: " + Data + " - " + Observacao;

           Sql = "UPDATE pedidovenda set pedidovenda.EmpresaId = " + IdEmpresa + ",pedidovenda.ObservacaoTransf = " + "'" + ObservacaoPed + "'" +  " WHERE pedidovenda.Id = " + IdPedido;
            _cn.ExecuteNonQuery(Sql);

        }

        public void AlteraQtdItensPedido(int IdPedido,decimal QtdItens)
        {
            string Sql = String.Empty;
           

            Sql = "UPDATE pedidovenda set pedidovenda.QtdPedida = " + QtdItens.ToString().Replace(",",".") + "  WHERE pedidovenda.Id = " + IdPedido;
            _cn.ExecuteNonQuery(Sql);

        }


        public IEnumerable<PedidoMatriz> GetByRelPedidoMatrizSoTamanhos(int IdPedidoVenda)
        {
            var cn = new DapperConnection<PedidoMatriz>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	t.id as IdTamanho,t.Abreviatura as Tamanho ");
            SQL.AppendLine("FROM 	pedidovenda pv");
            SQL.AppendLine("INNER JOIN itenspedidovenda ipv ON ipv.PedidoVendaId = pv.id");
            SQL.AppendLine("INNER JOIN produtos p ON ipv.ProdutoId = p.id");
            SQL.AppendLine("INNER JOIN cores c ON ipv.CorId = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON ipv.TamanhoId = t.id");
            SQL.AppendLine("WHERE  ");
            SQL.Append(FiltroEmpresa("", "pv"));
            SQL.AppendLine(" AND pv.id =  " + IdPedidoVenda);
            SQL.AppendLine("  GROUP By ipv.TamanhoId ");



            return cn.ExecuteStringSqlToList(new PedidoMatriz(), SQL.ToString());
        }

        public IEnumerable<PedidoMatriz> GetByPedidoMatizCor(int IdPedidoVenda)
        {
            var cn = new DapperConnection<PedidoMatriz>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	pv.*,");
            SQL.AppendLine("pv.id as IdPedido, pv.referencia as RefPedido, CONCAT(cob.referencia,'-',cob.nome) as Cliente ,cob.Id as IdCliente,");
            SQL.AppendLine("p.id as IdItem,p.referencia as RefItem,p.descricao as DescricaoItem, c.Descricao as cor,c.Id as IdCor ");
            SQL.AppendLine("FROM 	pedidovenda pv");
            SQL.AppendLine("INNER JOIN itenspedidovenda ipv ON ipv.PedidoVendaId = pv.id");
            SQL.AppendLine("INNER JOIN produtos p ON ipv.ProdutoId = p.id");
            SQL.AppendLine("INNER JOIN cores c ON ipv.CorId = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON ipv.TamanhoId = t.id");
            SQL.AppendLine("LEFT JOIN colaboradores cob ON cob.id = pv.ClienteId");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "pv"));
            SQL.AppendLine(" AND pv.id = " + IdPedidoVenda);
            SQL.AppendLine("  GROUP By p.id,ipv.CorId ");
            SQL.AppendLine(" ORDER BY pv.Referencia ");


            return cn.ExecuteStringSqlToList(new PedidoMatriz(), SQL.ToString());
        }

        public IEnumerable<PedidoMatriz> GetByPedidoVendaMatrizCorTamanho(int IdPedido, int IdItem, int IdCor)
        {
            var cn = new DapperConnection<PedidoMatriz>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	t.id as IdTamanho,t.Abreviatura as Tamanho,ipv.CorId, SUM(ipv.Qtd) as Quantidade ");
            SQL.AppendLine("FROM 	pedidovenda pv");
            SQL.AppendLine("INNER JOIN itenspedidovenda ipv ON ipv.PedidoVendaId = pv.id");
            SQL.AppendLine("INNER JOIN produtos p ON ipv.ProdutoId = p.id");
            SQL.AppendLine("INNER JOIN cores c ON ipv.CorId = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON ipv.TamanhoId = t.id");
            SQL.AppendLine("WHERE ipv.CorId = " + IdCor + " AND ");
            SQL.AppendLine(" ipv.ProdutoId = " + IdItem + " AND ");
            SQL.Append(FiltroEmpresa("", "pv"));
            SQL.AppendLine(" AND pv.id = " + IdPedido);
            SQL.AppendLine("  GROUP By ipv.TamanhoId,ipv.CorId ");
            SQL.AppendLine(" ORDER BY pv.Referencia ");


            return cn.ExecuteStringSqlToList(new PedidoMatriz(), SQL.ToString());
        }

       

        /*
        public bool PedidoEmProducao(int IdPedido)
        {
            var cn = new DapperConnection<PedidoVendaView>();
            bool EmProducao = false;
            string SQL = String.Empty;
            PedidoVendaView ret = new PedidoVendaView();

            SQL = "SELECT PossuiProducao FROM  pedidovenda where id = " + IdPedido;

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                if(ret.PossuiProducao == 1 )
                {
                    EmProducao = true;
                }
            }

            return EmProducao;

        }

        public void UpdatePedidoEmProducao(int IdPedido, bool EmProducao)
        {
            int Producao = 0;
            string SQL = String.Empty;

            if(EmProducao)
            {
                Producao = 1;
            }

            try
            {
                SQL = " UPDATE pedidovenda set PossuiProducao = " + Producao + " WHERE pedidovenda.id = " + IdPedido;
                _cn.ExecuteNonQuery(SQL);
            }
            catch(VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }

        }

        public void DefineConferenciaComProducao(int IdPedidoVenda, bool Confere)
        {
            var cn = new DapperConnection<ItemLiberacaoPedidoVendaView>();
            bool Conferido = false;
            decimal QtdConferencia = 0;
            decimal QtdConferido = 0;


            string SQL = String.Empty;
            

            var ret = new ItemLiberacaoPedidoVendaView();


            SQL = "select * from itensliberacaopedidovenda where itensliberacaopedidovenda.ItemPedidoVendaID in(select id from itenspedidovenda where itenspedidovenda.PedidoVendaId = " + IdPedidoVenda + ")";


            
            if (item.Qtd == item.QtdEmpenhada)
            {
                item.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
            }
            else if (item.QtdEmpenhada > 0 && item.QtdNaoAtendida > 0)
            {
                item.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
            }
            else if (item.QtdEmpenhada <= 0 && item.QtdNaoAtendida > 0)
            {
                item.Status = (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque;
            }
            


            var dados = cn.ExecuteStringSqlToList(ret, SQL.ToString());

            if (dados != null)
            {

                foreach (var item in dados)
                {
                    if (item.QtdFaturada == 0 && item.QtdEmpenhada > 0)
                    {
                        QtdConferencia = item.QtdEmpenhada;
                        QtdConferido = item.QtdEmpenhada;
                        Conferido = true;
                    }
                    else if(item.QtdFaturada > 0 && item.QtdEmpenhada > 0)
                    {
                        if(item.Qtd > item.QtdFaturada)
                        {

                        }
                    }
                } 

               
                

            }
        }
        */

    }
}
