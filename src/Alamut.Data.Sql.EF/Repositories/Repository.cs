using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.SSOT;
using Alamut.Data.Structure;
using Alamut.Helpers.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Alamut.Data.Sql.EF.Repositories
{
    public class Repository<TEntity> : QueryRepository<TEntity>,
        IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        public Repository(DbContext context) : base(context)
        { }

        public virtual ServiceResult<string> Create(TEntity entity)
        {
            try
            {
                base.DbSet.Add(entity);
                var item = base.Context.SaveChanges();
                //return ServiceResult<string>.Okay(entity.Id, $"{item} item successfully created.");
                return ServiceResult<string>.Okay(Messages.ItemCreated);
            }
            catch (Exception ex)
            {
                return ServiceResult<string>.Exception(ex);
            }
        }

        public virtual ServiceResult AddRange(IEnumerable<TEntity> list)
        {
            try
            {
                base.DbSet.AddRange(list);
                var item = base.Context.SaveChanges();
                //return ServiceResult.Okay($"{item} item(s) successfully created.");
                return ServiceResult.Okay(Messages.ItemsCreated);
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult Update(TEntity entity)
        {
            var entry = base.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
                base.DbSet.Attach(entity);

            entry.State = EntityState.Modified;

            try
            {
                var item = base.Context.SaveChanges();
                return ServiceResult.Okay(Messages.ItemUpdated);
                //return ServiceResult.Okay($"{item} item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult UpdateOne<TField>(string id, Expression<Func<TEntity, TField>> memberExpression, TField value)
        {
            var entity = base.DbSet.FirstOrDefault(q => q.Id == id);
            if (entity == null)
               return ServiceResult.Error($"there is no item in {typeof(TEntity).Name} with id : {id}");

            try
            {
                var memberName = LambdaExpressions.GetName(memberExpression);
                entity.GetType().GetProperty(memberName).SetValue(entity,value);
                var item = base.Context.SaveChanges();
                return ServiceResult.Okay(Messages.ItemUpdated);
                //return ServiceResult.Okay($"{item} item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult UpdateOne<TFilter, TField>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TField>> memberExpression, TField value)
        {
            var entity = base.DbSet.FirstOrDefault(filterExpression);
            if (entity == null)
                return ServiceResult.Error($"there is no item in {typeof(TEntity).Name} with id : {filterExpression}");

            try
            {
                var memberName = LambdaExpressions.GetName(memberExpression);
                entity.GetType().GetProperty(memberName).SetValue(entity, value);
                var item = base.Context.SaveChanges();
                return ServiceResult.Okay(Messages.ItemUpdated);
                //return ServiceResult.Okay($"{item} item successfully updated.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult GenericUpdate(string id, Dictionary<string, dynamic> fieldset)
        {
            var entity = base.DbSet.FirstOrDefault(q => q.Id == id);
            if (entity == null)
                return ServiceResult.Error($"there is no item in {typeof(TEntity).Name} with id : {id}");

            try
            {
                foreach (var field in fieldset)
                    entity.GetType().GetProperty(field.Key).SetValue(entity, field.Value);

                var item = base.Context.SaveChanges();
                return ServiceResult.Okay(Messages.ItemUpdated);
                //return ServiceResult.Okay($"{item} item(s) successfully updated.");

            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult AddToList<TValue>(string id, Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            throw new NotImplementedException("Sql Repository doesn't support this method");
        }

        public virtual ServiceResult RemoveFromList<TValue>(string id, Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            throw new NotImplementedException("Sql Repository doesn't support this method");
        }

        public virtual ServiceResult Delete(string id)
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

                return ServiceResult.Okay(Messages.ItemDeleted);
                //return ServiceResult.Okay($"{rows} item(s) successfully deleted.");

            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public virtual ServiceResult DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var entities = base.DbSet.Where(predicate);
                base.DbSet.RemoveRange(entities);

                var rows = base.Context.SaveChanges();

                return ServiceResult.Okay(Messages.ItemDeleted);
                //return ServiceResult.Okay($"{rows} item(s) successfully deleted.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
            
        }
        
    }
}
