using System;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }

        private readonly DbContext _context;

        //public IRepository<TEntity, TKey> CreateRepository<TEntity, TKey>()
        //    where TEntity : class, IEntity<TKey>, new()
        //{
        //    return new Repository<TEntity, TKey>(this._context);
        //}


        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class, IEntity, new()
        {
            //return new Repository<TEntity>(this._context);
            throw new NotImplementedException();
        }

        public void Commit()
        {

            try
            {
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                // log exception

                throw ex;
                // return service result.
            }
        }

        public void RollBack()
        {
            foreach (var entity in this._context.ChangeTracker.Entries())
            {
                if (entity.State == EntityState.Added)
                {
                    entity.State = EntityState.Detached;
                }

                if (entity.State == EntityState.Deleted || entity.State == EntityState.Modified)
                {
                    entity.State = EntityState.Unchanged;
                }
            }
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
