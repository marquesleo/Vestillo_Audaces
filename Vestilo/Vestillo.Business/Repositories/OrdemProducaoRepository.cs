using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class OrdemProducaoRepository : GenericRepository<OrdemProducao>
    {
        public OrdemProducaoRepository()
            : base(new DapperConnection<OrdemProducao>())
        {
        }

        public OrdemProducaoView GetByIdView(int id)
        {
            var cn = new DapperConnection<OrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("a.descricao as DescricaoAlmoxarifado, ");
            SQL.AppendLine("c.nome as colaborador ");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN almoxarifados a ON a.id = op.AlmoxarifadoId");
            SQL.AppendLine("LEFT JOIN colaboradores c ON c.id = op.IdColaborador");
            SQL.Append(" WHERE " + FiltroEmpresa("", "op"));
            SQL.AppendLine(" AND op.Id = " + id);

            OrdemProducaoView ret = new OrdemProducaoView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
                ret.Itens = itemOrdemProducaoRepository.GetByPedido(ret.Id).ToList();
            }

            return ret;
        }

        public IEnumerable<OrdemProducaoView> GetByRefView(string referencia, int IdColecao = 0)
        {
            var cn = new DapperConnection<OrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("a.descricao as DescricaoAlmoxarifado");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN almoxarifados a ON a.id = op.AlmoxarifadoId");
            SQL.AppendLine("WHERE op.referencia like '%" + referencia + "%'");

            if(IdColecao > 0)
            {
                SQL.AppendLine(" AND IdColecao = " + IdColecao );
            }
            

            SQL.Append(" AND " + FiltroEmpresa("", "op"));

            SQL.Append(" ORDER BY op.Id DESC ");


            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoView> GetByRefViewGestaoCompra(string referencia)
        {
            var cn = new DapperConnection<OrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("a.descricao as DescricaoAlmoxarifado");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN almoxarifados a ON a.id = op.AlmoxarifadoId");
            SQL.AppendLine("WHERE op.referencia = " + "'" + referencia + "'" );


            SQL.Append(" AND " + FiltroEmpresa("", "op"));

            SQL.Append(" ORDER BY op.Id DESC ");


            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }

        public OrdemProducaoView GetByProdutoIdView(int id)
        {
            var cn = new DapperConnection<OrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("a.descricao as DescricaoAlmoxarifado");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN almoxarifados a ON a.id = op.AlmoxarifadoId");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "op"));
            SQL.AppendLine(" AND op.Id = " + id);
            

            OrdemProducaoView ret = new OrdemProducaoView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            //if (ret != null)
            //{
            //    var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            //    ret.Itens = itemOrdemProducaoRepository.GetByPedido(ret.Id).ToList();
            //}

            return ret;
        }

        public IEnumerable<OrdemProducaoView> GetAllView(int IdOp = 0)
        {
            var cn = new DapperConnection<OrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("a.descricao as DescricaoAlmoxarifado,");
            SQL.AppendLine("cob.nome as colaborador ,");

            //SQL.AppendLine("SUM(iop.quantidade-iop.quantidadeatendida)/SUM(iop.quantidade) as aberto,");
            //SQL.AppendLine("SUM(iop.quantidadeatendida-iop.quantidadeproduzida)/SUM(iop.quantidade) as producao,"); ALEX comentei por causa das OPs sem pacotes 08/08/2022

            SQL.AppendLine("IF(SUM(iop.quantidadeproduzida)/SUM(iop.quantidade) = 1 AND SUM(iop.quantidade-iop.quantidadeatendida)/SUM(iop.quantidade) = 1,0,SUM(iop.quantidade-iop.quantidadeatendida)/SUM(iop.quantidade)) as aberto,");
            SQL.AppendLine("IF(SUM(iop.quantidadeatendida-iop.quantidadeproduzida)/SUM(iop.quantidade) < 0,0,SUM(iop.quantidadeatendida-iop.quantidadeproduzida)/SUM(iop.quantidade)) as producao,");

            SQL.AppendLine("SUM(iop.quantidadeproduzida)/SUM(iop.quantidade) as concluido,");
            SQL.AppendLine("SUM(iop.quantidadeproduzida) as qtdproduzida");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN almoxarifados a ON a.id = op.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("LEFT JOIN colaboradores cob ON cob.id = op.IdColaborador");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "op"));
            if(IdOp > 0 )
            {
                SQL.AppendLine(" AND op.id = " + IdOp );
            }
            SQL.AppendLine(" GROUP BY op.Id DESC");
            SQL.AppendLine(" ORDER BY op.Referencia DESC");

            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducao> GetAllByProduto(int produtoId,bool ExcluirRegisto)
        {
            var cn = new DapperConnection<OrdemProducao>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "op"));
            SQL.AppendLine(" AND iop.ProdutoId = " + produtoId);
            if(ExcluirRegisto == false)
            {
                SQL.AppendLine(" AND op.Status <> 0 AND op.Status <> 1 AND op.Status <> 5 AND op.Status <> 6"); // diferentes de ordens com status novo, aberto, finalizado e atendido
            }

            SQL.AppendLine("GROUP BY op.Id DESC");
            SQL.AppendLine("ORDER BY op.Referencia DESC");

            return cn.ExecuteStringSqlToList(new OrdemProducao(), SQL.ToString());
        }


        public void UpdateStatus(int OrdemProducaoId)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE ordemproducao SET ");
            SQL.AppendLine("Status = 6"); // FInalizado           
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(OrdemProducaoId);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public List<int> GetSemanas()
        {
            var cn = new DapperConnection<Int32>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.Semana as Semana ");
            SQL.AppendLine(" FROM 	ordemproducao op ");
            SQL.AppendLine(" WHERE  ");
            SQL.Append(FiltroEmpresa("", " op "));
            SQL.AppendLine(" AND op.Status <> 6 ");
            SQL.AppendLine(" GROUP BY op.Semana DESC ");
            SQL.AppendLine(" ORDER BY op.Semana ");



            return cn.ExecuteStringSqlToList(new Int32(), SQL.ToString()).ToList();
        }

        public void AlimentaItemCatalogo(List<ItemOrdemProducaoView> ListaIncluir, List<ItemOrdemProducaoView> ListaExcluir = null)
        {
            if (ListaExcluir != null)
            {
                foreach (ItemOrdemProducaoView itemExcluir in ListaExcluir)
                {
                    var SQL = new StringBuilder();
                    SQL.AppendLine("UPDATE produtodetalhes SET ");
                    SQL.AppendLine("TotalOp = TotalOp - ");
                    SQL.Append(itemExcluir.Quantidade.ToString().Replace(",","."));
                    SQL.AppendLine(" WHERE produtodetalhes.IdProduto =  ");
                    SQL.Append(itemExcluir.ProdutoId);
                    SQL.AppendLine(" AND   produtodetalhes.Idcor =  ");
                    SQL.Append(itemExcluir.CorId);
                    SQL.AppendLine(" AND   produtodetalhes.IdTamanho =  ");
                    SQL.Append(itemExcluir.TamanhoId);
                    _cn.ExecuteNonQuery(SQL.ToString());
                }

            }
            var SQL2 = new StringBuilder();
            SQL2.AppendLine(" UPDATE produtodetalhes set TotalOp = 0 WHERE produtodetalhes.TotalOp < 0 ");
            _cn.ExecuteNonQuery(SQL2.ToString());
                        

            
            foreach (ItemOrdemProducaoView itemIncluir in ListaIncluir)
            {
                var SQL3 = new StringBuilder();
                if (itemIncluir.Quantidade > 0)
                {
                    SQL3.AppendLine("UPDATE produtodetalhes SET ");
                    SQL3.AppendLine("TotalOp = TotalOp + ");
                    SQL3.Append(itemIncluir.Quantidade.ToString().Replace(",", "."));
                    SQL3.AppendLine(" WHERE produtodetalhes.IdProduto =  ");
                    SQL3.Append(itemIncluir.ProdutoId);
                    SQL3.AppendLine(" AND   produtodetalhes.Idcor =  ");
                    SQL3.Append(itemIncluir.CorId);
                    SQL3.AppendLine(" AND   produtodetalhes.IdTamanho =  ");
                    SQL3.Append(itemIncluir.TamanhoId);
                    _cn.ExecuteNonQuery(SQL3.ToString());
                }
            }
            
        }


        public void AlimentaItemCatalogoExcluir(List<ItemOrdemProducaoView> ListaExcluir)
        {

            foreach (ItemOrdemProducaoView itemExcluir in ListaExcluir)
            {
                var SQL = new StringBuilder();
                SQL.AppendLine("UPDATE produtodetalhes SET ");
                SQL.AppendLine("TotalOp = TotalOp - ");
                SQL.Append(itemExcluir.Quantidade.ToString().Replace(",", "."));
                SQL.AppendLine(" WHERE produtodetalhes.IdProduto =  ");
                SQL.Append(itemExcluir.ProdutoId);
                SQL.AppendLine(" AND   produtodetalhes.Idcor =  ");
                SQL.Append(itemExcluir.CorId);
                SQL.AppendLine(" AND   produtodetalhes.IdTamanho =  ");
                SQL.Append(itemExcluir.TamanhoId);
                _cn.ExecuteNonQuery(SQL.ToString());
            }

            var SQL2 = new StringBuilder();
            SQL2.AppendLine(" UPDATE produtodetalhes set TotalOp = 0 WHERE produtodetalhes.TotalOp < 0 ");
            _cn.ExecuteNonQuery(SQL2.ToString());
        }

        public void AtualizaTotalItens(int idOrdem)
        {
            string sqlItens = string.Empty;
            var cn = new DapperConnection<ItemOrdemProducao>();

            string sqlCliente = string.Empty;
            var cnCli = new DapperConnection<Colaborador>();

            sqlItens = "select SUM(itensordemproducao.Quantidade) as Quantidade from itensordemproducao where itensordemproducao.OrdemProducaoId =  " + idOrdem;
            var It = new ItemOrdemProducao();
            var dados = cn.ExecuteStringSqlToList(It, sqlItens.ToString());

            decimal TotalItens = 0;

            if (dados != null && dados.Count() > 0)
            {
                List<ItemOrdemProducao> Item = new List<ItemOrdemProducao>();
                Item = dados.ToList();
                TotalItens = Item[0].Quantidade;
            }

            sqlCliente = "UPDATE ordemproducao SET TotalItens =  " + TotalItens + " WHERE id = " + idOrdem;
            _cn.ExecuteNonQuery(sqlCliente);

           

        }


        public void ExcluirOrdensPedido(int idOrdem)
        {
            string sqlOrdemNoPedido = string.Empty;

            sqlOrdemNoPedido = "DELETE FROM pedidocompraordemproducao WHERE pedidocompraordemproducao.ordemproducaoid =  " + idOrdem;
            _cn.ExecuteNonQuery(sqlOrdemNoPedido);

        }


        public OrdemProducaoView GetByIdViewMudaData(int id)
        {
            var cn = new DapperConnection<OrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("a.descricao as DescricaoAlmoxarifado");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN almoxarifados a ON a.id = op.AlmoxarifadoId");
            SQL.Append(" WHERE " + FiltroEmpresa("", "op"));
            SQL.AppendLine(" AND op.Id = " + id);

            OrdemProducaoView ret = new OrdemProducaoView();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
                ret.Itens = itemOrdemProducaoRepository.GetByMudaData(ret.Id).ToList();
            }

            return ret;
        }

        public void TrataOrdemAberto()
        {
            string sqlOrdem = string.Empty;
            string sqlOrdemMaterial = string.Empty;

            sqlOrdem = " DELETE FROM itemliberacaoordemproducao WHERE ordemproducaoid IN ( "
                      +"   SELECT * FROM ( "
                      +"       SELECT op.id FROM ordemproducao op "
                      +"           LEFT JOIN ordemproducaomateriais opm ON opm.ordemproducaoid = op.id "
                      +"           LEFT JOIN itemliberacaoordemproducao iop ON iop.ordemproducaoid = op.id "
                      +"           WHERE op.status = 1 "
                      +"            GROUP BY op.id "
                      +"            HAVING count(opm.id) > 0 OR count(iop.id) > 0 "
	                  +"    ) AS id "
                      +" ) ";

            _cn.ExecuteNonQuery(sqlOrdem);

            sqlOrdemMaterial = " DELETE FROM ordemproducaomateriais WHERE ordemproducaoid IN ( "
                      + "   SELECT * FROM ( "
                      + "       SELECT op.id FROM ordemproducao op "
                      + "           LEFT JOIN ordemproducaomateriais opm ON opm.ordemproducaoid = op.id "
                      + "           LEFT JOIN itemliberacaoordemproducao iop ON iop.ordemproducaoid = op.id "
                      + "           WHERE op.status = 1 "
                      + "            GROUP BY op.id "
                      + "            HAVING count(opm.id) > 0 OR count(iop.id) > 0 "
                      + "    ) AS id "
                      + " ) ";

            _cn.ExecuteNonQuery(sqlOrdemMaterial);
        }

        public void EnviarParaCorte(bool cancelaEnvio, int ordemId)
        {
            if (cancelaEnvio) //retorna para liberado 
            {
                string sql = string.Empty;
                sql = "UPDATE ordemproducao SET Status =  " + (int)enumStatusOrdemProducao.Em_Corte + " , " +
                    " Corte = NULL" +
                    " WHERE id = " + ordemId;
                _cn.ExecuteNonQuery(sql);
            }
            else
            {
                var corte = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
                string sql = string.Empty;
                sql = "UPDATE ordemproducao SET Status =  " + (int)enumStatusOrdemProducao.Enviado_Corte + " , " +
                    " Corte = ' " + corte + " ' " +
                    " WHERE id = " + ordemId;
                _cn.ExecuteNonQuery(sql);
            }
        }

        public void UpdateObsMateriais(int ordemId, string observacao)
        {
            string sql = string.Empty;
            sql = "UPDATE ordemproducao SET observacaomateriais =  ' " + observacao + " ' " +
                " WHERE id = " + ordemId;
            _cn.ExecuteNonQuery(sql);
        }

        public void AlteraOp(int ordemId, string observacao,DateTime dtLiberacao,DateTime DtCorte, DateTime dtFinalizacao)
        {
            string sql = string.Empty;
            sql = "UPDATE ordemproducao SET Observacao =  '" + observacao + "'" + "," + " DataPrevisaoLiberacao = " +  "'" + dtLiberacao.ToString("yyyy-MM-dd HH:mm:ss") + "'" + "," + "DataPrevisaoCorte = " + "'" + DtCorte.ToString("yyyy-MM-dd HH:mm:ss") + "', DataPrevisaoFinalizacao = '" + dtFinalizacao.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " WHERE id = " + ordemId;
            _cn.ExecuteNonQuery(sql);
        }

        public IEnumerable<OrdemProducaoView> GetByItem(string referencia)
        {
            var cn = new DapperConnection<OrdemProducaoView>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("a.descricao as DescricaoAlmoxarifado,");
            SQL.AppendLine("cob.nome as colaborador ,");
            SQL.AppendLine("SUM(iop.quantidade-iop.quantidadeatendida)/SUM(iop.quantidade) as aberto,");
            SQL.AppendLine("SUM(iop.quantidadeatendida-iop.quantidadeproduzida)/SUM(iop.quantidade) as producao,");
            SQL.AppendLine("SUM(iop.quantidadeproduzida)/SUM(iop.quantidade) as concluido");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN almoxarifados a ON a.id = op.AlmoxarifadoId");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN produtos p ON iop.ProdutoId = p.id");
            SQL.AppendLine("LEFT JOIN colaboradores cob ON cob.id = op.IdColaborador");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "op"));
            if ( !string.IsNullOrEmpty(referencia))
            {
                SQL.AppendLine(" AND p.referencia like '%" + referencia + "%' ");
            }
            SQL.AppendLine(" GROUP BY op.Id DESC");
            SQL.AppendLine(" ORDER BY op.Referencia DESC");

            return cn.ExecuteStringSqlToList(new OrdemProducaoView(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoFaccao> GetByOrdemFacao(List<int> IdOrdem)
        {
            var cn = new DapperConnection<OrdemProducaoFaccao>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");     
            SQL.AppendLine("op.referencia as RefOrdem, CONCAT(cob.referencia,'-',cob.nome) as Faccao ,cob.Id as IdFaccao,");
            SQL.AppendLine("p.id as IdItem,p.referencia as RefItem,p.descricao as DescricaoItem, c.Descricao as cor, t.Abreviatura as Tamanho, iop.Quantidade");          
            SQL.AppendLine("FROM 	ordemproducao op");  
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN produtos p ON iop.ProdutoId = p.id");
            SQL.AppendLine("INNER JOIN cores c ON iop.CorId = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON iop.TamanhoId = t.id");
            SQL.AppendLine("LEFT JOIN colaboradores cob ON cob.id = op.IdColaborador");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "op"));           
            SQL.AppendLine(" AND op.id in (" + string.Join(", ", IdOrdem) + ") ");
            SQL.AppendLine(" ORDER BY op.Referencia ");


            return cn.ExecuteStringSqlToList(new OrdemProducaoFaccao(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoFaccao> GetByOrdemFacaoMatizCor(List<int> IdOrdem)
        {
            var cn = new DapperConnection<OrdemProducaoFaccao>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	op.*,");
            SQL.AppendLine("op.id as IdOrdem, op.referencia as RefOrdem, CONCAT(cob.referencia,'-',cob.nome) as Faccao ,cob.Id as IdFaccao,");
            SQL.AppendLine("p.id as IdItem,p.referencia as RefItem,p.descricao as DescricaoItem, c.Descricao as cor,c.Id as IdCor ");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN produtos p ON iop.ProdutoId = p.id");
            SQL.AppendLine("INNER JOIN cores c ON iop.CorId = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON iop.TamanhoId = t.id");
            SQL.AppendLine("LEFT JOIN colaboradores cob ON cob.id = op.IdColaborador");
            SQL.AppendLine("WHERE ");
            SQL.Append(FiltroEmpresa("", "op"));
            SQL.AppendLine(" AND op.id in (" + string.Join(", ", IdOrdem) + ") ");
            SQL.AppendLine("  GROUP By p.id,iop.CorId ");
            SQL.AppendLine(" ORDER BY op.Referencia ");


            return cn.ExecuteStringSqlToList(new OrdemProducaoFaccao(), SQL.ToString());
        }


        public IEnumerable<OrdemProducaoFaccao> GetByOrdemFacaoMatrizCorTamanho(int IdOrdem, int IdItem,int IdCor)
        {
            var cn = new DapperConnection<OrdemProducaoFaccao>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	t.id as IdTamanho,t.Abreviatura as Tamanho,iop.CorId, SUM(iop.Quantidade) as Quantidade ");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN produtos p ON iop.ProdutoId = p.id");
            SQL.AppendLine("INNER JOIN cores c ON iop.CorId = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON iop.TamanhoId = t.id");            
            SQL.AppendLine("WHERE iop.CorId = " + IdCor + " AND ");
            SQL.AppendLine(" iop.ProdutoId = " + IdItem + " AND ");
            SQL.Append(FiltroEmpresa("", "op"));
            SQL.AppendLine(" AND op.id = " + IdOrdem);
            SQL.AppendLine("  GROUP By iop.TamanhoId,iop.CorId ");
            SQL.AppendLine(" ORDER BY op.Referencia ");


            return cn.ExecuteStringSqlToList(new OrdemProducaoFaccao(), SQL.ToString());
        }

        public IEnumerable<OrdemProducaoFaccao> GetByOrdemFacaoMatrizSoTamanhos(List<int> IdOrdem)
        {
            var cn = new DapperConnection<OrdemProducaoFaccao>();


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	t.id as IdTamanho,t.Abreviatura as Tamanho ");
            SQL.AppendLine("FROM 	ordemproducao op");
            SQL.AppendLine("INNER JOIN itensordemproducao iop ON iop.ordemproducaoid = op.id");
            SQL.AppendLine("INNER JOIN produtos p ON iop.ProdutoId = p.id");
            SQL.AppendLine("INNER JOIN cores c ON iop.CorId = c.id");
            SQL.AppendLine("INNER JOIN tamanhos t ON iop.TamanhoId = t.id");
            SQL.AppendLine("WHERE  ");
            SQL.Append(FiltroEmpresa("", "op"));
            SQL.AppendLine(" AND op.id in (" + string.Join(", ", IdOrdem) + ") ");
            SQL.AppendLine("  GROUP By iop.TamanhoId ");
            


            return cn.ExecuteStringSqlToList(new OrdemProducaoFaccao(), SQL.ToString());
        }

        public OrdemProducao GetPorRefrencia(string Referencia)
        {
            string SQL = String.Empty;

            SQL = "SELECT * FROM ordemproducao WHERE ordemproducao.Referencia = " + "'" + Referencia + "'";

            OrdemProducao ret = new OrdemProducao();

            _cn.ExecuteToModel(ref ret, SQL.ToString());

            return ret;

        }

        public void MudaPrevisaoFinalizacao(int ordemId, DateTime dtPrevisaoFinalizacao)
        {
            string sql = string.Empty;
            sql = "UPDATE ordemproducao SET  DataPrevisaoFinalizacao = '" + dtPrevisaoFinalizacao.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " WHERE id = " + ordemId;
            _cn.ExecuteNonQuery(sql);
        }
    }
}
