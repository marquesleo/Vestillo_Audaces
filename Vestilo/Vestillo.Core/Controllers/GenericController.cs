using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Controllers
{
    public class GenericController<TModel, TRepository>: IDisposable
        where TModel : class
        where TRepository : GenericRepository<TModel>
    {

        protected TRepository _repository = InstanciateRepository();

        //public GenericController()
        //{
        //    TRepository _repository = this.InstanciateRepository();
        //}

        public virtual void Save(TModel entity)
        {
            _repository.Save(entity);
        }

        public virtual TModel GetById(int id)
        {
            return _repository.Find(id);
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            return _repository.ListAll();
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Dispose()
        {
            if (_repository != null)
            {
                _repository.Dispose();
            }
        }
       
        public virtual void Validate(TModel entity)
        {

        }

        private void PreencherContator(ref TModel entity)
        {
            _repository.FillReferencia(ref entity);
        }

        private static TRepository InstanciateRepository()
        {
            return (TRepository)Activator.CreateInstance(typeof(TRepository));
        }
    }
}
