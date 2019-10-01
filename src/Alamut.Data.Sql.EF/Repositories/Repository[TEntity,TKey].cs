using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.SSOT;
using Alamut.Abstractions.Structure;
using Alamut.Helpers.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Alamut.Data.Sql.EF.Repositories
{
    public class Repository<TEntity,TKey> : QueryRepository<TEntity,TKey>,
        IRepository<TEntity,TKey>
        where TEntity : class, IEntity<TKey>, new()

    {
        public Repository(DbContext context) : base(context)
        { }

        public virtual Result<TKey> Create(TEntity entity, bool commit = true)
        {
            try
            {
                base.DbSet.Add(entity);
                
                if(commit)
                    { var item = base.Context.SaveChanges(); }

                return Result<TKey>.Okay(entity.Id,Messages.ItemCreated);
            }
            catch (Exception ex)
            {
                return Result<TKey>.Exception(ex);
            }
        }

        public virtual Result AddRange(IEnumerable<TEntity> list, bool commit = true)
        {
            try
            {
                base.DbSet.AddRange(list);
                if(commit)
                    { var item = base.Context.SaveChanges(); }

                return Result.Okay(Messages.ItemsCreated);
            }
            catch (Exception ex)
            {
                return Result.Exception(ex);
            }
        }

        public virtual Result Update(TEntity entity, bool commit = true)
        {
            var entry = base.Context.Entry(entity);

            if (entry.State == EntityState.Detached)
                base.DbSet.Attach(entity);

            entry.State = EntityState.Modified;

            try
            {
                if(commit)
                    { var item = base.Context.SaveChanges(); }

                return Result.Okay(Messages.ItemUpdated);
            }
            catch (Exception ex)
            {
                return Result.Exception(ex);
            }
        }

        public virtual Result UpdateOne<TField>(TKey id, 
            Expression<Func<TEntity, TField>> memberExpression, 
            TField value)
        {
            var entity = base.DbSet.FirstOrDefault(q => q.Id.Equals(id));
            if (entity == null)
                return Result.Error($"there is no item in {typeof(TEntity).Name} with id : {id}");

            try
            {
                var memberName = LambdaExpressions.GetName(memberExpression);
                entity.GetType().GetProperty(memberName).SetValue(entity,value);
                var item = base.Context.SaveChanges();
                return Result.Okay(Messages.ItemUpdated);
                //return Result.Okay($"{item} item successfully updated.");
            }
            catch (Exception ex)
            {
                return Result.Exception(ex);
            }
        }

        public virtual Result UpdateOne<TFilter, TField>(Expression<Func<TEntity, bool>> filterExpression, 
            Expression<Func<TEntity, TField>> memberExpression, 
            TField value)
        {
            var entity = base.DbSet.FirstOrDefault(filterExpression);
            if (entity == null)
                return Result.Error($"there is no item in {typeof(TEntity).Name} with id : {filterExpression}");

            try
            {
                var memberName = LambdaExpressions.GetName(memberExpression);
                entity.GetType().GetProperty(memberName).SetValue(entity, value);
                var item = base.Context.SaveChanges();
                return Result.Okay(Messages.ItemUpdated);
                //return Result.Okay($"{item} item successfully updated.");
            }
            catch (Exception ex)
            {
                return Result.Exception(ex);
            }
        }

        public virtual Result GenericUpdate(TKey id, 
            Dictionary<string, dynamic> fieldset)
        {
            var entity = base.DbSet.FirstOrDefault(q => q.Id.Equals(id));
            if (entity == null)
                return Result.Error($"there is no item in {typeof(TEntity).Name} with id : {id}");

            try
            {
                foreach (var field in fieldset)
                    entity.GetType().GetProperty(field.Key).SetValue(entity, field.Value);

                var item = base.Context.SaveChanges();
                return Result.Okay(Messages.ItemUpdated);
                //return Result.Okay($"{item} item(s) successfully updated.");

            }
            catch (Exception ex)
            {
                return Result.Exception(ex);
            }
        }

        public virtual Result AddToList<TValue>(TKey id, Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            throw new NotImplementedException("Sql Repository doesn't support this method");
        }

        public virtual Result RemoveFromList<TValue>(TKey id, Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, TValue value)
        {
            throw new NotImplementedException("Sql Repository doesn't support this method");
        }

        public virtual Result Delete(TKey id, bool commit = true)
        {
            try
            {
                //string query = $"DELETE FROM [{typeof(TEntity).Name}] WHERE [ID]={id}";
                //var rows = base.Context.Database.ExecuteSqlCommand(query, id);
                //return Result.Okay($"{rows} item(s) successfully deleted.");

                var entity = base.DbSet.FirstOrDefault(q => q.Id.Equals(id));
                if (entity == null)
                    return Result.Error($"there is no item in {typeof (TEntity).Name} with id : {id}");

                base.DbSet.Remove(entity);
                if(commit)
                    { var item = base.Context.SaveChanges(); }

                return Result.Okay(Messages.ItemDeleted);
                //return Result.Okay($"{rows} item(s) successfully deleted.");

            }
            catch (Exception ex)
            {
                return Result.Exception(ex);
            }
        }

        public virtual Result DeleteMany(Expression<Func<TEntity, bool>> predicate, 
            bool commit = true)
        {
            try
            {
                var entities = base.DbSet.Where(predicate);
                base.DbSet.RemoveRange(entities);

                if(commit)
                    { var item = base.Context.SaveChanges(); }

                return Result.Okay(Messages.ItemDeleted);
                //return Result.Okay($"{rows} item(s) successfully deleted.");
            }
            catch (Exception ex)
            {
                return Result.Exception(ex);
            }
            
        }
        
    }
}
