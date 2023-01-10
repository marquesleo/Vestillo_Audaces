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
    public class CfopRepository: GenericRepository<Cfop>
    {
        public CfopRepository()
            : base(new DapperConnection<Cfop>())
        {
        }

        public IEnumerable<Cfop> GetPorReferencia(String referencia, String TipoCfop = "ENTRADA")
        {
            Cfop m = new Cfop();

            if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("ENTRADA"))
            {
                return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And (referencia >= '1101' and referencia <= '3949') order by referencia");
            } if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("SAIDA"))
            {
                return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%' And (referencia >= '5101' and referencia <= '7949') order by referencia");
            }

            return _cn.ExecuteToList(m, "referencia like '%" + referencia + "%'");
        }

        public IEnumerable<Cfop> GetPorDescricao(String desc, String TipoCfop)
        {
            Cfop m = new Cfop();

            if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("ENTRADA"))
            {
                return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And (referencia >= '1101' and referencia <= '3949') order by referencia");
            } if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("SAIDA"))
            {
                return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And (referencia >= '5101' and referencia <= '7949') order by referencia");
            }

            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%'");
        }
    }
}
