using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Structure;
using Alamut.Helpers.Linq;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF.Repositories
{
    public class Repository<TEntity> : QueryRepository<TEntity>,
        IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        public Repository(DbContext context) : base(context)
        { }

        public void Create(TEntity entity)
        {
            base.DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> list)
        {
            base.DbSet.AddRange(list);
        }

        public void Update(TEntity entity)
        {
            var entry = base.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
                base.DbSet.Attach(entity);

            entry.State = EntityState.Modified;
        }

        public void UpdateOne<TField>(string id, 
            Expression<Func<TEntity, TField>> memberExpression, 
            TField value)
        {
            var entity = base.DbSet.FirstOrDefault(q => q.Id == id);
            if (entity == null)
                return; //return ServiceResult.Error($"there is no item in {typeof(TEntity).Name} with id : {id}");

            var memberName = LambdaExpressions.GetName(memberExpression);

            entity.GetType().GetProperty(memberName).SetValue(entity,value);

            base.Context.SaveChanges();
        }

        public void UpdateOne<TFilter, TField>(Expression<Func<TEntity, bool>> filterExpression, 
            Expression<Func<TEntity, TField>> memberExpression, 
            TField value)
        {
            throw new NotImplementedException();
        }

        public void GenericUpdate(string id, Dictionary<string, dynamic> fieldset)
        {
            throw new NotImplementedException();
        }

        public void AddToList<TValue>(string id, 
            Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, 
            TValue value)
        {
            throw new NotImplementedException("Sql Repository doesn't support this method");
        }

        public void RemoveFromList<TValue>(string id, 
            Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, 
            TValue value)
        {
            throw new NotImplementedException("Sql Repository doesn't support this method");
        }

        public ServiceResult Delete(string id)
        {
            try
            {
                //string query = $"DELETE FROM [{typeof(TEntity).Name}] WHERE [ID]={id}";
                //var rows = base.Context.Database.ExecuteSqlCommand(query, id);
                //return ServiceResult.Okay($"{rows} item(s) successfully deleted.");

                var entity = base.DbSet.FirstOrDefault(q => q.Id == id);
                if (entity == null)
                    return ServiceResult.Error($"there is no item in {typeof (TEntity).Name} with id : {id}");

                base.DbSet.Remove(entity);
                var rows = base.Context.SaveChanges();

                return ServiceResult.Okay($"{rows} item(s) successfully deleted.");

            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public ServiceResult DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entities = base.DbSet.Where(predicate);
                base.DbSet.RemoveRange(entities);

                var rows = base.Context.SaveChanges();

                return ServiceResult.Okay($"{rows} item(s) successfully deleted.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
            
        }
        
    }
}
