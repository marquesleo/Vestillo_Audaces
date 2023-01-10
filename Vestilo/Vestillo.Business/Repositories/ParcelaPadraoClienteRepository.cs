using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Connection;

namespace Vestillo.Business.Repository
{
    public class ParcelaPadraoClienteRepository : GenericRepository<ParcelaPadraoCliente>
    {
        public ParcelaPadraoClienteRepository()
            : base(new DapperConnection<ParcelaPadraoCliente>())
        {
        }

        public void DeleteAllPorColaborador(int id)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("DELETE ");
            SQL.AppendLine("FROM    parcelapadraocliente");
            SQL.AppendLine("WHERE 	colaboradorId = " + id);

            _cn.ExecuteNonQuery(SQL.ToString());
        }

        public IEnumerable<ParcelaPadraoCliente> GetParcelasPorCliente(int clienteId)
        {

            var cn = new DapperConnection<ParcelaPadraoCliente>();

            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT 	*");
            SQL.AppendLine("FROM 	parcelapadraocliente");
            SQL.AppendLine("WHERE colaboradorId = " + clienteId);

            return cn.ExecuteStringSqlToList(new ParcelaPadraoCliente(), SQL.ToString());

        }
    }
}
