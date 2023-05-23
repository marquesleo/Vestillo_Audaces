﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class ClienteService : GenericService<Cliente, ClienteRepository>
    {
        public ClienteDadosFinanceiroView GetDadosFinanceiro(int clienteId)
        {
            return _repository.GetDadosFinanceiro(clienteId);
        }
        
        public IEnumerable<ClienteCarteiraView> ListDadosCarteiraClientes(int[] vendedores, int tipoConsulta)
        {
            return _repository.ListDadosCarteiraClientes(vendedores, tipoConsulta);
        }

    }
}

