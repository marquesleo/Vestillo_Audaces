using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Connection;
using Vestillo.Lib;

namespace Vestillo.Business.Repositories
{
    public class BorderoCobrancaDocumentoRepository: GenericRepository<BorderoCobrancaDocumento>
    {
        public BorderoCobrancaDocumentoRepository()
            : base(new DapperConnection<BorderoCobrancaDocumento>())
        {

        }

        public void DeleteByBordero(int borderoCobrancaId)
        {
            string SQL = "DELETE FROM BorderoCobrancaDocumentos WHERE BorderoCobrancaId = " + borderoCobrancaId.ToString();
            _cn.ExecuteNonQuery(SQL);
        }

        public IEnumerable<BorderoCobrancaDocumento> GetByBordero(int borderoCobrancaId)
        {
            return _cn.ExecuteToList(new BorderoCobrancaDocumento(), "BorderoCobrancaId = " + borderoCobrancaId.ToString());
        }
    }
}
