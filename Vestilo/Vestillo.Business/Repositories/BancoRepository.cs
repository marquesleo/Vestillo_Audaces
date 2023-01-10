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
    public class BancoRepository: GenericRepository<Banco>
    {
        public BancoRepository()
            : base(new DapperConnection<Banco>())
        {
        }

        public IEnumerable<Banco> GetPorNumBanco(string numBanco)
        {
            Banco m = new Banco();

            return _cn.ExecuteToList(m, "numbanco like '%" + numBanco + "%' And ativo = 1 ");
        }

        public IEnumerable<Banco> GetPorDescricao(string desc)
        {
            Banco m = new Banco();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1 ");
        }

        public IEnumerable<Banco> GetByIdList(int id)
        {
            Banco m = new Banco();
            return _cn.ExecuteToList(m, "id =" + id);
        }

        public IEnumerable<Banco> GetAllAtivos()
        {
            Banco m = new Banco();
            return _cn.ExecuteStringSqlToList(m, "SELECT * FROM Bancos WHERE ativo = 1 AND " + FiltroEmpresa("Bancos.idempresa") + "ORDER BY Descricao;");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdBanco"></param>
        /// <param name="Tipo">Tipo 1 crédito 2 Débito</param>
        /// <param name="Valor"></param>
        public void UpdateSaldo(int IdBanco, int Tipo,decimal Valor) // Tipo 1 crédito 2 Débito JAMAICA
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("UPDATE bancos SET ");
            SQL.AppendLine("saldo = ");
            if (Tipo == 1)
            {
                SQL.Append(" saldo + " + Valor.ToString().Replace(",","."));
            }
            else
            {
                SQL.Append(" saldo - " + Valor.ToString().Replace(",", "."));
            }           
            SQL.AppendLine(" WHERE id = ");
            SQL.Append(IdBanco);
            _cn.ExecuteNonQuery(SQL.ToString());
        }


        public IEnumerable<Banco> GetAllParaBoleto()
        {
            Banco m = new Banco();
            return _cn.ExecuteStringSqlToList(m, "SELECT * FROM Bancos WHERE ativo = 1 AND IFNULL(NomeBanco,0) > 0  AND " + FiltroEmpresa("Bancos.idempresa") + "ORDER BY Descricao;");
        }

        public Banco GetPadraoVenda()
        {
            Banco m = new Banco();
            _cn.ExecuteToModel( ref m, "SELECT * FROM Bancos WHERE ativo = 1 AND PadraoParaVendas = 1 AND " + FiltroEmpresa("Bancos.idempresa") + "ORDER BY Id LIMIT 1;");
            return m;
        }
    }
}
