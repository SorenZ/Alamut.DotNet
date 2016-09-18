using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alamut.Data.Entity;
using Alamut.Data.Linq;
using Alamut.Data.Paging;
using Alamut.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF.Repositories
{
    /// <summary>
    /// provide readonly repository based on sql server and EntityFramework (core)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class QueryRepository<TEntity> : IQueryRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;

        public QueryRepository(DbContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> DbSet => _context.Set<TEntity>();

        public virtual IQueryable<TEntity> Queryable => DbSet.AsQueryable();

        public virtual TEntity Get(string id) =>
            DbSet.FirstOrDefault(q => q.Id == id);

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate) =>
            DbSet.FirstOrDefault(predicate);


        public virtual TResult Get<TResult>(string id, Expression<Func<TEntity, TResult>> projection) =>
            DbSet.Where(q => q.Id == id)
                .Select(projection)
                .FirstOrDefault();

        public virtual TResult Get<TResult>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection) =>
                DbSet.Where(predicate)
                    .Select(projection)
                    .FirstOrDefault();

        public virtual List<TEntity> GetAll() => DbSet.ToList();

        public List<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> projection) =>
            DbSet.Select(projection)
                .ToList();

        public List<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate) =>
            DbSet.Where(predicate)
                .ToList();


        public List<TEntity> GetMany(IEnumerable<string> ids) =>
            DbSet.Where(q => ids.Contains(q.Id))
                .ToList();

        public List<TResult> GetMany<TResult>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> projection) =>
                DbSet.Where(predicate)
                    .Select(projection)
                    .ToList();

        public List<TResult> GetMany<TResult>(IEnumerable<string> ids,
            Expression<Func<TEntity, TResult>> projection) =>
                DbSet.Where(q => ids.Contains(q.Id))
                    .Select(projection)
                    .ToList();

        public IPaginated<TEntity> GetPaginated(PaginatedCriteria criteria = null) =>
            DbSet.ToPaginated(criteria ?? new PaginatedCriteria());
    }
}
