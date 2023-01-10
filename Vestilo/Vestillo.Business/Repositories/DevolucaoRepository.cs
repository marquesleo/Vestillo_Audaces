using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class DevolucaoRepository : GenericRepository<Devolucao>
    {
        public DevolucaoRepository() : base(new DapperConnection<Devolucao>())
        {
        }

        //para preencher o grid da tela de browse
        public IEnumerable<DevolucaoView> GetAllView()
        {
            var cn = new DapperConnection<DevolucaoView>();
            var p = new DevolucaoView();
            

            var SQL = new Select()
                .Campos("n.Id, n.referencia, n.IdCliente as IdCliente,cli.razaosocial as NomeCliente, n.idvendedor,  ven.nome as NomeVendedor, " +
                " tab.Descricao As DescTabela, n.* ")
                .From("devolucao n")
                .LeftJoin("colaboradores cli", "cli.id = n.IdCliente")
                .LeftJoin("colaboradores ven", "ven.id = n.idVendedor")                
                .LeftJoin("tabelaspreco tab", "tab.id = n.idtabela")
                .Where(FiltroEmpresa("n.Idempresa")); 


            return cn.ExecuteStringSqlToList(p, SQL.ToString());
        }
    }
}
