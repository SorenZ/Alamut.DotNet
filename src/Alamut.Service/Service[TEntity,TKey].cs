using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Service;

namespace Alamut.Service
{
    /// <inheritdoc />
    /// <summary>
    /// provice base service class 
    /// with access to database as readonly repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class Service<TEntity, TKey> : IService<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        public Service(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        protected IRepository<TEntity, TKey> Repository { get; }

        public IQueryRepository<TEntity, TKey> ReadOnlyRepository => this.Repository;
    }
}
