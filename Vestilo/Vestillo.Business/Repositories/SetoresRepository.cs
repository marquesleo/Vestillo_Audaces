
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
    public class SetoresRepository : GenericRepository<Setores>
    {
        public SetoresRepository()
            : base(new DapperConnection<Setores>())
        {
        }

        public IEnumerable<Setores> GetByAtivos(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("setores ")
                .Where(" Ativo = " + AtivoInativo);

            var tm = new Setores();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }

       

        public IEnumerable<Setores> GetListPorReferencia(string Abreviatura)
        {
            Setores m = new Setores();

            return _cn.ExecuteToList(m, "Abreviatura like '%" + Abreviatura + "%' And ativo = 1");
        }

        public IEnumerable<Setores> GetListPorDescricao(string desc)
        {
            Setores m = new Setores();
            return _cn.ExecuteToList(m, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Setores> GetListById(int id)
        {
            Setores m = new Setores();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1");
        }


        public IEnumerable<Setores> GetByBalanceamentos()
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("setores ")
                .Where(" Ativo = 1 AND Balanceamento = 1");

            var tm = new Setores();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }


        public IEnumerable<Setores> GetByAtivosBalanceamento(int AtivoInativo)
        {
            var SQL = new Select()
                .Campos("Id,Abreviatura,Descricao,Ativo ")
                .From("setores ")
                .Where(" Balanceamento = 1 AND Ativo = " + AtivoInativo);

            var tm = new Setores();
            return _cn.ExecuteStringSqlToList(tm, SQL.ToString());
        }      


        public IEnumerable<Setores> GetListPorReferenciaBalanceamento(string Abreviatura)
        {
            Setores m = new Setores();

            return _cn.ExecuteToList(m, "Balanceamento = 1 AND Abreviatura like '%" + Abreviatura + "%' And ativo = 1");
        }

        public IEnumerable<Setores> GetListPorDescricaoBalanceamento(string desc)
        {
            Setores m = new Setores();
            return _cn.ExecuteToList(m, "Balanceamento = 1 AND descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Setores> GetListByIdBalanceamento(int id)
        {
            Setores m = new Setores();
            return _cn.ExecuteToList(m, "id = " + id + " And ativo = 1 AND Balanceamento = 1 ");
        }

        public Setores SetorParaFicha(string Referencia)
        { 
            string  SQL = String.Empty;
            SQL = "Select * from setores where setores.Abreviatura = " + "'" + Referencia + "'";            

            var cn = new DapperConnection<Setores>();
            var ret = new Setores();
            cn.ExecuteToModel(ref ret, SQL.ToString());           
            return ret;
        }


    }
}
