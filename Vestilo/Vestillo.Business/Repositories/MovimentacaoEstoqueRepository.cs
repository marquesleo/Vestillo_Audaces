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
    public class MovimentacaoEstoqueRepository: GenericRepository<MovimentacaoEstoque>
    {
        public MovimentacaoEstoqueRepository()
            : base(new DapperConnection<MovimentacaoEstoque>())
        {
        }
    }
}
