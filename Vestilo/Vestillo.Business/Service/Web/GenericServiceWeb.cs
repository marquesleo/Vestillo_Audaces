using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;
using System.Reflection;

namespace Vestillo.Business.Service.Web
{
    public class GenericServiceWeb<TModel, TRepository, TController> : IService<TModel, TRepository, TController>
        where TModel : class
        where TController : GenericController<TModel, TRepository>
        where TRepository : GenericRepository<TModel>
    {
        public string RequestUri { get; set; }

        public GenericServiceWeb(string requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(ref TModel entity)
        {
            var c = new ConnectionWebAPI<TModel>(VestilloSession.UrlWebAPI);
            var id = GetIdPropertyModel(entity);

            if (id > 0)
            {
                c.Post(RequestUri, ref entity);
            }
            else
            {
                c.Put(RequestUri, GetIdPropertyModel(entity), ref entity);
            }
        }

           public void Delete(int id)
        {
            var c = new ConnectionWebAPI<TModel>(VestilloSession.UrlWebAPI);
            c.Delete(RequestUri, id);
        }

        public TModel GetById(int id)
        {
            var c = new ConnectionWebAPI<TModel>(VestilloSession.UrlWebAPI);
            return c.Get(RequestUri, id);
        }

        public IEnumerable<TModel> GetAll()
        {
            var c = new ConnectionWebAPI<TModel>(VestilloSession.UrlWebAPI);
            return c.Get(RequestUri);
        }

        public IEnumerable<TModel> GetById(int[] ids)
        {
            var c = new ConnectionWebAPI<TModel>(VestilloSession.UrlWebAPI);
            return c.Get(RequestUri);
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
    }
}
