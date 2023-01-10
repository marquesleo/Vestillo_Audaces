
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Repositories
{
    public class MovimentacaoBancoRepository : GenericRepository<MovimentacaoBanco>
    {
        public MovimentacaoBancoRepository()  : base(new DapperConnection<MovimentacaoBanco>())
        {

        }

        //para preencher o grid da tela de browse
        public IEnumerable<MovimentacaoBancoView> GetCamposBrowse()
        {

            var cn = new DapperConnection<MovimentacaoBancoView>();
            var p = new MovimentacaoBancoView();


            var SQL = new Select()
                .Campos(" movimentacaobanco.Id as Id,IdBanco, bancos.descricao as DescricaoBanco,bancos.agencia,bancos.conta,bancos.numbanco as NumBanco, IdContasReceber, " +
                        " IdContasPagar,IdCheque,IdUsuario,DataMovimento,Tipo,Valor,Observacao")
                .From("movimentacaobanco")
                .InnerJoin(" bancos", "bancos.id =  movimentacaobanco.IdBanco")
                .Where(FiltroEmpresa("movimentacaobanco.idEmpresa"))
                .OrderBy("movimentacaobanco.id");

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public void DeleteByCheque(int chequeId)
        {
            string sql = "DELETE FROM movimentacaobanco WHERE IdCheque = " + chequeId.ToString();
            _cn.ExecuteNonQuery(sql);
        }

        public void DeleteByContasReceber(int ctrId)
        {
            string sql = "DELETE FROM movimentacaobanco WHERE IdContasReceber = " + ctrId.ToString();
            _cn.ExecuteNonQuery(sql);
        }

        public void DeleteByContasPagar(int ctpId)
        {
            string sql = "DELETE FROM movimentacaobanco WHERE IdContasPagar = " + ctpId.ToString();
            _cn.ExecuteNonQuery(sql);
        }


        public IEnumerable<MovimentacaoBancoView> GetRelExtratoBancarioBrowse(FiltroExtratoBancarioRel filtro)
        {

            var cn = new DapperConnection<MovimentacaoBancoView>();
            var p = new MovimentacaoBancoView();
            string Natureza = String.Empty;
            string Lancamento = String.Empty;
            int TipoLancamento = 0;

            if (filtro.IdsNatureza != null)
            {
                Natureza = " AND movimentacaobanco.IdMovFinanceira in (" + string.Join(",", filtro.IdsNatureza.ToArray()) + ")";
            }

            switch (filtro.Lancamentos)
            {
                case "Crédito":
                    TipoLancamento = 1;
                        break;
                case "Débito":
                    TipoLancamento = 2;
                    break;
                case  "Transferência":
                    TipoLancamento = 3;
                    break;       
            }

            if(TipoLancamento !=0)
            {
                Lancamento = " AND movimentacaobanco.tipo = " + TipoLancamento;
            }




            var SQL = new Select()
                .Campos(" movimentacaobanco.Id as Id,IdBanco,IFNULL(IdBancoDestino,0) as IdBancoDestino, IFNULL(naturezasfinanceiras.referencia,'') as natureza,bancos.descricao as DescricaoBanco,bancos.agencia,bancos.conta,bancos.numbanco as NumBanco, IdContasReceber, " +
                        " IdContasPagar,IdCheque,IdUsuario,DataMovimento,Tipo,Valor,Observacao")
                .From("movimentacaobanco")
                .InnerJoin(" bancos", "bancos.id =  movimentacaobanco.IdBanco")
                .LeftJoin(" naturezasfinanceiras", "naturezasfinanceiras.id =  movimentacaobanco.IdMovFinanceira")
                .Where(FiltroEmpresa("movimentacaobanco.idEmpresa") + " AND Date(movimentacaobanco.DataMovimento) between '" + filtro.DaEmissao.ToString("yyyy-MM-dd") + "' AND '" + filtro.AteEmissao.ToString("yyyy-MM-dd") + "'" +
                " AND (movimentacaobanco.IdBanco = " + filtro.IdBanco + " OR movimentacaobanco.IdBancoDestino =  " + filtro.IdBanco + ")" + Natureza + Lancamento)
                .OrderBy("movimentacaobanco.DataMovimento");

            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }

        public decimal SaldoAnterior(int banco,DateTime data)
        {
            decimal valor = 0;
            decimal credito = 0;
            decimal debito = 0;

            string SQL = String.Empty;
            MovimentacaoBanco B = new MovimentacaoBanco();


            SQL = "SELECT DataMovimento, Valor, Tipo, IdBanco, IdBancoDestino " +
                  "FROM movimentacaobanco " +
                  "WHERE  Date(movimentacaobanco.DataMovimento) < " + "'" + data.ToString("yyyy-MM-dd") + "'" +
                  " AND (movimentacaobanco.IdBanco = " + banco +
                  " OR IdBancoDestino = " + banco + ")";


            var dados = _cn.ExecuteStringSqlToList(B, SQL);

            foreach (var item in dados)
            {
                if(item.IdBancoDestino != null)
                {
                    if (item.IdBancoDestino > 0 && item.IdBancoDestino == banco)
                    {
                        credito += item.Valor;
                    }
                    else if (item.IdBancoDestino > 0 && item.IdBancoDestino != banco)
                    {
                        debito += item.Valor;
                    }
                }                
                else if(item.Tipo == 1)
                {
                    credito += item.Valor;
                }
                else if(item.Tipo == 2)
                {
                    debito += item.Valor;
                }

            }

            valor = credito - debito;

            return valor;
        }
    }
}