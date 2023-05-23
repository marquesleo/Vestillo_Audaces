using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Reflection;
using System.Data;
using Vestillo;
using Vestillo.Lib;
using DapperExtensions;

namespace Vestillo.Connection
{
    public class DapperConnection<TModel> : IConnection<TModel>
    {
        private ProviderFactory _provider;

        public ProviderFactory Provider
        {
            get
            {
                return _provider;
            }
        }

        public DapperConnection()
        {
            _provider = ProviderFactory.Provider();
        }

        public void ExecuteToModel(ref TModel entity, int id)
        {
            try
            {
                entity = _provider.Connection().Query<TModel>(CreateStringSqlSelectById(entity, new int[] { id }), null, _provider.Transaction).FirstOrDefault();
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void ExecuteToModel(string where, ref TModel entity)
        {
            try
            {
                 entity = _provider.Connection().Query<TModel>(CreateStringSqlSelect(entity, where), null, _provider.Transaction).FirstOrDefault();
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void ExecuteToModel(ref TModel entity, string sql)
        {
            try
            {
                entity = _provider.Connection().Query<TModel>(sql, null, _provider.Transaction).FirstOrDefault();
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public IEnumerable<TModel> ExecuteToList(TModel entity, string where)
        {
            try
            {
                var ret = _provider.Connection().Query<TModel>(CreateStringSqlSelect(entity, where), null, _provider.Transaction).ToList();
                _provider.CloseConnection();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public IEnumerable<TModel> ExecuteToList(TModel entity)
        {
            try
            {
                var ret = _provider.Connection().Query<TModel>(CreateStringSqlSelect(entity, ""), null, _provider.Transaction).ToList();
                _provider.CloseConnection();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public IEnumerable<TModel> ExecuteToList(TModel entity, int[] ids)
        {
            try
            {
                var ret = _provider.Connection().Query<TModel>(CreateStringSqlSelectById(entity, ids), null, _provider.Transaction).ToList();
                _provider.CloseConnection();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public IEnumerable<TModel> ExecuteStringSqlToList(TModel entity, string sql)
        {
            try
            {
                var ret = _provider.Connection().Query<TModel>(sql, null, _provider.Transaction,false, int.MaxValue, CommandType.Text).ToList();
                _provider.CloseConnection();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public IEnumerable<TModel> ExecuteStringSqlToList(TModel entity, string sql, bool novaConexao)
        {
            try
            {
                if (novaConexao)
                {
                    IDbConnection cn = null;

                    try
                    {
                        cn = _provider.CreateConnection();
                        cn.Open();
                        var ret = cn.Query<TModel>(sql, null, null, false, int.MaxValue, CommandType.Text).ToList();
                        cn.Close();
                        cn.Dispose();

                        return ret;
                    }
                    catch (Exception ex)
                    {
                        if (cn != null)
                        {
                            cn.Close();
                            cn.Dispose();
                        }
                        throw ex;
                    }
                }
                else
                {
                    var ret = _provider.Connection().Query<TModel>(sql, null, _provider.Transaction, false, int.MaxValue, CommandType.Text).ToList();
                    _provider.CloseConnection();
                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(!novaConexao)
                    _provider.CloseConnection();
            }
        }


        public IEnumerable<TModel> ExecuteStringSqlToList(TModel entity, string sql, out int qtdTotalRegistros)
        {
            try
            {
                IEnumerable<TModel> ret;
                long qtd = 0;

                using (var multi = _provider.Connection().QueryMultiple(sql, null, _provider.Transaction))
                {
                    ret = multi.Read<TModel>().ToList();
                    qtd = multi.Read<long>().Single();
                    _provider.CloseConnection();
                }

                qtdTotalRegistros = Convert.ToInt32(qtd);

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void ExecuteUpdate(TModel entity)
        {
            try
            {
                //this.VerificaRegistroUnico(entity); ALEX 24-06-2022
                _provider.Connection().Execute(CreateStringSqlUpdate(entity), entity, _provider.Transaction);
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void ExecuteUpdate(TModel entity, string SQL)
        {
            try
            {
                
#if DEBUG
                //this.VerificaRegistroUnico(entity);
                System.Diagnostics.Debug.WriteLine("===================================================================================");
                System.Diagnostics.Debug.WriteLine(SQL);
                System.Diagnostics.Debug.WriteLine("===================================================================================");

#endif

                _provider.Connection().Execute(SQL, entity, _provider.Transaction);
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void ExecuteInsert(ref TModel entity)
        {
            try
            {
               this.VerificaRegistroUnico(entity);
            
                if (_provider.SGBD == ProviderFactory.enumSGBD.Mysql)
                {
                    var id = _provider.Connection().Query<ulong>(CreateStringSqlInsert(entity), entity, _provider.Transaction).Single();
                    this.UpdateIdPropertyModel(ref entity, (int)id);
                }
                else
                {
                    var id = _provider.Connection().Query<int>(CreateStringSqlInsert(entity), entity, _provider.Transaction).Single();
                    this.UpdateIdPropertyModel(ref entity, id);
                }
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void ExecuteDelete(TModel entity, int id)
        {
            try
            {
                _provider.Connection().Execute(CreateStringSqlDelete(entity, id), entity, _provider.Transaction);
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void ExecuteNonQuery(string sql)
        {
            try
            {
                _provider.Connection().Execute(sql, null, _provider.Transaction);
                _provider.CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _provider.CloseConnection();
            }
        }

        public void VerificaRegistroUnico(TModel entity)
        {
            string [] mensagens = null;
            string[] lstSql = CreateStringSqlSelectExists(entity, ref mensagens);

            if (lstSql != null)
            {
                var mensagem = new StringBuilder();
                int index = 0;
                bool campoRepetido = false;

                foreach (var sql in lstSql)
                {
                    var reg = _provider.Connection().Query<TModel>(sql, entity, _provider.Transaction).FirstOrDefault();

                    if (reg != null)
                    {
                        mensagem.Append(mensagens[index]);
                        campoRepetido = true;
                    }
                    index ++;
                }

                if (campoRepetido)
                {
                    _provider.CloseConnection();
                    throw new Vestillo.Lib.VestilloException(Enum_Tipo_VestilloNet_Exception.Registro_Duplicado, mensagem.ToString());
                }
            }
        }

        private string CreateStringSqlSelect(TModel entity, string where)
        {
            var sql = new StringBuilder();
            string orderBy = "";
            string filtroEmpresa = "";
            
            bool naoMapeado = false;
            PropertyInfo[] properties = entity.GetType().GetProperties();



            sql.AppendLine("SELECT ");
            foreach (var propertyInfo in properties)
            {
                foreach (NaoMapeado customAttribute in propertyInfo.GetCustomAttributes(typeof(NaoMapeado), false))
                {
                    if (!customAttribute.SomenteInsert)
                    {
                        naoMapeado = true;
                        break;
                    }
                }

                if (!naoMapeado)
                {
                    foreach (FiltroEmpresa customAttribute in propertyInfo.GetCustomAttributes(typeof(FiltroEmpresa), false))
                    {
                        filtroEmpresa = GetFiltroEmpresa(entity);
                        break;
                    }

                    foreach (OrderByColumn customAttribute in propertyInfo.GetCustomAttributes(typeof(OrderByColumn), false))
                    {
                        orderBy = " ORDER BY " + propertyInfo.Name + (customAttribute.Desc ? " DESC" : "");
                        break;
                    }

                    sql.AppendLine(propertyInfo.Name);
                    sql.Append(", ");
                }
                naoMapeado = false;
            }

            sql.Remove(sql.ToString().Length - 2, 2);
            sql.AppendLine(" FROM ");

            string nomeTabela = entity.GetType().Name;

            foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
            {
                nomeTabela = table.NomeTabela;
                break;
            }

            sql.Append(nomeTabela);

            if (!string.IsNullOrEmpty(where))
            {
                sql.AppendLine(" WHERE ");
                sql.Append(where);

                if (!string.IsNullOrEmpty(filtroEmpresa))
                    sql.AppendLine(" AND ");
            }
            else
            {
                if (!string.IsNullOrEmpty(filtroEmpresa))
                    sql.AppendLine(" WHERE ");
            }
           
            sql.Append(filtroEmpresa);
            sql.Append(orderBy);

            return sql.ToString();
        }

        private string GetFiltroEmpresa(TModel entity)
        {
            string filtroEmpresa = "";
            string tabela  = "";

            Type entityType = entity.GetType();

            PropertyInfo[] properties = entityType.GetProperties();

            foreach (Tabela table in entityType.GetCustomAttributes(typeof(Tabela), false))
            {
                tabela = table.NomeTabela;
                break;
            }

            foreach (var propertyInfo in properties)
            {
                foreach (FiltroEmpresa customAttribute in propertyInfo.GetCustomAttributes(typeof(FiltroEmpresa), false))
                {
                    var empresas = Vestillo.Lib.Funcoes.EmpresasAcesso.Where(x => x.Value == "" || x.Value.ToLower() == tabela.ToLower()).ToList();
                    filtroEmpresa = " (" + propertyInfo.Name + " IN (" + string.Join(",", empresas.Select(x => x.Key.ToString()).ToArray()) + ") OR " + propertyInfo.Name + " IS NULL " + ")";
                    break;
                }

                if (filtroEmpresa != "")
                    break;
            }

            return filtroEmpresa;
        }

        public string CreateStringSqlSelectById(TModel entity, int[] ids)
        {
            var sql = new StringBuilder();
            var where = new StringBuilder();
            bool naoMapeado = false;
            PropertyInfo[] properties = entity.GetType().GetProperties();


            int id = 0;

            if (ids.Length > 0)
                id = ids[0];

            sql.AppendLine("SELECT ");
            foreach (var propertyInfo in properties)
            {
                foreach (NaoMapeado customAttribute in propertyInfo.GetCustomAttributes(typeof(NaoMapeado), false))
                {
                    if (!customAttribute.SomenteInsert)
                    {
                        naoMapeado = true;
                        break;
                    }
                }

                if (!naoMapeado)
                {
                    sql.AppendLine(propertyInfo.Name);
                    sql.Append(", ");
                }
                naoMapeado = false;

                if (where.Length == 0)
                {
                    foreach (Chave customAttribute in propertyInfo.GetCustomAttributes(typeof(Chave), false))
                    {
                        where.AppendLine(" WHERE ");
                        where.Append(propertyInfo.Name);

                        if (ids.Length > 1)
                        {
                            where.Append(" IN( ");
                            where.Append(string.Join(",", ids));
                            where.Append(")");
                        }
                        else
                        {
                            where.Append(" = ");
                            where.Append(id.ToString());
                        }
                    }

                }
            }

            sql.Remove(sql.ToString().Length - 2, 2);
            sql.AppendLine(" FROM ");

            foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
            {
                sql.Append(table.NomeTabela);
                break;
            }

            sql.Append(where);

#if DEBUG
            System.Diagnostics.Debug.WriteLine("===================================================================================");
            System.Diagnostics.Debug.WriteLine(sql.ToString());
            System.Diagnostics.Debug.WriteLine("===================================================================================");

#endif

            return sql.ToString();
        }

        private string[] CreateStringSqlSelectExists(TModel entity, ref string[] mensagens) // entity = GrupProduto
        {
            var sql = new StringBuilder();
            var where = new StringBuilder();
            var lstWhere = new List<string>();
            var lstRet = new List<string>();
            var lstMensagens = new List<string>();
            string filtroEmpresa = "";
            string tabela = "";
           
            mensagens = null;

            PropertyInfo[] properties = entity.GetType().GetProperties(); // entity Descricao = null

            string sqlId = "";

            foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
            {
                tabela = table.NomeTabela;
                break;
            }

            

            sql.AppendLine("SELECT ");
            foreach (var propertyInfo in properties)
            {
                foreach (Chave customAttribute in propertyInfo.GetCustomAttributes(typeof(Chave), false))
                {
                    sql.AppendLine(propertyInfo.Name);
                    sqlId = " AND (" + propertyInfo.Name + " <> @" + propertyInfo.Name + ")";
                    break;
                }

                foreach (FiltroEmpresa customAttribute in propertyInfo.GetCustomAttributes(typeof(FiltroEmpresa), false))
                {
                    filtroEmpresa = " AND " + GetFiltroEmpresa(entity);
                }

                foreach (RegistroUnico customAttribute in propertyInfo.GetCustomAttributes(typeof(RegistroUnico), false))
                {
                    string PossuiEmpresa = GetFiltroEmpresa(entity);
                    if (!String.IsNullOrEmpty(PossuiEmpresa))
                    {
                        filtroEmpresa = " AND " + GetFiltroEmpresa(entity);
                    }

                    where = new StringBuilder();
                    where.AppendLine("WHERE ");
                    where.Append(propertyInfo.Name);
                    where.Append(" = @");
                    where.Append(propertyInfo.Name);
                    where.AppendLine(filtroEmpresa);
                    where.Append(sqlId);
                    lstWhere.Add(where.ToString());

                    if (!string.IsNullOrEmpty(customAttribute.Mensagem))
                    {
                        lstMensagens.Add(customAttribute.Mensagem + Environment.NewLine);
                    }
                    else
                    {
                        lstMensagens.Add(propertyInfo.Name + " " + propertyInfo.GetValue(entity).ToString() + " já existente na base." + Environment.NewLine);
                    }

                    break;
                }
            }
              
            if (lstWhere.Count() == 0)
            {
                return null;
            }


            if (lstMensagens.Count() != 0)
            {
                mensagens = lstMensagens.ToArray();
            }

            sql.Remove(sql.ToString().Length - 2, 2);
            sql.AppendLine(" FROM ");
            sql.Append(tabela);
         
            foreach (var item in lstWhere)
            {
                  lstRet.Add(sql.ToString() + " " + item);
            }

            return lstRet.ToArray();
        }


        private string CreateStringSqlUpdate(TModel entity)
        {
            var sql = new StringBuilder();
            var where = new StringBuilder();
            bool keyCollumn = false;
            bool naoMapeado = false;

            sql.Append("UPDATE ");
            foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
            {
                sql.Append(table.NomeTabela);
                break;
            }
            sql.Append(" SET ");

            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (where.Length == 0)
                {
                    foreach (Chave customAttribute in propertyInfo.GetCustomAttributes(typeof(Chave), false))
                    {
                        where.AppendLine(" WHERE ");
                        where.Append(propertyInfo.Name);
                        where.Append(" = ");
                        where.Append("@");
                        where.Append(propertyInfo.Name);
                        keyCollumn = true;
                    }
                }
                if (!keyCollumn)
                {
                    foreach (NaoMapeado customAttribute in propertyInfo.GetCustomAttributes(typeof(NaoMapeado), false))
                    {
                        naoMapeado = true;
                        break;
                    }

                    if (!naoMapeado)
                    {
                        bool campoEmpresa = false;
                        
                        foreach (FiltroEmpresa customAttribute in propertyInfo.GetCustomAttributes(typeof(FiltroEmpresa), false))
                        {
                            //if (propertyInfo.GetValue(entity).ToString() ==)
                            //{
                            //    propertyInfo.SetValue(entity, Vestillo.Lib.Funcoes.GetIdEmpresaLogada, null);
                            //}
                            campoEmpresa = true;
                            break;
                        }

                        if (!campoEmpresa)
                        {
                            sql.AppendLine(propertyInfo.Name);
                            sql.Append(" = ");
                            sql.Append("@");
                            sql.Append(propertyInfo.Name);
                            sql.Append(", ");
                        }
                    }
                    naoMapeado = false;
                }
                keyCollumn = false;
            }
            sql.Remove(sql.ToString().Length - 2, 2);
            sql.Append(where);

#if DEBUG
            System.Diagnostics.Debug.WriteLine("===================================================================================");
            System.Diagnostics.Debug.WriteLine(sql.ToString());
            System.Diagnostics.Debug.WriteLine("===================================================================================");

#endif

            return sql.ToString();
        }

        private string CreateStringSqlInsert(TModel entity)
        {
            var sql = new StringBuilder();
            var sqlValues = new StringBuilder();
            bool findIdentityColumn = false;
            bool naoMapeado = false;


            sql.Append("INSERT INTO ");

            string nomeTabela = "";

            foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
            {
                nomeTabela = table.NomeTabela;
                break;
            }

            if (nomeTabela == "")
                nomeTabela = entity.GetType().Name;

            sql.Append(nomeTabela);
            sql.Append(" (");
            sqlValues.Append(" VALUES (");

            PropertyInfo[] properties = entity.GetType().GetProperties();
            
            foreach (var propertyInfo in properties)
            {
                foreach (NaoMapeado customAttribute in propertyInfo.GetCustomAttributes(typeof(NaoMapeado), false))
                {
                    if (!customAttribute.SomenteInsert)
                    {
                        naoMapeado = true;
                        break;
                    }
                }

                foreach (Chave customAttribute in propertyInfo.GetCustomAttributes(typeof(Chave), false))
                {
                    findIdentityColumn = true;
                    break;
                }

                if (!naoMapeado && !findIdentityColumn)
                {
                    foreach (FiltroEmpresa customAttribute in propertyInfo.GetCustomAttributes(typeof(FiltroEmpresa), false))
                    {
                        propertyInfo.SetValue(entity, Vestillo.Lib.Funcoes.GetIdEmpresaLogada , null);
                        break;
                    }

                    foreach (DataAtual customAttribute in propertyInfo.GetCustomAttributes(typeof(DataAtual), false))
                    {
                        propertyInfo.SetValue(entity, DateTime.Now, null);
                        break;
                    }

                    sql.Append(propertyInfo.Name);
                    sql.Append(", ");

                    sqlValues.Append("@");
                    sqlValues.Append(propertyInfo.Name);
                    sqlValues.Append(", ");
                }
                naoMapeado = false;
                findIdentityColumn = false;
            }
            sql.Remove(sql.ToString().Length - 2, 2);
            sqlValues.Remove(sqlValues.ToString().Length - 2, 2);

            sql.Append(")");
            sqlValues.Append(")");

            sql.Append(sqlValues);
            sql.AppendLine(this.CreateStringSqlGetLastInsertId());

#if DEBUG
            System.Diagnostics.Debug.WriteLine("===================================================================================");
            System.Diagnostics.Debug.WriteLine(sql.ToString());
            System.Diagnostics.Debug.WriteLine("===================================================================================");

#endif

            return sql.ToString();
        }

        private string CreateStringSqlDelete(TModel entity, int id)
        {
            var sql = new StringBuilder();
            bool sair = false;

            sql.Append("DELETE FROM ");
            foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
            {
                sql.Append(table.NomeTabela);
                break;
            }
            sql.Append(" WHERE ");

            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {

                foreach (Chave customAttribute in propertyInfo.GetCustomAttributes(typeof(Chave), false))
                {
                    sql.Append(propertyInfo.Name);
                    sql.Append(" = ");
                    sql.Append(id.ToString());
                    sair = true;
                    break;
                }

                if (sair)
                {
                    break;
                }

            }

#if DEBUG
            System.Diagnostics.Debug.WriteLine("===================================================================================");
            System.Diagnostics.Debug.WriteLine(sql.ToString());
            System.Diagnostics.Debug.WriteLine("===================================================================================");

#endif

            return sql.ToString();
        }

        private string CreateStringSqlGetLastInsertId()
        {

            if (_provider.SGBD == ProviderFactory.enumSGBD.Mysql)
            {
                return ";SELECT (LAST_INSERT_ID()) AS ID";
            }
            else if (_provider.SGBD == ProviderFactory.enumSGBD.Sql_Server)
            {
                return ";SELECT CAST(SCOPE_IDENTITY() AS INT)";
            }
            else
            {
                return ";SELECT (LAST_INSERT_ID()) AS ID";
            }

        }

        private void UpdateIdPropertyModel(ref TModel entity, int id)
        {
            bool sair = false;

            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                foreach (Chave customAttribute in propertyInfo.GetCustomAttributes(typeof(Chave), false))
                {
                    propertyInfo.SetValue(entity, id, null);
                    sair = true;
                    break;
                }

                if (sair)
                {
                    break;
                }
            }
        }
        
        public DataTable ExecuteToDataTable(string sql)
        {
            var DT = new DataTable();

            if (_provider.SGBD == ProviderFactory.enumSGBD.Mysql)
            {                
                var CNMy = _provider.Connection() as MySql.Data.MySqlClient.MySqlConnection;
                new MySql.Data.MySqlClient.MySqlDataAdapter(sql, CNMy).Fill(DT);                
            }
            else
            {
                var CNSQL = _provider.Connection() as System.Data.SqlClient.SqlConnection;
                new System.Data.SqlClient.SqlDataAdapter(sql, CNSQL).Fill(DT);
            }

            _provider.CloseConnection();

            return DT;
        }
    }
}   
