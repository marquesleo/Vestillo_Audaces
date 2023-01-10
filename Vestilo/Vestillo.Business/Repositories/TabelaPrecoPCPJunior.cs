using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class TabelaPrecoPCPJuniorRepository : GenericRepository<TabelaPrecoPCPJunior>
    {
        public TabelaPrecoPCPJuniorRepository()
            : base(new DapperConnection<TabelaPrecoPCPJunior>())
        {
        }

        public IEnumerable<TabelaPrecoPCPJuniorView> GetAllView()
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT p.id as IdProduto, p.Custo, p.Referencia, p.Descricao, g.abreviatura as Grupo, t.*");
            SQL.AppendLine("FROM produtos p");
            SQL.AppendLine("LEFT JOIN tabelaprecopcpjunior t ON t.IdProduto = p.id");
            SQL.AppendLine("LEFT JOIN grupoprodutos g ON p.IdGrupo = g.id");
            SQL.AppendLine("WHERE p.TipoItem = 0 AND p.Ativo = 1");

            var cn = new DapperConnection<TabelaPrecoPCPJuniorView>();
            return cn.ExecuteStringSqlToList(new TabelaPrecoPCPJuniorView(), SQL.ToString());
        }
    }
}
