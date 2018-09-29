using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF.Repositories
{
    /// <inheritdoc />
    /// <summary>
    /// provide readonly repository based on sql server and EntityFramework (core)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class QueryRepository<TEntity,TKey> : IQueryRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly DbContext Context;

        public QueryRepository(DbContext context)
        {
            Context = context;
        }

        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();
        
        public virtual IQueryable<TEntity> Queryable => DbSet.AsNoTracking();

        public virtual TEntity Get(TKey id) 
            => DbSet.FirstOrDefault(q => q.Id.Equals(id));

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate) =>
            DbSet.FirstOrDefault(predicate);

        public virtual TResult Get<TResult>(TKey id) =>
            DbSet.Where(q => q.Id.Equals(id))
                .ProjectTo<TResult>()
                .FirstOrDefault();

        public virtual TResult Get<TResult>(Expression<Func<TEntity, bool>> predicate) =>
            DbSet.Where(predicate)
                .ProjectTo<TResult>()
                .FirstOrDefault();

        public virtual List<TEntity> GetAll() => DbSet.ToList();

        public List<TResult> GetAll<TResult>() =>
            DbSet.ProjectTo<TResult>()
                .ToList();

        public List<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate) =>
            DbSet.Where(predicate)
                .ToList();

        public List<TEntity> GetMany(IEnumerable<TKey> ids) =>
            DbSet.Where(q => ids.Contains(q.Id))
                .ToList();

        public List<TResult> GetMany<TResult>(Expression<Func<TEntity, bool>> predicate) =>
            DbSet.Where(predicate)
                .ProjectTo<TResult>()
                .ToList();

        public List<TResult> GetMany<TResult>(IEnumerable<TKey> ids) =>
            DbSet.Where(q => ids.Contains(q.Id))
                .ProjectTo<TResult>()
                .ToList();

        public IPaginated<TEntity> GetPaginated(PaginatedCriteria criteria = null) =>
            DbSet.ToPaginated(criteria ?? new PaginatedCriteria());
    }
}
