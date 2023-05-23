using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;
using Vestillo.Connection;
using System.Data;

namespace Vestillo.Core.Repositories
{
    public class PedidoXOrdemRepository : GenericRepository<PedidoXOrdemView>
    {
        public class OrdensGet
        {
            public int StatusOp { get; set; }
            public int Produto { get; set; }
            public int Cor { get; set; }
            public int Tamanho { get; set; }
            public decimal Quantidade { get; set; }
            public decimal QuantidadeProduzida { get; set; }
            public decimal Disponivel { get; set; }
            public DateTime DataPrevisao { get; set; }
            public string RefOrdem { get; set; }
            public int IdOrdem { get; set; }
            public string ObsOrdem { get; set; }
        }

       

        public IEnumerable<PedidoXOrdemView> GetDadosDaProducao(bool TestaAlmoxarifado)
        {
            //string[] mesAnoSQL = mesAno.Select(x => string.Concat("'", x.ToString("MM-yyyy"), "'")).ToArray();

            StringBuilder sqlCorte = new StringBuilder();
            StringBuilder sqlCurvaAbc = new StringBuilder();

            decimal ValorCorte = 0;
            var cnCorte = new DapperConnection<PedidoXOrdemView>();

            List<PedidoXOrdemView> MontaLista = null;
            MontaLista = new List<PedidoXOrdemView>();
            MontaLista.Clear();

            StringBuilder sql = new StringBuilder();



            sql.AppendLine(" select IFNULL(ordemproducao.Observacao,'') as ObsOrdem, ordemproducao.Status as StatusOp,ordemproducao.id as IdOrdem,ordemproducao.Referencia as RefOrdem, itensordemproducao.ProdutoId as Produto,itensordemproducao.DataPrevisao, ");
            sql.AppendLine("itensordemproducao.CorId as Cor,itensordemproducao.TamanhoId as Tamanho, ");
            sql.AppendLine("itensordemproducao.Quantidade,itensordemproducao.QuantidadeProduzida, ");
            sql.AppendLine("(itensordemproducao.Quantidade - itensordemproducao.QuantidadeProduzida) as Disponivel ");
            sql.AppendLine(" from itensordemproducao ");
            sql.AppendLine("INNER JOIN ordemproducao ON ordemproducao.id = itensordemproducao.OrdemProducaoId ");
            sql.AppendLine("WHERE(ordemproducao.Status <> 5 AND ordemproducao.Status <> 6) AND ");
            if(TestaAlmoxarifado)
            {
                sql.AppendLine(" ordemproducao.almoxarifadoid = 3 AND ");
            }
            sql.AppendLine("itensordemproducao.QuantidadeProduzida < itensordemproducao.Quantidade AND  NOT ISNULL(DataPrevisao) ");
            sql.AppendLine(" order by DataPrevisao ");

            var LisOrdens = VestilloConnection.ExecSQLToList<OrdensGet>(sql.ToString());

            //List<DataRow> LisOrdens = dtOrdens.AsEnumerable().ToList();
            //EnumerableRowCollection<DataRow> LisOrdens = dtOrdens.AsEnumerable();

           
            foreach (var item in LisOrdens)
            {

            }
          

            sql = new StringBuilder();

            sql.AppendLine(" select pedidovenda.id as IdPedido,clientes.RazaoSocial as NomeCliente, catalogo.descricao as Catalogo, colecoes.descricao as Colecao,itenspedidovenda.preco as Preco, pedidovenda.referencia as RefPedido,itensliberacaopedidovenda.Data as Liberacao, ");
            sql.AppendLine(" itenspedidovenda.ProdutoId as ProdutoId,CONCAT(Produtos.Referencia,'-',Produtos.Descricao) as RefProduto, ");
            sql.AppendLine(" itenspedidovenda.CorId as CorId,Cores.Abreviatura as RefCor, itenspedidovenda.TamanhoId as TamanhoId, ");
            sql.AppendLine(" tamanhos.Abreviatura as RefTamanho,itensliberacaopedidovenda.Qtd as QtdPedido,colaboradores.nome as Vendedor, ");
            sql.AppendLine(" itensliberacaopedidovenda.Qtd - (itensliberacaopedidovenda.QtdFaturada + itensliberacaopedidovenda.QtdEmpenhada) as QtdParaAtender, ");
            sql.AppendLine(" itensliberacaopedidovenda.QtdFaturada,itensliberacaopedidovenda.QtdEmpenhada,pedidovenda.Status as StatusPedido,pedidovenda.Obs as ObsPedido ");
            sql.AppendLine(" from  itensliberacaopedidovenda ");
            sql.AppendLine(" INNER JOIN itenspedidovenda ON itenspedidovenda.Id = itensliberacaopedidovenda.ItemPedidoVendaID ");
            sql.AppendLine(" INNER JOIN pedidovenda ON pedidovenda.id = itenspedidovenda.PedidoVendaId ");
            sql.AppendLine(" INNER JOIN produtos ON produtos.Id = itenspedidovenda.ProdutoId");
            sql.AppendLine(" INNER JOIN cores ON cores.Id = itenspedidovenda.CorId ");
            sql.AppendLine(" INNER JOIN tamanhos ON tamanhos.Id = itenspedidovenda.TamanhoId ");
            sql.AppendLine(" INNER JOIN colaboradores clientes ON clientes.Id = pedidovenda.ClienteId ");
            sql.AppendLine(" LEFT JOIN colaboradores ON colaboradores.id = pedidovenda.VendedorId ");
            sql.AppendLine(" LEFT JOIN catalogo ON catalogo.id = produtos.IdCatalogo ");
            sql.AppendLine(" LEFT JOIN colecoes ON colecoes.id = produtos.Idcolecao ");
            sql.AppendLine(" Where itensliberacaopedidovenda.Qtd > (itensliberacaopedidovenda.QtdFaturada + itensliberacaopedidovenda.QtdEmpenhada) ");
            sql.AppendLine(" AND pedidovenda.EmpresaId = 1 AND(pedidovenda.Status <> 3 AND pedidovenda.Status <> 4 AND pedidovenda.Status <> 5) ");
            sql.AppendLine(" order by itensliberacaopedidovenda.Data");

            DataTable dtPedidos = VestilloConnection.ExecToDataTable(sql.ToString());

            //List<DataRow> LisPedidos = dtPedidos.Rows.Cast<DataRow>().ToList();
            EnumerableRowCollection<DataRow> LisPedidos = dtPedidos.AsEnumerable();

            PedidoXOrdemView it = new PedidoXOrdemView(); 

            foreach (var itemPedido in LisPedidos)
            {
                int Produto = 0;
                int IdPedido = 0;
                int Cor = 0;
                int Tamanho = 0;
                decimal QtdParaAtender = 0;
                decimal QtdAtenderOficial = 0;
                DateTime DataLiberacao = new DateTime();
                bool PedidoAtendido = false;
                bool BuscaMaisOp = true;
                string RefCor = string.Empty;
                string RefPedido = string.Empty;
                string RefProduto = string.Empty;
                string RefTamanho = string.Empty;
                string Vendedor = string.Empty;
                string NomeCliente = string.Empty;
                string Catalogo = string.Empty;
                string Colecao = string.Empty;
                string ObsPedido = string.Empty;
                decimal preco = 0;



                IdPedido =  itemPedido.Field<int>("IdPedido");
                DataLiberacao = itemPedido.Field<DateTime>("Liberacao");
                Produto = itemPedido.Field<int>("ProdutoId");
                Cor = itemPedido.Field<int>("CorId");
                Tamanho = itemPedido.Field<int>("TamanhoId");
                QtdParaAtender =  itemPedido.Field<decimal>("QtdParaAtender");
                QtdAtenderOficial = itemPedido.Field<decimal>("QtdParaAtender");
                RefCor = itemPedido.Field<string>("RefCor");
                RefPedido = itemPedido.Field<string>("RefPedido");
                RefProduto = itemPedido.Field<string>("RefProduto");
                RefTamanho = itemPedido.Field<string>("RefTamanho");
                if(!String.IsNullOrEmpty(itemPedido.Field<string>("Vendedor")))
                {
                    Vendedor = itemPedido.Field<string>("Vendedor");
                }
                NomeCliente = itemPedido.Field<string>("NomeCliente");
                Catalogo = itemPedido.Field<string>("Catalogo");
                Colecao = itemPedido.Field<string>("Colecao");
                ObsPedido = itemPedido.Field<string>("ObsPedido");
                preco = itemPedido.Field<decimal>("Preco");

                //&& x.Field<int>("Cor") == Cor && x.Field<int>("Tamanho") == Tamanho).OrderBy(x=> x.Field<int>("DataPrevisao")

                while (BuscaMaisOp == true && PedidoAtendido == false)
                {
                    OrdensGet Result = new OrdensGet();

                    Result = LisOrdens.Where(x => x.Produto == Produto && x.Cor == Cor && x.Tamanho == Tamanho && x.Disponivel > 0).OrderBy(x => x.DataPrevisao).FirstOrDefault();
                    if (Result != null)
                    {
                        it = new PedidoXOrdemView();
                        it.CorId = Result.Cor;
                        it.DataQueIraAtender = Result.DataPrevisao;
                        it.IdPedido = IdPedido;
                        it.IdOrdem = Result.IdOrdem;
                        it.Vendedor = Vendedor;
                        it.Liberacao = DataLiberacao;
                        it.ProdutoId = Produto;
                        it.QtdParaAtender = QtdAtenderOficial;
                        if (Result.Disponivel > QtdParaAtender)
                        {                           
                            it.QtdQueSeraAtendida = QtdParaAtender;
                            it.Atendido = "Sim";
                            PedidoAtendido = true;
                            BuscaMaisOp = false;
                            Result.Disponivel = Result.Disponivel - QtdParaAtender;
                        }
                        else if (Result.Disponivel <= QtdParaAtender)
                        {
                            it.Atendido = "Sim";
                            it.QtdQueSeraAtendida = Result.Disponivel;
                            QtdParaAtender -= it.QtdQueSeraAtendida;
                            if(QtdParaAtender > 0)
                            {
                                BuscaMaisOp = true;
                                PedidoAtendido = false;
                            }
                            else
                            {
                                BuscaMaisOp = false;
                                PedidoAtendido = true;
                            }
                            
                            
                            Result.Disponivel = 0;
                        }
                        it.RefCor = RefCor;
                        it.RefDaOrdem = Result.RefOrdem;
                        it.RefPedido = RefPedido;
                        it.RefProduto = RefProduto;
                        it.RefTamanho = RefTamanho;
                        it.TamanhoId = Tamanho;
                        it.NomeCliente = NomeCliente;
                        it.Catalogo = Catalogo;
                        it.Colecao = Colecao;
                        it.ObsPedido = ObsPedido;
                        it.ObsOrdem = Result.ObsOrdem;
                        it.Preco = preco;
                        MontaLista.Add(it);

                    }
                    else if((BuscaMaisOp == true && PedidoAtendido == false) && QtdAtenderOficial != QtdParaAtender)
                    {
                        BuscaMaisOp = false;
                        PedidoAtendido = false;
                    }
                    else
                    {
                        it = new PedidoXOrdemView();
                        it.CorId = Cor;
                        it.Atendido = "Não";
                        it.DataQueIraAtender = null;
                        it.IdPedido = IdPedido;
                        it.IdOrdem = null;
                        it.Vendedor = Vendedor;
                        it.Liberacao = DataLiberacao;
                        it.ProdutoId = Produto;
                        it.QtdParaAtender = QtdAtenderOficial;
                        it.QtdQueSeraAtendida = 0;
                        it.RefCor = RefCor;
                        it.RefDaOrdem = null;
                        it.RefPedido = RefPedido;
                        it.RefProduto = RefProduto;
                        it.RefTamanho = RefTamanho;
                        it.TamanhoId = Tamanho;
                        BuscaMaisOp = false;
                        PedidoAtendido = false;
                        it.NomeCliente = NomeCliente;
                        it.Catalogo = Catalogo;
                        it.Colecao = Colecao;
                        it.ObsPedido = ObsPedido;                        
                        it.Preco = preco;
                        MontaLista.Add(it);
                    }
                   
                }






            }

            return MontaLista;
        }
    }
}
