using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using Vestillo.Lib;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace Vestillo.Connection
{
    public class ProviderFactory : IDisposable
    {
        public static bool IsAPI => (!string.IsNullOrEmpty(StringConnection));//Alterado para Audaces, testar
        public static string StringConnection { get; set; }

        public enum enumSGBD
        {
            Mysql = 1,
            Sql_Server = 2
        }

        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        private IDbConnection _cn;
        private IDbTransaction _transaction = null;
        private static ProviderFactory _provider = null;
        public enumSGBD SGBD { get; set; }

        public static ProviderFactory Provider()
        {
            if (_provider == null)
            {
                _provider = new ProviderFactory();
            }
            return _provider;
        }

        public ProviderFactory()
        {
            if (!IsAPI)//Alterado para Audaces, testar
            {
                var cripto = new Cripto();

                var cs = cripto.Decrypt(Funcoes.LerConfiguracao("cs"));
                var provider = Funcoes.LerConfiguracao("provider");
                var servidor = Funcoes.LerConfiguracao("database");
                var pw = cripto.Decrypt(Funcoes.LerConfiguracao("pw"));

                this.ConnectionString = string.Format(cs, servidor, pw);
                this.ProviderName = cripto.Decrypt(provider);

                switch (this.ProviderName)
                {
                    case "Mysql.Data.MysqlClient":
                        this.SGBD = enumSGBD.Mysql;
                        break;
                    case "System.Data.SqlClient":
                        this.SGBD = enumSGBD.Sql_Server;
                        break;
                    default:
                        this.SGBD = enumSGBD.Mysql;
                        break;

                }
            }
            else
            {
                this.ConnectionString = StringConnection;
                this.SGBD = enumSGBD.Mysql;
                this.ProviderName = "Mysql.Data.MysqlClient";
            }

        }

        public IDbConnection CreateConnection()
        {
            switch (this.SGBD)
            {
                case enumSGBD.Mysql:
                    return new MySqlConnection(this.ConnectionString);
                case enumSGBD.Sql_Server:
                    return new SqlConnection(this.ConnectionString);
                default:
                    return new MySqlConnection(this.ConnectionString);
            }
        }

        public IDbConnection Connection()
        {
            if (_cn == null)
                OpenConnection();

            return _cn;
        }

        public bool EmProcessamento()
        {
            if (_transaction != null || _cn != null)
                return true;

            return false;
        }

        public void CloseConnection()
        {
            bool matarConexao = true;

            if (_transaction == null)
            {
                if (_cn != null)
                {
                    try
                    {
                        if (_cn.State == ConnectionState.Executing)
                        {
                            matarConexao = false;
                            return;
                        }

                        _cn.Close();
                        _cn.Dispose();
                    }
                    finally
                    {
                        if (matarConexao)
                            _cn = null;
                    }
                }
            }
        }

        public void OpenConnection()
        {
            this.CloseConnection();

            if (_cn == null) // alterado audaces testar
            {
                if (!IsAPI)
                    _cn = this.CreateConnection();
                else
                    _cn = new MySqlConnection(this.ConnectionString);
                _cn.Open();
                return;
            }


        }

        public IDbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public int TransactionCount
        {
            get
            {
                return _transaction != null ? 1 : 0;
            }
        }

        public bool BeginTransaction()
        {

            if (_cn == null)
            {
                this.OpenConnection();
            }

            if (_transaction == null)
            {
                _transaction = _cn.BeginTransaction();
                return true;
            }

            return false;
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }

            this.CloseConnection();
        }

        public void RollbackTransaction()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
            finally
            {
                _transaction = null;
                CloseConnection();
            }
        }

        public void Dispose()
        {
            if (_cn != null)
            {
                this.RollbackTransaction();
                this.CloseConnection();
            }
        }
    }
}
