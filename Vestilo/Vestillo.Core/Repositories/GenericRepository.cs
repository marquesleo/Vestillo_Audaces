using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Vestillo.Core.Models;
using System.Reflection;
using Vestillo.Core.Connection;

namespace Vestillo.Core.Repositories
{
    public abstract class GenericRepository<TModel> : IDisposable, IRepository<TModel> where TModel : class
    {
        public GenericRepository()
        {
        }

        public virtual void Delete(int id)
        {
            VestilloConnection.Delete<TModel>(id);
        }

        public virtual TModel Find(int id)
        {
            return VestilloConnection.Find<TModel>(id);
        }

        public virtual IEnumerable<TModel> ListAll()
        {
            return VestilloConnection.ListAll<TModel>();
        }

        public virtual void Save(TModel entity)
        {
            if ((entity as IModel).Id > 0)
                VestilloConnection.Update<TModel>(ref entity);
            else
                VestilloConnection.Insert<TModel>(ref entity);
        }

        public bool BeginTransaction()
        {
            return Vestillo.Connection.ProviderFactory.Provider().BeginTransaction();
        }

        public void RollbackTransaction()
        {
            Vestillo.Connection.ProviderFactory.Provider().RollbackTransaction();
        }

        public void CommitTransaction()
        {
            Vestillo.Connection.ProviderFactory.Provider().CommitTransaction();
        }

        public void FillReferencia(ref TModel entity)
        {
            string contador = "";
            string nomeAtributo = "";
            string valorAtributo = "";

            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                foreach (Contador customAttribute in propertyInfo.GetCustomAttributes(typeof(Contador), false))
                {
                    contador = customAttribute.NomeContador;
                    nomeAtributo = propertyInfo.Name;

                    if (propertyInfo.GetValue(entity) != null)
                    {
                        valorAtributo = propertyInfo.GetValue(entity).ToString();
                    }
                    break;
                }

                if (contador != "")
                    break;
            }

            if (string.IsNullOrEmpty(valorAtributo) && contador != "")
            {
                using (ContadorReferenciaRepository contadorReferenciaRepository = new ContadorReferenciaRepository())
                {
                    string contadorAtual = contadorReferenciaRepository.GetNext(contador);
                    entity.GetType().GetProperty(nomeAtributo).SetValue(entity, contadorAtual, null);
                }
            }
        }

        public void Dispose()
        {
            RollbackTransaction();
        }

        protected string FiltroEmpresa(string campoEmpresa = "", string alias = "")
        {
            TModel entity = (TModel)Activator.CreateInstance(typeof(TModel));
            string tabela = "";

            Type entityType = entity.GetType();

            if (campoEmpresa == "")
            {
                PropertyInfo[] properties = entityType.GetProperties();
                foreach (var propertyInfo in properties)
                {
                    foreach (FiltroEmpresa customAttribute in propertyInfo.GetCustomAttributes(typeof(FiltroEmpresa), false))
                    {
                        campoEmpresa = propertyInfo.Name;
                        break;
                    }

                    if (campoEmpresa != "")
                        break;
                }
            }

            foreach (Tabela table in entityType.GetCustomAttributes(typeof(Tabela), false))
            {
                tabela = table.NomeTabela;
                break;
            }

            int empresaLogada = Vestillo.Lib.Funcoes.GetIdEmpresaLogada;

            List<int> empresasAcesso = Vestillo.Lib.Funcoes.EmpresasAcesso.Where(x => x.Value == "" || x.Value.ToLower() == tabela.ToLower()).Select(x => x.Key).ToList();
            
            empresasAcesso.Add(empresaLogada);

            var SQL = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(alias))
            {
                campoEmpresa = string.Concat(alias, ".", campoEmpresa);
            }

            SQL.Append(" (");
            SQL.Append(campoEmpresa);
            SQL.Append(" IS NULL OR ");
            SQL.Append(campoEmpresa);

            if (empresasAcesso.Count > 1)
            {
                SQL.Append(" IN (");
                SQL.Append(string.Join(",", empresasAcesso.ToArray()));
                SQL.Append(") ");
            }
            else
            {
                SQL.Append(" = " + empresaLogada.ToString());
            }

            SQL.Append(") ");

            return SQL.ToString();
        }
    }
}
