﻿

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
    public class NfceUpdateRepository : GenericRepository<NfceUpdate>
    {
        public NfceUpdateRepository() : base(new DapperConnection<NfceUpdate>())
        {
        }
    }
}