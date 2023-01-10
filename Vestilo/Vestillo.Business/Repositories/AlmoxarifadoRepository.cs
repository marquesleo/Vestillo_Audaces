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
    public class AlmoxarifadoRepository : GenericRepository<Almoxarifado>
    {
        public AlmoxarifadoRepository()
            : base(new DapperConnection<Almoxarifado>())
        {
        }
        
        public IEnumerable<Almoxarifado> GetListPorDescricao(string Descricao)
        {
            var SQL = new Select()
                .Campos("* ")
                .From("almoxarifados ")
                .Where(" descricao like '%" + Descricao + "%' AND " + FiltroEmpresa("IdEmpresa"));

            var cr = new Almoxarifado();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString());
        }

        public IEnumerable<Almoxarifado> GetListPorEmpresa(int IdEmpresa)
        {
            var SQL = new Select()
                .Campos("* ")
                .From("almoxarifados ")
                .Where(" idempresa = " + IdEmpresa)
                .OrderBy(" abreviatura");
            var cr = new Almoxarifado();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString());
        }

        public IEnumerable<Almoxarifado> GetByEmpresa()
        {
            var SQL = new Select()
                .Campos("* ")
                .From("almoxarifados ")
                .Where( FiltroEmpresa("IdEmpresa") )
                .OrderBy(" abreviatura");
            var cr = new Almoxarifado();
            return _cn.ExecuteStringSqlToList(cr, SQL.ToString());
        }
    }
}