using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;
using Alamut.Data.Structure;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Alamut.Service
{
    public class CrudService<TDocument> : Service<TDocument>,
        ICrudService<TDocument>
        where TDocument : IEntity
    {
        //protected IMapper Mapper { get; private set; }

        public CrudService(IRepository<TDocument> repository)
            : base(repository)
        {
            //Mapper = mapper;
        }

        public virtual ServiceResult<string> Create<TModel>(TModel model)
        {
            var entity = Mapper.Map<TDocument>(model);
            
            return base.Repository.Create(entity);

            //try
            //{
                
            //}
            //catch (Exception ex)
            //{
            //    return ServiceResult<string>.Exception(ex);
            //}

            //return ServiceResult<string>.Okay(entity.Id);
        }

        public virtual ServiceResult Update<TModel>(string id, TModel model)
        {
            var entity = base.Repository.Get(id);

            if (entity == null)
                return ServiceResult.Error("There is no entity with Id : " + id, 404);

            return base.Repository.Update(Mapper.Map(model, entity));

            //try
            //{
            //    base.Repository.Update(Mapper.Map(model, entity));
            //}
            //catch (Exception ex)
            //{
            //    return ServiceResult<string>.Exception(ex);
            //}

            //return ServiceResult.Okay();
        }

        public ServiceResult UpdateOne<TField>(string id, Expression<Func<TDocument, TField>> memberExpression, TField value)
        {
            return base.Repository.UpdateOne(id, memberExpression,value);
            

            //try
            //{
            //    base.Repository.UpdateOne(id, memberExpression,value);
            //}
            //catch (Exception ex)
            //{
            //    return ServiceResult<string>.Exception(ex);
            //}

            //return ServiceResult.Okay();
        }

        public virtual ServiceResult Delete(string id)
        {
            if (id == null)
                return ServiceResult.Error("Id could not be null");

            return base.Repository.Delete(id);
        }

        public virtual TResult Get<TResult>(string id) // // TODO : move to queryable repository to get rid of multi-language queryable
        {
            return base.Repository.Queryable
                .Where(q => q.Id == id)
                .ProjectTo<TResult>()
                .FirstOrDefault();
        }

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids) // TODO : move to queryable repository to get rid of multi-language queryable
        {
            return base.Repository.Queryable
                .Where(q => ids.Contains(q.Id))
                .ProjectTo<TResult>()
                .ToList();
        }

        public List<TResult> GetMany<TResult>(Expression<Func<TDocument, bool>> predicate)
        {
            return base.Repository.Queryable
                .Where(predicate)
                .ProjectTo<TResult>()
                .ToList();
        }
    }
    
}