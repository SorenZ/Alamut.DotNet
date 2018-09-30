using Alamut.Data.Entity;
using Alamut.Data.Repository;

namespace Alamut.Data.Service
{
    /// <summary>
    /// provide base service contract
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IService<TEntity, in TKey>  where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// provide a readonly repository in order to facilitate 
        /// access to data(readonly) from who that access to the service.
        /// </summary>
        IQueryRepository<TEntity, TKey> ReadOnlyRepository { get; }
    }
}
