using System;
using Alamut.Data.Entity;
using Alamut.Data.Repository;

namespace Alamut.Data.Sql
{
    /// <summary>
    /// Represents a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// provide basic repository for specific Entity and Key
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        //IRepository<TEntity, TKey> CreateRepository<TEntity, TKey>()
        //    where TEntity : class, IEntity<TKey>, new();

        /// <summary>
        /// provide basic repository for specific Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> CreateRepository<TEntity>()
            where TEntity : class, IEntity, new();

        /// <summary>
        /// Commits the works.
        /// </summary>
        void Commit();

        /// <summary>
        /// rolebacks the works.
        /// </summary>
        void RollBack();
    }
}
