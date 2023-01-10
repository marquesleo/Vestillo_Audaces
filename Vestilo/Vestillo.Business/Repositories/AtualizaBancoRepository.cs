using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;
using MySql.Data.MySqlClient;

namespace Vestillo.Business.Repositories
{
    public class AtualizaBancoRepository
    {
        private MySqlConnection mConn;

        private MySqlDataAdapter mAdapter;

        private DataSet mDataSet;

        public bool ExecutarComandos(List<string> comandos)
        {
            var cn = new DapperConnection<string>();

            try
            {
                cn.Provider.BeginTransaction();

                foreach (string sql in comandos)
                {
                    cn.ExecuteNonQuery(sql);
                }

                cn.Provider.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                cn.Provider.RollbackTransaction();
                throw ex;
            }

        }

        public int RegistroExiste(string Query)
        {
            int Count = 0;
            var cn = new DapperConnection<string>();

            var retorno = cn.ExecuteToDataTable(Query);

            if (retorno != null && retorno.Rows.Count > 0)
            {
                Count = Convert.ToInt32(retorno.Rows[0]["contador"]);
            }

            return Count;
        }

        public void AcertaTabelaFichaExcecoes()
        {
            string Query = String.Empty;
            try
            {
                Query = " select *, count(*) from fichatecnicadomaterialrelacao group by fichatecnicaId,materiaprimaId,cor_materiaprima_Id,tamanho_materiaprima_Id ,produtoId,tamanho_produto_Id,cor_produto_Id,fichatecnicaitemId having count(*) > 1 ";
                var cn = new DapperConnection<string>();

                var retorno = cn.ExecuteToDataTable(Query);

                if (retorno != null && retorno.Rows.Count > 0)
                {
                   
                    foreach (DataRow linhasGrades in retorno.Rows)
                    {
                        Query = String.Empty;

                        int fichatecnicaId = 0;
                        int materiaprimaId = 0;
                        int cor_materiaprima_Id = 0;
                        int tamanho_materiaprima_Id = 0;
                        int produtoId = 0;
                        int tamanho_produto_Id = 0;
                        int cor_produto_Id = 0;
                        int fichatecnicaitemId = 0;

                        int IdMenor = 0;

                        fichatecnicaId = (int)linhasGrades["fichatecnicaId"];
                        materiaprimaId = (int)linhasGrades["materiaprimaId"];
                        cor_materiaprima_Id = (int)linhasGrades["cor_materiaprima_Id"];
                        tamanho_materiaprima_Id = (int)linhasGrades["tamanho_materiaprima_Id"];
                        produtoId = (int)linhasGrades["ProdutoId"];
                        tamanho_produto_Id = (int)linhasGrades["tamanho_produto_Id"];
                        cor_produto_Id = (int)linhasGrades["cor_produto_Id"];
                        fichatecnicaitemId = (int)linhasGrades["fichatecnicaitemId"];





                        Query = " select MIN(fichatecnicadomaterialrelacao.id) as MenorId from fichatecnicadomaterialrelacao where fichatecnicaId = " + fichatecnicaId +
                              " and materiaprimaId = " + materiaprimaId + " and cor_materiaprima_Id = " + cor_materiaprima_Id + " and tamanho_materiaprima_Id = " + tamanho_materiaprima_Id +
                              " and produtoId = " + produtoId + " and tamanho_produto_Id =  " + tamanho_produto_Id +
                              " and cor_produto_Id = " + cor_produto_Id + " and fichatecnicaitemId = " + fichatecnicaitemId + " order by id ";
                        var retornoMin = cn.ExecuteToDataTable(Query);
                        

                        IdMenor = (int)retornoMin.Rows[0]["MenorId"];

                        Query = String.Empty;

                        Query = " delete from fichatecnicadomaterialrelacao  where fichatecnicaId = " + fichatecnicaId +
                              " and materiaprimaId = " + materiaprimaId + " and cor_materiaprima_Id = " + cor_materiaprima_Id + " and tamanho_materiaprima_Id = " + tamanho_materiaprima_Id +
                              " and produtoId = " + produtoId + " and tamanho_produto_Id =  " + tamanho_produto_Id +
                              " and cor_produto_Id = " + cor_produto_Id + " and fichatecnicaitemId = " + fichatecnicaitemId + " and id > " + IdMenor;

                        cn.ExecuteNonQuery(Query);
                    }

                }
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizaObsDaFicha()
        {
            string Query = String.Empty;
            try
            {
                Query = " select * from fichatecnica where produtoid order by fichatecnica.produtoid ";
                var cn = new DapperConnection<string>();

                var retorno = cn.ExecuteToDataTable(Query);

                if (retorno != null && retorno.Rows.Count > 0)
                {

                    foreach (DataRow linhasGrades in retorno.Rows)
                    {
                        Query = String.Empty;

                        int ProdutoId = 0;
                        string ObsFicha = String.Empty;

                        ProdutoId = (int)linhasGrades["produtoid"];
                        ObsFicha = linhasGrades["Observacao"].ToString();

                        if(String.IsNullOrEmpty(ObsFicha))
                        {
                            Query = "select * from fichatecnicadomaterial where fichatecnicadomaterial.ProdutoId = " + ProdutoId;
                            var cn2 = new DapperConnection<string>();
                            var retorno2 = cn.ExecuteToDataTable(Query);
                            if (retorno2 != null && retorno2.Rows.Count > 0)
                            {
                                ProdutoId = (int)linhasGrades["produtoid"];
                                ObsFicha = linhasGrades["Observacao"].ToString();

                                ExecutaInsert(ProdutoId, ObsFicha);

                            }
                        }
                        else
                        {
                            ExecutaInsert(ProdutoId, ObsFicha);
                        }
                                               
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void ExecutaInsert(int ProdutoId, string ObsFicha)
        {
            var cnInsert = new DapperConnection<string>();
            string SQL = "INSERT INTO observacaoproduto (ProdutoId, Observacao) " +
                         " VALUES (" + ProdutoId + "," + "'" + ObsFicha + "'" + ")";
            cnInsert.ExecuteNonQuery(SQL);

        }


        public void LiberarEmpenhoAMais()
        {
                       
            var cn = new DapperConnection<string>();
            var Sql = String.Empty;
            try
            {
                   

                Sql = " select SUM(estoque.Empenhado) as EmpenhoEstoque,estoque.ProdutoId as MatId,estoque.CorId as CorId,estoque.TamanhoId as TamanhoId from estoque WHERE AlmoxarifadoId = 3 " +
                            "  AND NOT ISNULL(estoque.CorId) AND NOT ISNULL(estoque.TamanhoId)  " +
                            " AND estoque.ProdutoId IN (select produtos.Id from produtos WHERE produtos.TipoItem = 1) " +
                            "  group by estoque.ProdutoId, estoque.TamanhoId, estoque.CorId " +
                            "  order by estoque.ProdutoId, estoque.TamanhoId, estoque.CorId; ";

                var retorno = cn.ExecuteToDataTable(Sql);

                   

                if (retorno != null && retorno.Rows.Count > 0)
                {
                    foreach (DataRow linhasItens in retorno.Rows)
                    {
                        int idItem = 0;
                        int IdCor = 0;
                        int IdTamanho = 0;
                        decimal EmpenhoOrdem = 0;
                        decimal EmpenhoSaldo = 0;
                        decimal EmpenhoAMais = 0;
                        Sql = String.Empty;

                       
                        idItem = (int)linhasItens["MatId"];
                        IdCor = (int)linhasItens["CorId"];
                        IdTamanho = (int)linhasItens["TamanhoId"];
                        EmpenhoSaldo = (decimal)linhasItens["EmpenhoEstoque"];



                        Sql = " select SUM(ordemproducaomateriais.quantidadeempenhada) as quantidadeempenhada " +
                                                " from ordemproducaomateriais " +
                                                " where quantidadeempenhada > 0 AND ordemproducaomateriais.materiaprimaid = " + idItem +
                                                " and ordemproducaomateriais.corid = " + IdCor +
                                                " and ordemproducaomateriais.tamanhoid = " + IdTamanho +
                                                " group by materiaprimaid, tamanhoid, corid " +
                                                " order by materiaprimaid, tamanhoid, corid; ";

                            
                        var retornoOrdem = cn.ExecuteToDataTable(Sql);


                        if (retornoOrdem.Rows.Count > 0 && retornoOrdem != null)
                        {
                            EmpenhoOrdem = Convert.ToDecimal(retornoOrdem.Rows[0]["quantidadeempenhada"]);
                            EmpenhoAMais = EmpenhoSaldo - EmpenhoOrdem;
                        }
                        else
                        {
                            EmpenhoAMais = EmpenhoSaldo;

                        }

                        if (EmpenhoAMais < 0)
                        {
                            EmpenhoAMais = 0;
                        }


                        Sql = "UPDATE estoque SET Saldo = estoque.Saldo + " + EmpenhoAMais.ToString().Replace(",", ".") + " WHERE ProdutoId = " + idItem +
                        " AND CorId = " + IdCor + " AND TamanhoId = " + IdTamanho + " AND estoque.AlmoxarifadoId = 3";                            
                        cn.ExecuteNonQuery(Sql);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }   

        }


        public void AcertarEmpenho()
        {
            var cn = new DapperConnection<string>();
            var Sql = String.Empty;
            try
            {
                DataTable dt = new DataTable();
                //cria um adapter usando a instrução SQL para acessar a tabela Clientes


                Sql = "update estoque set estoque.Empenhado = 0 where  estoque.ProdutoId IN(select produtos.id from produtos where TipoItem = 1)";
                cn.ExecuteNonQuery(Sql);

                
               
                Sql = " select produtos.id as MatId,cores.id as CorId,tamanhos.Id as TamanhoId,SUM(ordemproducaomateriais.quantidadeempenhada) as quantidadeempenhada " +
                                            " from ordemproducaomateriais " +
                                            " INNER JOIN produtos on produtos.id = ordemproducaomateriais.materiaprimaid " +
                                            " INNER JOIN cores on cores.id = ordemproducaomateriais.corid " +
                                            " INNER JOIN tamanhos on tamanhos.Id = ordemproducaomateriais.tamanhoid " +
                                            " where quantidadeempenhada > 0 " +
                                            " group by materiaprimaid, tamanhoid, corid " +
                                            " order by materiaprimaid, tamanhoid, corid; ";

                var retorno = cn.ExecuteToDataTable(Sql);



                foreach (DataRow linhasItens in retorno.Rows)
                {
                    int idItem = 0;
                    int IdCor = 0;
                    int IdTamanho = 0;
                    decimal Empenho = 0;

                    idItem = (int)linhasItens["MatId"];
                    IdCor = (int)linhasItens["CorId"];
                    IdTamanho = (int)linhasItens["TamanhoId"];
                    Empenho = (decimal)linhasItens["quantidadeempenhada"];

                    Sql = "UPDATE estoque SET Empenhado = " + Empenho.ToString().Replace(",", ".") + " WHERE ProdutoId = " + idItem +
                    " AND CorId = " + IdCor + " AND TamanhoId = " + IdTamanho;

                    cn.ExecuteNonQuery(Sql);

                  

                }

            }
            catch (Exception ex)
            {
                throw ex; 
            }
          

        }


    }
}
