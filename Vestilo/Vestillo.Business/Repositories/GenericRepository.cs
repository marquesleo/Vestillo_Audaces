using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Vestillo.Business.Models;
using Vestillo.Business.Service;
using Vestillo.Connection;
using System.Reflection;
using Newtonsoft.Json;

namespace Vestillo.Business.Repositories
{
    public abstract class GenericRepository<TModel> : IDisposable where TModel : class
    {
        public enum TipoOperacaoLog
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public IConnection<TModel> _cn;

        public GenericRepository(IConnection<TModel> cn)
        {
            this._cn = cn;
        }

        public bool BeginTransaction()
        {
            return _cn.Provider.BeginTransaction();
        }

        public void RollbackTransaction()
        {
            _cn.Provider.RollbackTransaction();
        }

        public void CommitTransaction()
        {
            _cn.Provider.CommitTransaction();
        }


        public virtual void Save(ref TModel entity)
        {
            if (GetIdPropertyModel(entity) > 0)
            {
                _cn.ExecuteUpdate(entity);
                GerarLog(entity, TipoOperacaoLog.Update);
                //PendenciasRepository.VerificarPendencia(entity, GenericRepository<Pendencias>.TipoOperacaoLog.Update);
            }
            else
            {
                _cn.ExecuteInsert(ref entity);
                GerarLog(entity, TipoOperacaoLog.Insert);
                //PendenciasRepository.VerificarPendencia(entity, GenericRepository<Pendencias>.TipoOperacaoLog.Insert);
            }
        }

        public void Delete(int id)
        {
            TModel entity = GetById(id);
            string modulo = String.Empty;
            string RefDeletada = String.Empty;
            int ObjetoId = 0;


            if (entity != null)
            {

                foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
                {
                    modulo = table.Modulo ?? "";
                    break;
                }


                ObjetoId = GetIdPropertyModel(entity);

                if (modulo == "Ficha Técnica De Operação")
                {
                    var dadosItemFicha = new FichaTecnicaService().GetServiceFactory().GetById(ObjetoId);
                    if (dadosItemFicha != null)
                    {
                        var Item = new ProdutoService().GetServiceFactory().GetById(dadosItemFicha.ProdutoId);
                        RefDeletada = Item.Referencia;
                    }

                }
                else if (modulo == "Ficha Tecnica Material")
                {
                    var dadosItemFicha = new FichaTecnicaDoMaterialService().GetServiceFactory().GetById(ObjetoId);
                    if (dadosItemFicha != null)
                    {
                        var Item = new ProdutoService().GetServiceFactory().GetById(dadosItemFicha.ProdutoId);
                        RefDeletada = Item.Referencia;
                    }

                }
                else if (modulo == "Despesas Fixa e Variável")
                {
                    var dadosItemDespesa = new DespesaFixaVariavelService().GetServiceFactory().GetById(ObjetoId);
                    if (dadosItemDespesa != null)
                    {
                        RefDeletada = " Ano: " + dadosItemDespesa.Ano.ToString();
                    }

                }


            }

            _cn.ExecuteDelete(InstanciateTModel(), id);

            if (entity != null)
            {
                GerarLog(entity, TipoOperacaoLog.Delete, RefDeletada);
                PendenciasRepository.VerificarPendencia(entity, GenericRepository<Pendencias>.TipoOperacaoLog.Delete);
            }
        }

        public virtual TModel GetById(int id)
        {
            TModel entity = InstanciateTModel();
            _cn.ExecuteToModel(ref entity, id);
            return entity;
        }

        public IEnumerable<TModel> GetById(int[] ids)
        {
            TModel entity = InstanciateTModel();
            return _cn.ExecuteToList(InstanciateTModel(), ids);
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            return _cn.ExecuteToList(InstanciateTModel());
        }

        public void Dispose()
        {

        }

        private TModel InstanciateTModel()
        {
            return (TModel)Activator.CreateInstance(typeof(TModel));
        }

        public int GetIdPropertyModel(TModel entity)
        {
            int id = 0;

            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                foreach (Chave customAttribute in propertyInfo.GetCustomAttributes(typeof(Chave), false))
                {
                    id = int.Parse(propertyInfo.GetValue(entity).ToString());
                    break;
                }

                if (id > 0)
                    break;
            }

            return id;
        }

        public string GetReferenciaPropertyModel(TModel entity)
        {
            string referencia = "";

            PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                foreach (Contador customAttribute in propertyInfo.GetCustomAttributes(typeof(Contador), false))
                {
                    referencia = propertyInfo.GetValue(entity).ToString();
                    return referencia;
                }

                if (propertyInfo.Name.ToLower().Equals("abreviatura"))
                {
                    referencia = propertyInfo.GetValue(entity).ToString();
                    return referencia;
                }

                foreach (RegistroUnico customAttribute in propertyInfo.GetCustomAttributes(typeof(RegistroUnico), false))
                {
                    referencia = propertyInfo.GetValue(entity).ToString();
                    return referencia;
                }
            }

