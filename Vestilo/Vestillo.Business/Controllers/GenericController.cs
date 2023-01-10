using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System.Runtime.Serialization;
using System.Reflection;

namespace Vestillo.Business.Controllers
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

        public virtual void Save(ref TModel entity)
        {
            Validate(entity);
            PreencherContator(ref entity);
            _repository.Save(ref entity);
        }

        public virtual TModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual IEnumerable<TModel> GetById(int[] ids)
        {
            return _repository.GetById(ids);
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            return _repository.GetAll();
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

        public int GetIdPropertyModel(TModel entity)
        {
            return _repository.GetIdPropertyModel(entity);
        }

        private void PreencherContator(ref TModel entity)
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
                var controller = new Controllers.ContadorCodigoController();
                string contadorAtual = controller.GetProximo(contador);

                entity.GetType().GetProperty(nomeAtributo).SetValue(entity, contadorAtual, null);
            }
        }

        private static TRepository InstanciateRepository()
        {
            return (TRepository)Activator.CreateInstance(typeof(TRepository));
        }
    }
}
