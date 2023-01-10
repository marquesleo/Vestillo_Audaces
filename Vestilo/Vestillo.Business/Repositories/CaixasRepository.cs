
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
    public class CaixasRepository : GenericRepository<Caixas>
    {
        public CaixasRepository() : base(new DapperConnection<Caixas>())
        {
        }

        public IEnumerable<Caixas> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos(" id,referencia,descricao,dataultabertura ,dataultfechamento,,Ativo,saldo   ")
                .From("caixas ")
                .Where(" Ativo = " + AtivoInativo + FiltroEmpresa());

            var cx = new Caixas();
            return _cn.ExecuteStringSqlToList(cx, SQL.ToString());
        }

        public IEnumerable<Caixas> GetListPorReferencia(string Referencia)
        {
            Caixas cx = new Caixas();

            return _cn.ExecuteToList(cx, "referencia like '%" + Referencia + "%' And ativo = 1" + FiltroEmpresa());
        }

        public IEnumerable<Caixas> GetListPorDescricao(string desc)
        {
            Caixas cx = new Caixas();
            return _cn.ExecuteToList(cx, "descricao like '%" + desc + "%' And ativo = 1" + FiltroEmpresa());
        }

        public IEnumerable<Caixas> GetListById(int id)
        {
            Caixas cx = new Caixas();
            return _cn.ExecuteToList(cx, "id = " + id + " And ativo = 1");
        }

        public IEnumerable<Caixas> GetAllTrataHoras()
        {
            var SQL = new Select()
                .Campos(" id,referencia,descricao,dataultabertura ,dataultfechamento,Ativo,saldo  ")
                .From("caixas ")
                .Where(FiltroEmpresa());
            var cx = new Caixas();
            return _cn.ExecuteStringSqlToList(cx, SQL.ToString());
        }


        public Caixas GetByIdTrataHoras(int id)
        {
            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT id,referencia,descricao,dataultabertura ,dataultfechamento,Ativo,saldo  ");
            SQL.AppendLine(" FROM caixas ");
            SQL.AppendLine(" WHERE caixas.Id = " + id);

            var cn = new DapperConnection<Caixas>();
            var ret = new Caixas();
            cn.ExecuteToModel(ref ret, SQL.ToString());
            return ret;
        }

    }
}