            return "";
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

            int empresaLogada = 0; //alterado Audaces, testar
            List<int> empresasAcesso = new List<int>();
            if (!ProviderFactory.IsAPI)
            {
                empresaLogada = VestilloSession.EmpresaLogada.Id;
                empresasAcesso = VestilloSession.EmpresaAcessoDados.Where(x => x.EmpresaId == empresaLogada
                                                                                  && (string.IsNullOrWhiteSpace(x.Tabela) || x.Tabela.ToLower() == tabela.ToLower())
                                                                                )
                                                                         .Select(x => x.EmpresaAcessoId)
                                                                         .Distinct()
                                                                         .ToList();
                
                empresasAcesso.Add(empresaLogada);
            }
            else
            {
                empresaLogada = Vestillo.Lib.Funcoes.GetIdEmpresaLogada;
                empresasAcesso.Add(empresaLogada);
            }
            //até aqui
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

        private void GerarLog(TModel entity, TipoOperacaoLog operacao, string RefDeletada = "")
        {
            try
            {

                if (entity != null && !(entity is Log))
                {

                    var log = new Log();
                    log.Operacao = (int)operacao;
                    if (!ProviderFactory.IsAPI) // alterado audaces testar
                        log.UsuarioId = VestilloSession.UsuarioLogado.Id;
                    else
                        log.UsuarioId = 2;

                    foreach (Tabela table in entity.GetType().GetCustomAttributes(typeof(Tabela), false))
                    {
                        log.Modulo = table.Modulo ?? "";
                        break;
                    }

                    switch (operacao)
                    {
                        case TipoOperacaoLog.Insert:
                            log.DescricaoOperacao = "Inclusão de ";
                            break;
                        case TipoOperacaoLog.Update:
                            log.DescricaoOperacao = "Alteração de ";
                            break;
                        case TipoOperacaoLog.Delete:
                            log.DescricaoOperacao = "Exclusão de ";
                            break;
                        default:
                            break;
                    }

                    string json = JsonConvert.SerializeObject(entity);
                    //string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(entity); alterado audaces
                    string referencia = GetReferenciaPropertyModel(entity);

                    log.ObjetoId = GetIdPropertyModel(entity);

                    if (log.Modulo == "Ficha Técnica De Operação" && String.IsNullOrEmpty(RefDeletada))
                    {
                        var dadosItemFicha = new FichaTecnicaService().GetServiceFactory().GetById(log.ObjetoId);
                        if (dadosItemFicha != null)
                        {
                            var Item = new ProdutoService().GetServiceFactory().GetById(dadosItemFicha.ProdutoId);
                            referencia = Item.Referencia;
                        }

                    }
                    else if (log.Modulo == "Ficha Técnica De Operação" && !String.IsNullOrEmpty(RefDeletada))
                    {

                        referencia = RefDeletada;

                    }
                    else if (log.Modulo == "Ficha Tecnica Material" && String.IsNullOrEmpty(RefDeletada))
                    {
                        var dadosItemFicha = new FichaTecnicaDoMaterialService().GetServiceFactory().GetById(log.ObjetoId);
                        if (dadosItemFicha != null)
                        {
                            var Item = new ProdutoService().GetServiceFactory().GetById(dadosItemFicha.ProdutoId);
                            referencia = Item.Referencia;
                        }

                    }
                    else if (log.Modulo == "Ficha Tecnica Material" && !String.IsNullOrEmpty(RefDeletada))
                    {

                        referencia = RefDeletada;

                    }

                    else if (log.Modulo == "Despesas Fixa e Variável" && String.IsNullOrEmpty(RefDeletada))
                    {
                        var dadosItemDespesa = new DespesaFixaVariavelService().GetServiceFactory().GetById(log.ObjetoId);
                        if (dadosItemDespesa != null)
                        {
                            referencia = " Ano: " + dadosItemDespesa.Ano.ToString();
                        }

                    }
                    else if (log.Modulo == "Despesas Fixa e Variável" && !String.IsNullOrEmpty(RefDeletada))
                    {
                        referencia = RefDeletada;

                    }

                    if (!string.IsNullOrEmpty(referencia))
                        log.DescricaoOperacao += log.Modulo + ": " + referencia;
                    else
                        log.DescricaoOperacao += log.Modulo;

                    log.Objeto = json;
                    log.Id = 0;

                    var logRepository = new LogRepository();
                    logRepository.Save(ref log);
                }
            }
            catch (Exception)
            {
                //throw ex;
            }
        }
    }
}
