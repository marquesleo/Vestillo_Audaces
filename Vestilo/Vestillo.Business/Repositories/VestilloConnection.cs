using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using Vestillo.Lib;
using Dapper;
using DapperExtensions;

namespace Vestillo.Business.Repositories
{
    public class VestilloConnection  : IDisposable
    {
        public enum Sistema
        {
            Novo = 1,
            Antigo = 2
        }

        public static VestilloConnection Conexao
        {
            get
            {
                if (_conexaoVestilloNovo == null)
                {
                    _conexaoVestilloNovo = new VestilloConnection(Sistema.Novo);
                }

                return _conexaoVestilloNovo;
            }
        }

        public static VestilloConnection ConexaoSistemaAntigo
        {
            get
            {
                if (_conexaoVestilloAntigo == null)
                {
                    _conexaoVestilloAntigo = new VestilloConnection(Sistema.Antigo);
                }

                return _conexaoVestilloAntigo;
            }
        }

        protected static VestilloConnection _conexaoVestilloNovo = null;
        protected static VestilloConnection _conexaoVestilloAntigo = null;
        protected string _cs;
        protected IDbConnection _cn = null;
        protected IDbTransaction _transaction = null;
        protected Sistema _sistema;
        //public enumSGBD SGBD { get; set; }

        public VestilloConnection(Sistema sitema)
        {
            string session;

            if (sitema ==  Sistema.Novo)
                session = "config";
            else
                session = "configVestilloAntigo";

            IniParser ini = new IniParser("Vestillo.ini");
            _sistema = sitema;

            Cripto cripto = new Cripto();

            string cs = cripto.Decrypt(ini.GetSetting(session, "cs"));
            string pw = cripto.Decrypt(ini.GetSetting(session, "pw"));
            string db = cripto.Decrypt(ini.GetSetting(session, "database"));

            _cs = string.Format(string.Format(cs, db, pw));
        }
        
        public IEnumerable<TModel> ExecSQL<TModel>(string sql) where TModel : class
        {
            try
            {
                OpenConnection();
                var result = _cn.Query<TModel>(sql, null, _transaction);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        
        }

        public DataTable ExecSQL(string sql) 
        {
            try
            {
                OpenConnection();
                DataTable dt = new DataTable();
                var cn = (_cn as MySql.Data.MySqlClient.MySqlConnection);
                new MySqlDataAdapter(sql, cn).Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
         
        }

        public int Exec<TModel>(string sql, TModel model) where TModel : class
        {
            try
            {
                OpenConnection();
                return _cn.Execute(sql, model, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Exec(string sql) 
        {
            try
            {
                OpenConnection();
                _cn.Execute(sql, null, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public int LastInsertedId()
        {
            string sql = "SELECT (LAST_INSERT_ID()) AS ID";
            DataTable dt = ExecSQL(sql);

            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["ID"]);

            return 0;
        }

        public bool BeginTransaction()
        {
            if (_transaction == null)
            {
                OpenConnection();
                _transaction = _cn.BeginTransaction(IsolationLevel.ReadUncommitted);
                return true;
            }

            return false;
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
            CloseConnection();
        }

        public void RollBackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
                CloseConnection();
            }
        }

        protected void OpenConnection()
        {
            
            if (_transaction != null)
                return;

            CloseConnection();

            if (_cn == null)
            {
                _cn = new MySqlConnection(_cs);
            }

            _cn.Open();
        }

        protected void CloseConnection()
        {
            try
            {
                if (_transaction != null)
                    return;

                if (_cn != null)
                {
                    _cn.Close();
                    _cn.Dispose();
                }
            }
            finally 
            {
                _cn = null;
            }
        }

        public void Dispose()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Dispose();

                CloseConnection();
            }
            finally 
            {
                _transaction = null;
                _cn = null;
            }
        }
    }
}
