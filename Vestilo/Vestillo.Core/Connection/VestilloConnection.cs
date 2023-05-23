using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Connection;
using System.Data;

namespace Vestillo.Core.Connection
{
    public static class VestilloConnection
    {
        public static void Insert<TModel>(ref TModel entity) where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            cn.ExecuteInsert(ref entity);
        }

        public static void Update<TModel>(ref TModel entity) where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            cn.ExecuteUpdate(entity);
        }

        public static void Delete<TModel>(int id) where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            TModel entity = InstanciateT<TModel>();
            cn.ExecuteDelete(entity, id);
        }

        public static TModel Find<TModel>(int id) where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            TModel entity = InstanciateT<TModel>();
            cn.ExecuteToModel(ref entity, id);
            return entity;
        }

        public static IEnumerable<TModel> ListAll<TModel>() where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            TModel entity = InstanciateT<TModel>();
            return cn.ExecuteToList(entity);
        }

        public static IEnumerable<TModel> ExecSQLToList<TModel>(string sql) where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            TModel entity = InstanciateT<TModel>();
            return cn.ExecuteStringSqlToList(entity, sql);
        }

        public static IEnumerable<TModel> ExecSQLToListWithNewConnection<TModel>(string sql) where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            TModel entity = InstanciateT<TModel>();
            return cn.ExecuteStringSqlToList(entity, sql, true);
        }

        public static IEnumerable<TModel> ExecSQLToList<TModel>(string sql, out int count) where TModel : class
        {
            count = 0;
            var cn = new DapperConnection<TModel>();
            TModel entity = InstanciateT<TModel>();
            return cn.ExecuteStringSqlToList(entity, sql, out count);
        }

        public static TModel ExecSQLToModel<TModel>(string sql) where TModel : class
        {
            var cn = new DapperConnection<TModel>();
            TModel entity = InstanciateT<TModel>();
            cn.ExecuteToModel(ref entity, sql);
            return entity;
        }

        public static void ExecNonQuery(string sql) 
        {
            var cn = new DapperConnection<String>();
            cn.ExecuteNonQuery(sql);
        }

        public static DataTable ExecToDataTable(string sql)
        {
             var cn = new DapperConnection<String>();
             return cn.ExecuteToDataTable(sql);
        }

        private static T InstanciateT<T>() where T : class
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
