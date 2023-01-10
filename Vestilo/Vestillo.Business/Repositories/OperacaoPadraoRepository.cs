
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
    public class OperacaoPadraoRepository: GenericRepository<OperacaoPadrao>
    {
        public OperacaoPadraoRepository() : base(new DapperConnection<OperacaoPadrao>())
        {
        }


        public IEnumerable<OperacaoPadrao> GetByAllSetorBal()
        {
            int qtdCasas = VestilloSession.QtdCasasTempo;
            var SQL = String.Empty;

            SQL = " SELECT operacaopadrao.id,operacaopadrao.Referencia,operacaopadrao.Descricao, ROUND(operacaopadrao.TempoCosturaSemAviamento, "+ qtdCasas.ToString() + ") as TempoCosturaSemAviamento, " +
                  "ROUND(operacaopadrao.TempoCosturaComAviamento, " + qtdCasas.ToString() + ") as TempoCosturaComAviamento ,operacaopadrao.Ativo,IFNULL(STR.descricao,'') as Setor,IFNULL(BAL.descricao,'') as Balanceamento  from operacaopadrao " +
                  " LEFT JOIN setores STR on STR.id = operacaopadrao.IdSetor " +
                  " LEFT JOIN setores BAL on BAL.id = operacaopadrao.IdBalanceamento ";

            var opr = new OperacaoPadrao();
            return _cn.ExecuteStringSqlToList(opr, SQL);
        }

        public IEnumerable<OperacaoPadrao> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("*")
                .From("operacaopadrao ");
            if (AtivoInativo > 0)
            {
                SQL.Where(" Ativo = " + AtivoInativo );
            }
            var opr = new OperacaoPadrao();
            return _cn.ExecuteStringSqlToList(opr, SQL.ToString());
        }

        public IEnumerable<OperacaoPadrao> GetListPorReferencia(string referencia)
        {
            OperacaoPadrao m = new OperacaoPadrao();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<OperacaoPadrao> GetListPorDescricao(string desc)
        {
            OperacaoPadrao m = new OperacaoPadrao();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<OperacaoPadrao> GetListById(int id)
        {
            OperacaoPadrao m = new OperacaoPadrao();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1");
        }
    }
}
