
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;
using System.Data;

namespace Vestillo.Core.Repositories
{
    public class DadosPacote
    {
        public decimal QtdEntregueFaccão { get; set; }
    }
    public class OrdemProducaoCoreRepository : GenericRepository<AcompanhamentoOrdemView>
    {
        public IEnumerable<AcompanhamentoOrdemView> ListDadosOrdem(DateTime DataInicio, DateTime DataFim,string CorTalao)
        {
            StringBuilder sql = new StringBuilder();


            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            sql.AppendLine("select itensordemproducao.ProdutoId,itensordemproducao.CorId,itensordemproducao.TamanhoId,ordemproducao.id as IdOrdem, ordemproducao.Referencia as RefOP, ordemproducao.Observacao as 'CorTalao', ");
            sql.AppendLine("produtos.Referencia as Produto, cores.Descricao as Cor, tamanhos.Abreviatura as Tamanho, ");
            sql.AppendLine("if((itensordemproducao.QuantidadeAtendida = 0 && itensordemproducao.QuantidadeProduzida = 0), 'Em Aberto',   ");
            sql.AppendLine("if((itensordemproducao.QuantidadeAtendida = itensordemproducao.Quantidade) && itensordemproducao.QuantidadeProduzida = 0, 'Pacotes Criados', if((itensordemproducao.QuantidadeAtendida < itensordemproducao.Quantidade) && itensordemproducao.QuantidadeAtendida <> 0, 'Parcialmente Em Pacotes', if((itensordemproducao.QuantidadeProduzida + itensordemproducao.QuantidadeDefeito) = itensordemproducao.Quantidade,'Produzido','Parcialmente Produzido')))) as Status, ");
            sql.AppendLine("CONCAT(MID(ordemproducao.Corte,9,2),'/',MID(ordemproducao.Corte,6,2),'/',MID(ordemproducao.Corte,1,4)) as 'DataCorte', ");
            sql.AppendLine("CONCAT(MID(pacotes.dataentrada,9,2),'/',MID(pacotes.dataentrada,6,2),'/',MID(pacotes.dataentrada,1,4)) as 'DataEntrada', ");
            sql.AppendLine("if(operacaooperadora.data > 0,CONCAT(MID(MIN(ifnull(operacaooperadora.data,'')),9,2),'/',MID(MIN(ifnull(operacaooperadora.data,'')),6,2),'/',LEFT(MIN(ifnull(operacaooperadora.data,'')),4)),'') as 'DataEntradaInterna', ");
            sql.AppendLine("itensordemproducao.Quantidade as 'QtdDaOP', ");
            sql.AppendLine("(itensordemproducao.QuantidadeAtendida - itensordemproducao.QuantidadeProduzida) as 'QtdEmProducao', ");
            sql.AppendLine("(itensordemproducao.Quantidade - itensordemproducao.QuantidadeAtendida) as 'QtdSemPacote', ");
            sql.AppendLine("itensordemproducao.QuantidadeProduzida as 'QtdProduzida',Ifnull(pacotes.id,0) as IdPacote ");
            sql.AppendLine("from itensordemproducao ");
            sql.AppendLine("inner join ordemproducao on ordemproducao.id = itensordemproducao.OrdemProducaoId ");
            sql.AppendLine("inner join produtos on produtos.Id = itensordemproducao.ProdutoId ");
            sql.AppendLine("inner join cores on cores.Id = itensordemproducao.CorId ");
            sql.AppendLine("inner join tamanhos on tamanhos.Id = itensordemproducao.TamanhoId ");
            sql.AppendLine("left join pacotes on pacotes.itemordemproducaoid = itensordemproducao.id ");
            sql.AppendLine("left join operacaooperadora on operacaooperadora.PacoteId = pacotes.id ");
            sql.AppendLine("where ordemproducao.Status <> 6 ");
            sql.AppendLine("and ordemproducao.Observacao like '%" + CorTalao + "%' " );            
            sql.AppendLine(" and SUBSTRING(ordemproducao.DataEmissao, 1, 10) BETWEEN " + Valor + " group by itensordemproducao.id order by ordemproducao.dataemissao, produtos.Referencia, cores.Descricao, tamanhos.id");

            var DadosItens = VestilloConnection.ExecSQLToListWithNewConnection<AcompanhamentoOrdemView>(sql.ToString());


            List<AcompanhamentoOrdemView> ListaOps = new List<AcompanhamentoOrdemView>();
            foreach (var item in DadosItens)
            {

                string SQL = " select sum(pacotes.quantidade) as 'QtdEntregueFaccão' from operacaofaccao " +
                             " INNER JOIN pacotes ON pacotes.id = operacaofaccao.PacoteId " +
                             " inner join itensordemproducao on itensordemproducao.id = pacotes.itemordemproducaoid " +
                             " inner join ordemproducao on ordemproducao.id = itensordemproducao.OrdemProducaoId " +
                             " inner join produtos on produtos.Id = itensordemproducao.ProdutoId " +
                             " inner join cores on cores.Id = itensordemproducao.CorId " +
                             " inner join tamanhos on tamanhos.Id = itensordemproducao.TamanhoId " +
                             " WHERE pacotes.id = " + item.IdPacote + " AND operacaofaccao.OperacaoId = 65 " +
                             " group by itensordemproducao.id ";
                DataTable dt = VestilloConnection.ExecToDataTable(SQL.ToString());

                decimal QtdFacao = 0;
                if (dt.Rows.Count > 0)
                {
                    QtdFacao = decimal.Parse("0" + dt.Rows[0][0].ToString());
                }

                item.QtdEntregueFaccao = QtdFacao;

                string SQLPacotes = " SELECT distinct(itensordemproducao.id) as IdItem from itensordemproducao " +
                                    " INNER JOIN pacotes ON pacotes.itemordemproducaoid = itensordemproducao.id " +
                                    " WHERE itensordemproducao.OrdemProducaoId = " + item.IdOrdem;
                DataTable dtPacotes = VestilloConnection.ExecToDataTable(SQLPacotes.ToString());
                if (dtPacotes.Rows.Count > 0)
                {
                    string NumPacotes = String.Empty;
                    for (int i = 0; i < dtPacotes.Rows.Count; i++)
                    {
                        NumPacotes += dtPacotes.Rows[i][0].ToString() + ",";
                    }

                    NumPacotes = NumPacotes.Substring(0, NumPacotes.Length - 1);
                    string SQLPacotes2 = " SELECT pacotes.id from pacotes where pacotes.itemordemproducaoid in (" + NumPacotes + ") Order by pacotes.id";
                    DataTable dtPacotes2 = VestilloConnection.ExecToDataTable(SQLPacotes2.ToString());


                    if(dtPacotes2.Rows.Count > 0)
                    {
                        string NumPacotesOperacao = String.Empty;
                        for (int i = 0; i < dtPacotes2.Rows.Count; i++)
                        {
                            NumPacotesOperacao += dtPacotes2.Rows[i][0].ToString() + ",";
                        }

                        NumPacotesOperacao = NumPacotesOperacao.Substring(0, NumPacotesOperacao.Length - 1);
                        string SQLDataInterna = " select IFNULL(MIN(data),'') as DataEntrada from operacaooperadora where operacaooperadora.PacoteId in(" + NumPacotesOperacao + ")"; 
                        DataTable dtDataInterna = VestilloConnection.ExecToDataTable(SQLDataInterna.ToString());

                        string DataInterna = String.Empty;

                        if (dtDataInterna.Rows.Count > 0)
                        {

                            string SQLOperadora = " SELECT * from operacaooperadora WHERE operacaooperadora.PacoteId IN(SELECT pacotes.id as PacoteId from itensordemproducao " +
                                                    " INNER JOIN pacotes ON pacotes.itemordemproducaoid = itensordemproducao.id " +
                                                    " WHERE itensordemproducao.ProdutoId = " + item.ProdutoId + " AND itensordemproducao.CorId = " + item.CorId + 
                                                    " AND itensordemproducao.TamanhoId =" +  item.TamanhoId + " AND itensordemproducao.OrdemProducaoId = " + item.IdOrdem +")";
                            DataTable dtOperadora = VestilloConnection.ExecToDataTable(SQLOperadora.ToString());

                            if(dtOperadora.Rows.Count > 0)
                            {
                                DataInterna = dtDataInterna.Rows[0][0].ToString();
                                item.DataEntradaInterna = DataInterna.ToString().Substring(8,2) + "/" + DataInterna.ToString().Substring(5,2) + "/" + DataInterna.ToString().Substring(0,4);
                            }
                            else
                            {
                                
                                item.DataEntradaInterna = String.Empty;
                            }
                            
                        }
                        else
                        {
                            item.DataEntradaInterna = String.Empty;
                        }
                    }
                    else
                    {
                        item.DataEntradaInterna = String.Empty;
                    }
                    
                    
                }
                else
                {
                    item.DataEntradaInterna = String.Empty;
                }


                ListaOps.Add(item);
            }

            //return VestilloConnection.ExecSQLToListWithNewConnection<AcompanhamentoOrdemView>(sql.ToString());
            return ListaOps;
        }

    }
}
