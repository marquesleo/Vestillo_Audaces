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
    public class ParametroRepository : GenericRepository<Parametro>
    {
        public ParametroRepository()
            : base(new DapperConnection<Parametro>())
        {
        }

        public Parametro GetByChave(string chave)
        {
            var param = new Parametro();
            _cn.ExecuteToModel("Chave = '" + chave + "'", ref param);
            return param;
        }

        public IEnumerable<Parametro> GetAllVisaoCliente()
        {
            var SQL = new Select()
                .Campos("*")
                .From("parametros ")
                .Where(" VisaoCliente = 1 " + " AND " + FiltroEmpresa("EmpresaId"));

            var col = new Parametro();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());
        }

        public string EstacaoXml(int IdEmpresa)
        {
            var SQL = String.Empty;
            var Estacao = String.Empty;

            SQL = "SELECT * FROM parametros WHERE parametros.Chave =  'UNIDADE_MAPEADA_XML_NA_RE' " + " AND parametros.EmpresaId = " + IdEmpresa;

            var cn = new DapperConnection<Parametro>();
            var par = new Parametro();
            var dados = cn.ExecuteStringSqlToList(par, SQL);

            if (dados != null)
            {
                foreach (var item in dados)
                {
                    Estacao = item.Valor;
                }
            }

            return Estacao;
        }

    }
}