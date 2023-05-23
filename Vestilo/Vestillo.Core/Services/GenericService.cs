using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;
using System.Reflection;

namespace Vestillo.Core.Services
{
    public abstract class GenericService<TModel, TRepository> : IService<TModel>, IDisposable
        where TModel : class
        where TRepository : IRepository<TModel>
    {
        protected TRepository _repository;

        public GenericService()
        {
            _repository = InstanciateRepository();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }


        public TModel Find(int id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TModel> ListAll()
        {
            return _repository.ListAll();
        }

        public void Save(TModel entity)
        {
            if ((entity as IModel).Id > 0)
            {
                Validate(entity, Operation.Update);
            }
            else
            {
                Validate(entity, Operation.Add);
                _repository.FillReferencia(ref entity);
            }

            _repository.Save(entity);
        }

        public virtual void Validate(TModel entity, Operation operation)
        {
            return;
        }

        public void Dispose()
        {
            if (_repository != null)
                _repository.Dispose();
        }

        private TRepository InstanciateRepository()
        {
            return (TRepository)Activator.CreateInstance(typeof(TRepository));
        }
    }
}
