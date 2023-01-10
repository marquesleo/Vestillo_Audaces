﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class PaisRepository:GenericRepository<Pais>
    {
        public PaisRepository(): base(new DapperConnection<Pais>())
        {
        }
    }
}
