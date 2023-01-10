
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
    public class MovimentosDaOperacaoRepository : GenericRepository<MovimentosDaOperacao>
    {
        public MovimentosDaOperacaoRepository() : base(new DapperConnection<MovimentosDaOperacao>())
        {
        }

        public IEnumerable<MovimentosDaOperacao> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id, Referencia, Descricao, Tempo,Ativo,Ordem ")
                .From("movimentosdaoperacao ");
            if (AtivoInativo > 0)
            {
                SQL.Where(" Ativo = " + AtivoInativo );
            }
            var opr = new MovimentosDaOperacao();
            return _cn.ExecuteStringSqlToList(opr, SQL.ToString());
        }

        public IEnumerable<MovimentosDaOperacao> GetListPorReferencia(string referencia)
        {
            MovimentosDaOperacao m = new MovimentosDaOperacao();

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<MovimentosDaOperacao> GetListPorDescricao(string desc)
        {
            MovimentosDaOperacao m = new MovimentosDaOperacao();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<MovimentosDaOperacao> GetListById(int id)
        {
            MovimentosDaOperacao m = new MovimentosDaOperacao();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1");
        }
    }
}
