

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
    public class SerieNfceRepository : GenericRepository<SerieNfce>
    {
        public SerieNfceRepository()
            : base(new DapperConnection<SerieNfce>())
        {
        }

        public SerieNfce  GetByNumeracao(int SerieNota)
        {
                        
            var Serie = new SerieNfce();
            _cn.ExecuteToModel(" Serie = " + SerieNota , ref Serie);
            return Serie;

        }
    }
}
