using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Alamut.Data.Entity;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Alamut.Data.Structure;
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
            Expression<Func<TEntity, TField>> memberExpression, TField value)
        {
            throw new NotImplementedException();
        }

        public void UpdateOne<TFilter, TField>(Expression<Func<TEntity, bool>> filterExpression, 
            Expression<Func<TEntity, TField>> memberExpression, TField value)
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
            throw new NotImplementedException();
        }

        public void RemoveFromList<TValue>(string id, 
            Expression<Func<TEntity, IEnumerable<TValue>>> memberExpression, 
            TValue value)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Delete(string id)
        {
            try
            {
                string query = $"DELETE FROM [{typeof(TEntity).Name}] WHERE [ID]={id}";
                var rows = base.Context.Database.ExecuteSqlCommand(query, id);
                return ServiceResult.Okay($"{rows} item(s) successfully deleted.");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        
    }
}
