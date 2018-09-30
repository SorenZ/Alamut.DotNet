using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using AutoMapper;

namespace Alamut.Service
{
    public class CrudService<TEntity, TKey> : Service<TEntity, TKey>,
        ICrudService<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        public CrudService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {  }


        public virtual ServiceResult<TKey> Create<TModel>(TModel model)
        { 
            var entity = Mapper.Map<TEntity>(model);
            
            return base.Repository.Create(entity);
        }

        public virtual ServiceResult Update<TModel>(TKey id, TModel model)
        {
            var entity = base.Repository.Get(id);

            if (entity == null)
                return ServiceResult.Error("There is no entity with Id : " + id);

            return base.Repository.Update(Mapper.Map(model, entity));
        }

        public ServiceResult UpdateOne<TField>(TKey id, Expression<Func<TEntity, TField>> memberExpression, TField value) => 
            base.Repository.UpdateOne(id, memberExpression,value);

        public virtual ServiceResult Delete(TKey id)
        {
            if (id == null)
                return ServiceResult.Error("Id could not be null");

            return base.Repository.Delete(id);
        }

        public virtual TResult Get<TResult>(TKey id) => 
            base.Repository.Get<TResult>(id);

        public List<TResult> GetMany<TResult>(IEnumerable<TKey> ids) => 
            base.Repository.GetMany<TResult>(ids);

        public List<TResult> GetMany<TResult>(Expression<Func<TEntity, bool>> predicate) => 
            base.Repository.GetMany<TResult>(predicate);
    }
}