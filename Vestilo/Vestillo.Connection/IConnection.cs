using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using System.Data;

namespace Vestillo.Connection
{
    
    public interface IConnection<TModel>
    {
        ProviderFactory Provider { get; }
        void ExecuteToModel(ref TModel entity, int id);
        void ExecuteToModel(ref TModel entity, string sql);
        void ExecuteToModel(string where, ref TModel entity);        
        IEnumerable<TModel> ExecuteToList(TModel entity, string where);
        IEnumerable<TModel> ExecuteToList(TModel entity);
        IEnumerable<TModel> ExecuteToList(TModel entity, int[] ids);
        IEnumerable<TModel> ExecuteStringSqlToList(TModel entity, string sql);
        IEnumerable<TModel> ExecuteStringSqlToList(TModel entity, string sql, out int qtdTotalRegistros);
        void ExecuteUpdate(TModel entity);
        void ExecuteUpdate(TModel entity, string SQL);
        void ExecuteInsert(ref TModel entity);
        void ExecuteDelete(TModel entity, int id);
        void ExecuteNonQuery(string sql);
        DataTable ExecuteToDataTable(string sql);
    }
}
